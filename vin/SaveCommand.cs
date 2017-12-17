using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace vin
{
    class SaveCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private MainWindow main_window;

        public SaveCommand(MainWindow _main_window)
        {
            this.main_window = _main_window;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var dialog = new Microsoft.Win32.SaveFileDialog
            {
                FileName = main_window.current_filepath.Text,
                DefaultExt = "txt",
                Filter = "テキストファイル (*.txt)|*.txt|すべてのファイル (*.*)|*.*"
            };

            if (dialog.ShowDialog() == true)
            {
                File.WriteAllText(dialog.FileName, main_window.text_area.Text);
                main_window.current_filepath.Text = dialog.FileName;
                main_window.Title = System.IO.Path.GetFileName(dialog.FileName);
            }
        }
    }
}
