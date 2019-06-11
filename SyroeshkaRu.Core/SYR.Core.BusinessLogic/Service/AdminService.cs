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

		public object GetUsers(string id)
		{
			if (string.IsNullOrEmpty(id))
			{
				return _mapper.Map<ICollection<Users>, ICollection<UsersViewModel>>(_db.Users.ToList());
			}

			return _mapper.Map<Users, UsersViewModel>(_db.Users.Find(id));
		}

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

		public object GetHistory()
		{
			return (from h in _db.History
				join s in _db.Storages on h.ItemId equals s.Id into g
				from history in g.DefaultIfEmpty()
				select new HistoryViewModel
				{
					Id = h.Id,
					Item = history.Title ?? "Удалено",
					DateIn = h.DateIn
				}).ToList();
		}
	}
}
