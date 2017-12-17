using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace vin
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private enum ManipMode
        {
            Cursor,
            Insert,
            Command
        };

        private ManipMode manip_mode = ManipMode.Cursor;



        public string currentFilePath = "";

        public MainWindow()
        {
            InitializeComponent();

            InitializeKeyBinding();
        }

        private void InitializeKeyBinding()
        {
            this.InputBindings.Add(new KeyBinding(new NewCommand(this), Key.N, ModifierKeys.Control));
            this.InputBindings.Add(new KeyBinding(new FileOpenCommand(this), Key.O, ModifierKeys.Control));
            this.InputBindings.Add(new KeyBinding(new SaveCommand(this), Key.S, ModifierKeys.Control));

        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length > 1)
            {
                current_filepath.Text = "Loading... : " + args[1];
                text_area.Text = File.ReadAllText(args[1], Encoding.Default);
                current_filepath.Text = args[1];
                Title = System.IO.Path.GetFileName(args[1]);
            }

            text_area.Focus();
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (manip_mode)
            {
                case ManipMode.Cursor:
                    //OnKeyDown_CursorMode(sender, e);
                    break;
                case ManipMode.Insert:
                    OnKeyDown_InsertMode(sender, e);
                    break;
                case ManipMode.Command:
                    break;
            }

        }

        private void OnKeyDown_CursorMode(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.L)
            {
                KeyEventArgs ee = new KeyEventArgs(e.KeyboardDevice, e.InputSource, e.Timestamp, Key.Right);
                ee.RoutedEvent = e.RoutedEvent;
                text_area.RaiseEvent(ee);
                e.Handled = true;
            }
            else if (e.Key == Key.H)
            {
                KeyEventArgs ee = new KeyEventArgs(e.KeyboardDevice, e.InputSource, e.Timestamp, Key.Left);
                ee.RoutedEvent = e.RoutedEvent;
                text_area.RaiseEvent(ee);
                e.Handled = true;
            }
            else if (e.Key == Key.J)
            {
                KeyEventArgs ee = new KeyEventArgs(e.KeyboardDevice, e.InputSource, e.Timestamp, Key.Down);
                ee.RoutedEvent = e.RoutedEvent;
                text_area.RaiseEvent(ee);
                e.Handled = true;
            }
            else if (e.Key == Key.K)
            {
                KeyEventArgs ee = new KeyEventArgs(e.KeyboardDevice, e.InputSource, e.Timestamp, Key.Up);
                ee.RoutedEvent = e.RoutedEvent;
                text_area.RaiseEvent(ee);
                e.Handled = true;
            }
            else if (e.Key == Key.I)
            {
                manip_mode = ManipMode.Insert;
            }
            e.Handled = true;
        }

        private void OnKeyDown_InsertMode(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                manip_mode = ManipMode.Cursor;
                e.Handled = true;
            }
        }

        private void Window_Drop(object sender, DragEventArgs e)
        {
            var files = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (files == null) return;

            text_area.Text = File.ReadAllText(files[0], Encoding.Default);
            current_filepath.Text = files[0];
            Title = System.IO.Path.GetFileName(files[0]);
        }
        
        private void Window_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void Window_DragLeave(object sender, DragEventArgs e)
        {

        }

        private void Window_DragOver(object sender, DragEventArgs e)
        {
        }

        private void Window_PreviewDragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop) == true)
            {
                e.Effects = DragDropEffects.Copy;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
            e.Handled = true;

        }

        private void Window_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            {
                if (e.Delta < 0)
                {
                    text_area.FontSize -= 1;
                    e.Handled = true;
                }
                if (e.Delta > 0)
                {
                    text_area.FontSize += 1;
                    e.Handled = true;
                }
            }

        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void Window_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            {
                if (e.Delta < 0)
                {
                    text_area.FontSize -= 1;
                    e.Handled = true;
                }
                if (e.Delta > 0)
                {
                    text_area.FontSize += 1;
                    e.Handled = true;
                }
            }
        }

        
    }
}
