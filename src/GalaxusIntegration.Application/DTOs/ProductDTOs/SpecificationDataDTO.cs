using System;
using System.Diagnostics.CodeAnalysis;

namespace GalaxusIntegration.Application.DTOs.ProductDTOs
{
    public class SpecificationDataDto
    {
        [NotNull]
        public string ProviderKey { get; set; }
        [NotNull]
        public string Key { get; set; }
        [NotNull]
        public string Value { get; set; }

        // Private constructor for the builder to use
        private SpecificationDataDto(string providerKey, string key, string value)
        {
            ProviderKey = providerKey;
            Key = key;
            Value = value;
        }

        // Fluent Builder
        public class SpecificationDataDtoBuilder
        {
            private string _providerKey;
            private string _key;
            private string _value;

            public SpecificationDataDtoBuilder WithProviderKey(string providerKey)
            {
                _providerKey = providerKey;
                return this;
            }

            public SpecificationDataDtoBuilder WithKey(string key)
            {
                _key = key;
                return this;
            }

            public SpecificationDataDtoBuilder WithValue(string value)
            {
                _value = value;
                return this;
            }

            // Build method to create the final object
            public SpecificationDataDto Build()
            {
                if (string.IsNullOrEmpty(_providerKey))
                {
                    throw new InvalidOperationException("ProviderKey must be provided.");
                }

                if (string.IsNullOrEmpty(_key))
                {
                    throw new InvalidOperationException("Key must be provided.");
                }

                if (string.IsNullOrEmpty(_value))
                {
                    throw new InvalidOperationException("Value must be provided.");
                }

                return new SpecificationDataDto(_providerKey, _key, _value);
            }
        }

        public List<string> DataToString()
        {
            List<string> resultList = new List<string>
            {
                ProviderKey,
                Key,
                Value
            };
            return resultList;
        }
        public List<string> HeadList()
        {
            List<string> resultList = new List<string>
            {
                "ProviderKey",
                "SpecificationKey",
                "SpecificationValue"
            };
            return resultList;
        }
    }
}
