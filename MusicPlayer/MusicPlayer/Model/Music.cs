using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.Model
{
    public class Music
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Index { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string FilePath { get; set; }
        [NotMapped]
        public int Length { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
}
