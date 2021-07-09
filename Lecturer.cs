using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAssignment
{
    class Lecturer:Person
    {
        private string diploma;
        private string lecturerId;
        private Lecturer next;

        public string Diploma { get => diploma; set => diploma = value; }
        public string LecturerId { get => lecturerId; set => lecturerId = value; }
        internal Lecturer Next { get => next; set => next = value; }

        public Lecturer(string name, string dateOfBirth, string email, string address, string idNumber, string passportId, string deployma, string lecturerId) : base(name, dateOfBirth, email, address, idNumber, passportId)
        {
            Diploma = diploma;
            LecturerId = lecturerId;
            next = null;
        }
        public Lecturer GetClone()
        {
            return (Lecturer) this.MemberwiseClone();
        }

        
    }
}
