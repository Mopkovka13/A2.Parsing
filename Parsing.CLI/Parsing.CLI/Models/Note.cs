
using System;

namespace Parsing.CLI.Models
{
    public class Note
    {
        public string sellerName { get; set; }
        public string sellerInn { get; set; }
        public string buyerName { get; set; }
        public string buyerInn { get; set; }
        public string woodVolumeBuyer { get; set; }
        public string woodVolumeSeller { get; set; }
        public DateTime dealDate { get; set; }
        public string dealNumber { get; set; }
        public string __typename { get; set; }
    }
}
