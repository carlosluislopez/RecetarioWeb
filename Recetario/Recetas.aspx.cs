using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Recetario
{
    public partial class Recetas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.ClientQueryString.Any())
            {
                this.sqlDSRecetas.DataBind();
            }

        }

        protected void btnAgregarRecetaTop_Click(object sender, EventArgs e)
        {
            NuevaReceta();
        }

        protected void NuevaReceta()
        {
            Response.Redirect("~/RecetasMantenimiento.aspx", true);
        }

        protected void btnAgregarRecetaBotton_Click(object sender, EventArgs e)
        {
            NuevaReceta();
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ID = this.grvRecetas.SelectedRow.Cells[1].Text;
            Response.Redirect(string.Format("~/RecetasMantenimiento.aspx?ID={0}&Action={1}", ID, "U"));
        }

        protected void grvRecetas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType == DataControlRowType.DataRow)
            {
                var datos = (DataRowView)e.Row.DataItem;
                //var bytes = (byte[]) datos["Foto"];
                //var base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                
                var img = (Image)e.Row.FindControl("imgFoto");
                //img.ImageUrl = "data:image/jpg;base64," + base64String;
                img.ImageUrl = datos["Foto"].ToString();
            }
        }
    }
}