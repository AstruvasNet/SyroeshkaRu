using System;
using System.Security.Claims;
using SYR.Core.DomainModel.System;

namespace SYR.Core.BusinessLogic.Interface
{
	public interface ISyroeshkaRu
	{
		object GetComplete();
		object GetUsers(ClaimsPrincipal user);
		object GetStorages(Guid? storageId = null);
		object GetCategories(Guid storageId);
		object GetProducts(Guid? productId);
		object GetProducts(Guid categoryId);
		object GetCategoryProducts(Guid categoryId);
		object GetStorageProperties(Guid productId);
		object GetCategoryProperties(Guid productId);
		object GetMainMenu();
		object GetSecondMenu(string parentId);
		object GetMenuController(Guid? parentId);
	}
}
