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
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
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
                const string sql = "exec dbo.SP_BuscarReceta ''";
                var getCustomerCmd = new SqlCommand(sql, dbConnection);
                getCustomerCmd.CommandType = CommandType.StoredProcedure;
                getCustomerCmd.Parameters.Clear();
                getCustomerCmd.Parameters.AddWithValue("@Busqueda", search);

                try
                {
                    var custDa = new SqlDataAdapter();
                    custDa.SelectCommand = getCustomerCmd;
                    var custDs = new DataSet();
                    custDa.Fill(custDs, "Recetas");
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
    }
}
