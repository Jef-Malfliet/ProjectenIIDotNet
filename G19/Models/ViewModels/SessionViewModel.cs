using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace G19.Models.ViewModels {
    public class SessionViewModel {
        
        [Required]
        [EnumDataType(typeof(FormuleEnum))]
        public FormuleEnum Formule { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Date{ get; set; }
    }
}
