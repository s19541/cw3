using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cwiczenia3.services
{
    public interface IStudentsDbService
    {
        Models.Enrollment createEnrollment(Requests.EnrollmentRequest request);
        Models.Enrollment createPromotion(Requests.PromotionRequestcs request);
        int getStudiesId(String name);
        Boolean inEnrollment(int studiesId, int semester);
        Boolean isIndexUnique(string index);
    }
}
