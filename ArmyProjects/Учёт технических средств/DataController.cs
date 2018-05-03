using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;

using Учёт_технических_средств.Source;
using Учёт_технических_средств.Helpers;
using Учёт_технических_средств.Windows;


namespace Учёт_технических_средств
{
    public class DataController
    {

        static DataController()
        {
            archive = new ProductDatabase();
            mainBase = new ProductDatabase();
            
            dataContext = new BackEnd();
            possibleStates = new List<string>();
            possibleStates.Add("По умолчанию");
            possibleStates.Add("Передано");
            possibleStates.Add("Получено");
            possibleStates.Add("Отправлено в сервис");
            windowsCount = 0;
           // workWnd.Closed += workWnd_Closed;
            
        }

        private int loadProgress;
        public int LoadProgress
        {
            get { return loadProgress; }
            set { loadProgress = value; }
        }

        public static BackEnd dataContext;


        

        public static void initBases()
        {
            //mainBase = DataHelper.TestDataBase();
            //string relativeSource = Environment.CurrentDirectory;
            
            //string fullPath = Path.Combine(relativeSource, "../mainBase.xml");
           
            dataContext = DataHelper.LoadXmlContext(System.IO.Path.Combine(DataHelper.FolderPath(), "settings.xml"));
            mainBase = DataHelper.LoadFromXml(dataContext.DefaultPath);
            archive = DataHelper.LoadFromXml(System.IO.Path.Combine(DataHelper.FolderPath(), @"Data\archive.xml"));
            openedFile = dataContext.DefaultPath;
            
        }
        //окна
        public static Window workWnd, mainWnd, stngWnd, archiveWnd, progressWnd, importWnd, errorWnd, askWnd;

        //Базы данных
        public static ProductDatabase archive;
        public static ProductDatabase mainBase;
        //Возможные состояния у продуктов
        public static List<string> possibleStates;
        //
        public static string openedFile
        {
            get;
            set;
        }
        //счетчик окон
        private static int windowsCount;

        public static bool MainBaseChanged
        { get; set; }
        public static bool ArchiveChanged
        { get; set; }
        public static bool IsSaved
        { get; set; }
        #region Порождение окон
        private static void ShowMainWindow()
        {
            if (mainWnd == null)
            {
                mainWnd = new MainWindow();
                mainWnd.Closed += activeWnd_Closed;
            }
            mainWnd.Show();
        }
        private static void ShowWorkWindow()
        {
            if (workWnd == null)
            {
                workWnd = new WorkWindow();
                workWnd.Closed += activeWnd_Closed;
                workWnd.Closing += workWnd_Closing;
            }
            workWnd.Show();
        }

        static void workWnd_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (DataController.MainBaseChanged)
                ShowAskWindow();
            if (IsSaved)
                DataHelper.SaveToXml(mainBase, DataController.openedFile);

        }

       
        private static void ShowArchiveWindow()
        {
            if (archiveWnd == null)
            {
                archiveWnd = new ArchiveWindow();
                archiveWnd.Closed += activeWnd_Closed;
            }
            archiveWnd.Show();
        }
        private static void ShowSettingsWindow()
        {
            if (stngWnd == null)
            {
                stngWnd = new SettingsWindow();
               // stngWnd.Closed += stngWnd_Closed;
            }
            stngWnd.Show();
        }
        private static void ShowImportWindow()
        {
            if (importWnd == null)
            {
                importWnd = new ExcelImportWindow();
            }
            importWnd.ShowDialog();
        }

        public static void ShowErrorWindow(string s)
        {
            if (errorWnd == null)
            {
                errorWnd = new ErrorWindow(s);
            }
            errorWnd.Show();
        }

        public static void ShowAskWindow()
        {
            if (askWnd == null)
            {
                askWnd = new AskWindow();
            }
            askWnd.ShowDialog();
        }
        #endregion

        #region обработчики событий
       
        private static void onErrorAppeared(object sender, Exception ex)
        {

        }
        private static void activeWnd_Closed(object sender, EventArgs e)
        {
            windowsCount--;
            if (windowsCount == 0)
            {
                DataHelper.SaveToXml(DataController.archive, System.IO.Path.Combine(DataHelper.FolderPath(), @"Data\archive.xml"));

                Application.Current.Shutdown();
            }
            if (sender.GetType().Equals(typeof(WorkWindow)))
            {
                workWnd.Closed -= activeWnd_Closed;
                workWnd = null;
            }
            if (sender.GetType().Equals(typeof(ArchiveWindow)))
            {
                archiveWnd.Closed -= activeWnd_Closed;
                archiveWnd = null;
            }
            if (sender.GetType().Equals(typeof(MainWindow)))
            {
                mainWnd.Closed -= activeWnd_Closed;
                mainWnd = null;
            }
           

        }

        private static void activeWnd_Closing(object sender, EventArgs e)
        {
            ShowAskWindow();
        }
        #endregion
        public static void ShowWindow(Type windowType)
        {
            if (windowType.Equals(typeof(WorkWindow)))
            {
                ShowWorkWindow();
                windowsCount++;
            }
            if (windowType.Equals(typeof(SettingsWindow)))
            {
                ShowSettingsWindow();
            }
            if (windowType.Equals(typeof(ArchiveWindow)))
            {
                ShowArchiveWindow();
                windowsCount++;
            }
            if (windowType.Equals(typeof(MainWindow)))
            {
                ShowMainWindow();
                windowsCount++;
            }
            if (windowType.Equals(typeof(ExcelImportWindow)))
            {
                ShowImportWindow();
                //windowsCount++;
            }
            
           
        }


        public static void CloseWindow(Type windowType)
        {
            if (windowType.Equals(typeof(WorkWindow)))
            {
                if (workWnd != null)
                {
                    workWnd.Hide();
                    workWnd.Close();
                    workWnd = null;
                }
                
            }
            if (windowType.Equals(typeof(SettingsWindow)))
            {
                if (stngWnd != null)
                {
                    stngWnd.Hide();
                    stngWnd.Close();
                    stngWnd = null;
                }
            }
            if (windowType.Equals(typeof(ArchiveWindow)))
            {
                if (archiveWnd != null)
                {
                    archiveWnd.Hide();
                    archiveWnd.Close();
                    archiveWnd = null;
                }
            }
            if (windowType.Equals(typeof(MainWindow)))
            {
                if (mainWnd != null)
                {
                    mainWnd.Hide();
                    mainWnd.Close();
                    mainWnd = null;
                }
            }
            if (windowType.Equals(typeof(ExcelImportWindow)))
            {
                if (importWnd != null)
                {
                    importWnd.Hide();
                    importWnd.Close();
                    importWnd = null;
                }
                //windowsCount++;
            }
            if (windowType.Equals(typeof(AskWindow)))
            {
                if (askWnd != null)
                {
                    askWnd.Hide();
                    askWnd.Close();
                    askWnd = null;
                }
                //windowsCount++;
            }



        }
        public static void showLoader()
        {
            if (progressWnd == null)
            {
                progressWnd = new ProgressWindow();
                progressWnd.Show();

            }
        }

        public static void closeLoader()
        {
            if (progressWnd != null)
            {
                progressWnd.Hide();
                progressWnd.Close();
                progressWnd = null;
            }
        }

        public static void updateMainBase(string path)
        {
            mainBase = DataHelper.LoadFromXls(path);
        }
   
    }

   
    
    
    public class BackEnd : BindableObject
    {
        public BackEnd()
        {

            defaultExpluatationPlace = "в/ч 36360";
            defaultResponsible = "Не указано";
            defaultPath = System.IO.Path.Combine(Environment.CurrentDirectory, @"Data\mainBase.xml");
            exploitationPlaceChosen = true;
            indexChosen = false;
            factoryNumberChosen = true;
            inventoryNumberChosen = true;
            bbtNomenclatureChosen = false;
            purposeChosen = false;
            manufacturerChosen = true;
            productionDateChosen = true;
            guaranteeChosen = false;
            endDateChosen = false;
            exploitationDateChosen = false;
            confirmationDocChosen = false;
            receivingDateChosen = false;
            orderNumberChosen = false;
            exploitationPeriodChosen = false;
            primaryCostChosen = true;
            aurumChosen = false;
            argentumChosen = false;
            platinumChosen = false;
            mpgChosen = false;
            responsiblePersonChosen = true;
            conditionChosen = false;
            serviceDateChosen = false;
            write_offChosen = false;
            commentChosen = false;
            stateChosen = false;
            defaultColumns();
        }

        #region Строковые переменные настроек
        private string defaultResponsible;
        public string DefaultResponsible
        {
            get
            {
                return defaultResponsible;
            }
            set
            {
                SetProperty(ref defaultResponsible, value);
            }
        }

        private string defaultExpluatationPlace;
        public string DefaultExpluatationPlace
        {
            get 
            {
                return defaultExpluatationPlace;
            }
            set 
            {
                SetProperty(ref defaultExpluatationPlace, value);
            }
        }

        private string defaultPath;
        public string DefaultPath
        {
            get
            {
                return defaultPath;
            }
            set
            {
                SetProperty(ref defaultPath, value);
            }
        }
        #endregion

        #region Выбор столбцов
        private bool? exploitationPlaceChosen;
        public bool? ExploitationPlaceChosen
        {
            get
            {
                return exploitationPlaceChosen;
            }
            set
            {
                SetProperty(ref exploitationPlaceChosen, value);
            }
        }

        private bool? indexChosen;
        public bool? IndexChosen
        {
            get
            {
                return indexChosen;
            }
            set
            {
                SetProperty(ref indexChosen, value);
            }
        }


        private bool? factoryNumberChosen;
        public bool? FactoryNumberChosen
        {
            get
            {
                return factoryNumberChosen;
            }
            set
            {
                SetProperty(ref factoryNumberChosen, value);
            }
        }

        private bool? inventoryNumberChosen;
        public bool? InventoryNumberChosen
        {
            get
            {
                return inventoryNumberChosen;
            }
            set
            {
                SetProperty(ref inventoryNumberChosen, value);
            }
        }


        private bool? bbtNomenclatureChosen;
        public bool? BbtNomenclatureChosen
        {
            get
            {
                return bbtNomenclatureChosen;
            }
            set
            {
                SetProperty(ref bbtNomenclatureChosen, value);
            }
        }


        private bool? purposeChosen;
        public bool? PurposeChosen
        {
            get
            {
                return purposeChosen;
            }
            set
            {
                SetProperty(ref purposeChosen, value);
            }
        }


        private bool? manufacturerChosen;
        public bool? ManufacturerChosen
        {
            get
            {
                return manufacturerChosen;
            }
            set
            {
                SetProperty(ref manufacturerChosen, value);
            }
        }


        private bool? productionDateChosen;
        public bool? ProductionDateChosen
        {
            get
            {
                return productionDateChosen;
            }
            set
            {
                SetProperty(ref productionDateChosen, value);
            }
        }


        private bool? guaranteeChosen;
        public bool? GuaranteeChosen
        {
            get
            {
                return guaranteeChosen;
            }
            set
            {
                SetProperty(ref guaranteeChosen, value);
            }
        }


        private bool? endDateChosen;
        public bool? EndDateChosen
        {
            get
            {
                return endDateChosen;
            }
            set
            {
                SetProperty(ref endDateChosen, value);
            }
        }


        private bool? exploitationPeriodChosen;
        public bool? ExploitationPeriodChosen
        {
            get
            {
                return exploitationPeriodChosen;
            }
            set
            {
                SetProperty(ref exploitationPeriodChosen, value);
            }
        }


        private bool? confirmationDocChosen;
        public bool? ConfirmationDocChosen
        {
            get
            {
                return confirmationDocChosen;
            }
            set
            {
                SetProperty(ref confirmationDocChosen, value);
            }
        }


        private bool? receivingDateChosen;
        public bool? ReceivingDateChosen
        {
            get
            {
                return receivingDateChosen;
            }
            set
            {
                SetProperty(ref receivingDateChosen, value);
            }
        }


        private bool? orderNumberChosen;
        public bool? OrderNumberChosen
        {
            get
            {
                return orderNumberChosen;
            }
            set
            {
                SetProperty(ref orderNumberChosen, value);
            }
        }


        private bool? exploitationDateChosen;
        public bool? ExploitationDateChosen
        {
            get
            {
                return exploitationDateChosen;
            }
            set
            {
                SetProperty(ref exploitationDateChosen, value);
            }
        }


        private bool? primaryCostChosen;
        public bool? PrimaryCostChosen
        {
            get
            {
                return primaryCostChosen;
            }
            set
            {
                SetProperty(ref primaryCostChosen, value);
            }
        }


        private bool? aurumChosen;
        public bool? AurumChosen
        {
            get
            {
                return aurumChosen;
            }
            set
            {
                SetProperty(ref aurumChosen, value);
            }
        }


        private bool? argentumChosen;
        public bool? ArgentumChosen
        {
            get
            {
                return argentumChosen;
            }
            set
            {
                SetProperty(ref argentumChosen, value);
            }
        }


        private bool? platinumChosen;
        public bool? PlatinumChosen
        {
            get
            {
                return platinumChosen;
            }
            set
            {
                SetProperty(ref platinumChosen, value);
            }
        }


        private bool? mpgChosen;
        public bool? MpgChosen
        {
            get
            {
                return mpgChosen;
            }
            set
            {
                SetProperty(ref mpgChosen, value);
            }
        }


        private bool? responsiblePersonChosen;
        public bool? ResponsiblePersonChosen
        {
            get
            {
                return responsiblePersonChosen;
            }
            set
            {
                SetProperty(ref responsiblePersonChosen, value);
            }
        }


        private bool? conditionChosen;
        public bool? ConditionChosen
        {
            get
            {
                return conditionChosen;
            }
            set
            {
                SetProperty(ref conditionChosen, value);
            }
        }


        private bool? serviceDateChosen;
        public bool? ServiceDateChosen
        {
            get
            {
                return serviceDateChosen;
            }
            set
            {
                SetProperty(ref serviceDateChosen, value);
            }
        }


        private bool? write_offChosen;
        public bool? Write_offChosen
        {
            get
            {
                return write_offChosen;
            }
            set
            {
                SetProperty(ref write_offChosen, value);
            }
        }


        private bool? commentChosen;
        public bool? CommentChosen
        {
            get
            {
                return commentChosen;
            }
            set
            {
                SetProperty(ref commentChosen, value);
            }
        }


        private bool? stateChosen;
        public bool? StateChosen
        {
            get
            {
                return stateChosen;
            }
            set
            {
                SetProperty(ref stateChosen, value);
            }
        }
        #endregion

        #region Названия столбцов для импорта
        private string nameColumn;
        public string NameColumn
        {
            get
            {
                return nameColumn;
            }
            set
            {
                SetProperty(ref nameColumn, value);
            }

        }

        
        //индекс
        private string indexColumn;
        public string IndexColumn
        {
            get
            {
                return indexColumn;
            }
            set
            {
                SetProperty(ref indexColumn, value);
            }
        }


        //Место эксплуатации
        private string exploitationPlaceColumn;
        public string ExploitationPlaceColumn
        {
            get
            {
                return exploitationPlaceColumn;
            }
            set
            {
                SetProperty(ref exploitationPlaceColumn, value);
            }

        }

        //номер заводской
        private string factoryNumberColumn;
        public string FactoryNumberColumn
        {
            get
            {
                return factoryNumberColumn;
            }
            set
            {
                SetProperty(ref factoryNumberColumn, value);
            }
        }



        //Инвентаризационный номер
        private string inventoryNumberColumn;
        public string InventoryNumberColumn
        {
            get
            {
                return inventoryNumberColumn;
            }
            set
            {
                SetProperty(ref inventoryNumberColumn, value);
            }
        }

        //номенклатура ВВТ
        private string bbtNomenclatureColumn;
        public string BbtNomenclatureColumn
        {
            get
            {
                return bbtNomenclatureColumn;
            }
            set
            {
                SetProperty(ref bbtNomenclatureColumn, value);
            }
        }

        //Краткое назначение изделия
        private string purposeColumn;
        public string PurposeColumn
        {
            get
            {
                return purposeColumn;
            }
            set
            {
                SetProperty(ref purposeColumn, value);
            }
        }

        //Организация-изготовитель
        private string manufacturerColumn;
        public string ManufacturerColumn
        {
            get
            {
                return manufacturerColumn;
            }
            set
            {
                SetProperty(ref manufacturerColumn, value);
            }
        }

        //Дата изготовления
        private string productionDateColumn;
        public string ProductionDateColumn
        {
            get
            {
                return productionDateColumn;
            }
            set
            {
                SetProperty(ref productionDateColumn, value);
            }
        }

        //Срок гарантии
        private string guaranteeColumn;
        public string GuaranteeColumn
        {
            get
            {
                return guaranteeColumn;
            }
            set
            {
                SetProperty(ref guaranteeColumn, value);
            }
        }

        //Дата окончания обязательств
        private string endDateColumn;
        public string EndDateColumn
        {
            get
            {
                return endDateColumn;
            }
            set
            {
                SetProperty(ref endDateColumn, value);
            }
        }

        //Срок эксплуатации
        private string exploitationPeriodColumn;
        public string ExploitationPeriodColumn
        {
            get
            {
                return exploitationPeriodColumn;
            }
            set
            {
                SetProperty(ref exploitationPeriodColumn, value);
            }
        }

        //Наименование документа, подтверждающего принятие изделия на ответственное хранение
        private string confirmationDocColumn;
        public string ConfirmationDocColumn
        {
            get
            {
                return confirmationDocColumn;
            }
            set
            {
                SetProperty(ref confirmationDocColumn, value);
            }
        }

        private string confirmationNumberColumn;
        public string ConfirmationNumberColumn
        {
            get
            {
                return confirmationNumberColumn;
            }
            set
            {
                SetProperty(ref confirmationNumberColumn, value);
            }
        }

        //Дата принятия объекта на ответственное хранение(аренду)
        private string receivingDateColumn;
        public string ReceivingDateColumn
        {
            get
            {
                return receivingDateColumn;
            }
            set
            {
                SetProperty(ref receivingDateColumn, value);
            }
        }

        //№ приказа о вводе в эксплуатацию
        private string orderNumberColumn;
        public string OrderNumberColumn
        {
            get
            {
                return orderNumberColumn;
            }
            set
            {
                SetProperty(ref orderNumberColumn, value);
            }
        }

        //Дата приказа о вводе в эксплуатацию
        private string exploitationDateColumn;
        public string ExploitationDateColumn
        {
            get
            {
                return exploitationDateColumn;
            }
            set
            {
                SetProperty(ref exploitationDateColumn, value);
            }
        }

        //Первоначальная стоимость, руб
        private string primaryCostColumn;
        public string PrimaryCostColumn
        {
            get
            {
                return primaryCostColumn;
            }
            set
            {
                SetProperty(ref primaryCostColumn, value);
            }
        }

        //Драг.металлы, золото, грамм
        private string aurumColumn;
        public string AurumColumn
        {
            get
            {
                return aurumColumn;
            }
            set
            {
                SetProperty(ref aurumColumn, value);
            }
        }

        //Драг. металлы, серебро, грамм
        private string argentumColumn;
        public string ArgentumColumn
        {
            get
            {
                return argentumColumn;
            }
            set
            {
                SetProperty(ref argentumColumn, value);
            }
        }

        //Драг. металлы, платина, грамм
        private string platinumColumn;
        public string PlatinumColumn
        {
            get
            {
                return platinumColumn;
            }
            set
            {
                SetProperty(ref platinumColumn, value);
            }
        }

        //Драг. металлы, МПГ, грамм
        private string mpgColumn;
        public string MpgColumn
        {
            get
            {
                return mpgColumn;
            }
            set
            {
                SetProperty(ref mpgColumn, value);
            }
        }

        //Мат. ответственное лицо
        private string responsiblePersonColumn;
        public string ResponsiblePersonColumn
        {
            get
            {
                return responsiblePersonColumn;
            }
            set
            {
                SetProperty(ref responsiblePersonColumn, value);
            }
        }

        //Состояние изделия
        private string conditionColumn;
        public string ConditionColumn
        {
            get
            {
                return conditionColumn;
            }
            set
            {
                SetProperty(ref conditionColumn, value);
            }
        }

        //Дата проведения последнего сервисного обслуживания
        private string serviceDateColumn;
        public string ServiceDateColumn
        {
            get
            {
                return serviceDateColumn;
            }
            set
            {
                SetProperty(ref serviceDateColumn, value);
            }
        }

        //Планируемый год списания
        private string write_offColumn;
        public string Write_offColumn
        {
            get
            {
                return write_offColumn;
            }
            set
            {
                SetProperty(ref write_offColumn, value);
            }
        }

        //Примечание
        private string commentColumn;
        public string CommentColumn
        {
            get
            {
                return commentColumn;
            }
            set
            {
                SetProperty(ref commentColumn, value);
            }
        }

        private string beginRange;
        public string BeginRange
        {
            get
            {
                return beginRange;
            }
            set
            {
                SetProperty(ref beginRange, value);
            }
        }
        #endregion

        public void defaultColumns()
        {
            this.NameColumn = "C";
            this.IndexColumn = "D";
            this.ExploitationPlaceColumn = "B";
            this.FactoryNumberColumn = "E";
            this.InventoryNumberColumn = "T";
            this.bbtNomenclatureColumn = "I";
            this.purposeColumn = "J";
            this.manufacturerColumn = "K";
            this.productionDateColumn = "L";
            this.guaranteeColumn = "M";
            this.endDateColumn = "N";
            this.exploitationPeriodColumn = "P";
            this.confirmationDocColumn = "Q";
            this.confirmationNumberColumn = "R";
            this.receivingDateColumn = "S";
            this.orderNumberColumn = "V";
            this.ExploitationDateColumn = "W";
            this.primaryCostColumn = "X";
            this.aurumColumn = "Y";
            this.argentumColumn = "Z";
            this.platinumColumn = "AA";
            this.mpgColumn = "AB";
            this.responsiblePersonColumn = "AC";
            this.conditionColumn = "AH";
            this.ServiceDateColumn = "AI";
            this.write_offColumn = "AJ";
            this.commentColumn = "AK";

            this.beginRange = "3";

        }
        public void DeepCopy(BackEnd src)
        {
            this.ArgentumChosen = src.ArgentumChosen;
            this.AurumChosen = src.AurumChosen;
            this.BbtNomenclatureChosen = src.BbtNomenclatureChosen;
            this.CommentChosen = src.CommentChosen;
            this.ConditionChosen = src.ConditionChosen;
            this.ConfirmationDocChosen = src.ConfirmationDocChosen;
            this.EndDateChosen = src.EndDateChosen;
            this.ExploitationDateChosen = src.ExploitationDateChosen;
            this.ExploitationPeriodChosen = src.ExploitationPeriodChosen;
            this.ExploitationPlaceChosen = src.ExploitationPlaceChosen;
            this.FactoryNumberChosen = src.FactoryNumberChosen;
            this.GuaranteeChosen = src.GuaranteeChosen;
            this.IndexChosen = src.IndexChosen;
            this.InventoryNumberChosen = src.InventoryNumberChosen;
            this.ManufacturerChosen = src.ManufacturerChosen;
            this.MpgChosen = src.MpgChosen;
            this.OrderNumberChosen = src.OrderNumberChosen;
            this.PlatinumChosen = src.PlatinumChosen;
            this.PrimaryCostChosen = src.PrimaryCostChosen;
            this.ProductionDateChosen = src.ProductionDateChosen;
            this.PurposeChosen = src.PurposeChosen;
            this.ReceivingDateChosen = src.ReceivingDateChosen;
            this.ResponsiblePersonChosen = src.ResponsiblePersonChosen;
            this.ServiceDateChosen = src.ServiceDateChosen;
            this.StateChosen = src.StateChosen;
            this.Write_offChosen = src.Write_offChosen;

        }
    
    }
}
