using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Editor.ViewModels
{
    class FilePersistence
    {

        public async Task SaveFileAsync(string data, string path = "./data")
        {
            try
            {
                await System.IO.File.WriteAllTextAsync(path, data);
            }
            catch (Exception e)
            {
                throw new FileOperationException(e.Message);
            }
        }


        public async Task<string> LoadFileAsync(string path)
        {
            try
            {
                string data = await System.IO.File.ReadAllTextAsync(path);
                return data;
            }
            catch
            {
                throw new FileOperationException("Cant load file");
            }
        }

    }
}
