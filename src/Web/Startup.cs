using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using FluentValidation.AspNetCore;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Web.Interfaces;
using Web.Middlewares;
using Web.Services;

namespace Web
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
            services.AddControllersWithViews().AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<Startup>());
            services.AddDbContext<GoloIdentityDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("GoloIdentityDbContext")));
            services.AddDbContext<GoloContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("GoloContext")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<GoloIdentityDbContext>();
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IFilterViewModelService, FilterViewModelService>();
            services.AddScoped<IHomeViewModelService, HomeViewModelService>();
            services.AddScoped<IProductDetailsViewModelService, ProductDetailsViewModelService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<ICartViewModelService, CartViewModelService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IGenreViewModelService, GenreViewModelService>();
            services.AddScoped<IPlatformService, PlatformService>();
            services.AddScoped<IPlatformViewModelService, PlatformViewModelService>();
            services.AddScoped<IGameService, GameService>();
            services.AddScoped<IGameViewModelService, GameViewModelService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductViewModelService, ProductViewModelService>();
            services.AddScoped<IDiscountService, DiscountService>();
            services.AddScoped<IDiscountViewModelService, DiscountViewModelService>();
            services.AddScoped<IKeyService, KeyService>();
            services.AddScoped<IKeyViewModelService, KeyViewModelService>();

            services.AddRazorPages();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var supportedCultures = new[] { "en-US" };
            var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);

            app.UseRequestLocalization(localizationOptions);

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
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCartTransfer();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
                );
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
