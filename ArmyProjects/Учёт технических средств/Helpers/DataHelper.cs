using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel=Microsoft.Office.Interop.Excel;
using System.Xml.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Учёт_технических_средств.Source;
namespace Учёт_технических_средств.Helpers

{

    //Класс, отвечающий за работу с файлами и отражение на них внутренней логики программы
    public static class DataHelper
    {
        
        
        public static ProductDatabase LoadFromDb(string path)
        {
            throw new NotImplementedException();
        }

        public static ProductDatabase TestDataBase()
        {
            ProductDatabase result = new ProductDatabase();
            result.AddProduct(ProductFactory.CreateProduct("Компьютер", "Пронин"));
            result.AddProduct(ProductFactory.CreateProduct("Монитор", "Пелых"));
            result.AddProduct(ProductFactory.CreateProduct("Принтер", "Пронин"));

            return result;
        }

        public static ProductDatabase LoadFromXml(string path)
        {
            
            ProductDatabase result = new ProductDatabase();
            

            XDocument mainBaseDoc;
            try
            {
                mainBaseDoc = XDocument.Load(path);
            }
            catch (Exception ex)
            {
                DataController.ShowErrorWindow(ex.Message);
                return result;

            }
            var xmlList = mainBaseDoc.Descendants("Product").ToList();

            int i = 0;
            foreach (XElement elem in xmlList)
            {
                i++;
                int number = i;
                string name = (string)elem.Element("Name");
                string exploitationPlace = (string)elem.Element("ExploitationPlace");
                string index = (string)elem.Element("Index");
                string factoryNumber = (string)elem.Element("FactoryNumber");
                string inventoryNumber = (string)elem.Element("InventoryNumber");
                string bbtNomenclature = (string)elem.Element("BbtNomenclature");
                string purpose = (string)elem.Element("Purpose");
                string manufacturer = (string)elem.Element("Manufacturer");
                string guarantee = (string)elem.Element("Guarantee");
                string exploitationPeriod = (string)elem.Element("ExploitationPeriod");
                string confirmationDoc = (string)elem.Element("ConfirmationDoc");
                string confirmationNumber = (string)elem.Element("ConfirmationNumber");
                string orderNumber = (string)elem.Element("OrderNumber");
                string primaryCost = (string)elem.Element("PrimaryCost");
                double aurum = (double)elem.Element("Aurum");
                double argentum = (double)elem.Element("Argentum");
                double mpg = (double)elem.Element("Mpg");
                double platinum = (double)elem.Element("Platinum");
                string responsiblePerson = (string)elem.Element("ResponsiblePerson");
                string condition = (string)elem.Element("Condition");
                string serviceDate = (string)elem.Element("ServiceDate");
                int write_off = (int)elem.Element("Write_off");
                string comment = (string)elem.Element("Comment");
                string state = (string)elem.Element("State");
                string exploitationDate = (string)elem.Element("ExploitationDate");
                string endDate = (string)elem.Element("EndDate");
                string receivingDate = (string)elem.Element("ReceivingDate");
                string productionDate = (string)elem.Element("ProductionDate");
                
                
                Product prod = new Product(number,name,index,exploitationPlace,factoryNumber,inventoryNumber,bbtNomenclature,
                    purpose,manufacturer,productionDate,guarantee,endDate,exploitationPeriod,confirmationDoc,
                    confirmationNumber,receivingDate, orderNumber,exploitationDate,primaryCost, aurum,argentum,platinum,mpg,
                    responsiblePerson,condition,serviceDate,write_off,comment,false,state);
                result.AddProduct(prod);
            }

            RaiseDatabaseUpdateInfo();
            return result;
        }

        public static void SaveToXml(ProductDatabase src, string path)
        {
            XElement productsDoc =
                new XElement("Products",
                    from c in src.Products
                    select new XElement("Product",
                        new XElement("Name", c.Name),
                        new XElement("Index", c.Index),
                        new XElement("ExploitationPlace", c.ExploitationPlace),
                        new XElement("FactoryNumber", c.FactoryNumber),
                        new XElement("InventoryNumber", c.InventoryNumber),
                        new XElement("BbtNomenclature", c.BbtNomenclature),
                        new XElement("Purpose", c.Purpose),
                        new XElement("Manufacturer", c.Manufacturer),
                        new XElement("ProductionDate", c.ProductionDate),
                        new XElement("Guarantee", c.Guarantee),
                        new XElement("EndDate", c.EndDate),
                        new XElement("ExploitationPeriod", c.ExploitationPeriod),
                        new XElement("ConfirmationDoc", c.ConfirmationDoc),
                        new XElement("ReceivingDate", c.ReceivingDate),
                        new XElement("OrderNumber", c.OrderNumber),
                        new XElement("ExploitationDate", c.ExploitationDate),
                        new XElement("PrimaryCost", c.PrimaryCost),
                        new XElement("Aurum", c.Aurum),
                        new XElement("Argentum", c.Argentum),
                        new XElement("Platinum", c.Platinum),
                        new XElement("Mpg", c.Mpg),
                        new XElement("ResponsiblePerson", c.ResponsiblePerson),
                        new XElement("Condition", c.Condition),
                        new XElement("ServiceDate", c.ServiceDate),
                        new XElement("Write_off", c.Write_off),
                        new XElement("Comment", c.Comment),
                        new XElement("State", c.State)
                        ));
            productsDoc.Save(path);

        }
        
        public static ProductDatabase LoadFromXls(string path)
        {
            

            Process[] excelProcsOld = Process.GetProcessesByName("EXCEL");

            Excel.Application excelApp = new Excel.Application();
            Excel.Workbooks workbooks;
            Excel.Workbook excelBook;
            workbooks = excelApp.Workbooks;
            excelBook = workbooks.Add(path);
            Excel.Worksheet excelSheet = (Excel.Worksheet)(excelBook.Sheets[1]);

            int lastRow = excelSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell).Row;
            int lastColumnNumber = excelSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell).Column;
            string lastColumn = DigToSymb(lastColumnNumber);
            int beginningIndex = Int32.Parse(DataController.dataContext.BeginRange);
            
            RaiseProgressInfo(0);
            int prevProg = 0;
            
            ProductDatabase result = new ProductDatabase();
            int maxIndex = lastRow;
            int divided = maxIndex / 10;
            System.Array MyValues = (System.Array)excelSheet.get_Range("A1" , lastColumn + lastRow.ToString()).Cells.Value;

            for (int i = beginningIndex; i <= lastRow;i++ )
            {
                for (int j=1;j<lastColumnNumber; j++)
                {
                    if (MyValues.GetValue(i,j) == null)
                        MyValues.SetValue("",i,j);
                }
            }


                for (int index = beginningIndex; index <= lastRow; index++)
                {
                    if ((index / divided) != prevProg)
                    {
                        prevProg = index / divided;
                        RaiseProgressInfo(prevProg * 10);

                    }



                    /*System.Array MyValues = (System.Array)excelSheet.get_Range("A" +
                       index.ToString(), lastColumn + index.ToString()).Cells.Value;*/



                    string name = "", ind = "", exPlace = "", facNum = "", invNum = "", bbtNom = "", purp = "", manufac = "",
                    prodDate = "", guar = "", endDate = "", exPer = "", confDoc = "", confNum = "", recDate = "", ordNum = "",
                    expDate = "", primCost = "", resp = "", cond = "", servDate = "", comm = "";
                    double aur = 0, arg = 0, plat = 0, mpg = 0;
                    int write = 0;


                    try
                    {
                        name = MyValues.GetValue(index, SymbToDig(DataController.dataContext.NameColumn)).ToString();
                        ind = MyValues.GetValue(index, SymbToDig(DataController.dataContext.IndexColumn)).ToString();
                        exPlace = MyValues.GetValue(index, SymbToDig(DataController.dataContext.ExploitationPlaceColumn)).ToString();
                        facNum = MyValues.GetValue(index, SymbToDig(DataController.dataContext.FactoryNumberColumn)).ToString();
                        invNum = MyValues.GetValue(index, SymbToDig(DataController.dataContext.InventoryNumberColumn)).ToString();
                        bbtNom = MyValues.GetValue(index, SymbToDig(DataController.dataContext.BbtNomenclatureColumn)).ToString();
                        purp = MyValues.GetValue(index, SymbToDig(DataController.dataContext.PurposeColumn)).ToString();
                        manufac = MyValues.GetValue(index, SymbToDig(DataController.dataContext.ManufacturerColumn)).ToString();
                        prodDate = MyValues.GetValue(index, SymbToDig(DataController.dataContext.ProductionDateColumn)).ToString();
                        guar = MyValues.GetValue(index, SymbToDig(DataController.dataContext.GuaranteeColumn)).ToString();
                        endDate = MyValues.GetValue(index, SymbToDig(DataController.dataContext.EndDateColumn)).ToString();
                        exPer = MyValues.GetValue(index, SymbToDig(DataController.dataContext.ExploitationPeriodColumn)).ToString();
                        confDoc = MyValues.GetValue(index, SymbToDig(DataController.dataContext.ConfirmationDocColumn)).ToString();
                        confNum = MyValues.GetValue(index, SymbToDig(DataController.dataContext.ConfirmationNumberColumn)).ToString();
                        recDate = MyValues.GetValue(index, SymbToDig(DataController.dataContext.ReceivingDateColumn)).ToString();
                        ordNum = MyValues.GetValue(index, SymbToDig(DataController.dataContext.OrderNumberColumn)).ToString();
                        expDate = MyValues.GetValue(index, SymbToDig(DataController.dataContext.ExploitationDateColumn)).ToString();
                        primCost = MyValues.GetValue(index, SymbToDig(DataController.dataContext.PrimaryCostColumn)).ToString();
                        resp = MyValues.GetValue(index, SymbToDig(DataController.dataContext.ResponsiblePersonColumn)).ToString();
                        cond = MyValues.GetValue(index, SymbToDig(DataController.dataContext.ConditionColumn)).ToString();
                        servDate = MyValues.GetValue(index, SymbToDig(DataController.dataContext.ServiceDateColumn)).ToString();
                        comm = MyValues.GetValue(index, SymbToDig(DataController.dataContext.CommentColumn)).ToString();

                        Double.TryParse(MyValues.GetValue(index, SymbToDig(DataController.dataContext.AurumColumn)).ToString(), out aur);
                        Double.TryParse(MyValues.GetValue(index, SymbToDig(DataController.dataContext.ArgentumColumn)).ToString(), out arg);
                        Double.TryParse(MyValues.GetValue(index, SymbToDig(DataController.dataContext.PlatinumColumn)).ToString(), out plat);
                        Double.TryParse(MyValues.GetValue(index, SymbToDig(DataController.dataContext.MpgColumn)).ToString(), out mpg);
                        Int32.TryParse(MyValues.GetValue(index, SymbToDig(DataController.dataContext.Write_offColumn)).ToString(), out write);
                        /*facPlace = MyValues.GetValue(1, 1).ToString();
                        name=MyValues.GetValue(1, 2).ToString();
                        ind = MyValues.GetValue(1, 3).ToString();
                        facNum = MyValues.GetValue(1, 4).ToString();
                        invNum = "";
                        bbtNom = MyValues.GetValue(1, 8).ToString();
                        purp = MyValues.GetValue(1, 9).ToString();
                        manufac = MyValues.GetValue(1, 10).ToString();
                        guar = MyValues.GetValue(1, 12).ToString();
                        exPer = MyValues.GetValue(1, 15).ToString();
                        confDoc=MyValues.GetValue(1, 16).ToString();
                        confNum = MyValues.GetValue(1, 17).ToString();
                        ordNum = MyValues.GetValue(1, 21).ToString();
                        primCost = MyValues.GetValue(1, 23).ToString();
                        resp = MyValues.GetValue(1, 28).ToString();
                        cond = MyValues.GetValue(1, 33).ToString();
                        */

                    }
                    catch (Exception ex)
                    {
                        DataController.ShowErrorWindow("Строка " + index.ToString() + "не импортирована");
                        continue;
                    }

                    result.AddProduct(new Product(0, name, ind, exPlace, facNum, invNum, bbtNom, purp, manufac, prodDate, guar, endDate, exPlace, confDoc,
                        confNum, recDate, ordNum, expDate, primCost, aur, arg, plat, mpg, resp, cond, servDate, write, comm));


                    


                }
            Marshal.ReleaseComObject(excelSheet);
            //Marshal.ReleaseComObject(sheets);
            
            excelBook.Close(true);
            Marshal.ReleaseComObject(excelBook);
            Marshal.ReleaseComObject(workbooks);
            excelApp.Quit();
            Marshal.ReleaseComObject(excelApp);
            excelSheet = null;
            excelBook = null;
            excelApp = null;

            Process[] excelProcsNew = Process.GetProcessesByName("EXCEL");
            foreach (Process procNew in excelProcsNew)
            {
                int exist = 0;
                foreach (Process procOld in excelProcsOld)
                {
                    if (procNew.Id == procOld.Id)
                    {
                        exist++;
                    }
                }
                if (exist == 0)
                {
                    procNew.Kill();
                }
            }


            RaiseDatabaseUpdateInfo();
            return result;
        }

        internal static void SaveToXls(ProductDatabase productDatabase, string filename)
        {
            Process[] excelProcsOld = Process.GetProcessesByName("EXCEL");


            #region Запуск Эксель
            Excel.Application excelApp = new Excel.Application();
            Excel.Workbooks workbooks;
            Excel.Workbook excelBook;
            workbooks = excelApp.Workbooks;
            excelBook = workbooks.Add();
            Excel.Worksheet excelSheet = (Excel.Worksheet)(excelBook.Sheets[1]);
            //excelSheet.get_Range("B1", "B1").EntireColumn.get_Resize(Type.Missing,40); //для столбца
            #endregion

            #region Общий стиль
            Excel.Range excelcells = (Excel.Range)excelSheet.Columns["A:AC", Type.Missing];
            excelcells.ColumnWidth = 25;
            excelcells.HorizontalAlignment = Excel.Constants.xlCenter;
            excelcells.VerticalAlignment = Excel.Constants.xlCenter;
            excelcells.Font.Name = "Times New Roman";
            excelcells.Font.Size = 10;
            //excelcells = (Excel.Range)excelSheet.Rows[Type.Missing, "1:"+(lastRow-1).ToString()];
            excelcells.Cells.WrapText = true;
            excelcells.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            excelcells.Borders.ColorIndex = 0;
            excelcells.Borders.Weight = Excel.XlBorderWeight.xlThin;
            #endregion 

            #region Запись шапки
            int lastRow = 1;
            excelSheet.Cells[lastRow, 1] = "№ п/п";
            excelSheet.Cells[lastRow, 2] = "Место эксплуатации изделия";
            excelSheet.Cells[lastRow, 3] = "Наименование изделия";
            excelSheet.Cells[lastRow, 4] = "Индекс";
            excelSheet.Cells[lastRow, 5] = "Номер заводской";
            excelSheet.Cells[lastRow, 6] = "Номер инвентарный";
            excelSheet.Cells[lastRow, 7] = "Номенклатура ВВТ";
            excelSheet.Cells[lastRow, 8] = "Краткое назначение изделия";
            excelSheet.Cells[lastRow, 9] = "Организация-изготовитель";
            excelSheet.Cells[lastRow, 10] = "Дата изготовления(дд.мм.гггг)";
            excelSheet.Cells[lastRow, 11] = "Срок гарантии";
            excelSheet.Cells[lastRow, 12] = "Дата окончания обязательств (дд.мм.гггг)";
            excelSheet.Cells[lastRow, 13] = "Срок эксплуатации изделия";
            excelSheet.Cells[lastRow, 14] = "Наименование документа, подтверждающий принятие изделия на ответственное хранение (аренду)";
            excelSheet.Cells[lastRow, 15] = "Номер документа, принятия  на ответственное хранение (аренду)";
            excelSheet.Cells[lastRow, 16] = "Дата принятия объекта на ответственное хранение (аренду) (дд.мм.гггг)";
            excelSheet.Cells[lastRow, 17] = "№ приказа о вводе в эксплуатацию";
            excelSheet.Cells[lastRow, 18] = "Дата приказа о вводе в эксплуатацию (дд.мм.гггг)";
            excelSheet.Cells[lastRow, 19] = "Первоначальная стоимость, руб.";
            excelSheet.Cells[lastRow, 20] = "Драг. мелаллы: Золото,  грамм";
            excelSheet.Cells[lastRow, 21] = "Драг. мелаллы: Серебро,  грамм";
            excelSheet.Cells[lastRow, 22] = "Драг. мелаллы: Платина,  грамм";
            excelSheet.Cells[lastRow, 23] = "Драг. мелаллы: Мпг,  грамм";
            excelSheet.Cells[lastRow, 24] = "Материально ответственное лицо (ФИО)";
            excelSheet.Cells[lastRow, 25] = "Состояние изделия";
            excelSheet.Cells[lastRow, 26] = "Дата проведения последнего сервисного обслуживания";
            excelSheet.Cells[lastRow, 27] = "Планируемый год списания";
            excelSheet.Cells[lastRow, 28] = "Примечание";
            excelSheet.Cells[lastRow, 29] = "Статус";
            

            lastRow++;

            for(int i = 1;i<=29;i++)
            {
                excelSheet.Cells[lastRow, i] = i;
            }

            lastRow++;
            #endregion

            #region Заполнение ячеек
            foreach (Product src in productDatabase.Products)
            {
                excelSheet.Cells[lastRow, 1] = src.Number;
                excelSheet.Cells[lastRow, 2] = src.ExploitationPlace;
                excelSheet.Cells[lastRow, 3] = src.Name;
                excelSheet.Cells[lastRow, 4] = src.Index;
                excelSheet.Cells[lastRow, 5] = src.FactoryNumber;
                excelSheet.Cells[lastRow, 6] = src.InventoryNumber;
                excelSheet.Cells[lastRow, 7] = src.BbtNomenclature;
                excelSheet.Cells[lastRow, 8] = src.Purpose;
                excelSheet.Cells[lastRow, 9] = src.Manufacturer;
                excelSheet.Cells[lastRow, 10] = src.ProductionDate;
                excelSheet.Cells[lastRow, 11] = src.Guarantee;
                excelSheet.Cells[lastRow, 12] = src.EndDate;
                excelSheet.Cells[lastRow, 13] = src.ExploitationPeriod;
                excelSheet.Cells[lastRow, 14] = src.ConfirmationDoc;
                excelSheet.Cells[lastRow, 15] = src.ConfirmationNumber;
                excelSheet.Cells[lastRow, 16] = src.ReceivingDate;
                excelSheet.Cells[lastRow, 17] = src.OrderNumber;
                excelSheet.Cells[lastRow, 18] = src.ExploitationDate;
                excelSheet.Cells[lastRow, 19] = src.PrimaryCost;
                excelSheet.Cells[lastRow, 20] = src.Aurum;
                excelSheet.Cells[lastRow, 21] = src.Argentum;
                excelSheet.Cells[lastRow, 22] = src.Platinum;
                excelSheet.Cells[lastRow, 23] = src.Mpg;
                excelSheet.Cells[lastRow, 24] = src.ResponsiblePerson;
                excelSheet.Cells[lastRow, 25] = src.Condition;
                excelSheet.Cells[lastRow, 26] = src.ServiceDate;
                excelSheet.Cells[lastRow, 27] = src.Write_off;
                excelSheet.Cells[lastRow, 28] = src.Comment;
                excelSheet.Cells[lastRow, 29] = src.State;

                lastRow++;
            }

            #endregion
            
            
            //Автоподгон по ширине
            excelcells.AutoFit();
                     
            
            //Подцветка строки и столбца с нумерацией
            excelcells = (Excel.Range)excelSheet.Columns["A:A", Type.Missing];
            excelcells.Interior.ColorIndex = 34;

            excelcells = (Excel.Range)excelSheet.Rows["2:2",Type.Missing];
            excelcells.Interior.ColorIndex = 34;
           
            //Сохранение файла
           // excelBook.PrintPreview();
            excelBook.SaveAs(@filename, Excel.XlFileFormat.xlExcel8);
            Marshal.ReleaseComObject(excelSheet);
            excelBook.Close(true);
            Marshal.ReleaseComObject(excelBook);
            Marshal.ReleaseComObject(workbooks);
            excelApp.Quit();
            Marshal.ReleaseComObject(excelApp);
            excelSheet = null;
            excelBook = null;
            excelApp = null;

            //убиваем все рабочие процессы Экселя
            Process[] excelProcsNew = Process.GetProcessesByName("EXCEL");
            foreach (Process procNew in excelProcsNew)
            {
                int exist = 0;
                foreach (Process procOld in excelProcsOld)
                {
                    if (procNew.Id == procOld.Id)
                    {
                        exist++;
                    }
                }
                if (exist == 0)
                {
                    procNew.Kill();
                }
            }



            
        }
        
        public static BackEnd LoadXmlContext(string filename)
        {
            BackEnd result = new BackEnd();
            XDocument settings;
            try
            {
                settings = XDocument.Load(filename);
            }
            catch
            {
                DataController.ShowErrorWindow("Файл с настройками не обнаружен. Установлены настройки по умолчанию");
                return result;
            }
            var xmlList = settings.Descendants("Settings").ToList();
            foreach (XElement elem in xmlList)
            {
                result.ExploitationPlaceChosen = (bool?)elem.Element("ExploitationPlaceChosen");
                result.IndexChosen = (bool?)elem.Element("IndexChosen");
                result.FactoryNumberChosen = (bool?)elem.Element("FactoryNumberChosen");
                result.InventoryNumberChosen = (bool?)elem.Element("InventoryNumberChosen");
                result.BbtNomenclatureChosen = (bool?)elem.Element("BbtNomenclatureChosen");
                result.PurposeChosen = (bool?)elem.Element("PurposeChosen");
                result.ManufacturerChosen = (bool?)elem.Element("ManufacturerChosen");
                result.ProductionDateChosen = (bool?)elem.Element("ProductionDateChosen");
                result.GuaranteeChosen = (bool?)elem.Element("GuaranteeChosen");
                result.EndDateChosen = (bool?)elem.Element("EndDateChosen");
                result.ExploitationPeriodChosen = (bool?)elem.Element("ExploitationPeriodChosen");
                result.ConfirmationDocChosen = (bool?)elem.Element("ConfirmationDocChosen");
                result.ReceivingDateChosen = (bool?)elem.Element("ReceivingDateChosen");
                result.OrderNumberChosen = (bool?)elem.Element("OrderNumberChosen");
                result.ExploitationDateChosen = (bool?)elem.Element("ExploitationDateChosen");
                result.PrimaryCostChosen = (bool?)elem.Element("PrimaryCostChosen");
                result.AurumChosen = (bool?)elem.Element("AurumChosen");
                result.ArgentumChosen = (bool?)elem.Element("ArgentumChosen");
                result.PlatinumChosen = (bool?)elem.Element("PlatinumChosen");
                result.MpgChosen = (bool?)elem.Element("MpgChosen");
                result.ResponsiblePersonChosen = (bool?)elem.Element("ResponsiblePersonChosen");
                result.ConditionChosen = (bool?)elem.Element("ConditionChosen");
                result.ServiceDateChosen = (bool?)elem.Element("ServiceDateChosen");
                result.Write_offChosen = (bool?)elem.Element("Write_offChosen");
                result.CommentChosen = (bool?)elem.Element("CommentChosen");
                result.StateChosen = (bool?)elem.Element("StateChosen");
                result.DefaultResponsible = (string)elem.Element("DefaultResponsible");
                result.DefaultExpluatationPlace = (string)elem.Element("DefaultExpluatationPlace");

            }
            return result;

        }
        public static void SaveXmlContext(BackEnd dataContext, string filename)
        {
            XElement settingsDoc = new XElement("Settings",
                new XElement("DefaultResponsible", dataContext.DefaultResponsible),
                new XElement("DefaultExpluatationPlace", dataContext.DefaultExpluatationPlace),
                new XElement("ExploitationPlaceChosen", dataContext.ExploitationPlaceChosen),
                new XElement("IndexChosen", dataContext.IndexChosen),
                new XElement("FactoryNumberChosen", dataContext.FactoryNumberChosen),
                new XElement("InventoryNumberChosen", dataContext.InventoryNumberChosen),
                new XElement("BbtNomenclatureChosen", dataContext.BbtNomenclatureChosen),
                new XElement("PurposeChosen", dataContext.PurposeChosen),
                new XElement("ManufacturerChosen", dataContext.ManufacturerChosen),
                new XElement("ProductionDateChosen", dataContext.ProductionDateChosen),
                new XElement("GuaranteeChosen", dataContext.GuaranteeChosen),
                new XElement("EndDateChosen", dataContext.EndDateChosen),
                new XElement("ExploitationPeriodChosen", dataContext.ExploitationPeriodChosen),
                new XElement("ConfirmationDocChosen", dataContext.ConfirmationDocChosen),
                new XElement("ReceivingDateChosen", dataContext.ReceivingDateChosen),
                new XElement("OrderNumberChosen", dataContext.OrderNumberChosen),
                new XElement("ExploitationDateChosen", dataContext.ExploitationDateChosen),
                new XElement("PrimaryCostChosen", dataContext.PrimaryCostChosen),
                new XElement("AurumChosen", dataContext.AurumChosen),
                new XElement("ArgentumChosen", dataContext.ArgentumChosen),
                new XElement("PlatinumChosen", dataContext.PlatinumChosen),
                new XElement("MpgChosen", dataContext.MpgChosen),
                new XElement("ResponsiblePersonChosen", dataContext.ResponsiblePersonChosen),
                new XElement("ConditionChosen", dataContext.ConditionChosen),
                new XElement("ServiceDateChosen", dataContext.ServiceDateChosen),
                new XElement("Write_offChosen", dataContext.Write_offChosen),
                new XElement("CommentChosen", dataContext.CommentChosen),
                new XElement("StateChosen", dataContext.StateChosen)


                );
            settingsDoc.Save(filename);
        }

        public  static event EventHandler<ProgressInfoEventArgs> ProgressInfo;

        public static event EventHandler<DataBaseUpdatedInfoEventArgs> DataBaseUpdatedInfo;

        public static void RaiseDatabaseUpdateInfo()
        {
            EventHandler<DataBaseUpdatedInfoEventArgs> databaseUpdateInfo = DataBaseUpdatedInfo;
            if (databaseUpdateInfo!=null)
            {
                databaseUpdateInfo(new Object(), new DataBaseUpdatedInfoEventArgs());
            }
        }
        public static void RaiseProgressInfo(int curProgress)
        {
            EventHandler<ProgressInfoEventArgs> progressInfo = ProgressInfo;
            if (progressInfo != null)
            {
                progressInfo(new Object(), new ProgressInfoEventArgs(curProgress));
            }
        }

        private static int SymbToDig(string src)
        {
            int result=0;
            if (src.Length == 0 | src.Length > 2)
                throw new Exception("Неправильный входной формат столбца: больше 2 символов");
            char c;
            if (src.Length == 1)
            {
                c = src[0];
                result = c - 'A'+1;
                if (result > 0 && result <= 26)
                    return result;
                else
                    throw new Exception("Неправильный входной формат столбца: допустимы только заглавные латинские буквы");

            }
            else
            {
                c = src[1];
                result = c - 'A' + 1;
                if (result >= 0 && result < 26)
                {
                    int a = src[0] - 'A' + 1;
                    if (a > 0 && a <= 26)
                    {
                        result += 26 * a;
                        return result;
                    }
                    else
                        throw new Exception("Неправильный входной формат столбца: допустимы только заглавные латинские буквы");
                }
                else
                    throw new Exception("Неправильный входной формат столбца: допустимы только заглавные латинские буквы");
            }
        }
        

        private static string DigToSymb(int src)
        {
            string result="";

            while (src>0)
            {
                int a = src % 26 - 1;
                char c = (char)('A' + (char)a);
                result += c;
                src = src / 26;
            }

            string final = "";
            for(int i = result.Length-1;i>=0;i--)
            {
                final += result[i];
            }
            return final;
        }

        public static string FolderPath()
        {
            return System.Windows.Forms.Application.StartupPath;
        }
    }

    public class ProgressInfoEventArgs:EventArgs
    {
        public ProgressInfoEventArgs(int stat)
        {
            this.Stat = stat;
        }
        public int Stat { get; private set; }
    }

    public class DataBaseUpdatedInfoEventArgs:EventArgs
    {
        public DataBaseUpdatedInfoEventArgs()
        {

        }
    }


}
