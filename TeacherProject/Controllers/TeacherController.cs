using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
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

        public ActionResult list(string search_string = null)
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

        /// <summary>
        /// Form to create a new entry of teacher in the list
        /// </summary>
        /// <param ></param>
        /// <returns></returns>
        /// <example>
        /// /Teacher/new         -> Form to create a new teacher entry
        ///</example>
        // GET:  /Teacher/new
        public ActionResult New()
        {
            return View();
        
        }


        /// <summary>
        /// Add teacher with the name, employe no., hiredate,salary
        /// </summary>
        /// <param name={fname,lname,employeeno,hiredate,salaray}>First name,Last name, employee no., 
        /// hiredate, salary</param>
        /// <returns></returns>
        /// <example>
        /// /Teacher/create     
        /// 
        ///</example>
        // GET:  /Teacher/create/

        [HttpPost]
        public ActionResult create(string fname, string lname, string employeeno, string hiredate,string salary)
        {
            TeacherDataController T_Data = new TeacherDataController();
            Teacher newTeacher = new Teacher();
            
            newTeacher.fname = fname;
            newTeacher.lname = lname;
            newTeacher.employeeno = employeeno;
            newTeacher.salary = Convert.ToDouble(salary);
            newTeacher.hiredate = hiredate; 

            T_Data.Create(newTeacher);

            return RedirectToAction("list");

        }

        /// <summary>
        /// Deletes a teacher with a given id 
        /// </summary>
        /// <param name="id">Teacher ID</param>
        /// <returns> Redirects to list page</returns>
        /// <example>
        /// /Teacher/delete/3       ->    list of teachers 
        /// 
        /// 
        ///</example>
        // GET:  /Teacher/delete/3

        [HttpPost]
        public ActionResult delete(int id)
        {
            TeacherDataController T_data = new TeacherDataController();
            T_data.Delete(id);
            return RedirectToAction("list");
        }


        public ActionResult add_ajax()
        {
           
            return View();
        }

        /// <summary>
        /// Displays the teacher with a given id 
        /// </summary>
        /// <param name="id">Teacher ID</param>
        /// <returns> teacher with the given id</returns>
        /// <example>
        /// /Teacher/delete_confirm/3       ->    Employee Name : Lauren Smith
        ///                                       Emplyee no: T385
        ///                                       Hire date: 6/22/2014 12:00:00 AM
        ///                                       Salary:74.2  
        /// 
        ///</example>
        // GET:  /Teacher/delete_confirm/3


        public ActionResult delete_confirm(int id)
        {   TeacherDataController T_data = new TeacherDataController();
            Teacher teacher = new Teacher();
            teacher = T_data.Show(id);
           
            return View(teacher);
        }


        /// <summary>
        /// Show teacher with a given id
        /// </summary>
        /// <param name="id">TeacherID</param>
        /// <returns>Teacher with that specific ID</returns>
        /// <example>
        /// /Teacher/update/4       -> Employee Name : Lauren Smith
        ///                         Emplyee no: T385
        ///                         Hire date: 6/22/2014 12:00:00 AM
        ///                         Salary:74.2
        ///</example>
        ///
        //GET://Teacher/update/{id}
        [HttpGet]
        public ActionResult update(int id)
        {
            TeacherDataController T_Data = new TeacherDataController();
            Teacher selectedTeacher = T_Data.Show(id);
          

            return View(selectedTeacher);
        }

        /// <summary>
        /// Add teacher with the name, employe no., hiredate,salary
        /// </summary>
        /// <param name={fname,lname,employeeno,hiredate,salaray}>First name,Last name, employee no., 
        /// hiredate, salary</param>
        /// <returns></returns>
        /// <example>
        /// /Teacher/update/id    
        /// Form Data / POST DATA
        /// {
        /// "fname":"Pranay",
        /// "lname":"Mangal",
        /// 
        /// "employeeno":"m100",
        /// "hiredate":"2023/02/12",
        /// "salary":"98.5"
        /// }
        ///</example>
        ///
        //POST://Teacher/update/{id}
        [HttpPost]
        public ActionResult update(int id, string fname, string lname, string employeeno, string hiredate, string salary)
        {
            TeacherDataController T_Data = new TeacherDataController();
            Teacher newTeacher = new Teacher();

            newTeacher.fname = fname;
            newTeacher.lname = lname;
            newTeacher.employeeno = employeeno;
            newTeacher.salary = Convert.ToDouble(salary);
            newTeacher.hiredate = hiredate;

            T_Data.Update(id,newTeacher);


            return RedirectToAction("show/"+id);
        }

    }
}