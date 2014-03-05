using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Recetario
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
                return;

            Session.Add("CantImages", 0);

            if (!this.Page.ClientQueryString.Any())
            {
                return;
            }

            if (this.Request.QueryString.Get("Action") == "D")
            {
                btnGuardar.Text = "Eliminar";
                txtNombre.ReadOnly = true;
                txtDescripcion.ReadOnly = true;
            }

            this.SqlDataSource1.DataBind();
            this.SqlDataSource2.DataBind();
            this.cboCategoria.DataBind();

            var args = new DataSourceSelectArguments();
            var view = (DataView)SqlDataSource1.Select(args);
            var dt = view.ToTable();
            
            foreach (DataRow row in dt.Rows)
            {
                this.txtNombre.Text = row["Nombre"].ToString();
                this.txtDescripcion.Text = row["Descripcion"].ToString();
                this.cboCategoria.SelectedIndex = indexCategoria(row["IdCategoria"].ToString());
                this.chkInactivo.Checked = bool.Parse(row["Inactivo"].ToString());
                this.imgFoto.ImageUrl = row["Foto"].ToString();
                this.lblDireccion.Text = this.imgFoto.ImageUrl;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!this.Page.ClientQueryString.Any())
            {
                this.SqlDataSource1.Insert();
            }
            else
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

        protected int indexCategoria(string IdCategoria)
        {
            int index = 0;
            foreach (ListItem item in this.cboCategoria.Items)
            {
                if(item.Value.Equals(IdCategoria))
                {
                    return index;
                }
                index++;
            }
            return -1;
        }

        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            var ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms.ToArray();
        }

        public System.Drawing.Image byteArrayToImage(byte[] byteArrayIn)
        {
            var ms = new MemoryStream(byteArrayIn);
            var returnImage = System.Drawing.Image.FromStream(ms);
            return returnImage;
        }

        protected void btnSubirImagen_Click(object sender, EventArgs e)
        {
            if(!this.fileFoto.HasFile)
                return;

            var sName = fileFoto.FileName;
            
            var sExt = Path.GetExtension(sName);
            if(!ValidaExtension(sExt))
                return;

            this.fileFoto.SaveAs(MapPath("~/temp/" + sName));

            this.imgFoto.ImageUrl = "~/temp/" + sName;
            this.lblDireccion.Text = this.imgFoto.ImageUrl;
        }

        protected bool ValidaExtension(string ext)
        {
            switch (ext)
            {
                case ".jpg":
                case ".jpeg":
                case ".png":
                case ".bmp":
                case ".gif":
                    return true;
                default:
                    return false;
            }
        }
    }
}