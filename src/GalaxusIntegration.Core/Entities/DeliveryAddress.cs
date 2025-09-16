/*
 * DeliveryAddress
        {
            Name = addr?.Name,
            Name2 = addr?.Name2,
            Name3 = addr?.Name3,
            Department = addr?.Department,
            ContactFirstName = addr?.ContactDetails?.FirstName,
            ContactLastName = addr?.ContactDetails?.ContactName,
            Street = addr?.Street,
            Zip = addr?.Zip,
            BoxNo = addr?.BoxNo,
            City = addr?.City,
            Country = addr?.Country,
            CountryCode = addr?.CountryCoded
        };
 */
namespace GalaxusIntegration.Core.Entities
{
    public class DeliveryAddress
    {
        public string Name { get; set; }
        public string Name2 { get; set; }
        public string Name3 { get; set; }
        public string Department { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }= string.Empty;
        public string Street {  get; set; }
        public string Zip {  get; set; }
        public string BoxNo { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
    }
}