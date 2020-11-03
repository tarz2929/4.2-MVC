using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FirstASP.Models
{
    [Table("emailaddress")]
    public class EMailAddress
    {
        [Key]
        [Column(TypeName = "int(10)")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Column(TypeName = "int(10)")]
        public int PersonID { get; set; }

        [Column(TypeName = "varchar(40)")]
        public string Address { get; set; }

        [ForeignKey(nameof(PersonID))]
        [InverseProperty(nameof(Models.Person.EMailAddresses))]
        public virtual Person Person { get; set; }
    }
}