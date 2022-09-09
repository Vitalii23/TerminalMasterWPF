using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using TerminalMasterWPF.DataType;

namespace TerminalMaster.SaveFile
{
    class JsonFile<T> : IFile<T> where T : class
    {

        public async void WriteJsonAsync(ObservableCollection<T> element, string path)
        {
            FileStream fileStream = new FileStream($".\\Json\\{path}.json", FileMode.OpenOrCreate);
            await JsonSerializer.SerializeAsync<ObservableCollection<T>>(fileStream, element);
        }

        public async void ReadJsonAsync(ObservableCollection<T> element, string path) 
        {
            FileStream fileStream = new FileStream($"{path}.json", FileMode.OpenOrCreate);
          //  await JsonSerializer.DeserializeAsync<T>(element);
        }
    }
}
