using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Recetario.Account
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            Page.Validate("register");
            if (!Page.IsValid)
                return;

            var connectionString = WebConfigurationManager.ConnectionStrings["RecetarioConnectionString"].ConnectionString;
            SqlConnection dbConnection = null;
            var errorMessage = "";
            var userDt = new DataTable("Usuarios");

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
                const string sql = "SP_Login";
                var cmdUser = new SqlCommand(sql, dbConnection);
                cmdUser.CommandType = CommandType.StoredProcedure;
                cmdUser.Parameters.Clear();
                cmdUser.Parameters.AddWithValue("@Usuario", this.txtUsuario.Text);
                cmdUser.Parameters.AddWithValue("@Correo", this.txtCorreo.Text);
                cmdUser.Parameters.AddWithValue("@Pass", this.txtPass.Text);
                cmdUser.Parameters.AddWithValue("@NombreCompleto", this.txtNombre.Text);
                cmdUser.Parameters.AddWithValue("@Foto", this.fileFoto.FileBytes);

                try
                {
                    cmdUser.ExecuteNonQuery();
                    dbConnection.Close();
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

            if (errorMessage != "")
            {
                this.lblError.Text = errorMessage;
                return;
            }

            Response.Redirect("~/Account/Login.aspx", true);

        }
    }
}