using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Cwiczenia3.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Cwiczenia3.Services
{
    public class SqlServerDbService : IStudentsDbService
    {
        public Models.Enrollment createEnrollment(EnrollmentRequest request)
        {
            var connection = new SqlConnection("Data Source=db-mssql;Initial Catalog=s19541;Integrated Security=True");
            connection.Open();
            var transaction = connection.BeginTransaction("myTransaction");
            if (request.indexNumber == null || request.firstName == null || request.lastName == null || request.birthDate.Equals(new DateTime(0001, 01, 01, 00, 00, 00)) || request.studies == null)
                return null;
            if (getStudiesId(request.studies) == -1)
                return null;
            if (!inEnrollment(getStudiesId(request.studies), 1))
            {
                using (var com = new SqlCommand())
                {
                    com.Connection = connection;
                    com.CommandText = "insert into Enrollment values((select max(IdEnrollment)+1 from Enrollment),1,@idStudies,Getdate())";
                    com.Parameters.AddWithValue("idStudies", getStudiesId(request.studies));
                    com.Transaction = transaction;
                    com.ExecuteNonQuery();
                }
            }
            if (!isIndexUnique(request.indexNumber))
            {
                transaction.Rollback();
                return null;
            }
            using (var com = new SqlCommand())
            {
                com.Connection = connection;
                com.CommandText = "insert into Student values(@indexNumber,@firstName,@lastName,@birthDate,(select idEnrollment from Enrollment where idStudy=@idStudies AND semester=1))";
                com.Parameters.AddWithValue("indexNumber", request.indexNumber);
                com.Parameters.AddWithValue("firstName", request.firstName);
                com.Parameters.AddWithValue("lastName", request.lastName);
                com.Parameters.AddWithValue("birthDate", request.birthDate);
                com.Parameters.AddWithValue("idStudies", getStudiesId(request.studies));
                com.Transaction = transaction;
                com.ExecuteNonQuery();
            }
            transaction.Commit();
            Models.Enrollment enrollment = new Models.Enrollment(); ;
            using (var com = new SqlCommand())
            {
                com.Connection = connection;
                com.CommandText = "select * from Enrollment where idStudy=@idStudies";
                com.Parameters.AddWithValue("idStudies", getStudiesId(request.studies));

                var dr = com.ExecuteReader();
                while (dr.Read())
                {
                    enrollment.idEnrollment = Int32.Parse(dr["IdEnrollment"].ToString());
                    enrollment.semestr = Int32.Parse(dr["Semester"].ToString());
                    enrollment.idStudies = Int32.Parse(dr["IdStudy"].ToString());
                    enrollment.startDate = (DateTime)dr["StartDate"];
                }

            }
            connection.Close();
            return enrollment;
        }

        public Models.Enrollment createPromotion(PromotionRequestcs request)
        {
            if (!inEnrollment(getStudiesId(request.studies), request.semester))
                return null;
            var connection = new SqlConnection("Data Source=db-mssql;Initial Catalog=s19541;Integrated Security=True");
            using (var com = new SqlCommand())
            {
                connection.Open();
                com.Connection = connection;
                com.CommandText = "Execute promoteStudents @semester,@study";
                com.Parameters.AddWithValue("semester", request.semester);
                com.Parameters.AddWithValue("study", request.studies);
                com.ExecuteNonQuery();
            }
            Models.Enrollment enrollment = new Models.Enrollment(); ;
            using (var com = new SqlCommand())
            {
                com.Connection = connection;
                com.CommandText = "select * from Enrollment where semester=@semester+1 AND IdStudy=@idStudies";
                com.Parameters.AddWithValue("semester", request.semester);
                com.Parameters.AddWithValue("idStudies", getStudiesId(request.studies));

                var dr = com.ExecuteReader();
                while (dr.Read())
                {
                    enrollment.idEnrollment = Int32.Parse(dr["IdEnrollment"].ToString());
                    enrollment.semestr = Int32.Parse(dr["Semester"].ToString());
                    enrollment.idStudies = Int32.Parse(dr["IdStudy"].ToString());
                    enrollment.startDate = (DateTime)dr["StartDate"];
                }

            }
            connection.Close();
            return enrollment;
        }

        public int getStudiesId(string name)
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

        public bool inEnrollment(int studiesId, int semester)
        {
            using (var connection = new SqlConnection("Data Source=db-mssql;Initial Catalog=s19541;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                com.Connection = connection;
                com.CommandText = "select * from Enrollment";

                connection.Open();
                var dr = com.ExecuteReader();
                while (dr.Read())
                    if (studiesId == Int32.Parse(dr["IdStudy"].ToString()) && Int32.Parse(dr["Semester"].ToString()) == semester)
                        return true;
                return false;
            }
        }

        public bool isIndexUnique(string index)
        {
            using (var connection = new SqlConnection("Data Source=db-mssql;Initial Catalog=s19541;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                com.Connection = connection;
                com.CommandText = "select * from Student";

                connection.Open();
                var dr = com.ExecuteReader();
                while (dr.Read())
                    if (index == dr["IndexNumber"].ToString())
                        return false;
                return true;
            }
        }
    }
}
