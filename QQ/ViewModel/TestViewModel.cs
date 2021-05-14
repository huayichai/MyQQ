using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;

namespace QQ.ViewModel
{
    class TestViewModel : ViewModelBase
    {
        public TestViewModel()
        {
            ClassName = "1618304";
            Students = new ObservableCollection<Student>();
            Students.Add(new Student { ID = "1", Name = "张三", Age = 18, Sex = "男" });
            Students.Add(new Student { ID = "2", Name = "李四", Age = 18, Sex = "男" });
            Students.Add(new Student { ID = "3", Name = "王五", Age = 18, Sex = "男" });

            Hello = new RelayCommand(() =>
            {
                MessageBox.Show("hello WPF");
            });

            EditCommand = new RelayCommand<string>((ID) =>
            {
                MessageBox.Show(ID);
            });

            Open = new RelayCommand(() =>
            {
                MainWindow main = new MainWindow();
                main.Show();
            });
        }

        public string ClassName { get; set; }

        private ObservableCollection<Student> students;

        public ObservableCollection<Student> Students
        {
            get { return students; }
            set { students = value; RaisePropertyChanged(); }
        }

        public RelayCommand Hello { get; set; }

        public RelayCommand<string> EditCommand { get; set; }

        public RelayCommand Open { get; set; }
    }

    public class Student
    {
        public string ID { get; set; }
        public string Name { get; set; }

        public int Age { get; set; }

        public string Sex { get; set; }
    }
}
