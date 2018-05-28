using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformationSecurity.Models
{
    public class Role
    {
        public long Id { get; set; }

        public string Name { get; set; }

        [Required]
        public int Priority { get; set; }

        public long? LimitTime { get; set; }

        //[Required, ForeignKey("AllowedDayz")]
        public int AllowedDays { get; set; }

        //[DisplayFormat(DataFormatString = "{0:HH:MM:SS}", ApplyFormatInEditMode = true)]
        //[DataType(DataType.Time)]
        //public Nullable<DateTime> StartTime { get; set; }

        [DisplayFormat(DataFormatString = "{0:HH:MM:SS}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Time)]
        public Nullable<DateTime> AllowedTime { get; set; }

    }
}
