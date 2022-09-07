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
        public int PrintedPageCounter { get; set; }

        [DisplayAttribute(Name = "Дата")]
        [Column(Name = "date")]
        [DisplayFormat(DataFormatString = "dd-MM-yyyy")]
        public DateTime Date { get; set; }

        [DisplayAttribute(Name = "Примечание")]
        [Column(Name = "condition")]
        public string Сondition { get; set; }

        [DisplayAttribute(Name = "Имя принтера")]
        public string Printer { get; set; }

        [DisplayAttribute(AutoGenerateField = false)]
        [Column(Name = "id_printer")]
        public int IdPrinter { get; set; }
    }
}
