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

        [DisplayAttribute(Name = "Дата документы")]
        [DisplayFormat(DataFormatString = "dd-MM-yyyy")]
        [Column(Name = "date_document")]
        public DateTime DateDocument { get; set; }

        [DisplayAttribute(AutoGenerateField = false)]
        public string FileNamePath { get; set; }

        [DisplayAttribute(Name = "Документ")]
        [Column(Name = "file_binary")]
        public byte[] FileBinary { get; set; }
    }
}
