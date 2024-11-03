using MovieDataLayer;
using System.Text;
using System.Net;
using System.Text.Json;

namespace MovieUnitTests
{
    public class WebAPI_Test
    {

        readonly string baseUrl = "https://localhost:7154/api/user";
        Random rng = new Random();

        [Fact]
        public async Task CallWebService_API_UserController_Func_GetAll_ShouldReturnOk()
        {
            HttpClient httpClient = new HttpClient();

            using HttpResponseMessage response = await httpClient.GetAsync(baseUrl);

            HttpStatusCode statusCode = response.EnsureSuccessStatusCode().StatusCode;

            Assert.Equal(statusCode, HttpStatusCode.OK);

            // var jsonResponse = await response.Content.ReadAsStringAsync();
        }

        [Fact]
        public async Task CallWebService_API_UserController_Func_GetById_ShouldReturnNotFound()
        {
            HttpClient httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("id", "9999");

            using HttpResponseMessage response = await httpClient.GetAsync(baseUrl +"/user-profile");

            HttpStatusCode statusCode = response.StatusCode;

            Assert.Equal(statusCode, HttpStatusCode.NotFound);
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

            using HttpResponseMessage response = await httpClient.PostAsync(baseUrl, jsonContent);

            HttpStatusCode statusCode = response.StatusCode;

            Assert.Equal(statusCode, HttpStatusCode.BadRequest);

            // var jsonResponse = await response.Content.ReadAsStringAsync();
        }


        [Fact]
        public async Task CallWebService_API_UserController_Func_Put_ShouldReturnOK()
        {
            HttpClient httpClient = new HttpClient();

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
            var usersList = JsonSerializer.Deserialize<List<User>>(usersJsonList, new JsonSerializerOptions
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

            using HttpResponseMessage responseMessage = await httpClient.PutAsync(baseUrl, jsonContent);

            using HttpResponseMessage getAllUsers_numberTwo = await httpClient.GetAsync(baseUrl);

            var updatedUsersJsonList = await getAllUsers_numberTwo.Content.ReadAsStringAsync();
            var updatedUserList = JsonSerializer.Deserialize<List<User>>(updatedUsersJsonList, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var userToDelete = updatedUserList.Where(x => x.Email == "testharryp@ruc.dk").FirstOrDefault();

            Assert.Equal("Harry potter", userToDelete.FirstName);

            // clean up
            httpClient.DefaultRequestHeaders.Remove("id");
            httpClient.DefaultRequestHeaders.Add("id", userToDelete.Id.ToString());
            using HttpResponseMessage responseMessage3 = await httpClient.DeleteAsync(baseUrl);
        }

        [Fact]
        public async Task CallWebService_API_UserController_Func_Delete_ShouldReturnNotFound()
        {
            HttpClient httpClient = new HttpClient();
            string userID = "404";
            httpClient.DefaultRequestHeaders.Add("id", userID);
            using HttpResponseMessage response = await httpClient.DeleteAsync(baseUrl);

            HttpStatusCode statusCode = response.StatusCode;

            Assert.Equal(HttpStatusCode.NotFound, statusCode);
        }

    }
}
