using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NurseryDataAccess;

namespace NurseryAPIS.Controllers
{
    public class NotificationController : ApiController
    {
        public HttpResponseMessage Get(int id)
        {
            NurseryEntities entities = new NurseryEntities();

            var entity = entities.notifications.Where(e => e.nursery_id == id).ToList();

            if (entity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, entity);
            }
            else
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "ID Not Found");
        }
        public HttpResponseMessage Get(DateTime created, int id)
        {
            NurseryEntities entities = new NurseryEntities();

            var entity = entities.notifications.Where(e => e.created_on.Year == created.Year
                                                  && e.created_on.Month == created.Month
                                                  && e.created_on.Day == created.Day
                                                  && e.nursery_id == id);

            if (entity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, entity);
            }
            else
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "ID Not Found");
        }

        public HttpResponseMessage Post([FromBody] notification noti)
        {

            try
            {
                NurseryEntities entities = new NurseryEntities();

                noti.created_on = DateTime.Now;
                entities.notifications.Add(noti);
                entities.SaveChanges();

                var message = Request.CreateResponse(HttpStatusCode.Created, noti);

                message.Headers.Location = new Uri(Request.RequestUri + "/" + noti.id.ToString());
                return message;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
