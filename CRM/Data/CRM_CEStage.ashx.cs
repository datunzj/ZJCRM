﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using XHD.Common;
using System.Web.Security;

namespace XHD.CRM.Data
{
    /// <summary>
    /// CRM_CEStage 的摘要说明
    /// </summary>
    public class CRM_CEStage : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            HttpRequest request = context.Request;

            BLL.CRM_CEStage ccpc = new BLL.CRM_CEStage();
            Model.CRM_CEStage model = new Model.CRM_CEStage();

            var cookie = context.Request.Cookies[FormsAuthentication.FormsCookieName];
            var ticket = FormsAuthentication.Decrypt(cookie.Value);
            string CoockiesID = ticket.UserData;

            BLL.hr_employee emp = new BLL.hr_employee();
            int emp_id = int.Parse(CoockiesID);
            DataSet dsemp = emp.GetList("id=" + emp_id);
            string empname = dsemp.Tables[0].Rows[0]["name"].ToString();
            string uid = dsemp.Tables[0].Rows[0]["uid"].ToString();

            if (request["Action"] == "save")
            {
                //string parentid = PageValidate.InputText(request["T_category_parent_val"], 50);
                //model.parentid = int.Parse(parentid);
                //model.product_category = Common.PageValidate.InputText(request["T_category_name"], 250);
                //model.product_icon = Common.PageValidate.InputText(request["T_category_icon"], 250);
                model.CustomerID = int.Parse(Common.PageValidate.InputText(request["T_companyid"], 50));
                model.CustomerName = Common.PageValidate.InputText(request["T_company"], 250);
                model.sgjl = Common.PageValidate.InputText(request["T_employee_sg"], 250);
                model.sgjlid = StringToInt(Common.PageValidate.InputText(request["T_employee1_sg"], 50));
                model.sjs = Common.PageValidate.InputText(request["T_employee_sj"], 250);
                model.sjsid = StringToInt(Common.PageValidate.InputText(request["T_employee1_sj"], 50));
                model.SpecialScore = StringToDecimal(Common.PageValidate.InputText(request["T_SpecialScore"], 50));
                model.tel = Common.PageValidate.InputText(request["T_company_tel"], 250);
                model.ywy = Common.PageValidate.InputText(request["T_employee"], 250);
                model.ywyid = StringToInt(Common.PageValidate.InputText(request["T_employee1"], 50));
                model.Stage_icon = Common.PageValidate.InputText(request["T_private"], 250);
                string id = PageValidate.InputText(request["id"], 50);
                //string pid = PageValidate.InputText(request["T_category_parent_val"], 50);
                model.Remarks = Common.PageValidate.InputText(request["T_remarks"], 250);
                if (!string.IsNullOrEmpty(id) && id != "null")
                {
                    model.id = int.Parse(id);

                    //DataSet ds = ccpc.GetList(" id=" + int.Parse(id));
                    //DataRow dr = ds.Tables[0].Rows[0];

                    if (model.sgjl == "" || model.sgjl == "null" || string.IsNullOrEmpty(model.sgjl))
                        context.Response.Write("false:type");
                    else
                    {
                       bool aa= ccpc.Update(model,StringToInt(id));
                    }


                //        //日志
                //        C_Sys_log log = new C_Sys_log();

                //        int UserID = emp_id;
                //        string UserName = empname;
                //        string IPStreet = request.UserHostAddress;
                //        string EventTitle = model.product_category;
                //        string EventType = "产品类别修改";
                //        int EventID = model.id;
                //        if (dr["product_category"].ToString() != request["T_category_name"])
                //        {
                //            log.Add_log(UserID, UserName, IPStreet, EventTitle, EventType, EventID, "产品类别", dr["product_category"].ToString(), request["T_category_name"]);
                //        }
                //        if (dr["product_icon"].ToString() != request["T_category_icon"])
                //        {
                //            log.Add_log(UserID, UserName, IPStreet, EventTitle, EventType, EventID, "类别图标", dr["product_icon"].ToString(), request["T_category_icon"]);
                //        }
                //        if (dr["parentid"].ToString() != request["T_category_parent_val"])
                //        {
                //            log.Add_log(UserID, UserName, IPStreet, EventTitle, EventType, EventID, "上级类别", dr["parentid"].ToString(), request["T_category_parent_val"]);
                //        }
                    
                }

                else
                {
                    if (model.sgjl == "" || model.sgjl == "null" || string.IsNullOrEmpty(model.sgjl))
                        context.Response.Write("false:type");
                   // model.isDelete = 0;
                    else
                    ccpc.Add(model);
                }
            }

            if (request["Action"] == "getcustomer")
            {
                int PageIndex = int.Parse(request["page"] == null ? "1" : request["page"]);
                int PageSize = int.Parse(request["pagesize"] == null ? "30" : request["pagesize"]);
                string sortname = request["sortname"];
                string sortorder = request["sortorder"];

                if (string.IsNullOrEmpty(sortname))
                    sortname = " id";
                if (string.IsNullOrEmpty(sortorder))
                    sortorder = " desc";

                string sorttext = " " + sortname + " " + sortorder;
                string Total;
                string serchtxt = "1=1";
               serchtxt +=" and id not in(SELECT CustomerID FROM dbo.CRM_CEStage) ";
               

                string dt = "";

                DataSet ds = ccpc.GetListCustomer(PageSize, PageIndex, serchtxt, sorttext, out Total);
                dt = Common.GetGridJSON.DataTableToJSON1(ds.Tables[0], Total);

                context.Response.Write(dt);

            }
            if (request["Action"] == "grid")
            {
                int PageIndex = int.Parse(request["page"] == null ? "1" : request["page"]);
                int PageSize = int.Parse(request["pagesize"] == null ? "30" : request["pagesize"]);
                string sortname = request["sortname"];
                string sortorder = request["sortorder"];

                if (string.IsNullOrEmpty(sortname))
                    sortname = " id";
                if (string.IsNullOrEmpty(sortorder))
                    sortorder = " desc";

                string sorttext = " " + sortname + " " + sortorder;

                string Total;
                string serchtxt = "1=1";
                //if (!string.IsNullOrEmpty(request["company"]))
                //    serchtxt += " and product_category like N'%" + PageValidate.InputText(request["company"], 50) + "%'";

                //if (!string.IsNullOrEmpty(request["startdate_del"]))
                //{
                //    serchtxt += " and Delete_time >= '" + PageValidate.InputText(request["startdate_del"], 50) + "'";
                //}
                //if (!string.IsNullOrEmpty(request["enddate_del"]))
                //{
                //    DateTime enddate = DateTime.Parse(request["enddate_del"]);
                //    serchtxt += " and Delete_time  <= '" + enddate.AddHours(23).AddMinutes(59).AddSeconds(59) + "'";
                //}
                //权限


                string dt = "";
               
                    DataSet ds = ccpc.GetListDetail(PageSize, PageIndex, serchtxt, sorttext, out Total);
                    dt = Common.GetGridJSON.DataTableToJSON1(ds.Tables[0], Total);
                 
                context.Response.Write(dt);
            }
             
            
            if (request["Action"] == "form")
            {
                string cid = PageValidate.InputText(request["cid"], 50);
                string dt;
                if (PageValidate.IsNumber(cid))
                {
                    DataSet ds = ccpc.GetList("id=" + cid);
                    dt = Common.DataToJson.DataToJSON(ds);
                }
                else
                {
                    dt = "{}";
                }

                context.Response.Write(dt);
            }

            //del
            if (request["Action"] == "del")
            {
                //参数安全过滤
                string c_id = PageValidate.InputText(request["id"], 50);

                DataSet ds = ccpc.GetList(" id=" + int.Parse(c_id));
 
                    bool isdel = ccpc.Delete(int.Parse(c_id));
                    if (isdel)
                    {
                        //日志
                        string EventType = "施工项目删除";

                        int UserID = emp_id;
                        string UserName = empname;
                        string IPStreet = request.UserHostAddress;
                        int EventID = int.Parse(c_id);
                        string EventTitle = ds.Tables[0].Rows[0]["id"].ToString();
                        string Original_txt = null;
                        string Current_txt = null;

                        C_Sys_log log = new C_Sys_log();

                        log.Add_log(UserID, UserName, IPStreet, EventTitle, EventType, EventID, null, Original_txt, Current_txt);

                        context.Response.Write("true");
                    }
                    else
                    {
                        context.Response.Write("false");
                    }
                

            }

        }
        private static string GetTasksString(int Id, DataTable table)
        {
            DataRow[] rows = table.Select("parentid=" + Id.ToString());

            if (rows.Length == 0) return string.Empty; ;
            StringBuilder str = new StringBuilder();

            foreach (DataRow row in rows)
            {
                str.Append("{");
                for (int i = 0; i < row.Table.Columns.Count; i++)
                {
                    if (i != 0) str.Append(",");
                    str.Append(row.Table.Columns[i].ColumnName);
                    str.Append(":'");
                    str.Append(row[i].ToString());
                    str.Append("'");
                }
                if (GetTasksString((int)row["id"], table).Length > 0)
                {
                    str.Append(",children:[");
                    str.Append(GetTasksString((int)row["id"], table));
                    str.Append("]},");
                }
                else
                {
                    str.Append("},");
                }
            }
            return str[str.Length - 1] == ',' ? str.ToString(0, str.Length - 1) : str.ToString();
        }
        private static string GetTreeString(int Id, DataTable table)
        {
            DataRow[] rows = table.Select(string.Format("parentid={0}", Id));

            if (rows.Length == 0) return string.Empty;
            StringBuilder str = new StringBuilder();

            foreach (DataRow row in rows)
            {
                str.Append("{id:" + (int)row["id"] + ",text:'" + (string)row["product_category"] + "',d_icon:'../../" + (string)row["product_icon"] + "'");

                if (GetTreeString((int)row["id"], table).Length > 0)
                {
                    str.Append(",children:[");
                    str.Append(GetTreeString((int)row["id"], table));
                    str.Append("]},");
                }
                else
                {
                    str.Append("},");
                }
            }
            return str[str.Length - 1] == ',' ? str.ToString(0, str.Length - 1) : str.ToString();
        }

        private static int StringToInt(string code)
        {
            try {
             return   int.Parse(code);
            }
            catch {

                return 0;
            }
        }
        private static decimal StringToDecimal(string code)
        {
            try
            {
                return decimal.Parse(code);
            }
            catch
            {

                return 0;
            }
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}