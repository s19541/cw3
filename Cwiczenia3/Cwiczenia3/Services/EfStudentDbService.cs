using Cwiczenia3.ModelsF;
using Cwiczenia3.Requests;
using Cwiczenia3.services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;




namespace Cwiczenia3.Services
{
    public class EfStudentDbService: IStudentsDbService
    {
        private readonly s19541Context _context;
       
        public EfStudentDbService(s19541Context context)
        {
            _context = context;
        }
        public IEnumerable<Student> getStudents()
        {
            return _context.Student.ToList();
        }
        public IActionResult deleteStudent(string id)
        {
            var studentToDelete = _context.Student.Where(student => student.IndexNumber == id);
            if (studentToDelete.Count() == 0)
                return new BadRequestResult();
            _context.Student.Remove(studentToDelete.First());
            _context.SaveChanges();
            return new OkResult();
        }
        public IActionResult modifyStudent(Student studentReq)
        {
            var studentToModify = _context.Student.FirstOrDefault(student => student.IndexNumber == studentReq.IndexNumber);
            if (studentToModify != null)
            {
                if (studentReq.FirstName != null)
                    studentToModify.FirstName = studentReq.FirstName;
                if (studentReq.LastName != null)
                    studentToModify.LastName = studentReq.LastName;
                studentToModify.BirthDate = studentReq.BirthDate;
                
            }
            _context.SaveChanges();
            return new OkResult();
        }
        public IActionResult createEnrollment(EnrollmentRequest request)
        {
            if (request.indexNumber == null || request.firstName == null || request.lastName == null || request.birthDate.Equals(new DateTime(0001, 01, 01, 00, 00, 00)) || request.studies == null)
                return new BadRequestResult();
           
            var studiesId = _context.Studies.Where(studies => studies.Name == request.studies).Select(studies => new { studies.IdStudy }).First().IdStudy;
           
            var tmp = _context.Enrollment.Where(enrollmentx => enrollmentx.IdStudy == studiesId&&enrollmentx.Semester == 1);
                                    
            if (!tmp.Any())
            {
                _context.Add(new Enrollment()
                {
                    IdEnrollment = _context.Enrollment.Max(enrollmentx => enrollmentx.IdEnrollment) + 1,
                    IdStudy = studiesId,
                    Semester = 1,
                    StartDate = DateTime.Now
                });
            }
            _context.Add(new Student()
            {
                IndexNumber = request.indexNumber,
                FirstName = request.firstName,
                LastName = request.lastName,
                BirthDate = request.birthDate
               
            });

            var tmp2 = _context.Student.Where(student => student.IndexNumber == request.indexNumber);

            if (!tmp.Any())
                return new BadRequestResult();


                var enrollment = _context.Student.Add(new Student()
            {
                IndexNumber = request.indexNumber,
                FirstName = request.firstName,
                LastName = request.lastName,
                BirthDate = request.birthDate,
                IdEnrollment = _context.Enrollment.Max(enrollmentx => enrollmentx.IdEnrollment) + 1
        });
            _context.SaveChanges();
            return new OkObjectResult(enrollment);
        }

        public IActionResult createPromotion(PromotionRequestcs request)
        {
            var studiesId = _context.Studies.Where(studies => studies.Name == request.studies).Select(studies => new { studies.IdStudy }).First().IdStudy;

            var tmp = _context.Enrollment.Where(enrollmentx => enrollmentx.IdStudy == studiesId && enrollmentx.Semester == request.semester);

            if (!tmp.Any())
                return new BadRequestResult();

                _context.Database.ExecuteSqlInterpolated($"PromoteStudents {request.semester},{request.studies}");
                _context.SaveChanges();
           
            return new OkObjectResult(request);
        }
    
    }
}
