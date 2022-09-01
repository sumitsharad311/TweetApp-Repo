using TWEETApp.Repository;
using TWEETApp.Databasesettings;
using TWEETApp.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace TWEETApp
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
            services.Configure<TweetsDatabaseSettings>(
                Configuration.GetSection(nameof(TweetsDatabaseSettings)));

            services.AddSingleton<ITweetsDatabasesettings>(sp =>
                sp.GetRequiredService<IOptions<TweetsDatabaseSettings>>().Value);

            services.AddScoped<ITweetsRepository, TweetsRepository>();
            services.AddScoped<ITweetService, TweetService>();

            services.Configure<UsersDatabaseSettings>(
                Configuration.GetSection(nameof(UsersDatabaseSettings)));

            services.AddSingleton<IUsersDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<UsersDatabaseSettings>>().Value);

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserServices, UserServices>();


            services.AddControllers();
            services.AddCors();
            services.AddSwaggerGen();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors(builder => {
                builder.AllowAnyOrigin();
                builder.AllowAnyMethod();
                builder.AllowAnyHeader();
            });
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Test1 Api v1");
            });
        }
    }
}
