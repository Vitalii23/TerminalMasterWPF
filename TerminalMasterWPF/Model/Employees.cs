using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace TerminalMasterWPF.Model
{
    [Table(Name = "Employees")]
    class Employees
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

        [DisplayAttribute(Name = "Должность")]
        [Column(Name = "post")]
        public string Post { get; set; }

        [DisplayAttribute(Name = "Внутренный номер")]
        [Column(Name = "internal_number")]
        public string InternalNumber { get; set; }

        [DisplayAttribute(Name = " Мобильный номер")]
        [Column(Name = "mobile_number")]
        public string MobileNumber { get; set; }

        [DisplayAttribute(Name = "Статус")]
        [Column(Name = "status")]
        public string Status { get; set; }

        [DisplayAttribute(Name = "Отдел")]
        [Column(Name = "department")]
        public string Department { get; set; }

        public Employees() { }
    }
}
