using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SYR.Core.BusinessLogic.Interface;
using SYR.Core.BusinessLogic.Mapping;
using SYR.Core.BusinessLogic.ViewModel;
using SYR.Core.DomainModel;
using SYR.Core.DomainModel.Client;
using SYR.Core.DomainModel.System;

namespace SYR.Core.BusinessLogic.Service
{
	//TODO Почистить класс
	public class SyroeshkaRuService : ISyroeshkaRu
	{
		private readonly ModelContext _db;
		private readonly IMapper _mapper;
		private readonly UserManager<Users> _userManager;

		public SyroeshkaRuService()
		{
			_db = new ModelContext();
			_mapper = new Configure().Configuration().CreateMapper();
		}

		public SyroeshkaRuService(UserManager<Users> userManager)
		{
			_db = new ModelContext();
			_mapper = new Configure().Configuration().CreateMapper();
			_userManager = userManager;
		}

		public object GetMenu()
		{
			return _mapper.Map<ICollection<Menu>, ICollection<MenuViewModel>>(_db.Menu.ToList());
		}

		public object GetMenuController(Guid? parentId)
		{
			return _mapper.Map<Menu, MenuViewModel>(_db.Menu.FirstOrDefault(i => i.Id == parentId)).Name;
		}

		public object GetUsers(ClaimsPrincipal user)
		{
			return _userManager.GetUserId(user);
		}

		/* #region GetMenu */
		/* #endregion */

		#region Дерево Склады -> Продукты

		//public object GetComplete() =>
		//	_mapper.Map<ICollection<StoragesProducts>, ICollection<StoragesProductsViewModel>>(_db.StoragesProducts
		//		.Include(i => i.Storage)
		//		.Include(n => n.Product)
		//		.ThenInclude(m => m.CategoriesProducts)
		//		.ThenInclude(m => m.Category)
		//		.GroupBy(i => i.StorageId)
		//		.Select(i => i.First())
		//		.ToList());

		public object GetComplete() =>
			_mapper.Map<ICollection<StoragesCategories>, ICollection<StoragesCategoriesViewModel>>(
				_db.StoragesCategories.Include(i => i.Category).GroupBy(i => i.StorageId).Select(i => i.First()).ToList());

		#endregion Дерево Склады -> Продукты

		#region Категории

		public object GetCategories(Guid storageId)
		{
			return _mapper.Map<ICollection<Categories>, ICollection<CategoriesViewModel>>(_db.StoragesCategories.Where(i => i.StorageId == storageId).Select(n => n.Category).ToList());
		}

		#endregion Категории

		#region Продукты

		public object GetProducts(Guid? productId)
		{
			if (productId != Guid.Empty)
			{
				return _mapper.Map<Products, ProductsViewModel>(_db.Products.Find(productId));
			}
			else
			{
				return _mapper.Map<ICollection<Products>, ICollection<ProductsViewModel>>(_db.Products.ToList());
			}
		}

		#endregion Продукты

		#region Продукты категории

		public object GetProducts(Guid categoryId)
		{
			return _mapper.Map<ICollection<Products>, ICollection<ProductsViewModel>>(_db.StoragesProducts
				.Where(i => i.StorageId == categoryId).Select(n => n.Product).ToList());
		}

		#endregion Продукты категории

		#region Продукты категории

		//public object GetCategoryProducts(Guid categoryId)
		//{
		//	return _mapper.Map<ICollection<Products>, ICollection<ProductsViewModel>>(_db.CategoriesProducts
		//		.Where(i => i.CategoryId == categoryId).Select(n => n.Product).ToList());
		//}

		#endregion Продукты категории

		//TODO: Разобраться с продуктами

		#region Параметры склада

		public object GetStorageProperties(Guid productId)
		{
			return _mapper.Map<StoragesProducts, StoragesProductsViewModel>(_db.StoragesProducts.FirstOrDefault(i => i.ProductId == productId));
		}

		#endregion Параметры склада

		#region Параметры категории

		//public object GetCategoryProperties(Guid productId)
		//{
		//	return _mapper.Map<ICollection<CategoriesProducts>, ICollection<CategoriesProductsViewModel>>(
		//		_db.CategoriesProducts.Where(i => i.ProductId == productId).ToList());
		//}

		#endregion Параметры категории
	}
}
