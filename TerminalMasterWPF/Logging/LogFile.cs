using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminalMasterWPF.Logging
{
    class LogFile
    {
        public async Task WriteLogAsync(string message, string functionExp)
        {
            try
            {
                //StorageFolder storageFolder = KnownFolders.DocumentsLibrary;
                //StorageFile storageFile;
                //string errorText = "\r\nDate: " +
                //    $"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}\n" +
                //    "Void: " + $"{functionExp}\n " +
                //    "Error: " + $"{message}";

                //if (await storageFolder.TryGetItemAsync("log.txt") != null)
                //{
                //    storageFile = await storageFolder.GetFileAsync("log.txt");
                //    await FileIO.AppendTextAsync(storageFile, errorText);
                //}
                //else
                //{
                //    storageFile = await storageFolder.CreateFileAsync("log.txt", CreationCollisionOption.ReplaceExisting);
                //    await FileIO.WriteTextAsync(storageFile, errorText);
                //}
                //await new MessageDialog("Ошибка программы: " + errorText).ShowAsync();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }

        }

        public void ReaderLog(StreamReader reader)
        {
           
        }
    }
}
