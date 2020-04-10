using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using AppToDoList.Model;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace AppToDoList.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        private BoxView bxView { get; set; }
        private bool action { get; set; }
        private string stgBoxView { get; set; }
        public RegisterPage()
        {
            InitializeComponent();
        }
        public void ActionPrioritySelected (object sender, EventArgs args)
        {
            var stacks = sLPriorities.Children;
            foreach (var label in stacks)
            {
                Label slPriorities = ((StackLayout)label).Children[1] as Label;
                slPriorities.TextColor = Color.Gray;
            }

            ((Label)((StackLayout)sender).Children[1]).TextColor = Color.Black;
            string ahtomanocu = ((Label)((StackLayout)sender).Children[1]).Text;
            if (ahtomanocu == "Urgent and important")
            {
                stgBoxView = "Red";
            }
            else if (ahtomanocu == "Urgent but not important")
            {
                stgBoxView = "Orange";
            }
            else if (ahtomanocu == "Not urgent but important")
            {
                stgBoxView = "Yellow";
            }
            else
            {
                stgBoxView = "Green";
            }                        
            action = true;
        }
        public void ActionSave (object sender, EventArgs args)
        {
            bool error = false;
            if (txtName.Text == null || txtName.Text.Trim().Length <= 0)
            {
                error = true;
                DisplayAlert("Error", "Empty task name", "Ok");
            }
            
            if (action != true)
            {
                error = true;
                DisplayAlert("Error", "Priority not informed", "Ok");
            }
            if(error == false)
            {
                ToDo task = new ToDo();
                task.Name = txtName.Text.Trim();
                task.Priority = stgBoxView;

                new ToDo().SaveTask(task);

                txtName.Text = new ToDo().ListAllTasks().Count.ToString();

                App.Current.MainPage = new NavigationPage(new HomePage());
            }
        }
    }
}