using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace Recetario.Account
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Page.Validate("login");
            if(!Page.IsValid)
                return;

            var connectionString = WebConfigurationManager.ConnectionStrings["RecetarioConnectionString"].ConnectionString;
            SqlConnection dbConnection = null;
            var errorMessage = "";
            var userDt = new DataTable("Usuarios");

            try
            {
                dbConnection = new SqlConnection(connectionString);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            if (errorMessage == "" && dbConnection != null)
            {
                // Build an insert command
                const string sql = "SP_Login";
                var cmdUser = new SqlCommand(sql, dbConnection);
                cmdUser.CommandType = CommandType.StoredProcedure;
                cmdUser.Parameters.Clear();
                cmdUser.Parameters.AddWithValue("@Usuario", this.txtUser.Text);
                cmdUser.Parameters.AddWithValue("@Pass", this.txtPass.Text);

                try
                {
                    var odata = new SqlDataAdapter(cmdUser);
                    odata.Fill(userDt);
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;

                }
                finally
                {
                    if (dbConnection.State == ConnectionState.Open)
                        dbConnection.Close();
                    dbConnection.Dispose();
                }
            }

            if(errorMessage != "")
            {
                this.lblError.Text = errorMessage;
                return;
            }

            if (userDt.Rows.Count == 0)
            {
                this.lblError.Text = "Usuario y clave incorrecta";
                return;
            }

            if (Session["IdUsuario"] == null)
            {
                Session["IdUsuario"] = userDt.Rows[0]["IdUsuario"];
            }

            if (Session["Usuario"] == null)
            {
                Session["Usuario"] = userDt.Rows[0]["Usuario"];
            }

            if (Session["NombreCompleto"] == null)
            {
                Session["NombreCompleto"] = userDt.Rows[0]["NombreCompleto"];
            }

            Response.Redirect("~/Default.aspx", true);

        }
    }
}