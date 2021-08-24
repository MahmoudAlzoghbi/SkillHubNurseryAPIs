using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NurseryDataAccess;

namespace NurseryAPIS.Controllers
{
    public class KidsController : ApiController
    {
        public IEnumerable<Kid> Get()
        {
            NurseryEntities entities = new NurseryEntities();

            return entities.Kids.ToList();
        }

        public HttpResponseMessage Get(int id)
        {
            NurseryEntities entities = new NurseryEntities();

            var entity = entities.Kids.Where(e => e.nursery_id == id).ToList();

            if (entity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, entity);
            }
            else
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "ID Not Found");
        }

        public HttpResponseMessage PostNursery([FromBody] Kid kid)
        {
            try
            {
                NurseryEntities entities = new NurseryEntities();

                kid.created_on = DateTime.Now;

                entities.Kids.Add(kid);
                entities.SaveChanges();

                var message = Request.CreateResponse(HttpStatusCode.Created, kid);

                message.Headers.Location = new Uri(Request.RequestUri + "/" + kid.id.ToString());
                return message;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage PutKid(int id, [FromBody] Kid kid)
        {
            try
            {
                NurseryEntities entities = new NurseryEntities();

                var entity = entities.Kids.FirstOrDefault(e => e.id == id);

                if (entity == null)
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "ID Not Found");
                else
                {
                    entity.name = kid.name;
                    entity.age = kid.age;
                    entity.phone1 = kid.phone1;
                    entity.phone2 = kid.phone2;
                    entity.email = kid.email;
                    entity.password = kid.password;
                    entity.approve = kid.approve;
                    entity.collected = kid.collected;
                    entity.image = kid.image;
                    entity.date_of_birth = kid.date_of_birth;



                    entities.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage DeleteKid(int id)
        {
            try
            {
                NurseryEntities entities = new NurseryEntities();

                var entity = entities.Kids.FirstOrDefault(e => e.id == id);

                if (entity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound , "Kid Not Found");
                }
                else
                {
                    entities.Kids.Remove(entity);
                    entities.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);
                } 
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest , ex);
            }
        }
        public HttpResponseMessage GetKidLogIn(string username, string password)
        {
            NurseryEntities entities = new NurseryEntities();

            var entityusername = entities.Kids.Where(e => e.email.Equals(username) && e.password == password).FirstOrDefault();

            if (entityusername != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, entityusername);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "userName not found");
            }
        }
    }
}
