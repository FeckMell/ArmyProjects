using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Учёт_технических_средств.Helpers;
namespace Учёт_технических_средств.Source
{
    //Класс, отвечающий за внутреннее представление изделий
    public class Product:BindableObject
    {
        public Product(string name="", string responsible="")
        {
            Number = 0;
            Name = name;
            Index = "";
            FactoryNumber = "";
            InventoryNumber = "";
            BbtNomenclature = "";
            Purpose = "";
            Manufacturer = "";
            Guarantee = "";
            ExploitationPeriod = "";
            ConfirmationDoc = "";
            OrderNumber = "";
            PrimaryCost = "";
            Aurum = 0;
            Argentum = 0;
            Mpg = 0;
            Platinum = 0;
            ResponsiblePerson = responsible;
            Condition = "";
            Write_off = 0;
            State = "По умолчанию";
            ProductChosen = false;
            
        }

        public Product( int number=0, string name="",string index="", string exploitationPlace="",string factoryNumber="",
            string inventoryNumber="", string bbtNomenclature="",string purpose="", string manufacturer="", 
            string productionDate="", string guarantee="",string endDate="", string exploitationPeriod="", 
            string confirmationDoc="",string confirmationNumber="",string receivingDate="", string orderNum="", 
            string exploitationDate="", string primaryCost="", double aurum=0, double argentum=0, double platinum=0, double mpg=0, 
            string resp="", string condition="",string serviceDate="", int write_off=0, string comment="",
            bool productChosen = false, string state = "По умолчанию")
        {
            this.number = number;
            this.name=name;
            this.index=index;
            this.exploitationPlace = exploitationPlace;
            this.factoryNumber=factoryNumber;
            this.inventoryNumber=inventoryNumber;
            this.bbtNomenclature=bbtNomenclature;
            this.purpose=purpose;
            this.manufacturer=manufacturer;
            this.guarantee=guarantee;
            this.exploitationPeriod=exploitationPeriod;
            this.confirmationDoc=confirmationDoc;
            this.confirmationNumber = confirmationNumber;
            this.orderNumber=orderNum;
            this.primaryCost=primaryCost;
            this.aurum=aurum;
            this.argentum=argentum;
            this.platinum=platinum;
            this.mpg=mpg;
            this.responsiblePerson=resp;
            this.condition=condition;
            this.write_off=write_off;
            this.comment=comment;
            this.productChosen=productChosen;
            this.state = state;
            this.productionDate = productionDate;
            this.endDate = endDate;
            this.exploitationDate = exploitationDate;
            this.receivingDate = receivingDate;
            this.serviceDate = serviceDate;

        }
        //Инфополя о изделии. Задаются как свойства.
        
        //Номер по списку
        private int number;
        public int Number 
        {
            get 
            {
                return number;
            }
            set 
            {
                SetProperty(ref number, value);
            }
        }

        //Наименование изделия
        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                SetProperty(ref name, value);
            }

        }
        
        //индекс
        private string index;
        public string Index
        {
            get
            {
                return index;
            }
            set 
            {
                SetProperty(ref index, value);
            }
        }


        //Место эксплуатации
        private string exploitationPlace;
        public string ExploitationPlace
        {
            get
            {
                return exploitationPlace;
            }
            set
            {
                SetProperty(ref exploitationPlace, value);
            }

        }

        //номер заводской
        private string factoryNumber;
        public string FactoryNumber
        {
            get
            {
                return factoryNumber;
            }
            set 
            {
                SetProperty(ref factoryNumber, value);
            }
        }

        

        //Инвентаризационный номер
        private string inventoryNumber;
        public string InventoryNumber
        {
            get
            {
                return inventoryNumber;
            }
            set
            {
                SetProperty(ref inventoryNumber, value);
            }
        }

        //номенклатура ВВТ
        private string bbtNomenclature;
        public string BbtNomenclature
        {
            get
            {
                return bbtNomenclature;
            }
            set
            {
                SetProperty(ref bbtNomenclature, value);
            }
        }

        //Краткое назначение изделия
        private string purpose;
        public string Purpose
        {
            get
            {
                return purpose;
            }
            set
            {
                SetProperty(ref purpose, value);
            }
        }

        //Организация-изготовитель
        private string manufacturer;
        public string Manufacturer
        {
            get
            {
                return manufacturer;
            }
            set
            {
                SetProperty(ref manufacturer, value);
            }
        }

        //Дата изготовления
        private string productionDate;
        public string ProductionDate 
        {
            get
            {
                return productionDate;
            }
            set
            {
                SetProperty(ref productionDate, value);
            }
        }

        //Срок гарантии
        private string guarantee;
        public string Guarantee
        {
            get
            {
                return guarantee;
            }
            set
            {
                SetProperty(ref guarantee, value);
            }
        }

        //Дата окончания обязательств
        private string endDate;
        public string EndDate
        {
            get
            {
                return endDate;
            }
            set
            {
                SetProperty(ref endDate, value);
            }
        }

        //Срок эксплуатации
        private string exploitationPeriod;
        public string ExploitationPeriod
        {
            get
            {
                return exploitationPeriod;
            }
            set
            {
                SetProperty(ref exploitationPeriod, value);
            }
        }

        //Наименование документа, подтверждающего принятие изделия на ответственное хранение
        private string confirmationDoc;
        public string ConfirmationDoc
        {
            get
            {
                return confirmationDoc;
            }
            set
            {
                SetProperty(ref confirmationDoc, value);
            }
        }

        private string confirmationNumber;
        public string ConfirmationNumber
        {
            get
            {
                return confirmationNumber;
            }
            set
            {
                SetProperty(ref confirmationNumber, value);
            }
        }

        //Дата принятия объекта на ответственное хранение(аренду)
        private string receivingDate;
        public string ReceivingDate
        {
            get
            {
                return receivingDate;
            }
            set
            {
                SetProperty(ref receivingDate, value);
            }
        }

        //№ приказа о вводе в эксплуатацию
        private string orderNumber;
        public string OrderNumber
        {
            get
            {
                return orderNumber;
            }
            set
            {
                SetProperty(ref orderNumber, value);
            }
        }

        //Дата приказа о вводе в эксплуатацию
         private string exploitationDate;
        public string ExploitationDate
        {
            get
            {
                return exploitationDate;
            }
            set
            {
                SetProperty(ref exploitationDate, value);
            }
        }

        //Первоначальная стоимость, руб
        private string primaryCost;
        public string PrimaryCost
        {
            get
            {
                return primaryCost;
            }
            set
            {
                SetProperty(ref primaryCost, value);
            }
        }

        //Драг.металлы, золото, грамм
        private double aurum ;    
        public double Aurum 
        {
            get
            {
                return aurum;
            }
            set
            {
                SetProperty(ref aurum , value);
            }
        }

        //Драг. металлы, серебро, грамм
        private double argentum;
        public double Argentum
        {
            get
            {
                return argentum;
            }
            set
            {
                SetProperty(ref argentum, value);
            }
        }

        //Драг. металлы, платина, грамм
        private double platinum;
        public double Platinum
        {
            get
            {
                return platinum;
            }
            set
            {
                SetProperty(ref platinum, value);
            }
        }

        //Драг. металлы, МПГ, грамм
        private double mpg;
        public double Mpg
        {
            get
            {
                return mpg;
            }
            set
            {
                SetProperty(ref mpg, value);
            }
        }

        //Мат. ответственное лицо
        private string responsiblePerson;
        public string ResponsiblePerson
        {
            get
            {
                return responsiblePerson;
            }
            set
            {
                SetProperty(ref responsiblePerson, value);
            }
        }

        //Состояние изделия
        private string condition;
        public string Condition
        {
            get
            {
                return condition;
            }
            set
            {
                SetProperty(ref condition, value);
            }
        }

        //Дата проведения последнего сервисного обслуживания
        private string serviceDate;
        public string ServiceDate
        {
            get
            {
                return serviceDate;
            }
            set
            {
                SetProperty(ref serviceDate, value);
            }
        }

        //Планируемый год списания
        private int write_off;
        public int Write_off
        {
            get
            {
                return write_off;
            }
            set
            {
                SetProperty(ref write_off, value);
            }
        }

        //Примечание
        private string comment;
        public string Comment
        {
            get
            {
                return comment;
            }
            set
            {
                SetProperty(ref comment, value);
            }
        }

        //Состояние(в смысле Списано и тд)
        private string state;
        public string State
        {
            get
            {
                return state;
            }
            set
            {
                SetProperty(ref state, value);
            }
        }

        

        private bool productChosen;
        public bool ProductChosen
        {
            get
            {
                return productChosen;
            }
            set
            {
                SetProperty(ref productChosen, value);
            }
        }

        public bool Contains(string subj)
        {


            PropertyInfo[] ProductPropertyInfo;
            ProductPropertyInfo = Type.GetType("Учёт_технических_средств.Source.Product").GetProperties();
            bool result = false;
            foreach (PropertyInfo prop in ProductPropertyInfo)
            {
                result = result | prop.GetValue(this).ToString().Contains(subj);
            }
            return result;
        }
    }
}
