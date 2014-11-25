using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Tingle_WebForms.Models
{
    public class SystemUsers
    {
        [Key]
        public int SystemUserID { get; set; }

        [MaxLength(50)]
        public string UserName { get; set; }

        [MaxLength(50)]
        public string DisplayName { get; set; }

        public virtual UserRoles UserRole { get; set; }

        public Int16 Status { get; set; }

        public SystemUsers()
        {
            Status = 1;
        }

    }
}