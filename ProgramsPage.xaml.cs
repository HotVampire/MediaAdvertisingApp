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
using System.Data.Entity.Infrastructure;

namespace MediaAdvertisingApp
{
    public partial class ProgramsPage : Page
    {
        private readonly MediaAdvertisingDBEntities _dbContext = new MediaAdvertisingDBEntities();

        public ProgramsPage()
        {
            InitializeComponent();
            LoadPrograms(); // Загрузка данных при инициализации страницы
        }

        private void ProgramsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool isRowSelected = ProgramsDataGrid.SelectedItem != null;

            // Включаем или отключаем кнопки в зависимости от выбранной строки
            AddProgramButton.IsEnabled = true; // Добавление всегда доступно
            DeleteProgramButton.IsEnabled = isRowSelected;
            EditProgramButton.IsEnabled = isRowSelected;
        }

        private void LoadPrograms()
        {
            try
            {
                var programs = _dbContext.Программа
                    .Select(p => new
                    {
                        Программа_ID = p.Программа_ID,
                        Название_программы = p.Название_программы,
                        Рейтинг = p.Рейтинг,
                        Время_рекламных_блоков = p.Время_рекламных_блоков,
                        Стоимость_минуты = p.Стоимость_минуты
                    })
                    .ToList();

                ProgramsDataGrid.ItemsSource = programs;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}");
            }
        }


        private void AddProgram_Click(object sender, RoutedEventArgs e)
        {
            // Создание нового окна для добавления программы
            AddProgramWindow addProgramWindow = new AddProgramWindow();
            addProgramWindow.ShowDialog(); // Отображение окна как модального
            LoadPrograms(); // Перезагрузка данных после закрытия окна добавления
        }

        private void DeleteProgram_Click(object sender, RoutedEventArgs e)
        {
            if (ProgramsDataGrid.SelectedItem != null)
            {
                try
                {
                    dynamic selectedProgram = ProgramsDataGrid.SelectedItem;

                    int programId = selectedProgram.Программа_ID; // Получение ID выбранной программы

                    var programToDelete = _dbContext.Программа.FirstOrDefault(p => p.Программа_ID == programId);

                    if (programToDelete != null)
                    {
                        _dbContext.Программа.Remove(programToDelete); // Удаление программы из БД
                        _dbContext.SaveChanges(); // Сохранение изменений

                        LoadPrograms(); // Перезагрузка данных после удаления
                    }
                }
                catch (DbUpdateException ex)
                {
                    if (ex.InnerException != null && ex.InnerException.InnerException != null)
                    {
                        MessageBox.Show($"Ошибка удаления программы: {ex.InnerException.InnerException.Message}");
                    }
                    else
                    {
                        MessageBox.Show($"Ошибка удаления программы: {ex.Message}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка удаления программы: {ex.Message}");
                }
            }
        }

        private void EditProgram_Click(object sender, RoutedEventArgs e)
        {
            if (ProgramsDataGrid.SelectedItem != null)
            {
                // Получение выбранной программы
                var selectedProgram = (dynamic)ProgramsDataGrid.SelectedItem;

                // Создание нового экземпляра Программа и передача данных
                Программа program = new Программа
                {
                    Программа_ID = selectedProgram.Программа_ID,
                    Название_программы = selectedProgram.Название_программы,
                    Рейтинг = selectedProgram.Рейтинг,
                    Время_рекламных_блоков =  selectedProgram.Время_рекламных_блоков,
                    Стоимость_минуты = selectedProgram.Стоимость_минуты,
                    // Добавьте остальные поля программы аналогично
                };

                // Создание нового окна для редактирования программы
                EditProgramWindow editProgramWindow = new EditProgramWindow(program);
                editProgramWindow.ShowDialog(); // Отображение окна как модального
                LoadPrograms(); // Перезагрузка данных после закрытия окна редактирования
            }
        }
    }
}

