using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TerminalMasterWPF.Logging
{
    class LogFile
    {
        public async Task WriteLogAsync(string message, string functionExp)
        {
            try
            {
                string errorText = "\r\nDate: " +
                $"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}\n" +
                        "Void: " + $"{functionExp}\n " +
                                "Error: " + $"{message}";
                string path = ".\\Error\\";


                DirectoryInfo directoryInfo = new DirectoryInfo(path);
                if (!directoryInfo.Exists)
                {
                    directoryInfo.Create();
                }

                if (!File.Exists(path + "\\log.txt"))
                {
                    using (FileStream fileStream = new FileStream(path + "\\log.txt", FileMode.OpenOrCreate))
                    {
                        byte[] buffer = Encoding.Default.GetBytes(errorText);
                        await fileStream.WriteAsync(buffer, 0, buffer.Length);
                    }
                }
                else 
                {
                    using (FileStream fileStream = new FileStream(path + "\\log.txt", FileMode.Append))
                    {

                        byte[] buffer = Encoding.Default.GetBytes(errorText);
                        await fileStream.WriteAsync(buffer, 0, buffer.Length);
                    }
                }


                MessageBox.Show("Ошибка программы: " + errorText, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }

        }

        public void ReaderLog(StreamReader reader)
        {
            //using (FileStream fstream = new FileStream(path, FileMode.OpenOrCreate))
            //{
            //    fstream.Seek(-5, SeekOrigin.End);

            //    byte[] output = new byte[5];
            //    await fstream.ReadAsync(output, 0, output.Length);
            //    // декодируем байты в строку
            //    string textFromFile = Encoding.Default.GetString(output);
            //    Console.WriteLine($"Текст из файла: {textFromFile}"); // world
            //}
        }
    }
}
