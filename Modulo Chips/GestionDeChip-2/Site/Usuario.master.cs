
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Entidad;
using Negocio;
using log4net;

public partial class Usuario : System.Web.UI.MasterPage
{

    #region
    UsuarioBE oUsuarioBE = new UsuarioBE();
    UsuarioBL oUsuarioBL = new UsuarioBL();
    private static ILog mLogger = LogManager.GetLogger("Usuario");
    #endregion


    //public void cargarOpcionesUsuario(int idUsuario, TreeView tvw)
    //{

    //    string grupo = "", modulo = "";
    //    TreeNode nodoG = new TreeNode();
    //    TreeNode nodoM = new TreeNode();

    //    DataTable dtt = oUsuarioBL.PoblarMenuUsuario(idUsuario);

    //    for (int i = 0; i < dtt.Rows.Count; i++)
    //    {
    //        DataRow filaM = dtt.Rows[i];

    //        if (modulo != filaM[1].ToString())
    //        {
    //            grupo = filaM[3].ToString();
    //            nodoG = new TreeNode(grupo, filaM[2].ToString());

    //            modulo = filaM[1].ToString();
    //            nodoM = new TreeNode(modulo, filaM[0].ToString());


    //            nodoG.ChildNodes.Add(new TreeNode(filaM[5].ToString(), filaM[4].ToString(), "", filaM[6].ToString(), "_parent"));


    //            nodoM.ChildNodes.Add(nodoG);
    //            tvw.Nodes.Add(nodoM);
                
    //        }

    //        else
    //        {
    //            if (grupo != filaM[3].ToString())
    //            {
    //                grupo = filaM[3].ToString();
    //                nodoG = new TreeNode(grupo, filaM[2].ToString());
    //                nodoM.ChildNodes.Add(nodoG);
    //            }
    //            nodoG.ChildNodes.Add(new TreeNode(filaM[5].ToString(), filaM[4].ToString(), "", filaM[6].ToString(), "_parent"));
                
    //        }
    //    }

    //    dtt.Dispose();

    //    dtt = null;

    //}

    

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack) { return; }

        dynamic IdUsuario = Session["IdUser"];        
        //cargarOpcionesUsuario(Convert.ToInt16(IdUsuario),TreeView1);
        
        dynamic NombreUsuario = Session["NombreUsuario"];
        lbl_user.Text = NombreUsuario;
        //Session.Timeout = 30;

        dynamic Compania = Session["Compania"];

        //if (Compania == "01")
        //{
        //    Image2.ImageUrl = "~/Images/icono_presupuesto_.png";
            
            
        //}
        //else if(Compania == "02")
        //{
        //    Image2.ImageUrl = "~/Images/icono_presupuesto_.png";
            
        //}
        


    }

    protected void lbl_cerrarsesion_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session.Abandon();        
        Response.Redirect("Default.aspx", true);
    }



    
}
