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

namespace KirkwoodLibrary
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        

        private void btnLoginAsAdmin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //var adminLoginWindow = new frmLogIn();
                var adminLoginWindow = new frmAdminWindow();

                var result = adminLoginWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                string message = ex.Message;

                if (ex.InnerException != null)
                {
                    message += "\n\n" + ex.InnerException.Message;
                }

                MessageBox.Show(message, "Login Failed!",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);

                
                return;
            }
            
        }

        private void btnLoginAsStudent_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //var adminLoginWindow = new frmLogIn();
                var studentLoginWindow = new frmStudentWindow();

                var result = studentLoginWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                string message = ex.Message;

                if (ex.InnerException != null)
                {
                    message += "\n\n" + ex.InnerException.Message;
                }

                MessageBox.Show(message, "Login Failed!",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);


                return;
            }
            
        }
    }
}
