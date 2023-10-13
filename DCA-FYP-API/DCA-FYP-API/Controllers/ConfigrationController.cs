using DCA_FYP_API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DCA_FYP_API.Controllers
{
    public class ConfigrationController : ApiController
    {
        DBEntities db = new DBEntities();

      
        [HttpPost]
        public HttpResponseMessage SetValue(int val)
        {
            var res = db.TRASH_hold.FirstOrDefault();
            if (res != null)
            {

                res.Trash_Hold1 = val;
                db.Entry(res).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                res = new TRASH_hold
                {
                    id=1,
                    Trash_Hold1=val
                };
                db.TRASH_hold.Add(res);
                db.SaveChanges();
            }
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }

       
        [HttpGet]
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
        public HttpResponseMessage GetReg10studentByCategory(int id)
        {
            var response = new List<User>();
            var Cases = db.ReportCases.ToList();
            var users = db.Users.ToList();
            var Trashhold = db.TRASH_hold.FirstOrDefault();

            foreach (var u in users)
            {
                var count = Cases.Where(c => c.StudentId == u.Uid && c.CategoryId==id).Count();
                if (count >= Trashhold.Trash_Hold1)
                {
                    response.Add(u);

                }
            }


            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
    }
}
