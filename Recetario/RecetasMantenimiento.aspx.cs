using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Recetario
{
    public partial class WebForm1 : System.Web.UI.Page
    {

        protected SqlConnection ocn;
        protected SqlCommand cmd;
        protected SqlDataAdapter odata;
        protected DataSet oset;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
                return;

            //Session.Add("CantImages", 0);

            if (Session["IdUsuario"] == null)
            {
                Session["IdUsuario"] = 1;
            }

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

                var bytes = (byte[])row["Foto"];
                var base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                
                imgFoto.ImageUrl = "data:image/jpg;base64," + base64String;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Page.Validate("Recetas");
            if(!Page.IsValid)
                return;

            var guardo = true;
            if (!this.Page.ClientQueryString.Any())
            {
                guardo = InsertReceta();
            }
            else
            {
                var bolEliminado = this.Request.QueryString.Get("Action") == "D";
                guardo = UpdateReceta(bolEliminado);
            }

            if (guardo)
                Cancelar();
            else
                MostrarError();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Cancelar();
        }

        protected void Cancelar()
        {
            Response.Redirect("~/Default.aspx", true);
        }
        
        protected void MostrarError()
        {
            this.lblError.Text = "Ocurrio un error al momento de realizar la operacion";
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

            var fileName = Path.GetFileName(fileFoto.FileName);
            var serverFolderPath = Server.MapPath("~/Images/");
            var directoryInfo = new DirectoryInfo(serverFolderPath);

            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }

            var path = Path.Combine(serverFolderPath, fileName);

            try
            {
                fileFoto.SaveAs(path);
                this.imgFoto.ImageUrl = "~/Images/" + fileFoto.FileName;
                this.lblError.Text = this.imgFoto.ImageUrl;
            }catch(Exception ex)
            {
                this.lblError.Text = ex.Message;
            }

            //this.imgFoto.ImageUrl = "~/Images/" + fileFoto.FileName;
            //this.lblError.Text = this.imgFoto.ImageUrl;

            //var fs = fileFoto.PostedFile.InputStream;
            //var br = new BinaryReader(fs);
            //var bytes = br.ReadBytes((Int32)fs.Length);
            //var base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
            //imgFoto.ImageUrl = "data:image/jpg;base64," + base64String;
            //imgFoto.Visible = true;

            

            //var sName = fileFoto.FileName;
            
            //var sExt = Path.GetExtension(sName);
            //if(!ValidaExtension(sExt))
            //    return;

            //this.fileFoto.SaveAs(MapPath("~/temp/" + sName));

            //this.imgFoto.ImageUrl = "~/temp/" + sName;
            //this.lblDireccion.Text = this.imgFoto.ImageUrl;
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

        protected bool InsertReceta()
        {
            var retorno = true;
            ocn = new SqlConnection(WebConfigurationManager.ConnectionStrings["RecetarioConnectionString"].ConnectionString);
            cmd = new SqlCommand("SP_GuardarReceta", ocn);
            cmd.CommandType = CommandType.StoredProcedure;
            

            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@IdCategoria", this.cboCategoria.SelectedValue);
            cmd.Parameters.AddWithValue("@Nombre", this.txtNombre.Text);
            cmd.Parameters.AddWithValue("@Descripcion", this.txtDescripcion.Text);
            cmd.Parameters.AddWithValue("@Inactivo", this.chkInactivo.Checked);
            cmd.Parameters.AddWithValue("@IdUsuario", Session["IdUsuario"]);

            var paramId = new SqlParameter("@IdReceta", SqlDbType.Int);
            paramId.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(paramId);

            ocn.Open();
            SqlTransaction tran = ocn.BeginTransaction("Recetas");
            cmd.Transaction = tran;
            try
            {
                cmd.ExecuteNonQuery();
                var idReceta = paramId.Value;

                cmd.CommandText = "SP_GuardarReceta_Foto";

                var fs = fileFoto.PostedFile.InputStream;
                var br = new BinaryReader(fs);
                var bytes = br.ReadBytes((Int32)fs.Length);

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@IdReceta", idReceta);
                cmd.Parameters.AddWithValue("@Foto", bytes);
                cmd.ExecuteNonQuery();
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback("Recetas");
                retorno = false;
            }

            if(ocn.State == ConnectionState.Open)
                ocn.Close();

            return retorno;
        }

        protected bool UpdateReceta(bool eliminar)
        {
            var retorno = true;
            ocn = new SqlConnection(WebConfigurationManager.ConnectionStrings["RecetarioConnectionString"].ConnectionString);
            cmd = new SqlCommand("SP_ModificaReceta", ocn);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@IdReceta", this.Request.QueryString.Get("ID"));
            cmd.Parameters.AddWithValue("@IdCategoria", this.cboCategoria.SelectedValue);
            cmd.Parameters.AddWithValue("@Nombre", this.txtNombre.Text);
            cmd.Parameters.AddWithValue("@Descripcion", this.txtDescripcion.Text);
            cmd.Parameters.AddWithValue("@Inactivo", this.chkInactivo.Checked);
            cmd.Parameters.AddWithValue("@IdUsuario", Session["IdUsuario"]);

            ocn.Open();
            SqlTransaction tran = ocn.BeginTransaction("Recetas");
            cmd.Transaction = tran;
            try
            {
                cmd.ExecuteNonQuery();
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback("Recetas");
                retorno = false;
            }

            if (ocn.State == ConnectionState.Open)
                ocn.Close();

            return retorno;
        }
    }
}