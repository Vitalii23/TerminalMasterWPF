using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace TerminalMasterWPF.Model
{
    [Table(Name = "Documents")]
    class Documents
    {
        [DisplayAttribute(AutoGenerateField = false)]
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }

        [DisplayAttribute(Name = "Имя документа")]
        [Column(Name = "name_document")]
        public string NameDocument { get; set;}

        [DisplayAttribute(Name = "Номер документа")]
        [Column(Name = "number_document")]
        public string NumberDocument { get; set; }

        [DisplayAttribute(Name = "Дата документы")]
        [Column(Name = "date_document")]
        [DisplayFormat(DataFormatString = "yyyy-MM-dd")]
        public DateTime DateDocument { get; set; }

        [DisplayAttribute(Name = "Документ")]
        [Column(Name = "file_binary")]
        public byte[] FileBinary { get; set; }
    }
}
