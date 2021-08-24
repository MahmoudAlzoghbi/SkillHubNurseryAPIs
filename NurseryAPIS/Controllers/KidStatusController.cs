using NurseryDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NurseryAPIS.Controllers
{
    public class KidStatusController : ApiController
    {
        public HttpResponseMessage PutUserStatus(int id, [FromBody] Kid kid)
        {
            try
            {
                NurseryEntities entities = new NurseryEntities();

                var entity = entities.Kids.FirstOrDefault(e => e.id == id);

                if (entity == null)
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "ID Not Found");
                else
                {
                    entity.collected = kid.collected;
                    entity.approve = kid.approve;

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
