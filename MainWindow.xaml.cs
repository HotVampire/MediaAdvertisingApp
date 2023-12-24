using MediaAdvertisingApp.AppData;
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

namespace MediaAdvertisingApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            /// подключение БД
            ConnectOdb.conObj = new MediaAdvertisingDBEntities();
        }

        private void GoToHomePage(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = null; // Очистим содержимое MainFrame, чтобы вернуться на главную страницу
        }

        private void ShowPrograms_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new ProgramsPage(); // Создание экземпляра страницы и установка в Frame
        }

        private void ShowCustomers_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new CustomersPage(); // Создание экземпляра страницы и установка в Frame
        }

        private void ShowEmployees_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new EmployeesPage(); // Создание экземпляра страницы и установка в Frame
        }

        private void ShowAssignPage_Click(object sender, RoutedEventArgs e)
        {
            // Открываете страницу "Назначение программы сотруднику и заказчику"
            MainFrame.Content = (new AssignEmployeeToCustomerAndProgramPage());
        }

        private void ShowAdvertisementInfo_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = (new CustomerAdvertisementInfoPage());
        }

    }
}
