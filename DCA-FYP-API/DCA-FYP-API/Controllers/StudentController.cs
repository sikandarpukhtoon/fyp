using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DCA_FYP_API.Controllers
{
    public class StudentController : ApiController
    {
        SqlConnection conn;
        string constr = @"Data Source=Persist Security Info=False;User ID=sa;Password=12345;Initial Catalog=DisciplineCommitteeAssistant;Data Source=DESKTOP-1PGU9H6";
        [HttpGet]
        public HttpResponseMessage GetAllStudents()
        {
            conn = new SqlConnection(constr);
            conn.Open();
            string query = "select * from [student]";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            cmd.ExecuteNonQuery();
            DataSet ds = new DataSet();
            sda.Fill(ds);
            conn.Close();
            return Request.CreateResponse(HttpStatusCode.OK, ds.Tables[0]);
        }
        [HttpPost]
        public HttpResponseMessage SaveStudent(string Fname, string Lname, string degree,string section,string contact,string AridNo)
        {
            try
            {
                conn = new SqlConnection(constr);
                conn.Open();
                string query = ("insert into [Student] values('" + Fname + "','" + Lname + "','" + degree + "','" + section + "','" + contact + "','" + AridNo + "')");
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                conn.Close();
                return Request.CreateResponse(HttpStatusCode.OK, "Saved Successfully");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        [HttpDelete]
        public HttpResponseMessage DeleteStudent(int sid)
        {
            try
            {
                conn = new SqlConnection(constr);
                conn.Open();
                string query = "delete from [Student] where sid='" + sid + "';";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                conn.Close();
                return Request.CreateResponse(HttpStatusCode.OK, "Delete Succesfully");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        [HttpPut]
        public HttpResponseMessage UpdateStudent(int sid, string Fname, string Lname, string degree,string section,string contact,string AridNo)
        {
            try
            {
                conn = new SqlConnection(constr);
                conn.Open();
                string query = "update [Student] set Fname='" + Fname + "',Lname='" + Lname + "',Degree='" + degree + "',Section='" + section + "',Contact='" + contact + "',AridNo='" + AridNo + "'where sid='" + sid + "';";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                conn.Close();
                return Request.CreateResponse(HttpStatusCode.OK, "Updated Successfully");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}