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
        builder.Services.AddSingleton<INotesRemoteRepositoryControl, FirebaseDBControl>();
        builder.Services.AddSingleton<INotesRepositoryControl, NotesRepositoryControl>();
        builder.Services.AddSingleton<INotesLoaderService, NotesLoaderService>();
		

        return builder.Build();
	}
}
