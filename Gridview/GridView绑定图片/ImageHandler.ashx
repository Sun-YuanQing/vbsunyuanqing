<%@ WebHandler Language="C#" Class="ImageHandler" %>

using System;
using System.Web;
using System.Data.SqlClient;

public class ImageHandler : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) 
    {
         string imageid = context.Request.QueryString["ImID"];
         SqlConnection connection = new SqlConnection("Data Source=(local);Database=wxd;Uid=sa;Pwd=sa");
         DBHelper.OpenConnection();
         SqlCommand command = new SqlCommand("select [Image] from [Image] where ImageID=" + imageid,DBHelper.Conn);
         SqlDataReader dr = command.ExecuteReader();
         dr.Read();
         context.Response.BinaryWrite((Byte[])dr[0]);
          DBHelper.CloseConnection();
         context.Response.End();

    }
 
    public bool IsReusable
    {
        get {
            return false;
        }
    }

}