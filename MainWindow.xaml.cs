using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Lab4.ViewModels;


namespace Lab4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += Window_Loaded;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Style != null)
            {
                Button b1 = (Button)this.Template.FindName("btnClose", this);
                b1.Click += (sender1, e1) => { Close(); };
                Button b2 = (Button)this.Template.FindName("btnMinimize", this);
                b2.Click += (sender1, e1) =>
                {
                    if (WindowState != WindowState.Minimized)
                    {
                        WindowState = WindowState.Minimized;
                    }
                };
                Button b3 = (Button)this.Template.FindName("btnMaximize", this);
                b3.Click += (sender1, e1) =>
                {
                    if (WindowState != WindowState.Maximized)
                    {
                        WindowState = WindowState.Maximized;
                    }
                    else
                    {
                        WindowState = WindowState.Normal;
                    }
                };
                Grid grid = (Grid)this.Template.FindName("gridMove", this);
                grid.MouseLeftButtonDown += (sender1, e1) => { DragMove(); };
            }
        }

    }
}
