using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Uval4.Source
{
    public class Contract : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged() { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ThatData")); }

        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        static private Contract thatInst = new Contract();
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        private ObservableCollection<ContractEntry> thatInstPrev = new ObservableCollection<ContractEntry>();/*For Previous week*/
        private ObservableCollection<ContractEntry> thatInstCur = new ObservableCollection<ContractEntry>();/*For Current week*/
        private ObservableCollection<ContractEntry> thatInstNext = new ObservableCollection<ContractEntry>();/*For Next week*/

        private List<ContractEntry> thatInstStorage = new List<ContractEntry>();/*For all data*/

        public ObservableCollection<ContractEntry> ThatInstPrev { get => thatInstPrev; set => thatInstPrev = value; }
        public ObservableCollection<ContractEntry> ThatInstCur { get => thatInstCur; set => thatInstCur = value; }
        public ObservableCollection<ContractEntry> ThatInstNext { get => thatInstNext; set => thatInstNext = value; }
        public List<ContractEntry> ThatInstStorage { get => thatInstStorage; set => thatInstStorage = value; }

        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------

    }
    public class ContractEntry
    {
        private int thatID;
        private string thatName;
        private List<string> thatPrev;
        private List<string> thatCur;
        private List<string> thatnext;
    }


}
