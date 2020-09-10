using System;
using System.IO;
using System.Text;
using AutoMapper;
using Lottery.Data;
using Lottery.Entities;
using Lottery.Hubs;
using Lottery.Repositories;
using Lottery.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace Lottery
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
            services.AddControllers();

            services.Configure<MvcOptions>(options =>
            {
                options.MaxValidationDepth = 5000;
            });

            services.AddSignalR();

            #region DbContext

            services.AddDbContext<ApplicationDbContext>(option =>
            {
                option.UseSqlite(Configuration.GetConnectionString("DefaultConnection"));
            });

            #endregion

            #region Identity
            
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
                options.User.AllowedUserNameCharacters = @"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+#$%\/()[]*&:><^!{}";
                options.User.RequireUniqueEmail = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
                options.Lockout.MaxFailedAccessAttempts = 20;
                options.Lockout.AllowedForNewUsers = true;
            });

            #endregion

            #region Authentication
            
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = Configuration["JwtSettings:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = Configuration["JwtSettings:Audience"],
                        ValidateIssuerSigningKey = false,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSettings:Key"]))
                    };
                });
            
            #endregion

            #region Cors

            services.AddCors(options =>
            {
                options.AddPolicy("api", policy =>
                {
                    policy.WithOrigins(Configuration["FrontendUrl"])
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });

            #endregion

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<IAttendeeRepository, AttendeeRepository>();
            services.AddScoped<IWinnerRepository, WinnerRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseForwardedHeaders(new ForwardedHeadersOptions
                {
                    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
                });
                
                FileExtensionContentTypeProvider provider = new FileExtensionContentTypeProvider();
                provider.Mappings[".webmanifest"] = "application/manifest+json";
                
                app.Use(async (context, next) =>
                {
                    await next();
                    if (context.Response.StatusCode == 404 && !Path.HasExtension(context.Request.Path.Value))
                    {
                        context.Request.Path = "/index.html";
                        context.Response.StatusCode = 200;
                        await next();
                    }
                });
                
                app.UseDefaultFiles();
                app.UseStaticFiles(new StaticFileOptions()
                {
                    ContentTypeProvider = provider
                });
            }

            app.UseRouting();

            app.UseCors("api");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<LotteryHub>("/api/lottery");
            });
        }
    }
}
