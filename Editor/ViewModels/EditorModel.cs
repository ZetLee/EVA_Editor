using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Editor.ViewModels
{
    class EditorModel
    {
        private string _path;

        public string Path
        {
            get => _path;
            set => _path = value;
        }

        private string _name;

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        private string _data;

        public string Data
        {
            get => _data;
            set => _data = value;
        }

        //private FileOperationEventArgs args;


        private FilePersistence pers;

        //public DelegateCommand save { get; private set; }

        public EditorModel(FilePersistence asd)
        {
            _path = null;
            pers = asd;

            //save = new DelegateCommand(param => FileSavedHandler());
        }

        public event EventHandler<FileOperationEventArgs> FileOpened;
        public event EventHandler<FileOperationEventArgs> FileSaved;

        
        public async Task FileOpenedHandler(string path = "./data")
        {
            if (pers == null)
                throw new FileOperationException("persistence hiba");
            Path = path;
            Data = await pers.LoadFileAsync(Path);
            Name = System.IO.Path.GetFileName(Path);
            OnFileOpened();
        }

        public async Task FileSavedHandler(string data, string path)
        {
            if (pers == null)
                throw new FileOperationException("persistence hiba");
            Path = path;
            await pers.SaveFileAsync(data, path);
            OnFileSaved();
        }


        private void OnFileOpened()
        {
            if (FileOpened != null)
                FileOpened(this, new FileOperationEventArgs(Path, Data));
        }

        private void OnFileSaved()
        {
            if (FileSaved != null)
                FileSaved(this, new FileOperationEventArgs(Path, Data));
        }


        public bool HasPath()
        {
            return _path != null;
        }

    }
}
