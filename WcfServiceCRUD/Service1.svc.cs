using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace WcfServiceCRUD
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        SqlConnection con = new SqlConnection("Server=DESKTOP-KHPHNGJ\\SQLEXPRESS; Database=register; Integrated Security = True; Encrypt=False");

        public string Insert(InsertUser user)
        {
            string msg;
            con.Open();
            SqlCommand cmd = new SqlCommand("Insert into UserTab (Name,Email) values (@Name,@Email)", con);
            cmd.Parameters.AddWithValue("@Name", user.Name);
            cmd.Parameters.AddWithValue("@Email", user.Email);

            int g = cmd.ExecuteNonQuery();
            if (g == 1)
            {
                msg = "Successfully Inserted";
            }
            else
            {
                msg = "Failed to insert";
            }
            return msg;
        }



        public string Update(UpdateUser u)
        {
            string Message;
            con.Open();
            SqlCommand cmd = new SqlCommand("Update UserTab set Name=@Name, Email=@Email where UserId=@UserId ", con);
            cmd.Parameters.AddWithValue("@UserId", u.UID);
            cmd.Parameters.AddWithValue("@Name", u.Name);
            cmd.Parameters.AddWithValue("@Email", u.Email);
            int res = cmd.ExecuteNonQuery();
            if (res == 1)
            {
                Message = "Successfully Updated";

            }
            else
            {
                Message = "Failed to Update ";
            }
            return Message;
        }

        public string Delete(DeleteUser d)
        {
            string msg;
            con.Open();
            SqlCommand cmd = new SqlCommand("delete UserTab where UserId=@UserId", con);
            cmd.Parameters.AddWithValue("@UserId", d.UID);
            int res = cmd.ExecuteNonQuery();
            if (res == 1)
            {
                msg = "Successfully deleted";
            }
            else
            {
                msg = "Failed to delete";
            }
            return msg;
        }
        public List<User> GetUsers(User us)
        {
            List<User> users = new List<User>();
            try
            {
                SqlCommand cmd = new SqlCommand("select * from UserTab", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    User user = new User();
                    {
                        user.Id = Convert.ToInt32(reader[0]);
                        user.Name = reader[1].ToString();
                        user.Email = reader[2].ToString();
                    }
                    users.Add(user);
                }
                return users;
            }

            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }


    }
}
