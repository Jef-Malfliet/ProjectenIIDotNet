using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace G19.Models.ViewModels {
    public class _CommentsViewModel {
        [Required]
        public string Comments { get; set; }
    }
}
