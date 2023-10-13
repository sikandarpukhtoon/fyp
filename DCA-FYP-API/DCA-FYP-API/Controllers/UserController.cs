using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Ajax.Utilities;
using DCA_FYP_API.Models;
using DCA_FYP_API.Dto;
using System.Web;
using System.IO;

namespace DCA_FYP_API.Controllers
{
    public class UserController : ApiController
    {
        DBEntities db = new DBEntities();

        [HttpGet]
        public HttpResponseMessage GetUsersByRole(string role)
        {
            var users = db.Users.Where(u => u.Usertype == role).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, users);
        }

        [HttpPost]
        public HttpResponseMessage AddUser(UserDto req)
        {
            try
            {
                var u = new User { UserName = req.UserName, Password = req.passwrd, Usertype = req.Role, AridNo = "" };
                db.Users.Add(u);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, req);
            }
            catch (System.Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, ex.ToString());
            }

        }
        [HttpGet]
        public HttpResponseMessage Block(int userId)
        {
            try
            {
                var a = db.Users.FirstOrDefault(req => req.Uid == userId);
                if (a != null)
                {
                    a.IsActive = true;
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
        public HttpResponseMessage UnBlock(int userId)
        {
            try
            {
                var a = db.Users.FirstOrDefault(req => req.Uid == userId);
                if (a != null)
                {
                    a.IsActive = true;
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


        //[HttpPost]
        //public HttpResponseMessage uploadImage()
        //{
        //    var file = HttpContext.Current.Request.Files.Count > 0 ?
        //        HttpContext.Current.Request.Files["image"] : null;
        //    var UserName = HttpContext.Current.Request.Form["name"];
        //    var passowrd = HttpContext.Current.Request.Form["passowrd"];
        //    var uType = HttpContext.Current.Request.Form["uType"];



        //    if (file != null && file.ContentLength > 0)
        //    {

        //        BinaryReader br = new BinaryReader(file.InputStream);

        //        var name = Path.GetFileName(file.FileName);
        //        var ImageName = name + ".jpg";
        //        var path = @"D:\1stBackup for the fyp project\DCA-FYP-API\DCA-FYP-API\DCA-FYP-API\images";
        //        var imgPath = Path.Combine(path, ImageName);
        //        file.SaveAs(imgPath);
        //        User u = new User();
        //        u.UserName = UserName;
        //        u.image = ImageName;
        //        u.Password = passowrd;
        //        u.Usertype = uType;

        //        db.Users.Add(u);
        //        db.SaveChanges();
        //        return Request.CreateResponse(HttpStatusCode.OK, 1);
        //    }
        //    else
        //    {
        //        return Request.CreateResponse(HttpStatusCode.OK, 0);
        //    }
        //}


    }
}
