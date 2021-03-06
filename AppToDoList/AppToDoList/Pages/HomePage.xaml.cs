﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppToDoList.Model;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppToDoList.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();                                    
            NavigationPage.SetHasNavigationBar(this, false); // Hide title bar
            datetimeNow.Text = DateTime.Now.DayOfWeek.ToString() + ", " + DateTime.Now.ToString("dd/MM");
            LoadingTasks();
        }
        private void ActionGoRegisterPage (object sender, EventArgs args)
        {
            Navigation.PushAsync(new RegisterPage());
        }

        private void LoadingTasks()
        {
            sLTasks.Children.Clear();
            List<ToDo> list = new ToDo().ListAllTasks();
            int i = 0;
            foreach (ToDo x in list)
            {
                FeedingStackLayout(x, i);
                i++;
            }
        }        
        
        public void FeedingStackLayout(ToDo task, int index)
        {
            var convert = new ColorTypeConverter();
            Color colorBox = (Color)convert.ConvertFromInvariantString(task.Priority);
                        
            Image delete = new Image() { VerticalOptions = LayoutOptions.Center, Source = "trash.png" };
            TapGestureRecognizer deleteTap = new TapGestureRecognizer();
            deleteTap.Tapped += delegate
            {
                new ToDo().DeleteTask(index);
                LoadingTasks();
            };
            delete.GestureRecognizers.Add(deleteTap);
            BoxView bView = new BoxView() { CornerRadius = 13, WidthRequest = 30, BackgroundColor = colorBox };
            View stackCentral = null;
            if (task.EndTask == null)
            {
                stackCentral = new Label() { VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.FillAndExpand, Text = task.Name };
            }            
            else
            {
                stackCentral = new StackLayout() { VerticalOptions = LayoutOptions.Center, Spacing = 0, HorizontalOptions = LayoutOptions.FillAndExpand };
                ((StackLayout)stackCentral).Children.Add(new Label() { Text = task.Name, TextColor = Color.Gray });
                ((StackLayout)stackCentral).Children.Add(new Label() { Text = "Finalizado em" + task.EndTask.Value.ToString("dd/MM/yyyy - hh:mm"), TextColor = Color.Gray, FontSize = 12 });
            }
            
            Image check = new Image() { VerticalOptions = LayoutOptions.Center, Source = "checkOff.png"};
            if (task.EndTask != null)
            {
                check.Source = "checkOn.png";
            }
            TapGestureRecognizer checkTap = new TapGestureRecognizer();
            checkTap.Tapped += delegate
            {
                new ToDo().EndOfTask(index, task);
                LoadingTasks();
            };
            check.GestureRecognizers.Add(checkTap);

            StackLayout stack = new StackLayout() { Orientation = StackOrientation.Horizontal, Spacing = 15, HeightRequest = 30 };
            stack.Children.Add(check);
            stack.Children.Add(stackCentral);
            stack.Children.Add(bView);
            stack.Children.Add(delete);

            sLTasks.Children.Add(stack);
            
        }
    }
}