using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyScriptureJournal.Models
{
    public class Scripture
    {
        public int ID { get; set; }

        [Display(Name = "Entry Date"), DataType(DataType.Date)]
        [Required]
        public DateTime EntryDate { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Book { get; set; }

        [Range(1, 200)]
        [Required]
        public int Chapter { get; set; }

        [Range(1, 200)]
        [Required]
        public int Verse { get; set; }

        [StringLength(500, MinimumLength = 1)]
        [Required]
        public string JournalEntry { get; set; }
    }
}
