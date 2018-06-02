using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EvanApp
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
            GameBtn.Clicked += GameBtn_Clicked;
            chatbtn.Clicked += Chatbtn_Clicked;
		}

        private void Chatbtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Droid.Pages.Chat.ChatClient());
        }

        private void GameBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Droid.Pages.NumberGuessingGamePage());
        }
    }
}
