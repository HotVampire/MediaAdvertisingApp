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
    public partial class EditProgramWindow : Window
    {
        private readonly Программа _programData;

        public EditProgramWindow(Программа programData)
        {
            InitializeComponent();
            _programData = programData;

            // Заполнение данными окна редактирования
            NameTextBox.Text = _programData.Название_программы;
            RatingTextBox.Text = _programData.Рейтинг.ToString();
            AdBlocksTimeTextBox.Text = _programData.Время_рекламных_блоков.ToString();
            CostPerMinuteTextBox.Text = _programData.Стоимость_минуты.ToString();
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            // Проверка на успешное преобразование текста в числовые значения
            if (int.TryParse(RatingTextBox.Text, out int rating) && TimeSpan.TryParse(AdBlocksTimeTextBox.Text, out TimeSpan adBlocksTime) && decimal.TryParse(CostPerMinuteTextBox.Text, out decimal costPerMinute))
            {
                // Сохранение изменений в объекте программы
                _programData.Название_программы = NameTextBox.Text;
                _programData.Рейтинг = rating;
                _programData.Время_рекламных_блоков = adBlocksTime;
                _programData.Стоимость_минуты = costPerMinute;

                using (var context = new MediaAdvertisingDBEntities())
                {
                    // Сохранение изменений в базе данных
                    context.SaveChanges();
                }

                MessageBox.Show("Изменения сохранены!");
                Close(); // Закрытие окна после сохранения
            }
            else
            {
                MessageBox.Show("Проверьте корректность введенных данных.");
            }
        }
    }
}
