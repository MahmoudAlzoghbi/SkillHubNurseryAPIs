using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NurseryDataAccess;

namespace NurseryAPIS.Controllers
{
    public class UserController : ApiController
    {
        public IEnumerable<user> Get()
        {
            NurseryEntities entities = new NurseryEntities();

            return entities.users.ToList();
        }

        public HttpResponseMessage Get(int id)
        {
            NurseryEntities entities = new NurseryEntities();

            var entity = entities.users.FirstOrDefault(e => e.id == id);

            if (entity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, entity);
            }
            else
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "ID Not Found");
        }

        public HttpResponseMessage PutUser(int id , [FromBody] user user)
        {
            try {
                NurseryEntities entities = new NurseryEntities();

                var entity = entities.users.FirstOrDefault(e => e.id == id);

                if (entity == null)
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "ID Not Found");
                else
                {
                    entity.name = user.name;
                    entity.username = user.username;
                    entity.password = user.password;
                    entity.contact = user.contact;
                    entity.age = user.age;
                    entity.address = user.address;

                    entities.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage DeleteUser(int id)
        {
            try
            {
                NurseryEntities entities = new NurseryEntities();

                var entity = entities.users.FirstOrDefault(e => e.id == id);

                if (entity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User Not Found");
                }
                else
                {
                    entities.users.Remove(entity);
                    entities.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK , "User Deleted");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

    }
}
