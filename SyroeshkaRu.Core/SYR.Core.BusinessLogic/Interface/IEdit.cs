using SYR.Core.BusinessLogic.ViewModel;

namespace SYR.Core.BusinessLogic.Interface
{
	public interface IEdit
	{
		object EditProducts(ProductsViewModel model);

		object EditStoragesProducts(StoragesProductsViewModel model);

		object EditStorages(StoragesViewModel model);

		object DeleteStorages(StoragesViewModel model);
	}
}