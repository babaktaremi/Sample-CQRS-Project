using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sample.Core.Common.Marks;
using Sample.Core.Common.Pipelines;
using Sample.Core.MovieApplication.BackgroundWorker.AddReadMovie;
using Sample.Core.MovieApplication.BackgroundWorker.Common.Channels;
using Sample.Core.MovieApplication.BackgroundWorker.DeleteReadMovie;
using Sample.DAL;
using Sample.DAL.ReadRepositories;
using Sample.DAL.WriteRepositories;
using MongoDB.Driver;

namespace Sample.Web
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
            #region DbContext
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer("Data Source=.;Initial Catalog=SampleCqrs;Integrated Security=true");
            });
            #endregion

            #region IOC
            services.AddScoped<WriteMovieRepository>();
            services.AddScoped<DirectorRepository>();

            services.AddSingleton<ReadModelChannel>();
            services.AddSingleton<DeleteModelChannel>();


            #region Mongo Singleton Injection

            var mongoClient = new MongoClient("mongodb://localhost:27017");
            var mongoDatabase = mongoClient.GetDatabase("moviesdatabase");
            services.AddSingleton(mongoDatabase);

            #endregion


            services.AddScoped<ReadMovieRepository>();

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            #endregion

            services.AddMediatR(typeof(ICommitable).Assembly);

            services.AddHostedService<AddReadModelWorker>();
            services.AddHostedService<DeleteReadMovieWorker>();

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sample.Web", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sample.Web v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}