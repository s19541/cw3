using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Cwiczenia3.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cwiczenia3.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private readonly DAL.IDbService _dbService;

        public StudentsController(DAL.IDbService dbService)
        {
            _dbService = dbService;
        }
        [HttpGet]
        public IActionResult GetStudents(string orderBy)
        {
            var stList = new List<Student>();
            using (var connection = new SqlConnection("Data Source=db-mssql;Initial Catalog=s19541;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                com.Connection = connection;
                com.CommandText = "select * from Student";

                connection.Open();
                var dr = com.ExecuteReader();
                while (dr.Read())
                {
                    var st = new Student();
                    st.indexNumber = dr["IndexNumber"].ToString();
                    st.firstName = dr["FirstName"].ToString();
                    st.lastName = dr["LastName"].ToString();
                    st.birthDate = (DateTime)dr["BirthDate"];
                    st.idEnrollment = Int32.Parse(dr["IdEnrollment"].ToString());
                    
                    stList.Add(st);
                }
            }
            return Ok(stList);
            //return Ok(_dbService.GetStudents());
        }
        public string GetStudent(string orderBy)
        {
            return $"Kowalski, Malewski, Andrzejewski sortowanie={orderBy}";
        }
        [HttpGet("{id}")]
        public IActionResult GetStudent(int id)
        {
            /*if(id == 1)
            {
                return Ok("Kowalski");
            }
            else if (id == 2)
            {
                return Ok("Malewski");
            }
            return NotFound("Nie znaleziono studenta");*/
            Enrollment en=new Enrollment();
            int eId;
            using (var connection = new SqlConnection("Data Source=db-mssql;Initial Catalog=s19541;Integrated Security=True"))
            using (var com = new SqlCommand())
            {

                com.Connection = connection;
                com.CommandText = "select * from Student where IndexNumber=@id";
                com.Parameters.AddWithValue("id", id);

                connection.Open();
                var dr = com.ExecuteReader();
                dr.Read();
                eId = Int32.Parse(dr["IdEnrollment"].ToString());
                connection.Close();

                com.CommandText = "select * from Enrollment where IdEnrollment="+eId;

                connection.Open();
                dr = com.ExecuteReader();
                dr.Read();


                    en.idEnrollment = Int32.Parse(dr["IdEnrollment"].ToString());
                    en.semestr = Int32.Parse(dr["Semester"].ToString());
                    en.idStudies = Int32.Parse(dr["IdStudy"].ToString());
                    en.startDate = (DateTime)dr["StartDate"];

                    
               
            }
            return Ok(en);

        }
        [HttpPost]
        public IActionResult CreateStudent(Models.Student student)
        {
            student.indexNumber = $"s{new Random().Next(1, 20000)}";
            return Ok(student);
        }
        [HttpPut("{id}")]
        public IActionResult PutStudent(int id)
        {
            if (id == 1 || id == 2)
            {
                return Ok("Aktualizacja ukończona");
            }
            return NotFound("Nie znaleziono studenta");
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            if (id == 1 || id == 2)
            {
                return Ok("Usuwanie ukończone");
            }
            return NotFound("Nie znaleziono studenta");
        }
    }
}