using System;
using System.Collections.Generic;
using System.Text;

namespace Editor.ViewModels
{
    class EditorViewModel : ViewModelBase
    {
        private string _currentText;
        public string CurrentText
        {
            get => _currentText;
            set { _currentText = value; OnPropertyChanged(); setLines(); IsDirty = true; }
        }

        private string _lineNumbers;
        public string LineNumbers
        {
            get => _lineNumbers;
            set { _lineNumbers = value; OnPropertyChanged(); }
        }

        private double _fontSize;
        public double FontSize
        {
            get => _fontSize;
            set { if (value >= 1) { _fontSize = value; OnPropertyChanged(); } }
        }

        private bool _isdirty;
        public bool IsDirty
        {
            get => _isdirty;
            set { _isdirty = value; OnPropertyChanged(); }
        }


        public DelegateCommand IncreaseFont { get; private set; }
        public DelegateCommand DecreaseFont { get; private set; }
        public DelegateCommand Load { get; private set; }
        public DelegateCommand Save { get; private set; }


        public event EventHandler<FileOperationEventArgs> OpenFile;
        public event EventHandler<FileOperationEventArgs> SaveFile;


        public EditorViewModel()
        {
            FontSize = 24;
            IsDirty = false;
            
            IncreaseFont = new DelegateCommand(param => OnIncFT());
            DecreaseFont = new DelegateCommand(param => OnDecFT());
            Load = new DelegateCommand(param => OpenFile(this, new FileOperationEventArgs(CurrentText, IsDirty)));
            Save = new DelegateCommand(param => SaveFile(this, new FileOperationEventArgs(CurrentText, IsDirty)));
        }
        
        private int CountLines()
        {
            if (_currentText == null)
                throw new Exception();
            if (_currentText == string.Empty)
                return 1;
            int count = 1;
            foreach (char c in _currentText)
                if (c == '\n')
                    count++;
            return count;
        }

        private void setLines()
        {
            string str = "";
            int lines = CountLines();
            for (int i = 1; i <= lines; i++)
                str = str + i.ToString() + '\n';
            LineNumbers = str;
        }

        private void OnIncFT()
        {
            FontSize = FontSize + 1;
        }

        private void OnDecFT()
        {
            if (FontSize >=2)
                FontSize = FontSize - 1;
        }

        public void FileOpened(object sender, FileOperationEventArgs e)
        {
            CurrentText = e.Data;
            IsDirty = false;
        }

        public void FileSaved(object sender, FileOperationEventArgs e)
        {
            IsDirty = false;
        }

        
    }
}
