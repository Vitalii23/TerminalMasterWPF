using System;
using System.ComponentModel.DataAnnotations;

namespace TerminalMasterWPF.Model
{
    class CashRegister
    {
        public int Id { get; set; }
        public string NameDevice { get; set; } // ККМ
        public string Brand { get; set; } // Бренд устройства
        public string FactoryNumber { get; set; } // Заводской номер
        public string SerialNumber { get; set; } // Серийный номер
        public string PaymentNumber { get; set; } // Номер счета
        public string Holder { get; set; } // Владелец по договору
        public string User { get; set; } // Пользователь
        [DisplayFormat(DataFormatString = "yyyy-MM-dd")]
        public DateTime DateReception { get; set; } // Дата получения
        public string DateReceptionString { get; set; } // Дата получения ТЕХТ
        [DisplayFormat(DataFormatString = "yyyy-MM-dd")]
        public DateTime DateKeyActivationFiscalDataOperator { get; set; } // Дата активации ключа ОФД
        public string DateKeyActivationFiscalDataOperatorString { get; set; }
        [DisplayFormat(DataFormatString = "yyyy-MM-dd")]
        public DateTime DateEndFiscalMemory { get; set; } // Дата окончания ФН
        public string DateEndFiscalMemoryString { get; set; }
        public string Location { get; set; } // Место нахождения
        public int IdHolder { get; set; }
        public int IdUser { get; set; }

        public CashRegister()
        {
        }
    }
}
