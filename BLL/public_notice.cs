using System;
using System.Data;
using System.Collections.Generic;
using XHD.Common;
using XHD.Model;
namespace XHD.BLL
{
	/// <summary>
	/// public_notice
	/// </summary>
	public partial class public_notice
	{
		private readonly XHD.DAL.public_notice dal=new XHD.DAL.public_notice();
		public public_notice()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
			return dal.GetMaxId();
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			return dal.Exists(id);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(XHD.Model.public_notice model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(XHD.Model.public_notice model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 预删除
		/// </summary>
		/// <param name="id"></param>
		/// <param name="isDelete"></param>
		/// <param name="time"></param>
		/// <returns></returns>
		public bool AdvanceDelete(int id, int isDelete, string time)
		{
			return dal.AdvanceDelete(id, isDelete, time);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int id)
		{
			
			return dal.Delete(id);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string idlist )
		{
			return dal.DeleteList(idlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public XHD.Model.public_notice GetModel(int id)
		{
			
			return dal.GetModel(id);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public XHD.Model.public_notice GetModelByCache(int id)
		{
			
			string CacheKey = "public_noticeModel-" + id;
			object objModel = XHD.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(id);
					if (objModel != null)
					{
						int ModelCache = XHD.Common.ConfigHelper.GetConfigInt("ModelCache");
						XHD.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (XHD.Model.public_notice)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<XHD.Model.public_notice> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<XHD.Model.public_notice> DataTableToList(DataTable dt)
		{
			List<XHD.Model.public_notice> modelList = new List<XHD.Model.public_notice>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				XHD.Model.public_notice model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new XHD.Model.public_notice();
					if(dt.Rows[n]["id"]!=null && dt.Rows[n]["id"].ToString()!="")
					{
						model.id=int.Parse(dt.Rows[n]["id"].ToString());
					}
					if(dt.Rows[n]["notice_title"]!=null && dt.Rows[n]["notice_title"].ToString()!="")
					{
					model.notice_title=dt.Rows[n]["notice_title"].ToString();
					}
					if(dt.Rows[n]["notice_content"]!=null && dt.Rows[n]["notice_content"].ToString()!="")
					{
					model.notice_content=dt.Rows[n]["notice_content"].ToString();
					}
					if(dt.Rows[n]["create_id"]!=null && dt.Rows[n]["create_id"].ToString()!="")
					{
						model.create_id=int.Parse(dt.Rows[n]["create_id"].ToString());
					}
					if(dt.Rows[n]["create_name"]!=null && dt.Rows[n]["create_name"].ToString()!="")
					{
					model.create_name=dt.Rows[n]["create_name"].ToString();
					}
					if(dt.Rows[n]["dep_id"]!=null && dt.Rows[n]["dep_id"].ToString()!="")
					{
						model.dep_id=int.Parse(dt.Rows[n]["dep_id"].ToString());
					}
					if(dt.Rows[n]["dep_name"]!=null && dt.Rows[n]["dep_name"].ToString()!="")
					{
					model.dep_name=dt.Rows[n]["dep_name"].ToString();
					}
					if(dt.Rows[n]["notice_time"]!=null && dt.Rows[n]["notice_time"].ToString()!="")
					{
						model.notice_time=DateTime.Parse(dt.Rows[n]["notice_time"].ToString());
					}
					modelList.Add(model);
				}
			}
			return modelList;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize, int PageIndex, string strWhere, string filedOrder, out string Total)
		{
			return dal.GetList(PageSize, PageIndex, strWhere, filedOrder, out Total);
		}

		#endregion  Method
	}
}

