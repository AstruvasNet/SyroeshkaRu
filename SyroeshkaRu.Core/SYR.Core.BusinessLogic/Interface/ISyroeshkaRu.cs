using System;
using System.Security.Claims;

namespace SYR.Core.BusinessLogic.Interface
{
	public interface ISyroeshkaRu
	{
		object GetComplete();

		object GetUsers(ClaimsPrincipal user);

		object GetCategories(Guid storageId);

		object GetProducts(Guid? productId);

		object GetProducts(Guid categoryId);

		//object GetCategoryProducts(Guid categoryId);

		object GetStorageProperties(Guid productId);

		//object GetCategoryProperties(Guid productId);

		object GetMenu();

		object GetMenuController(Guid? parentId);
	}
}