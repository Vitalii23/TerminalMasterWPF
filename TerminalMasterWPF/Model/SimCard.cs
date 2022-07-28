namespace TerminalMasterWPF.Model
{
    internal class SimCard
    {
        public int Id { get; set; }
        public string NameTerminal { get; set; }
        public string Operator { get; set; } // Оператор связи
        public string IdentNumber { get; set; } // Идентификационный номер (ИН)
        public string Brand { get; set; } // Фирма
        public string TypeDevice { get; set; } // Тип устройства
        public string TMS { get; set; } // Номер телефона (TMS)
        public string ICC { get; set; } // Уникальный серийный номер sim-card (ICC)
        public string IndividualEntrepreneur { get; set; } // Индивидуальный предприниматель (ИП)
        public string Status { get; set; } // Статус
        public int IdIndividual { get; set; }
        public int IdCashRegister { get; set; }

        public SimCard()
        {
        }
    }
}
