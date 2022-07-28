namespace TerminalMasterWPF.Model
{
    class Cartridge
    {
        public int Id { get; set; }
        public string Brand { get; set; } // Бренд устройства
        public string Model { get; set; } // Модель устройства
        public string VendorCode { get; set; } // Артикуль
        public string Status { get; set; } // Состояние
        public Cartridge()
        {
        }
    }

}
