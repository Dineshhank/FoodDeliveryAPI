using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Application.Features.Auth.DTOs
{
    public class GetProfileResponse
    {
        public Guid Id { get; set; }

        public string Fullname { get; set; } = null!;

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public bool Isactive { get; set; }

        public bool Isphoneverified { get; set; }

        public bool Isemailverified { get; set; }

        public DateTime? Lastloginat { get; set; }

        public DateTime Createdat { get; set; }

        public DateTime? Updatedat { get; set; }

        public DateTime? Deletedat { get; set; }

        public bool Isdeleted { get; set; }

        public DateOnly? Dateofbirth { get; set; }

        public bool? Isprofilecompleted { get; set; }
    }
}
