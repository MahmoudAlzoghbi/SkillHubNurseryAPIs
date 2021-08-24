using NurseryDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace NurseryAPIS.Controllers
{
    public class DailyReportController : ApiController
    {
        public HttpResponseMessage Get(int id)
        {
            NurseryEntities entities = new NurseryEntities();

            var entity = entities.daily_report.Where(e => e.kid_id == id);

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

            var entity = entities.daily_report.Where(e => e.created_on.Year == created.Year
                                                  && e.created_on.Month == created.Month
                                                  && e.created_on.Day == created.Day
                                                  && e.kid_id == id);

            if (entity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, entity);
            }
            else
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "ID Not Found");
        }

        public HttpResponseMessage Post([FromBody] daily_report dailyReport)
        {

            try
            {
                NurseryEntities entities = new NurseryEntities();

                dailyReport.created_on = DateTime.Now;
                entities.daily_report.Add(dailyReport);
                entities.SaveChanges();

                var message = Request.CreateResponse(HttpStatusCode.Created, dailyReport);

                message.Headers.Location = new Uri(Request.RequestUri + "/" + dailyReport.id.ToString());
                return message;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
