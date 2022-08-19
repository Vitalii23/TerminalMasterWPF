using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;
using System.Runtime.CompilerServices;

namespace TerminalMasterWPF.Model
{
    [Table(Name = "Cartrides")]
    class Cartridge
    {
        [DisplayAttribute(AutoGenerateField = false)]
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }

        [DisplayAttribute(Name = "Бренд")]
        [Column(Name = "brand")]
        public string Brand { get; set; }

        [DisplayAttribute(Name = "Модель")]
        [Column(Name = "model")]
        public string Model { get; set; }

        [DisplayAttribute(Name = "Артикуль")]
        [Column(Name = "vendor_code")]
        public string VendorCode { get; set; }

        [DisplayAttribute(Name = "Статус")]
        [Column(Name = "status")]
        public string Status { get; set; }
    }
}