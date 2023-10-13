using DCA_FYP_API.Dto;
using DCA_FYP_API.Models;
using DCA_FYP_API.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DCA_FYP_API.Controllers
{
    public class RequestAppealController : ApiController
    {

        DBEntities db = new DBEntities();
        [HttpPost]
        public HttpResponseMessage Create(RequestAppeal req)
        {
            try
            {
                db.RequestAppeals.Add(req);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, req);
            }
            catch (System.Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpPost]
        public HttpResponseMessage update(UpdateAppeal app)
        {
            try
            {
                var a = db.RequestAppeals.FirstOrDefault(req=>req.caseid==app.id);
                if (a != null)
                {
                    a.Directorcomments = app.comments;
                    db.Entry(a).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                
                return Request.CreateResponse(HttpStatusCode.OK, a);
            }
            catch (System.Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpPost]
        public HttpResponseMessage updatehoccomment(UpdateAppeal app)
        {
            try
            {
                var a = db.RequestAppeals.FirstOrDefault(req => req.caseid == app.id);
                if (a != null)
                {
                    a.hocComment= app.comments;
                    db.Entry(a).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }

                return Request.CreateResponse(HttpStatusCode.OK, a);
            }
            catch (System.Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpPost]
        public HttpResponseMessage Reg10(Register10 app)
        {
            try
            {
                var a = db.Register10.FirstOrDefault( );
                if (a != null)
                {
                
                    db.Entry(a).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }

                return Request.CreateResponse(HttpStatusCode.OK, a);
            }
            catch (System.Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }


        [HttpGet]
        public HttpResponseMessage GetRequestAppeals()
        {
            var response = new List<ReportCaseViewModel> { };
            var Cases = db.ReportCases.ToList();
            var users = db.Users.ToList();
            var categories = db.Casecategories.ToList();
            var meetings = db.CaseMeetings.ToList();
            var appeals = db.RequestAppeals.ToList();
            var punishment = db.AssignPunishents.ToList();
            foreach (var c in Cases)
            {
                var reportedBy = users.FirstOrDefault(u => u.Uid == c.ReportedBy);
                var reportedTo = users.FirstOrDefault(u => u.Uid == c.ReportedTo);
                var reportedStudent = users.FirstOrDefault(u => u.Uid == c.StudentId);
                var category = categories.FirstOrDefault(ct => ct.Categoryid == c.CategoryId);
                var appeal = appeals.FirstOrDefault(ct => ct.caseid == c.Id);
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
                    Meeting = meetings.FirstOrDefault(m => m.CaseId == c.Id),
                    Appeal=appeal,
                    Punishment=punishment.FirstOrDefault(p=>p.CaseId==c.Id)
                });

            }
            response = response.Where(c => c.IsMeetingSet == true && c.Punishment!=null && c.Appeal!=null).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [HttpGet]
        public HttpResponseMessage GetAppeal(int caseId)
        {
            try
            {
                var a = db.RequestAppeals.FirstOrDefault(req => req.caseid == caseId);
                
                return Request.CreateResponse(HttpStatusCode.OK, a);
            }
            catch (System.Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }
        [HttpGet]
        public HttpResponseMessage Registerten(int user_id)
        {
            try
            {
                var user = db.Users.Find(user_id);
                var cases = (from rc in db.ReportCases
                             join u in db.Users on rc.StudentId equals u.Uid
                             where u.Uid == user_id
                             select u.Uid).ToList();
                int count = int.Parse(cases.Count().ToString());
                if (count >= 6)
                {
                   Register10 rg = new Register10();
                    rg.id = user_id;
                    // Add the new Case to the Cases table
                    db.Register10.Add(rg);
                    // Save changes to the database
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, "Alert Register10 student ");
                }

                return Request.CreateResponse(HttpStatusCode.OK, "p");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
                throw;
            }
        }


    }
}
