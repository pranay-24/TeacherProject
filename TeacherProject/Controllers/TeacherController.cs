using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeacherProject.Models;
namespace TeacherProject.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// List of teachers and search based on name
        /// </summary>
        /// <param name="search_string">Either first name,lastname or full name</param>
        /// <returns>List of teachers</returns>
        /// <example>
        /// GET:  /Teacher/list/Linda   --> Linda Chan 60.22 8/22/2015 12:00:00 AM
        /// GET:  /Teacher/list/Dana Ford   --> Dana Ford 71.15 6/26/2014 12:00:00 AM
        /// 
        /// </example>
        //GET:  /Teacher/list/{search_string}

        public ActionResult list(string search_string)
        {
            TeacherDataController T_Data = new TeacherDataController();
          IEnumerable<Teacher> teachers =   T_Data.ListTeachers(search_string);

            return View(teachers);

        }

        /// <summary>
        /// List of teachers and search if salary more than searchstring
        /// </summary>
        /// <param name="search_string">Salary search string</param>
        /// <returns>List of teachers with salary more than that</returns>
        /// <example>
        /// GET:  /Teacher/list/70       --> Lauren Smith 74.2 6/22/2014 12:00:00 AM
        ///                                 Dana Ford 71.15 6/26/2014 12:00:00 AM
      ///                                   John Taram 79.63 10/23/2015 12:00:00 AM
        ///  
        /// </example>
        /// 
        //GET:  /Teacher/listSalary/{search_string1}
        public ActionResult listSalary(string search_string1)
        {
            TeacherDataController T_Data = new TeacherDataController();
            IEnumerable<Teacher> teachers = T_Data.ListTeachersSalary(search_string1);

            return View(teachers);


        }


        /// <summary>
        /// Show teacher with a given id
        /// </summary>
        /// <param name="id">TeacherID</param>
        /// <returns>Teacher with that specific ID</returns>
        /// <example>
        /// /Teacher/show/4       -> Employee Name : Lauren Smith
        ///                         Emplyee no: T385
        ///                         Hire date: 6/22/2014 12:00:00 AM
        ///                         Salary:74.2
        ///</example>
        // GET:  /Teacher/show/{id}
        public ActionResult show(int id)
        {
            TeacherDataController T_Data = new TeacherDataController();
            Teacher  iteacher = T_Data.Show(id);

            return View(iteacher);

        }
        
    }
}