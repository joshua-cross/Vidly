using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class MembershipType
    {
        public byte Id { get; set; }
        public short SignUpFee { get; set; }
        public byte DurationInMonths { get; set; }
        public byte DiscountRate { get; set; }
        public string DiscountName { get; set; }

        /*can be used by the custom attributes instead of using magic numbers*/
        public static readonly byte Unknown = 0;
        public static readonly byte PayAsYouGo = 1;
    }
}