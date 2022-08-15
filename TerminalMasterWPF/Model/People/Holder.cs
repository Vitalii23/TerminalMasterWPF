using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace TerminalMasterWPF.Model
{
    [Table(Name = "Holder")]
    class Holder
    {
        [DisplayAttribute(AutoGenerateField = false)]
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }

        [DisplayAttribute(Name = "Фамилия")]
        [Column(Name = "last_name")]
        public string LastName { get; set; }

        [DisplayAttribute(Name = "Имя")]
        [Column(Name = "first_name")]
        public string FirstName { get; set; }

        [DisplayAttribute(Name = "Отчество")]
        [Column(Name = "middle_name")]
        public string MiddleName { get; set; }

        [DisplayAttribute(Name = "Статус")]
        [Column(Name = "status")]
        public string Status { get; set; }

        [DisplayAttribute(Name = "Номер")]
        [Column(Name = "number")]
        public string Number { get; set; }

    }
}
