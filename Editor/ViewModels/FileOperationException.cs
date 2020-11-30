using System;
using System.Collections.Generic;
using System.Text;

namespace Editor.ViewModels
{
    class FileOperationException : Exception
    {

        public FileOperationException(String message) : base(message) { }

    }
}
