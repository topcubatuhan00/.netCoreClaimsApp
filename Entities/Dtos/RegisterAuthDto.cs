using Microsoft.AspNetCore.Http;

namespace Entities.Dtos
{
    public class RegisterAuthDto
    {
        public string? name { get; set; }
        public string? password { get; set; }

        public IFormFile image { get; set; }

    }
}
