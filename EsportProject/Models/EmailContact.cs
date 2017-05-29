using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EsportProject.Models
{
    public class EmailContact
    {
        [Required]
        [EmailAddress]
        public string EmailAdress { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        public string FirstName { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        public string LastName { get; set; }

        [Required]
        [StringLength(200)]
        [DataType(DataType.MultilineText)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        public string Message { get; set; }

        [StringLength(30)]
        public string Info { get; set; }

    }
}
