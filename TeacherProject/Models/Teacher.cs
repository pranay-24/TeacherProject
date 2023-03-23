using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeacherProject.Models
{
    public class Teacher
    {
        public string fname { get; set; }  
        public string lname { get; set; }
        public int teacherid { get; set; }

        public string employeeno { get; set; }
        public string hiredate { get; set; }    
                 
        public double salary { get; set; }
    }
}