using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Учёт_технических_средств.Source;

namespace Учёт_технических_средств.Helpers
{
    //фабрика изделий
    public static class ProductFactory
    {
        public static Product CreateProduct(string name="", string responsible="")
        {
            return new Product(name, responsible);
        }
    }
}
