using Entity.DbAccessLayer;
using Entity.ServiceLayer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Entity.Helpers;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

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
            services.AddControllersWithViews();
            //services.AddTransient<Database, SqlDatabase>();
            services.TryAddEnumerable(new[] {
                //ServiceDescriptor.Transient<IBaseDataAccess, BaseDataAccess>(),
                ServiceDescriptor.Transient<IDataBaseServiceLayer, DataBaseServiceLayer>(),
                ServiceDescriptor.Transient<IDataBaseRepository, DataBaseRepository>()
            });
            //services.AddTransient<IBaseDataAccess, BaseDataAccess>((_) => new BaseDataAccess(""));
            services.AddTransient<Database, SqlDatabase>((_) => new SqlDatabase(Configuration["ConnectionString:DevDb"].ToString()));
            //services.AddDbContext<DataBaseRepositoryContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("DevDb")));
            //services.Add(new ServiceDescriptor(typeof(Database), typeof(SqlDatabase), ServiceLifetime.Transient,));
            //services.Add(new ServiceDescriptor(typeof(IDataBaseServiceLayer), typeof(DataBaseServiceLayer), ServiceLifetime.Transient));
            //services.Add(new ServiceDescriptor(typeof(IDataBaseRepository), typeof(DataBaseRepository), ServiceLifetime.Transient));
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
