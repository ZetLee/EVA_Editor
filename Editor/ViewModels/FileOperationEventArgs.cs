using System;
using System.Collections.Generic;
using System.Text;

namespace Editor.ViewModels
{
    public class FileOperationEventArgs : EventArgs
    {

        public string Path;
        public string Data;
        public bool IsDirty;

        public FileOperationEventArgs(string p, string d)
        {
            Path = p;
            Data = d;
        }

        public FileOperationEventArgs(string p, bool b)
        {
            Path = p;
            IsDirty = b;
        }
    }
}
