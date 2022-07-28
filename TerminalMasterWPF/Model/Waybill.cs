using System;

namespace TerminalMasterWPF.Model
{
    class Waybill
    {
        public int Id { get; set; }
        public string NameDocument { get; set;}
        public string NumberDocument { get; set; }
        public string NumberSuppliers { get; set; }
        public DateTime DateDocument { get; set; }
        public string DateDocumentString { get; set; }
        public string FileName { get; set; }
        public byte[] FilePDF { get; set; }
        public int IdHolder { get; set; }
        public string Holder { get; set; }
    }
}
