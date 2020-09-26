using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VehicleDemo.Common.Helpers;
using VehicleDemo.Model;
using VehicleDemo.Model.Common;
using VehicleDemo.Repository.Common;
using Xunit;

namespace VehicleDemo.Service.Tests
{
    public class VehicleModelServiceTests
    {
        private readonly VehicleModelService _sut;
        private readonly Mock<IVehicleModelRepository> _modelRepoMock = new Mock<IVehicleModelRepository>();
        private readonly Mock<IUnitOfWork> _uowMock = new Mock<IUnitOfWork>();

        public VehicleModelServiceTests()
        {
            _sut = new VehicleModelService(_uowMock.Object, _modelRepoMock.Object);
        }
        [Fact]
        public async Task ShouldReturnModelList()
        {
            //Arrange
            var vehicleModels = new List<IVehicleModel>()
            {
                new VehicleModel()
                {
                    Id = 1,
                    Name = "123",
                    Abrv = "Mercedes",
                    MakeId = 1
                },
                new VehicleModel()
                {
                    Id = 2,
                    Name = "A5",
                    Abrv = "Audi",
                    MakeId = 2
                },
            }.AsEnumerable();

            string searchString = "";
            string sortBy = "";
            int page = 0;

            VehicleFilters filters = new VehicleFilters(searchString);
            VehicleSorting sorting = new VehicleSorting(sortBy);
            VehiclePaging paging = new VehiclePaging(page);

            _modelRepoMock.Setup(x => x.GetAll(filters, sorting, paging)).Returns(Task.FromResult(vehicleModels));
            //Act
            var result = await _sut.GetVehicleModels(filters, sorting, paging);

            //Assert
            result.Should().BeEquivalentTo(vehicleModels);
        }

        [Fact]
        public async Task ShouldReturnEmptyModelList()
        {
            //Arrange
            var vehicleModels = new List<IVehicleModel>().AsEnumerable();

            string searchString = "";
            string sortBy = "";
            int page = 0;

            VehicleFilters filters = new VehicleFilters(searchString);
            VehicleSorting sorting = new VehicleSorting(sortBy);
            VehiclePaging paging = new VehiclePaging(page);

            _modelRepoMock.Setup(x => x.GetAll(filters, sorting, paging)).Returns(Task.FromResult(vehicleModels));
            //Act
            var result = await _sut.GetVehicleModels(filters, sorting, paging);

            //Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task ShouldReturnVehicleModel()
        {
            //Arrange
            var modelId = 2;
            var vehicleModel = new VehicleModel()
            {
                Id = modelId,
                Name = "Car Model",
                Abrv = "CM",
                MakeId = 1
            };

            _modelRepoMock.Setup(x => x.FindById(modelId)).ReturnsAsync(vehicleModel);

            //Act
            IVehicleModel model = await _sut.FindVehicleModel(modelId);

            //Assert
            //Assert.Equal(modelId, model.Id);
            model.Id.Should().Be(modelId);
        }

        [Fact]
        public async Task ShouldCreateNewModel()
        {
            //Arrange
            var vehicleModel = new VehicleModel()
            {
                Id = 1,
                Name = "Car Model",
                Abrv = "CM",
                MakeId = 1
            };

            _modelRepoMock.Setup(x => x.Create(vehicleModel)).ReturnsAsync(true);

            //Act
            var result = await _sut.CreateVehicleModel(vehicleModel);

            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task ShouldEditModel()
        {
            //Arrange
            var vehicleModel = new VehicleModel()
            {
                Id = 1,
                Name = "Car Model",
                Abrv = "CM",
                MakeId = 1
            };

            _modelRepoMock.Setup(x =>x.Edit(vehicleModel)).ReturnsAsync(true);

            //Act
            var result = await _sut.EditVehicleModel(vehicleModel);

            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task ShouldDeleteModel()
        {
            //Arrange
            var modelId = 2;

            _modelRepoMock.Setup(x => x.Delete(modelId)).ReturnsAsync(true);

            //Act
            var result = await _sut.DeleteVehicleModel(modelId);

            //Assert
            result.Should().BeTrue();
        }
    }
}