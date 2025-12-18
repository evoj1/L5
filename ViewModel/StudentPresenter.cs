using GalaSoft.MvvmLight.Command;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModel
{
    public class StudentPresenter : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly StudentBL _studentBL = new StudentBL();

        private Student _selectedStudent;

        private string _newName;
        private string _newSpecialty;
        private string _newGroup;

        public ObservableCollection<Student> Students { get; }

        public Student SelectedStudent
        {
            get => _selectedStudent;
            set
            {
                if (_selectedStudent == value) return;
                _selectedStudent = value;
                OnPropertyChanged();

                if (_selectedStudent != null)
                {
                    NewName = _selectedStudent.Name;
                    NewSpecialty = _selectedStudent.Specialty;
                    NewGroup = _selectedStudent.Group;
                }
            }
        }
        public string NewName
        {
            get => _newName;
            set
            {
                if (_newName == value) return;
                _newName = value;
                OnPropertyChanged();
            }
        }
        public string NewSpecialty
        {
            get => _newSpecialty;
            set
            {
                if (_newSpecialty == value) return;
                _newSpecialty = value;
                OnPropertyChanged();
            }
        }
        public string NewGroup
        {
            get => _newGroup;
            set
            {
                if (_newGroup == value) return;
                _newGroup = value;
                OnPropertyChanged();
            }
        }
        public ICommand AddStudentCommand { get; }
        public ICommand RemoveStudentCommand { get; }
        public ICommand SaveStudentCommand { get; }

        public StudentPresenter()
        {
            Students = new ObservableCollection<Student>();

            AddStudentCommand = new RelayCommand(_ => AddStudent(), _ => CanAddOrEdit());
            RemoveStudentCommand = new RelayCommand(_ => RemoveStudent(), _ => SelectedStudent != null);
            SaveStudentCommand = new RelayCommand(_ => SaveStudent(), _ => SelectedStudent != null);

            foreach (var s in _studentBL.GetStudents())
                Students.Add(s);
        }
        private bool CanAddOrEdit()
        {
            return !string.IsNullOrWhiteSpace(NewName)
                   && !string.IsNullOrWhiteSpace(NewSpecialty)
                   && !string.IsNullOrWhiteSpace(NewGroup);
        }
        private void AddStudent()
        {
            var student = new Student
            {
                Name = NewName,
                Specialty = NewSpecialty,
                Group = NewGroup
            };

            _studentBL.AddStudent(student);
            Students.Add(student);

            NewName = string.Empty;
            NewSpecialty = string.Empty;
            NewGroup = string.Empty;
        }
        private void RemoveStudent()
        {
            if (SelectedStudent == null) return;

            _studentBL.RemoveStudent(SelectedStudent);
            Students.Remove(SelectedStudent);
            SelectedStudent = null;

            NewName = string.Empty;
            NewSpecialty = string.Empty;
            NewGroup = string.Empty;
        }
        private void SaveStudent()
        {
            if (SelectedStudent == null) return;

            var newStudent = new Student
            {
                Name = NewName,
                Specialty = NewSpecialty,
                Group = NewGroup
            };
            _studentBL.UpdateStudent(SelectedStudent, newStudent);
            // очистка полей после сохранения
            NewName = string.Empty;
            NewSpecialty = string.Empty;
            NewGroup = string.Empty;

            // опционально: снять выделение
            SelectedStudent = null;
        }
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
