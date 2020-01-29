using Entity.DbAccessLayer;
using Entity.ServiceLayer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.Hosting;
using Entity.Helpers;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Tokens;

namespace WebApp
{
    public class Startup
    {
        public static string ConnectionString { get; private set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ConnectionString = Configuration.GetConnectionString("DevDb");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<OpenIdConnectOptions>(Configuration.GetSection("Authentication:Cognito"));
            var serviceProvider = services.BuildServiceProvider();
            var authOptions = serviceProvider.GetService<IOptions<OpenIdConnectOptions>>();
            services.AddControllersWithViews();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            }
            )
            .AddCookie()
            .AddOpenIdConnect(options =>
            {
                options.ResponseType = authOptions.Value.ResponseType;
                options.MetadataAddress = authOptions.Value.MetadataAddress;
                options.ClientId = authOptions.Value.ClientId;
                options.ClientSecret = authOptions.Value.ClientSecret;
                options.SaveTokens = authOptions.Value.SaveTokens;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = authOptions.Value.TokenValidationParameters.ValidateIssuer
                };
            });
            services.TryAddEnumerable(new[] {
                ServiceDescriptor.Transient<IDataBaseServiceLayer, DataBaseServiceLayer>(),
                ServiceDescriptor.Transient<IDataBaseRepository, DataBaseRepository>()
            });
            services.AddTransient<Database, SqlDatabase>((_) => new SqlDatabase(Configuration["ConnectionString:DevDb"].ToString()));
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
