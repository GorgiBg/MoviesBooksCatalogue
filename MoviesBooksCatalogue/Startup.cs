using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MoviesBooksCatalogue.Data;
using MoviesBooksCatalogue.Models;
using MoviesBooksCatalogue.Services;

namespace MoviesBooksCatalogue
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MoviesAndBooksCatalogue", Version = "v1" });
            });

            // Register the IRepository<T> with its implementation
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            // Register the IBookService with its implementation
            services.AddScoped<IBookService, BookService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MoviesAndBooksCatalogue v1"));
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            // Ensure the database is created and seeded
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                context.Database.EnsureCreated();
                SeedDatabase(context);
            }
        }

        public static void SeedDatabase(AppDbContext context)
        {
            if (!context.Books.Any())
            {
                var books = new List<Book>
                {
                    new Book { Title = "Book 1", Genre = "Fiction", ReleaseDate = new DateTime(2020, 1, 1), Rating = 4.5 },
                    new Book { Title = "Book 2", Genre = "Non-Fiction", ReleaseDate = new DateTime(2021, 3, 15), Rating = 4.8 },
                };

                context.Books.AddRange(books);
                context.SaveChanges();
            }
        }
    }
}