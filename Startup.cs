using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.FileProviders;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

using backend.Middleware;
using backend.Middleware.jwt;
using backend.util;
using backend.Schedule;

using Coravel;
using backend.Middleware.jwt_t;
using Microsoft.AspNetCore.Http.Features;

namespace backend
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
      services.AddHttpContextAccessor();
      services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

      string[] corsOrigins = Configuration["Cors:AllowOrigin"].Split(',', StringSplitOptions.RemoveEmptyEntries);
      services.AddCors(options =>
      {
        options.AddDefaultPolicy(
                  builder =>
                  {
                    if (corsOrigins.Contains("*"))
                    {
                      builder.SetIsOriginAllowed(_ => true);
                    }
                    else
                    {
                      builder.WithOrigins(corsOrigins);
                    }
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                    builder.AllowCredentials();
                  });
      });
      # region sample+留言板
      // Service
      services.AddScoped<Services.sampleService>();
      services.AddScoped<Services.GuestbooksService>();
      services.AddScoped<Services.MailService>();
      services.AddScoped<Services.KidService>();
      services.AddScoped<Services.PaintingService>();
      services.AddScoped<Services.MedicalService>();


      // Dao
      services.AddScoped<dao.sampleDao>();
      services.AddScoped<dao.guestbooksDao>();
      services.AddScoped<dao.kidDao>();
      services.AddScoped<dao.paintingDao>();
      services.AddScoped<dao.medicalDao>();


      # endregion
      // JWT Authorize
      services.AddScoped<IUserService_T, loginService>();
      services.AddScoped<loginDao>();


      services.AddControllers()
          .AddJsonOptions(options =>
              options.JsonSerializerOptions.PropertyNamingPolicy = null);

      services.Configure<appSettings>(Configuration.GetSection("appSettings"));
      //圖片大小
      services.Configure<FormOptions>(options =>
      {
        options.MultipartBodyLengthLimit = long.MaxValue; // or a specific limit you desire
      });

      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "backend", Version = "v1" });
      });
      services.AddMvc();

      /* 套件參考 Coravel */
      /// 排程
      services.AddScheduler();
      services.AddTransient<analysisSchedule>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      app.UseCors();

      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      app.UseSwagger();
      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = "";
      });
      // app.UseHttpsRedirection();

      app.UseStaticFiles();
      // app.UseStaticFiles(new StaticFileOptions{
      //     FileProviders = new PhysicalFileProvider(
      //         Path.Combine(env.ContentRootPath, "Files")
      //     ),
      //     RequestPath = "/Files"
      // });

      app.UseRouting();


      app.UseAuthorization();

      /* 中介軟體 Middleware 設定 */
      app.UseMiddleware<jwtMiddleware>();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });

      // 排程
      // var provider = app.ApplicationServices;

      // provider.UseScheduler(scheduler =>
      // {
      /* 套件參考 Coravel */
      //// utc 時間，換算台北時間要+8
      //// 故台北回推 -8
      // scheduler.Schedule<createTableSchedule>().Cron("00 16 30 12 *");
      // });
    }
  }
}
