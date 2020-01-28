using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.DbAccessLayer
{
    public interface IDataBaseRepository
    {
        List<Category> GetCategories();
    }
}
