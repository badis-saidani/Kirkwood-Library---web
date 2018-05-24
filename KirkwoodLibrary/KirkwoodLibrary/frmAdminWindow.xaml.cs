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
    /// Interaction logic for frmAdminWindow.xaml
    /// </summary>
    public partial class frmAdminWindow : Window
    {
        private AdminnManager _adminnManager = new AdminnManager();
        private BookManager _bookManager = new BookManager();
        private List<Book> _bookList;
        private StudentManager _studentManager = new StudentManager();
        private List<Student> _studentList;

        private Adminn _adminn = null;

        private const int MIN_PASSWORD_LENGTH = 5;  // business rule
        private const int MIN_USERNAME_LENGTH = 8;  // forced by naming rules
        private const int MAX_USERNAME_LENGTH = 100;// forced by db field length

        public frmAdminWindow()
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
                    ". You are logged in as: Admin";
                /*foreach (var r in _adminn.Roles)
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
            txtUsername.Focus();
            this.btnLogin.IsDefault = true;
            hideAllTabs();

            refreshBookList();
            refreshStudentList();
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

        private void refreshStudentListByID(string studentID)
        {
            try
            {
                _studentList = _studentManager.RetrieveStudentListByID(studentID);
            }
            catch (Exception ex)
            {
                var message = ex.Message + "\n\n" + ex.InnerException.Message;
                MessageBox.Show(message, "Data Retrieval Error!",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void refreshStudentListByName(string studentID)
        {
            try
            {
                _studentList = _studentManager.RetrieveStudentListByName(studentID);
            }
            catch (Exception ex)
            {
                var message = ex.Message + "\n\n" + ex.InnerException.Message;
                MessageBox.Show(message, "Data Retrieval Error!",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void refreshStudentList(bool active = true)
        {
            try
            {
                _studentList = _studentManager.RetrieveStudentList(active);
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
                    var result = frmDetails.ShowDialog();
                    if (result == true)
                    {
                        refreshBookList();
                        dgBooks.ItemsSource = _bookList;
                    }
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

        private void tabStudents_GotFocus(object sender, RoutedEventArgs e)
        {
            dgStudents.ItemsSource = _studentList;
        }

    

        private void btnAddBook_Click(object sender, RoutedEventArgs e)
        {
            var detailForm = new frmBookDetail(_bookManager);
            var result = detailForm.ShowDialog();
            if (result == true)
            {
                refreshBooks();
            }
        }

        private void btnDeactivateBook_Click(object sender, RoutedEventArgs e)
        {
            if (dgBooks.SelectedItems.Count == 0)
            {
                MessageBox.Show("You need to select something!");
                return;
            }
            else
            {
                var result = MessageBox.Show("Deactivate " +
                    ((Book)(dgBooks.SelectedItem)).Title +
                    "? Are you sure?", "Deactivate Book",
                    MessageBoxButton.OKCancel, MessageBoxImage.Warning);

                if (result == MessageBoxResult.OK)
                {
                    _bookManager.DeactivateBook((Book)dgBooks.SelectedItem);
                    refreshBooks();
                }
                else
                {
                    return;
                }
            }
        }

        private void btnEditBook_Click(object sender, RoutedEventArgs e)
        {
            Book item = null;
            Book bkDetail = null;
            if (this.dgBooks.SelectedItems.Count > 0)
            {
                item = (Book)this.dgBooks.SelectedItem;
                try
                {
                    bkDetail = _bookManager.RetrieveBookByID(item);
                    var detailForm = new frmBookDetail(_bookManager, bkDetail, BookDetailForm.Edit);
                    var result = detailForm.ShowDialog();
                    if (result == true)
                    {
                        refreshBookList();
                        dgBooks.ItemsSource = _bookList;
                    }
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
        }

        private void btnDeactivateStudent_Click(object sender, RoutedEventArgs e)
        {
            if (dgStudents.SelectedItems.Count == 0)
            {
                MessageBox.Show("You need to select something!");
                return;
            }
            else
            {
                var result = MessageBox.Show("Block " +
                    ((Student)(dgStudents.SelectedItem)).FirstName +
                    "? Are you sure?", "Block Student",
                    MessageBoxButton.OKCancel, MessageBoxImage.Warning);

                if (result == MessageBoxResult.OK)
                {
                    _studentManager.DeactivateStudent((Student)dgStudents.SelectedItem);
                    refreshStudents();
                }
                else
                {
                    return;
                }
            }
        }

        private void refreshBooks()
        {
            dgBooks.ItemsSource = null;
            dgBooks.Items.Clear();
            refreshBookList();
            dgBooks.ItemsSource = _bookList;
        }
        private void refreshStudents()
        {
            dgStudents.ItemsSource = null;
            dgStudents.Items.Clear();
            refreshStudentList();
            dgStudents.ItemsSource = _studentList;

        }

        private void btnSearchByID_Click(object sender, RoutedEventArgs e)
        {
            int bookID ; 
                if(int.TryParse( this.txtSearchByID.Text, out bookID))
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

        private void btnDeleteBook_Click(object sender, RoutedEventArgs e)
        {
            if (dgBooks.SelectedItems.Count == 0)
            {
                MessageBox.Show("You need to select something!");
                return;
            }
            else
            {
                var result = MessageBox.Show("Delete " +
                    ((Book)(dgBooks.SelectedItem)).Title +
                    "? Are you sure?", "Delete Book",
                    MessageBoxButton.OKCancel, MessageBoxImage.Warning);

                if (result == MessageBoxResult.OK)
                {
                    _bookManager.DeleteBook((Book)dgBooks.SelectedItem);
                    refreshBooks();
                }
                else
                {
                    return;
                }
            }
        }

        private void btnSearchByStdID_Click(object sender, RoutedEventArgs e)
        {
            string studentID = "";
            studentID = txtSearchByStdID.Text;
            if (studentID != "")
            {

                refreshStudentListByID(studentID);
                dgStudents.ItemsSource = _studentList;
                txtSearchByWord.Text = "";
            }
            else
            {
                refreshStudentList();
                dgStudents.ItemsSource = _studentList;
            }
        }

        private void btnSearchByName_Click(object sender, RoutedEventArgs e)
        {
            string name = "";
            name = txtSearchByName.Text;
            if (name != "")
            {

                refreshStudentListByName(name);
                dgStudents.ItemsSource = _studentList;
                txtSearchByWord.Text = "";
            }
            else
            {
                refreshStudentList();
                dgStudents.ItemsSource = _studentList;
            }
        }
    }
}
