namespace TerminalMasterWPF.Model.People
{
    class IndividualEntrepreneur
    {
        public int Id { get; set; }
        public string LastName { get; set; } // Фамилия 
        public string FirstName { get; set; } // Имя
        public string MiddleName { get; set; } // Отчество
        public string PSRNIE { get; set; }  // ОГРНИП
        public string TIN { get; set; } // ИНН
    }
}
