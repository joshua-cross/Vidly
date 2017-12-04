using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace Vidly.Models
{
    public class Genre
    {
        //property for the Id which is the primary key in the database.
        public byte Id { get; set; }

        //a sdtring that is required, and has a maximul length of 255 characters.
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        //the default ie. no genre selected is 0, so going to add byte to reflect that here.
        public static readonly byte Unknown = 0;
    }
}