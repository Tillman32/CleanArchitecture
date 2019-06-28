using CleanArchitecture.Core.Data;
using CleanArchitecture.Core.Data.Entity;
using CleanArchitecture.Test.Data.Entity;
using Moq;
using System;
using Xunit;

namespace CleanArchitecture.Test.Data
{
    /// <summary>
    /// Test class for IGenericRepository Class.
    /// </summary>
    public class GenericRepositoryTest
    {
        private readonly IEntity entity;
        private readonly Mock<IGenericRepository<IEntity>> mockRepository;

        /// <summary>
        /// Setup before each Test case.
        /// </summary>
        public GenericRepositoryTest()
        {
            entity = new SimpleEntity { Id = 2, DateCreated = DateTime.Now };
            mockRepository = new Mock<IGenericRepository<IEntity>>();
        }

        /// <summary>
        /// Test for Creating entities in the repository.
        /// </summary>
        [Fact]
        public void Create_Entity_Async()
        {
            //Arrange
            mockRepository.Setup(x => x.Create(entity));

            //Act
            var repository = mockRepository.Object;
            repository.Create(entity);

            //Assert
            mockRepository.Verify(x => x.Create(entity), Times.Once());

        }

        /// <summary>
        /// Test for get entity with Id.
        /// </summary>
        [Fact]
        public void Get_Entity_By_IdAsync()
        {
            //Arrange
            mockRepository
                .Setup(x => x.GetById(2))
                .ReturnsAsync(entity);

            //Act
            var repository = mockRepository.Object;
            repository.GetById(2);

            //Assert
            mockRepository.Verify(x => x.GetById(entity.Id), Times.Once());

        }

        /// <summary>
        /// Test for updating a entity.
        /// </summary>
        [Fact]
        public void Update_Entity_Async()
        {
            //Arrange
            mockRepository.Setup(x => x.Update(2, entity));

            //Act
            var repository = mockRepository.Object;
            repository.Update(2, entity);


            //Assert
            mockRepository.Verify(x => x.Update(2, entity), Times.Once());
        }

        /// <summary>
        /// Test for removing a entity.
        /// </summary>
        [Fact]
        public void Delete_Entity_Async()
        {
            //Arrange
            mockRepository.Setup(x => x.Delete(2));

            //Act
            var repository = mockRepository.Object;
            repository.Delete(2);

            //Assert
            mockRepository.Verify(x => x.Delete(entity.Id), Times.Once());
        }
    }
}
