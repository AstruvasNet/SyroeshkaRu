using System;
using SYR.Core.BusinessLogic.Service;

namespace SYR.Tests.UnitTests.Common {
	public static class Repository {

		#region GetTestStoragesElements

		public static object GetTestStorages(Guid? id = null)
		{
			var db = new AdminService();

			return id == Guid.Empty ? db.GetStorages() : db.GetStorages(id);
		}

		#endregion
	}
}