using Sharpnado.CollectionView;
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

		return builder.Build();
	}
}
