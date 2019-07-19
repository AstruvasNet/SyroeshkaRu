using System;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SYR.Core.BusinessLogic.Helpers;
using SYR.Core.BusinessLogic.Interface;
using SYR.Core.BusinessLogic.ViewModel;
using SYR.UserInterface.MVC.Areas.Admin.Controllers;
using Xunit;

namespace SYR.Tests.UnitTests.Controllers {
	public class ApiControllerTests {

		private readonly Mock<IEdit> _edit = new Mock<IEdit>();

		#region EditTestStorages

		[Fact]
		public async void AddStoragesReturnsAddStorage()
		{
			var controller = new ApiController(_edit.Object);
			var storage = new StoragesViewModel
			{
				Id = Guid.NewGuid(),
				Title = "Test",
				Description = "Test",
				DateTime = DisplayValues.ConvertToTimestamp(DateTime.Now),
			};

			if (string.IsNullOrEmpty(storage.Title))
				controller.ModelState.AddModelError("Title", "Required");

			if (string.IsNullOrEmpty(storage.Description))
				controller.ModelState.AddModelError("Description", "Required");

			if (storage.DateTime == 0)
				controller.ModelState.AddModelError("DateTime", "Required");

			var result = await controller.EditStorages(storage);
			var okResult = Assert.IsType<OkObjectResult>(result);

			Assert.NotNull(okResult);
			_edit.Verify(i => i.EditStorages(storage));
		}

		#endregion
	}
}