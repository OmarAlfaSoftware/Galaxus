this file contains how the data structued in stage 1 to start integrate with the company.
these Images are related to Stage 1:
1- In the excel we cant repeat columns or set spaces or new line like this Image [header]("../Images/Stage 1/column headers example.png")
2- When Converting from CSV to EXCEL we should take care about number because it maybe look like this [ISSUE]("../Images/Stage 1/convert from csv to excel issue.png")
3- when write a GTIN or number it maybe contain leading Zeroes so the excel will ignore them so need to tak care about it [Numbers]("../Images/Stage 1/leading zeroes in cells.png")
4- we should not set empty lines in the sheet [emptyLines]("../Images/Stage 1//no empty lines.png")
5- we should make the excel file contain only one tab(sheet) because any other tabs will be ignored [Tabs]("../Images/no other tabs.png")
6- we should ensure the number are written in right format like this [Format]("../Images/Stage 1/number in excel must be in this format.png")
7- there are possibles of files structure in stage one like this [structure]("../Images/Stage 1/Possible files structure in stage 1.png")


first we have the offers:-
to push an offer so we make a file contain at least:
1- item number.
2- price.
3- stock.
4- EAN/GTIN.

and for activate the products on the website we must create a file:
1- product data
2- price
3- stock
and better to divide them into three files like image in [Image]("'../Images/Stage 1/Possible files sructure in stage 1.png'")


ASSORTMENT FILE:
the most Important file in the structure
because it control wich products are considered as active products and available on the market for sale or the MP.
also if any products is have stock 0 or unavailable its better to remove his product from the file.
Only Products in this file will uploaded and be Available.

Stock Handle:
if a product has stock of 0, we can do either set the time that the item will be available again or the date (NOT_BOTH).
or we just can set the Date to zero and its equal to none or not determined time for recharge.


MOQ (Minumum order quantity):
this helping in shipping as there are some products can't shipped alone or small number so we set the minimum number of products that must be orderd to accept the order.
if the MOQ is not setted its by default take value of 1


OQS (Orders Qunatity Steps):
its mean that if this product can't be shipping in any number like 2 or 3 or 4 
no it must be a number that fit the shipping package like cooking items or cups like it must be 6 in the package so the order must be 6 or 12 or 18 and so on.


GTIN:
to get GTIN for the product we must sign the Company in GS1 website to get the company number,
then we set each products align with the company number like if the company number equal to 123456 and my product ID = 10 so the GTIN = 123456000010.


STRUCTURES:

DATA Format

| **Type**                       | **CSV-Standard**                | **XLSX-Standard**                | **Exception/ Remark / Example**   |
|---------------------------------|---------------------------------|----------------------------------|-----------------------------------|
| **File format**                 | CSV / TXT                       | XLSX                             | Other formats possible in consultation but not recommended |
| **File name**                   | File names for initial configuration - without spaces / special characters / umlaut. <br> File names for file updates - the files **must be replaced** by the old files for each transmission and **have exactly the same file names** as the previous ones. | Examples: <br> ProductData_DigitecGalaxus.csv <br> PriceData_DigitecGalaxus.xlsx |                                   |
| **Character coding**            | UTF-8                           | -                                | Other character codes possible in consultation but not recommended |
| **Separator**                   | **;** (Semicolon)               | -                                | Other limit signs possible in consultation |
| **Header**                       | Header with column designation is mandatory | Header with column designation is mandatory | Duplicates are not allowed for column names. Column name must be present. Column names must not contain line breaks. |
| **Decimals**                    | Decimal places must be separated with a **.** (dot) or **,** (comma). <br> Example - VatRatePercentage: 8.1 <br> Thousands separators are not allowed: 1000 or 1,00 | -                                | Comma must not be used if the separator in the CSV is also a comma. |
| **Text / Parsing**              | Strings (data type: text) must be enclosed in inverted commas ("). | -                                | In the case of descriptions and product data, it is particularly important that these are enclosed in inverted commas because they can contain the characters that also act as separators (semicolon, comma, pipe) and/or line breaks which, if unmasked, prevent the import. Example: LongDescription_en: "TV with size 46"" and stand with white label ""Kings of E-Commerce"" |
| **Structure**                   | The structuring of the data within the text file must be observed. | The data to be read in is in the first sheet tab and there are no hidden sheet tabs. <br> Completely empty lines or notes in the file should be avoided if possible. | Example XLSX - Multiple worksheets |
| **Scientific notations**        | Scientific notations are not allowed | -                                | Often CSV files are opened and saved in Excel. This process changes the structure of the numbers contained in the file. Information such as GTIN, item number and others become unusable as a result. |
| **Number format**               | -                               | Cells with numeric values may only be in standard or number format. Other formats, such as accounting or user-defined, are not allowed. <br> Excel deletes leading zeros from numbers, which can lead to problems, especially with article numbers or GTINs. In such a case, the partner should format the affected column as text. | Example - number formats |
| **Date**                         | Option 1: YYYY-MM-DD           | Option 2: DD.MM.YYYY            | One-digit days/months must be supplemented with a leading zero, e.g. 2020-07-01 or 01.07.2020. <br> Separators are to be used according to the formats. |


Notes on Documentation 
| **Topic**                        | **Designation**                                                                          | **Example**                              |
|----------------------------------|-----------------------------------------------------------------------------------------|------------------------------------------|
| **Data declaration**             | Str: String, Text <br> Int: Integer, Number <br> Short: (Signed short) 0 - 32'767 <br> Ushort: (Unsigned short) 0 - 65’535 <br> Dec: Decimal, Number with decimals <br> Bool: Boolean, true/false, 1/0 <br> Date: Date | -                                        |
| **Information in brackets**      | After the data type, the permissible number range, the number of characters, or the format is indicated in brackets. <br> (10): A maximum of 10 digits or characters are permitted. <br> (10,4): A maximum of 10 digits before and 4 digits after the decimal point are permitted. <br> (8-14): Between 8 and 14 digits or characters are allowed. | Str(100), Int(5), Dec(8,4)              |
| **Indexing**                     | *index: Describes a consecutive numbering of columns with the same content.             | ImageUrl_1, ImageUrl_2                  |
| **Languages**                    | *language: Describes the language identification of language-dependent columns with the same content. (following standard ISO 639-1) | ProductTitle_de, ProductTitle_en        |
| **Currency**                     | *currency: Describes how to mark the currency of price columns. Prices can be indicated in CHF or in EUR. | SalesPriceExclVat_CHF, PurchasePriceExclVat_EUR |
| **Units**                        | *unit: Describes the labelling of the units of dimension-specific columns. The permissible units are indicated in the corresponding columns. | Weight_g, Length_cm                     |
| **Countries**                    | *country: Describes the country identification of country-specific columns. (following standard ISO 3166 ALPHA-2) | ReleaseDate_CH, ReleaseDate_DE         |


Minimal criteria: PriceData Supplier Model (Retail / Vendor)
| **Header**                     | **Data type**                    | **Designation**                            | **Description**                                                                                                                                                                     |
|---------------------------------|-----------------------------------|--------------------------------------------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| **ProviderKey**                 | Str(50) <br> ASCII 32-126         | Partner specific item number              | Unique identification number of the partner item. <br> The item numbers must be unique. Duplicates are not permitted. <br> We expect only characters in the range ASCII 32-126.      |
| **PurchasePriceExclVat_[currency]** | Dec(8,4)                          | Purchase price excl. VAT                  | Digitec Galaxus expects purchase prices excl. VAT and incl./excl. product fees. <br> "currency": Specification of the currency in the header in CHF or EUR. <br> Marketplace CH: Only CHF is accepted as currency. |

Minimal criteria: PriceData Merchant

| Header                                 | Data type | Designation                          | Description                                                                                 |
|----------------------------------------|-----------|--------------------------------------|---------------------------------------------------------------------------------------------|
| **SuggestedRetailPriceInclVat_[currency]** | Dec(8,4)  | Recommended retail price incl. VAT. | *Recommended retail price incl. VAT.*  <br> _"currency": Specification of the currency in the header in CHF or EUR._ |
| **VatRatePercentage**                 | Dec(2,2)  | VAT rate                             | *VAT rate as a figure without % sign.* <br> _E.g.: Switzerland: 8.1, 2.6, 0.0_              |


Supplementary criteria: Price data supplier model (Retail / Vendor)

| **Header**                      | **Data type**                | **Designation**                                      | **Description**                                                   |
|---------------------------------|------------------------------|----------------------------------------------------|-------------------------------------------------------------------|
| **ProviderKey**                 | Str(50) <br> ASCII 32-126    | Partner specific item number                       | Unique identification number of the partner item. <br> The item numbers must be unique. Duplicates are not permitted. <br> We expect only characters in the range ASCII 32-126 |
| **SalesPriceExclVat_[currency]**| Dec(8,4)                     | Sales price excl. VAT                               | Sales price of a retail pack (ConsumerUnit, CU) excl. VAT <br> "Currency": Specification of the currency in the header in CHF or EUR. |
| **VatRatePercentage**           | Dec(2,2)                     | VAT rate                                           | VAT rate as a figure without % sign. <br> E.g.: Switzerland: 8.1, 2.6, 0.0 |

Minimal criteria Stock Data
| **Header**                            | **Data type** | **Designation**            | **Description**                                                                                     |
|---------------------------------------|---------------|----------------------------|-----------------------------------------------------------------------------------------------------|
| **ProviderKey**                       | Str(50)       | Partner specific item number | Unique identification number of the partner item. <br> The item numbers must be unique. Duplicates are not permitted. <br> We expect only characters in the range ASCII 32-126. |
| **QuantityOnStock**                   | Int(5)        | Exact stock quantity        | We recommend submitting the exact stock quantity. This value influences the prioritisation of the dealer/supplier when selecting sales offers. <br> Negative stocks are treated as backlogs. |


Supplementary criteria for optimizing availability and order processing

# Supplementary criteria for optimizing availability and order processing

The following criteria optimize availability and order processing:

| **Header**                | **Data type** | **Designation**                    | **Description**                                                                                                               |
|---------------------------|---------------|-------------------------------------|-------------------------------------------------------------------------------------------------------------------------------|
| **RestockTime**            | Int(3)        | Repurchase period in working days   | Restocking or provisioning time in working days until the item is back in the partner's warehouse. (RestockDate - today). National holidays do not have to be considered. <br> *Maximum value = 365* <br> If the value 0 (days) is transmitted, we interpret it the same way as if no value were transmitted. <br> Either RestockTime OR RestockDate is possible - not both. <br> RestockTime or RestockDate are considered only if the quantity on stock is equal to 0. In case of a positive stock quantity, restocktime or restockdate are ignored. |
| **RestockDate**           | Date          | Repurchase date                    | Date from which the item is back in stock at the partner. <br> *We recommend specifying the replenishment period or the replenishment date in all cases (regardless of the stock level).* This information has an impact on availability and is used for automated ordering processes. <br> *Warning*: Either RestockTime OR RestockDate is possible - not both. <br> RestockTime or RestockDate are considered only if the quantity on stock is equal to 0. In case of a positive stock quantity, restocktime or restockdate are ignored. |
| **ExpectedRestockQuantity** | Int(5)        | Expected repurchase quantity        | The expected available stock at the time the item is back in stock with the partner. <br> *This information is only relevant if the replacement date is available and will be used for automated ordering processes.* <br> Criteria: *Positive number* <br> RestockQuantity = 0 → equals unknown availability |
| **MinimumOrderQuantity**   | Short         | Minimum order quantity (MOQ)        | The minimum orderable quantity of retail packaging (consumer units). <br> Eg: MOQ = 5 → possible order quantities: 5, 6, 7, 8,... <br> MOQ > 1 should only be set for logistical reasons and always in combination with the order quantity step. <br> *Maximum value = 65,535*. If no MOQ value is transmitted, we assume 1 piece as the default value. <br> The specification of MOQ < OQS is not valid. |
| **OrderQuantitySteps**     | Short         | Order quantity step (OQS)           | The minimum orderable quantity and step size of retail packaging (ConsumerUnits). <br> Eg: OQS = 5 → possible order quantities: 5, 10, 15,... <br> OQS > 1 should only be set for logistical reasons and always in combination with the minimum order quantity. <br> *Maximum value = 32,767*. If no OQS value is transmitted, we assume 1 piece as the default value. <br> The specification of OQS > MOQ is not valid. |
| **TradeUnit**              | Ushort        | Overpack unit (TU)                  | Number of retail items (ConsumerUnits) per repackaging quantity. The TU is only relevant for stock deliveries. <br> Eg: TU = 6 → probable order quantities: 6, 12, 18,... <br> *Maximum value = 65,535*. |
| **LogisticUnit**           | Ushort        | Full Pallet quantity (LU)           | Number of items in retail packaging (ConsumerUnits) per pallet. The LU specification is only relevant for warehouse deliveries. <br> *Maximum value = 65,535*. |
| **WarehouseCountry**       | Str(2)        | Warehouse country of origin         | The country of the warehouse from which the item is shipped. This information is relevant for assortments with several warehouses and different delivery times. <br> *Standard ISO 3166-2 Country code* <br> For marketplace traders, we do not currently offer this specification. |
| **WarehouseId**            | Str(50)       | Warehouse identification number     | The identification number of the warehouse. This information is relevant for assortments with multiple warehouses located in the same country. <br> *Global Location Number (GLN) | GS1 Switzerland*. If no GLN can be transmitted, the zip code can also be used. |
| **DirectDeliverySupported**| Bool          | Direct delivery supported           | For suppliers with activated end customer delivery, individual articles can be approved for or excluded from end customer delivery. <br> Permissible values: *true, false / 1, 0* |

Minimum criteria: ProductData for offer creation and allocation (identification features)
| **Header**        | **Data type**           | **Designation**             | **Description**                                                                                                                                          |
|-------------------|-------------------------|-----------------------------|----------------------------------------------------------------------------------------------------------------------------------------------------------|
| **ProviderKey**   | Str(50) <br> *ASCII 32-126* | Partner specific item number | Unique identification number of the partner item. <br> The item numbers must be unique. Duplicates are not permitted. <br> We expect only characters in the range ASCII 32-126. |
| **Gtin**          | Int(8) <br> Int(12) <br> Int(13) <br> Int(14) | EAN/GTIN code of the article | Unique and globally valid identification number of the article. The following data structures are permitted within the GTIN family: <br> - GTIN-12 (UPC), GTIN-13 (EAN-13, ISSN, ISBN), GTIN-14 (EAN/UCC-128 or ITF-14), GTIN-8 (EAN-8). <br> The EAN/GTIN forms the basis for our automated allocation and product creation processes. Therefore, request GTINs for your products for a smooth data transfer: GS1 Switzerland: Request barcode, GS1 Germany: Request barcode. <br> The EAN/GTINs must be unique and globally valid. Duplicates are not permitted. <br> The EAN/GTIN must be visible on the retail packaging. <br> Further information on Gtins and GS1 at: [gtin.info](http://gtin.info) |

