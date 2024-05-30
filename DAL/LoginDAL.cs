using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class LoginDAL
    {
        private static LoginDAL instance;

        public static LoginDAL Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LoginDAL();
                }
                return LoginDAL.instance;
            }
            private set { instance = value; }
        }

        private LoginDAL() { }

        public object Login(Login lg)
        {
            object fname = Database.Instance.ExecuteScalar("select FirstName + ' ' + LastName AS FullName from Employee where usr = @username and pwd = @password", new object[] { lg.username, lg.password });
            return fname;
        }
        public object GetEmployeeId(Login lg)
        {
            object emid = Database.Instance.ExecuteScalar("select employeeid from Employee where usr = @username and pwd = @password", new object[] { lg.username, lg.password });
            return emid;
        }
    }
}
