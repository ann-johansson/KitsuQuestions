using KitsuQuestions.Application.Common.Exceptions;
using KitsuQuestions.Application.DTOs.Goals;
using KitsuQuestions.Application.Interfaces.Repositories;
using KitsuQuestions.Application.Services;
using KitsuQuestions.Domain.Entities;
using KitsuQuestions.Domain.Enums;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;

namespace KitsuQuestions.Tests.Services
{
    public class GoalServiceTests
    {
        [Fact]
        public async Task CreateAsync_WithValidGoal_ReturnsGoalWithToLearnStatus()
        {
            // Arrange — build a fake repository and the service that uses it
            var goalRepo = Substitute.For<IGoalRepository>();
            var service = new GoalService(goalRepo);
            var dto = new CreateGoalDto { Title = "Learn C#" };

            // Act — call the method under test
            var result = await service.CreateAsync(dto);

            // Assert — check the returned DTO looks right
            Assert.Equal("Learn C#", result.Title);
            Assert.Equal("ToLearn", result.Status);

            // Also assert that the service actually used the repository correctly
            await goalRepo.Received(1).AddAsync(Arg.Any<Goal>());
            await goalRepo.Received(1).SaveChangesAsync();
        }

        [Fact]
        public async Task CreateAsync_WithEmptyTitle_ThrowsBusinessRuleException()
        {
            // Arrange
            var goalRepo = Substitute.For<IGoalRepository>();
            var service = new GoalService(goalRepo);
            var dto = new CreateGoalDto { Title = "" };

            // Act + Assert
            await Assert.ThrowsAsync<BusinessRuleException>(() => service.CreateAsync(dto));
        }

        [Fact]
        public async Task GetByIdAsync_WhenGoalMissing_ThrowsNotFoundException()
        {
            // Arrange — tell the mock to return null when asked for any id
            var goalRepo = Substitute.For<IGoalRepository>();
            goalRepo.GetByIdWithDetailsAsync(Arg.Any<int>()).Returns((Goal?)null);
            var service = new GoalService(goalRepo);

            // Act + Assert
            await Assert.ThrowsAsync<NotFoundException>(() => service.GetByIdAsync(999));
        }

        [Fact]
        public async Task MoveToLearningAsync_WhenUnderCap_SetsStatusToLearning()
        {
            // Arrange — mock returns a ToLearn goal and a Learning count of 3 (under the cap of 8)
            var goalRepo = Substitute.For<IGoalRepository>();
            var existing = new Goal { Id = 1, Title = "Learn", Status = GoalStatus.ToLearn };
            goalRepo.GetByIdAsync(1).Returns(existing);
            goalRepo.CountByStatusAsync(GoalStatus.Learning).Returns(3);
            var service = new GoalService(goalRepo);

            // Act
            var result = await service.MoveToLearningAsync(1);

            // Assert
            Assert.Equal("Learning", result.Status);
        }

        [Fact]
        public async Task MoveToLearningAsync_WhenCapReached_ThrowsBusinessRuleException()
        {
            // Arrange — Learning count is already at the cap
            var goalRepo = Substitute.For<IGoalRepository>();
            var existing = new Goal { Id = 1, Title = "Learn", Status = GoalStatus.ToLearn };
            goalRepo.GetByIdAsync(1).Returns(existing);
            goalRepo.CountByStatusAsync(GoalStatus.Learning).Returns(8);
            var service = new GoalService(goalRepo);

            // Act + Assert
            await Assert.ThrowsAsync<BusinessRuleException>(() => service.MoveToLearningAsync(1));
        }

        [Fact]
        public async Task MoveToLearningAsync_WhenGoalFulfilled_ThrowsBusinessRuleException()
        {
            // Arrange — fulfilled goals cannot go back to Learning
            var goalRepo = Substitute.For<IGoalRepository>();
            var fulfilled = new Goal { Id = 1, Title = "Learn", Status = GoalStatus.Fulfilled };
            goalRepo.GetByIdAsync(1).Returns(fulfilled);
            var service = new GoalService(goalRepo);

            // Act + Assert
            await Assert.ThrowsAsync<BusinessRuleException>(() => service.MoveToLearningAsync(1));
        }

        [Fact]
        public async Task MoveToToLearnAsync_ResetsStatusAndClearsFulfilledAt()
        {
            // Arrange — a previously fulfilled goal being reset
            var goalRepo = Substitute.For<IGoalRepository>();
            var fulfilled = new Goal
            {
                Id = 1,
                Title = "Learn",
                Status = GoalStatus.Fulfilled,
                FulfilledAt = DateTime.UtcNow
            };
            goalRepo.GetByIdAsync(1).Returns(fulfilled);
            var service = new GoalService(goalRepo);

            // Act
            var result = await service.MoveToToLearnAsync(1);

            // Assert — both the status and the timestamp must be cleared
            Assert.Equal("ToLearn", result.Status);
            Assert.Null(result.FulfilledAt);
        }

        [Fact]
        public async Task DeleteAsync_WhenGoalMissing_ThrowsNotFoundException()
        {
            // Arrange
            var goalRepo = Substitute.For<IGoalRepository>();
            goalRepo.GetByIdAsync(Arg.Any<int>()).Returns((Goal?)null);
            var service = new GoalService(goalRepo);

            // Act + Assert
            await Assert.ThrowsAsync<NotFoundException>(() => service.DeleteAsync(999));
        }
    }
}
