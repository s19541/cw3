using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cwiczenia3.Models;

namespace Cwiczenia3.DAL
{
    public class MockDbService : IDbService
    {
        private static IEnumerable<Student> _students;

        static MockDbService()
        {
            _students = new List<Student>
            {
                new Student{indexNumber="s195",firstName="Jan",lastName="Kowalski"},
                new Student{indexNumber="s195333",firstName="Anna",lastName="Malewski"},
                new Student{indexNumber="s1",firstName="Andrzej",lastName="Andrzejewicz"}
            };
        }
        public IEnumerable<Student> GetStudents()
        {
            return _students;
        }
    }
}
