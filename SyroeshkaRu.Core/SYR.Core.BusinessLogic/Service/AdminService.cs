using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using SYR.Core.BusinessLogic.Interface;
using SYR.Core.BusinessLogic.Mapping;
using SYR.Core.BusinessLogic.ViewModel;
using SYR.Core.DomainModel;
using SYR.Core.DomainModel.System;
using System;
using System.Collections.Generic;
using System.Linq;
using SYR.Core.DomainModel.Client;

namespace SYR.Core.BusinessLogic.Service
{
	[Authorize]
	public class AdminService : IAdmin
	{
		private readonly ModelContext _db;
		private readonly IMapper _mapper;

		public AdminService()
		{
			_db = new ModelContext();
			_mapper = new Configure().Configuration().CreateMapper();
		}

		#region Users

		public object GetUsers(string id = null)
		{
			if (string.IsNullOrEmpty(id))
			{
				return _mapper.Map<ICollection<Users>, ICollection<UsersViewModel>>(_db.Users.ToList());
			}

			return _mapper.Map<Users, UsersViewModel>(_db.Users.Find(id));
		}

		#endregion

		#region Склады

		public object GetStorages(Guid? storageId = null)
		{
			if (!string.IsNullOrEmpty(storageId.ToString()))
			{
				return _mapper.Map<Storages, StoragesViewModel>(_db.Storages.Find(storageId));
			}

			return _mapper.Map<ICollection<Storages>, ICollection<StoragesViewModel>>(_db.Storages
				.Include(i => i.Products)
				.ThenInclude(i => i.Product)
				.Include(i => i.Categories)
				.ToList());
		}

		#endregion

		public object GetSequrityProfiles(Guid? id)
		{
			if (!string.IsNullOrEmpty(id.ToString()))
				return _mapper.Map<SequrityProfiles, SequrityProfilesViewModel>(_db.SequrityProfiles
					.Include(i => i.SequrityRoles).FirstOrDefault(m => m.Id == id));
			return _mapper.Map<ICollection<SequrityProfiles>, ICollection<SequrityProfilesViewModel>>(
				_db.SequrityProfiles
					.Include(i => i.SequrityRoles)
					.ThenInclude(i => i.Roles).ToList());
		}

		public object GetRoles()
		{
			return _mapper.Map<ICollection<Roles>, ICollection<RolesViewModel>>(_db.Roles.ToList());
		}

		public object GetUserRoles(string id)
		{
			return _db.UserRoles.Where(i => i.UserId == id).Select(i => i.RoleId).ToList();
		}

		#region Paginations

		public object GetUsers(int page, int pageSize)
		{
			return new IndexViewModel
			{
				PageViewModel = new PageViewModel(((ICollection<UsersViewModel>)GetUsers()).Count, page, pageSize),
				ModelObject = ((ICollection<UsersViewModel>)GetUsers()).Skip((page - 1) * pageSize).Take(pageSize).ToList()
			};
		}

		public object GetStorages(int page, int pageSize)
		{
			return new IndexViewModel
			{
				PageViewModel = new PageViewModel(((ICollection<StoragesViewModel>)GetStorages()).Count, page, pageSize),
				ModelObject = ((ICollection<StoragesViewModel>)GetStorages()).Skip((page - 1) * pageSize).Take(pageSize).ToList()
			};
		}

		public object GetHistory(int page, int pageSize)
		{
			var model = _mapper.Map<ICollection<History>, ICollection<HistoryViewModel>>(_db.History.ToList());
			return (from h in _db.History
				join s in _db.Storages on h.ItemId equals s.Id into g
				from history in g.DefaultIfEmpty()
				select new IndexViewModel
				{
					PageViewModel = new PageViewModel(model.Count, page, pageSize),
					ModelObject = model.Skip((page - 1) * pageSize).Take(pageSize).ToList()
				}).FirstOrDefault();
		}

		#endregion

	}
}
