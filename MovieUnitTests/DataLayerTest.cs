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
            Repository<Genre> genreController = new Repository<Genre>(new IMDBContext());
            int expectedValue = 27;

            int actualValue = (await genreController.GetAll()).Count;

            Assert.Equal(actualValue, expectedValue);
        }

        [Fact]
        public async Task CallAPI_UserReposity_Func_Get_ShouldGetUser()
        {
            UserRepository userRepository = new UserRepository(new IMDBContext());
            var expectedValue = new { Id = 1, Email = "test@ruc.dk", FirstName = "Harry" };

            var actualValue = await userRepository.Get(1);

            Assert.Equal(actualValue.Id, expectedValue.Id);
            Assert.Equal(actualValue.Email, expectedValue.Email);
            Assert.Equal(actualValue.FirstName, expectedValue.FirstName);
        }

        [Fact]
        public async Task CallAPI_UserReposity_Func_Add_ShouldAddUser()
        {
            UserRepository userRepository = new UserRepository(new IMDBContext());
            bool expectedValue = true;

            bool actualValue = await userRepository.Add(new User() { Id = default, Email = "test@ruc99.dk", FirstName = "Harry", Password = "bigsecrets" });
            User user = (await userRepository.GetAll()).Where(x => x.Email == "test@ruc99.dk").First();
            Assert.Equal(expectedValue, actualValue);

            // clean up
            await userRepository.Delete(user.Id);
        }

        [Fact]
        public async Task CallAPI_UserReposity_Func_Update_ShouldUpdateUser()
        {
            UserRepository userRepository = new UserRepository(new IMDBContext());
            var expectedValue = new { Id = 2, Email = "test@ruc22.dk", FirstName = "Harry potter", Password = "bigsecrets" };

            await userRepository.Update(new User() { Id = 2, Email = "test@ruc22.dk", FirstName = "Harry potter", Password = "bigsecrets" });
            var actualValue = await userRepository.Get(2);

            Assert.Equal(actualValue.Id, expectedValue.Id);
            Assert.Equal(actualValue.Email, expectedValue.Email);
            Assert.Equal(actualValue.FirstName, expectedValue.FirstName);

            // clean up
            await userRepository.Update(new User() { Id = 2, Email = "test@ruc22.dk", FirstName = "Harry", Password = "bigsecrets" });
        }

        [Fact]
        public async Task CallAPI_UserReposity_Func_Delete_ShouldDeleteUser()
        {
            UserRepository userRepository = new UserRepository(new IMDBContext());

            await userRepository.Add(new User() { Id = default, Email = "test@ruc22.dk", FirstName = "Harry potter", Password = "bigsecrets" });
            User user = (await userRepository.GetAll()).Where(x => x.Email == "test@ruc22.dk").First();
            var success = await userRepository.Delete(user.Id);

            Assert.True(success);
        }

        //[Fact]
        //public async Task CallAPI_UserReposity_Func_Delete_ShouldDeleteUser() // call sql :(
        //{
        //    UserRepository userRepository = new UserRepository(new IMDBContext());

        //    await userRepository.Add(new User() { Id = default, Email = "test@ruc22.dk", FirstName = "Harry potter", Password = "bigsecrets" });
        //    User user = (await userRepository.GetAll()).Where(x => x.Email == "test@ruc22.dk").First();
        //    var success = await userRepository.Delete(user.Id);

        //    Assert.True(success);
        //}
    }
}