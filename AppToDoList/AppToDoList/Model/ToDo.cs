using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

using Xamarin.Forms;

namespace AppToDoList.Model
{
    public class ToDo
    {
        public string Name { get; set; }
        public DateTime? EndTask { get; set; }        
        public string Priority { get; set; }

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
        private void SaveProperties(List<ToDo> List)
        {
            if (App.Current.Properties.ContainsKey("NewTasks"))
            {
                App.Current.Properties.Remove("NewTasks");
            }
            string jsonContent = JsonConvert.SerializeObject(List);
            App.Current.Properties.Add("NewTasks", jsonContent);
        }
        private List<ToDo> ListProperties()
        {            
            if (App.Current.Properties.ContainsKey("NewTasks"))
            {
                string jsonContent =(string)App.Current.Properties["NewTasks"];
                List<ToDo> list = JsonConvert.DeserializeObject<List<ToDo>>(jsonContent);
                return list;
                //return (List<ToDo>)App.Current.Properties["NoEsPossible"];
            }
            return new List<ToDo>();
        }
    }
}