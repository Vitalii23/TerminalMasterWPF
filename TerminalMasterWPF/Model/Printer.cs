using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace TerminalMasterWPF.Model
{
    [Table(Name = "Printer")]
    class Printer
    {
        [DisplayAttribute(AutoGenerateField = false)]
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }

        [DisplayAttribute(Name = "Фирма")]
        [Column(Name = "brand")]
        public string BrandPrinter { get; set; }

        [DisplayAttribute(Name = "Модель принтера")]
        [Column(Name = "model")]
        public string ModelPrinter { get; set; }

        [DisplayAttribute(Name = "Катридж")]
        [Column(Name = "cartridge")]
        public string Cartridge { get; set; }

        [DisplayAttribute(Name = "Имена портов")]
        [Column(Name = "name_port")]
        public string NamePort { get; set; }

        [DisplayAttribute(Name = "Расположение принтера")]
        [Column(Name = "location")]
        public string LocationPrinter { get; set; }

        [DisplayAttribute(Name = "Статус")]
        [Column(Name = "status")]
        public string Status { get; set; }

        [DisplayAttribute(Name = "Артикули принтера")]
        [Column(Name = "vendor_code")]
        public string VendorCodePrinter { get; set; }

        public Printer(string brandPrinter, string modelPrinter, string cartridge, string namePort, string locationPrinter, string status, string vendorCodePrinter)
        {
            BrandPrinter = brandPrinter ?? throw new ArgumentNullException(nameof(brandPrinter));
            ModelPrinter = modelPrinter ?? throw new ArgumentNullException(nameof(modelPrinter));
            Cartridge = cartridge ?? throw new ArgumentNullException(nameof(cartridge));
            NamePort = namePort ?? throw new ArgumentNullException(nameof(namePort));
            LocationPrinter = locationPrinter ?? throw new ArgumentNullException(nameof(locationPrinter));
            Status = status ?? throw new ArgumentNullException(nameof(status));
            VendorCodePrinter = vendorCodePrinter ?? throw new ArgumentNullException(nameof(vendorCodePrinter));
        }

        public Printer() { }
    }
}
