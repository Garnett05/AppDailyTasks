using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using AppToDoList.Model;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppToDoList.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        private BoxView bxView { get; set; }
        private bool action { get; set; }
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
            BoxView source = ((BoxView)((StackLayout)sender).Children[0]);
            bxView = new BoxView();
            bxView.BackgroundColor = source.BackgroundColor;
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
                task.Priority = bxView;

                new ToDo().SaveTask(task);

                txtName.Text = new ToDo().ListAllTasks().Count.ToString();

                App.Current.MainPage = new NavigationPage(new HomePage());
            }
        }
    }
}