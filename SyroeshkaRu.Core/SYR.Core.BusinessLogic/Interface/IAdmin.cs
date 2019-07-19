using System;
using System.Reflection;

namespace SYR.Core.BusinessLogic.Interface
{
	public interface IAdmin
	{
		/// <summary>
		/// Коллекция пользователя
		/// </summary>
		/// <param name="id">Id пользователя</param>
		/// <returns>GetUsers(), GetUsers(Id)</returns>
		/// <see cref="GetUsers(Guid or null)"/>
		object GetUsers(Guid? id = null);

		object GetUsers(int page, int pageSize);

		object GetStorages(Guid? storageId = null);

		object GetSequrityProfiles(Guid? id = null);

		object GetSequrityProfiles(string name);

		object GetSequrityProfiles(Assembly assembly);

		object GetRoles();

		object GetUserRoles(string user);

		object GetHistory(int page, int pageSize);

		object GetStorages(int page, int pageSize);

		object GetMainMenu();

		object GetSecondMenu(string parentId);
	}
}