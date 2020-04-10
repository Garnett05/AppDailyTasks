using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;

namespace AppToDoList.Model
{
    public class ToDo
    {
        public string Name { get; set; }
        public DateTime? EndTask { get; set; }
        public BoxView Priority { get; set; }

        private List<ToDo> List { get; set; }
        public void SaveTask(ToDo task)
        {
            List = ListAllTasks();
            List.Add(task);
            SaveProperties(List);
        }
        public void DeleteTask(ToDo task)
        {
            List.Remove(task);
            SaveProperties(List);
        }
        public void EndOfTask(int index, ToDo task)
        {
            List = ListAllTasks();
            List.RemoveAt(index);

            List.Add(task);
            SaveProperties(List);
        }
        public List<ToDo> ListAllTasks()
        {
            return ListProperties();
        }
        private void SaveProperties(List<ToDo> list)
        {
            if (App.Current.Properties.ContainsKey("NoEsPossible"))
            {
                App.Current.Properties.Remove("NoEsPossible");
            }
            App.Current.Properties.Add("NoEsPossible", list);
        }
        private List<ToDo> ListProperties()
        {
            if (App.Current.Properties.ContainsKey("NoEsPossible"))
            {
                return (List<ToDo>)App.Current.Properties["NoEsPossible"];
            }
            return new List<ToDo>();
        }
    }
}
