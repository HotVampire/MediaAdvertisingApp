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

using MediaAdvertisingApp.AppData;

namespace MediaAdvertisingApp
{
    public partial class AddEmployeeWindow : Window
    {
        private readonly MediaAdvertisingDBEntities _dbContext = new MediaAdvertisingDBEntities();

        public AddEmployeeWindow()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Создание нового сотрудника и добавление его в базу данных
                Сотрудник newEmployee = new Сотрудник
                {
                    ФИО_сотрудника = EmployeeNameTextBox.Text,
                    Должность = PositionTextBox.Text
                };

                _dbContext.Сотрудник.Add(newEmployee);
                _dbContext.SaveChanges();

                MessageBox.Show("Новый сотрудник добавлен!");
                Close(); // Закрытие окна после добавления
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка добавления сотрудника: {ex.Message}");
            }
        }
    }
}
