using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAssignment
{
    class Assignment
    {
        private string assignmentID;
        private string grade;
        private string subjectTitle;
        private string studentName;
        private string feedback;
        private string assignmentTitle;
        private Assignment next;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assignmentID"></param>
        /// <param name="grade"></param>
        /// <param name="subjectTitle"></param>
        /// <param name="studentName"></param>
        /// <param name="feedback"></param>
        /// <param name="assignmentTitle"></param>
        public Assignment(string assignmentID, string grade, string subjectTitle, string studentName, string feedback, string assignmentTitle)
        {
            Next = null;
            AssignmentID = assignmentID;
            Grade = grade;
            SubjectTitle = subjectTitle;
            StudentName = studentName;
            Feedback = feedback;
            AssignmentTitle = assignmentTitle;
        }

        public string AssignmentID { get => assignmentID; set => assignmentID = value; }
        public string Grade { get => grade; set => grade = value; }
        public string SubjectTitle { get => subjectTitle; set => subjectTitle = value; }
        public string StudentName { get => studentName; set => studentName = value; }
        public string Feedback { get => feedback; set => feedback = value; }
        public string AssignmentTitle { get => assignmentTitle; set => assignmentTitle = value; }
        internal Assignment Next { get => next; set => next = value; }
        public Assignment GetClone()
        {
            return (Assignment)this.MemberwiseClone();
        }
    }
}
