using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxusIntegration.Application.DTOs.ProductDTOs
{
    public class ProudctDataDTO
    {
        public string ProviderKey { get; set; }
        public string GTIN { get; set; }
        public string BrandName { get; set; }
        public string CategoryGroup_1 { get; set; }
        public string CategoryGroup_2 { get; set; }
        public string CategoryGroup_3 { get; set; }
        public string ProductCategory { get; set; }
        public double Weight_g { get; set; }
        public double Length_m { get; set; }
        public double Width_m { get; set; }
        public double Height_m { get; set; }
        public string TARESCode { get; set; }
        public string TARICCode { get; set; }
        public string CountryOfOrigin { get; set; }
        public string ManufacturerKey { get; set; }
        public DateTime ReleaseDate_CH { get; set; }
        public string VariantName { get; set; }
        public Dictionary<string,string> LanguageProductTitles { get; set; }
        public Dictionary<string,string>LanguageProductLongDescriptions { get; set; }
        public Dictionary<string,string>LanguageProductApplication { get; set; }
        public Dictionary<string,string> LanguageProductComposition { get; set; }

        private ProudctDataDTO(string providerKey, string gtin, string brandName, string categoryGroup_1,
            string categoryGroup_2, string categoryGroup_3, string productCategory, double weight_g, double length_m,
            double width_m, double height_m, Dictionary<string,string> languageProductTitles, 
            string tARESCode, string tARICCode, string countryOfOrigin,
           Dictionary<string, string> languageProductLongDescription, string manufacturerKey, DateTime releaseDate_CH,
            string variantName,Dictionary<string,string>languageProductComposition,Dictionary<string,string> languageProductApplication)
        {
            ProviderKey = providerKey;
            GTIN = gtin;
            BrandName = brandName;
            CategoryGroup_1 = categoryGroup_1;
            CategoryGroup_2 = categoryGroup_2;
            CategoryGroup_3 = categoryGroup_3;
            ProductCategory = productCategory;
            Weight_g = weight_g;
            Length_m = length_m;
            Width_m = width_m;
            Height_m = height_m;
            LanguageProductTitles = languageProductTitles;
            LanguageProductLongDescriptions = languageProductLongDescription;
            TARESCode = tARESCode;
            TARICCode = tARICCode;
            CountryOfOrigin = countryOfOrigin;
            ManufacturerKey = manufacturerKey;
            ReleaseDate_CH = releaseDate_CH;
            VariantName = variantName;
            LanguageProductApplication=languageProductApplication;
            LanguageProductComposition=languageProductComposition;
        }

        public List<string> DataToString()
        {   // Convert the object properties to a list of strings for Excel row representation
            var list = new List<string>();
            list.Add(ProviderKey);
            list.Add(GTIN);
            list.Add(BrandName);
            list.Add(CategoryGroup_1);
            list.Add(CategoryGroup_2);
            list.Add(CategoryGroup_3);
            list.Add(ProductCategory);
            list.Add(Weight_g.ToString());
            list.Add(Length_m.ToString());
            list.Add(Width_m.ToString());
            list.Add(Height_m.ToString());
            list.Add(TARESCode);
            list.Add(TARICCode);
            list.Add(CountryOfOrigin);
            list.Add(ManufacturerKey);
            list.Add(ReleaseDate_CH.ToString("yyyy-MM-dd"));
            list.Add(VariantName);
            if (LanguageProductTitles != null)
            {
                foreach (var title in LanguageProductTitles)
                {
                    list.Add(title.Value); // Assuming title.Key is the language code
                }
            }
            if (LanguageProductLongDescriptions != null)
            {
                foreach (var desc in LanguageProductLongDescriptions)
                {
                    list.Add(desc.Value); // Assuming desc.Key is the language code
                }
            }
            if (LanguageProductApplication != null)
            {
                foreach (var app in LanguageProductApplication)
                {
                    list.Add(app.Value); // Assuming app.Key is the language code
                }
            }
            if (LanguageProductComposition != null)
            {
                foreach (var comp in LanguageProductComposition)
                {
                    list.Add(comp.Value); // Assuming comp.Key is the language code
                }
            }



            return list;
        }

        public List<string> HeadList()
        {
            // Convert the object properties to a list of strings for Excel header representation
            var result = new List<string>();
            result.Add("ProviderKey");
            result.Add("GTIN");
            result.Add("BrandName");
            result.Add("CategoryGroup_1");
            result.Add("CategoryGroup_2");
            result.Add("CategoryGroup_3");
            result.Add("ProductCategory");
            result.Add("Weight_g");
            result.Add("Length_m");
            result.Add("Width_m");
            result.Add("Height_m");
            result.Add("TARESCode");
            result.Add("TARICCode");
            result.Add("CountryOfOrigin");
            result.Add("ManufacturerKey");
            result.Add("ReleaseDate_CH");
            result.Add("VariantName");
            if (LanguageProductTitles != null)
            {
                foreach (var title in LanguageProductTitles)
                {
                    result.Add($"ProductTitle_{title.Key}"); // Assuming title.Key is the language code
                }
            }
            if (LanguageProductLongDescriptions != null)
            {
                foreach (var desc in LanguageProductLongDescriptions)
                {
                    result.Add($"ProductLongDescription_{desc.Key}"); // Assuming desc.Key is the language code
                }
            }
            if (LanguageProductApplication != null)
            {
                foreach (var app in LanguageProductApplication)
                {
                    result.Add($"ProductApplication_{app.Key}"); // Assuming app.Key is the language code
                }
            }
            if (LanguageProductComposition != null)
            {
                foreach (var comp in LanguageProductComposition)
                {
                    result.Add($"ProductComposition_{comp.Key}"); // Assuming comp.Key is the language code
                }
            }
            return result;
        }
        public class ProductDataDTOBuilder
        {
            private string _providerKey;
            private string _gtin;
            private string _brandName;
            private string _categoryGroup_1;
            private string _categoryGroup_2;
            private string _categoryGroup_3;
            private string _productCategory;
            private double _weight_g;
            private string _tARESCode;
            private string _tARICCode;
            private string _countryOfOrigin;
            private string _manufacturerKey;
            private double _length_m;
            private double _width_m;
            private double _height_m;
            private DateTime _releaseDate_CH;
            private string _variantName;
            private Dictionary<string, string> _languageProductTitle;
            private Dictionary<string, string> _languageProductDesciption;
            private Dictionary<string, string> _languageApplication;
            private Dictionary<string, string> _languageComposition;

            public ProductDataDTOBuilder WithProviderKey(string providerKey)
            {
                _providerKey = providerKey;
                return this;
            }

            public ProductDataDTOBuilder WithGTIN(string gtin)
            {
                _gtin = gtin;
                return this;
            }

            public ProductDataDTOBuilder WithBrandName(string brandName)
            {
                _brandName = brandName;
                return this;
            }

            public ProductDataDTOBuilder WithCategoryGroup_1(string categoryGroup_1)
            {
                _categoryGroup_1 = categoryGroup_1;
                return this;
            }

            public ProductDataDTOBuilder WithCategoryGroup_2(string categoryGroup_2)
            {
                _categoryGroup_2 = categoryGroup_2;
                return this;
            }

            public ProductDataDTOBuilder WithCategoryGroup_3(string categoryGroup_3)
            {
                _categoryGroup_3 = categoryGroup_3;
                return this;
            }

            public ProductDataDTOBuilder WithProductCategory(string productCategory)
            {
                _productCategory = productCategory;
                return this;
            }

            public ProductDataDTOBuilder WithWeight_g(double weight_g)
            {
                _weight_g = weight_g;
                return this;
            }

            public ProductDataDTOBuilder WithLength_m(double length_m)
            {
                _length_m = length_m;
                return this;
            }

            public ProductDataDTOBuilder WithWidth_m(double width_m)
            {
                _width_m = width_m;
                return this;
            }

            public ProductDataDTOBuilder WithHeight_m(double height_m)
            {
                _height_m = height_m;
                return this;
            }

         
            public ProductDataDTOBuilder WithTARESCode(string tARESCode)
            {
                _tARESCode = tARESCode;
                return this;
            }

            public ProductDataDTOBuilder WithTARICCode(string tARICCode)
            {
                _tARICCode = tARICCode;
                return this;
            }

            public ProductDataDTOBuilder WithCountryOfOrigin(string countryOfOrigin)
            {
                _countryOfOrigin = countryOfOrigin;
                return this;
            }

         public ProductDataDTOBuilder AddProductTitleInWithLanguage(string language, string title)
            {
                if (_languageProductTitle == null)
                {
                    _languageProductTitle = new Dictionary<string, string>();
                }
                _languageProductTitle[language] = title;
                return this;
            }
            public ProductDataDTOBuilder AddProductDescriptionInWithLanguage(string language, string title)
            {
                if (_languageProductDesciption == null)
                {
                    _languageProductDesciption = new Dictionary<string, string>();
                }
                _languageProductDesciption[language] = title;
                return this;
            }
            public ProductDataDTOBuilder AddProductApplicationInWithLanguage(string language, string application)
            {
                if (_languageApplication == null)
                {
                    _languageApplication = new Dictionary<string, string>();
                }
                _languageApplication[language] = application;
                return this;
            }
            public ProductDataDTOBuilder AddProductCompositionInWithLanguage(string language, string composition)
            {
                if (_languageComposition == null)
                {
                    _languageComposition = new Dictionary<string, string>();
                }
                _languageComposition[language] = composition;
                return this;
            }

            public ProductDataDTOBuilder WithManufacturerKey(string manufacturerKey)
            {
                _manufacturerKey = manufacturerKey;
                return this;
            }

            public ProductDataDTOBuilder WithReleaseDate_CH(DateTime releaseDate_CH)
            {
                _releaseDate_CH = releaseDate_CH;
                return this;
            }

            public ProductDataDTOBuilder WithVariantName(string variantName)
            {
                _variantName = variantName;
                return this;
            }

            public ProudctDataDTO Build()
            {
                if (string.IsNullOrEmpty(_providerKey))
                {
                    throw new InvalidOperationException("ProviderKey must be provided.");
                }

                if (string.IsNullOrEmpty(_gtin))
                {
                    throw new InvalidOperationException("GTIN must be provided.");
                }

                if (string.IsNullOrEmpty(_brandName))
                {
                    throw new InvalidOperationException("BrandName must be provided.");
                }

                if (string.IsNullOrEmpty(_categoryGroup_1))
                {
                    throw new InvalidOperationException("CategoryGroup_1 must be provided.");
                }

                if (string.IsNullOrEmpty(_productCategory))
                {
                    throw new InvalidOperationException("ProductCategory must be provided.");
                }

                if (_weight_g < 0)
                {
                    throw new InvalidOperationException("Weight_g must be non-negative.");
                }

                if (_length_m < 0)
                {
                    throw new InvalidOperationException("Length_m must be non-negative.");
                }

                if (_width_m < 0)
                {
                    throw new InvalidOperationException("Width_m must be non-negative.");
                }

                if (_height_m < 0)
                {
                    throw new InvalidOperationException("Height_m must be non-negative.");
                }

                if (!_languageProductDesciption.Any())
                {
                    throw new InvalidOperationException("At least one description should be provided.");
                }
                if (!_languageProductDesciption.Any())
                {
                    throw new InvalidOperationException("At least one title should be provided.");
                }
                if (string.IsNullOrEmpty(_countryOfOrigin))
                {
                    throw new InvalidOperationException("CountryOfOrigin must be provided.");
                }

                if (string.IsNullOrEmpty(_manufacturerKey))
                {
                    throw new InvalidOperationException("ManufacturerKey must be provided.");
                }

                if (_releaseDate_CH == default)
                {
                    throw new InvalidOperationException("ReleaseDate_CH must be provided.");
                }

                return new ProudctDataDTO(_providerKey, _gtin, _brandName, _categoryGroup_1, _categoryGroup_2,
                    _categoryGroup_3, _productCategory, _weight_g, _length_m, _width_m, _height_m, _languageProductTitle, _tARESCode, _tARICCode, _countryOfOrigin, 
                    _languageProductDesciption, _manufacturerKey, _releaseDate_CH, _variantName,_languageComposition,_languageApplication);

            }
        }
    }
}
