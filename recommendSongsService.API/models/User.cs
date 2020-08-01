
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace recommendSongsService.Model
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Email { get; set; }
        public string Password { get; set; }
        public string Hometown { get; set; }
        public Note PersonalNotes  { get; set; }
    }
}