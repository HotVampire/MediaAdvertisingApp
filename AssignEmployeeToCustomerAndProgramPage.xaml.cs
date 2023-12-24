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
    public partial class AssignEmployeeToCustomerAndProgramPage : Page
    {
        private readonly MediaAdvertisingDBEntities _dbContext = new MediaAdvertisingDBEntities();

        public AssignEmployeeToCustomerAndProgramPage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var customers = _dbContext.Заказчик.ToList();
                var programs = _dbContext.Программа.ToList();
                var employees = _dbContext.Сотрудник.ToList();

                CustomersComboBox.ItemsSource = customers;
                ProgramsComboBox.ItemsSource = programs;
                EmployeesComboBox.ItemsSource = employees;

                EmployeesListBox.ItemsSource = employees;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}");
            }
        }

        private void AssignButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CustomersComboBox.SelectedItem != null &&
                    ProgramsComboBox.SelectedItem != null &&
                    EmployeesComboBox.SelectedItem != null)
                {
                    var selectedCustomer = (Заказчик)CustomersComboBox.SelectedItem;
                    var selectedProgram = (Программа)ProgramsComboBox.SelectedItem;
                    var selectedEmployee = (Сотрудник)EmployeesComboBox.SelectedItem;

                    selectedCustomer.Программа_ID = selectedProgram.Программа_ID;
                    selectedEmployee.ID_заказчика = selectedCustomer.Заказчик_ID;
                    selectedEmployee.ID_программы = selectedProgram.Программа_ID;

                    _dbContext.SaveChanges();
                    LoadData();

                    SelectedEmployeeTextBlock.Text = string.Empty;
                    SelectedProgramTextBlock.Text = string.Empty;
                    SelectedCustomerTextBlock.Text = string.Empty;

                    MessageBox.Show("Заказчику присвоена программа, а сотруднику присвоены заказчик и программа!");
                }
                else
                {
                    MessageBox.Show("Пожалуйста, выберите программу и сотрудника.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при привязке данных: {ex.Message}");
            }
        }

        private void UnassignButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (var item in EmployeesListBox.Items)
                {
                    var listBoxItem = EmployeesListBox.ItemContainerGenerator.ContainerFromItem(item) as ListBoxItem;
                    var checkBox = FindVisualChild<CheckBox>(listBoxItem);

                    if (checkBox != null && checkBox.IsChecked == true)
                    {
                        var selectedEmployee = (Сотрудник)listBoxItem.DataContext;
                        selectedEmployee.ID_заказчика = null;
                        selectedEmployee.ID_программы = null;
                    }
                }

                _dbContext.SaveChanges();
                LoadData();

                SelectedEmployeeTextBlock.Text = string.Empty;
                SelectedProgramTextBlock.Text = string.Empty;
                SelectedCustomerTextBlock.Text = string.Empty;

                MessageBox.Show("Отвязка успешно выполнена!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при отвязке данных: {ex.Message}");
            }
        }

        private T FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (child != null && child is T)
                {
                    return (T)child;
                }
                else
                {
                    T childOfChild = FindVisualChild<T>(child);
                    if (childOfChild != null)
                    {
                        return childOfChild;
                    }
                }
            }
            return null;
        }
    }
}
