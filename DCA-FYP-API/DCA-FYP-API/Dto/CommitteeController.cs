using DCA_FYP_API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DCA_FYP_API.Dto
{
    public class CommitteeController : ApiController
    {
        DBEntities db = new DBEntities();

        [HttpGet]
        public HttpResponseMessage GetCategories()
        {
            var casecategories = db.Casecategories.ToList();
            return Request.CreateResponse(HttpStatusCode.OK, casecategories);
        }
        [HttpGet]
        public HttpResponseMessage GetCategoryTeachers(int categoryId)
        {
            var tc = db.Teachercategories.Where(t => t.Categoryid == categoryId).Select(t => t.Teacherid).ToList();
            if (tc!=null)
            {
                var teacherIds = tc.Select(x => int.Parse(x)).ToList();
                var teachers = db.Users.Where(t => teacherIds.Contains(t.Uid)).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, teachers);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, new List<User> { });

            }
        }
        [HttpPost]
        public HttpResponseMessage Create(Teachercategory req)
        {
            try
            {
                db.Teachercategories.Add(req);
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
