using DCA_FYP_API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DCA_FYP_API.Controllers
{
    public class MeetingController : ApiController
    {
        DBEntities db = new DBEntities();

       
        public HttpResponseMessage GetReg10student()
        {
            var response = new List<User>();
            var Cases = db.ReportCases.ToList();
            var users = db.Users.ToList();
            var Trashhold = db.TRASH_hold.FirstOrDefault();

            foreach (var u in users)
            {
                var count = Cases.Where(c => c.StudentId == u.Uid).Count();
                if (count >= Trashhold.Trash_Hold1)
                {
                    response.Add(u);

                }
            }


            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [HttpGet]
        public HttpResponseMessage GetStudentMeetings(int studentId)
        {
            try
            {
                var response = new List<CaseMeeting> { };
                var caseIds = db.ReportCases.Where(c => c.StudentId == studentId).Select(c => c.Id).ToList();

                response = db.CaseMeetings.Where(m => caseIds.Contains((int)m.CaseId)).ToList();

                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (System.Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        [HttpGet]
        public HttpResponseMessage GetTeacherMeetings(int ReportedTo)
        {
            try
            {
                var response = new List<CaseMeeting> { };
                var caseIds = db.ReportCases.Where(c => c.ReportedTo == ReportedTo).Select(c => c.Id).ToList();

                response = db.CaseMeetings.Where(m => caseIds.Contains((int)m.CaseId)).ToList();

                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (System.Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
           
        }

        [HttpPost]
        public HttpResponseMessage Create(CaseMeeting req)
        {
            try
            {
                db.CaseMeetings.Add(req);
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
