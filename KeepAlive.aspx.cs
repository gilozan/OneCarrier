using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class KeepAlive : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            string sessionType = Session["type"].ToString();
            Response.Write("success");
        }
        catch
        {
            Response.Write("sessionend");
        }
    }
}