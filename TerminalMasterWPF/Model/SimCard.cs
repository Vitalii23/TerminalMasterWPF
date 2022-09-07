using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace TerminalMasterWPF.Model
{
    [Table(Name = "SimCard")]
    internal class SimCard
    {
        [DisplayAttribute(AutoGenerateField = false)]
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }

        [DisplayAttribute(Name = "Имя терминала")]
        public string NameTerminal { get; set; }

        [DisplayAttribute(Name = "Оператор связи")]
        [Column(Name = "operator")]
        public string Operator { get; set; }

        [DisplayAttribute(Name = "Идентификационный номер (ИН)")]
        [Column(Name = "identifaction_number")]
        public string IdentNumber { get; set; }

        [DisplayAttribute(Name = "Фирма")]
        public string Brand { get; set; }

        [DisplayAttribute(Name = "Тип устройства")]
        [Column(Name = "type_device")]
        public string TypeDevice { get; set; }

        [DisplayAttribute(Name = "Номер телефона (TMS)")]
        [Column(Name = "tms")]
        public string TMS { get; set; }

        [DisplayAttribute(Name = "Уникальный серийный номер sim-card (ICC)")]
        [Column(Name = "icc")]
        public string ICC { get; set; }

        [DisplayAttribute(Name = "Индивидуальный предприниматель (ИП)")]
        public string IndividualEntrepreneur { get; set; }

        [DisplayAttribute(Name = "Статус")]
        [Column(Name = "status")]
        public string Status { get; set; }

        [DisplayAttribute(AutoGenerateField = false)]
        [Column(Name = "id_individual")]
        public int IdIndividual { get; set; }

        [DisplayAttribute(AutoGenerateField = false)]
        [Column(Name = "id_cashRegister")]
        public int IdCashRegister { get; set; }

        public SimCard() { }
    }
}
