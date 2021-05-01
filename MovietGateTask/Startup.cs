using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MovieGateTask.DAL.Models;
using Microsoft.EntityFrameworkCore;
using MovieGateTask.DAL.Repo;
using MovietGateTask.BLL.BOs;
using Newtonsoft;

namespace MovietGateTask {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddControllers().AddNewtonsoftJson();
            services.AddMvc().AddNewtonsoftJson(x=>x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            

            //dbContext
            services.AddDbContext<TaskContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SqlServer")));
            //Repos
            services.AddTransient<IRepo<Employees>,GenericRepo<Employees>>();
            services.AddTransient<IRepo<Loans>,GenericRepo<Loans>>();
            services.AddTransient<IRepo<LoanTypes> ,GenericRepo<LoanTypes>>();
            //BO's
            services.AddTransient<EmployeeBO>();
            services.AddTransient<LoanBO>();
            services.AddTransient<LoanTypeBO>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app ,IWebHostEnvironment env) {
            if(env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
            
        }
    }
}
