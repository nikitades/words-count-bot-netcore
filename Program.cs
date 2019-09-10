using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace WordsCountBot
{
    /**
    V Сделать инициализацию БД
    V Сделать миграции
    V Сделать пробный селект
    V Сделать модели, прибавляющие 1 использование, assureChatAdded, assureWordAdded и проч
    Мутить телеграм-клиент с командами
        V сделать хендлер обращений
        V сделать установку вебхука
        V сделать обработчик просто входящих сообщений
        V почему-то не ставится вебхук
        V сделать обработчик команды Count
        V придумать как не мапить WordText, либо как забирать заполненный Word через RawSql
        - тесты
     */
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
