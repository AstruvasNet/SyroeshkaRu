using System;
using System.Linq;
using System.Security.Principal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SYR.Core.BusinessLogic.Interface;
using SYR.Core.BusinessLogic.Service;
using SYR.Core.BusinessLogic.ViewModel;
using SYR.Tests.UnitTests.Common;
using SYR.UserInterface.MVC.Areas.Admin.Controllers;
using Xunit;

namespace SYR.Tests.UnitTests.Controllers {
	public class RootControllerTests {
		private readonly Mock<IAdmin> _mock = new Mock<IAdmin>();
		private readonly Repository _repo = new Repository(new AdminService());
		private readonly IAdmin _db = new AdminService();

		#region GetSequrityController

		[Fact]
		public void GetSequrityControllerForAdmin()
		{
			var user = new GenericIdentity("156b8d8d-5db8-4ba1-971a-06177cf8172e");

			var context = new ControllerContext
			{
				HttpContext = new DefaultHttpContext
				{
					User = new GenericPrincipal(user, null)
				}
			};

			var controller = new RootController(_mock.Object) {ControllerContext = context,};
			Assert.Equal(expected: ((SequrityProfilesViewModel)_db.GetSequrityProfiles("root"))
				.SequrityRoles.Select(i => i.RoleId),
				_db.GetUserRoles(controller.User.Identity.Name));
		}

		[Fact]
		public void GetSequrityControllerForCustomer()
		{
			var user = new GenericIdentity("d50817f9-1713-4358-8206-992810f49796");

			var context = new ControllerContext
			{
				HttpContext = new DefaultHttpContext
				{
					User = new GenericPrincipal(user, null)
				}
			};

			var controller = new RootController(_mock.Object) {ControllerContext = context,};
			Assert.NotEqual(expected: ((SequrityProfilesViewModel)_db.GetSequrityProfiles("root"))
				.SequrityRoles.Select(i => i.RoleId),
				_db.GetUserRoles(controller.User.Identity.Name));
		}

		#endregion

		#region GetTestUsers

		[Fact]
		public async void GetUsersReturnsViewResultListOrItemOrNotFound()
		{
			var id = new Guid("2d65fadd-cdfc-4a67-9e85-2b200bd269da");
			//var id = Guid.Empty;
			_mock.Setup(repo => repo.GetUsers(id)).Returns(_repo.GetTestUsers(id));

			var controller = new RootController(_mock.Object);
			var result = await controller.Index(id);
			var viewResult = Assert.IsType<ViewResult>(result);

			Assert.NotNull(viewResult.Model);
			Assert.Equal(viewResult.Model.ToString().GetHashCode(),
				_repo.GetTestUsers(id).ToString().GetHashCode());
		}

		#endregion

		#region GetTestStorages

		[Fact]
		public async void GetStoragesReturnsViewResultListOrItemOrNotFound()
		{
			var id = new Guid("11687780-3255-4B10-85A6-8A562E5DAD2F");
			//var id = Guid.Empty;
			_mock.Setup(repo => repo.GetStorages(id)).Returns(_repo.GetTestStorages(id));

			var controller = new RootController(_mock.Object);
			var result = await controller.Storages(id);
			var viewResult = Assert.IsType<ViewResult>(result);

			Assert.NotNull(viewResult.Model);
			Assert.Equal(viewResult.Model.ToString().GetHashCode(),
				_repo.GetTestStorages(id).ToString().GetHashCode());
		}

		#endregion
	}
}