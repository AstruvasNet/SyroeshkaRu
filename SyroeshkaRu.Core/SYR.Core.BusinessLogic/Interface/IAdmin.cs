using System;

namespace SYR.Core.BusinessLogic.Interface
{
	public interface IAdmin
	{
		object GetUsers(string id);
		object GetSequrityProfiles(Guid? id = null);
		object GetRoles();
		object GetUserRoles(string user);
		object GetHistory();
	}
}
