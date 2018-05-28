using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformationSecurity.Models
{
    public class Level
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public int? CountOfEnter { get; set; }

        [DisplayFormat(DataFormatString = "{0:HH:MM:SS}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Time)]
        public Nullable<DateTime> StartTime { get; set; }

        [DisplayFormat(DataFormatString = "{0:HH:MM:SS}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Time)]
        public Nullable<DateTime> EndTime { get; set; }

    }
}
