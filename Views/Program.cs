using System;
using DAL;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Model.Services;
using Model.Services.ViewService;
using TestMVVM.Windows;
using ViewModels.ViewModel;

namespace TestMVVM;

public static class Program
{
    [STAThread]
    public static void Main()
    {
        var host = Host.CreateDefaultBuilder()
            .ConfigureHostConfiguration(op => { op.AddJsonFile("AppSettings.json"); })
            .ConfigureServices((context, services) =>
            {
                services.AddSingleton<App>();
                services.AddSingleton<MainWindow>();
                services.AddSingleton<IViewsLoader, ViewLoader>();
                // services.AddSingleton<LoginWindow>();
                // services.AddSingleton<LoginViewModel>();

                services.AddSingleton<IUserService, UserService>();

                services.AddSingleton<LoginWindow>();
                services.AddSingleton<LoginVm>();

                services.AddSingleton<RegistrationWindow>();
                services.AddSingleton<RegistrationVm>();

                services.AddSingleton<MainMenuWindow>();
                services.AddSingleton<MainMenuVm>();

                services.AddSingleton<QuizWindow>();
                services.AddSingleton<QuizVm>();

                services.AddSingleton<ResultWindow>();
                services.AddSingleton<ResultVm>();

                services.AddSingleton<TopicsRepository>();
                services.AddSingleton<QuestionsRepository>();
                services.AddSingleton<QuizEngine>();

                // Тут commit в ветку NewBranch

                services.AddDbContextFactory<QuizDb>(op =>
                {
                    var connectionString = context.Configuration.GetConnectionString("QuizDb");
                    op.UseNpgsql(connectionString);
                });
                services.AddDbContext<QuizDb>(op =>
                {
                    var connectionString = context.Configuration.GetConnectionString("QuizDb");
                    op.UseNpgsql(connectionString);
                });
            })
            .Build();

        var app = host.Services.GetService<App>();
        app?.Run();
    }
}