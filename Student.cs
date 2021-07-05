using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAssignment
{
    class Student:Person
    {
        private string academicYear;
        private string major;
        private string studentId;
        private Student next;

        public string AcademicYear { get => academicYear; set => academicYear = value; }
        public string Major { get => major; set => major = value; }
        public string StudentId { get => studentId; set => studentId = value; }
        internal Student Next { get => next; set => next = value; }

        public Student(string name, string dateOfBirth, string email, string address, string idNumber, string passportId, string major, string acedamicYear, string studentId) : base(name, dateOfBirth, email, address, idNumber, passportId)
        {
            Major = major;
            StudentId = studentId;
            AcademicYear = acedamicYear;
            Next = null;
        }
        public Student GetClone()
        {
            return (Student)this.MemberwiseClone();
        }

    }
}
