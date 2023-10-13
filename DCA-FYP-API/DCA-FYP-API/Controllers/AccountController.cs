using DCA_FYP_API.Dto;
using DCA_FYP_API.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DCA_FYP_API.Controllers
{
    public class AccountController : ApiController
    {
        DBEntities db = new DBEntities();

        [HttpPost]
        public HttpResponseMessage Login(LoginRequestDto req)
        {
            var user = db.Users.FirstOrDefault(u=>u.UserName==req.Username && u.Password==req.Password);
            if (user!=null && user?.Usertype == "Teacher")
            {
                var teacherCaseCategory = db.Teachercategories.FirstOrDefault(tc => tc.Teacherid.Contains(user.Uid+""));
                if (teacherCaseCategory == null)
                {
                    user = null;
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, user);
        }
    }
}
