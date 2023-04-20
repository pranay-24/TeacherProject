using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using Google.Protobuf;
using MySql.Data.MySqlClient;
using TeacherProject.Models;
using static Mysqlx.Expect.Open.Types.Condition.Types;


namespace TeacherProject.Controllers
{
    public class TeacherDataController : ApiController
    {
       private  SchoolDbContext School = new SchoolDbContext();

        /// <summary>
        /// Method to list all teachers from the school database 
        /// </summary>
        /// <param name="SearchKey">For searching name or salary or hire date</param>
        /// <returns>list of teachers</returns>
        /// <example>
        /// http://localhost:62808/api/TeacherData/ListTeachers/?SearchKey=Linda
        /// <ListofTeachers>
        ///     <employeeno>T382</employeeno>
        ///     <fname>Linda</fname>
        ///     <hiredate>8/22/2015 12:00:00 AM</hiredate>
        ///     <lname>Chan</lname>
        ///     <salary>60.22</salary>
        ///     <teacherid>3</teacherid>
        /// </ListofTeachers>
        /// </example>
        /// 
        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{SearchKey?}")]
        public List<Teacher> ListTeachers(string SearchKey=null)
        {
            MySqlConnection conn = School.AccessDatabase();

            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = "SELECT * FROM teachers " +
                "WHERE lower(teacherfname) LIKE lower(@key) " +
                "OR lower(teacherlname) LIKE lower(@key) " +
                "OR lower(concat (teacherfname, ' ', teacherlname) )LIKE lower(@key)" +
                "OR hiredate like @key"; 

            cmd.Parameters.AddWithValue("@key","%"+ SearchKey+"%");
          

            List<Teacher> teachs = new List<Teacher>();

            MySqlDataReader ResultSet = cmd.ExecuteReader();    
            while (ResultSet.Read())
            {
                Teacher teach = new Teacher();
                teach.teacherid = (int)ResultSet["teacherid"];
                teach.fname = ResultSet["teacherfname"].ToString();
                teach.lname = ResultSet["teacherlname"].ToString();
                teach.employeeno = ResultSet["employeenumber"].ToString();
               teach.hiredate =ResultSet["hiredate"].ToString();
                teach.salary = Convert.ToDouble(ResultSet["salary"]);
               
                teachs.Add(teach);

            }

            conn.Close();
            return teachs;
        }

        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{SearchKey1?}")]
        public List<Teacher> ListTeachersSalary( string SearchKey1 = null)
        {
            MySqlConnection conn = School.AccessDatabase();

            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = "SELECT * FROM teachers " +
                "WHERE salary > @key1 ";
                

            
            cmd.Parameters.AddWithValue("@key1", Convert.ToInt32(SearchKey1));

            List<Teacher> teachs = new List<Teacher>();

            MySqlDataReader ResultSet = cmd.ExecuteReader();
            while (ResultSet.Read())
            {
                Teacher teach = new Teacher();
                teach.teacherid = (int)ResultSet["teacherid"];
                teach.fname = ResultSet["teacherfname"].ToString();
                teach.lname = ResultSet["teacherlname"].ToString();
                teach.employeeno = ResultSet["employeenumber"].ToString();
               teach.hiredate = ResultSet["hiredate"].ToString();
                teach.salary = Convert.ToDouble(ResultSet["salary"]);

                teachs.Add(teach);

            }

            conn.Close();
            return teachs;
        }


        /// <summary>
        /// Method to output a  teacher based on id
        /// 
        /// </summary>
        /// <param name="id">Teacherid</param>
        /// <returns>Returns a teacher with the given ID</returns>
        /// <example>
        /// http://localhost:62808/api/TeacherData/Show/2
        /// 
        /// <employeeno>T381</employeeno>
        /// <fname>Caitlin</fname>
        /// <hiredate>6/10/2014 12:00:00 AM</hiredate>
        /// <lname>Cummings</lname>
        ///  <salary>62.77</salary>
        /// <teacherid>2</teacherid>

        /// </example>
        [HttpGet]
        [Route("api/TeacherData/Show/{id}")]
        public Teacher Show(int id)
        {
            MySqlConnection conn = School.AccessDatabase();

            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = "SELECT * FROM teachers WHERE teacherid = "+ id;

            Teacher newteacher = new Teacher();

            MySqlDataReader ResultSet = cmd.ExecuteReader();
            while (ResultSet.Read())
            {
                newteacher.teacherid = (int)ResultSet["teacherid"];
                newteacher.fname = ResultSet["teacherfname"].ToString();
                newteacher.lname = ResultSet["teacherlname"].ToString();
                newteacher.employeeno = ResultSet["employeenumber"].ToString();
                newteacher.hiredate =ResultSet["hiredate"].ToString();
               newteacher.salary = Convert.ToDouble(ResultSet["salary"]);
            }
            conn.Close();
            return newteacher;
        }


        [HttpPost]
        [EnableCors(origins: "*", methods: "*", headers: "*")] 
      
       /* [Route("api/TeacherData/Create")] */

        public void Create([FromBody]Teacher newteacher)
        {
            MySqlConnection conn = School.AccessDatabase();

            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();

            /*Console.WriteLine(newteacher);*/
            System.Diagnostics.Debug.WriteLine(newteacher.fname);

            cmd.CommandText = "INSERT INTO teachers(teacherfname,teacherlname,employeenumber" +
               ",salary) VALUES (@teacherfname,@teacherlname,@employeenumber,@salary)";

            string format = "yyyy-MM-dd ";
            DateTime dateTime2 = DateTime.Parse(newteacher.hiredate); 

            cmd.Parameters.AddWithValue("@teacherfname",newteacher.fname);
            cmd.Parameters.AddWithValue("@teacherlname", newteacher.lname);
            cmd.Parameters.AddWithValue("@employeenumber", newteacher.employeeno);
           cmd.Parameters.AddWithValue("@hiredate", dateTime2); 
            cmd.Parameters.AddWithValue("@salary", newteacher.salary);
            
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            
            conn.Close();
            ;
        }


        
        [HttpPost]
        
        [Route("api/TeacherData/Delete/{id}")]
        public void Delete(int id)
        {
            MySqlConnection conn = School.AccessDatabase();

            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = "DELETE FROM teachers WHERE teacherid = @Teacherid ";

           cmd.Parameters.AddWithValue("@teacherid", id);
            cmd.Prepare();
           cmd.ExecuteNonQuery();

            conn.Close();
            
        }



        public void Update(int id, [FromBody] Teacher newteacher)
        {
            MySqlConnection conn = School.AccessDatabase();

            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();

            /*Console.WriteLine(newteacher);*/
            System.Diagnostics.Debug.WriteLine(newteacher.fname);

            cmd.CommandText = "UPDATE teachers set teacherfname=@teacherfname,teacherlname=@teacherlname," +
                "employeenumber=@employeenumber,salary=@salary  where teacherid = @teacherID";

            string format = "yyyy-MM-dd ";
            DateTime dateTime2 = DateTime.Parse(newteacher.hiredate);

            cmd.Parameters.AddWithValue("@teacherfname", newteacher.fname);
            cmd.Parameters.AddWithValue("@teacherlname", newteacher.lname);
            cmd.Parameters.AddWithValue("@employeenumber", newteacher.employeeno);
            cmd.Parameters.AddWithValue("@hiredate", dateTime2);
            cmd.Parameters.AddWithValue("@salary", newteacher.salary);
            cmd.Parameters.AddWithValue("@teacherID", id);

            cmd.Prepare();
            cmd.ExecuteNonQuery();

            conn.Close();
        }


    }

}
