using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.ServiceLayer
{
    public interface IDataBaseServiceLayer
    {
        List<Category> GetCategories();
    }
}
