using NurseryDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NurseryAPIS.Controllers
{
    public class LoginController : ApiController
    {

        public HttpResponseMessage GetUserLogIn( string username , string password)
        {
            NurseryEntities entities = new NurseryEntities();

            var entityusername = entities.users.Where(e => e.username.Equals(username) && e.password == password).FirstOrDefault();

            if (entityusername != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK , entityusername);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound , "userName not found");
            }
        }
        public IEnumerable<user> Get()
        {
            NurseryEntities entities = new NurseryEntities();

            return entities.users.ToList();
        }
        public HttpResponseMessage getUser(int id)
        {
            NurseryEntities entities = new NurseryEntities();

            var user = entities.users.FirstOrDefault(e => e.id == id);

            if (user != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK , user);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound , "User Not Found");
            }
        }
        public HttpResponseMessage PostUser([FromBody] user user)
        {
            try
            {
                NurseryEntities entities = new NurseryEntities();

                user.creaeted_on = DateTime.Now;
                entities.users.Add(user);
                entities.SaveChanges();

                var message = Request.CreateResponse(HttpStatusCode.Created, user);

                message.Headers.Location = new Uri(Request.RequestUri + "/" + user.id.ToString());
                return message;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        public HttpResponseMessage PutUser(int id, [FromBody] user user)
        {
            try
            {
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
                    entity.nursery_id = user.nursery_id;

                    entities.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
