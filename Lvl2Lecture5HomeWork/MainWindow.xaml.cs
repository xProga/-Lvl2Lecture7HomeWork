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
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Lvl2Lecture5HomeWork
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 

    public class Department
    {
        public string NameDepartment { get; set; }

        public static ObservableCollection<Department> GetDepartments()
        {
            ObservableCollection<Department> liDep = new ObservableCollection<Department>();

            using (Lvl2Lesson7DBEntities db = new Lvl2Lesson7DBEntities())
            {
                var rawDepartments = db.tbl_Departments.ToList();
                foreach (var item in rawDepartments)
                {
                    liDep.Add(new Department { NameDepartment = item.Name_Department });
                }
            }
            return liDep;
        }

        public static void AddDepartment(string nameDepartment)
        {
            using (Lvl2Lesson7DBEntities db = new Lvl2Lesson7DBEntities())
            {
                db.tbl_Departments.Add(new tbl_Departments {
                    Name_Department = nameDepartment
                });
                db.SaveChanges();
            }
        }

        public static void DeleteDepartment(Department dp)
        {
            using (Lvl2Lesson7DBEntities db = new Lvl2Lesson7DBEntities())
            {
                var dep = db.tbl_Departments.Where(x => x.Name_Department.Equals(dp.NameDepartment)).FirstOrDefault();
                db.tbl_Departments.Remove(dep);
            }
        }
    }

    public sealed class Employees : INotifyPropertyChanged
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Department Department { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        private string m_Property;

        public Employees(string firstName, string lastName, Department department)
        {
            FirstName = firstName;
            LastName = lastName;
            Department = department;
        }

        public string Property
        {
            get { return m_Property; }
            set
            {
                m_Property = value;
                OnPropertyChanged("Department.NameDepartment");
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public static ObservableCollection<Employees> GetEmployees()
        {
            ObservableCollection<Employees> liEmp = new ObservableCollection<Employees>();

            using (Lvl2Lesson7DBEntities db = new Lvl2Lesson7DBEntities())
            {
                var rawEmployees = db.tbl_Employees.ToList();

                foreach (var item in rawEmployees)
                {
                    string depart = db.tbl_Departments.Where(x => x.ID_Departments == item.Code_Department).FirstOrDefault().Name_Department;
                    liEmp.Add(new Employees(item.First_Name, item.Last_Name, new Department { NameDepartment = depart }));
                }
            }

            return liEmp;
        }

        public static void EditEmployeeDepartment(Employees emp, string newDepartment)
        {
            using (Lvl2Lesson7DBEntities db = new Lvl2Lesson7DBEntities())
            {
                var editEmp = db.tbl_Employees.Where(x => x.First_Name.Equals(emp.FirstName) && x.Last_Name.Equals(emp.LastName)).FirstOrDefault();
                editEmp.Code_Department = db.tbl_Departments.Where(x => x.Name_Department.Equals(newDepartment)).FirstOrDefault().ID_Departments;
                db.SaveChanges();
            }
        }

        public static void CreateEmployee(string firstName, string lastName, string nameDepartment)
        {
            using (Lvl2Lesson7DBEntities db = new Lvl2Lesson7DBEntities())
            {
                tbl_Employees emp = new tbl_Employees {
                    First_Name = firstName,
                    Last_Name = lastName,
                    Code_Department = db.tbl_Departments.Where(x => x.Name_Department.Equals(nameDepartment)).FirstOrDefault().ID_Departments
                };
                db.tbl_Employees.Add(emp);
                db.SaveChanges();
            }
        }
    }
    
    public partial class MainWindow : Window
    {
        public ObservableCollection<Employees> liEmp = new ObservableCollection<Employees>();
        public ObservableCollection<Department> liDep = new ObservableCollection<Department>();

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        /// <summary>
        /// Создание базовой информации о работниках и службах/департаментах, а так же связывание источников с формой.
        /// </summary>
        private void CreateInfoAboutEmployeesAndDepartments()
        {
            //ObservableCollection<Department> liDep = new ObservableCollection<Department> {
            //    new Department{ NameDepartment = "Служба снабжения"},
            //    new Department{ NameDepartment = "Служба ремонта"},
            //    new Department{ NameDepartment = "Отдел кадров"}
            //};

            //this.liDep = liDep;
            liDep = Department.GetDepartments();

            //ObservableCollection<Employees> liEmp = new ObservableCollection<Employees>
            //{
            //    new Employees("Алексей", "Алексеев", liDep[0]),
            //    new Employees("Ольга", "Олеговна", liDep[1]),
            //    new Employees("Василий", "Васильевич", liDep[2])
            //};
            //this.liEmp = liEmp;
            liEmp = Employees.GetEmployees();

            lbEmployees.ItemsSource = liEmp;
            lbDepartment.ItemsSource = liEmp;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            CreateInfoAboutEmployeesAndDepartments();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddRemoveDep_Click(object sender, RoutedEventArgs e)
        {
            EditDepartment ed = new EditDepartment(liDep, this);
            ed.ShowDialog();
        }

        private void EditEmployees_Click(object sender, RoutedEventArgs e)
        {
            EditEmployee ee = new EditEmployee(liEmp, liDep, this);
            ee.ShowDialog();
        }

        private void lbEmployees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbEmployees.SelectedIndex != lbDepartment.SelectedIndex)
            {
                lbDepartment.SelectedIndex = lbEmployees.SelectedIndex;
            }
        }

        private void lbDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbDepartment.SelectedIndex != lbEmployees.SelectedIndex)
            {
                lbEmployees.SelectedIndex = lbDepartment.SelectedIndex;
            }
        }
    }
}
