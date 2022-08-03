using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerminalMasterWPF.Model;
using TerminalMasterWPF.Model.People;

namespace TerminalMasterWPF.ViewModel
{
    class DataGets : INotifyPropertyChanged
    {
        public DataGets()
        {
          
        }

        private ObservableCollection<Cartridge> _cartridges = new ObservableCollection<Cartridge>();
        private ObservableCollection<CashRegister> _cashRegister = new ObservableCollection<CashRegister>();
        private ObservableCollection<PhoneBook> _phoneBook = new ObservableCollection<PhoneBook>();
        private ObservableCollection<Printer> _printer = new ObservableCollection<Printer>();
        private ObservableCollection<SimCard> _simCard = new ObservableCollection<SimCard>();
        private ObservableCollection<Holder> _holder = new ObservableCollection<Holder>();
        private ObservableCollection<User> _user = new ObservableCollection<User>();
        private ObservableCollection<IndividualEntrepreneur> _individual = new ObservableCollection<IndividualEntrepreneur>();
        private ObservableCollection<Waybill> _waybill = new ObservableCollection<Waybill>();
        private int _selectedXIndex;
        private int _selectedId;
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChangedEventArgs e = new PropertyChangedEventArgs(propertyName);
                PropertyChanged(this, e);
            }
        }
        public ObservableCollection<Cartridge> CartridgesList
        {
            get =>_cartridges;
            set
            {
                _cartridges = value;
                OnPropertyChanged("CartridgesList");
            }
        }
        public ObservableCollection<CashRegister> CashRegisterList
        {
            get => _cashRegister;
            set
            {
                _cashRegister = value;
                OnPropertyChanged("CashRegisterList");
            }
        }
        public ObservableCollection<PhoneBook> PhoneBookList
        {
            get => _phoneBook;
            set
            {
                _phoneBook = value;
                OnPropertyChanged("PhoneBookList");
            }
        }
        public ObservableCollection<Printer> PrinterList
        {
            get => _printer;
            set
            {
                _printer = value;
                OnPropertyChanged("PrinterList");
            }
        }
        public ObservableCollection<SimCard> SimCardList
        {
            get => _simCard;
            set
            {
                _simCard = value;
                OnPropertyChanged("SimCardList");
            }
        }
        public ObservableCollection<Holder> HolderList
        {
            get => _holder;
            set
            {
                _holder = value;
                OnPropertyChanged("HolderList");
            }
        }
        public ObservableCollection<User> UserList
        {
            get => _user;
            set
            {
                _user = value;
                OnPropertyChanged("UserList");
            }
        }
        public ObservableCollection<IndividualEntrepreneur> IndividualEntrepreneurList
        {
            get => _individual;
            set
            {
                _individual = value;
                OnPropertyChanged("IndividualEntrepreneurList");
            }
        }
        public ObservableCollection<Waybill> WaybillList
        {
            get => _waybill;
            set
            {
                _waybill = value;
                OnPropertyChanged("WaybillList");
            }
        }

        public int SelectedXIndex
        {
            get => _selectedXIndex;
            set
            {
                _selectedXIndex = value;
                OnPropertyChanged("SelectedXIndex");
            }
        }

        public int SelectedId
        {
            get => _selectedId;
            set
            {
                _selectedId = value;
                OnPropertyChanged("SelectedId");
            }
        }
    }
}
