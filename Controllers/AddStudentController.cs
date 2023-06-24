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
    public class AddStudentController : Controller
    {
        [HttpPost]
        public ActionResult SaveStudent(string stu_firstname, string stu_lastname, string stu_genderid, string stu_address, string stu_phoneon, string stu_yearid, string stu_facultyid)
        {
            // connect to the database
            string constr = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(constr))
            {
                string strSQL = "INSERT INTO Student (Stu_Firstname,Stu_Lastname,Stu_Gender_ID,Stu_Address,Stu_PhoneNo,Stu_Year_ID,Stu_Faculty_ID,Stu_Edit_By)"
                              + " VALUES ('" + stu_firstname + "','" + stu_lastname + "','" + stu_genderid + "','" + stu_address + "','" + stu_phoneon + "','" + stu_yearid + "','" + stu_facultyid + "','Administrator')";
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
            TempData["SuccessMessage"] = "บันทึกข้อมูลเรียบร้อย";
            return View("~/Views/Students/StudentAdd.cshtml");
        }
    }
}
