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
    public class ProjectServiceTests
    {
        private readonly Mock<IProjectRepository> _repoMock;
        private readonly ProjectService _service;

        public ProjectServiceTests()
        {
            _repoMock = new Mock<IProjectRepository>();
            _service = new ProjectService(_repoMock.Object);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnCreatedProjectDto()
        {
            // Arrange
            var ownerId = Guid.NewGuid();
            var createDto = new CreateProjectDto
            {
                Name = "Test Project",
                Description = "Desc"
            };
            var storedEntity = new Project
            {
                Id = Guid.NewGuid(),
                Name = createDto.Name,
                Description = createDto.Description,
                OwnerId = ownerId
            };
            _repoMock
                .Setup(r => r.AddAsync(It.IsAny<Project>()))
                .ReturnsAsync(storedEntity);

            // Act
            var result = await _service.CreateAsync(createDto, ownerId);

            // Assert
            Assert.Equal(storedEntity.Id, result.Id);
            Assert.Equal(createDto.Name, result.Name);
            Assert.Equal(createDto.Description, result.Description);
            _repoMock.Verify(r => r.AddAsync(It.Is<Project>(
                p => p.Name == createDto.Name &&
                     p.Description == createDto.Description &&
                     p.OwnerId == ownerId
            )), Times.Once);
        }

        [Fact]
        public async Task ListForUserAsync_ShouldReturnProjectDtoList()
        {
            // Arrange
            var ownerId = Guid.NewGuid();
            var entities = new List<Project>
            {
                new Project { Id = Guid.NewGuid(), Name = "P1", Description = "D1", OwnerId = ownerId },
                new Project { Id = Guid.NewGuid(), Name = "P2", Description = "D2", OwnerId = ownerId }
            };
            _repoMock
                .Setup(r => r.ListByOwnerAsync(ownerId))
                .ReturnsAsync(entities);

            // Act
            var list = await _service.ListForUserAsync(ownerId);

            // Assert
            Assert.Collection(list,
                dto => {
                    Assert.Equal("P1", dto.Name);
                    Assert.Equal("D1", dto.Description);
                },
                dto => {
                    Assert.Equal("P2", dto.Name);
                    Assert.Equal("D2", dto.Description);
                });
            _repoMock.Verify(r => r.ListByOwnerAsync(ownerId), Times.Once);
        }
    }
}
