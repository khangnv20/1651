using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAssignment
{
    class Subject
    {
        private string subjectTitle;
        private string subjectId;
        private int slots;
        private int numberOfAssignment;
        private Subject next;

        public string SubjectTitle { get => subjectTitle; set => subjectTitle = value; }
        public string SubjectId { get => subjectId; set => subjectId = value; }
        public int Slots { get => slots; set => slots = value; }
        public int NumberOfAssignment { get => numberOfAssignment; set => numberOfAssignment = value; }
        internal Subject Next { get => next; set => next = value; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Id of subject</param>
        /// <param name="title">Title of subject</param>
        /// <param name="slots">Number of slots that subject has</param>
        /// <param name="assingments">Number of assignments</param>
        public Subject(string id, string title, int slots, int assingments)
        {
            subjectId = id;
            subjectTitle = title;
            this.slots = slots;
            numberOfAssignment =  assingments;
            Next = null;
        }
        public Subject GetClone()
        {
            return (Subject)this.MemberwiseClone();
        }
    }
}
