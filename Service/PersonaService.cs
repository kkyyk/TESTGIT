using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TESTMVCCORE.Models.DB;
using TESTMVCCORE.Models.Extend;
using TESTMVCCORE.Models.Home;
using TESTMVCCORE.Service.Base;

namespace TESTMVCCORE.Service
{
	public class PersonaService : ServiceBase
	{
		private readonly DBContext _dbContext;
		private readonly IConfiguration _conf;
		public PersonaService(DBContext context,
			IConfiguration configuration
			) : base(context)
		{
			_dbContext = context;
			_conf = configuration;
		}

		/// <summary>
		/// 取下拉選單
		/// </summary>
		/// <param name="model"></param>
		public void GetDDL(ref Model_Add model)
		{
			model.ddl_Type.Add(new SelectListItem
			{
				Text = "請選擇",
				Value = ""
			});
			model.ddl_Type.AddRange(_dbContext.Type.Select(x => new SelectListItem
			{
				Text = x.TypeName,
				Value = x.Type1
			}).ToList());

		}

		/// <summary>
		/// 列表
		/// </summary>
		/// <returns></returns>
		public IQueryable<PersonaExtend> Query()
		{
			try
			{
				IQueryable<PersonaExtend> dataList = (from Persona in _context.Persona
													  select new PersonaExtend
													  {
														  Persona = Persona,
														  PersonaDetailList = _context.PersonaDetail.Where(x => x.PersonaId == Persona.Id).ToList()
													  });

				return dataList.AsNoTracking();
			}
			catch
			{
				throw;
			}
		}

		/// <summary>
		/// 查詢個體
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public Model_Add Detail(int id)
		{
			Model_Add model = new Model_Add();
			PersonaExtend persona = Query().Where(x => x.Persona.Id == id).FirstOrDefault();
			if (persona == null)
			{
				return null;
			}
			model.Persona = persona.Persona;
			model.PersonaDetailList = persona.PersonaDetailList;
			return model;
		}

		/// <summary>
		/// 資料驗證
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public string CheckData(Model_Add model)
		{
			string result = "";
			//資料驗證
			if (string.IsNullOrEmpty(model.Persona.Name))
			{
				result = "姓名不可為空";
				return result;
			}
			else if (model.Persona.Name.Length > 10)
			{
				result = "姓名不可超過10個字";
				return result;
			}
			if (string.IsNullOrEmpty(model.Persona.Type))
			{
				result = "類別不可為空";
				return result;
			}
			if (model.Persona.Age < 0)
			{
				result = "年齡不可小於0";
				return result;
			}
			if (model.PersonaDetailList != null && model.PersonaDetailList.Count() > 0)
			{
				if (model.PersonaDetailList.Any(x => string.IsNullOrEmpty(x.Comment)))
				{
					result = "備註不可為空";
					return result;
				}
				else if (model.PersonaDetailList.Any(x => x.Comment.Length > 10))
				{
					result = "備註不可超過10個字";
					return result;
				}
			}
			return result;
		}

		/// <summary>
		/// 新增資料
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public string AddPersona(Model_Add model)
		{
			string result = "";

			//資料驗證
			result = CheckData(model);
			if (!string.IsNullOrEmpty(result))
			{
				return result;
			}

			try
			{
				//開啟交易
				using (var transaction = this.GetTransaction())
				{
					//新增Persona

					this.Insert(model.Persona);
					//新增PersonaDetail
					if (model.PersonaDetailList != null)
					{
						foreach (var item in model.PersonaDetailList)
						{
							item.PersonaId = model.Persona.Id;
							this.Insert(item);
						}
					}
					//交易完成
					transaction.Commit();
				}
			}
			catch (Exception ex)
			{
				result = "新增失敗:" + ex.Message;
			}
			return result;
		}

		/// <summary>
		/// 編輯資料
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public string EditPersona(Model_Add model)
		{
			string result = "";
			//檢查model
			if (model.Persona == null)
			{
				return "沒有資料!";
			}
			//檢查Persona
			var PersonaModel = Detail(model.Persona.Id);
			if (PersonaModel == null)
			{
				return "沒有此人!";
			}

			//資料驗證
			result = CheckData(model);
			if (!string.IsNullOrEmpty(result))
			{
				return result;
			}

			try
			{
				//開啟交易
				using (var transaction = this.GetTransaction())
				{
					PersonaModel.Persona.Name = model.Persona.Name;
					PersonaModel.Persona.Age = model.Persona.Age;
					PersonaModel.Persona.Type = model.Persona.Type;
					//修改Persona
					this.Update(PersonaModel.Persona);


					//Detail有Id及無Id，有Id分辨是否更新還是刪除，沒Id則為新增
					if (model.PersonaDetailList != null && model.PersonaDetailList.Count() > 0)
					{
						List<PersonaDetail> dellist = new List<PersonaDetail>();

						foreach (var item in PersonaModel.PersonaDetailList)
						{
							var exist = model.PersonaDetailList?.Where(x => x.Id == item.Id).FirstOrDefault();

							if (exist != null)
							{
								item.Comment = exist.Comment;
								this.Update(item);
							}
							else
							{
								dellist.Add(item);
							}
						}
						this.DeleteRange(dellist);

						//新增PersonaDetail
						var addlist = model.PersonaDetailList.Where(x => x.Id == 0).ToList();
						if (addlist != null && addlist.Count() > 0)
						{
							foreach (var item in addlist)
							{
								item.PersonaId = PersonaModel.Persona.Id;
								this.Insert(item);
							}
						}
					}
					else
					{
						//刪除Detail
						this.DeleteRange(PersonaModel.PersonaDetailList);
					}
					//交易完成
					transaction.Commit();
				}
			}
			catch (Exception ex)
			{
				result = "修改失敗:" + ex.Message;
			}
			return result;
		}

		/// <summary>
		/// 刪除資料
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public string DeletePersona(int id)
		{
			string result = "";
			try
			{
				//開啟交易
				using (var transaction = this.GetTransaction())
				{
					//找出Persona
					var Persona = this.Find<Persona>(id);
					if (Persona == null)
					{
						return "沒有此人!";
					}
					//找出是否有Detail
					var PersonaDetailList = _dbContext.PersonaDetail.Where(x => x.PersonaId == Persona.Id).ToList();
					if (PersonaDetailList != null && PersonaDetailList.Count() > 0)
					{
						this.DeleteRange(PersonaDetailList);
					}
					//刪除Persona
					this.Delete(Persona);
					//交易完成
					transaction.Commit();
				}
			}
			catch (Exception ex)
			{
				result = "刪除失敗:" + ex.Message;
			}
			return result;
		}
	}
}
