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
    public class AdminController : Controller
    {
        // GET: Admin
        SqlConnection sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
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
        public ActionResult AdminProfile()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddProducts()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddProducts(ProductMaster productMaster)
        {
            try
            {
                cmd.Connection = sqlCon;
                cmd.CommandText = "addproduct";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@ProductId", System.Data.SqlDbType.Int).Value = productMaster.ProductId;
                cmd.Parameters.Add("@ProductName", System.Data.SqlDbType.VarChar, 50).Value = productMaster.ProductName;
                cmd.Parameters.Add("@Brand", System.Data.SqlDbType.VarChar, 50).Value = productMaster.Brand;
                cmd.Parameters.Add("@Colour", System.Data.SqlDbType.VarChar, 50).Value = productMaster.Colour;
                cmd.Parameters.Add("@Hard_Drive_Size", System.Data.SqlDbType.VarChar, 50).Value = productMaster.Hard_Drive_Size;
                cmd.Parameters.Add("@RAM_Size", System.Data.SqlDbType.VarChar, 50).Value = productMaster.RAM_Size;
                cmd.Parameters.Add("@Processor_Type", System.Data.SqlDbType.VarChar, 50).Value = productMaster.Processor_Type;
                cmd.Parameters.Add("@MRP", System.Data.SqlDbType.VarChar, 15).Value = productMaster.MRP;
                
                if (sqlCon.State == System.Data.ConnectionState.Closed)
                {
                    sqlCon.Open();
                }
                cmd.ExecuteNonQuery();
            }
            catch(SqlException ex)
            { 
            }
            finally
            {
                sqlCon.Close();
            }
            return RedirectToAction("ProductList");
        }

        [HttpGet]
        public ActionResult DeleteProduct(int id)
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
        public ActionResult DeleteProduct(ProductMaster productMaster)
        {
            try
            {
                cmd.Connection = sqlCon;
                cmd.CommandText = "DeleteProduct";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@ProductId", System.Data.SqlDbType.Int).Value = productMaster.ProductId;
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

        [HttpGet]
        public ActionResult UpdateProduct(int id) {
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
        public ActionResult UpdateProduct(ProductMaster productMaster)
        {
            try
            {
                cmd.Connection = sqlCon;
                cmd.CommandText = "UpdateProduct";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@ProductId",System.Data.SqlDbType.Int).Value = productMaster.ProductId;
                cmd.Parameters.Add("@ProductName", System.Data.SqlDbType.VarChar,50).Value = productMaster.ProductName;
                cmd.Parameters.Add("@Brand", System.Data.SqlDbType.VarChar, 50).Value = productMaster.Brand;
                cmd.Parameters.Add("@Colour", System.Data.SqlDbType.VarChar, 50).Value = productMaster.Colour;
                cmd.Parameters.Add("@Hard_Drive_Size", System.Data.SqlDbType.VarChar, 50).Value = productMaster.Hard_Drive_Size;
                cmd.Parameters.Add("@RAM_Size", System.Data.SqlDbType.VarChar, 50).Value = productMaster.RAM_Size;
                cmd.Parameters.Add("@Processor_Type", System.Data.SqlDbType.VarChar, 50).Value = productMaster.Processor_Type;
                cmd.Parameters.Add("@MRP", System.Data.SqlDbType.VarChar, 15).Value = productMaster.MRP;
                if(sqlCon.State == System.Data.ConnectionState.Closed)
                {
                    sqlCon.Open();
                }
                cmd.ExecuteNonQuery();
            }
            catch { }
            finally {
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