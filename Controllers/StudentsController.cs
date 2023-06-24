using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Production.Models;


namespace Production.Controllers
{
    public class StudentsController : Controller
    {


        public ActionResult StudentAdd()
        {
            return View();
        }
        public ActionResult Dashboard()
        {
            // Student model = new Student();
            return View();
        }

        



        [HttpGet]
        public ActionResult Dashboard(string stu_facultyid_search, string stu_firstname_search, string stu_lastname_search, string stu_id_search)
        {
            if (Session["LoginID"] == null)
            {
                return RedirectToAction("Loginform", "UserLogins");
            }

            Student model = new Student();
            List<Student> studentslist = new List<Student>();
            string constr = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString; // connect database
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(constr))
            {
                string strSQL = "";
                if (Session["IsLoginStatus"] == "0")
                {
                    Session["IsLoginStatus"] = "1";
                    strSQL = "SELECT  CAST(Stu.Stu_Year_ID AS nvarchar) +''+ CAST(Stu.Stu_ID AS nvarchar) AS StudentID,"
                          + " Stu.Stu_Firstname,Stu.Stu_Lastname,StuGen.Stu_Gender_Desc,Stu.Stu_Address,Stu.Stu_PhoneNo,"
                          + " Stu_Year_ID,Faculty_Name,Stu_Edit_By"
                          + " FROM Student AS Stu"
                          + " Left join Faculty AS Fac"
                          + " ON Stu.Stu_Faculty_ID = Fac.Faculty_ID"
                          + " Left join StudentGender AS StuGen"
                          + " ON Stu.Stu_Gender_ID = StuGen.Stu_Gender_ID"
                          + " WHERE CAST(Stu.Stu_Year_ID AS nvarchar) = '99999' ";
                }
                else
                {
                    strSQL = "SELECT  CAST(Stu.Stu_Year_ID AS nvarchar) +''+ CAST(Stu.Stu_ID AS nvarchar) AS StudentID,"
                              + " Stu.Stu_Firstname,Stu.Stu_Lastname,StuGen.Stu_Gender_Desc,Stu.Stu_Address,Stu.Stu_PhoneNo,"
                              + " Stu_Year_ID,Faculty_Name,Stu_Edit_By"
                              + " FROM Student AS Stu"
                              + " Left join Faculty AS Fac"
                              + " ON Stu.Stu_Faculty_ID = Fac.Faculty_ID"
                              + " Left join StudentGender AS StuGen"
                              + " ON Stu.Stu_Gender_ID = StuGen.Stu_Gender_ID"
                              + " WHERE 1=1 ";
                    if ((stu_firstname_search != null) && (stu_firstname_search != ""))
                    {
                        strSQL += " and Stu.Stu_Firstname = '" + stu_firstname_search + "'";
                    }
                    if ((stu_lastname_search != null) && (stu_lastname_search != ""))
                    {
                        strSQL += " and Stu.Stu_Lastname = '" + stu_lastname_search + "'";
                    }
                    if ((stu_id_search != null) && (stu_id_search != ""))
                    {
                        strSQL += " and CAST(Stu.Stu_Year_ID AS nvarchar) +''+ CAST(Stu.Stu_ID AS nvarchar) = '" + stu_id_search + "'";
                    }
                    if ((stu_facultyid_search != "0") && (stu_facultyid_search != null))
                    {
                        strSQL += " and Stu.Stu_Faculty_ID = '" + stu_facultyid_search + "'";
                    }
                }
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
                foreach (DataRow dr in dt.Rows)
                {
                    Student std = new Student();
                    std.Stu_ID = dr["StudentID"].ToString();
                    std.Stu_Firstname = dr["Stu_Firstname"].ToString();
                    std.Stu_Lastname = dr["Stu_Lastname"].ToString();
                    std.Faculty_Name = dr["Faculty_Name"].ToString();
                    studentslist.Add(std);
                }

            }
            else
            {
            }
            model.students = studentslist;
            return View(model);

        }
        public ActionResult ClearScreen()
        {
            Session.Clear();
            return View("~/Views/Students/Dashboard.cshtml");
        }
        [HttpGet]
        public ActionResult EditStudent(string Stu_ID)
        {
            DataTable dt = new DataTable();
            Student std = new Student();
            dt = getStudent(Stu_ID);
            foreach (DataRow dr in dt.Rows)
            {
                //std.Stu_ID = dr["Stu_ID"].ToString();
                //std.Stu_Firstname = dr["Stu_Firstname"].ToString();
                //std.Stu_Lastname = dr["Stu_Lastname"].ToString();
                //std.Faculty_Name = dr["Faculty_Name"].ToString();  

                Session["Stu_ID"] = dr["Stu_ID"].ToString();
                Session["Stu_Firstname"] = dr["Stu_Firstname"].ToString();
                Session["Stu_Lastname"] = dr["Stu_Lastname"].ToString();
                Session["Faculty_Name"] = dr["Faculty_Name"].ToString();
            }
            return View("~/Views/EditStudent/EditStudent.cshtml");
            //return View(std);
        }
        [HttpPost]
        public ActionResult SaveStudent(string Stu_Firstname, string Stu_Lastname, string Stu_Address, string Stu_PhoneNo,string Stu_Year_ID,string Faculty_Name,string Stu_Gender_Desc)
        {
            // connect to the database
            string constr = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(constr))
            {
                string strSQL = "UPDATE Student"
                              + " SET Stu_Firstname = '" + Stu_Firstname + "' ,Stu_Lastname  = '" + Stu_Lastname + "',"
                              + " Stu_Address = '" + Stu_Address + "',Stu_PhoneNo = '" + Stu_PhoneNo + "' "
                              + " WHERE Stu_ID =  '" + Session["Stu_ID"].ToString().Substring(2,3) + "' ";
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
            TempData["SuccessMessage"] = "บันทึกข้อมูลเรียบร้อย";
            return RedirectToAction("EditStudent", "Students");
        }
        //public ActionResult EditStudent(string Stu_ID, string Stu_Firstname, string Stu_Lastname, string Stu_Address)
        //{
        //    // connect database
        //    string constr = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
        //    DataTable dt = new DataTable();
        //    using (SqlConnection con = new SqlConnection(constr))
        //    {
        //        string strSQL = ""
        //                      + "";
        //        using (SqlCommand cmd = new SqlCommand(strSQL))
        //        {
        //            cmd.Connection = con;
        //            con.Open();

        //            cmd.Connection = con;
        //            cmd.CommandTimeout = 0;
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.CommandText = "SP_UpdateProfile";

        //            cmd.Parameters.Add("@Stu_Firstname", SqlDbType.NVarChar).Value = Stu_Firstname;
        //            cmd.Parameters.Add("@Stu_Lastname", SqlDbType.NVarChar).Value = Stu_Lastname;
        //            //cmd.Parameters.Add("@Stu_Firstname", SqlDbType.NVarChar).Value = Stu_Firstname;
        //            //cmd.Parameters.Add("@Stu_Firstname", SqlDbType.NVarChar).Value = Stu_Firstname;
        //            //cmd.Parameters.Add("@Stu_Firstname", SqlDbType.NVarChar).Value = Stu_Firstname;
        //            //cmd.Parameters.Add("@Stu_Firstname", SqlDbType.NVarChar).Value = Stu_Firstname;
        //            //cmd.Parameters.Add("@Stu_Firstname", SqlDbType.NVarChar).Value = Stu_Firstname;
        //            //cmd.Parameters.Add("@Stu_Firstname", SqlDbType.NVarChar).Value = Stu_Firstname;
        //            cmd.ExecuteNonQuery();
        //        }
        //        con.Close();
        //    }
        //    if (dt.Rows.Count > 0)
        //    {
        //        //RedirectToAction("");
        //        return View("~/Views/Students/Dashboard.cshtml");
        //    }
        //    else
        //    {
        //        return View();
        //        //return View("~/Views/Home/Loginform.cshtml");
        //    }
        //}
        public DataTable getStudent(string Stu_ID)
        {
            string constr = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString; // connect database
        DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(constr))
            {
                string strSQL = "SELECT CAST(Stu.Stu_Year_ID AS nvarchar) +''+ CAST(Stu.Stu_ID AS nvarchar) AS Stu_ID,"
                              + " Stu.Stu_Firstname,Stu.Stu_Lastname,StuGen.Stu_Gender_Desc,Stu.Stu_Address,Stu.Stu_PhoneNo,"
                              + " Stu_Year_ID,Faculty_Name,Stu_Edit_By"
                              + " FROM Student AS Stu,StudentGender AS StuGen,Faculty AS Fac"
                              + " WHERE Stu.Stu_Gender_ID = StuGen.Stu_Gender_ID"
                              + " AND Stu.Stu_Faculty_ID = Fac.Faculty_ID"
                              + " AND CAST(Stu.Stu_Year_ID AS nvarchar) +''+ CAST(Stu.Stu_ID AS nvarchar) = '" + Stu_ID + "'" ;
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
             for (int i = 0; i < dt.Rows.Count; i++)
    {
        Session["Stu_ID"] = dt.Rows[i]["Stu_ID"].ToString();
        Session["Stu_Firstname"] = dt.Rows[i]["Stu_Firstname"].ToString();
        Session["Stu_Lastname"] = dt.Rows[i]["Stu_Lastname"].ToString();
        Session["Stu_Gender_Desc"] = dt.Rows[i]["Stu_Gender_Desc"].ToString();
        Session["Stu_Address"] = dt.Rows[i]["Stu_Address"].ToString();
        Session["Stu_PhoneNo"] = dt.Rows[i]["Stu_PhoneNo"].ToString();
        Session["Stu_Year_ID"] = dt.Rows[i]["Stu_Year_ID"].ToString();
        Session["Faculty_Name"] = dt.Rows[i]["Faculty_Name"].ToString();
        Session["Stu_Edit_By"] = dt.Rows[i]["Stu_Edit_By"].ToString();
    }
}
else
{
}
{
    return dt;
}
        }
        
    }
}