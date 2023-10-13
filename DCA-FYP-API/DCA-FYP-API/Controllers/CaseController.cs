using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using DCA_FYP_API.Models;
using DCA_FYP_API.ViewModel;

namespace DCA_FYP_API.Controllers
{
    public class CaseController : ApiController
    {
        DBEntities db = new DBEntities();
        [HttpGet]
        public HttpResponseMessage GetCases()
        {
            var response = new List<ReportCaseViewModel> { };
            var Cases = db.ReportCases.ToList();
            var users = db.Users.ToList();
            var categories = db.Casecategories.ToList();

            foreach(var c in Cases)
            {
                var reportedBy = users.FirstOrDefault(u => u.Uid == c.ReportedBy);
                var reportedTo = users.FirstOrDefault(u => u.Uid == c.ReportedTo);
                var reportedStudent = users.FirstOrDefault(u => u.Uid == c.StudentId);
                var category = categories.FirstOrDefault(ct => ct.Categoryid == c.CategoryId);

                response.Add(new ReportCaseViewModel
                {
                    Casecategory=category,
                    ReportedBy=reportedBy,
                    ReportedTo=reportedTo,
                    Student=reportedStudent,
                    Description=c.Description,
                    Image=c.Image,
                    CaseId=c.Id,
                    ReportedDate = c.ReportedDate
                });

            }


            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [HttpGet]
        public HttpResponseMessage GetTeacherCases(int ReportedTo)
        {
            var response = new List<ReportCaseViewModel> { };
            var tCategories = db.Teachercategories.Where(tc => tc.Teacherid.Contains(ReportedTo+"")).Select(tc => tc.Categoryid).ToList();
            var Cases = db.ReportCases.Where(c => c.ReportedTo == ReportedTo || tCategories.Contains(c.CategoryId)).ToList();
            var users = db.Users.ToList();
            var categories = db.Casecategories.ToList();
            var meetings = db.CaseMeetings.ToList();
            foreach (var c in Cases)
            {
                var reportedBy = users.FirstOrDefault(u => u.Uid == c.ReportedBy);
                var reportedTo = users.FirstOrDefault(u => u.Uid == c.ReportedTo);
                var reportedStudent = users.FirstOrDefault(u => u.Uid == c.StudentId);
                var category = categories.FirstOrDefault(ct => ct.Categoryid == c.CategoryId);

                response.Add(new ReportCaseViewModel
                {
                    Casecategory = category,
                    ReportedBy = reportedBy,
                    ReportedTo = reportedTo,
                    Student = reportedStudent,
                    Description = c.Description,
                    Image = c.Image,
                    CaseId = c.Id,
                    ReportedDate=c.ReportedDate,
                    CaseType=c.CaseType,
                    IsMeetingSet = meetings.Where(m => m.CaseId == c.Id).Any()
                });

            }


            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [HttpGet]
        public HttpResponseMessage GetStudentCases(int studentId)
        {
            var response = new List<ReportCaseViewModel> { };
            var Cases = db.ReportCases.Where(c => c.StudentId == studentId).ToList();
            var users = db.Users.ToList();
            var meetings = db.CaseMeetings.ToList();
            var categories = db.Casecategories.ToList();

            foreach (var c in Cases)
            {
                var reportedBy = users.FirstOrDefault(u => u.Uid == c.ReportedBy);
                var reportedTo = users.FirstOrDefault(u => u.Uid == c.ReportedTo);
                var reportedStudent = users.FirstOrDefault(u => u.Uid == c.StudentId);
                var category = categories.FirstOrDefault(ct => ct.Categoryid == c.CategoryId);

                response.Add(new ReportCaseViewModel
                {
                    Casecategory = category,
                    ReportedBy = reportedBy,
                    ReportedTo = reportedTo,
                    Student = reportedStudent,
                    Description = c.Description,
                    Image = c.Image,
                    CaseId = c.Id,
                    ReportedDate = c.ReportedDate,
                    CaseType = c.CaseType,
                    IsMeetingSet=meetings.Where(m=>m.CaseId==c.Id).Any()
                });

            }


            return Request.CreateResponse(HttpStatusCode.OK, response);
        }


        [HttpGet]
        public HttpResponseMessage GetCase(int CaseId)
        {
            var response = new List<ReportCaseViewModel> { };
            var Cases = db.ReportCases.Where(c => c.Id == CaseId).ToList();
            var users = db.Users.ToList();
            var meetings = db.CaseMeetings.ToList();
            var categories = db.Casecategories.ToList();
            var punishment = db.AssignPunishents.FirstOrDefault(p => p.CaseId == CaseId);
            foreach (var c in Cases)
            {
                var reportedBy = users.FirstOrDefault(u => u.Uid == c.ReportedBy);
                var reportedTo = users.FirstOrDefault(u => u.Uid == c.ReportedTo);
                var reportedStudent = users.FirstOrDefault(u => u.Uid == c.StudentId);
                var category = categories.FirstOrDefault(ct => ct.Categoryid == c.CategoryId);

                response.Add(new ReportCaseViewModel
                {
                    Casecategory = category,
                    ReportedBy = reportedBy,
                    ReportedTo = reportedTo,
                    Student = reportedStudent,
                    Description = c.Description,
                    Image = c.Image,
                    CaseId = c.Id,
                    ReportedDate = c.ReportedDate,
                    CaseType = c.CaseType,
                    Meeting = meetings.FirstOrDefault(m => m.CaseId == c.Id),
                    Punishment=punishment
                });

            }


            return Request.CreateResponse(HttpStatusCode.OK, response.FirstOrDefault());
        }

        [HttpGet]
        public HttpResponseMessage GetMeetingCases(int teacherId)
        {
            var response = new List<ReportCaseViewModel> { };
            var Cases = db.ReportCases.Where(c=>c.ReportedTo==teacherId).ToList();
            var users = db.Users.ToList();
            var categories = db.Casecategories.ToList();
            var meetings = db.CaseMeetings.ToList();
            foreach (var c in Cases)
            {
                var reportedBy = users.FirstOrDefault(u => u.Uid == c.ReportedBy);
                var reportedTo = users.FirstOrDefault(u => u.Uid == c.ReportedTo);
                var reportedStudent = users.FirstOrDefault(u => u.Uid == c.StudentId);
                var category = categories.FirstOrDefault(ct => ct.Categoryid == c.CategoryId);

                response.Add(new ReportCaseViewModel
                {
                    Casecategory = category,
                    ReportedBy = reportedBy,
                    ReportedTo = reportedTo,
                    Student = reportedStudent,
                    Description = c.Description,
                    Image = c.Image,
                    CaseId = c.Id,
                    ReportedDate = c.ReportedDate,
                    CaseType = c.CaseType,
                    IsMeetingSet = meetings.Where(m => m.CaseId == c.Id).Any(),
                    Meeting = meetings.FirstOrDefault(m => m.CaseId == c.Id)
                }); 

            }

            response = response.Where(c => c.IsMeetingSet == true).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }


        [HttpGet]
        public HttpResponseMessage GetAttendance(int CaseId)
        {
            var dates = db.TrackPunishments
                        .Where(c => c.caseid == CaseId)
                        .Select(c => c.date).ToList();

            var res = dates.Select(d => d.ToString("dd-MM-yyyy")).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }


        [HttpGet]
        public HttpResponseMessage GetRegCases ()
        {
            var response = new List<StudentCaseCountViewModel> { };
            var Cases = db.ReportCases.GroupBy(c => c.StudentId).ToList();
            var users = db.Users.ToList();
            
            foreach (var c in Cases)
            {
                var reportedStudent = users.FirstOrDefault(u => u.Uid == c.Key);
                var totalCases = c.Count();
                response.Add(new StudentCaseCountViewModel
                {
                    Id = reportedStudent.Uid,
                    Name=reportedStudent.UserName,
                    RegNo=reportedStudent.AridNo,
                    TotalCases=totalCases
                }) ;

            }


            return Request.CreateResponse(HttpStatusCode.OK, response);
        }



        [HttpPost]
        public HttpResponseMessage Create(ReportCase req)
        {
            try
            {
                db.ReportCases.Add(req);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, req);
            }
            catch (System.Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }


      



    }
}