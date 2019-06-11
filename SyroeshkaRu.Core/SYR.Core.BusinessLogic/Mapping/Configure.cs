using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using AutoMapper;
using SYR.Core.BusinessLogic.ViewModel;
using SYR.Core.DomainModel.Client;
using SYR.Core.DomainModel.System;

namespace SYR.Core.BusinessLogic.Mapping
{
	public class Configure
	{
		public MapperConfiguration Configuration()
		{
			return new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<Users, UsersViewModel>();
				cfg.CreateMap<UsersViewModel, Users>();

				cfg.CreateMap<Storages, StoragesViewModel>();
				cfg.CreateMap<StoragesViewModel, Storages>();

				cfg.CreateMap<Categories, CategoriesViewModel>();
				cfg.CreateMap<CategoriesViewModel, Categories>();

				cfg.CreateMap<Products, ProductsViewModel>();
				cfg.CreateMap<ProductsViewModel, Products>();

				cfg.CreateMap<StoragesProducts, StoragesProductsViewModel>();
				cfg.CreateMap<StoragesProductsViewModel, StoragesProducts>();

				cfg.CreateMap<SequrityProfiles, SequrityProfilesViewModel>();
				cfg.CreateMap<SequrityProfilesViewModel, SequrityProfiles>();

				cfg.CreateMap<SequrityRoles, SequrityRolesViewModel>();
				cfg.CreateMap<SequrityRolesViewModel, SequrityRoles>();

				cfg.CreateMap<Roles, RolesViewModel>();
				cfg.CreateMap<RolesViewModel, Roles>();

				cfg.CreateMap<Menu, MenuViewModel>();
				cfg.CreateMap<MenuViewModel, Menu>();

				cfg.CreateMap<History, HistoryViewModel>();
				cfg.CreateMap<HistoryViewModel, History>();
			});
		}
	}
}
