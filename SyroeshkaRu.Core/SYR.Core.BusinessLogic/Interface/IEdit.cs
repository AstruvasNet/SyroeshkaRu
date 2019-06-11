using SYR.Core.BusinessLogic.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

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
