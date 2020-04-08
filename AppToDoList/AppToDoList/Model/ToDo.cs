using System;
using System.Collections.Generic;
using System.Text;

namespace AppToDoList.Model
{
    public class ToDo
    {
        public string Name { get; set; }
        public DateTime EndTask { get; set; }
        public byte Priority { get; set; }
                
        private List<ToDo> List { get; set; }
        public void SaveTask(ToDo task)
        {
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
            if (App.Current.Properties.ContainsKey("Tasks"))
            {
                App.Current.Properties.Remove("Tasks");
            }
            App.Current.Properties.Add("Tasks", list);
        }
        private List<ToDo> ListProperties()
        {
            if (App.Current.Properties.ContainsKey("Tasks"))
            {
                return (List<ToDo>)App.Current.Properties["Tasks"];
            }
            return new List<ToDo>();
        }
    }
}
