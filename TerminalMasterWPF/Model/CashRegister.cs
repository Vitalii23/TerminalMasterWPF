using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace TerminalMasterWPF.Model
{
    [Table(Name = "CashRegister")]
    class CashRegister
    {
        [DisplayAttribute(AutoGenerateField = false)]
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }

        [DisplayAttribute(Name = "ККМ")]
        [Column(Name = "name")]
        public string NameDevice { get; set; }

        [DisplayAttribute(Name = "Бренд устройства")]
        [Column(Name = "brand")]
        public string Brand { get; set; }

        [DisplayAttribute(Name = "Заводской номер")]
        [Column(Name = "factory_number")]
        public string FactoryNumber { get; set; }

        [DisplayAttribute(Name = "Серийный номер")]
        [Column(Name = "serial_number")]
        public string SerialNumber { get; set; }

        [DisplayAttribute(Name = "Номер счета")]
        [Column(Name = "payment_number")]
        public string PaymentNumber { get; set; }

        [DisplayAttribute(Name = "Владелец по договору")]
        public string Holder { get; set; }

        [DisplayAttribute(Name = "Пользователь")]
        public string User { get; set; }

        [DisplayAttribute(Name = "Дата получения")]
        [Column(Name = "date_reception")]
        [DisplayFormat(DataFormatString = "yyyy-MM-dd")]
        public DateTime DateReception { get; set; }

        [DisplayAttribute(AutoGenerateField = false)]
        public string DateReceptionString { get; set; }

        [DisplayAttribute(Name = "Дата активации ключа ОФД")]
        [Column(Name = "date_end_fiscal_memory")]
        [DisplayFormat(DataFormatString = "yyyy-MM-dd")]
        public DateTime DateKeyActivationFiscalDataOperator { get; set; }

        [DisplayAttribute(AutoGenerateField = false)]
        public string DateKeyActivationFiscalDataOperatorString { get; set; }

        [DisplayAttribute(Name = "Дата окончания ФН")]
        [Column(Name = "date_key_activ_fisc_data")]
        [DisplayFormat(DataFormatString = "yyyy-MM-dd")]
        public DateTime DateEndFiscalMemory { get; set; }

        [DisplayAttribute(AutoGenerateField = false)]
        public string DateEndFiscalMemoryString { get; set; }

        [DisplayAttribute(Name = "Место нахождения")]
        [Column(Name = "date_key_activ_fisc_data")]
        public string Location { get; set; }

        [DisplayAttribute(AutoGenerateField = false)]
        public int IdHolder { get; set; }

        [DisplayAttribute(AutoGenerateField = false)]
        public int IdUser { get; set; }

        public CashRegister() { }
    }
}
