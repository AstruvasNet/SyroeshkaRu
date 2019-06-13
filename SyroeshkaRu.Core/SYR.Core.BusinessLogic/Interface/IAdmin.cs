using System;

namespace SYR.Core.BusinessLogic.Interface
{
	public interface IAdmin
	{
		/// <summary>
		/// Коллекция пользователя
		/// </summary>
		/// <param name="id">Id пользователя</param>
		/// <returns>GetUsers(), GetUsers(Id)</returns>
		/// <see cref="GetUsers(string)"/>
		object GetUsers(string id = null);
		object GetUsers(int page, int pageSize);
		object GetStorages(Guid? storageId = null);
		object GetSequrityProfiles(Guid? id = null);
		object GetRoles();
		object GetUserRoles(string user);
		object GetHistory(int page, int pageSize);
		object GetStorages(int page, int pageSize);
	}
}
