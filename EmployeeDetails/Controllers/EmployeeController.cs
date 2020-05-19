using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmployeeAccessData;

namespace EmployeeDetails.Controllers
{
    public class EmployeeController : ApiController
    {
        //public IEnumerable<AccountHolderDetail> Get()

        //{
        //    using (WebApiEntities entities = new WebApiEntities())
        //    {
        //        return entities.AccountHolderDetails.ToList();
        //    }

        //}
        public HttpResponseMessage Get(int id)
        {
            using (WebApiEntities entities = new WebApiEntities())
            {
                var entity = entities.AccountHolderDetails.FirstOrDefault(e => e.Id == id);
                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with Id= " + id.ToString() + " not found");

                }
            }
        }

        public HttpResponseMessage Get(string gender = "All")
        {
            using (WebApiEntities entities = new WebApiEntities())
            {
                switch (gender.ToLower())
                {
                    case "all":
                        return Request.CreateResponse(HttpStatusCode.OK, entities.AccountHolderDetails.ToList());
                    case "male":
                        return Request.CreateResponse(HttpStatusCode.OK, entities.AccountHolderDetails.Where(e => e.Gender.ToLower() == "male").ToList());
                    case "female":
                        return Request.CreateResponse(HttpStatusCode.OK, entities.AccountHolderDetails.Where(e => e.Gender.ToLower() == "female").ToList());
                    default:
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "value for gender must be Male,Female or All." + gender + " is invalid.");
                }

            }
        }
        public HttpResponseMessage  Post([FromBody] AccountHolderDetail employee)
        {
            try
            {
                using (WebApiEntities entities = new WebApiEntities()) 
                {
                    entities.AccountHolderDetails.Add(employee);
                    entities.SaveChanges();
                    var massage = Request.CreateResponse(HttpStatusCode.Created, employee);
                    massage.Headers.Location = new Uri(Request.RequestUri + employee.Id.ToString());
                    return massage;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (WebApiEntities entities = new WebApiEntities())
                {
                    var entity = entities.AccountHolderDetails.FirstOrDefault(e => e.Id == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with Id= " + id.ToString() + " not found");
                    }
                    else
                    {
                        entities.AccountHolderDetails.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }

                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        //public HttpResponseMessage Put(int id, [FromBody]AccountHolderDetail employee)
        //{
        //    try
        //    {
        //        using (WebApiEntities entities = new WebApiEntities())
        //        {
        //            var entity = entities.AccountHolderDetails.FirstOrDefault(e => e.Id == id);
        //            if (entity == null)
        //            {
        //                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with id= " + id.ToString() + " not found");

        //            }
        //            else
        //            {
        //                entity.Bank_Name = employee.Bank_Name;
        //                entity.Customer_Name = employee.Customer_Name;
        //                entity.Father_Name = employee.Father_Name;
        //                entities.SaveChanges();
        //                return Request.CreateResponse(HttpStatusCode.OK, entity);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
        //    }
            

        //}

        public HttpResponseMessage Put(int id, [FromUri]AccountHolderDetail employee)
        {
            try
            {
                using (WebApiEntities entities = new WebApiEntities())
                {
                    var entity = entities.AccountHolderDetails.FirstOrDefault(e => e.Id == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with id= " + id.ToString() + " not found");

                    }
                    else
                    {
                        entity.Bank_Name = employee.Bank_Name;
                        entity.Customer_Name = employee.Customer_Name;
                        entity.Father_Name = employee.Father_Name;
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }


        }
    }
}
