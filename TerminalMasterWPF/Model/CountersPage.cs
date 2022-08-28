using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace TerminalMasterWPF.Model
{
    [Table(Name = "CountersPage")]
    class CountersPage
    {
        [DisplayAttribute(AutoGenerateField = false)]
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }

        [DisplayAttribute(Name = "Счетчик принтера")]
        [Column(Name = "printed_page_counter")]
        public string PrintedPageCounter { get; set; }

        [DisplayAttribute(Name = "Дата")]
        [Column(Name = "date")]
        [DisplayFormat(DataFormatString = "yyyy-MM-dd")]
        public DateTime Date { get; set; }

        [DisplayAttribute(Name = "Имя принтера")]
        [Column(Name = "condition")]
        public string Printers { get; set; }

        [DisplayAttribute(AutoGenerateField = false)]
        public string IdPrinter { get; set; }
    }
}
