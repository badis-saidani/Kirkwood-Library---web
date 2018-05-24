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
using System.Windows.Shapes;
using DataAccessLayer;
using DataTransferObjects;
using LogicLayer;

namespace KirkwoodLibrary
{
    /// <summary>
    /// Logique d'interaction pour frmLogIn.xaml
    /// </summary>
    public partial class frmLogIn : Window
    {
        private AdminnManager _adminnManager = new AdminnManager();
        //private EquipmentManager _equipmentManager = new EquipmentManager();
        //private List<Equipment> _equipmentList;

        private Adminn _adminn = null;

        private const int MIN_PASSWORD_LENGTH = 5;  // business rule
        private const int MIN_USERNAME_LENGTH = 8;  // forced by naming rules
        private const int MAX_USERNAME_LENGTH = 100;// forced by db field length        

        public frmLogIn()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
           /* MessageBox.Show("welcome", "Login successed!",
                   MessageBoxButton.OK, MessageBoxImage.Exclamation);
            return;*/

            if (_adminn != null) // this means someone is logged in, so log out!
            {
                logout();
                return;
            }

            // accept the input
            var username = txtUsername.Text;
            var password = txtPassword.Password;

            // check for missing or invalid data
            if (username.Length < MIN_USERNAME_LENGTH ||
                username.Length > MAX_USERNAME_LENGTH)
            {
                MessageBox.Show("Invalid Username", "Login Failed!",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);

                clearLogin();

                return;
            }
            if (password.Length <= MIN_PASSWORD_LENGTH)
            {
                MessageBox.Show("Invalid Password", "Login Failed!",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);

                clearLogin();
                return;
            }

            // normally, we would also include some logic here to test for
            // password complexity rules, usually against a regular expression.
            // That makes testing slow, during development, because passwords
            // that are complex are a pain in the neck.

            // before checking for the user token, we need to use a try block
            try
            {
                _adminn = _adminnManager.AuthenticateUser(username, password);

              /*  if (_adminn.Roles.Count == 0)
                {
                    // check for unauthorized user
                    _adminn = null;

                    MessageBox.Show("You have not been assigned any roles. \nYou will be logged out. \nPlease see your supervisor.",
                        "Unauthorized Admin", MessageBoxButton.OK,
                        MessageBoxImage.Stop);

                    clearLogin();

                    return;
                }*/
                // user is now logged in
                var message = "Welcome back, " + _adminn.FirstName +
                    ". You are logged in as: ";
                /*foreach (var r in _adminn.Roles)
                {
                    message += r.RoleID + "   ";
                }*/

                //showUserTabs();
                statusMain.Items[0] = message;

                clearLogin();
                txtPassword.Visibility = Visibility.Hidden;
                txtUsername.Visibility = Visibility.Hidden;
                lblPassword.Visibility = Visibility.Hidden;
                lblUsername.Visibility = Visibility.Hidden;

                // we need to stop having the login button as default for hitting
                // the enter key when someone is logged in to prevent accidental logouts
                this.btnLogin.IsDefault = false;
                btnLogin.Content = "Log Out";

                // check for expired password
               /* if (_adminn.PasswordMustBeChanged)
                {
                    changePassword();
                }*/


            }
            catch (Exception ex) // nowhere to throw an exception at the presentation layer
            {
                string message = ex.Message;

                if (ex.InnerException != null)
                {
                    message += "\n\n" + ex.InnerException.Message;
                }

                MessageBox.Show(message, "Login Failed!",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);

                clearLogin();
                return;
            }
        }

        private void logout()
        {
            _adminn = null;
            // do anything else we need to do to clear the screen, to be done later

            // reenable the login controls
          /*  txtPassword.Visibility = Visibility.Visible;
            txtUsername.Visibility = Visibility.Visible;
            lblPassword.Visibility = Visibility.Visible;
            lblUsername.Visibility = Visibility.Visible;
            btnLogin.Content = "Log In";
            clearLogin();
            statusMain.Items[0] = "You are not logged in.";*/

            //hideAllTabs();
        }

        private void clearLogin()
        {
            this.btnLogin.IsDefault = true;
            txtUsername.Text = "";
            txtPassword.Password = "";
            txtUsername.Focus();
        }
    }
}
