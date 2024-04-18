﻿using AppealsProject.Controllers;
using AppealsProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace Tests
{
    public class AppealControllerTests
    {
        [Fact]
        public void Index_ReturnsSortedAppealsInView()
        {
            // Arrange
            var controller = new AppealController();
            var appeals = new List<Appeal>
            {
                new Appeal { AppealId = 1, DueDateTime = DateTime.Now.AddDays(1) },
                new Appeal { AppealId = 2, DueDateTime = DateTime.Now.AddDays(2) }
            };
            AppealController._appeals.AddRange(appeals);

            // Act
            var result = controller.Index() as ViewResult;
            var model = result?.Model as List<Appeal>;

            // Assert
            Assert.NotNull(result);
            if (model != null)
            {
                Assert.Equal(2, model.Count);
                Assert.Equal(1, model[0].AppealId);
                Assert.Equal(2, model[1].AppealId);
            }

            AppealController._appeals.Clear();
        }

        [Fact]
        public void Create_POST_ReturnsRedirectToIndexWhenModelIsValid()
        {
            // Arrange
            var controller = new AppealController();
            var appeal = new Appeal { AppealId = 1, DueDateTime = DateTime.Now.AddDays(1) };

            // Act
            var result = controller.Create(appeal) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);

            AppealController._appeals.Clear();
        }

        [Fact]
        public void Create_POST_ReturnsViewWithModelWhenModelIsInvalid()
        {
            // Arrange
            var controller = new AppealController();
            var appeal = new Appeal();

            // Simulating ModelState errors
            controller.ModelState.AddModelError("Test", "Test error");

            // Act
            var result = controller.Create(appeal) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(appeal, result.Model);

            AppealController._appeals.Clear();
        }

        [Fact]
        public void Delete_RemovesAppealAndRedirectsToIndex()
        {
            // Arrange
            var controller = new AppealController();
            var appeal = new Appeal { AppealId = 1, DueDateTime = DateTime.Now.AddDays(1) };
            AppealController._appeals.Add(appeal);

            // Act
            var result = controller.Delete(1) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            Assert.DoesNotContain(appeal, AppealController._appeals);

            AppealController._appeals.Clear();
        }
        [Fact]
        public void Delete_ReturnsNotFoundWhenAppealNotFound()
        {
            // Arrange
            var controller = new AppealController();
            var id = 1;

            // Act
            var result = controller.Delete(id) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }
    }
}