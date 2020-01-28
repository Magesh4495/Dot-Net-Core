using Entity.Entities;
using Entity.Helpers;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace Entity.DbAccessLayer
{
    public class DataBaseRepository : IDataBaseRepository
    {
        private readonly Database db;
        public DataBaseRepository()
        {

        }
        public DataBaseRepository(Database db)
        {
            if (db == null)
            {
                throw new ArgumentNullException("db");
            }
            this.db = db;
        }
        public List<Category> GetCategories()
        {
            List<Category> CategoriesList = new List<Category>();
            using (DbCommand cmd = this.db.GetStoredProcCommand(StoredProcNames.GetCategories))
            {
                cmd.CommandTimeout = 120;
                using (IDataReader reader = this.db.ExecuteReader(cmd))
                {
                    while(reader.Read())
                    {
                        Category category = new Category
                        {
                            CategoryId = reader.GetValue<int>("CategoryID"),
                            CatergoryName = reader.GetValue<string>("CategoryName"),
                            Description = reader.GetValue<string>("Description"),
                            Picture =reader.GetValue<byte[]>("Picture")
                        };
                        CategoriesList.Add(category);
                    }
                }
                return CategoriesList;
            }

        }
    }
}
