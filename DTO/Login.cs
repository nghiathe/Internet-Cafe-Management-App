using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Login
    {
        public string username { get; set; }
        public string password { get; set; }
    }
    public static class Employee
    {
        public static byte emId { get; set; }
        public static string fullName { get; set; }
    }
}
