using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace recommendSongsService.Model
{
    [Table("Users")]
    public class User
    {
        [Key]
        [IgnoreDataMemberAttribute]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Hometown { get; set; }
        public virtual List<Note> PersonalNotes  { get; set; }
    }
}