using Moq;
using TrainComponentManagement.Application.DTOs;
using TrainComponentManagement.Application.Services;
using TrainComponentManagement.Domain.Entities;
using TrainComponentManagement.Domain.Interfaces;

namespace TrainComponentManagement.Tests.Application
{
    public class ComponentServiceTests
    {
        private readonly Mock<IComponentRepository> _repositoryMock;
        private readonly ComponentService _service;

        public ComponentServiceTests()
        {
            _repositoryMock = new Mock<IComponentRepository>();
            _service = new ComponentService(_repositoryMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsComponents()
        {
            // Arrange
            var components = new List<Component>
            {
                new Component { Id = 1, Name = "Engine", UniqueNumber = "ENG123" },
                new Component { Id = 2, Name = "Wheel", UniqueNumber = "WHL123" }
            };

            _repositoryMock.Setup(r => r.GetAllAsync(null))
                .ReturnsAsync(components);

            // Act
            var result = await _service.GetAllAsync(null);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsComponentDto_WhenExists()
        {
            // Arrange
            var component = new Component
            {
                Id = 1,
                Name = "Engine",
                UniqueNumber = "ENG123",
                CanAssignQuantity = false
            };

            _repositoryMock.Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(component);

            // Act
            var result = await _service.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Engine", result!.Name);
            Assert.Equal("ENG123", result.UniqueNumber);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsNull_WhenNotFound()
        {
            // Arrange
            _repositoryMock.Setup(r => r.GetByIdAsync(99))
                .ReturnsAsync((Component?)null);

            // Act
            var result = await _service.GetByIdAsync(99);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateAsync_ThrowsException_WhenUniqueNumberExists()
        {
            // Arrange
            var dto = new CreateComponentDto
            {
                Name = "Door",
                UniqueNumber = "DR001",
                CanAssignQuantity = true
            };

            _repositoryMock.Setup(r => r.ExistsByUniqueNumberAsync("DR001"))
                .ReturnsAsync(true);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _service.CreateAsync(dto));
        }

        [Fact]
        public async Task CreateAsync_ReturnsCreatedDto_WhenValid()
        {
            // Arrange
            var dto = new CreateComponentDto
            {
                Name = "Wheel",
                UniqueNumber = "WHL123",
                CanAssignQuantity = true
            };

            var createdComponent = new Component
            {
                Id = 1,
                Name = "Wheel",
                UniqueNumber = "WHL123",
                CanAssignQuantity = true,
                Quantity = 0
            };

            _repositoryMock.Setup(r => r.ExistsByUniqueNumberAsync("WHL123"))
                .ReturnsAsync(false);

            _repositoryMock.Setup(r => r.AddAsync(It.IsAny<Component>()))
                .ReturnsAsync(createdComponent);

            // Act
            var result = await _service.CreateAsync(dto);

            // Assert
            Assert.Equal(1, result.Id);
            Assert.Equal("Wheel", result.Name);
            Assert.Equal("WHL123", result.UniqueNumber);
        }

        [Fact]
        public async Task UpdateQuantityAsync_ReturnsTrue_WhenRepositoryReturnsTrue()
        {
            // Arrange
            _repositoryMock.Setup(r => r.UpdateQuantityAsync(1, 5))
                .ReturnsAsync(true);

            // Act
            var result = await _service.UpdateQuantityAsync(1, 5);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task UpdateQuantityAsync_ReturnsFalse_WhenRepositoryReturnsFalse()
        {
            // Arrange
            _repositoryMock.Setup(r => r.UpdateQuantityAsync(1, -5))
                .ReturnsAsync(false);

            // Act
            var result = await _service.UpdateQuantityAsync(1, -5);

            // Assert
            Assert.False(result);
        }
    }
}
