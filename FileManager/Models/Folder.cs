using System;
using System.Collections.Generic;
using System.Text;

namespace FileManager.Models
{
    public class Folder
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string CountItem { get; set; }
        public bool OverView { get; set; }
        public bool OpenMenu { get; set; }
        public string DataCreated { get; set; }
        public bool IsDeleted { get; set; }

    }
}
