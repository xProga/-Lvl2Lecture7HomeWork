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
    /// Логика взаимодействия для EditEmployee.xaml
    /// </summary>
    /// 

    public partial class EditEmployee : Window
    {
        private ObservableCollection<Employees> liEmp = new ObservableCollection<Employees>();
        private ObservableCollection<Department> liDep = new ObservableCollection<Department>();
        private MainWindow mw;

        /// <summary>
        /// Добавление или обновление информации о работнике
        /// </summary>
        /// <param name="firstName">Фамилия работника</param>
        /// <param name="lastName">Имя работника</param>
        private void AddOrUpdateDataAboutEmployees(string firstName, string lastName)
        {
            if (firstName != string.Empty && lastName != string.Empty)
            {
                if (liEmp.Where(x => x.FirstName.Equals(firstName) && x.LastName.Equals(lastName)).Count() > 0)
                {
                    var selectedEmployee = liEmp.Where(x => x.FirstName.Equals(firstName) && x.LastName.Equals(lastName)).FirstOrDefault();
                    selectedEmployee.Department.NameDepartment = (string)DepartmentComboBox.SelectedItem;
                    selectedEmployee.Property = "Change department";
                    Employees.EditEmployeeDepartment(selectedEmployee, (string)DepartmentComboBox.SelectedItem);
                }
                else
                {
                    liEmp.Add(new Employees(firstName, lastName, new Department { NameDepartment = DepartmentComboBox.SelectedItem.ToString() }));
                    Employees.CreateEmployee(firstName, lastName, DepartmentComboBox.SelectedItem.ToString());
                }
                MessageBox.Show("Изменения сохранены!", "Успешно!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Одно или несколько полей не заполнены!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Внесение информации о работнике в Текстбоксы
        /// </summary>
        /// <param name="employee">Информация о работнике</param>
        private void InputInfoAboutEmployeeToTextBoxes(Employees employee)
        {
            FirstNameTextBox.Text = employee.FirstName;
            LastNameTextBox.Text = employee.LastName;
            DepartmentComboBox.SelectedItem = liDep.Where(x => x.NameDepartment.Equals(employee.Department.NameDepartment))
                .Select(x => x.NameDepartment).FirstOrDefault();
        }

        /// <summary>
        /// Инициализация формы и заполнение списков работников а так же служб/департаментов
        /// </summary>
        /// <param name="liEmp">Список работников</param>
        /// <param name="liDep">Список департаментов</param>
        /// <param name="mw">Ссылка на основную форму</param>
        public EditEmployee(ObservableCollection<Employees> liEmp, ObservableCollection<Department> liDep, MainWindow mw)
        {
            this.liEmp = liEmp;
            this.liDep = liDep;
            this.mw = mw;
            InitializeComponent();
            foreach (var item in liDep)
            {
                DepartmentComboBox.Items.Add(item.NameDepartment);
            }
            listEmployees.ItemsSource = liEmp;
        }

        /// <summary>
        /// Передача обновленных данных о работниках обратно в основную форму
        /// </summary>
        private void SendUpdatedInfoAboutEmployeesToMain()
        {
            mw.liEmp = liEmp;
            for(int i = 0; i < liEmp.Count; i++)
            {
                if (liEmp[i].Property != string.Empty)
                {
                    mw.lbEmployees.Items.Refresh();
                    mw.lbDepartment.Items.Refresh();
                    break;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddOrUpdateDataAboutEmployees(FirstNameTextBox.Text, LastNameTextBox.Text);
        }

        private void listEmployees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Employees employee = (Employees)listEmployees.SelectedItem;
            InputInfoAboutEmployeeToTextBoxes(employee);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SendUpdatedInfoAboutEmployeesToMain();
        }
    }
}
