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
    /// Interaction logic for frmStudentWindow.xaml
    /// </summary>
    public partial class frmStudentWindow : Window
    {
        private StudentManager _studentManager = new StudentManager();
        private BookManager _bookManager = new BookManager();
        private List<Book> _bookList;
        //private List<Student> _studentList;

        private Student _student = null;

        private const int MIN_PASSWORD_LENGTH = 5;  // business rule
        private const int MIN_USERNAME_LENGTH = 8;  // forced by naming rules
        private const int MAX_USERNAME_LENGTH = 100;// forced by db field length

        public frmStudentWindow()
        {

            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            /* MessageBox.Show("welcome", "Login successed!",
                   MessageBoxButton.OK, MessageBoxImage.Exclamation);
            return;*/

            if (_student != null) // this means someone is logged in, so log out!
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
                _student = _studentManager.AuthenticateUser(username, password);

                /*  if (_student.Roles.Count == 0)
                  {
                      // check for unauthorized user
                      _student = null;

                      MessageBox.Show("You have not been assigned any roles. \nYou will be logged out. \nPlease see your supervisor.",
                          "Unauthorized Student", MessageBoxButton.OK,
                          MessageBoxImage.Stop);

                      clearLogin();

                      return;
                  }*/
                // user is now logged in
                var message = "Welcome back, " + _student.FirstName +
                    ". You are logged in as: Student";
                /*foreach (var r in _student.Roles)
                {
                    message += r.RoleID + "   ";
                }*/

                showUserTabs();
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
                /* if (_student.PasswordMustBeChanged)
                 {
                     changePassword();
                 }*/


            }
            catch (Exception ex) // nowhere to throw an exception at the presentation layer
            {
                //string message = ex.Message;

                string message = "You are not authorized to use this app.\n Please contact the administration of the library.";


                //if (ex.InnerException != null)
                //{
                //    message += "\n\n" + ex.InnerException.Message;
                //}

                MessageBox.Show(message, "Login Failed!",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);

                clearLogin();
                return;
            }
        }

        private void logout()
        {
            _student = null;
            // do anything else we need to do to clear the screen, to be done later

            // reenable the login controls
            txtPassword.Visibility = Visibility.Visible;
            txtUsername.Visibility = Visibility.Visible;
            lblPassword.Visibility = Visibility.Visible;
            lblUsername.Visibility = Visibility.Visible;
            btnLogin.Content = "Log In";
            clearLogin();
            statusMain.Items[0] = "You are not logged in.";

            hideAllTabs();
        }

        private void clearLogin()
        {
            this.btnLogin.IsDefault = true;
            txtUsername.Text = "";
            txtPassword.Password = "";
            txtUsername.Focus();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //if(_student.Active == false)
            //{
            //    MessageBox.Show("You are not authorized to use this app.\n Please contact the administration of the library. ", "Login Failed!!!",
            //        MessageBoxButton.OK, MessageBoxImage.Exclamation);
            //    logout();
            //    return;
            //}
            txtUsername.Focus();
            this.btnLogin.IsDefault = true;
            hideAllTabs();

            refreshBookList();
        }

        private void showUserTabs()
        {
            this.lblScreenCover.Visibility = Visibility.Hidden;
            foreach (var tab in tabsetMain.Items)
            {
                // collapse all the tabs
                ((TabItem)tab).Visibility = Visibility.Visible;

                // hide the tabset completely
                tabsetMain.Visibility = Visibility.Visible;
            }
            tabBooks.IsSelected = true;
        }
        private void hideAllTabs()
        {
            foreach (var tab in tabsetMain.Items)
            {
                // collapse all the tabs
                ((TabItem)tab).Visibility = Visibility.Collapsed;
                // hide the tabset completely
                tabsetMain.Visibility = Visibility.Hidden;
            }
            this.lblScreenCover.Visibility = Visibility.Visible;
        }


        private void refreshBookList(bool active = true)
        {
            try
            {
                _bookList = _bookManager.RetrieveBookList(active);
            }
            catch (Exception ex)
            {
                var message = ex.Message + "\n\n" + ex.InnerException.Message;
                MessageBox.Show(message, "Data Retrieval Error!",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        

        private void TabItem_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void tabBooks_GotFocus(object sender, RoutedEventArgs e)
        {
            dgBooks.ItemsSource = _bookList;
        }

        private void btnBookDetails_Click(object sender, RoutedEventArgs e)
        {
            Book bookItem = null;
            Book bkDetail = null;
            if (this.dgBooks.SelectedItems.Count > 0)
            {

                bookItem = (Book)this.dgBooks.SelectedItem;
                try
                {
                    bkDetail = _bookManager.RetrieveBookByID(bookItem);
                    var frmDetails = new frmBookDetail(_bookManager, bkDetail, BookDetailForm.View);
                    //var frmDetails = new frmBookDetail(_bookManager, bkDetail, BookDetailForm.View, string userRole);
                    var result = frmDetails.ShowDialog();
                    if (result == true)
                    {
                        refreshBookList();
                        dgBooks.ItemsSource = _bookList;
                    }

                    // frmDetails.btnEditSave.Content = "Save";
                    frmDetails.btnEditSave.Visibility = Visibility.Hidden;
                    frmDetails.txtStudentID.Visibility = Visibility.Hidden;
                    frmDetails.txtCheckout.Visibility = Visibility.Hidden;
                    frmDetails.txtreturn.Visibility = Visibility.Hidden;
                    frmDetails.lblStudentID.Visibility = Visibility.Hidden;
                    frmDetails.lblCheckout.Visibility = Visibility.Hidden;
                    frmDetails.lblReturn.Visibility = Visibility.Hidden;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message, "Ouch!");
                }
            }
            else
            {
                MessageBox.Show("You need to select something!");
            }

            // get the item and pass it to an book manager method
            // to return an book detail object, which we will pass
            // to an book detail window for display
        }

     


        private void refreshBooks()
        {
            dgBooks.ItemsSource = null;
            dgBooks.Items.Clear();
            refreshBookList();
            dgBooks.ItemsSource = _bookList;
        }

        

        private void refreshBookListByID(int bookID)
        {
            try
            {
                _bookList = _bookManager.RetrieveBookListByID(bookID);
            }
            catch (Exception ex)
            {
                var message = ex.Message + "\n\n" + ex.InnerException.Message;
                MessageBox.Show(message, "Data Retrieval Error!",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void refreshBookListByWord(string word)
        {
            try
            {
                _bookList = _bookManager.RetrieveBookListByWord(word);
            }
            catch (Exception ex)
            {
                var message = ex.Message + "\n\n" + ex.InnerException.Message;
                MessageBox.Show(message, "Data Retrieval Error!",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void btnSearchByID_Click(object sender, RoutedEventArgs e)
        {
            int bookID;
            if (int.TryParse(this.txtSearchByID.Text, out bookID))
            {

                refreshBookListByID(bookID);
                dgBooks.ItemsSource = _bookList;
                txtSearchByID.Text = "";
            }
            else
            {
                refreshBookList();
                dgBooks.ItemsSource = _bookList;
            }

        }

        private void btnSearchByWord_Click(object sender, RoutedEventArgs e)
        {
            string word = "";
            word = txtSearchByWord.Text;
            if (word != "")
            {

                refreshBookListByWord(word);
                dgBooks.ItemsSource = _bookList;
                txtSearchByWord.Text = "";
            }
            else
            {
                refreshBookList();
                dgBooks.ItemsSource = _bookList;
            }
        }

        private void btnHoldBook_Click(object sender, RoutedEventArgs e)
        {
            if (dgBooks.SelectedItems.Count == 0)
            {
                MessageBox.Show("You need to select something!");
                return;
            }
            else
            {
                Book book = (Book)dgBooks.SelectedItem;
                if (book.StatusID != "In Held" && book.StatusID != "Out")
                {
                    var result = MessageBox.Show("Do you want to hold " +
                    book.Title +
                    "?", "Hold Book",
                    MessageBoxButton.OKCancel, MessageBoxImage.Warning);


                    if (result == MessageBoxResult.OK)
                    {

                        _bookManager.HoldBook(book, _student.StudentID);
                        refreshBooks();


                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Book is not available.");
                }
            }
        }
    }
}
