using System;
using System.Collections.Generic;
using System.Text;

namespace TodoList.ViewModels
{
    public class Item
    {
        public int id { get; set; } = -1;
        public string text { get; set; } = "";
        public string createdDate { get; set; } = "";
        public string description { get; set; } = "";
        public bool isDone { get; set; } = false;
        public string doneDate { get; set; } = "";
        public int priority { get; set; } = -1;
    }
}
