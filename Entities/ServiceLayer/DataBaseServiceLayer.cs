using Entity.DbAccessLayer;
using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Entity.ServiceLayer
{
    public class DataBaseServiceLayer : IDataBaseServiceLayer
    {
        private readonly IDataBaseRepository DataBaseRepository;

        public DataBaseServiceLayer(IDataBaseRepository DataBaseRepository)
        {
            this.DataBaseRepository = DataBaseRepository;
        }
        public List<Category> GetCategories()
        {
            return DataBaseRepository.GetCategories();
        }
    }
}
