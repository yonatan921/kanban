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
using Frontend.Model;
using Frontend.View;
using IntroSE.Kanban.Backend.BusinessLayer;
using IntroSE.Kanban.Backend.ServiceLayer;
using MaterialDesignThemes.Wpf;

namespace Frontend
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel viewModel;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel();
            this.viewModel = (MainViewModel) DataContext;
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            Response<string> userModel = viewModel.Login();
            string returnValue = (string)userModel.ReturnValue;
            if (userModel.ErrorMessage == null)
            {
                // UserModel u = new UserModel(viewModel.Controller,(string)(userModel.ReturnValue));
                UserModel u = new UserModel(viewModel.Controller, returnValue);

                Boards boardView = new Boards(u);
                boardView.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show(userModel.ErrorMessage, "Alert", MessageBoxButton.OK, MessageBoxImage.Information);

            }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            Response<string> response = viewModel.Register();
            if (response.ErrorMessage != null)
            {
                MessageBox.Show(response.ErrorMessage, "Alert", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            else
            {
                UserModel u = new UserModel(viewModel.Controller, viewModel.Username);
                Boards boardView = new Boards(u);
                boardView.Show();
                this.Close();
            }
        }

        // public bool isDarkTheme { get; set; }
        // private readonly PaletteHelper paletteHelper = new PaletteHelper();
        //
        // // adding dark Mode
        // private void themeToggle_Click(object sender, RoutedEventArgs e)
        // {
        //     ITheme theme = paletteHelper.GetTheme();
        //
        //     if (isDarkTheme = theme.GetBaseTheme() == BaseTheme.Dark)
        //     {
        //         isDarkTheme = false;
        //         theme.SetBaseTheme(Theme.Light);
        //     }
        //     else
        //     {
        //         isDarkTheme = true;
        //         theme.SetBaseTheme(Theme.Dark);
        //     }
        //
        //     paletteHelper.SetTheme(theme);
        // }

        private void exitApp(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }
    }
}