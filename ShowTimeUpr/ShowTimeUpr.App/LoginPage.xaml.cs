using Android.Content.Res;
using ShowTimeUpr.DataAccess;

namespace ShowTimeUpr.App;

public partial class LoginPage(IUserRepo repo) : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    public async void OnRegisterBtnClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegisterPage());
    }

    public async void OnLoginBtnClicked(object sender, EventArgs e)
    {
        var username = UsernameEntry.Text;
        var pass = PasswordEntry.Text;

        if (username == null || pass == null)
        {
            await DisplayAlert("Error", "All fields are required!", "Ok");
        }

        var user = await repo.Login(username, pass);
        if (user != null)
        {
            await SecureStorage.SetAsync("username",user.Username);
            await DisplayAlert("Success", "You have been logged", "Ok");

            await Navigation.PushAsync(new MainPage());
        }
    }

}