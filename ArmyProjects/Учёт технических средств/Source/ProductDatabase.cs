using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
namespace Учёт_технических_средств.Source
{
    //Класс, хранящий набор изделий
    public class ProductDatabase
    {
        //конструктор по умолчанию
        public ProductDatabase()
        {
            products = new ObservableCollection<Product>();
        }
        //сама БД
        ObservableCollection<Product> products;

        public ObservableCollection<Product> Products
        {
            get
            {
                return products;

            }
        }

        //Поиск по базе данных
        public Product SearchInBase(string pointOfInterest)
        {

            Product result = new Product("","");
            foreach (Product p in products)
            {
                if (p.Contains(pointOfInterest))
                {
                    return p;
                }
            }
            return result;
        }

        //Добавление одного изделия
        public void AddProduct(Product src)
        {
            src.Number = products.Count()+1;
            products.Add(src);
            
        }

        //Слияние двух баз
        public void AddSeveralProducts(ProductDatabase src)
        {
            foreach(Product srcItem in src.products)
            {
                AddProduct(srcItem);
            }
        }

        //Удаление всех помеченных изделий
        public void DeleteChosenProducts()
        {
            int i = 0;
            while (i<products.Count)
            {
                Product src = products[i];
                if (src.ProductChosen== true)
                {
                    
                    products.Remove(src);
                    continue;
                }
                i++;
            }
            this.CheckNumeration();
        }

        //Перенос помеченных изделий в другую БД
        public void MoveChosenToAnotherBase(ProductDatabase dst)
        {
            int i=0;
            while(i<products.Count)
            {
                Product src = products[i];
                if (src.ProductChosen == true)
                {
                    src.ProductChosen = false;
                    dst.AddProduct(src);
                    products.Remove(src);
                    continue;
                }
                i++;
            }
            this.CheckNumeration();
        }
        
        //поддержка правильности нумерации
        public void CheckNumeration()
        {
            int i =1;
            foreach(Product src in products)
            {
                src.Number = i;
                i++;
            }
        }

        public void SelectAll(bool value)
        {
            foreach (Product src in products)
            {
                src.ProductChosen = value;
            }
        }
        public void setStatusToChosen(string stat)
        {
            
            foreach (Product src in products)
            {
                if (src.ProductChosen)
                {
                    src.State = stat;
                }
            }
        }
    }
}
