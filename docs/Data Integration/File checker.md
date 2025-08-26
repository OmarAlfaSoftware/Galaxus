# File Cheeker Information

this file contains all information related to file cheeker

file cheeker its a toll provided by galaxus team to help the developers that need to make integration with them to check on the files if it has something wrong or need some modification or advices to improve the file before publish it

## Installation

first this is the tool link for installation [Installation]("https://confdg.atlassian.net/wiki/download/attachments/168804843821/DG_Feedchecker.zip?api=v2")
then extrat the zip file then run the app

## INSTRUCTIONS

### Step 1: Provider Type Selection

First, the provider type must be determined, depending on the provider type. You can choose between:

* Marketplace - Merchant in CH
* Marketplace EU-Hub (CH) - Merchant in CH from EU with CH fiscal representation
* Marketplace EU-Hub (EU) - Merchant in CH from EU without CH fiscal representation
* Retail (CH) - Swiss Supplier
* Retail (Non-CH) - Supplier worldwide

### Step 2: File Selection

Select file/feed for verification (supported types are CSV or XLSX (Excel))

### Step 3: CSV Separator Configuration

For CSV files, the separator should also be determined. Possible options are:

* auto (automatic detection of separator)
* semicolon (;)
* comma (,)
* tabulator (Tab ↹)
* hyphen (|)

**Note:** If the auto option detects the separator incorrectly, it is recommended to directly select the separator used.

### Step 4: Import Feed

Click on "Import Feed". This will import the selected file and the result will be shown in the "Status" field (8). If successful, a preview of the file is displayed in the "Preview" field (9).

The "Preview" field shows the first 10 lines from the file that were read in for checking.

### Step 5: Export Directory

You can specify the directory where the feedcheck report can be exported. If nothing is entered, the report will be exported to the same directory as the checked file.

### Step 6: Language Selection

You can select the language of the feedcheck report.

### Step 7: Start Feed Checker

Start Feedcheck by clicking on "Start Feed Checker". A pop-up message informs the user that the check has been completed and the report exported.

### Step 8: Start Image Checker

Start the image link check by clicking on "Start Image Checker". A pop-up message informs the user that the check has been completed and the report exported.

### Status Interface

On the "Status" interface, the user is informed about the progress of the checks. The error messages that prevent the file from being read in for the check are also displayed here (E.g. text parsing issues)

## The Validation Process

| **Columns** | **Validation** | **Alias** |
|-------------|----------------|-----------|
| **FEEDCHECKER EXAMINATION** ||| 
| whole file | • Does a row contain more or less columns than the header?<br>• Are the values correctly parsed? | |
| headers | • Does it contain mandatory columns (depending on provider and feed type)?<br>• does it contain duplicates?<br>• does it contain empty headers?<br>• does it contain line breaks? | |
| ProviderKey | • is empty?<br>• are there duplicates?<br>• is > 100 characters?<br>• does it contain characters outside ASCII 32-126?<br>• is sorted? *(only relevant for SpecificationData variant 1)* | *productkey, artikelnr, lieferant artikelnr., artikel-nr., lieferant-artikel-nr., artikelnr., lieferanten artikel-nr.* |
| Gtin | • is empty?<br>• are there duplicates?<br>• is valid (length 8, 12, 13, 14)?<br>• is check digit correct?<br>• is internal?<br>• is voucher? | *ean-nummer, ean-code, eancode, ean code, barcode, upc, ean nummer, gtin / ean, upc-code, produkt gtin, ean/upc-code, ean-nr., ean, gtin/ean, ean/gtin, eannummer* |
| ManufacturerKey | • are there duplicates?<br>• is > 4 and < 50 characters? | *hersteller nummer, hersteller-nr, hersteller-nummer, herstellernr, herstellernummer, herstellernr., hersteller-nr., hersteller nr.* |
| BrandName | • is empty? | *marke, brandname* |
| ProductCategory | • is empty?<br>• is > 200 characters? | *kategorie, category, hauptkategorie, kategorisierung, produktkategorie, produkttyp* |
| Weight_g | ℹ️ only for EU hub<br>• is empty?<br>• is 0?<br>• does it contain thousand separators? | *weight, bruttogewicht, weightkg, weight_kg, gewicht, weightg* |
| TaresCode | ℹ️ only for EU hub<br>• is empty?<br>• is valid (formats "12345678" or "1234.5678") | *tares* |
| TaricCode | ℹ️ only for EU hub<br>• is empty?<br>• is 6-11 digits (without spaces)? | *taric* |
| CountryOfOrigin | ℹ️ only for EU hub<br>• is empty?<br>• is 2-digit? | *ursprungsland* |
| ProductTitle_de | • is empty?<br>• is < 100 characters?<br>• does it contain the mark at the end? | *produktname, productname, produktname deutsch, producttitlede, artikelname, product name, produktname (de), name, producttitle* |
| LongDescription_de | • is < 4000 characters? | *productdescription, productdescription_de, beschreibung, marketing beschreibung* |
| MainImageUrl | • is empty?<br>• is < 300 characters?<br>• is the URL text valid? | *imgeurl, , image_url, bild* |
| ImageUrl_1 | • is < 300 characters?<br>• is the URL text valid? | *imageurl_2, imageurl_3, imageurl_4, imageurl_5, imageurl_6, imageurl_7, imageurl_8* |
| VatRatePercentage | ℹ️ only for marketplace and Eu hub with fisc. Representation<br>• is empty?<br>• is CH (8.1 or 2.6)? | *mwst., mwst-satz, vatrate, mwst* |
| PurchasePriceExclVatAndFee | ℹ️ only for Retail<br>• is empty?<br>• is 0?<br>• does it contain thousand separators? | *purchasepriceexcludingvatandfee, purchasepriceexcludingfee, purchasepriceexclfee* |
| PurchasePriceExclVat | ℹ️ only for Retail<br>• is empty?<br>• is 0?<br>• does it contain thousand separators? | *einkaufspreis (exkl.), purchasepriceexclvat_[currency], purchasepriceexclvat_chf, purchasepriceexclvat_eur, ek, ep netto, ek netto, einkaufspreis, price, ep, purchasepriceexcludingvat, ek chf, preis* |
| SalesPriceExclVat | ℹ️ only for Marketplace and Eu-Hub<br>• is empty?<br>• is 0?<br>• does it contain thousand separators? | *vk preis, vk, vk-preis, salespriceexcludingvat, verkaufspreis, salespriceexclvat_[currency], salespriceexclvat_chf, salespriceexclvat_eur* |
| QuantityOnStock | • is empty?<br>• does it contain thousand separators? | *lagerbestand, bestand, stock* |
| RestockDate | • is the format valid ("YYYY-MM-DD" or "DD.MM.YYYY")?<br>• is it in the past? | *deliverydate* |
| RestockTime | • is< 365? | |
| MinimumOrderQuantity_dd | • is < OrderQuantitySteps?<br>• is integer? | *mindestbestellmenge (stück), mindestbestellmenge, bestellmengen, minimumorderquantity* |
| MinimumOrderQuantity_wd | • is < OrderQuantitySteps?<br>• is integer? | |
| OrderQuantitySteps | • is empty? | *bestellmengenschritt* |
| SpecificationKey | • is < 200 characters?<br>• does it contain HTML tags? | *specificationkey_de* |
| SpecificationValue | • is < 200 characters?<br>• does it contain HTML tags? | *specificationvalue_de* |
| **IMAGE CHECKER Examination** |||
| MainImageUrl | • is the URL text valid?<br>• Is the URL valid?<br>• Does the URL redirect to another URL?<br>• Is the image size < 14 MB<br>• Is the larger dimension of the image > 400px? | *imgeurl, image_url, bild, bildurl, bild_url* |
| ImageUrl_1 | • is the URL text valid?<br>• Is the URL valid?<br>• Does the URL redirect to another URL?<br>• Is the image size < 14 MB<br>• Is the larger dimension of the image > 400px? | *imageurl_2, imageurl_3, imageurl_4, imageurl_5, imageurl_6, imageurl_7, imageurl_8* |