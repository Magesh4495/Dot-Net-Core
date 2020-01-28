using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CatergoryName { get; set; }
        public string Description { get; set; }
        public byte[] Picture { get; set; }
    }
}
