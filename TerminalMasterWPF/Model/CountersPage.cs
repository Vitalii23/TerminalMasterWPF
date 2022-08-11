using System;
using System.Collections.Generic;
using System.Text;

namespace TerminalMasterWPF.Model
{
    class CountersPage
    {
        public int Id { get; set; }
        public string PrintedPageCounter { get; set; }
        public DateTime Date { get; set; }
        public string Printers { get; set; }
        public string IdPrinter { get; set; }
    }
}
