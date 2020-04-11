using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;



namespace Cwiczenia3
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<DAL.IDbService, DAL.MockDbService>();
            services.AddSingleton<Services.IStudentsDbService,Services.SqlServerDbService>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //services.AddTransient<DAL.IDbService, Services.SqlServerDbService>();
            //services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseMiddleware<MiddleWares.LoggingMiddleware>();
            app.UseHttpsRedirection();
            app.UseMvc();
            app.Use(async (context, next) =>
            {

                if (!context.Request.Headers.ContainsKey("Index"))
                {
                    context.Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Brak nagłówku z indexem");
                    return;
                }
                else
                {
                    string index = context.Request.Headers["Index"].ToString();
                    var connection = new SqlConnection("Data Source=db-mssql;Initial Catalog=s19541;Integrated Security=True");
                    using(var com = new SqlCommand())
                    {
                        com.Connection = connection;
                        com.CommandText = "select IndexNumber from Student";
                        connection.Open();
                        var dr = com.ExecuteReader();
                        Boolean tmp = false;
                        while (dr.Read())
                        {
                            if (dr["IndexNumber"].ToString().Equals(index))
                                tmp = true;
                            
                        }
                        if(!tmp)
                        {
                            context.Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound;
                            await context.Response.WriteAsync("Brak studenta z podanym indexem");
                            return;
                        }
                    }
                    connection.Close();
                }

                await next();
            });
            //app.UseRouting();

           // app.UseAuthorization();

           // app.UseEndpoints(endpoints =>
           // {
           //     endpoints.MapControllers();
           // });
            
        }
    }
}
