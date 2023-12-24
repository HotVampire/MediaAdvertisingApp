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
    public partial class AddProgramWindow : Window
    {
        public AddProgramWindow()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (decimal.TryParse(AdBlocksTimeTextBox.Text, out decimal durationMinutes) &&
                int.TryParse(RatingTextBox.Text, out int rating) &&
                decimal.TryParse(CostPerMinuteTextBox.Text, out decimal costPerMinute))
            {
                Программа newProgram = new Программа
                {
                    Название_программы = NameTextBox.Text,
                    Рейтинг = rating,
                    Время_рекламных_блоков = TimeSpan.FromMinutes((double)durationMinutes),
                    Стоимость_минуты = costPerMinute
                };

                using (var context = new MediaAdvertisingDBEntities())
                {
                    context.Программа.Add(newProgram);
                    context.SaveChanges();
                }

                MessageBox.Show("Новая программа добавлена!");
                Close(); // Закрытие окна после добавления
            }
            else
            {
                MessageBox.Show("Проверьте правильность введенных данных.");
            }
        }
    }
}
