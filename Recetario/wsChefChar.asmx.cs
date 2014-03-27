using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Services;
using System.Xml;

namespace Recetario
{
    /// <summary>
    /// Summary description for wsChefChar
    /// </summary>
    //[WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]

    [WebService(Namespace = "http://tempuri.org/",
    Description = "WebService para la aplicacion de ChefChar",
    Name = "ChefCharWebService")]

    public class wsChefChar : System.Web.Services.WebService
    {
        [WebMethod]
        public string SearchRecipies(string search)
        {
            var errorMessage = "";
            var myDatas = new XmlDocument();
            //Connection string is stored 
            //in the web.config file as an appSetting

            var connectionString = WebConfigurationManager.ConnectionStrings["RecetarioConnectionString"].ConnectionString;
            SqlConnection dbConnection = null;
            // Open a connection to the database
            try
            {
                dbConnection = new SqlConnection(connectionString);
                dbConnection.Open();
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            if (errorMessage == "" && dbConnection != null)
            {
                const string sql = "SP_BuscarReceta";
                var cmd = new SqlCommand(sql, dbConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Busqueda", search);

                try
                {
                    var oData = new SqlDataAdapter();
                    oData.SelectCommand = cmd;
                    var custDs = new DataSet("Recetas");
                    oData.Fill(custDs, "Receta");

                    myDatas.LoadXml(custDs.GetXml());
                    dbConnection.Close();

                }
                catch (System.Exception ex)
                {
                    errorMessage = ex.Message;

                }
                finally
                {
                    dbConnection.Dispose();
                }
            }
            
            if (myDatas == null)
                return "";
            
            return myDatas.InnerXml;
        }

        //[WebMethod]
        //public string SearchRecipiesString(string search)
        //{
        //    return SearchRecipies(search).InnerXml;
        //}

        [WebMethod]
        public string Login(string usuario, string pass)
        {
            var errorMessage = "";
            var myDatas = new XmlDocument();

            var connectionString = WebConfigurationManager.ConnectionStrings["RecetarioConnectionString"].ConnectionString;
            SqlConnection dbConnection = null;
            
            try
            {
                dbConnection = new SqlConnection(connectionString);
                dbConnection.Open();
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            if (errorMessage == "" && dbConnection != null)
            {
                const string sql = "SP_Login";
                var cmd = new SqlCommand(sql, dbConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Usuario", usuario);
                cmd.Parameters.AddWithValue("@Pass", pass);

                try
                {
                    var oData = new SqlDataAdapter();
                    oData.SelectCommand = cmd;
                    var oset = new DataSet("Usuarios");
                    oData.Fill(oset, "Usuario");

                    if(oset.Tables.Count > 1)
                    {
                        if(oset.Tables[0].Columns.Contains("Foto"))
                        {
                            oset.Tables[0].Columns.Remove(oset.Tables[0].Columns["Foto"]);
                        }
                    }

                    myDatas.LoadXml(oset.GetXml());
                    dbConnection.Close();

                }
                catch (System.Exception ex)
                {
                    errorMessage = ex.Message;

                }
                finally
                {
                    dbConnection.Dispose();
                }
            }
            return myDatas.InnerXml;
        }

        [WebMethod]
        public int Register(string usuario, string pass, string email, string nombre)
        {
            var resultado = 0;

            var connectionString = WebConfigurationManager.ConnectionStrings["RecetarioConnectionString"].ConnectionString;
            SqlConnection dbConnection = null;

            try
            {
                dbConnection = new SqlConnection(connectionString);
                dbConnection.Open();
            }
            catch (Exception ex) { }
            
            if (dbConnection != null)
            {
                const string sql = "SP_CrearUsuarios";
                var cmd = new SqlCommand(sql, dbConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Usuario", usuario);
                cmd.Parameters.AddWithValue("@Pass", pass);
                cmd.Parameters.AddWithValue("@Correo", email);
                cmd.Parameters.AddWithValue("@NombreCompleto", nombre);

                try
                {
                    cmd.ExecuteNonQuery();
                    dbConnection.Close();
                    resultado = 1;
                }
                catch (System.Exception ex) { }
                finally
                {
                    dbConnection.Dispose();
                }
            }
            return resultado;
        }

        [WebMethod]
        public float Rating(int IdReceta, int IdUsuario, float rating)
        {
            float resultado = 0;

            var connectionString = WebConfigurationManager.ConnectionStrings["RecetarioConnectionString"].ConnectionString;
            SqlConnection dbConnection = null;

            try
            {
                dbConnection = new SqlConnection(connectionString);
                dbConnection.Open();
            }
            catch (Exception ex) { }

            if (dbConnection != null)
            {
                const string sql = "SP_Rating";
                var cmd = new SqlCommand(sql, dbConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@IdReceta", IdReceta);
                cmd.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                cmd.Parameters.AddWithValue("@Rating", rating);

                try
                {
                    resultado = float.Parse(cmd.ExecuteScalar().ToString());
                    dbConnection.Close();
                    resultado = 1;
                }
                catch (System.Exception ex) { }
                finally
                {
                    dbConnection.Dispose();
                }
            }
            return resultado;
        }

    }
}
