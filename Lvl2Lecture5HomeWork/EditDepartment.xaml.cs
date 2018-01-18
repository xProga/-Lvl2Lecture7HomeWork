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
using System.Collections.ObjectModel;

namespace Lvl2Lecture5HomeWork
{
    /// <summary>
    /// Логика взаимодействия для EditDepartment.xaml
    /// </summary>
    public partial class EditDepartment : Window
    {
        private ObservableCollection<Department> liDep = new ObservableCollection<Department>();
        private MainWindow mw;

        /// <summary>
        /// Добавление информации о новой службе/департаменте
        /// </summary>
        private void AddNewDepartment(string newDepartment)
        {
            if (liDep.Where(x => x.NameDepartment.Contains(newDepartment)).Count() == 0)
            {
                liDep.Add(new Department { NameDepartment = NewDepartmentTextBox.Text });
            }
            else
            {
                MessageBox.Show("Такой департамент/служба уже существует", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Удаление выделенной службы/департамента
        /// </summary>
        private void DeleteSelectedDepartment(string selectedDepartment)
        {
            if (liDepart.SelectedItems.Count > 0)
            {
                Department dep = liDep.Where(x => x.NameDepartment.Equals(selectedDepartment)).FirstOrDefault();
                liDep.Remove(dep);
            }
            else
            {
                MessageBox.Show("Не выделено ни одного элемента!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Передача измененных данных обратно в основную форму
        /// </summary>
        private void SendUpdatedDepartmentListToMain()
        {
            mw.liDep = liDep;
            mw.lbDepartment.Items.Refresh();
        }

        /// <summary>
        /// Инициализация формы и заполниение ее данными о службах/департаментах
        /// </summary>
        /// <param name="liDep">Список департаментов</param>
        /// <param name="mw">Ссылка на основную форму</param>
        public EditDepartment(ObservableCollection<Department> liDep, MainWindow mw)
        {
            this.liDep = liDep;
            this.mw = mw;
            InitializeComponent();
            liDepart.ItemsSource = liDep;
        }

        private void SaveNewDepartment_Click(object sender, RoutedEventArgs e)
        {
            AddNewDepartment(NewDepartmentTextBox.Text);
            Department.AddDepartment(NewDepartmentTextBox.Text);
            NewDepartmentTextBox.Text = string.Empty;
        }

        private void DeleteSelectedDepart_Click(object sender, RoutedEventArgs e)
        {
            DeleteSelectedDepartment(((Department)liDepart.SelectedValue).NameDepartment);
            Department.DeleteDepartment((Department)liDepart.SelectedValue);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mw.liDep = liDep;
        }
    }
}
