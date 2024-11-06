using MovieDataLayer;
using System.Text;
using System.Net;
using System.Text.Json;

namespace MovieUnitTests
{
    public class WebApiTest
    {

        readonly string baseUrl = "https://localhost:7154/api/user";

        [Fact]
        public async Task CallWebService_API_UserController_Func_GetAll_ShouldReturnOk()
        {
            // Arrange
            HttpClient httpClient = new HttpClient();
            await Util.UserLoginHelper(httpClient);
            HttpStatusCode expectedValue = HttpStatusCode.OK;

            // Act
            using HttpResponseMessage response = await httpClient.GetAsync(baseUrl);
            HttpStatusCode actualValue = response.EnsureSuccessStatusCode().StatusCode;

            // Assert
            Assert.Equal(expectedValue, actualValue);

        }

        [Fact]
        public async Task CallWebService_API_UserController_Func_GetById_ShouldReturnNotFound()
        {
            // Arrange
            HttpClient httpClient = new HttpClient();
            await Util.UserLoginHelper(httpClient);
            HttpStatusCode expectedValue = HttpStatusCode.NotFound;
            string userId = "9999";

            // Act
            httpClient.DefaultRequestHeaders.Add("id", userId);
            using HttpResponseMessage response = await httpClient.GetAsync(baseUrl + "/user-profile");
            HttpStatusCode actualValue = response.StatusCode;

            // Assert
            Assert.Equal(expectedValue, actualValue);

        }

        [Fact]
        public async Task CallWebService_API_UserController_Func_Add_ShouldReturnBadRequest()
        {
            // Arrange
            HttpClient httpClient = new HttpClient();
            await Util.UserLoginHelper(httpClient);
            HttpStatusCode expectedValue = HttpStatusCode.BadRequest;

            using StringContent jsonContent = new(
            JsonSerializer.Serialize(new
            {
                email = "steven@ruc.dk",
                firstName = "stevenAlligator123794579843579873459743859353537594327597435345345345435455373773473747348374873438748737438",
                password = "*34vrj'^2F_,d"
            }),
            Encoding.UTF8,
            "application/json");

            // Act
            using HttpResponseMessage response = await httpClient.PostAsync(baseUrl, jsonContent);
            HttpStatusCode actualValue = response.StatusCode;

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }


        [Fact]
        public async Task CallWebService_API_UserController_Func_Put_ShouldReturnOK()
        {
            // Arrange
            HttpClient httpClient = new HttpClient();
            await Util.UserLoginHelper(httpClient);
            string expectedValue = "Harry potter";

            // check initial value
            using StringContent jsonContent_Harry = new(
                JsonSerializer.Serialize(new
                {
                    email = "testharryp@ruc.dk",
                    firstName = "Harry",
                    password = "Harry1234"
                }),
                Encoding.UTF8,
                "application/json");


            using HttpResponseMessage setHarry = await httpClient.PostAsync(baseUrl, jsonContent_Harry);
            using HttpResponseMessage getAllUsers = await httpClient.GetAsync(baseUrl);

            var usersJsonList = await getAllUsers.Content.ReadAsStringAsync();
            var usersList = JsonSerializer.Deserialize<List<UserModel>>(usersJsonList, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var userUpdate = usersList.Where(x => x.Email == "testharryp@ruc.dk").FirstOrDefault();

            using StringContent jsonContent = new(
                JsonSerializer.Serialize(new
                {
                    email = "testharryp@ruc.dk",
                    firstName = "Harry potter",
                    password = "Harry1234"
                }),
                Encoding.UTF8,
                "application/json");

            httpClient.DefaultRequestHeaders.Add("id", userUpdate.Id.ToString());

            // Act
            using HttpResponseMessage responseMessage = await httpClient.PutAsync(baseUrl, jsonContent);

            using HttpResponseMessage getAllUsers_numberTwo = await httpClient.GetAsync(baseUrl);

            var updatedUsersJsonList = await getAllUsers_numberTwo.Content.ReadAsStringAsync();
            var updatedUserList = JsonSerializer.Deserialize<List<UserModel>>(updatedUsersJsonList, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var userToDelete = updatedUserList.Where(x => x.Email == "testharryp@ruc.dk").FirstOrDefault();

            // Assert
            Assert.Equal(expectedValue, userToDelete.FirstName);

            // Clean up
            httpClient.DefaultRequestHeaders.Remove("id");
            httpClient.DefaultRequestHeaders.Add("id", userToDelete.Id.ToString());
            using HttpResponseMessage responseMessage3 = await httpClient.DeleteAsync(baseUrl);
        }

        [Fact]
        public async Task CallWebService_API_UserController_Func_Delete_ShouldReturnNotFound()
        {
            // Arrange
            HttpClient httpClient = new HttpClient();
            await Util.UserLoginHelper(httpClient);
            HttpStatusCode expectedValue = HttpStatusCode.NotFound;
            string userID = "404";

            // Act
            httpClient.DefaultRequestHeaders.Add("id", userID);
            using HttpResponseMessage response = await httpClient.DeleteAsync(baseUrl);
            HttpStatusCode actualValue = response.StatusCode;

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }



    }
    //Helper class and methods
    public static class Util
    {
        public static async Task UserLoginHelper(HttpClient httpClient) //Helper method to login user, and return the JWT token
        {
            StringContent userLoginToJSON = new(
                        JsonSerializer.Serialize(new
                        {
                            email = "test@ruc.dk",
                            password = "bigsecrets"
                        }),
                        Encoding.UTF8,
                        "application/json");

            var loginUserResponse = await httpClient.PostAsync("https://localhost:7154/api/user/login", userLoginToJSON);

            loginUserResponse.EnsureSuccessStatusCode(); //Throws error if login was unsuccessful
            var token = await loginUserResponse.Content.ReadAsStringAsync();

            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token); //Adds JWT token to Request Header

        }
    }
}