using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Editor.ViewModels
{
    interface IPersistence
    {

        Task SaveFileAsync(string stuff, string path = "./data");

        Task LoadFileAsync(string path);

    }
}
