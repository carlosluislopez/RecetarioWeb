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
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public int Add(int a, int b)
        {
            return a + b;
        }

        [WebMethod]
        public XmlDocument SearchRecipies(string search)
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
                // Build an insert command
                const string sql = "SP_BuscarReceta";
                var getCustomerCmd = new SqlCommand(sql, dbConnection);
                getCustomerCmd.CommandType = CommandType.StoredProcedure;
                getCustomerCmd.Parameters.Clear();
                getCustomerCmd.Parameters.AddWithValue("@Busqueda", search);

                try
                {
                    var custDa = new SqlDataAdapter();
                    custDa.SelectCommand = getCustomerCmd;
                    var custDs = new DataSet("Recetas");
                    custDa.Fill(custDs, "Receta");

                    //var dt = new DataTable("Recetas");
                    //dt.Columns.Add("Id", typeof (Int32));
                    //dt.Columns.Add("Nombre", typeof(String));
                    //dt.Columns.Add("Foto", typeof(byte[]));

                    //custDs.Tables.Add(dt);

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
            return myDatas;
        }

        [WebMethod]
        public string SearchRecipiesString(string search)
        {
            return SearchRecipies(search).InnerXml.ToString();
        }

        [WebMethod]
        public Recipie getRecipie()
        {
            return new Recipie(1, "Pollo", null);
        }

        [WebMethod]
        public Recipie[] getRecipieCollection()
        {
            var recetas = new Recipie[]
                                    {
                                        new Recipie(1, "Pollo", null),
                                        new Recipie(2, "Res", null),
                                        new Recipie(3, "Sopa de Res", null),
                                        new Recipie(4, "Flan de Chocolate", null),
                                    };
            return recetas;
        }
    }

    public class Recipie
    {
        public int ID;
        public string Nombre;
        public byte[] Foto;

        public Recipie()
        {
            
        }

        public Recipie(int id, string nombre, byte[] foto)
        {
            this.ID = id;
            this.Nombre = nombre;
            this.Foto = foto;
        }

    }
}
