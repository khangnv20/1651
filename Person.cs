using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAssignment
{
    class Person 
    {
        private string name;
        private string dateOfbirth;
        private string email;
        private string address;
        private string idNumber;
        private string passportId;

        public string Name { get => name; set { name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToLower()); ; } }
        public string DateOfbirth { get => dateOfbirth; set => dateOfbirth = value; }
        public string Email { get => email; set => email = value; }
        public string Address { get => address; set => address = value; }
        public string IdNumber { get => idNumber; set => idNumber = value; }
        public string PassportId { get => passportId; set => passportId = value; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">Name will be formated by uppercase first character each of word</param>
        /// <param name="dateOfBirth"></param>
        /// <param name="email"></param>
        /// <param name="address"></param>
        /// <param name="idNumber"></param>
        /// <param name="passportId"></param>
        public Person(string name, string dateOfBirth, string email, string address, string idNumber, string passportId)
        {
            Name = name;
            DateOfbirth = dateOfBirth;
            Email = email;
            Address = address;
            IdNumber = idNumber;
            PassportId = passportId;
        }
    }
}
