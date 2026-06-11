using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRNProductAssessment.Application.DTOs
{
    public class UpdateProductDto
    {
        public string ProductName { get; set; } = string.Empty;
        public string ModifiedBy { get; set; } = string.Empty;
    }
}
