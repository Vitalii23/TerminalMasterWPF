using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TerminalMasterWPF.Model;
using TerminalMasterWPF.Model.People;

namespace TerminalMasterWPF.ViewModel
{
    class DataGets : INotifyPropertyChanged
    {
        public DataGets()
        {
          
        }

        private Cartridge _cartridges;
        private CashRegister _cashRegister;
        private Employees _phoneBook;
        private Printer _printer;
        private SimCard _simCard;
        private Holder _holder;
        private IndividualEntrepreneur _individual;
        private Documents _documents;
        private CountersPage _countersPages;

        public IList<Cartridge> CartridgesList { get; set; }
        public ObservableCollection<CashRegister> CashRegisterList { get; set; }
        public IList<Employees> EmployessList { get; set; }
        public IList<Printer> PrinterList { get; set; }
        public ObservableCollection<SimCard> SimCardList { get; set; }
        public ObservableCollection<Holder> HolderList { get; set; }
        public IList<IndividualEntrepreneur> IndividualList { get; set; }
        public IList<Documents> DocumentsList { get; set; }
        public IList<CountersPage> CountersPagesList { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChangedEventArgs e = new PropertyChangedEventArgs(propertyName);
                PropertyChanged(this, e);
            }
        }

        public Cartridge Cartridges
        {
            get =>_cartridges;
            set
            {
                _cartridges = value;
                OnPropertyChanged("Cartridge");
            }
        }

        public CashRegister CashRegister
        {
            get => _cashRegister;
            set
            {
                _cashRegister = value;
                OnPropertyChanged("CashRegister");
            }
        }

        public Employees Employees
        {
            get => _phoneBook;
            set
            {
                _phoneBook = value;
                OnPropertyChanged("PhoneBook");
            }
        }

        public Printer Printer
        {
            get => _printer;
            set
            {
                _printer = value;
                OnPropertyChanged("Printer");
            }
        }

        public SimCard SimCard
        {
            get => _simCard;
            set
            {
                _simCard = value;
                OnPropertyChanged("SimCard");
            }
        }

        public Holder Holder
        {
            get => _holder;
            set
            {
                _holder = value;
                OnPropertyChanged("Holder");
            }
        }

        public IndividualEntrepreneur IndividualEntrepreneur
        {
            get => _individual;
            set
            {
                _individual = value;
                OnPropertyChanged("IndividualEntrepreneur");
            }
        }

        public Documents Documents
        {
            get => _documents;
            set
            {
                _documents = value;
                OnPropertyChanged("Documents");
            }
        }

        public CountersPage CountersPage
        {
            get => _countersPages;
            set
            {
                _countersPages = value;
                OnPropertyChanged("CountersPage");
            }
        }
    }
}
