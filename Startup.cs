using Literature.Data;
using Literature.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Literature
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
      // Add framework services.
      services.AddMvc();

      // Use SQL Database if in Azure, otherwise, use SQLite
      if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
        services.AddDbContext<MyDatabaseContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("LitDbConnection")));
      else
        services.AddDbContext<MyDatabaseContext>(options =>
                options.UseSqlite("Data Source=localdatabase.db"));

      var context = services.BuildServiceProvider().GetService<MyDatabaseContext>();
      // Automatically perform database migration
      context.Database.Migrate();

      //Apply seeding
      DbInitialiser.Initialize(context);

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
    {
      loggerFactory.AddConsole(Configuration.GetSection("Logging"));
      loggerFactory.AddDebug();
      loggerFactory.AddAzureWebAppDiagnostics();

      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseBrowserLink();
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
      }

      app.UseStaticFiles();

      app.UseMvc(routes =>
      {
        routes.MapRoute(
                  name: "default",
                  template: "{controller=Publications}/{action=Index}/{id?}");
      });
    }
  }
}