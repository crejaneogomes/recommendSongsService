
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections;
using System.Collections.Generic;

namespace recommendSongsService.Model
{
    [Table("Notes")]
    public class Note
    {
        [Key]
        public int Id { get; set; }
        public string NoteData { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}