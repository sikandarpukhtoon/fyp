using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DCA_FYP_API.Models;
using DCA_FYP_API.Dto;

namespace DCA_FYP_API.Controllers
{
    public class PunishmentController : ApiController
    {
        DBEntities db = new DBEntities();
        [HttpPost]
        public HttpResponseMessage Create(AssignPunishent req)
        {
            try
            {
                db.AssignPunishents.Add(req);
                db.SaveChanges();
                var c= db.ReportCases.FirstOrDefault(cs => cs.Id == req.CaseId);
                if (c != null)
                {
                    c.CaseType = "Active";
                    db.Entry(c).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                return Request.CreateResponse(HttpStatusCode.OK, req);
            }
            catch (System.Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }
        [HttpGet]
        public HttpResponseMessage GetPunishment(int CaseId)
        {
            var punishment = db.AssignPunishents.FirstOrDefault(p => p.CaseId == CaseId);
            

            return Request.CreateResponse(HttpStatusCode.OK, punishment);
        }
        [HttpGet]
        public HttpResponseMessage GetRegStudent(int studentId)
        {
            var reg = db.Register10.FirstOrDefault(p => p.id == studentId);


            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        public HttpResponseMessage update(UpdatePunishment app)
        {
            try
            {
                var a = db.AssignPunishents.FirstOrDefault(req => req.CaseId == app.id);
                if (a != null)
                {
                    a.EndDate = app.EndDate.ToString("yyyy-MM-dd");
                    a.Fine = app.fine;
                    db.Entry(a).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }

                return Request.CreateResponse(HttpStatusCode.OK, a);
            }
            catch (System.Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }

        }


    }
}