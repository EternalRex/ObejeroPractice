using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.IO;

namespace ObejeroPractice.Controllers
{
    public class HomeController : Controller
    {
        string connDB = WebConfigurationManager.ConnectionStrings["connDB"].ConnectionString;
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

       
        public ActionResult HomePage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult HomePage(FormCollection collection)
        {
            /*This is where the firstname value from the form is stored*/
            var firstname = Convert.ToString(collection["fnameData"]);
            /*This is where the last name is stored*/
            var lastname = Convert.ToString(collection["lnameData"]);
            /*This is where the age will be stored*/
            var age = Convert.ToString(collection["ageData"]);

            /*Declaring a variable name to represent the database*/
            using (var database = new SqlConnection(connDB))
            {
                /*This opens the database*/
                database.Open();
                /*This creates a variable to be used in order to give an sql command*/
                using(var command = database.CreateCommand())
                {
                    /*This initiates the command type as a text command or an sql query*/
                    command.CommandType = System.Data.CommandType.Text;
                    /*This is where the sql query statement be put*/
                    command.CommandText = "INSERT INTO PEROSNALINFO (fname,lname,age)" +
                        "VALUES (@fname,@lname,@age)";
                    /*This is the process of storing the data to the database*/
                    command.Parameters.AddWithValue("@fname",firstname);
                    command.Parameters.AddWithValue("@lname", lastname);
                    command.Parameters.AddWithValue("@age", age);

                    /*We will create a varible that will check if the data is inserted to
                     the databse successfully*/
                    var control = command.ExecuteNonQuery();

                    /*Use an if statement to check if the data is inserted to the database*/
                    if(control >= 0)
                    {
                        /*This is an alert statement to confirm database data insertion*/
                        Response.Write("<script>alert('Data is Saved!')</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('Data is NOT Saved!')</script>");
                    }
                }
            }
            return View();
        }

        /*This is for the jquery type of saving data*/
        public ActionResult SaveData1()
        {
          var  data = new List<Object>();

            var fname = Request["firstname"];
            var lname = Request["lastname"];
            var age = Request["age1"];
         
            using (var database = new SqlConnection(connDB))
            {
                database.Open();
                using(var command = database.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "INSERT INTO PEROSNALINFO (fname,lname,age)" +
                        "VALUES (@fname,@lname,@age)";
                    command.Parameters.AddWithValue("@fname",fname);
                    command.Parameters.AddWithValue("@lname",lname);
                    command.Parameters.AddWithValue("@age",age);

                    var control = command.ExecuteNonQuery();

                    if(control >= 0)
                    {
                        data.Add(new { value = 1 });
                    }
                    else
                    {
                        data.Add(new { value = 2 });
                    }    
                }
            }

            return Json(data,JsonRequestBehavior.AllowGet);
        }
    }
}