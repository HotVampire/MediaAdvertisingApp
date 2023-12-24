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
    public partial class EditEmployeeWindow : Window
    {
        private readonly dynamic _employeeData;

        public EditEmployeeWindow(dynamic employeeData)
        {
            InitializeComponent();
            _employeeData = employeeData;

            // Заполнение данными окна редактирования
            EmployeeNameTextBox.Text = _employeeData.ФИО_сотрудника;
            PositionTextBox.Text = _employeeData.Должность;
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Применение изменений
                _employeeData.ФИО_сотрудника = EmployeeNameTextBox.Text;
                _employeeData.Должность = PositionTextBox.Text;

                // Сохранение изменений в базе данных или другая обработка данных

                MessageBox.Show("Изменения сохранены!");
                Close(); // Закрытие окна после сохранения
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}");
            }
        }
    }
}
