using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MediaAdvertisingApp.AppData;

namespace MediaAdvertisingApp
{
    public partial class CustomerAdvertisementInfoPage : Page
    {
        private readonly MediaAdvertisingDBEntities _dbContext = new MediaAdvertisingDBEntities();

        public CustomerAdvertisementInfoPage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var customers = _dbContext.Заказчик.ToList();
                CustomersComboBox.ItemsSource = customers;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}");
            }
        }

        private void CustomersComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CustomersComboBox.SelectedItem != null)
            {
                var selectedCustomer = (Заказчик)CustomersComboBox.SelectedItem;
                var customerProgram = _dbContext.Программа.FirstOrDefault(p => p.Программа_ID == selectedCustomer.Программа_ID);

                if (customerProgram != null)
                {
                    double totalMinutes = customerProgram.Время_рекламных_блоков?.TotalMinutes ?? 0;
                    double costPerMinute = Convert.ToDouble(customerProgram.Стоимость_минуты ?? 0);
                    double advertisementCost = totalMinutes * costPerMinute;

                    AdvertisementCostTextBlock.Text = $"Так мы будем писать, что для {selectedCustomer.Название_компании} реклама будет стоить {advertisementCost} рублей";
                }
                else
                {
                    AdvertisementCostTextBlock.Text = "Для данного заказчика нет программы";
                }

                CalculateTotalEarnings();
            }
        }

        private void CalculateTotalEarnings()
        {
            double totalEarnings = _dbContext.Заказчик
                .Where(c => c.Программа_ID != null)
                .AsEnumerable()
                .Sum(customer =>
                {
                    var program = _dbContext.Программа.FirstOrDefault(p => p.Программа_ID == customer.Программа_ID);
                    double totalMinutes = program?.Время_рекламных_блоков?.TotalMinutes ?? 0;
                    double costPerMinute = Convert.ToDouble(program?.Стоимость_минуты ?? 0);

                    return totalMinutes * costPerMinute;
                });

            TotalEarningsTextBlock.Text = $"Заработок со всей рекламы: {totalEarnings} рублей";
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            if (CustomersComboBox.SelectedItem != null)
            {
                CustomersComboBox_SelectionChanged(sender, null);
            }
        }
    }
}
