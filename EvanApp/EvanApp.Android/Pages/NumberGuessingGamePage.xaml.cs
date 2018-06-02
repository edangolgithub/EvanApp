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
            r = new Random();
            int value = r.Next(1, 100);
            memory = value;
            infotxt.Text = "";
            chance = 8;
        }

        private void Submitbtn_Clicked(object sender, EventArgs e)
        {
            if (numtxt.Text.Length < 1)
            {
                DisplayAlert("Warning", "You did not enter any thing", "ok");
                return;
            }
            infotxt.Text = "";
            if (!int.TryParse(numtxt.Text, out num))
            {
                DisplayAlert("error", "That doesnt look like a number", "ok");
                return;
            }


            if (chance <= 1)
            {
                infotxt.Text = "you are not good player \nyou lost!!!!!" +
                 "\nthe number was " + memory;
                return;
            }

            chance--;

            if (num < memory)
            {
                infotxt.Text = "too low" + " you have " + (chance) + " chance left";
            }
            else if (num > memory)
            {
                infotxt.Text = "too high" + " you have " + (chance) + " chance left";
            }
            else
            {
                infotxt.Text = "correct answer"
                  + "\nyou are very good player \n congratulation!!!!!";

            }

            numtxt.Text = "";

         

        }
    }
}