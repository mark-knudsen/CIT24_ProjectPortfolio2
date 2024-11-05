using MovieDataLayer.DataService.UserFrameworkRepository;

using MovieDataLayer;
using System.Collections;
using MovieWebApi.Controllers;
using MovieDataLayer.DataService;
using MovieDataLayer.Models.IMDB_Models;

namespace MovieUnitTests
{
    public class DataLayerTest
    {
        [Fact]
        public async Task CallAPI_GenreReposity_Func_Get_ShouldGetAllGenres()
        {
            // Arrange
            Repository<Genre> genreController = new Repository<Genre>(new IMDBContext());
            int expectedValue = 27;

            // Act
            int actualValue = (await genreController.GetAll()).Count;

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public async Task CallAPI_UserReposity_Func_Get_ShouldGetUser()
        {
            // Arrange
            UserRepository userRepository = new UserRepository(new IMDBContext());
            var expectedValue = new { Id = 1, Email = "test@ruc.dk", FirstName = "Harry" };

            // Act
            var actualValue = await userRepository.Get(1);

            // Assert
            Assert.Equal(expectedValue.Id, actualValue.Id);
            Assert.Equal(expectedValue.Email, actualValue.Email);
            Assert.Equal(expectedValue.FirstName, actualValue.FirstName);
        }

        [Fact]
        public async Task CallAPI_UserReposity_Func_Add_ShouldAddUser()
        {
            // Arrange
            UserRepository userRepository = new UserRepository(new IMDBContext());

            // Act
            bool success = await userRepository.Add(new User() { Id = default, Email = "test@ruc99.dk", FirstName = "Bob", Password = "12345" });

            // Assert
            Assert.True(success);

            // Clean up
            User user = (await userRepository.GetAll()).Where(x => x.Email == "test@ruc99.dk").First();
            await userRepository.Delete(user.Id);
        }

        [Fact]
        public async Task CallAPI_UserReposity_Func_Update_ShouldUpdateUser()
        {
            // Arrange
            IMDBContext imdbContext = new IMDBContext();
            UserRepository userRepository = new UserRepository(imdbContext);
            var expectedValue = new { Id = 1, Email = "test@ruc.dk", FirstName = "Harry potter", Password = "bigsecrets" };

            // Act
            await userRepository.Update(new User() { Id = 1, Email = "test@ruc.dk", FirstName = "Harry potter", Password = "bigsecrets" });
            var actualValue = await userRepository.Get(1);

            // Assert
            Assert.Equal(expectedValue.Id, actualValue.Id);
            Assert.Equal(expectedValue.Email, actualValue.Email);
            Assert.Equal(expectedValue.FirstName, actualValue.FirstName);

            // Clean up
            imdbContext.Dispose();
            imdbContext = new IMDBContext();
            userRepository = new UserRepository(imdbContext);
            await userRepository.Update(new User() { Id = 1, Email = "test@ruc.dk", FirstName = "Harry", Password = "bigsecrets" });
        }



        [Fact]
        public async Task CallAPI_UserReposity_Func_Delete_ShouldDeleteUser()
        {
            // Arrange
            UserRepository userRepository = new UserRepository(new IMDBContext());
            await userRepository.Add(new User() { Id = default, Email = "bobby@ruc22.dk", FirstName = "Bobby", Password = "12345" });
            User user = (await userRepository.GetAll()).Where(x => x.Email == "bobby@ruc22.dk").First();

            // Act
            var success = await userRepository.Delete(user.Id);

            // Assert
            Assert.True(success);
        }

    }
}