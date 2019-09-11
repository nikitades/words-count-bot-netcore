using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using WordsCountBot.Contracts;
using WordsCountBot.Database;
using WordsCountBot.Extensions;
using WordsCountBot.TelegramBot;

namespace WordsCountBot
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
                .AddMvc()
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddDbContext<WordsCountBotDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")),
                ServiceLifetime.Transient
            );
            services.AddRepositories();
            services.Configure<WcbTelegramBotConfig>(Configuration.GetSection("BotConfiguration"));
            services.AddTransient<ITelegramBot, WcbTelegramBot>();
            services.AddSingleton<WcbTelegramClient>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
