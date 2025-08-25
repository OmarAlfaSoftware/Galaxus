this file contain the details of what is Stage 2 and what its contains and its importance.

# Stage 2 Integration Data Structure

## Idea:

Start with the idea of Stage 2:
Now after publishing the products and setting the offers in Stage 1, now we need to improve these products by adding more details like colors, size, the category, or other details.  
So this step helps the end customer to find the products and know more details about it so he can buy it.

---

## The Images Used in Stage 2:

We can add the data in different types but we can find the possible structure in this image [Image](<../Images/Stage 2/Possible Files Structure.png>)  
Second, the TARIC DB code form [Image](<../Images/Stage 2/taric DB.png>)

---

## Structure:

We can add the data in different types but we can find the possible structure in this image [Image](<../Images/Stage 2/Possible Files Structure.png>)

Each file has its own structure, so let's take them one by one.

---

### First File: ProductData

The naming of the file must be `ProductData_[ProviderName].[FileFormat]`  
Eg: `ProductData_digitecgalaxus.csv`

The file contains a lot of structures but some are mysterious like Category and Product Category:  
The website divides the Category into three sections like `Category_1`, `Category_2`, `Category_3`:

|--Beauty & Health
|----Health
|------SportsFood

So they divide by three levels. For better filtration, it's preferable to add more subCategories.  
Also, we have ProductCategory and this I think is unique under all the categories with their different levels,  
so it’s like Children's Headphone, Building Blocks, etc.  
You can find all of the categories in this file [Category](<../Files/20231101_Kategorien_DE_EN.xlsx>)

If there is a new category that doesn't exist in this file, they will review it and add it to their website.

---

### Minimal Criteria of ProductData

#### Product Data Table Description

| **Header**            | **Data Type**               | **Designation**                          | **Description**                                                                                                                                                                                                                                                                                 |
|-----------------------|-----------------------------|------------------------------------------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| **ProviderKey**        | `Str(50)` <br> ASCII 32-126 | Partner specific item number            | Unique identification number of the partner item. This must be unique. Duplicates are not allowed. The provider key should be an ASCII string containing characters from the range 32-126.                                                                                                      |
| **Gtin**               | `Int(8)`, `Int(12)`, `Int(13)`, `Int(14)` | EAN/GTIN code of the article            | Unique and globally valid identification number of the article. Valid GTIN codes include: GTIN-12 (UPC), GTIN-13 (EAN-13), GTIN-14 (EAN/UCC-128 or ITF-14), and GTIN-8 (EAN-8). The GTIN must be visible on the retail packaging. Duplicates are not permitted. |
| **BrandName**          | `Str(100)`                  | Manufacturer's brand of the item         | The brand name associated with the item. It is mandatory for products to be created/processed with an associated brand.                                                                                                                                                                       |
| **CategoryGroup_ [index]** | `Str(200)`                  | Category groups of the article           | Category groups form a hierarchical structure of categories. CategoryGroup_1, CategoryGroup_2, CategoryGroup_3, etc. This helps assign items to their respective categories within the platform. If the product categories are already complete, these are optional.                               |
| **ProductCategory**    | `Str(200)`                  | Product category of the article          | The deepest (most detailed) level of categorization for the item. This category defines the specific product classification in the catalog.                                                                                                                                                    |
| **Weight_ [unit]**     | `Dec(8,4)`                  | Packaging weight (mg, g, kg)             | The weight of the product plus the weight of the original packaging. This should be provided for EU hub merchants. Units can be in milligrams (mg), grams (g), or kilograms (kg).                                                                                                            |
| **ProductTitle_de**    | `Str(100)`                  | German article/product name             | The product title for the German-speaking region. Product names should be cleansed of specifications and be uniform. A consistent appearance in the online shop is crucial for product visibility and purchase.                                                                                     |
| **TARICCode**          | `Int(6-11)` or `Str(14)`    | Commodity code (TARIC) for the EU        | The TARIC code used for the customs classification of the product in the EU. The code can be 6-11 characters long or 14 characters (with spaces). This code is necessary for customs processing and classification.                                                                               |
| **CountryOfOrigin**    | `Str(2)`                    | Country of origin                        | The country in which the product was obtained or produced, or where the last substantial processing was carried out. It must be provided in ISO 3166 ALPHA-2 format (e.g., CH for Switzerland, DE for Germany). Only one country of origin can be transmitted per article number.                    |

---

### Key Notes:

- **ProviderKey**: Must be unique and should only include ASCII characters.
- **Gtin**: EAN/GTIN codes should be globally valid and visible on the product's retail packaging.
- **BrandName**: A valid brand name must be assigned to the product.
- **CategoryGroup_**: Used for grouping items in a hierarchical structure, and can be optional if categories are clearly defined.
- **ProductCategory**: Defines the most detailed product classification.
- **Weight_**: Must include the weight of the product along with its packaging.
- **ProductTitle_de**: The product title specifically for German-speaking regions.
- **TARICCode**: Used for customs classification in the EU and must conform to the proper format.
- **CountryOfOrigin**: Should be listed in the ISO 3166 ALPHA-2 format, with only one country allowed per article.

---

For further details on product categorization and data preparation, refer to the provided [guidelines on data quality](https://confdg.atlassian.net/wiki/spaces/PI/pages/168665885851).

---

## Supplementary Criteria for Product Safety Regulations: ProductData

### Manufacturer Data Table

| **Header**           | **Data Type**               | **Designation**                               | **Description**                                                                                                                                                                                      |
|----------------------|-----------------------------|-----------------------------------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| **GPSRNameAddress**   | `Str(300)`                  | Name and postal address of manufacturer       | The company name **and** the postal address of the manufacturer importing the product into the European Economic Area (EEA). Format: `{name}, {street} {number}, {zip code} {town}, {country}`. |
| **GPSREmailUrl**      | `Str(200)`                  | Email address or URL of the manufacturer      | The email address **or** URL of the manufacturer importing the product into the European Economic Area (EEA). The URLs must be accessible.                                                           |

---

### Key Details:

- **GPSRNameAddress**: The address format should include the company name, street and house number, postal code, city, and country. Optionally, additional address suffixes like "c/o" or "floor" can be included.
  
- **GPSREmailUrl**: The email or URL of the manufacturer must be provided. Ensure that the email or URL is valid and accessible.

---

### Examples:

1. **GPSRNameAddress**  
   Example: "Mustermann GmbH, Musterstraße 123A, 12345 Musterstadt, Deutschland"

2. **GPSREmailUrl**  
   Example: "info@mustermann.com" or "http://www.mustermann.com"

---

## Supplementary Criteria for Optimising Data Quality: ProductData

### Product Data Table

| **Header**             | **Data Type**               | **Designation**                                | **Description**                                                                                                                                                                                                                                                                                                                                                                                                         |
|------------------------|-----------------------------|------------------------------------------------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| **ManufacturerKey**     | `Str(4-50)`                 | Manufacturer number of the article             | Unique identification number of the manufacturer of a product. Manufacturer numbers must be unique within the same brand/manufacturer. Example: "1234"                                                                                                                                                                                                                                                                |
| **Length**              | `Dec(8,4)`                  | Packing length                                | Width of the original packaging. Possible units: mm (millimeter), cm (centimeter), m (meter).                                                                                                                                                                                                                                                                |
| **Width**               | `Dec(8,4)`                  | Packaging width                               | Width of the original packaging. Possible units: mm (millimeter), cm (centimeter), m (meter).                                                                                                                                                                                                                                                                |
| **Height**              | `Dec(8,4)`                  | Packaging height                              | Height of the original packaging. Possible units: mm (millimeter), cm (centimeter), m (meter).                                                                                                                                                                                                                                                               |
| **ReleaseDate_**        | `Date`                      | Publication date                              | Date information for new publications. The date is displayed in the shop. Example: ReleaseDate_en                                                                                                                                                                                                                                                              |
| **TARESCode**           | `Str(9)`                    | Swiss customs tariff number (TARES)            | TARES database, e.g., "12345678" or "1234.5678". The validation of the content only allows the optional character "." (dot). Other special characters are not allowed.                                                                                                                                                                                   |
| **WEEENumber**          | `Str(10)`                   | WEEE Number                                    | e.g., "DE12345678". Only valid WEEE numbers can be transmitted. If the partner deregisters from the EAR register (market exit), the number may no longer be transmitted. If more than one WEEE number is maintained, the most recent number should be transmitted.                                                                                             |
| **VariantName**         | `Str(100)`                  | Family name/variant name                       | Unique identification for items that belong together. Digitec Galaxus uses this information to create product variants.                                                                                                                                                                                                                                                                                                 |
| **ProductTitle_**       | `Str(100)`                  | Item name/Product name [en,fr,it]               | Cleaned name of the item in a complementary language such as English, French, or Italian. Product names must be consistent and cleansed of specifications. Example: ProductTitle_en                                                                                                                                                                           |
| **LongDescription_**    | `Str(4000)`                 | Detailed description of the article [de,en,fr,it] | Detailed description of the item with relevant information about the product such as size, material, age range, special features, technical specifications, shape, pattern, fabric type, design.                                                                                                                                                            |
| **Composition_**        | `Str(4000)`                 | Composition [de,en,fr,it]                      | Description of the composition or ingredients of a product. These are required by law for specific product groups, i.e., they are compliance-relevant. For example, a list of ingredients or allergy declarations.                                                                                                                                               |
| **Application_**        | `Str(4000)`                 | Application [de,en,fr,it]                      | Description of the application of a product. These are required by law for specific product groups, i.e., they are compliance-relevant, such as medical devices or medicines.                                                                                                                                                                               |

---

### Key Details:

- **ManufacturerKey**: This field holds the unique identification number assigned to the manufacturer. This identifier must be unique for each brand or manufacturer. It helps to track the manufacturer of the product.
  
- **Length, Width, Height**: These fields describe the dimensions of the original packaging of the product, which are important for logistics, inventory management, and displaying product dimensions.

- **ReleaseDate_**: The publication date for a product’s availability. It helps to track and display the product's release timeline on the shop.

- **TARESCode**: A Swiss customs tariff number, essential for international shipping and customs. It must adhere to a specific format and includes validation rules.

- **WEEENumber**: This number is crucial for compliance with the European Waste Electrical and Electronic Equipment (WEEE) Directive. It helps to ensure the product is correctly registered and tracked for recycling.

- **VariantName**: The unique identifier for a product variant, essential for identifying related items that belong together, especially for products available in multiple variants.

- **ProductTitle_**: The product name in a specific language. It is crucial for maintaining consistency in the product catalog across multiple languages and ensuring the products are easy to find.

- **LongDescription_**: The detailed description of the product, which should cover all essential details about the product, such as its features, material, size, etc.

- **Composition_**: The composition or ingredients of the product, especially for food, cosmetics, or products that require regulatory compliance. This is important for customer safety and regulatory purposes.

- **Application_**: Details on the application of the product, required for some specific product groups by law, especially for products like medicines or medical devices.

---

## Media Data

**Note:**  
I think that the images must be on the web, not local in the devices, as they said that they are not counting on their DB, instead, they count on our DB,  
and because the product is published, we can't set local path images.

| **Header**                  | **Data type**   | **Designation**                        | **Description**                                                                                                                                                           |
|-----------------------------|-----------------|----------------------------------------|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| **ProductLink** _[language]_[index] | Str(300)        | Product links (HTTPS)                  | One link per column. Product links can point to manufacturer pages, product reviews, etc.                                                                                  |
|                             |                 |                                        | - Links to resale platforms and other online shops are prohibited.                                                                                                       |
|                             |                 |                                        | - "language" based on standard ISO 639-1.                                                                                                                                   |
|                             |                 |                                        | - "index" - Consecutive numbering for multiple URLs in the same language.                                                                                                 |
|                             |                 |                                        | **E.g.**: ProductLink_de_1, ProductLink_de_2, ProductLink_en_1, ProductLink_en_2, …                                                                                       |
| **MainImageURL**            | Str(500)        | First image link (HTTPS)               | Cropped product image with white background, preferably frontal view. The longest edge of the images must be at least 600px (without border around the product). The size of 8 MB per image must not be exceeded. |
| **ImageURL** _[index]       | Str(500)        | Additional image links (HTTPS)         | One link per column. The link must be freely accessible. JPG, JPEG and PNG are supported. The longest edge of the images must be at least 600px (without border around the product). The size of 8 MB per image must not be exceeded. |
