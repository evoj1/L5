using System;
using Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class StudentBL
    {
        private readonly List<Student> _students = new List<Student>();
        public IEnumerable<Student> GetStudents() { return _students; }
        public void AddStudent(Student student)
        {
            if (student == null) return;
            _students.Add(student);
        }
        public void RemoveStudent(Student student)
        {
            if (student == null) return;
            _students.Remove(student);
        }
        public void UpdateStudent(Student oldStudent,  Student newStudent)
        {
            if (oldStudent == null || newStudent == null) return;

            oldStudent.Name = newStudent.Name;
            oldStudent.Specialty = newStudent.Specialty;
            oldStudent.Group = newStudent.Group;
        }
    }
}
