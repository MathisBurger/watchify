using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using watchify.Modules;
using watchify.Services;
using watchify.Shared;

namespace watchify;

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
            var jwtSigningKey = Configuration.GetValue<string>("Authorization:JWTSigningKey");
            services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            services
                .AddSingleton<IPasswordHasher, Argon2IdHasher>()
                .AddDbContext<IContext, DatabaseContext>(ctx => 
                    ctx.UseNpgsql(Configuration.GetValue<string>("DB:connString")))
                .AddScoped<DbAccess>()
                .AddSingleton<IAuthorization>((services) => jwtSigningKey == null
                    ? new JWTAuthorization()
                    : new JWTAuthorization(jwtSigningKey))
                .AddScoped<VideoService>()
                .AddSingleton<IConfiguration>(Configuration);

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Watchify API endpoints", Version = "v1" });
            });
            services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors(cors => cors
                    .WithOrigins("https://localhost:3000")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials());
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseHttpsRedirection();
            }

            app.UseRouting();

            app.UseAuthorization();
            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger");
                options.RoutePrefix = "swagger";
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
            using (var scope = app.ApplicationServices.CreateScope())
            using (var db = scope.ServiceProvider.GetService<IContext>()!)
            {
                //db.Database.Migrate();
            }
        }
    }