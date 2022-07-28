namespace TerminalMasterWPF.Model
{
    class PhoneBook
    {
        public int Id { get; set; }
        public string LastName { get; set; } // Фамилия 
        public string FirstName { get; set; } // Имя
        public string MiddleName { get; set; } // Отчество
        public string Post { get; set; } // Должность
        public string InternalNumber { get; set; } // Внутренный номер
        public string MobileNumber { get; set; } // Мобильный номер

        public PhoneBook()
        {
        }
    }
}
