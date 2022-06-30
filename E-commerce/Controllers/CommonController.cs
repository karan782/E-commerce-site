using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;
using E_commerce.Models;
namespace E_commerce.Controllers
{
    public class CommonController : Controller
    {
        SqlConnection sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        public ActionResult Home()
        {
            return View();
        }

        public ActionResult AboutUs()
        {
            return View();
        }

        public ActionResult ProductList()
        {
            SqlConnection sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;

            List<ProductMaster> ProductMasters = new List<ProductMaster>();
            try
            {
                cmd.Connection = sqlCon;
                cmd.CommandText = "SelectAllProducts";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if (sqlCon.State == System.Data.ConnectionState.Closed)
                {
                    sqlCon.Open();
                }
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ProductMaster ProductMaster = new ProductMaster();
                        ProductMaster.ProductId = Convert.ToInt32(dr["ProductId"]);
                        ProductMaster.ProductName = Convert.ToString(dr["ProductName"]);
                        ProductMaster.Brand = Convert.ToString(dr["Brand"]);
                        ProductMaster.Colour = Convert.ToString(dr["Colour"]);
                        ProductMaster.RAM_Size = Convert.ToString(dr["RAM_Size"]);
                        ProductMaster.Hard_Drive_Size = Convert.ToString(dr["Hard_Drive_Size"]);
                        ProductMaster.Processor_Type = Convert.ToString(dr["Processor_Type"]);
                        ProductMaster.MRP = Convert.ToString(dr["MRP"]);

                        ProductMasters.Add(ProductMaster);
                    }
                    dr.Close();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                sqlCon.Close();
            }
            return View(ProductMasters);
        }

        public ActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ValidateAdmin(UserInfo userInfo)
        {
            if (userInfo.EmailId == "admin@gmail.com" && userInfo.Password == "admin123")
            {
                return RedirectToRoute(new
                {
                    controller = "Admin",
                    action = "AdminProfile",
                });
            }
            else
            {
                ViewBag.error = "Incorrect Email ID or Password";
                return View("AdminLogin");
            }
        }
        public ActionResult UserLogin()
        {
            return View();
        }

        public ActionResult ValidateUser(UserInfo userInfo)
        {
            try
            {
                cmd.Connection = sqlCon;
                cmd.CommandText = "checkuser";
                cmd.Parameters.Add("@Email", System.Data.SqlDbType.VarChar,100).Value = userInfo.EmailId;
                cmd.Parameters.Add("@Password", System.Data.SqlDbType.VarChar, 15).Value = userInfo.Password;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if (sqlCon.State == System.Data.ConnectionState.Closed)
                {
                    sqlCon.Open();
                }
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    return RedirectToRoute(new
                    {
                        controller = "User",
                        action = "ProductList"
                    });
                }
                else
                {
                    ViewBag.error = "Incorrect Email ID or Password";
                    return View("UserLogin");
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                dr.Close();
                sqlCon.Close();
            }
            return View("UserLogin");
        }

        [HttpGet]
        public ActionResult NewUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewUser(UserInfo userInfo)
        {
            try
            {
                cmd.Connection = sqlCon;
                cmd.CommandText = "adduser";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@CustName", System.Data.SqlDbType.VarChar, 50).Value = Convert.ToString(userInfo.CustName);
                cmd.Parameters.Add("@Email", System.Data.SqlDbType.VarChar, 100).Value = Convert.ToString(userInfo.EmailId);
                cmd.Parameters.Add("@Zipcode", System.Data.SqlDbType.Int).Value = userInfo.Zipcode;
                cmd.Parameters.Add("@country", System.Data.SqlDbType.VarChar, 50).Value = Convert.ToString(userInfo.country);
                cmd.Parameters.Add("@Password", System.Data.SqlDbType.VarChar, 15).Value = Convert.ToString(userInfo.Password);


                if (sqlCon.State == System.Data.ConnectionState.Closed)
                {
                    sqlCon.Open();
                }
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
            }
            finally
            {
                sqlCon.Close();
            }
            return RedirectToAction("ProductList");
        }
    }
}



//cmd.Parameters.Add("@CustName", System.Data.SqlDbType.VarChar, 50).Value = userInfo.CustName;
//cmd.Parameters.Add("@Email", System.Data.SqlDbType.VarChar, 100).Value = userInfo.EmailId;
//cmd.Parameters.Add("@Zipcode", System.Data.SqlDbType.Int).Value = userInfo.Zipcode;
//cmd.Parameters.Add("@country", System.Data.SqlDbType.VarChar, 50).Value = userInfo.country;
//cmd.Parameters.Add("@Password", System.Data.SqlDbType.VarChar, 15).Value = userInfo.Password;
