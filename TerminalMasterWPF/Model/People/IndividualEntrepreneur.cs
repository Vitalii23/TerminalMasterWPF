using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace TerminalMasterWPF.Model.People
{
    [Table(Name = "IndividualEntrepreneur")]
    class IndividualEntrepreneur
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

        [DisplayAttribute(Name = "ОГРНИП")]
        [Column(Name = "psrnie")]
        public string PSRNIE { get; set; }

        [DisplayAttribute(Name = "ИНН")]
        [Column(Name = "tin")]
        public string TIN { get; set; }
    }
}
