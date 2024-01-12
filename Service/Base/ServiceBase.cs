using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TESTMVCCORE.Models.DB;

namespace TESTMVCCORE.Service.Base
{
	public class ServiceBase
	{
		/// <summary> 
		/// Context 連線資訊
		/// </summary>
		public DBContext _context { get; set; } = null!;


		/// <summary>
		/// 建構式
		/// </summary>
		/// <param name="context">Entity Framework Core DbContext物件</param>
		public ServiceBase(DBContext context)
		{
			try
			{
				_context = context;
			}
			catch (Exception ex)
			{

			}
		}

		#region 查詢 Query

		/// <summary> 
		/// 取得資料
		/// </summary>
		/// <typeparam name="T">方法型別</typeparam>
		/// <param name="ErrMsg">錯誤訊息</param>
		/// <param name="filter">LINQ 表達式</param>
		/// <returns></returns>
		public IQueryable<T> Lookup<T>(Expression<Func<T, bool>>? filter = null) where T : class
		{
			try
			{
				/* 標記為 NoTracking，防止對取出 Items 做變更後接續 SaveChanges() 動作一併儲存 */
				var dataList = _context.Set<T>().AsNoTracking();

				if (filter != null)
				{
					dataList = dataList.Where(filter);
				}

				return dataList;
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		/// <summary>
		/// 以 Primary Key 取得資料
		/// </summary>
		/// <typeparam name="T">方法型別</typeparam>
		/// <param name="key">型態須正確(不會自動轉型)</param>
		/// <returns></returns>
		public  T? Find<T>(params object[] key) where T : class
		{
			try
			{
				var data = _context.Set<T>().Find(key);

				return data;
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		/// <summary>
		/// 以 Primary Key 取得資料
		/// </summary>
		/// <typeparam name="T">方法型別</typeparam>
		/// <param name="key">型態須正確(不會自動轉型)</param>
		/// <returns></returns>
		public async Task<T?> FindAsync<T>(params object[] key) where T : class
		{
			try
			{
				var data = await _context.Set<T>().FindAsync(key);

				return data;
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		#endregion

		#region 新增Insert
		public  string Insert<T>(T entity) where T : class
		{
			string result = "";
			try
			{
				_context.Set<T>().Add(entity);
				_context.SaveChanges();

			}
			catch (Exception ex)
			{
				_context.Entry<T>(entity).State = EntityState.Detached;
				result = typeof(T) + "新增失敗:" + ex.Message;
			}

			return result;
		}


		/// <summary>
		/// 新增資料
		/// </summary>
		/// <typeparam name="T">方法型別</typeparam>
		/// <param name="entity">類別</param>
		/// <returns></returns>
		//public async Task<ActionResultModel<T>> InsertAsync<T>(T entity) where T : class
		//{
		//	ActionResultModel<T> result = new ActionResultModel<T>();
		//	try
		//	{
		//		_context.Set<T>().Add(entity);
		//		await _context.SaveChangesAsync();

		//		result.IsSuccess = true;
		//		result.Data = entity;
		//	}
		//	catch (Exception ex)
		//	{
		//		_context.Entry<T>(entity).State = EntityState.Detached;
		//		throw;
		//	}

		//	return result;
		//}

		/// <summary>
		/// 新增資料(多筆)
		/// </summary>
		/// <typeparam name="T">方法型別</typeparam>
		/// <param name="entities">類別</param>
		/// <returns></returns>
		//public async Task<ActionResultModel<IEnumerable<T>>> InsertRange<T>(IEnumerable<T> entities) where T : class
		//{
		//	ActionResultModel<IEnumerable<T>> result = new ActionResultModel<IEnumerable<T>>();
		//	try
		//	{
		//		_context.Set<T>().AddRange(entities);
		//		await _context.SaveChangesAsync();

		//		result.IsSuccess = true;
		//		result.Data = entities;
		//	}
		//	catch (Exception ex)
		//	{
		//		foreach (var entity in entities)
		//		{
		//			_context.Entry<T>(entity).State = EntityState.Detached;
		//		}
		//		throw;
		//	}
		//	return result;
		//}


		/// <summary>
		/// 新增資料(多筆)同步型
		/// </summary>
		/// <typeparam name="T">方法型別</typeparam>
		/// <param name="entities">類別</param>
		/// <returns></returns>
		//public ActionResultModel<IEnumerable<T>> InsertRangeSync<T>(IEnumerable<T> entities) where T : class
		//{
		//	ActionResultModel<IEnumerable<T>> result = new ActionResultModel<IEnumerable<T>>();
		//	try
		//	{
		//		_context.Set<T>().AddRange(entities);
		//		_context.SaveChanges();

		//		result.IsSuccess = true;
		//		result.Data = entities;
		//	}
		//	catch (Exception ex)
		//	{
		//		foreach (var entity in entities)
		//		{
		//			_context.Entry<T>(entity).State = EntityState.Detached;
		//		}
		//		throw;
		//	}
		//	return result;
		//}

		#endregion

		#region 修改 Update
		public string Update<T>(T entity) where T : class
		{
			string result = "";
			try
			{
				_context.Update(entity);
				_context.SaveChanges();

			}
			catch (Exception ex)
			{
				_context.Entry<T>(entity).State = EntityState.Detached;
				result = typeof(T) + "修改失敗:" + ex.Message;
			}

			return result;
		}
		/// <summary>
		/// 修改資料
		/// </summary>
		/// <typeparam name="T">方法型別</typeparam>
		/// <param name="entity">類別</param>
		/// <returns></returns>
		//public async Task<ActionResultModel<T>> Update<T>(T entity) where T : class
		//{
		//	ActionResultModel<T> result = new ActionResultModel<T>();
		//	try
		//	{
		//		_context.Update(entity);
		//		await _context.SaveChangesAsync();

		//		result.IsSuccess = true;
		//		result.Data = entity;
		//	}
		//	catch (Exception ex)
		//	{
		//		_context.Entry<T>(entity).State = EntityState.Detached;
		//		throw;
		//	}
		//	return result;
		//}

		/// <summary>
		/// 修改資料(多筆)
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="entities"></param>
		/// <param name="ErrMsg"></param>
		/// <returns></returns>
		//public async Task<ActionResultModel<IEnumerable<T>>> UpdateRange<T>
		//	(IEnumerable<T> entities) where T : class
		//{
		//	ActionResultModel<IEnumerable<T>> result = new ActionResultModel<IEnumerable<T>>();
		//	try
		//	{
		//		_context.UpdateRange(entities);
		//		await _context.SaveChangesAsync();

		//		result.IsSuccess = true;
		//		result.Data = entities;
		//	}
		//	catch (Exception ex)
		//	{
		//		foreach (var entity in entities)
		//		{
		//			_context.Entry<T>(entity).State = EntityState.Detached;
		//		}
		//		throw;
		//	}
		//	return result;
		//}

		#endregion

		#region 刪除
		public string Delete<T>(T entity) where T : class
		{
			string result = "";
			try
			{
				_context.Remove(entity);
				_context.SaveChanges();

			}
			catch (Exception ex)
			{
				_context.Entry<T>(entity).State = EntityState.Detached;
				throw;
			}
			return result;
		}

		/// <summary>
		/// 刪除資料
		/// </summary>
		/// <typeparam name="T">方法型別</typeparam>
		/// <param name="entity">類別</param>
		/// <returns></returns>
		//public async Task<ActionResultModel<T>> Delete<T>(T entity) where T : class
		//{
		//	ActionResultModel<T> result = new ActionResultModel<T>();
		//	try
		//	{
		//		_context.Remove(entity);
		//		await _context.SaveChangesAsync();

		//		result.IsSuccess = true;
		//		result.Data = entity;
		//	}
		//	catch (Exception ex)
		//	{
		//		_context.Entry<T>(entity).State = EntityState.Detached;
		//		throw;
		//	}
		//	return result;
		//}

		/// <summary>
		/// 刪除資料(多筆)
		/// </summary>
		/// <typeparam name="T">方法型別</typeparam>
		/// <param name="entities">類別</param>
		/// <returns></returns>
		//public async Task<ActionResultModel<IEnumerable<T>>> DeleteRange<T>
		//	(IEnumerable<T> entities) where T : class
		//{
		//	ActionResultModel<IEnumerable<T>> result = new ActionResultModel<IEnumerable<T>>();
		//	try
		//	{
		//		_context.RemoveRange(entities);
		//		await _context.SaveChangesAsync();

		//		result.IsSuccess = true;
		//		result.Data = entities;
		//	}
		//	catch (Exception ex)
		//	{
		//		foreach (var entity in entities)
		//		{
		//			_context.Entry<T>(entity).State = EntityState.Detached;
		//		}
		//		throw;
		//	}
		//	return result;
		//}

		#endregion

		#region 改變物件 entity state

		/// <summary>
		/// 設定EntityState
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="entity"></param>
		/// <param name="state"></param>
		public void SetEntityState<T>(T entity, EntityState state) where T : class
		{
			_context.Entry(entity).State = state;
		}

		/// <summary>
		/// 設定EntityState(多筆)
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="entities"></param>
		/// <param name="state"></param>
		public void SetEntitiesState<T>(IEnumerable<T> entities, EntityState state) where T : class
		{
			if (entities != null)
			{
				foreach (T entity in entities)
				{
					SetEntityState(entity, state);
				}
			}
		}

		#endregion


		/// <summary>
		/// 建立Transaction
		/// </summary>
		/// <returns></returns>
		public IDbContextTransaction GetTransaction()
		{
			return _context.Database.BeginTransaction();
		}

		/// <summary> 
		///  純 SQL 執行
		/// </summary>
		/// <param name="query">sql 語法</param>
		/// <returns></returns>
		public async Task<bool> ExecuteSqlCommand(string query)
		{
			try
			{
				await _context.Database.ExecuteSqlRawAsync(query);
				return true;
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		/// <summary> 
		///  在 Transaction 中鎖定 Table，不可查詢/修改
		/// </summary>
		/// <typeparam name="T">資料庫 Table 型別</typeparam>
		/// <param name="transaction"></param>
		/// <returns></returns>
		public async Task<bool> TableLock<T>(IDbContextTransaction transaction) where T : class
		{
			try
			{
				// 傳入型別名稱
				string table_name = typeof(T).Name;

				_context.Database.UseTransaction(transaction.GetDbTransaction());

				await ExecuteSqlCommand(String.Format("SELECT * FROM {0} WITH (TABLOCKX)", table_name));

				return true;
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		/// <summary> 
		///  在 Transaction 中鎖定 Row(s)，不可查詢/修改
		/// </summary>
		/// <typeparam name="T">資料庫 Table 型別</typeparam>
		/// <param name="condition">where 條件，ex: where UserID = 'U000000001' </param>
		/// <returns></returns>
		public async Task<bool> RowLock<T>(string condition) where T : class
		{
			try
			{
				// 傳入型別名稱
				string table_name = typeof(T).Name;

				await ExecuteSqlCommand(String.Format("SELECT * FROM {0} WITH(updlock, rowlock, holdlock) {1}", table_name, condition));

				return true;
			}
			catch (Exception ex)
			{
				throw;
			}
		}
	}
}
