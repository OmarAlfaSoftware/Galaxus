using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxusIntegration.Application.DTOs.ProductDTOs
{
    public class PriceDataDTO
    {
        public string ProviderKey { get; set; }
        public decimal PriceWithoutVAT { get; set; }
        public decimal PriceWithVAT { get; set; }
        public decimal VAT { get; set; }

        private PriceDataDTO(string providerKey, decimal priceWithoutVAT, decimal priceWithVat, decimal vat)
        {
            ProviderKey = providerKey;
            PriceWithoutVAT = priceWithoutVAT;
            PriceWithVAT = priceWithVat;
            VAT = vat;
        }

        public class PriceDataDTOBuilder
        {
            private string _providerKey;
            private decimal _priceWithoutVAT;
            private decimal _priceWithVAT;
            private decimal _vat;

            public PriceDataDTOBuilder WithProviderKey(string providerKey)
            {
                _providerKey = providerKey;
                return this;
            }

            public PriceDataDTOBuilder WithPriceWithoutVAT(decimal priceWithoutVAT)
            {
                _priceWithoutVAT = priceWithoutVAT;
                return this;
            }

            public PriceDataDTOBuilder WithPriceWithVAT(decimal priceWithVAT)
            {
                _priceWithVAT = priceWithVAT;
                return this;
            }

            public PriceDataDTOBuilder WithVAT(decimal vat)
            {
                _vat = vat;
                return this;
            }

            public PriceDataDTO Build()
            {
                if (string.IsNullOrEmpty(_providerKey))
                {
                    throw new InvalidOperationException("ProviderKey must be provided.");
                }

                if (_priceWithoutVAT < 0)
                {
                    throw new InvalidOperationException("PriceWithoutVAT must be non-negative.");
                }

                if (_vat < 0)
                {
                    throw new InvalidOperationException("VAT must be non-negative.");
                }

                return new PriceDataDTO(_providerKey, _priceWithoutVAT, _priceWithVAT, _vat);
            }
        }

        public List<string> HeadList()
        {
            List<string> result = new();
            result.Add("ProviderKey");
            result.Add("PriceWithoutVAT");
            result.Add("PriceWithVAT");
            result.Add("VAT");
            return result;
        }

        public List<string> DataToString()
        {
            List<string> result = new();
            result.Add(ProviderKey);
            result.Add(PriceWithoutVAT.ToString());
            result.Add(PriceWithVAT.ToString());
            result.Add(VAT.ToString());
            return result;
        }
    }
}
