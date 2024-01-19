using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace YourNamespace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly string functionUrl = "https://funfunctionforlog.azurewebsites.net/api/StoreTextInBlob?code=FoKyxwfSmDF2_wYe8XQp7xzKt4tABZ7BQ6H2Aw1oymSgAzFuKN6XMg==S"; // Replace with your Azure Function URL

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            // Mock user validation (replace this with your actual user validation logic)
            if (loginModel.Username == "string" && loginModel.Password == "string")
            {
                string successMessage = $"Login successful for user: {loginModel.Username}";

                using (var client = new HttpClient())
                {
                    try
                    {
                        var content = new StringContent(successMessage, Encoding.UTF8, "application/json");
                        var response = await client.PostAsync(functionUrl, content);

                        if (response.IsSuccessStatusCode)
                        {
                            return Ok("Login successful and success message stored in Blob Storage");
                        }
                        else
                        {
                            return StatusCode((int)response.StatusCode, "Failed to trigger Azure Function");
                        }
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(500, $"Error: {ex.Message}");
                    }
                }
            }

            return BadRequest("Invalid credentials");
        }
    }

        public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
