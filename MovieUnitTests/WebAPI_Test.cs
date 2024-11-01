using MovieDataLayer.DataService.UserFrameworkRepository;
using MovieDataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Text.Json;

namespace MovieUnitTests
{
    public class WebAPI_Test
    {
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

            using HttpResponseMessage response = await httpClient.GetAsync("https://localhost:7154/api/users/9999");

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


    }
}
