namespace GalaxusIntegration.Application.DTOs.ProductDTOs
{
    public class ProductAccessoryDataDTO
    {
        public string ProviderKey { get; set; }
        public string AccessoryProviderKey { get; set; }
        private ProductAccessoryDataDTO(string providerKey, string accessoryProviderKeys)
        {
            ProviderKey = providerKey;
            AccessoryProviderKey = accessoryProviderKeys;
        }
        public class ProductAccessoryDataDTOBuilder
        {
            private string _providerKey;
            private string _accessoryProviderKey ;
            public ProductAccessoryDataDTOBuilder WithProviderKey(string providerKey)
            {
                _providerKey = providerKey;
                return this;
            }

            public ProductAccessoryDataDTOBuilder WithAccessoryProviderKey(string accessoryProviderKey)
            {
                _accessoryProviderKey=accessoryProviderKey;
                return this;
            }
            public ProductAccessoryDataDTO Build()
            {
                if (string.IsNullOrEmpty(_providerKey))
                {
                    throw new InvalidOperationException("ProviderKey must be provided.");
                }
                if(string.IsNullOrEmpty(_accessoryProviderKey))
                    throw new InvalidOperationException("AccessoryProviderKey must be provided.");
                return new ProductAccessoryDataDTO(_providerKey, _accessoryProviderKey);
            }
        }

        public List<string> HeadList()
        {
            List<string>resultList= new List<string>
            {
                "ProviderKey",
                "AccessoryProviderKey"
            };
            return resultList;
        }

        public List<string> DataToString()
        {
            List<string> resultList = new List<string>
            {
                ProviderKey,
                AccessoryProviderKey
            };
            return resultList;
        }
    }
}
