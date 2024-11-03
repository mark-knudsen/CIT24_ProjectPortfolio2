using MovieDataLayer;
using System.Text;
using System.Net;
using System.Text.Json;

namespace MovieUnitTests
{
    public class WebAPI_Test
    {
        Random rng = new Random();

        [Fact]
        public async Task CallWebService_API_UserController_Func_GetAll_ShouldReturnOk()
        {
            HttpClient httpClient = new HttpClient();

            using HttpResponseMessage response = await httpClient.GetAsync("https://localhost:7154/api/users");

            HttpStatusCode statusCode = response.EnsureSuccessStatusCode().StatusCode;

            Assert.Equal(statusCode, HttpStatusCode.OK);

            // var jsonResponse = await response.Content.ReadAsStringAsync();
        }

        [Fact]
        public async Task CallWebService_API_UserController_Func_GetById_ShouldReturnNotFound()
        {
            HttpClient httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("id", "1");

            using HttpResponseMessage response = await httpClient.GetAsync("https://localhost:7154/api/users/");

            HttpStatusCode statusCode = response.StatusCode;

            Assert.Equal(statusCode, HttpStatusCode.NotFound);

            // var jsonResponse = await response.Content.ReadAsStringAsync();
        }

        [Fact]
        public async Task CallWebService_API_UserController_Func_Add_ShouldReturnBadRequest()
        {
            HttpClient httpClient = new HttpClient();

            using StringContent jsonContent = new(
        JsonSerializer.Serialize(new
        {
            email = "steven@ruc.dk",
            firstName = "stevenAlligator123794579843579873459743859353537594327597435345345345435455373773473747348374873438748737438",
            password = "*34vrj'^2F_,d"
        }),
        Encoding.UTF8,
        "application/json");

            using HttpResponseMessage response = await httpClient.PostAsync("https://localhost:7154/api/users", jsonContent);

            HttpStatusCode statusCode = response.StatusCode;

            Assert.Equal(statusCode, HttpStatusCode.BadRequest);

            // var jsonResponse = await response.Content.ReadAsStringAsync();
        }


        [Fact]
        public async Task CallWebService_API_UserController_Func_Put2_ShouldReturnOK()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("id", "1");

            // check initial value
            using HttpResponseMessage getAllUsers = await httpClient.GetAsync("https://localhost:7154/api/users");

            var usersJson = await getAllUsers.Content.ReadAsStringAsync();
            var users = JsonSerializer.Deserialize<List<User>>(usersJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var user = users.Where(x => x.Email == "test@ruc.dk").FirstOrDefault();

            Assert.Equal("Harry", user.FirstName);


                        using StringContent jsonContent = new(
        JsonSerializer.Serialize(new
        {
            email = "test@ruc.dk",
            firstName = "Harry potter",
            password = "Harry1234"
        }),
        Encoding.UTF8,
        "application/json");

            using HttpResponseMessage responseMessage = await httpClient.PutAsync("https://localhost:7154/api/users", jsonContent);

            using HttpResponseMessage getAllUsers2 = await httpClient.GetAsync("https://localhost:7154/api/users");

            var usersJson2 = await getAllUsers2.Content.ReadAsStringAsync();
            var users2 = JsonSerializer.Deserialize<List<User>>(usersJson2, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var user2 = users.Where(x => x.Email == "test@ruc.dk").FirstOrDefault();

            Assert.Equal("Harry potter", user2.FirstName);
        }

        [Fact]
        public async Task CallWebService_API_UserController_Func_Put_ShouldReturnOK()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("id", "1");
            using HttpResponseMessage getAllUsers = await httpClient.GetAsync("https://localhost:7154/api/users");

            var usersJson = await getAllUsers.Content.ReadAsStringAsync();
            var users = JsonSerializer.Deserialize<List<User>>(usersJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (users == null || users.Count == 0)
            {
                throw new Exception("No users found");
            }

            int randomId = rng.Next(1, users.Count); // get random index from User table
            int userID = users[randomId].Id; // get User ID
            string newName = "New Name";

            User originalUser = users[randomId]; // updated value
            //User updateUser = new User
            //{
            //    Id = originalUser.Id,
            //    Email = originalUser.Email,
            //    FirstName = "New Name", // Updated first name
            //    Password = originalUser.Password // Use original password or change if needed
            //};

                 using StringContent jsonContent = new(
        JsonSerializer.Serialize(new
        {
            email = "test@ruc.dk",
            firstName = "Harry potter",
            password = "Harry1234"
        }),
        Encoding.UTF8,
        "application/json");

            //using StringContent updateUserContent = new StringContent(
            //    JsonSerializer.Serialize(updateUser), Encoding.UTF8, "application/json"
            //);


            // Create the PUT request without including the Id in the URL
            var request = new HttpRequestMessage(HttpMethod.Put, "https://localhost:7154/api/users")
            {
                Content = jsonContent
            };

            // Add the Id to the headers
            //request.Headers.Add("User-Id", "20");

            // Send the request
            using HttpResponseMessage response = await httpClient.SendAsync(request);


            var updatedUserJson = await getAllUsers.Content.ReadAsStringAsync();
            var updatedUserResponse = JsonSerializer.Deserialize<User>(updatedUserJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            HttpStatusCode statusCode = response.StatusCode;

            Assert.Equal(statusCode, HttpStatusCode.OK);
            //Assert.NotEqual(updatedUserResponse.FirstName, newName);


            // clean up
            //using StringContent updateUserContentBack = new StringContent(
            //    JsonSerializer.Serialize(originalUser), Encoding.UTF8, "application/json"
            // );




            //            using StringContent jsonContent = new(
            //JsonSerializer.Serialize(originalUser),
            //Encoding.UTF8,
            //"application/json");


            //            // Create the PUT request without including the Id in the URL
            //            var request2 = await httpClient.PutAsync("https://localhost:7154/api/users", jsonContent);


            // Add the Id to the headers
            //request.Headers.Add("User-Id", updateUser.Id.ToString());

            // Send the request
            //using HttpResponseMessage response2 = await httpClient.SendAsync(request2);

        }

        [Fact]
        public async Task CallWebService_API_UserController_Func_Delete_ShouldReturnNotFound()
        {
            HttpClient httpClient = new HttpClient();
            int userID = 404;
            using HttpResponseMessage response = await httpClient.DeleteAsync($"https://localhost:7154/api/users/{userID}");

            HttpStatusCode statusCode = response.StatusCode;

            // Output the status code for debugging purposes
            Console.WriteLine($"Response status code: {statusCode}");

            Assert.Equal(HttpStatusCode.NotFound, statusCode);
        }

    }
}
