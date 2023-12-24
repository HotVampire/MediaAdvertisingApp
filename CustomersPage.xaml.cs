using MediaAdvertisingApp.AppData;
using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MediaAdvertisingApp
{
    public partial class CustomersPage : Page
    {
        private readonly MediaAdvertisingDBEntities _dbContext = new MediaAdvertisingDBEntities();

        public CustomersPage()
        {
            InitializeComponent();
            LoadCustomers(); // Загрузка данных при инициализации страницы
        }

        private void CustomersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool isRowSelected = CustomersDataGrid.SelectedItem != null;

            // Включаем или отключаем кнопки в зависимости от выбранной строки
            AddCustomerButton.IsEnabled = true; // Добавление всегда доступно
            DeleteCustomerButton.IsEnabled = isRowSelected;
            EditCustomerButton.IsEnabled = isRowSelected;
        }



        private void LoadCustomers()
        {
            var customers = _dbContext.Заказчик
                .Select(c => new {
                    Заказчик_ID = c.Заказчик_ID,
                    Название_компании = c.Название_компании,
                    Длительность_рекламы = c.Длительность_рекламы,
                    Банковские_реквизиты = c.Банковские_реквизиты,
                    Контактный_телефон = c.Контактный_телефон,
                    Контактное_лицо = c.Контактное_лицо
                })
                .ToList();

            CustomersDataGrid.ItemsSource = customers;
        }



        private void AddCustomer_Click(object sender, RoutedEventArgs e)
        {
            // Создание нового окна для добавления заказчика
            AddCustomerWindow addCustomerWindow = new AddCustomerWindow();
            addCustomerWindow.ShowDialog(); // Отображение окна как модального
            LoadCustomers(); // Перезагрузка данных после закрытия окна добавления
        }

        private void DeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (CustomersDataGrid.SelectedItem != null)
            {
                try
                {
                    dynamic selectedCustomer = CustomersDataGrid.SelectedItem;

                    int customerId = selectedCustomer.Заказчик_ID; // Получение ID выбранного заказчика

                    var customerToDelete = _dbContext.Заказчик.FirstOrDefault(c => c.Заказчик_ID == customerId);

                    if (customerToDelete != null)
                    {
                        _dbContext.Заказчик.Remove(customerToDelete); // Удаление заказчика из БД
                        _dbContext.SaveChanges(); // Сохранение изменений

                        LoadCustomers(); // Перезагрузка данных после удаления
                    }
                }
                catch (DbUpdateException ex)
                {
                    if (ex.InnerException != null && ex.InnerException.InnerException != null)
                    {
                        MessageBox.Show($"Ошибка удаления заказчика: {ex.InnerException.InnerException.Message}");
                    }
                    else
                    {
                        MessageBox.Show($"Ошибка удаления заказчика: {ex.Message}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка удаления заказчика: {ex.Message}");
                }
            }
        }



        private void EditCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (CustomersDataGrid.SelectedItem != null)
            {
                // Получение выбранного заказчика
                var selectedCustomer = (dynamic)CustomersDataGrid.SelectedItem;

                // Создание нового экземпляра Заказчик и передача данных
                Заказчик customer = new Заказчик
                {
                    Заказчик_ID = selectedCustomer.Заказчик_ID,
                    Название_компании = selectedCustomer.Название_компании,
                    Длительность_рекламы = selectedCustomer.Длительность_рекламы,
                    Банковские_реквизиты = selectedCustomer.Банковские_реквизиты,
                    Контактный_телефон = selectedCustomer.Контактный_телефон,
                    Контактное_лицо = selectedCustomer.Контактное_лицо
                };

                // Создание нового окна для редактирования заказчика
                EditCustomerWindow editCustomerWindow = new EditCustomerWindow(customer);
                editCustomerWindow.ShowDialog(); // Отображение окна как модального
                LoadCustomers(); // Перезагрузка данных после закрытия окна редактирования
            }
        }



    }
}
