using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopUI.Models
{
    public class UserModel
    {
        /// <summary>
        /// The user's ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The user's firstname
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// The user's lastname
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// The user's phonenumber
        /// </summary>
        public string PhoneNmb { get; set; }
        /// <summary>
        /// The user's emailaddress
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// The user's password
        /// </summary>
        public string Psw { get; set; }
        

        /// <summary>
        /// Constructor used when creating a new user
        /// </summary>
        /// <param name="firstname">Firstname property</param>
        /// <param name="lastname">Lastname property</param>
        /// <param name="phonenmb">Phonenumber property</param>
        /// <param name="email">Emailaddress property</param>
        /// <param name="psw">Password property</param>
        public UserModel(string firstname, string lastname, string phonenmb, string email, string psw)
        {
            FirstName = firstname;
            LastName = lastname;
            PhoneNmb = phonenmb;
            Email = email;
            Psw = psw;
        }

        /// <summary>
        /// Constructor to get the user's info upon login
        /// </summary>
        /// <param name="firstname">Firstname property</param>
        /// <param name="lastname">Lastname property</param>
        /// <param name="id">Id property</param>
        public UserModel(int id, string firstname, string lastname)
        {
            Id = id;
            FirstName = firstname;
            LastName = lastname;
        }
    }
}
