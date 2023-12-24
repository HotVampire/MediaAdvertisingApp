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

using MediaAdvertisingApp.AppData;

namespace MediaAdvertisingApp
{
    public partial class EmployeesPage : Page
    {
        private readonly MediaAdvertisingDBEntities _dbContext = new MediaAdvertisingDBEntities();

        public EmployeesPage()
        {
            InitializeComponent();
            LoadEmployees(); // Загрузка данных при инициализации страницы
        }

        private void EmployeesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool isRowSelected = EmployeesDataGrid.SelectedItem != null;

            // Включаем или отключаем кнопки в зависимости от выбранной строки
            AddEmployeeButton.IsEnabled = true; // Добавление всегда доступно
            DeleteEmployeeButton.IsEnabled = isRowSelected;
            EditEmployeeButton.IsEnabled = isRowSelected;
        }

        private void LoadEmployees()
        {
            try
            {
                var employees = _dbContext.Сотрудник
                    .Select(emp => new
                    {
                        Сотрудник_ID = emp.Сотрудник_ID,
                        ФИО_сотрудника = emp.ФИО_сотрудника,
                        Должность = emp.Должность
                    })
                    .ToList();

                EmployeesDataGrid.ItemsSource = employees;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}");
            }
        }

        private void AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            // Создание нового окна для добавления сотрудника
            AddEmployeeWindow addEmployeeWindow = new AddEmployeeWindow();
            addEmployeeWindow.ShowDialog(); // Отображение окна как модального

            LoadEmployees(); // Перезагрузка данных после закрытия окна добавления
        }

        private void DeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeesDataGrid.SelectedItem != null)
            {
                try
                {
                    dynamic selectedEmployee = EmployeesDataGrid.SelectedItem;

                    int employeeId = selectedEmployee.Сотрудник_ID; // Получение ID выбранного сотрудника

                    var employeeToDelete = _dbContext.Сотрудник.FirstOrDefault(emp => emp.Сотрудник_ID == employeeId);

                    if (employeeToDelete != null)
                    {
                        _dbContext.Сотрудник.Remove(employeeToDelete); // Удаление сотрудника из БД
                        _dbContext.SaveChanges(); // Сохранение изменений

                        LoadEmployees(); // Перезагрузка данных после удаления
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка удаления сотрудника: {ex.Message}");
                }
            }
        }

        private void EditEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeesDataGrid.SelectedItem != null)
            {
                // Получение выбранного сотрудника
                var selectedEmployee = (dynamic)EmployeesDataGrid.SelectedItem;

                // Создание нового экземпляра Сотрудник и передача данных
                Сотрудник employee = new Сотрудник
                {
                    Сотрудник_ID = selectedEmployee.Сотрудник_ID,
                    ФИО_сотрудника = selectedEmployee.ФИО_сотрудника,
                    Должность = selectedEmployee.Должность
                };

                // Создание нового окна для редактирования сотрудника
                EditEmployeeWindow editEmployeeWindow = new EditEmployeeWindow(employee);
                editEmployeeWindow.ShowDialog(); // Отображение окна как модального

                LoadEmployees(); // Перезагрузка данных после закрытия окна редактирования
            }
        }
    }
}
