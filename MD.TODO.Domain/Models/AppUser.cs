using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MD.TODO.Domain.Models
{
    public class AppUser:IdentityUser
    {
        // Additional properties will go here

        public virtual ICollection<TodoItem> TodoItems { get; set; }
    }
}
