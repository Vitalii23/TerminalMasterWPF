namespace TerminalMasterWPF.Model
{
    class User
    {
        public int Id { get; set; }
        public string LastName { get; set; } // Фамилия 
        public string FirstName { get; set; } // Имя
        public string MiddleName { get; set; } // Отчество
        public string Status { get; set; } // Статус
        public string Number { get; set; }
    }
}
