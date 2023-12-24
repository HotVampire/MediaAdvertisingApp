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
    public partial class AddCustomerWindow : Window
    {
        public AddCustomerWindow()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // Проверка на успешное преобразование текста в числовое значение
            if (!int.TryParse(DurationTextBox.Text, out int duration))
            {
                MessageBox.Show("Введите корректную длительность рекламы (целое число).");
                return;
            }

            // Создание нового заказчика и добавление его в базу данных
            Заказчик newCustomer = new Заказчик
            {
                Название_компании = NameTextBox.Text,
                Длительность_рекламы = duration,
                Банковские_реквизиты = BankDetailsTextBox.Text,
                Контактный_телефон = PhoneTextBox.Text,
                Контактное_лицо = ContactPersonTextBox.Text
            };

            using (var context = new MediaAdvertisingDBEntities())
            {
                context.Заказчик.Add(newCustomer);
                context.SaveChanges();
            }

            MessageBox.Show("Новый заказчик добавлен!");
            Close(); // Закрытие окна после добавления
        }

    }
}

