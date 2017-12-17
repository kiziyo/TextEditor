using System;
using System.IO;
using System.Windows.Input;
using System.Windows;

namespace vin
{
    class NewCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private MainWindow main_window;

        public NewCommand(MainWindow _main_window)
        {
            this.main_window = _main_window;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            MessageBoxResult result = MessageBox.Show(main_window, "Clear?", "OK?", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (result == MessageBoxResult.OK)
            {
                main_window.text_area.Text = "";
                main_window.current_filepath.Text = "new file";
            }
        }
    }
}
