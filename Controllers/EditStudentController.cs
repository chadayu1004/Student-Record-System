using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Production.Controllers
{
    public class EditStudentController : Controller
    {
        public ActionResult ShowStudent()
        {
            Session.Clear();
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
                              + " AND CAST(Stu.Stu_Year_ID AS nvarchar) +''+ CAST(Stu.Stu_ID AS nvarchar) = @Stu_ID";

                using (SqlCommand cmd = new SqlCommand(strSQL, con))
                {
                    cmd.Parameters.AddWithValue("@Stu_ID", Session["Stu_ID"]);

                    con.Open();

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                    }
                }
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

            return View();
        }

        [HttpPost]
        public ActionResult SaveEdit(string Stu_ID, string Stu_Firstname, string Stu_Lastname, string Stu_Address)
        {
            string constr = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SP_UpdateProfile", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@Stu_ID", SqlDbType.NVarChar).Value = Stu_ID;
                    cmd.Parameters.Add("@Stu_Firstname", SqlDbType.NVarChar).Value = Stu_Firstname;
                    cmd.Parameters.Add("@Stu_Lastname", SqlDbType.NVarChar).Value = Stu_Lastname;
                    cmd.Parameters.Add("@Stu_Address", SqlDbType.NVarChar).Value = Stu_Address;

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            TempData["SuccessMessage"] = "บันทึกข้อมูลเรียบร้อย";
            return RedirectToAction("Dashboard", "Students");
        }
    }
}
