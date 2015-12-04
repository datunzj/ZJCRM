﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Web.Security;

namespace DataShare
{
    public partial class Favorites_Adv_View : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                var ticket = FormsAuthentication.Decrypt(cookie.Value);
                var sbHtml = new System.Text.StringBuilder();
                var sb = new System.Text.StringBuilder();
                //图片广告
                sb.AppendLine("SELECT * FROM dbo.v_CRM_Favorites_Share ");
                sb.AppendLine("WHERE IsShowPic='Y' AND IsDel='N' AND InEmpID='1' ");
                sb.AppendLine("ORDER BY OrderID ");
                DataTable dt = SqlDB.ExecuteDataTable(sb.ToString()).Output1;
                sbHtml.Append("<table style='border:0;witdh:90%;'><tr style='border:0;'><td style='border:0;'>");
                foreach (DataRow dr in dt.Rows)
                    sbHtml.Append("<a href='" + dr["Url"] + "' target='_blank'><img src='http://gl.xczs.com/images/upload/favimg/" + dr["PicUrl"] + "' style='border-radius: 4px; box-shadow: 1px 1px 3px #111; width: 150px; height: 75px; margin-left: 5px; background: #d2d2f2; border: 3px solid #fff; behavior: url(../css/pie.htc);' /></a>&nbsp;&nbsp;");
                sbHtml.Append("</td></tr></table>");
                sbHtml.Append("<table style='border:0;width:90%;'><tr style='border:0;'><td style='border:0;text-align:center;'><a href='public_data_share.aspx' style='color:red;font-size:24px;font-weight:normal;text-decoration:none;'>点击进入共享资料</a></td></tr></table>");
                //个人收藏
                sb.Clear();
                sb.AppendLine("SELECT * FROM dbo.v_CRM_Favorites_Share ");
                sb.AppendLine("WHERE IsShowPic='N' AND IsDel='N' AND InEmpID='1' ");
                sb.AppendLine("ORDER BY OrderID ");
                dt = SqlDB.ExecuteDataTable(sb.ToString()).Output1;
                DataTable cdt = dt.DefaultView.ToTable(true, "CategoryID", "CategoryName");
                if (cdt.Rows.Count > 0)
                    sbHtml.Append("<table class='fav'> ");
                foreach (DataRow cdr in cdt.Rows)
                {
                    sbHtml.Append("<tr><td class='title'>" + cdr["CategoryName"] + "</td>");
                    DataRow[] ddr=dt.Select("CategoryID='" + cdr["CategoryID"] + "'");
                    if(ddr.Length>0)
                        sbHtml.Append("<td>");
                    foreach (DataRow dr in ddr)
                        sbHtml.Append("<li><a href='" + dr["Url"] + "' target='_blank'>" + dr["Name"] + "</a></li>");
                    if (ddr.Length > 0)
                        sbHtml.Append("</td>");
                }
                if (cdt.Rows.Count > 0)
                    sbHtml.Append("</tr></table>");
                lb.Text = sbHtml.ToString();
            }
        }
    }
}