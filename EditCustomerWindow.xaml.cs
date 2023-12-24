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
    public partial class EditCustomerWindow : Window
    {
        private readonly dynamic _customerData;

        // Создание события для передачи обновленных данных заказчика
        public event EventHandler<CustomerUpdatedEventArgs> CustomerUpdated;

        public EditCustomerWindow(dynamic customerData)
        {
            InitializeComponent();
            _customerData = customerData;

            // Заполнение текстовых полей данными заказчика
            NameTextBox.Text = _customerData.Название_компании;
            DurationTextBox.Text = _customerData.Длительность_рекламы.ToString();
            BankDetailsTextBox.Text = _customerData.Банковские_реквизиты;
            PhoneTextBox.Text = _customerData.Контактный_телефон;
            ContactPersonTextBox.Text = _customerData.Контактное_лицо;
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            // Преобразование текста в числовой формат
            if (int.TryParse(DurationTextBox.Text, out int duration))
            {
                // Сохранение изменений в объекте заказчика
                _customerData.Длительность_рекламы = duration;

                // Создание объекта аргумента с обновленными данными
                var updatedCustomerData = new CustomerUpdatedEventArgs
                {
                    UpdatedCustomer = _customerData // Обновленные данные заказчика
                };

                // Вызов события для передачи обновленных данных
                CustomerUpdated?.Invoke(this, updatedCustomerData);
                Close(); // Закрытие окна после сохранения
            }
            else
            {
                MessageBox.Show("Введите корректное значение для длительности рекламы.");
            }
        }
    }

    // Класс аргументов события для передачи обновленных данных
    public class CustomerUpdatedEventArgs : EventArgs
    {
        public dynamic UpdatedCustomer { get; set; }
    }
}
