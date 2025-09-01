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
        public double PriceWithoutVAT { get; set; }
        public double PriceWithVAT { get; set; }
        public double VAT { get; set; }

        private PriceDataDTO(string providerKey, double priceWithoutVAT, double priceWithVat, double vat)
        {
            ProviderKey = providerKey;
            PriceWithoutVAT = priceWithoutVAT;
            PriceWithVAT = priceWithVat;
            VAT = vat;
        }

        public class PriceDataDTOBuilder
        {
            private string _providerKey;
            private double _priceWithoutVAT;
            private double _priceWithVAT;
            private double _vat;

            public PriceDataDTOBuilder WithProviderKey(string providerKey)
            {
                _providerKey = providerKey;
                return this;
            }

            public PriceDataDTOBuilder WithPriceWithoutVAT(double priceWithoutVAT)
            {
                _priceWithoutVAT = priceWithoutVAT;
                return this;
            }

            public PriceDataDTOBuilder WithPriceWithVAT(double priceWithVAT)
            {
                _priceWithVAT = priceWithVAT;
                return this;
            }

            public PriceDataDTOBuilder WithVAT(double vat)
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
