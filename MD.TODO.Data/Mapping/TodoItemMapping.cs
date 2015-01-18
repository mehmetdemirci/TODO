using MD.TODO.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MD.TODO.Data.Mapping
{
    public class TodoItemMapping : BaseEntityMapping<TodoItem>
    {
        public TodoItemMapping()
        {
            // Table Name
            ToTable("T_TODO_ITEM");

            // Table Specific Properties
            Property(u => u.Title).HasColumnName("NAME");
        }
    }
}
