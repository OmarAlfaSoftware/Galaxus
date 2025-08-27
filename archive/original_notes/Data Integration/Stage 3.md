Stage 3 Integration Process Details

### Definition of Stage 3

now after add the products and its details, in stage 3 we going to enrich these products by adding more properties like variants or accessories or declaration or sustainability terms;
this will help users finds the products faster by filtrization even by energy power or smiliar.
the accessories of the product will be visible in the product details page.

### Two Ways of Adding Elements

there are two way of adding the elements of this stage:
* first we could go with key value option
* or column options

#### Key Value Option

like we have:

| ProviderKey | SpecificationKey | SpecificationValue |
|-------------|-----------------|------------------|
| item_01     | color           | black            |
| item_01     | color           | yellow           |
| item_01     | color           | blue             |
| item_01     | material        | plastic          |
| item_01     | material        | metal            |

also we can add more values in the same ros like this:

| ProviderKey | SpecificationKey | SpecificationValue          |
|-------------|-----------------|----------------------------|
| item_01     | color           | black \| yellow \| blue    |
| item_01     | material        | plastic \| metal           |

**Requirements:**
* SpecificationKey, SpecificationValue must be: Str(200)
* German and English are preferred languages. For other languages, please contact your Digitec Galaxus representative.
* For each item number listed in this file, there must be at least one "SpecificationKey" and one "SpecificationValue" in one language.

#### Column Option

or we can use the column option like this:

| ProviderKey | color       | material  | size | target group |
|-------------|------------|----------|------|--------------|
| item_10     | blue       | Cotton   | M    | women        |
| item_11     | red        | Polyester| M    | women        |
| item_12     | yellow     | Cotton   | M    | men          |
| item_13     | yellow, black | Cotton | L    | men          |

each column must be:
Specification values of the articles for the specification contained in the header line.
E.g.. header: "colour", specification values: blue, black, red, etc.

### Sustainability Data

now in the sustainability:
there is the label, certificate, body, country of origin.
this mean that this product is good and has a good quality based on the certificate.
but we must ensure that the certificate is written well like it not "FSC" it must be spcifed as "FSC Resycled" for example because any mistake will cause that it will not be listed in the product details, and if any new one recognized it will be added to the website.

| Header (Key) | Description | Value (Value) |
|---|---|---|
| **Sustainability Label** | Specific standard of the sustainability label | *Indication of the specific certification standard of the sustainability label* <br><br> ⚠️ **Warning**: The exact certification standard must be specified when importing the data: <br><br> **Example 1: Forest Stewardship Council (FSC)** <br> • Correct Standard: FSC Recycled, FSC Mix, FSC 100% ✅ <br> • Incorrect Standard: FSC ❌ <br><br> **Example 2: Oeko-Tex** <br> • Correct Standard: Oeko-Tex Eco Passport, Oeko-Tex Made in Green, Oeko-Tex Standard 100 ✅ <br> • Incorrect Standard: Oeko-Tex ❌ <br><br> **Selection and assignment of labels including sustainability filter** <br> *On the product detail page, only selected labels that have been checked for validity by the internal sustainability team are displayed on the products. To make it easier for customers to understand and filter the labels, they are assigned to so-called sustainability properties (e.g. 'FSC Recycled' appears in the filter as 'Recycled materials'). New sustainability standards are checked by the Sustainability Team. The process can take some time.* <br><br> ℹ️ **Possible Values**: Rainforest Alliance, V-Label, Oeko-Tex Standard 100, Blauer Engel, FSC Mix etc. <br><br> *An overview of the labels accepted to date and their allocation to the respective sustainability properties can be found [here](https://www.galaxus.ch/en/page/sustainability-criteria-and-labels-for-our-product-range-34169).* |
| **Sustainability Certificate** | Certificate number/licence number | *Specification of the certificate number* <br><br> ℹ️ **Possible Values**: 10857700, CU3934720, etc. <br><br> ⚠️ **Warning**: This information is used to validate the sustainability standards. Failure to provide this information may result in the sustainability labels not being displayed on the product detail page due to a lack of validation! |
| **Certification Body** | Certification body for verifying the certificates | *Indication of the certification body that issued/verified the certificate* <br><br> ⚠️ **Warning**: This information is not yet displayed on the product detail page but is already used to validate the sustainability standards. Failure to provide this information may result in the sustainability labels not being displayed on the product detail page due to a lack of validation! <br><br> ℹ️ **Possible Values**: Control Union, CERES, Ecocert etc. |
| **Country of Origin** | Country of Origin (corresponds to Production Country/Country of Origin) | *Origin (Production Country) of the delivered item or country where the majority of value creation has taken place* The [EU Rules of Origin](https://taxation-customs.ec.europa.eu/customs-4/international-affairs/origin-goods_de) must be observed. If a GTIN or product has multiple countries of origin because it was manufactured in different countries (usually only the case for large manufacturers), all countries of origin must be provided. *Possible values are country names or country codes: Switzerland, China, Ecuador, Thailand, US, UK* |

### Declaration Data

now for the other declaration data like energy efficency and power connector an others like this that help the customer know which exactley what he buy so he can ensured that this product is the right one

#### Product Attributes Specification Table

| Header (Key) | Designation | Description (Value) |
|--------------|-------------|---------------------|
| **EnergyEfficiencyClass_2021** | Energy efficiency class | *Values A to G (based on standard 2021)* |
| **PowerConnectorAppliance** | Power supply to the device | *Specification of the socket for articles with power operation* <br><br> **Expected Values:** No current, built-in, USB, EC-60320 C1, EC-60320 C5, EC-60320 C7, EC-60320 C13, EC-60320 C15, EC-60320 C15A, EC-60320 C17, Others |
| **PowerConnectorWall** | Power supply to the wall | *Specification of the plug for articles with power operation* <br><br> **Expected Values:** Plug-type C (CEE 7/16), Plug-type C (CEE 7/17), Plug-type F (CEE 7/4), Plug-type J (SEC 1011), others |
| **HazardStatements** | H-phrases | *Information on hazards of chemical substances* <br><br> **E.g.:** H201, H301+H331, EUH204 |
| **SolidWoodType** | Solid wood type | *Scientific name of solid wood according to the [wood database](https://www.konsum.admin.ch/bfk/de/home/holzdeklaration/holzdatenbank.html)* |
| **SolidWoodOrigin** | Solid wood origin | *SolidWoodOrigin based on standard ISO 3166 ALPHA-2, E.g.: CH, DE* |

#### Power Connector Types Details

##### PowerConnectorAppliance Types

| Designation | Description | Illustration |
|-------------|-------------|--------------|
| No current | The article has no connection to the power grid. | - |
| Built-in | The article has a fixed power cable | - |
| USB | Socket for USB cable | - |
| EC-60320 C1 | Socket for electric shavers. These are somewhat similar to the C7 connector, but have no waist. | [Image] |
| EC60320 C5 | Commonly referred to as a "clover-leaf" or "Mickey Mouse". Used for many switched-mode power supplies for notebooks. | [Image] |
| EC-60320 C7 | Socket for small appliances. Commonly referred to as "figure 8" or "infinity" in the UK. | [Image] |
| EC60320 C13 | Very common on personal computers and peripherals. Mainly used for the power connection of devices which do not generate any significant heat during operation. | [Image] |
| EC60320 C15 | For use in high-temperature settings (for example electric kettle, computer networking closets). | [Image] |
| EC60320 C15A | For use in very-high-temperature settings, such as some stage lighting instruments. Similar to C15/C16, but the top is narrowed to exclude the C15 cord connector. | [Image] |
| EC60320 C17 | Socket for cold appliance plug without earthing contact. | [Image] |
| Others | "Others" includes all connector types that are not included in the list. | - |

##### PowerConnectorWall Types

| Designation | Description | Illustration |
|-------------|-------------|--------------|
| Plug type C (CEE 7/16) | The **Europlug** has no protective conductor and is not reverse polarity protected. The contacts with a diameter of 4 mm are at a distance of 19 mm from each other. | [Image] |
| Plug type C (CEE 7/17) | This contour connector has two contacts with a diameter of 4.8 mm at a distance of 19 mm. Above and below the axis, there are two recesses to accommodate the French earthing pin or the German earthing terminals. | [Image] |
| Plug type F (CEE 7/4) | This plug, commonly referred to as **Schuko plug**, has two contacts with 4.8 mm diameter and 19 mm length. Two contact surfaces on the connector side ensure the earth contact with existing contact springs on the socket. | [Image] |
| Plug type J (SEV 1011) | The Swiss plug type J has three contact pins. The middle pin serves as a protective contact and is slightly offset. In addition, it forms the first contact, since the current-carrying contacts are lower in the socket. | [Image] |
| Others | "Others" includes all connector types that are not included in the list. | - |

#### Notes
* Energy efficiency classes follow the 2021 standard (A-G rating system)
* Power connector specifications follow international standards (IEC 60320, CEE, etc.)
* Hazard statements use standard H-phrases for chemical safety
* Wood specifications follow ISO standards for origin codes
* Images referenced in original document are not included in this markdown conversion

### Accessory Data

lastley is the accessory data it must be refrence to another product like:
i have an iphone 13 so i must ref to a cover for iphone 13 and so on

| Header               | Data type | Designation                  | Description                                                       |
|----------------------|-----------|------------------------------|-------------------------------------------------------------------|
| ProviderKey           | Str(50)  | Article number of main article | *Article number of the main article*                               |
| AccessoryProviderKey  | Str(100) | Article number of accessory   | *Article number of the accessory article (matching to the main article)* |

---
