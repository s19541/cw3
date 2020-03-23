﻿using System;
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
                    st.FirstName = dr["FirstName"].ToString();
                    st.LastName = dr["LastName"].ToString();
                    st.IndexNumber = dr["IndexNumber"].ToString();
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
            if(id == 1)
            {
                return Ok("Kowalski");
            }
            else if (id == 2)
            {
                return Ok("Malewski");
            }
            return NotFound("Nie znaleziono studenta");
        }
        [HttpPost]
        public IActionResult CreateStudent(Models.Student student)
        {
            student.IndexNumber = $"s{new Random().Next(1, 20000)}";
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