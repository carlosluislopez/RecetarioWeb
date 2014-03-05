using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace Recetario
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Page.IsPostBack)
                return;

            if(!this.Page.ClientQueryString.Any())
            {
                return;
            }

            if(this.Request.QueryString.Get("Action") == "D")
            {
                btnGuardar.Text = "Eliminar";
                txtNombre.ReadOnly = true;
                txtDescripcion.ReadOnly = true;
            }

            this.SqlDataSource1.DataBind();
            var args = new DataSourceSelectArguments();
            var view = (DataView)SqlDataSource1.Select(args);
            var dt = view.ToTable();
            
            var columnNumber = 0;
            foreach (DataRow row in dt.Rows)
            {
                this.txtNombre.Text = row["Categoria"].ToString();
                this.txtDescripcion.Text = row["Descripcion"].ToString();
                this.chkInactivo.Checked = bool.Parse(row["Inactivo"].ToString());
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!this.Page.ClientQueryString.Any())
            {
                this.SqlDataSource1.Insert();
            }else
            {
                var strEliminado = "False";
                if (this.Request.QueryString.Get("Action") == "D")
                {
                    strEliminado = "True";
                }

                this.SqlDataSource1.UpdateParameters["Eliminado"].DefaultValue = strEliminado;
                this.SqlDataSource1.Update();   
            }

            cancelar();

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            cancelar();
        }

        protected void cancelar()
        {
            Response.Redirect("~/Default.aspx", true);
        }
    }
}