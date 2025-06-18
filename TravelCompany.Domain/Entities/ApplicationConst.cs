using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelCompany.Domain.Entities
{
    public class ApplicationConst
    {

        [Key]
        public string Name { get; set; } = null!;
        public string Value { get; set; } = null!;

    }
}
