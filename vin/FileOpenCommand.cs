using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace vin
{
    class FileOpenCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private MainWindow main_window;

        public FileOpenCommand(MainWindow _main_window)
        {
            main_window = _main_window;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                main_window.text_area.Text = File.ReadAllText(dialog.FileName, Encoding.Default);
                main_window.current_filepath.Text = dialog.FileName;
                main_window.Title = System.IO.Path.GetFileName(dialog.FileName);
            }
        }
    }
}
