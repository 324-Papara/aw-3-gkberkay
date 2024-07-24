using Para.Base.Schema;
using System.Text.Json.Serialization;

namespace Para.Schema
{
    public class CustomerRequest : BaseRequest
    {
        [JsonIgnore]
        public int CustomerNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentityNumber { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
    }

    public class CustomerResponse : BaseResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentityNumber { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public int CustomerNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
    }

}
