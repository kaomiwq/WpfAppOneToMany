using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAppOneToMany
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Ananyin223OneToManyDbEntities db;
        public MainWindow()
        {
            InitializeComponent();

            db = new Ananyin223OneToManyDbEntities();

            dataGridAuthors.ItemsSource = db.Authors.ToList();
            dataGridBooks.ItemsSource = db.Books.ToList();
            comboBoxAuthor.ItemsSource = db.Authors.ToList();
            comboBoxAuthor.DisplayMemberPath = "Name";

        }
        private void dataGridAuthors_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            Author selectedAuthor = (Author)dataGridAuthors.SelectedItem;
            if (selectedAuthor != null)
            {
                textBoxAuthorId.Text = selectedAuthor.Id.ToString();
                textBoxAuthorName.Text = selectedAuthor.Name;
                textBoxAuthorCountry.Text = selectedAuthor.Country;
            }
        }
        private void dataGridBooks_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            Book selectedBook = (Book)dataGridBooks.SelectedItem;
            if (selectedBook != null)
            {
                textBoxBookId.Text = selectedBook.Id.ToString();
                textBoxBookTitle.Text = selectedBook.Title;
                textBoxBookYear.Text = selectedBook.Year.ToString();
                comboBoxAuthor.Text = selectedBook.AuthorId.ToString();
            }
        }
        private void buttonClearAuthorFields_Click(object sender, RoutedEventArgs e)
        {
            textBoxAuthorId.Clear();
            textBoxAuthorName.Clear();
            textBoxAuthorCountry.Clear();
        }
        private void buttonClearBookFields_Click(object sender, RoutedEventArgs e)
        {
            textBoxBookId.Clear();
            textBoxBookTitle.Clear();
            textBoxBookYear.Clear();
            comboBoxAuthor.SelectedIndex = -1;
        }
        private void ButtonDeleteCurrentAuthor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Author selectedAuthor = (Author)dataGridAuthors.SelectedItem;

                db.Authors.Remove(selectedAuthor);

                db.SaveChanges();

                dataGridAuthors.ItemsSource = null;
                dataGridAuthors.ItemsSource = db.Authors.ToList();
                

                textBoxAuthorId.Clear();
                textBoxAuthorName.Clear();
                textBoxAuthorCountry.Clear();

                MessageBox.Show("Успех. Автор удален.");
            }
            catch
            {
                MessageBox.Show("Ошибка. Проблема соединения с БД. Повторите попытку позже.");
            }
        }
        private void ButtonDeleteCurrentBook_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Book selectedBook = (Book)dataGridBooks.SelectedItem;

                db.Books.Remove(selectedBook);

                db.SaveChanges();

                dataGridBooks.ItemsSource = null;
                dataGridBooks.ItemsSource = db.Books.ToList();

                textBoxBookId.Clear();
                textBoxBookTitle.Clear();
                textBoxBookYear.Clear();
                comboBoxAuthor.SelectedIndex = -1;

                MessageBox.Show("Успех. Книга удалена.");
            }
            catch
            {
                MessageBox.Show("Ошибка. Проблема соединения с БД. Повторите попытку позже.");
            }
        }
        
        private void buttonAddBook_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxBookTitle.Text.Length == 0)
            {
                MessageBox.Show("Ошибка. Название не заполнено.");
                return;
            }

            if (textBoxBookYear.Text.Length == 0)
            {
                MessageBox.Show("Ошибка. Год выпуска не заполнена.");
                return;
            }

            if (comboBoxAuthor.Text.Length == 0)
            {
                MessageBox.Show("Ошибка. Автор не выбран.");
                return;
            }

            Book newBook = new Book
            {
                Title = textBoxBookTitle.Text,
                Year = int.Parse(textBoxBookYear.Text),
                Author = (Author)comboBoxAuthor.SelectedItem
            };

            db.Books.Add(newBook);

            db.SaveChanges();

            dataGridBooks.ItemsSource = null;
            dataGridBooks.ItemsSource = db.Books.ToList();

            textBoxBookId.Clear();
            textBoxBookTitle.Clear();
            textBoxBookYear.Clear();
            comboBoxAuthor.SelectedIndex = -1;

            MessageBox.Show("Успех. Книга успешно добавлена.");
        }
        private void buttonUpdateAuthor_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxAuthorId.Text.Length == 0)
            {
                MessageBox.Show("Ошибка. ИД не заполнен.");
                return;
            }

            if (textBoxAuthorName.Text.Length == 0)
            {
                MessageBox.Show("Ошибка. ФИО не заполнено.");
                return;
            }

            if (textBoxAuthorCountry.Text.Length == 0)
            {
                MessageBox.Show("Ошибка. Страна не заполнена.");
                return;
            }

            int updatedId = int.Parse(textBoxAuthorId.Text);

            Author selectedAuthor = db.Authors.FirstOrDefault(item => item.Id == updatedId);

            selectedAuthor.Name = textBoxAuthorName.Text;
            selectedAuthor.Country = textBoxAuthorCountry.Text;

            db.SaveChanges();

            dataGridAuthors.ItemsSource = null;
            dataGridAuthors.ItemsSource = db.Authors.ToList();
           

            textBoxAuthorId.Clear();
            textBoxAuthorName.Clear();
            textBoxAuthorCountry.Clear();

            MessageBox.Show("Успех. Автор успешно обновлён.");

        }
        private void buttonUpdateBook_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxBookId.Text.Length == 0)
            {
                MessageBox.Show("Ошибка. ИД не заполнен.");
                return;
            }

            if (textBoxBookTitle.Text.Length == 0)
            {
                MessageBox.Show("Ошибка. Название не заполнено.");
                return;
            }

            if (textBoxBookYear.Text.Length == 0)
            {
                MessageBox.Show("Ошибка. Год выпуска не заполнен.");
                return;
            }
            if (comboBoxAuthor.Text.Length == 0)
            {
                MessageBox.Show("Ошибка. Автор не выбран.");
                return;
            }

            int updatedId = int.Parse(textBoxBookId.Text);

            Book selectedBook = db.Books.FirstOrDefault(item => item.Id == updatedId);

            selectedBook.Title = textBoxBookTitle.Text;
            selectedBook.Year = int.Parse(textBoxBookYear.Text);
            selectedBook.AuthorId = int.Parse(comboBoxAuthor.Text);

            db.SaveChanges();

            dataGridBooks.ItemsSource = null;
            dataGridBooks.ItemsSource = db.Books.ToList();

            textBoxBookId.Clear();
            textBoxBookTitle.Clear();
            textBoxBookYear.Clear();
            comboBoxAuthor.SelectedIndex = -1;

            MessageBox.Show("Успех. Книга успешно обновлена.");

        }

        private void buttonAddAuthor_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxAuthorName.Text.Length == 0)
            {
                MessageBox.Show("Ошибка. ФИО не заполнено.");
                return;
            }

            if (textBoxAuthorCountry.Text.Length == 0)
            {
                MessageBox.Show("Ошибка. Страна не заполнена.");
                return;
            }


            Author newAuthor = new Author
            {
                Name = textBoxAuthorName.Text,
                Country = textBoxAuthorCountry.Text
            };

            db.Authors.Add(newAuthor);

            db.SaveChanges();

            dataGridAuthors.ItemsSource = null;
            dataGridAuthors.ItemsSource = db.Authors.ToList();
           

            textBoxAuthorId.Clear();
            textBoxAuthorName.Clear();
            textBoxAuthorCountry.Clear();

            MessageBox.Show("Успех. Автор успешно добавлен.");

        }
    }
}
