using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using ProjectTracker.Application.Dtos;
using ProjectTracker.Application.Services;
using ProjectTracker.Domain.Entities;
using ProjectTracker.Infrastructure.Repositories;
using Xunit;

namespace ProjectTracker.Tests.Services
{
    public class TaskServiceTests
    {
        private readonly Mock<ITaskRepository> _repoMock;
        private readonly TaskService _service;

        public TaskServiceTests()
        {
            _repoMock = new Mock<ITaskRepository>();
            _service = new TaskService(_repoMock.Object);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnCreatedTaskDto()
        {
            // Arrange
            var projectId = Guid.NewGuid();
            var createDto = new CreateTaskDto
            {
                Title = "Test Task",
                DueDate = DateTime.UtcNow.AddDays(1),
                Description = "Desc"
            };
            var storedEntity = new TaskItem
            {
                Id = Guid.NewGuid(),
                Title = createDto.Title,
                Description = createDto.Description,
                DueDate = createDto.DueDate,
                ProjectId = projectId,
                IsCompleted = false
            };
            _repoMock
                .Setup(r => r.AddAsync(It.IsAny<TaskItem>()))
                .ReturnsAsync(storedEntity);

            // Act
            var result = await _service.CreateAsync(projectId, createDto);

            // Assert
            Assert.Equal(storedEntity.Id, result.Id);
            Assert.Equal(createDto.Title, result.Title);
            Assert.Equal(createDto.Description, result.Description);
            Assert.Equal(createDto.DueDate, result.DueDate);
            Assert.False(result.IsCompleted);
            _repoMock.Verify(r => r.AddAsync(It.Is<TaskItem>(
                t => t.Title == createDto.Title &&
                     t.ProjectId == projectId
            )), Times.Once);
        }

        [Fact]
        public async Task ListByProjectAsync_ShouldReturnTaskDtoList()
        {
            // Arrange
            var projectId = Guid.NewGuid();
            var entities = new List<TaskItem>
            {
                new TaskItem { Id = Guid.NewGuid(), Title="T1", Description="D1", DueDate=DateTime.UtcNow.AddDays(1), IsCompleted=false, ProjectId=projectId },
                new TaskItem { Id = Guid.NewGuid(), Title="T2", Description="D2", DueDate=DateTime.UtcNow.AddDays(2), IsCompleted=true,  ProjectId=projectId }
            };
            _repoMock
                .Setup(r => r.ListByProjectAsync(projectId))
                .ReturnsAsync(entities);

            // Act
            var list = await _service.ListByProjectAsync(projectId);

            // Assert
            Assert.Collection(list,
                dto => {
                    Assert.Equal("T1", dto.Title);
                    Assert.False(dto.IsCompleted);
                },
                dto => {
                    Assert.Equal("T2", dto.Title);
                    Assert.True(dto.IsCompleted);
                });
            _repoMock.Verify(r => r.ListByProjectAsync(projectId), Times.Once);
        }
    }
}
