using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FirstASP.Models
{
    // In-class practice: Fill out the manufacturer class to function as a basic model. Foreign key stuff not needed.

    // Challenge: Implement the foreign key.
    [Table("person")]
    class Person
    {
        public Person()
        {
            EMailAddresses = new HashSet<EMailAddress>();
        }

        [Key]
        [Column(TypeName = "int(10)")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string FirstName { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string LastName { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateOfBirth { get; set; }

        [InverseProperty(nameof(Models.EMailAddress.Person))]
        public virtual ICollection<EMailAddress> EMailAddresses { get; set; }
    }
}