using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cwiczenia3.Services
{
    public interface IStudentsDbService
    {
        IEnumerable<ModelsF.Student> getStudents();
        IActionResult deleteStudent(string id);
        IActionResult modifyStudent(ModelsF.Student studentReq);
        IActionResult createEnrollment(Requests.EnrollmentRequest request);
        IActionResult createPromotion(Requests.PromotionRequestcs request);
        //int getStudiesId(String name);
       // Boolean inEnrollment(int studiesId, int semester);
       // Boolean isIndexUnique(string index);
    }
}
