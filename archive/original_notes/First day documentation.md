### Galaxus is MP that work with:

First, they have two ways of working. As of 8/22/2025, there is no API available for work, so we can only work with FTP. We set the files of our data, and then they put a new file for orders, etc. It works in three stages:

1. **Create offers on existing orders**:  
   The first way is that we have existing products (whether they belong to us or anyone else). Then we can create an offer on them so we can change the price, the shipment method, or anything available on the website. Afterward, we publish it to get the orders with at least the minimum requirements.

2. **Activate product creation**:  
   After we set a file that contains the offer, they review the files, apply the offers, and publish it to the market to get the orders from the customers. If there is a problem, they create a file with advice and problems to help us, and they send the details using this email: [pdi@digitecgalaxus.ch](mailto:pdi@digitecgalaxus.ch).

3. **Now after publishing, we should edit the details to ensure that we are making sales.**

---

## Files Structures

| **Stages**  | **Model**  | **Data type** | **Sample files** |
|-------------|------------|---------------|------------------|
| **Stage 1** | **Supplier** _(Retail / Vendor)_ | **csv** | [Minimal](TemplateFiles_E1_retail_min_csv.zip?api=v2), [Extended](TemplateFiles_E1_retail_ext_csv.zip?api=v2) |
|             |            | **xlsx** | [Minimal](TemplateFiles_E1_retail_min_xlsx.zip?api=v2), [Extended](TemplateFiles_E1_retail_ext_xlsx.zip?api=v2) |
| **Stage 2** | **Supplier** _(Retail / Vendor)_ / **Merchant** | **csv** | [Minimal](TemplateFiles_E2_min_csv.zip?api=v2), [Extended](TemplateFiles_E2_ext_csv.zip?api=v2) |
|             |            | **xlsx** | [Minimal](TemplateFiles_E2_min_xlsx.zip?api=v2), [Extended](TemplateFiles_E2_ext_xlsx.zip?api=v2) |
| **Stage 3** | **All** | **csv** | [Minimal](TemplateFiles_E3_csv.zip?api=v2) |
|             |            | **xlsx** | [Minimal](TemplateFiles_E3_xlsx.zip?api=v2) |
| **All stages** | **Supplier** _(Retail / Vendor)_ | **csv** | [Extended](TemplateFiles_E1E2E3_retail_ext_csv.zip?api=v2) |
|             |            | **xlsx** | [Extended](TemplateFiles_E1E2E3_retail_ext_xlsx.zip?api=v2) |
| **Merchant / EU-Hub** | **csv** | [Extended](Templatefiles_E1E2E3_marketplace_ext_csv.zip?api=v2) |
|             |            | **xlsx** | [Extended](TemplateFiles_E1E2E3_marketplace_ext_xlsx.zip?api=v2) |

---

## The Conclusion of the First Day:

I realized that we work with file structures, where we have some fixed structures for each stage.  
Also, there are some rules for the file content, like columns and data, which I can find in Stage 1 file structure.  
Additionally, we have some images and files that help me understand how my code should handle the products, set the offers, and manage the media.

---

## Remain Read from Stage 3.
