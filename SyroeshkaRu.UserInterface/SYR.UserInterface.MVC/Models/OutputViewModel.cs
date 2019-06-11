using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SYR.Core.BusinessLogic.Interface;
using SYR.Core.BusinessLogic.Mapping;
using SYR.Core.BusinessLogic.Service;
using SYR.Core.BusinessLogic.ViewModel;
using SYR.Core.DomainModel;

namespace SYR.UserInterface.MVC.Models
{
	public class OutputViewModel
	{
		private readonly ISyroeshkaRu _db;

		public OutputViewModel(ISyroeshkaRu db)
		{
			_db = db;
		}


	    //public ICollection<StoragesProductsViewModel> StoragesMerge
	    //{
	    //    get { return (ICollection<StoragesProductsViewModel>)_db.GetGroupComplete(); }
	    //}

		public ICollection<StoragesProductsViewModel> StoragesProducts => (ICollection<StoragesProductsViewModel>)_db.GetComplete();
	}
}
