using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Student.DataAccess;

namespace StudentManagement.Controllers
{
    public class StudentController : ApiController
    {
        // GET: api/Student
        public IEnumerable<Student_Table> Get()
        {
            using (OgrenciDBEntities entities = new OgrenciDBEntities())
            {
                return entities.Student_Table.ToList();

            }

        }

        // GET: api/Student/5
        public HttpResponseMessage Get(int id)
        {
            using (OgrenciDBEntities entities = new OgrenciDBEntities())
            {
                var entity = entities.Student_Table.FirstOrDefault(e => e.ID == id);
                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                        "Student with Id " + id.ToString() + " not found");
                }
            }
        }

        // POST: api/Student
        public HttpResponseMessage Post([FromBody] Student_Table student)
        {
            try
            {
                using (OgrenciDBEntities entities = new OgrenciDBEntities())
                {
                    entities.Student_Table.Add(student);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, student);
                    message.Headers.Location = new Uri(Request.RequestUri +
                        student.ID.ToString());

                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // PUT: api/Student/5
        public HttpResponseMessage Put(int id, [FromBody]Student_Table student)
        {
            try
            {
                using (OgrenciDBEntities entities = new OgrenciDBEntities())
                {
                    var entity = entities.Student_Table.FirstOrDefault(e => e.ID == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "Student with Id " + id.ToString() + " not found to update");
                    }
                    else
                    {
                        entity.FirstName = student.FirstName;
                        entity.LastName = student.LastName;
                        entity.Department = student.Department;

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

        // DELETE: api/Student/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (OgrenciDBEntities entities = new OgrenciDBEntities())
                {
                    var entity = entities.Student_Table.FirstOrDefault(e => e.ID == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "Student with Id = " + id.ToString() + " not found to delete");
                    }
                    else
                    {
                        entities.Student_Table.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
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
    
