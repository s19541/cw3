using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Cwiczenia3.Controllers
{
    [ApiController]
    [Route("api/enrollments")]
    public class EnrollmentsController : ControllerBase
    {
        [HttpPost]
        public IActionResult createEnrollment(Requests.EnrollmentRequest request)
        {
          
            if (request.indexNumber == null || request.firstName == null || request.lastName == null || request.birthDate.Equals(new DateTime(0001, 01, 01, 00, 00, 00)) || request.studies == null)
                return BadRequest("brak wszyskich danych");
            if (getStudiesId(request.studies)==-1)
                return BadRequest("brak studiów w bazie");
            if (!inEnrollment(getStudiesId(request.studies)))
               //TODO dodac do enrollment nowy wpis
               //TODO dodac studenta
            return Ok(request);
        }
        int getStudiesId(String name)
        {
            using (var connection = new SqlConnection("Data Source=db-mssql;Initial Catalog=s19541;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                com.Connection = connection;
                com.CommandText = "select * from Studies";

                connection.Open();
                var dr = com.ExecuteReader();
                while (dr.Read())
                    if (name.Equals(dr["Name"].ToString()))
                        return Int32.Parse(dr["IdStudy"].ToString());
                return -1;
            }

        }
        Boolean inEnrollment(int studiesId)
        {
            using (var connection = new SqlConnection("Data Source=db-mssql;Initial Catalog=s19541;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                com.Connection = connection;
                com.CommandText = "select * from Enrollment";

                connection.Open();
                var dr = com.ExecuteReader();
                while (dr.Read())
                    if (studiesId == Int32.Parse(dr["IdStudy"].ToString())&& Int32.Parse(dr["IdStudy"].ToString())==1)
                        return true;
                return false;
            }
        }
    }
}
