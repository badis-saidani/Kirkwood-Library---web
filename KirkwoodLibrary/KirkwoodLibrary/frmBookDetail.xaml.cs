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
using DataTransferObjects;
using LogicLayer;


namespace KirkwoodLibrary
{
    /// <summary>
    /// Interaction logic for frmBookDetail.xaml
    /// </summary>
    public partial class frmBookDetail : Window
    {
        private BookManager _bookManager;
        private Book _bookDetail;
        private List<Status> _bookStatusList;
        private BookDetailForm _type;
        private List<Category> _categories;
        private List<Library> _libraries;

        public frmBookDetail(BookManager bkMgr, Book bkDetail, BookDetailForm type)
        {
            _bookManager = bkMgr;
            _bookDetail = bkDetail;
            _type = type;

            InitializeComponent();
        }
        public frmBookDetail(BookManager bkMgr)
        {
            _bookManager = bkMgr;
            _type = BookDetailForm.Add;

            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            try
            {
                _bookStatusList = _bookManager.RetrieveStatusList();
                _categories = _bookManager.RetrieveCategoryList();
                _libraries = _bookManager.RetrieveLibraryList();

                this.cboLibrary.ItemsSource = _libraries;
                this.cboBookCategory.ItemsSource = _categories;
                this.cboStatus.ItemsSource = _bookStatusList;
            }
            catch
            {
                MessageBox.Show("One or more option lists were not found!");
            }
            switch (_type)  // how should we set up the form?
            {
                case BookDetailForm.Add:
                    setupAddForm();
                    break;
                case BookDetailForm.Edit:
                    setupEditForm();
                    break;
                case BookDetailForm.View:
                    setupViewForm();
                    break;
                default:
                    break;
            }
            this.txtBookID.IsEnabled = false;

            /*************/
            if (cboStatus.Text == "Available")
            {
                this.txtStudentID.Visibility = Visibility.Hidden;
                this.txtCheckout.Visibility = Visibility.Hidden;
                this.txtreturn.Visibility = Visibility.Hidden;

                this.lblStudentID.Visibility = Visibility.Hidden;
                this.lblCheckout.Visibility = Visibility.Hidden;
                this.lblReturn.Visibility = Visibility.Hidden;
            }
            else if (cboStatus.Text == "Out")
            {
                this.txtStudentID.Visibility = Visibility.Visible;
                this.txtCheckout.Visibility = Visibility.Visible;
                this.txtreturn.Visibility = Visibility.Visible;

                this.lblStudentID.Visibility = Visibility.Visible;
                this.lblCheckout.Visibility = Visibility.Visible;
                this.lblReturn.Visibility = Visibility.Visible;
            }
            else if (cboStatus.Text == "In Held")
            {
                this.txtStudentID.Visibility = Visibility.Visible;
                this.txtCheckout.Visibility = Visibility.Hidden;
                this.txtreturn.Visibility = Visibility.Hidden;

                this.lblStudentID.Visibility = Visibility.Visible;
                this.lblCheckout.Visibility = Visibility.Hidden;
                this.lblReturn.Visibility = Visibility.Hidden;
            }
        }

        private void disableInputs()
        {

            this.txtBookID.IsReadOnly = true;
            this.txtISBN.IsReadOnly = true;
            this.txtTitle.IsReadOnly = true;
            this.txtEdition.IsReadOnly = true;
            this.txtEditionYear.IsReadOnly = true;
            this.cboStatus.IsReadOnly = true;
            this.txtAuthor.IsReadOnly = true;
            this.txtBookDescription.IsReadOnly = true;
            this.cboBookCategory.IsReadOnly = true;
            this.txtAuthor.IsReadOnly = true;
            this.txtStudentID.IsReadOnly = true;
            this.txtCheckout.IsReadOnly = true;
            this.txtreturn.IsReadOnly = true;
        }

      /*  private void btnEditSave_Click(object sender, RoutedEventArgs e)
        {
            if (_mode == BookDetailMode.View) // if in view mode, switch to edit mode
            {
                setupEditMode();
                this.btnSaveEdit.Content = "Save";
                return;
            }
            // need logic to actually save the record
        }*/

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void setupAddForm()
        {
            this.btnEditSave.Content = "Save";
            this.Title = "Add a New Book Record";
            this.cboStatus.Text = "Available";
            this.cboStatus.IsEnabled = false;

            this.txtStudentID.Visibility = Visibility.Hidden;
            this.txtCheckout.Visibility = Visibility.Hidden;
            this.txtreturn.Visibility = Visibility.Hidden;

            this.lblStudentID.Visibility = Visibility.Hidden;
            this.lblCheckout.Visibility = Visibility.Hidden;
            this.lblReturn.Visibility = Visibility.Hidden;
        }
        private void setupEditForm()
        {
            this.btnEditSave.Content = "Save";
            this.Title = "Edit an Book Record";

            setControls(readOnly: false);
            populateControls();
        }
        private void setupViewForm()
        {
            this.btnEditSave.Content = "Edit";
            this.Title = "Review an Book Record";
            populateControls();
            setControls(readOnly: true);
        }

        /*private void setInputs(bool readOnly = true)
        {
            this.txtBookID.IsReadOnly = true; // this doesn't change - not user editable
            this.txtISBN.IsReadOnly = readOnly;
            this.txtTitle.IsReadOnly = readOnly;
            this.txtEdition.IsReadOnly = readOnly;
            this.txtEditionYear.IsReadOnly = readOnly;
            this.cboStatus.IsReadOnly = !readOnly;
            this.txtAuthor.IsReadOnly = readOnly;
            this.txtBookDescription.IsReadOnly = readOnly;
            this.cboBookCategory.IsReadOnly = readOnly;
            this.txtAuthor.IsReadOnly = readOnly;
            this.txtStudentID.IsReadOnly = readOnly;
            this.txtCheckout.IsReadOnly = readOnly;
            this.txtreturn.IsReadOnly = readOnly;
        }*/

        private void populateControls()
        {

            this.txtBookID.Text = _bookDetail.BookID.ToString();
            this.txtISBN.Text = _bookDetail.ISBN;
            this.txtTitle.Text = _bookDetail.Title;
            this.txtEdition.Text = _bookDetail.Edition;
            this.txtEditionYear.Text = _bookDetail.EditionYear.ToString();
            this.cboStatus.Text = _bookDetail.StatusID;
            this.txtAuthor.Text = _bookDetail.AuthorID.ToString();
            this.txtBookDescription.Text = _bookDetail.Description;
            this.cboLibrary.Text = _bookDetail.LibraryID;
            this.cboBookCategory.Text = _bookDetail.CategoryID;
            this.txtAuthor.Text = _bookDetail.AuthorID.ToString();
            this.txtStudentID.Text = _bookDetail.StudentEmail;
            this.txtCheckout.Text = _bookDetail.DateOfCheckout.ToString();
            this.txtreturn.Text = _bookDetail.DateToReturn.ToString();
        }

        private void setControls(bool readOnly = true)
        {
            this.txtISBN.IsReadOnly = readOnly;
            this.txtTitle.IsReadOnly = readOnly;
            this.txtEdition.IsReadOnly = readOnly;
            this.txtEditionYear.IsReadOnly = readOnly;
            this.cboStatus.IsReadOnly = readOnly;
            this.txtAuthor.IsReadOnly = readOnly;
            this.txtBookDescription.IsReadOnly = readOnly;
            this.cboBookCategory.IsReadOnly = readOnly;
            this.txtAuthor.IsReadOnly = readOnly;
            this.txtStudentID.IsReadOnly = readOnly;
            this.txtCheckout.IsReadOnly = readOnly;
            this.txtreturn.IsReadOnly = readOnly;
        }

        private void btnEditSave_Click(object sender, RoutedEventArgs e)
        {
            if (_type == BookDetailForm.View)
            {
                _type = BookDetailForm.Edit;
                setupEditForm();
                return;
            }

            var book = new Book();

            switch (_type)
            {
                case BookDetailForm.Add:
                    
                    // validate inputs and build the Book
                    if (captureBook(book) == false)
                    {
                        return;
                    }

                    // pass it to a manager class method
                    try
                    {
                        if (_bookManager.SaveNewBook(book))
                        {
                            this.DialogResult = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    break;
                case BookDetailForm.Edit:
                    if (captureBook(book) == false)
                    {
                        return;
                    }
                    book.BookID = _bookDetail.BookID;
                    var oldBook = _bookDetail;
                    // pass it to a manager class method
                    try
                    {
                        string updateType = book.StatusID;

                        if (_bookManager.EditBook(book, oldBook, updateType))
                        {
                            this.DialogResult = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    break;
                case BookDetailForm.View:
                    break;
                default:
                    break;
            }

        }

        private bool captureBook(Book book)
        {
            if (this.cboStatus.SelectedItem == null)
            {
                MessageBox.Show("You must select a status.");
                return false;
            }
            else
            {
                book.StatusID = ((Status)cboStatus.SelectedItem).StatusID;
            }
            if (this.txtISBN.Text == "")
            {
                MessageBox.Show("You must enter an ISBN.");
                return false;
            }
            else
            {
                book.ISBN = txtISBN.Text;
            }
            if (this.txtTitle.Text == "")
            {
                MessageBox.Show("You must enter a title.");
                return false;
            }
            else
            {
                book.Title = txtTitle.Text;
            }
            if (this.txtEdition.Text == "")
            {
                MessageBox.Show("You must enter a Edition.");
                return false;
            }
            else
            {
                book.Edition = txtEdition.Text;
            }
            int year;
            if (!int.TryParse(this.txtEditionYear.Text, out year))
            {
                MessageBox.Show("You must enter a valid year");
                return false;
            }
            else
            {
                book.EditionYear = year;
            }
            int author;
            if (!int.TryParse(this.txtAuthor.Text, out author))
            {
                MessageBox.Show("You must enter an author.");
                return false;
            }
            else
            {
                book.AuthorID = author;
            }

            if (this.cboLibrary.SelectedItem == null)
            {
                MessageBox.Show("You must select a Library.");
                return false;
            }
            else
            {
                book.LibraryID = ((Library)this.cboLibrary.SelectedItem).LibraryID;
            }
            if (this.cboBookCategory.SelectedItem == null)
            {
                MessageBox.Show("You must select an book category.");
                return false;
            }
            else
            {
                book.CategoryID = ((Category)this.cboBookCategory.SelectedItem).CategoryID;
            }

            if (this.txtBookDescription.Text == "")
            {
                MessageBox.Show("You must enter a StudentID.");
                return false;
            }
            else
            {
                book.Description = txtBookDescription.Text;
            }

            if (cboStatus.Text == "In Held" || cboStatus.Text == "Out")
            {
                if (this.txtStudentID.Text == "")
                {
                    MessageBox.Show("You must enter a StudentID.");
                    return false;
                }
                else
                {
                    book.StudentEmail = txtStudentID.Text;
                }
            }
            if (cboStatus.Text == "Out")
            {
                DateTime date1;
                if (!DateTime.TryParse(this.txtCheckout.Text, out date1))
                //  if (this.txtCheckout.Text == "")
                {
                    MessageBox.Show("You must enter a date of checkout.");
                    return false;
                }
                else
                {
                    book.DateOfCheckout = date1;
                }
                DateTime date2;
                if (!DateTime.TryParse(this.txtreturn.Text, out date2))
                //  if (this.txtreturn.Text == "")
                {
                    MessageBox.Show("You must enter a date of return.");
                    return false;
                }
                else
                {
                    book.DateToReturn = date2;
                }
            }


            return true;
        }

       /* private void cboStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboStatus.Text == "Available")
            {
                this.txtStudentID.Visibility = Visibility.Hidden;
                this.txtCheckout.Visibility = Visibility.Hidden;
                this.txtreturn.Visibility = Visibility.Hidden;

                this.lblStudentID.Visibility = Visibility.Hidden;
                this.lblCheckout.Visibility = Visibility.Hidden;
                this.lblReturn.Visibility = Visibility.Hidden;
            }
            else if (cboStatus.Text == "Out")
            {
                this.txtStudentID.Visibility = Visibility.Visible;
                this.txtCheckout.Visibility = Visibility.Visible;
                this.txtreturn.Visibility = Visibility.Visible;

                this.lblStudentID.Visibility = Visibility.Visible;
                this.lblCheckout.Visibility = Visibility.Visible;
                this.lblReturn.Visibility = Visibility.Visible;
            } else if (cboStatus.Text == "In Held")
            {
                this.txtStudentID.Visibility = Visibility.Visible;
                this.txtCheckout.Visibility = Visibility.Hidden;
                this.txtreturn.Visibility = Visibility.Hidden;

                this.lblStudentID.Visibility = Visibility.Visible;
                this.lblCheckout.Visibility = Visibility.Hidden;
                this.lblReturn.Visibility = Visibility.Hidden;
            }


        }*/

        private void cboStatus_MouseLeave(object sender, MouseEventArgs e)
        {
            if (cboStatus.Text == "Available")
            {
                this.txtStudentID.Visibility = Visibility.Hidden;
                this.txtCheckout.Visibility = Visibility.Hidden;
                this.txtreturn.Visibility = Visibility.Hidden;

                this.lblStudentID.Visibility = Visibility.Hidden;
                this.lblCheckout.Visibility = Visibility.Hidden;
                this.lblReturn.Visibility = Visibility.Hidden;
            }
            else if (cboStatus.Text == "Out")
            {
                this.txtStudentID.Visibility = Visibility.Visible;
                this.txtCheckout.Visibility = Visibility.Visible;
                this.txtreturn.Visibility = Visibility.Visible;

                this.lblStudentID.Visibility = Visibility.Visible;
                this.lblCheckout.Visibility = Visibility.Visible;
                this.lblReturn.Visibility = Visibility.Visible;
            }
            else if (cboStatus.Text == "In Held")
            {
                this.txtStudentID.Visibility = Visibility.Visible;
                this.txtCheckout.Visibility = Visibility.Hidden;
                this.txtreturn.Visibility = Visibility.Hidden;

                this.lblStudentID.Visibility = Visibility.Visible;
                this.lblCheckout.Visibility = Visibility.Hidden;
                this.lblReturn.Visibility = Visibility.Hidden;
            }
        }
    }
}
