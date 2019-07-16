using System;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SYR.Core.BusinessLogic.Interface;
using SYR.Tests.UnitTests.Common;
using SYR.UserInterface.MVC.Areas.Admin.Controllers;
using Xunit;

namespace SYR.Tests.UnitTests.Controllers {
	public class RootControllerTests {
		private readonly Mock<IAdmin> _mock = new Mock<IAdmin>();

		#region GetTestStorages

		[Fact]
		public async void GetStoragesReturnsViewResultListOrItemOrNotFound()
		{
			var id = new Guid("11687780-3255-4B10-85A6-8A562E5DAD2F");
			//var id = Guid.Empty;
			_mock.Setup(repo => repo.GetStorages(id)).Returns(Repository.GetTestStorages(id));
			var controller = new RootController(_mock.Object);

			var result = await controller.Storages(id);
			var viewResult = Assert.IsType<ViewResult>(result);

			Assert.NotEqual(Repository.GetTestStorages(id), viewResult.Model);
		}

		#endregion
	}
}