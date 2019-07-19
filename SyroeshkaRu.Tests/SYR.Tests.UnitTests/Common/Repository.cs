using System;
using SYR.Core.BusinessLogic.Interface;
using SYR.Core.BusinessLogic.Service;

namespace SYR.Tests.UnitTests.Common {
	public class Repository {

		private readonly IAdmin _db;

		public Repository(IAdmin db)
		{
			_db = db;
		}

		#region GetUsersElements

		public object GetTestUsers(Guid? id = null)
		{
			return id == Guid.Empty ? _db.GetUsers() : _db.GetUsers(id);
		}

		#endregion

		#region GetTestStoragesElements

		public object GetTestStorages(Guid? id = null)
		{
			return id == Guid.Empty ? _db.GetStorages() : _db.GetStorages(id);
		}

		#endregion
	}
}