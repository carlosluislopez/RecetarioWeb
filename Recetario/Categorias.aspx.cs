using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Recetario
{
    public partial class Categorias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] == null)
            {
                Response.Redirect("~/Account/Login.aspx", true);
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ID = this.grvCategorias.SelectedRow.Cells[1].Text;
            Response.Redirect(string.Format("~/CategoriasMantenimiento.aspx?ID={0}&Action={1}", ID, "U"));
        }

        protected void btnAgregarCategoriaBotton_Click(object sender, EventArgs e)
        {
            NuevaCategoria();
        }

        protected void NuevaCategoria()
        {
            Response.Redirect("~/CategoriasMantenimiento.aspx", true);
        }

        protected void btnAgregarCategoriaTop_Click(object sender, EventArgs e)
        {
            NuevaCategoria();
        }

       
    }
}