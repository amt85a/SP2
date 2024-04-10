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
using MahApps.Metro.Controls;
using System.Windows.Shapes;
using Flocks_Desk.Models;
using Flocks_Desk.Views;

namespace Flocks_Desk
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class AuthenticateWindow : MetroWindow
    {
        public AuthenticateWindow()
        {
            InitializeComponent();
        }

        private void Btn_Signin_Click(object sender, RoutedEventArgs e)
        {
            String message = Auth.Login(tbx_email.Text, pbx_pwd.Password);

            if (message == "Success")
            {

                // Create and Show the ShellWindow view
                DashboardWindow mainWindow = new DashboardWindow();
                mainWindow.Show();

                // Close AuthenticateWindow
                this.Close();
            }
            else
            {
                // Set error message
                tbx_message.Text = message;
            }

        }
    }
}
