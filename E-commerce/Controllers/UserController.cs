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
    public class UserController : Controller
    {
        SqlConnection sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;

        public ActionResult ViewUser()
        {
            UserInfo userInfo = new UserInfo();
            userInfo.EmailId = "";
            userInfo.Password = "";
            return View(userInfo);
        }

        [HttpPost]
        public ActionResult UserProfile(UserInfo userInfo)
        {
            UserInfo UserInfo = new UserInfo();
            try
            {
                cmd.Connection = sqlCon;
                cmd.CommandText = "selectuserbyemail";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@email", System.Data.SqlDbType.VarChar, 50).Value = userInfo.EmailId;
                if (sqlCon.State == System.Data.ConnectionState.Closed)
                {
                    sqlCon.Open();
                }
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        UserInfo ProductMaster = new UserInfo();
                        UserInfo.CustId = Convert.ToInt32(dr["CustID"]);
                        UserInfo.CustName = Convert.ToString(dr["CustName"]);
                        UserInfo.EmailId = Convert.ToString(dr["Email"]);
                        UserInfo.Zipcode = Convert.ToInt32(dr["Zipcode"]);
                        UserInfo.country = Convert.ToString(dr["Country"]);
                        UserInfo.Password = Convert.ToString(dr["Password"]);

                    }
                    dr.Close();
                }
            }
            catch { }
            finally
            {
                sqlCon.Close();
            }
            return View(UserInfo);
        }

        public ActionResult ProductList()
        {
            
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

        [HttpGet]
        public ActionResult ViewOrders()
        {   UserInfo userInfo = new UserInfo();
            userInfo.EmailId = "";
            userInfo.Password = "";
            return View(userInfo);
        }

        [HttpPost]
        public ActionResult ViewCart(UserInfo userInfo)
        {


            List<OrderDetails> OrderDetails = new List<OrderDetails>();
            try
            {
                cmd.Connection = sqlCon;
                cmd.CommandText = "SelectOrderByEmail";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@email", System.Data.SqlDbType.VarChar, 50).Value = userInfo.EmailId;
                if (sqlCon.State == System.Data.ConnectionState.Closed)
                {
                    sqlCon.Open();
                }
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        OrderDetails OrderDetail = new OrderDetails();
                        OrderDetail.OrderID = Convert.ToInt32(dr["OrderID"]);
                        OrderDetail.ProuctID = Convert.ToInt32(dr["ProductID"]);
                        OrderDetail.Quantity = Convert.ToInt32(dr["Quantity"]);
                        OrderDetail.OrderDate = Convert.ToDateTime(dr["OrderDate"]);
                        OrderDetail.CustID = Convert.ToInt32(dr["CustID"]);
                        OrderDetail.Price = Convert.ToString(dr["Price"]);

                        OrderDetails.Add(OrderDetail);
                    }
                    dr.Close();
                }
            }
            catch { }
            finally
            {
                sqlCon.Close();
            }
            return View(OrderDetails);
        }

        [HttpGet]
        public ActionResult BuyProduct(int id)
        {
            ProductMaster ProductMaster = new ProductMaster();
            try
            {
                cmd.Connection = sqlCon;
                cmd.CommandText = "SelectProductByCode";
                cmd.Parameters.Add("@ProductId", System.Data.SqlDbType.Int).Value = id;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if (sqlCon.State == System.Data.ConnectionState.Closed)
                {
                    sqlCon.Open();
                }
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();

                    ProductMaster.ProductId = Convert.ToInt32(dr["ProductId"]);
                    ProductMaster.ProductName = Convert.ToString(dr["ProductName"]);
                    ProductMaster.Brand = Convert.ToString(dr["Brand"]);
                    ProductMaster.Colour = Convert.ToString(dr["Colour"]);
                    ProductMaster.RAM_Size = Convert.ToString(dr["RAM_Size"]);
                    ProductMaster.Hard_Drive_Size = Convert.ToString(dr["Hard_Drive_Size"]);
                    ProductMaster.Processor_Type = Convert.ToString(dr["Processor_Type"]);
                    ProductMaster.MRP = Convert.ToString(dr["MRP"]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
            finally
            {
                sqlCon.Close();
            }
            return View(ProductMaster);
        }

        [HttpPost]
        public ActionResult BuyProduct(ProductMaster productMaster)
        {
            try
            {
                cmd.Connection = sqlCon;
                cmd.CommandText = "updateorderdetails";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@ProductId", System.Data.SqlDbType.Int).Value = productMaster.ProductId;
                cmd.Parameters.Add("@Quantity", System.Data.SqlDbType.Int).Value = productMaster.Quantity;
                cmd.Parameters.Add("@OrderDate", System.Data.SqlDbType.DateTime).Value = DateTime.Now.ToString("dd-MM-yyyy");
                cmd.Parameters.Add("@Email", System.Data.SqlDbType.VarChar, 50).Value = productMaster.EmailId;
                cmd.Parameters.Add("@MRP", System.Data.SqlDbType.VarChar,15).Value = productMaster.MRP;
                if (sqlCon.State == System.Data.ConnectionState.Closed)
                {
                    sqlCon.Open();
                }
                cmd.ExecuteNonQuery();
            }
            catch { }
            finally
            {
                sqlCon.Close();
            }
            return RedirectToAction("ProductList");
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToRoute(new
            {
                controller = "Common",
                action = "Home"
            });
        }
    }
}