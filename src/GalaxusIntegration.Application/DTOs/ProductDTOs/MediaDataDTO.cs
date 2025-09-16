using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxusIntegration.Application.DTOs.ProductDTOs
{
    public class MediaDataDTO
    {
        public string ProviderKey { get; set; }
        public List<string>Images { get; set; }
        public List<(string,string)>ProductLinksWithLangugeKey { get; set; }
        private MediaDataDTO(string providerKey, List<string> images, List<(string, string)> productLinksWithLangugeKey)
        {
            ProviderKey = providerKey;
            Images = images;
            ProductLinksWithLangugeKey = productLinksWithLangugeKey;
        }
        public class MediaDataDTOBuilder
        {
            private string _providerKey;
            private List<string> _images = new List<string>();
            private List<(string,string)> _productLinksWithLangugeKey = new List<(string, string)>();
            public MediaDataDTOBuilder WithProviderKey(string providerKey)
            {
                _providerKey = providerKey;
                return this;
            }
            public MediaDataDTOBuilder WithImages(List<string> images)
            {
                _images = images;
                return this;
            }
            public MediaDataDTOBuilder AddImage(string image)
            {
                _images.Add(image);
                return this;
            }
         
            public MediaDataDTOBuilder AddProductLink(string languageKey, string productLink)
            {
                _productLinksWithLangugeKey.Add((languageKey, productLink));
                return this;
            }
            public MediaDataDTO Build()
            {
                if (string.IsNullOrEmpty(_providerKey))
                {
                    throw new InvalidOperationException("ProviderKey must be provided.");
                }
                if(!_images.Any())
                    throw new InvalidOperationException("At least one image must be provided.");
                return new MediaDataDTO(_providerKey, _images, _productLinksWithLangugeKey);
            }
        }

        public List<string> HeadList()
        {
            List<string> result = new();
            result.Add("ProviderKey");

            //Add image headers based on the number of images
            //first image is main image
            result.Add("MainImageUrl");
            for (int i = 1; i < Images.Count; i++)
            {
                result.Add($"ImageUrl_{i}");
            }
            // Add product links with language keys
            foreach (var langKey in ProductLinksWithLangugeKey)
            {
                result.Add($"ProductLink_{langKey.Item1}");
            }
            return result;
        }
        public List<string> DataToString()
        {
            List<string> result = new();
            result.Add(ProviderKey);
            // Add images
            if (Images.Any())
            {
                result.Add(Images[0]); // Main image
                for (int i = 1; i < Images.Count; i++)
                {
                    result.Add(Images[i]);
                }
            }
            else
            {
                result.Add(""); // Placeholder for main image if none provided
            }
            // Add product links with language keys
            foreach (var langKey in ProductLinksWithLangugeKey)
            {
                result.Add(langKey.Item2);
            }
            return result;
        }
    }
}
