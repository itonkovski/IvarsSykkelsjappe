﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using IvarsSykkelsjappe.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc;
using IvarsSykkelsjappe.Infrastructure.Extensions;
using IvarsSykkelsjappe.Services.Bikes;
using IvarsSykkelsjappe.Services.Bookings;
using IvarsSykkelsjappe.Services.Assistances;
using IvarsSykkelsjappe.Services.Products;
using IvarsSykkelsjappe.Hubs;
using IvarsSykkelsjappe.Infrastructure.Photos;
using Microsoft.AspNetCore.Http;
using IvarsSykkelsjappe.Services.Messaging;

namespace IvarsSykkelsjappe
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
            services
                .AddDbContext<ApplicationDbContext>(options => options
                    .UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection")));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddMemoryCache();
            services.AddDistributedSqlServerCache(
                options =>
                {
                    options.ConnectionString = Configuration.GetConnectionString("DefaultConnection");
                    options.SchemaName = "dbo";
                    options.TableName = "CacheRecords";
                });

            services
                .AddDefaultIdentity<IdentityUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<CookiePolicyOptions>(
                options =>
                {
                    options.CheckConsentNeeded = context => true;
                    options.MinimumSameSitePolicy = SameSiteMode.None;
                });

            services.AddSignalR();
            services.AddAutoMapper(typeof(Startup));
            services.AddAuthentication().AddFacebook(option =>
            {
                option.AppId = Configuration["Facebook:AppId"];
                option.AppSecret = Configuration["Facebook:AppSecret"];
                option.AccessDeniedPath = "/AccessDeniedPathInfo";
            });
            
            services.AddControllersWithViews(options =>
            {
                options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
            });

            services.AddTransient<IEmailSender>(x => new SendGridEmailSender(Configuration["SendGrid:ApiKey"]));
            services.AddTransient<IBikeService, BikeService>();
            services.AddTransient<IBookingService, BookingService>();
            services.AddTransient<IAssistanceService, AssistanceService>();
            services.AddTransient<IProductService, ProductService>();

            services.Configure<CloudinarySettings>(Configuration.GetSection("Cloudinary"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.PrepareDatabase();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection()
                .UseStaticFiles()
                .UseRouting()
                .UseCookiePolicy()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute(
                    name: "Areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                    endpoints.MapDefaultControllerRoute();
                    endpoints.MapRazorPages();
                    endpoints.MapHub<ChatHub>("/chat"); 
                });
        }
    }
}
    