
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace recommendSongsService.Model
{
    [Table("Notes")]
    public class Note
    {
        [Key]
        public int Id { get; set; }
        public string NoteData { get; set; }
    }
}