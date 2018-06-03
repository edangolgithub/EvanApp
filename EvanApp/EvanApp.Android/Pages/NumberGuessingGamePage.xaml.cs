using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EvanApp.Droid.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NumberGuessingGamePage : ContentPage
    {
        int num;
        int chance = 7;
        int memory = 67;
        Random r;
        public NumberGuessingGamePage()
        {
            InitializeComponent();
            Submitbtn.Clicked += Submitbtn_Clicked;
            restartbtn.Clicked += Restartbtn_Clicked;
            r = new Random();
            int value = r.Next(1, 100);
            memory = value;
           // Entry entry = new Entry { Keyboard = Keyboard.Numeric };
            numtxt.Keyboard = Keyboard.Numeric;
           // numtxt.Completed += Numtxt_Completed;
          
        }

        //private void Numtxt_Completed(object sender, EventArgs e)
        //{
        //    Submitbtn.Focus();
        //    Submitbtn.Command.Execute(null);
        //}

        private void Restartbtn_Clicked(object sender, EventArgs e)
        {
            Restart();
        }

        private void Restart()
        {
            r = new Random();
            int value = r.Next(1, 100);
            memory = value;
            infotxt.Text = "";
            numtxt.Text = "";
            kk.Text = "";
            chance = 7;
        }

        private void Submitbtn_Clicked(object sender, EventArgs e)
        {
            infotxt.Text = "";
            if (numtxt.Text.Length < 1)
            {
                DisplayAlert("Warning", "You did not enter any thing", "ok");
                return;
            }
            
            if (!int.TryParse(numtxt.Text, out num))
            {
                DisplayAlert("Error", "That doesnt look like a number", "ok");
                return;
            }


            chance--;

            kk.Text = "You entered " + numtxt.Text;
            if (num < memory)
            {
                infotxt.Text = "Too Low" + " you have " + (chance) + " chance(s) left";
            }
            else if (num > memory)
            {
                infotxt.Text = "Too High" + " you have " + (chance) + " chance(s) left";
            }
            else
            {
                infotxt.Text = "Correct answer"
                  + "\nyou are very good player \n congratulation!!!!!";

            }
            
            numtxt.Text = "";
            if (chance <= 0&&num!=memory)
            {
                DisplayAlert("Looser", "you are not good player \nyou lost!!!!!" +
                   "\nthe number was " + memory, "Ok");
                Restart();
                return;
            }




        }
    }
}