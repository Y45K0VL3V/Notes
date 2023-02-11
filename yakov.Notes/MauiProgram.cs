using Microsoft.Extensions.DependencyInjection;
using Sharpnado.CollectionView;
using Sharpnado.Tabs;
using yakov.Notes.Domain.Interfaces;
using yakov.Notes.Application.RemoteDB;
using yakov.Notes.Application.LocalDB;
using yakov.Notes.Navigation;
using yakov.Notes.Services;
using yakov.Notes.ViewModel;
using yakov.Notes.Views;
using Microsoft.EntityFrameworkCore;

namespace yakov.Notes;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseSharpnadoCollectionView(loggerEnable: false)
			.UseSharpnadoTabs(loggerEnable: false)
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        builder.Services.AddTransient<LoginPage>();
		builder.Services.AddTransient<LoginPageVM>();
		builder.Services.AddTransient<RegisterPage>();
		builder.Services.AddTransient<RegisterPageVM>();
		builder.Services.AddTransient<MainPage>();
		builder.Services.AddTransient<MainPageVM>();
		builder.Services.AddTransient<NoteEditorPage>();
		builder.Services.AddTransient<NoteEditorPageVM>();

		builder.Services.AddSingleton<IAuthService, FirebaseAuthService>();
        builder.Services.AddSingleton<INavigationService, NavigationService>();
        builder.Services.AddScoped<INotesRemoteRepositoryControl, FirebaseDBControl>();
        builder.Services.AddScoped<INotesRepositoryControl, NotesRepositoryControl>();
        builder.Services.AddSingleton<INotesLoaderService, NotesLoaderService>();
		
        builder.Services.AddDbContext<NotesContext>(
            options => options.UseSqlite($"Filename={GetDatabasePath()}", x => x.MigrationsAssembly(nameof(yakov.Notes.Application))));

        return builder.Build();
	}

    public static string GetDatabasePath()
    {
        var databasePath = "";
        var databaseName = "yakov.Notes.db";

        if (DeviceInfo.Platform == DevicePlatform.Android)
        {
            databasePath = Path.Combine(FileSystem.AppDataDirectory, databaseName);
        }
        if (DeviceInfo.Platform == DevicePlatform.iOS)
        {
            SQLitePCL.Batteries_V2.Init();
            databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "..", "Library", databaseName); ;
        }

        return databasePath;

    }
}
