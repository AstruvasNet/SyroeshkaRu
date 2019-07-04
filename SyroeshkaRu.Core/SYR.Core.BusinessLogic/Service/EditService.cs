using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SYR.Core.BusinessLogic.Helpers;
using SYR.Core.BusinessLogic.Interface;
using SYR.Core.BusinessLogic.ViewModel;
using SYR.Core.DomainModel;
using SYR.Core.DomainModel.System;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace SYR.Core.BusinessLogic.Service
{
	public class EditService : IEdit
	{
		private readonly UserManager<Users> _userManager;
		private readonly IHttpContextAccessor _user;
		private readonly ModelContext _db;
		private SqlParameter _output;
		private string _input;

		private SqlParameter Output()
		{
			return new SqlParameter
			{
				ParameterName = "@output",
				SqlDbType = SqlDbType.VarChar,
				Direction = ParameterDirection.Output,
				Size = 2000
			};
		}

		private string Input(ICollection<SqlParameter> param)
		{
			return param.Aggregate("", (current, t) => current + $"{t.ParameterName} = '{t.SourceColumn}',");
		}

		public EditService(ModelContext db, IHttpContextAccessor user, UserManager<Users> userManager)
		{
			_db = db;
			_user = user;
			_userManager = userManager;
		}

		private void InputOutputInit(ICollection<SqlParameter> param)
		{
			_output = Output();
			_input = Input(param);
		}

		public object EditProducts(ProductsViewModel model)
		{
			var param = new List<SqlParameter>
			{
				new SqlParameter("@id", model.Id),
				new SqlParameter("@name", model.Name),
				new SqlParameter("@description", model.Description),
				new SqlParameter("@keywords", model.Keywords),
				new SqlParameter("@content", model.Content),
				new SqlParameter("@isNew", model.IsNew)
			};
			var output = Output();
			_db.Database.ExecuteSqlCommand($"dbo.sp_EDIT_Products {Input(param)} @output OUT",
				output);
			return output.Value;
		}

		public object EditStoragesProducts(StoragesProductsViewModel model)
		{
			var param = new List<SqlParameter>
			{
				new SqlParameter("@storageId", model.StorageId),
				new SqlParameter("@productId", model.ProductId),
				new SqlParameter("@price", model.Price),
				new SqlParameter("@quantity", model.Quantity)
			};
			try
			{
				_db.Database.ExecuteSqlCommand($"dbo._sp_EDIT_StorageProducts {param}, @output OUT",
					_output);
				return _output.Value;
			}
			catch (SqlException ex)
			{
				return ex;
			}
		}

		public object EditStorages(StoragesViewModel model)
		{
			var param = new List<SqlParameter>
			{
				new SqlParameter("@id", SqlDbType.UniqueIdentifier, 37, model.Id.ToString()),
				new SqlParameter("@title", SqlDbType.NVarChar, Int32.MaxValue, model.Title),
				new SqlParameter("@description", SqlDbType.NVarChar, 50, model.Description),
				new SqlParameter("@isDefault", SqlDbType.Bit, 1, Convert.ToBoolean(model.IsDefault).ToString()),
				new SqlParameter("@dateTime", SqlDbType.Int, 10, DisplayValues.ConvertToTimestamp(DateTime.Now).ToString()),
				new SqlParameter("@userId", SqlDbType.UniqueIdentifier, 37, _userManager.GetUserId(_user.HttpContext.User))
			};

			InputOutputInit(param);
			try
			{
				if (!string.IsNullOrEmpty(model.Title))
					_db.Database.ExecuteSqlCommand($"[dbo].[root_sp_EDIT_Storages] {_input} @output = @output OUT", _output);
				return _output.Value;
			}
			catch (Exception ex)
			{
				return $"0//{ex.Message}";
			}
		}

		//TODO При полном удалении элемента удалять историю
		public object DeleteStorages(StoragesViewModel model)
		{
			var param = new List<SqlParameter>
			{
				new SqlParameter("@id", SqlDbType.UniqueIdentifier, 37, model.Id.ToString()),
				new SqlParameter("@title", SqlDbType.NVarChar, Int32.MaxValue, model.Title),
				new SqlParameter("@dateTime", SqlDbType.Int, 10, DisplayValues.ConvertToTimestamp(DateTime.UtcNow).ToString()),
				new SqlParameter("@flag", SqlDbType.Int, 10, 0.ToString()),
				new SqlParameter("@userId", SqlDbType.UniqueIdentifier, 37,
					_userManager.GetUserId(_user.HttpContext.User))
			};

			InputOutputInit(param);
			try
			{
				_db.Database.ExecuteSqlCommand($"[dbo].[root_sp_EDIT_Storages] {_input} @output = @output OUT", _output);
				return _output.Value;
			}
			catch (Exception ex)
			{
				return $"0//{ex.Message}";
			}
		}
	}
}