using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace Production.Controllers
{
    public class UserLoginsController : Controller
    {
        // GET: UserLogins
        public ActionResult Loginform()
        {
            return View();
        }

        public ActionResult AddUser()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult logout()
        {
            Session.Clear();
            return View("~/Views/UserLogins/loginform.cshtml");
        }





        [HttpPost]
        public ActionResult Loginform(string username, string password)
        {
            // connect to the database
            string constr = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(constr))
            {
                string strSQL = "SELECT Username , Password , Firstname , Lastname , Position , UserlevelID FROM UserLogin WHERE Username = '" + username + "' AND Password = CONVERT(NVARCHAR(32), HASHBYTES('SHA1', '" + password + "'), 1)";
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                    }
                }
                con.Close();
            }

            if (dt.Rows.Count > 0)
            {
                // Redirect to a different action
                foreach (DataRow r in dt.Rows)
                {
                    Session["LoginID"] = r["Username"].ToString();
                    Session["Firstname"] = r["Firstname"].ToString();
                    Session["Lastname"] = r["Lastname"].ToString();
                    Session["Position"] = r["Position"].ToString();

                    if (r["UserlevelID"].ToString().Trim() == "1")
                    {
                        Session["UserlevelID"] = "1";
                    }
                    else
                    {
                        Session["UserlevelID"] = "2";
                    }
                }
                if (Session["LoginID"] != null)
                {
                    Session["IsLoginStatus"] = "0";
                }
                return RedirectToAction("Dashboard", "Students");
            }
            else
            {
                // Add error message to ModelState
                ModelState.AddModelError("", "ชื่อผู้ใช้งานหรือรหัสผ่านไม่ถูกต้อง");
                return View("~/Views/UserLogins/Loginfailure.cshtml");
            }
        }

        public ActionResult ClearScreen()
        {
            Session.Clear();
            return View("~/Views/UserLogins/Adduser.cshtml");
        }
        [HttpPost]
        public ActionResult SaveUser(string Username, string Password, string Firstname, string Lastname, string Position, string userlevelID)
        {
            // connect to the database
            string constr = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(constr))
            {
                string strSQL = "INSERT INTO UserLogin(Username,Password,Firstname,Lastname,Position,UserlevelID)"
                                + " VALUES ('" + Username + "',CONVERT(NVARCHAR(32), HASHBYTES('SHA1', '" + Password + "'), 1),'" + Firstname + "','" + Lastname + "','" + Position + "','" + userlevelID + "')";
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
            TempData["SuccessMessage"] = "บันทึกข้อมูลเรียบร้อย";
            return View("~/Views/UserLogins/Adduser.cshtml");
            
        }  

    }
}
