namespace Entities.Dtos
{
    public class UserChangePassDto
    {
        public int UserId { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
