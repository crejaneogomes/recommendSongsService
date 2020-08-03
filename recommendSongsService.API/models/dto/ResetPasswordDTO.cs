namespace recommendSongsService.API.models.dto
{
    public class ResetPasswordDTO
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
        public string ForgotPasswordCode { get; set; }
    }
}