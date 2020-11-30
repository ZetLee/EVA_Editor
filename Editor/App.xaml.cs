using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Editor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 

    


    public partial class App : Application
    {

        ViewModels.EditorViewModel _viewModel;
        MainWindow _window;
        ViewModels.EditorModel _model;

        public App ()
        {
            Startup += new StartupEventHandler(App_Startup);
        }


        

        void App_Startup(object sender, StartupEventArgs eventArgs)
        {
            _model = new ViewModels.EditorModel(new ViewModels.FilePersistence());
            
            _viewModel = new ViewModels.EditorViewModel();

            _window = new MainWindow();

            _window.DataContext = _viewModel;

            _model.FileOpened += _viewModel.FileOpened;
            _model.FileSaved += _viewModel.FileSaved;
            _viewModel.OpenFile += OpenFile;
            _viewModel.SaveFile += SaveFile;

            


            _window.Show();

        }


        public async void OpenFile(object sender, ViewModels.FileOperationEventArgs e)
        {
            if(e.IsDirty)
            {
                MessageBoxResult result = MessageBox.Show("File nincs elmentve! Biztos felülírod??", "Nincs mentve", MessageBoxButton.YesNo);
                if(result == MessageBoxResult.Yes)
                {
                    OpenFileDialog dialog = new OpenFileDialog();
                    if (dialog.ShowDialog() == true)
                        await _model.FileOpenedHandler(dialog.FileName);
                }
            }
            else
            {
                OpenFileDialog dialog = new OpenFileDialog();
                if (dialog.ShowDialog() == true)
                    await _model.FileOpenedHandler(dialog.FileName);
            }
            
            
        }

        public async void SaveFile(object sender, ViewModels.FileOperationEventArgs e)
        {
            try
            {
                if (_model.HasPath())
                {
                    await _model.FileSavedHandler(e.Data, _model.Path);
                }
                else
                {
                    SaveFileDialog dialog = new SaveFileDialog();
                    if (dialog.ShowDialog() == true)
                        await _model.FileSavedHandler(e.Data, dialog.FileName);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            
        }

    }
}
