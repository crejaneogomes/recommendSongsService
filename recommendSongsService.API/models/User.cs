using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections;
using System.Collections.Generic;

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
        public virtual List<Note> PersonalNotes  { get; set; }
    }
}