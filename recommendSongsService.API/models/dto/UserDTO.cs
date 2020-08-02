using System.Collections.Generic;

namespace recommendSongsService.API.models.dto
{
    public class UserDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Hometown { get; set; }
        public List<NoteDTO> PersonalNotes { get; set; }
    }
}