using System;
using System.Collections.Generic;
using System.Linq;
using Appeals.Controllers;
using Appeals.Models;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Appeals.Tests
{
    public class AppealControllerTests
    {
        [Fact]
        public void ReturningWithSortedAppeals()
        {
            var controller = new AppealController();
            var appeals = new List<Appeal>
            {
                new Appeal { AppealId = 1, DueDateTime = DateTime.Now.AddDays(1) },
                new Appeal { AppealId = 2, DueDateTime = DateTime.Now.AddDays(2) }
            };
            AppealController._appeals.AddRange(appeals);

            var result = controller.Index() as ViewResult;
            var model = result.Model as List<Appeal>;

            Assert.NotNull(result);
            Assert.Equal(2, model.Count);
            Assert.Equal(1, model[0].AppealId);
            Assert.Equal(2, model[1].AppealId);

            AppealController._appeals.Clear();
        }

        [Fact]
        public void Create_POST_ReturnsRedirectToIndexWhenModelIsValid()
        {
            var controller = new AppealController();
            var appeal = new Appeal { AppealId = 1, DueDateTime = DateTime.Now.AddDays(1) };

            var result = controller.Create(appeal) as RedirectToActionResult;

            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);

            AppealController._appeals.Clear();
        }

        [Fact]
        public void Create_POST_ReturnsViewWithModelWhenModelIsInvalid()
        {
            var controller = new AppealController();
            var appeal = new Appeal();

            controller.ModelState.AddModelError("Test", "Test error");

            var result = controller.Create(appeal) as ViewResult;

            Assert.NotNull(result);
            Assert.Equal(appeal, result.Model);

            AppealController._appeals.Clear();
        }

        [Fact]
        public void Delete_RemovesAppealAndRedirectsToIndex()
        {
            var controller = new AppealController();
            var appeal = new Appeal { AppealId = 1, DueDateTime = DateTime.Now.AddDays(1) };
            AppealController._appeals.Add(appeal);

            var result = controller.Delete(1) as RedirectToActionResult;

            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            Assert.DoesNotContain(appeal, AppealController._appeals);

            AppealController._appeals.Clear();
        }
    }
}