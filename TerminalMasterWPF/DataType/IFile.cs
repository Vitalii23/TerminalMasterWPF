using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace TerminalMasterWPF.DataType
{
    interface IFile<T>
    {
        void WriteJsonAsync(ObservableCollection<T> element, string path);

        void ReadJsonAsync(ObservableCollection<T> element, string path);
    }
}
