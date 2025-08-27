now in the order process also know as GORDP
first its name must be like this **GORDP_<SupplierId>_<OrderId>.xml**
and its only exists in dg2partner folder
the purpose of this file is to tell us that there is a new order coming from Galaxus and wait for the confirmation from us in ORDR

this xml is consist from a static structure as this image ![structure](<../Images/Order/order strucutre.png>)

lets dive into this structure and explain it
first we have the header and we talk about this in the [Readme.md](<./Readme.md>)
after that we have the order header and this header contain the public info of the order like the id, date, language, and the parties

the parties its like a group of the sides like the buyer, gln, warehouse delveries or direct delivery.
and the details of the meaning of each one will found in the table below,
each party consist of the id, role, address and the address consisit of contacts and the details that related to this role

after that we have the order reference that have the order id and its not exists in all the types of the order as we have 12 type of requests 
they will be exists at the end of this file
then we have order parties refernce and that carry the internal customer id and the partner id
then we have the currency and the header_UDX (user defined extension)
then the order list that carry the order items

each order item carry the line item id (Continuous document line numbering, starting at «1». Is not needed in the answer documents)
then product id and their data,  then the Quantity and the orderunit  and the product price fix

the price line amount and the delivery date start, end
then the order summary that has total item number and amount.


|     |     |     |     |     |
| --- | --- | --- | --- | --- |
| **XML Element** | **M/C/S** | **Sample values** | [**Data type**](https://confdg.atlassian.net/wiki/spaces/PI/pages/168689833771 "https://confdg.atlassian.net/wiki/spaces/PI/pages/168689833771") **\[maxLength\]** | **Description** |
| ORDER | Must | \-  | \-  | See [Namespaces](https://confdg.atlassian.net/wiki/spaces/PI/pages/168689833747 "https://confdg.atlassian.net/wiki/spaces/PI/pages/168689833747") regarding the correct usage of namespaces. |
| . ORDER\_HEADER | Must | \-  | \-  |     |
| . . CONTROL\_INFO | Must | \-  | \-  |     |
| . . . GENERATION\_DATE | Must | 2017-09-22T15:42:57 | dtDATETIME | Order placement date |
| . . ORDER\_INFO | Must | \-  | \-  |     |
| . . . ORDER\_ID | Must | 9316271 | dtSTRING\[25\] | Galaxus purchase order ID  <br>![Warning](https://pf-emoji-service--cdn.us-east-1.prod.public.atl-paas.net/atlassian/productivityEmojis/exclamation-32px.png) is unique per order. Even with multiple order placements, only one shipment of goods should ever be made |
| . . . ORDER\_DATE | Must | 2017-09-22T15:30:33 | dtDATETIME | Date of order entry at Digitec Galaxus |
| . . . LANGUAGE | Must | ger, eng, fra, ita | dtLANG<br><br>**BMEcat NS** | Language of end customer according to ISO-639-2/B, must be used on paper delivery notes and in RESPONSECOMMENTs. |
| . . . PARTIES | Must | \-  | \-  |     |
| . . . . PARTY | Must | \-  | \-  |     |
| . . . . . PARTY\_ID | Must | 2045638, 7640151820008<br><br>CH, DE, AT (marketplace) | dtSTRING\[250\]<br><br>**BMEcat NS** | **type="buyer\_specific"** is the Galaxus internal customer ID (except with the PARTY\_ROLE = marketplace where this value can be customized)<br><br>**type="gln"** ist the GLN number (also known as ILN) of the commercial register address or of the warehouse address. Only transmitted if available.<br><br>**Warehouse deliveries**  <br>For PARTY\_ROLE = marketplace and UDX.DG.DELIVERY\_TYPE = warehouse\_delivery PARTY\_ID corresponds to COUNTRY\_CODED of PARTY\_ROLE = buyer (CH for Switzerland or DE for the whole EU)<br><br>**Direct deliveries (dealer & supplier)**  <br>If PARTY\_ROLE = marketplace and UDX.DG.DELIVERY\_TYPE = direct\_delivery, PARTY\_ID corresponds to the country of the sales portal (ISO 3166-2) |
| . . . . . PARTY\_ROLE | Must | buyer, supplier, delivery, marketplace | dtSTRING\[20\] | **buyer**  <br>**supplier**  <br>**delivery**  <br>**marketplace**<br><br>For suppliers, the buyer is always Digitec Galaxus AG  <br>For dealers, the buyer is always the end customer |
| . . . . . ADDRESS | Must | \-  | \-  | ![Warning](https://pf-emoji-service--cdn.us-east-1.prod.public.atl-paas.net/atlassian/productivityEmojis/exclamation-32px.png) All address fields/combinations can occur with both UDX.DG.CUSTOMER\_TYPE=**private\_customer** and UDX.DG.CUSTOMER\_TYPE=**company** |
| . . . . . . NAME | Can \* | Mustermann GmbH | dtMLSTRING\[50\]<br><br>**BMEcat NS** | Company name<br><br>For **EU-Hub partners** with the PARTY\_ROLE = delivery, always Galaxus Deutschland GmbH is transmitted (also for private customers) |
| . . . . . . NAME2 | Can | Ulla Mustermann | dtMLSTRING\[50\]<br><br>**BMEcat NS** | For the attention of |
| . . . . . . NAME3 | Can | Block A | dtMLSTRING\[50\]<br><br>**BMEcat NS** | Building |
| . . . . . . DEPARTMENT | Can | Accounting | dtMLSTRING\[50\]<br><br>**BMEcat NS** | Department |
| . . . . . . CONTACT\_DETAILS | Can | \-  | \-  |     |
| . . . . . . . TITLE | Can | Mr., Ms. | dtMLSTRING\[20\]<br><br>**BMEcat NS** | Salutation (depending on the language of the customer) |
| . . . . . . . FIRST\_NAME | Can \* | Ulla | dtMLSTRING\[50\]<br><br>**BMEcat NS** | First name  <br>\* = Either company name or first name and surname are mandatory |
| . . . . . . . CONTACT\_NAME | Can \* | Mustermann | dtMLSTRING\[50\]<br><br>**BMEcat NS** | Last name  <br>\* = Either company name or first name and surname are mandatory |
| . . . . . . STREET | Can | Musterstrasse 200b | dtMLSTRING\[50\]<br><br>**BMEcat NS** | Street and house number |
| . . . . . . ZIP | Must | 8000 | dtMLSTRING\[20\]<br><br>**BMEcat NS** | ZIP code |
| . . . . . . BOXNO | Can | 7   | dtMLSTRING\[20\]<br><br>**BMEcat NS** | P.O. box number (the text _P.O. box_ must be added to the address label) |
| . . . . . . CITY | Must | Zürich | dtMLSTRING\[50\]<br><br>**BMEcat NS** | City |
| . . . . . . COUNTRY | Must | Schweiz, Deutschland, Liechtenstein, Österreich | dtMLSTRING\[50\]<br><br>**BMEcat NS** | Country (transmitted in German only) |
| . . . . . . COUNTRY\_CODED | Must | CH, DE, LI, AT | dtCOUNTRIES<br><br>**BMEcat NS** | Country code according ISO 3166-1 ALPHA-2 |
| . . . . . . PHONE | Can | 0448447700 | dtMLSTRING\[50\]<br><br>**BMEcat NS** | Phone number |
| . . . . . . EMAIL | Must | noreply@galaxus.ch, noreply@galaxus.de | dtSTRING\[255\]<br><br>**BMEcat NS** | Galaxus Email Address (hardcoded)  <br>![Warning](https://pf-emoji-service--cdn.us-east-1.prod.public.atl-paas.net/atlassian/productivityEmojis/exclamation-32px.png) Please _do not_ send any emails to this address, the mailbox is not maintained  <br>![Question Mark](https://pf-emoji-service--cdn.us-east-1.prod.public.atl-paas.net/atlassian/productivityEmojis/question-32px.png) The email address of the end customer can be viewed in the [Partner Portal](https://confdg.atlassian.net/wiki/spaces/PI/pages/169009284340 "https://confdg.atlassian.net/wiki/spaces/PI/pages/169009284340") if required (carrier notification, service case):<br><br>Click here to expand<br><br>Open image-20220913-140718.png<br><br>![](blob:https://confdg.atlassian.net/f890d6e5-26a8-4839-b3d2-23c6f0a7de61#media-blob-url=true&id=81a22a24-60a0-44cf-955e-bba2d0604a09&collection=contentId-168689833798&contextId=168689833798&mimeType=image%2Fpng&name=image-20220913-140718.png&size=4935&width=404&height=67&alt=) |
| . . . . . . VAT\_ID | Can | CHE-109.049.266 | dtMLSTRING\[50\]<br><br>**BMEcat NS** | VAT number (currently _only_ for supplier orders the VAT\_ID of our commercial register address is transmitted in the PARTY\_ROLE=buyer) |
| . . . CUSTOMER\_ORDER\_REFERENCE | Can | \-  | \-  | Only transmitted for direct deliveries to end customers |
| . . . . ORDER\_ID | Can | 10515922 | dtSTRING\[250\] | End customer Galaxus order number.  <br>Must be printed on the paper delivery note for direct deliveries. |
| . . . ORDER\_PARTIES\_REFERENCE | Must | \-  | \-  |     |
| . . . . BUYER\_IDREF | Must | 2045638 | dtSTRING\[250\]<br><br>**BMEcat NS** | Galaxus internal customer ID |
| . . . . SUPPLIER\_IDREF | Must | 1928008 | dtSTRING\[250\]<br><br>**BMEcat NS** | Galaxus internal partner ID |
| . . . CURRENCY | Must | CHF, EUR | dtCURRENCIES<br><br>**BMEcat NS** | Transaction currency according to ISO 4217:1995 |
| . . . HEADER\_UDX | Must | \-  | udxHEADER |     |
| . . . . UDX.DG.CUSTOMER\_TYPE | Must | private\_customer | dtSTRING\[250\] | **private\_customer**  <br>**company** |
| . . . . UDX.DG.DELIVERY\_TYPE | Must | direct\_delivery | dtSTRING\[250\] | **direct\_delivery**  <br>**warehouse\_delivery** |
| . . . . UDX.DG.SATURDAY\_DELIVERY\_ALLOWED | Can | true, false | dtBOOLEAN | Only transmitted with **direct\_delivery**.  <br>**true** = Saturday delivery allowed, delivery from Monday to Saturday if possible  <br>**false** = Saturday delivery not allowed, no delivery on Saturday<br><br>For orders in the EU is always transmitted **false**. Therefore, please ignore and perform the package delivery from Monday to Saturday. |
| . . . . UDX.DG.IS\_COLLECTIVE\_ORDER | Must | true, false | dtBOOLEAN | **true** = Collective order  <br>**false** = No collective order  <br>Internal field with no relevance for order processing |
| . . . . UDX.DG.END\_CUSTOMER\_ORDER\_REFERENCE | Can | A362799 | dtSTRING\[150\] | Order reference for direct deliveries of business end customers (max. 150 characters).  <br>Must be printed on the paper delivery note. |
| . . . . UDX.DG.PHYSICAL\_DELIVERY\_NOTE\_REQUIRED | Must | false, true | dtBOOLEAN | **true** = Delivery must contain a paper delivery note  <br>**false** = Delivery must not contain a paper delivery note<br><br>If this cannot be distinguished, a delivery note must be included in each package. |
| . ORDER\_ITEM\_LIST | Must | \-  | \-  |     |
| . . ORDER\_ITEM | Must | \-  | \-  |     |
| . . . LINE\_ITEM\_ID | Must | 1   | dtSTRING\[50\] | Continuous document line numbering, starting at «1». Is not needed in the answer documents. |
| . . . PRODUCT\_ID | Must | \-  | \-  | At least 2 of the following 3 IDs will be sent |
| . . . . SUPPLIER\_PID | Must | A375-129 | dtSTRING\[50\]<br><br>**BMEcat NS** | Partner product key |
| . . . . INTERNATIONAL\_PID | Should | 09783404175109 | dtSTRING\[14\]<br><br>**BMEcat NS** | GTIN-14 with leading zeros - is only transmitted if also included in the product data feed<br><br>![Info](https://pf-emoji-service--cdn.us-east-1.prod.public.atl-paas.net/atlassian/productivityEmojis/information-32px.png) GTIN-8,-12,-13 are converterd to GTIN-14 (by adding leading zeroes) |
| . . . . BUYER\_PID | Must | 6406567 | dtSTRING\[50\]<br><br>**BMEcat NS** | Galaxus product key |
| . . . . DESCRIPTION\_SHORT | Must | Fingerring, Herr der Ringe | dtMLSTRING\[150\]<br><br>**BMEcat NS** | Galaxus internal product name (without brand) |
| . . . QUANTITY | Must | 2   | dtNUMBER | Item position quantity |
| . . . ORDER\_UNIT | Must | C62 | dtPUNIT\[2\]<br><br>**BMEcat NS** | Always C62 (hardcoded), unit = piece |
| . . . PRODUCT\_PRICE\_FIX | Must | \-  | \-  |     |
| . . . . PRICE\_AMOUNT | Must | 12.59 | dtNUMBER<br><br>**BMEcat NS** | Unit price excl. VAT |
| . . . . TAX\_DETAILS\_FIX | Must | \-  | \-  |     |
| . . . . . TAX\_AMOUNT | Must | 1.02 | dtNUMBER | VAT amount per piece |
| . . . PRICE\_LINE\_AMOUNT | Must | 25.18 | dtNUMBER | Unit Price excl. VAT \* quantity |
| . . . DELIVERY\_DATE | Can | \-  | \-  | Latest delivery date according to calculated standard delivery time<br><br>type="**optional**" (default): Always fastest possible delivery until this date  <br>type="**fixed**": Fixed order - delivery exactly on this date |
| . . . . DELIVERY\_START\_DATE | Can | 2020-11-30T00:00:00 | dtDATETIME | Latest arrival date |
| . . . . DELIVERY\_END\_DATE | Can | 2020-11-30T00:00:00 | dtDATETIME | Same value as in DELIVERY\_START\_DATE |
| . ORDER\_SUMMARY | Must | \-  | \-  |     |
| . . TOTAL\_ITEM\_NUM | Must | 2   | dtCOUNT | Sum of all QUANTITYs |
| . . TOTAL\_AMOUNT | Must | 25.18 | dtNUMBER | Sum of all PRICE\_LINE\_AMOUNTs excl. VAT |


then we have all the possible of the incoming order style like.

1. private customer
 ```<?xml version="1.0" encoding="utf-8"?>
<ORDER xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" version="2.1" type="standard" xmlns="http://www.opentrans.org/XMLSchema/2.1">
	<ORDER_HEADER>
		<CONTROL_INFO>
			<GENERATION_DATE>2022-03-28T11:31:27</GENERATION_DATE>
		</CONTROL_INFO>
		<ORDER_INFO>
			<ORDER_ID>61730204</ORDER_ID>
			<ORDER_DATE>2022-03-28T11:31:19</ORDER_DATE>
			<LANGUAGE xmlns="http://www.bmecat.org/bmecat/2005">ger</LANGUAGE>
			<PARTIES>
				<PARTY>
					<PARTY_ID type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">406802</PARTY_ID>
					<PARTY_ID type="gln" xmlns="http://www.bmecat.org/bmecat/2005">7640151820008</PARTY_ID>
					<PARTY_ROLE>buyer</PARTY_ROLE>
					<ADDRESS>
						<NAME xmlns="http://www.bmecat.org/bmecat/2005">Digitec Galaxus AG</NAME>
						<DEPARTMENT xmlns="http://www.bmecat.org/bmecat/2005">Accounting</DEPARTMENT>
						<STREET xmlns="http://www.bmecat.org/bmecat/2005">Pfingstweidstrasse 60b</STREET>
						<ZIP xmlns="http://www.bmecat.org/bmecat/2005">8005</ZIP>
						<CITY xmlns="http://www.bmecat.org/bmecat/2005">Zürich</CITY>
						<COUNTRY xmlns="http://www.bmecat.org/bmecat/2005">Schweiz</COUNTRY>
						<COUNTRY_CODED xmlns="http://www.bmecat.org/bmecat/2005">CH</COUNTRY_CODED>
						<EMAIL xmlns="http://www.bmecat.org/bmecat/2005">noreply@galaxus.ch</EMAIL>
						<VAT_ID xmlns="http://www.bmecat.org/bmecat/2005">CHE-109.049.266</VAT_ID>
					</ADDRESS>
				</PARTY>
				<PARTY>
					<PARTY_ID type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">407514</PARTY_ID>
					<PARTY_ROLE>supplier</PARTY_ROLE>
					<ADDRESS>
						<NAME xmlns="http://www.bmecat.org/bmecat/2005">Musterfirma AG</NAME>
						<STREET xmlns="http://www.bmecat.org/bmecat/2005">Teststrasse 17</STREET>
						<ZIP xmlns="http://www.bmecat.org/bmecat/2005">8000</ZIP>
						<CITY xmlns="http://www.bmecat.org/bmecat/2005">Zürich</CITY>
						<COUNTRY xmlns="http://www.bmecat.org/bmecat/2005">Schweiz</COUNTRY>
						<COUNTRY_CODED xmlns="http://www.bmecat.org/bmecat/2005">CH</COUNTRY_CODED>
						<EMAIL xmlns="http://www.bmecat.org/bmecat/2005">noreply@galaxus.ch</EMAIL>
					</ADDRESS>
				</PARTY>
				<PARTY>
					<PARTY_ID type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">2210700</PARTY_ID>
					<PARTY_ROLE>delivery</PARTY_ROLE>
					<ADDRESS>
						<CONTACT_DETAILS>
							<TITLE xmlns="http://www.bmecat.org/bmecat/2005">Herr</TITLE>
							<FIRST_NAME xmlns="http://www.bmecat.org/bmecat/2005">Richard &amp; Annelies</FIRST_NAME>
							<CONTACT_NAME xmlns="http://www.bmecat.org/bmecat/2005">Kaufmann+Meiermann Co.</CONTACT_NAME>
						</CONTACT_DETAILS>
						<STREET xmlns="http://www.bmecat.org/bmecat/2005">Bahnhofstrasse 50</STREET>
						<ZIP xmlns="http://www.bmecat.org/bmecat/2005">8957</ZIP>
						<BOXNO xmlns="http://www.bmecat.org/bmecat/2005">75</BOXNO>
						<CITY xmlns="http://www.bmecat.org/bmecat/2005">Spreitenach</CITY>
						<COUNTRY xmlns="http://www.bmecat.org/bmecat/2005">Schweiz</COUNTRY>
						<COUNTRY_CODED xmlns="http://www.bmecat.org/bmecat/2005">CH</COUNTRY_CODED>
						<PHONE xmlns="http://www.bmecat.org/bmecat/2005">+41440000000</PHONE>
						<EMAIL xmlns="http://www.bmecat.org/bmecat/2005">noreply@galaxus.ch</EMAIL>
					</ADDRESS>
				</PARTY>
				<PARTY>
					<PARTY_ID type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">10709476</PARTY_ID>
					<PARTY_ROLE>marketplace</PARTY_ROLE>
					<ADDRESS>
						<NAME xmlns="http://www.bmecat.org/bmecat/2005">Digitec Galaxus AG</NAME>
						<STREET xmlns="http://www.bmecat.org/bmecat/2005">Pfingstweidstrasse 60b</STREET>
						<ZIP xmlns="http://www.bmecat.org/bmecat/2005">8005</ZIP>
						<CITY xmlns="http://www.bmecat.org/bmecat/2005">Zürich</CITY>
						<COUNTRY xmlns="http://www.bmecat.org/bmecat/2005">Schweiz</COUNTRY>
						<COUNTRY_CODED xmlns="http://www.bmecat.org/bmecat/2005">CH</COUNTRY_CODED>
						<EMAIL xmlns="http://www.bmecat.org/bmecat/2005">noreply@galaxus.ch</EMAIL>
					</ADDRESS>
				</PARTY>
			</PARTIES>
			<CUSTOMER_ORDER_REFERENCE>
				<ORDER_ID>61730203</ORDER_ID>
			</CUSTOMER_ORDER_REFERENCE>
			<ORDER_PARTIES_REFERENCE>
				<BUYER_IDREF type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">406802</BUYER_IDREF>
				<SUPPLIER_IDREF type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">407514</SUPPLIER_IDREF>
			</ORDER_PARTIES_REFERENCE>
			<CURRENCY xmlns="http://www.bmecat.org/bmecat/2005">CHF</CURRENCY>
			<HEADER_UDX>
				<UDX.DG.CUSTOMER_TYPE>private_customer</UDX.DG.CUSTOMER_TYPE>
				<UDX.DG.DELIVERY_TYPE>direct_delivery</UDX.DG.DELIVERY_TYPE>
				<UDX.DG.IS_COLLECTIVE_ORDER>false</UDX.DG.IS_COLLECTIVE_ORDER>
				<UDX.DG.PHYSICAL_DELIVERY_NOTE_REQUIRED>false</UDX.DG.PHYSICAL_DELIVERY_NOTE_REQUIRED>
			</HEADER_UDX>
		</ORDER_INFO>
	</ORDER_HEADER>
	<ORDER_ITEM_LIST>
		<ORDER_ITEM>
			<LINE_ITEM_ID>1</LINE_ITEM_ID>
			<PRODUCT_ID>
				<SUPPLIER_PID type="supplierProductKey" xmlns="http://www.bmecat.org/bmecat/2005">4135378</SUPPLIER_PID>
				<INTERNATIONAL_PID type="gtin" xmlns="http://www.bmecat.org/bmecat/2005">00889842737240</INTERNATIONAL_PID>
				<BUYER_PID type="DgProductId" xmlns="http://www.bmecat.org/bmecat/2005">15395678</BUYER_PID>
				<DESCRIPTION_SHORT xmlns="http://www.bmecat.org/bmecat/2005">Microsoft Surface Laptop 4 (13.50 ", Intel Core i7-1185G7, 16 GB, 512 GB)</DESCRIPTION_SHORT>
			</PRODUCT_ID>
			<QUANTITY>1</QUANTITY>
			<ORDER_UNIT xmlns="http://www.bmecat.org/bmecat/2005">C62</ORDER_UNIT>
			<PRODUCT_PRICE_FIX>
				<PRICE_AMOUNT xmlns="http://www.bmecat.org/bmecat/2005">1390.33</PRICE_AMOUNT>
				<TAX_DETAILS_FIX>
					<TAX_AMOUNT>107.06</TAX_AMOUNT>
				</TAX_DETAILS_FIX>
			</PRODUCT_PRICE_FIX>
			<PRICE_LINE_AMOUNT>1390.33</PRICE_LINE_AMOUNT>
			<DELIVERY_DATE type="optional">
				<DELIVERY_START_DATE>2022-07-20T00:00:00</DELIVERY_START_DATE>
				<DELIVERY_END_DATE>2022-07-20T00:00:00</DELIVERY_END_DATE>
			</DELIVERY_DATE>
		</ORDER_ITEM>
	</ORDER_ITEM_LIST>
	<ORDER_SUMMARY>
		<TOTAL_ITEM_NUM>1</TOTAL_ITEM_NUM>
		<TOTAL_AMOUNT>1390.33</TOTAL_AMOUNT>
	</ORDER_SUMMARY>
</ORDER> 
```
2. private customer with company address
```<?xml version="1.0" encoding="utf-8"?>
<ORDER xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" version="2.1" type="standard" xmlns="http://www.opentrans.org/XMLSchema/2.1">
	<ORDER_HEADER>
		<CONTROL_INFO>
			<GENERATION_DATE>2022-03-28T11:55:32</GENERATION_DATE>
		</CONTROL_INFO>
		<ORDER_INFO>
			<ORDER_ID>61730211</ORDER_ID>
			<ORDER_DATE>2022-03-28T11:55:29</ORDER_DATE>
			<LANGUAGE xmlns="http://www.bmecat.org/bmecat/2005">ger</LANGUAGE>
			<PARTIES>
				<PARTY>
					<PARTY_ID type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">406802</PARTY_ID>
					<PARTY_ID type="gln" xmlns="http://www.bmecat.org/bmecat/2005">7640151820008</PARTY_ID>
					<PARTY_ROLE>buyer</PARTY_ROLE>
					<ADDRESS>
						<NAME xmlns="http://www.bmecat.org/bmecat/2005">Digitec Galaxus AG</NAME>
						<DEPARTMENT xmlns="http://www.bmecat.org/bmecat/2005">Accounting</DEPARTMENT>
						<STREET xmlns="http://www.bmecat.org/bmecat/2005">Pfingstweidstrasse 60b</STREET>
						<ZIP xmlns="http://www.bmecat.org/bmecat/2005">8005</ZIP>
						<CITY xmlns="http://www.bmecat.org/bmecat/2005">Zürich</CITY>
						<COUNTRY xmlns="http://www.bmecat.org/bmecat/2005">Schweiz</COUNTRY>
						<COUNTRY_CODED xmlns="http://www.bmecat.org/bmecat/2005">CH</COUNTRY_CODED>
						<EMAIL xmlns="http://www.bmecat.org/bmecat/2005">noreply@galaxus.ch</EMAIL>
						<VAT_ID xmlns="http://www.bmecat.org/bmecat/2005">CHE-109.049.266</VAT_ID>
					</ADDRESS>
				</PARTY>
				<PARTY>
					<PARTY_ID type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">407514</PARTY_ID>
					<PARTY_ROLE>supplier</PARTY_ROLE>
					<ADDRESS>
						<NAME xmlns="http://www.bmecat.org/bmecat/2005">Musterfirma AG</NAME>
						<STREET xmlns="http://www.bmecat.org/bmecat/2005">Teststrasse 17</STREET>
						<ZIP xmlns="http://www.bmecat.org/bmecat/2005">8000</ZIP>
						<CITY xmlns="http://www.bmecat.org/bmecat/2005">Zürich</CITY>
						<COUNTRY xmlns="http://www.bmecat.org/bmecat/2005">Schweiz</COUNTRY>
						<COUNTRY_CODED xmlns="http://www.bmecat.org/bmecat/2005">CH</COUNTRY_CODED>
						<EMAIL xmlns="http://www.bmecat.org/bmecat/2005">noreply@galaxus.ch</EMAIL>
					</ADDRESS>
				</PARTY>
				<PARTY>
					<PARTY_ID type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">2210700</PARTY_ID>
					<PARTY_ROLE>delivery</PARTY_ROLE>
					<ADDRESS>
						<NAME xmlns="http://www.bmecat.org/bmecat/2005">Muster AG AG</NAME>
						<NAME2 xmlns="http://www.bmecat.org/bmecat/2005">Max Müller</NAME2>
						<NAME3 xmlns="http://www.bmecat.org/bmecat/2005">Gebäude B</NAME3>
						<DEPARTMENT xmlns="http://www.bmecat.org/bmecat/2005">Finanzen</DEPARTMENT>
						<STREET xmlns="http://www.bmecat.org/bmecat/2005">Kaufmannstrasse 3</STREET>
						<ZIP xmlns="http://www.bmecat.org/bmecat/2005">8005</ZIP>
						<CITY xmlns="http://www.bmecat.org/bmecat/2005">Zürich</CITY>
						<COUNTRY xmlns="http://www.bmecat.org/bmecat/2005">Schweiz</COUNTRY>
						<COUNTRY_CODED xmlns="http://www.bmecat.org/bmecat/2005">CH</COUNTRY_CODED>
						<PHONE xmlns="http://www.bmecat.org/bmecat/2005">+41440000000</PHONE>
						<EMAIL xmlns="http://www.bmecat.org/bmecat/2005">noreply@galaxus.ch</EMAIL>
					</ADDRESS>
				</PARTY>
				<PARTY>
					<PARTY_ID type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">10709476</PARTY_ID>
					<PARTY_ROLE>marketplace</PARTY_ROLE>
					<ADDRESS>
						<NAME xmlns="http://www.bmecat.org/bmecat/2005">Digitec Galaxus AG</NAME>
						<STREET xmlns="http://www.bmecat.org/bmecat/2005">Pfingstweidstrasse 60b</STREET>
						<ZIP xmlns="http://www.bmecat.org/bmecat/2005">8005</ZIP>
						<CITY xmlns="http://www.bmecat.org/bmecat/2005">Zürich</CITY>
						<COUNTRY xmlns="http://www.bmecat.org/bmecat/2005">Schweiz</COUNTRY>
						<COUNTRY_CODED xmlns="http://www.bmecat.org/bmecat/2005">CH</COUNTRY_CODED>
						<EMAIL xmlns="http://www.bmecat.org/bmecat/2005">noreply@galaxus.ch</EMAIL>
					</ADDRESS>
				</PARTY>
			</PARTIES>
			<CUSTOMER_ORDER_REFERENCE>
				<ORDER_ID>61730208</ORDER_ID>
			</CUSTOMER_ORDER_REFERENCE>
			<ORDER_PARTIES_REFERENCE>
				<BUYER_IDREF type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">406802</BUYER_IDREF>
				<SUPPLIER_IDREF type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">407514</SUPPLIER_IDREF>
			</ORDER_PARTIES_REFERENCE>
			<CURRENCY xmlns="http://www.bmecat.org/bmecat/2005">CHF</CURRENCY>
			<HEADER_UDX>
				<UDX.DG.CUSTOMER_TYPE>private_customer</UDX.DG.CUSTOMER_TYPE>
				<UDX.DG.DELIVERY_TYPE>direct_delivery</UDX.DG.DELIVERY_TYPE>
				<UDX.DG.IS_COLLECTIVE_ORDER>false</UDX.DG.IS_COLLECTIVE_ORDER>
				<UDX.DG.PHYSICAL_DELIVERY_NOTE_REQUIRED>false</UDX.DG.PHYSICAL_DELIVERY_NOTE_REQUIRED>
			</HEADER_UDX>
		</ORDER_INFO>
	</ORDER_HEADER>
	<ORDER_ITEM_LIST>
		<ORDER_ITEM>
			<LINE_ITEM_ID>1</LINE_ITEM_ID>
			<PRODUCT_ID>
				<SUPPLIER_PID type="supplierProductKey" xmlns="http://www.bmecat.org/bmecat/2005">4135378</SUPPLIER_PID>
				<INTERNATIONAL_PID type="gtin" xmlns="http://www.bmecat.org/bmecat/2005">00889842737240</INTERNATIONAL_PID>
				<BUYER_PID type="DgProductId" xmlns="http://www.bmecat.org/bmecat/2005">15395678</BUYER_PID>
				<DESCRIPTION_SHORT xmlns="http://www.bmecat.org/bmecat/2005">Microsoft Surface Laptop 4 (13.50 ", Intel Core i7-1185G7, 16 GB, 512 GB)</DESCRIPTION_SHORT>
			</PRODUCT_ID>
			<QUANTITY>1</QUANTITY>
			<ORDER_UNIT xmlns="http://www.bmecat.org/bmecat/2005">C62</ORDER_UNIT>
			<PRODUCT_PRICE_FIX>
				<PRICE_AMOUNT xmlns="http://www.bmecat.org/bmecat/2005">1390.33</PRICE_AMOUNT>
				<TAX_DETAILS_FIX>
					<TAX_AMOUNT>107.06</TAX_AMOUNT>
				</TAX_DETAILS_FIX>
			</PRODUCT_PRICE_FIX>
			<PRICE_LINE_AMOUNT>1390.33</PRICE_LINE_AMOUNT>
			<DELIVERY_DATE type="optional">
				<DELIVERY_START_DATE>2022-07-20T00:00:00</DELIVERY_START_DATE>
				<DELIVERY_END_DATE>2022-07-20T00:00:00</DELIVERY_END_DATE>
			</DELIVERY_DATE>
		</ORDER_ITEM>
	</ORDER_ITEM_LIST>
	<ORDER_SUMMARY>
		<TOTAL_ITEM_NUM>1</TOTAL_ITEM_NUM>
		<TOTAL_AMOUNT>1390.33</TOTAL_AMOUNT>
	</ORDER_SUMMARY>
</ORDER>
```
3. company customer
```<?xml version="1.0" encoding="utf-8"?>
<ORDER xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" version="2.1" type="standard" xmlns="http://www.opentrans.org/XMLSchema/2.1">
	<ORDER_HEADER>
		<CONTROL_INFO>
			<GENERATION_DATE>2022-03-28T13:10:15</GENERATION_DATE>
		</CONTROL_INFO>
		<ORDER_INFO>
			<ORDER_ID>61730220</ORDER_ID>
			<ORDER_DATE>2022-03-28T13:10:07</ORDER_DATE>
			<LANGUAGE xmlns="http://www.bmecat.org/bmecat/2005">ger</LANGUAGE>
			<PARTIES>
				<PARTY>
					<PARTY_ID type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">406802</PARTY_ID>
					<PARTY_ID type="gln" xmlns="http://www.bmecat.org/bmecat/2005">7640151820008</PARTY_ID>
					<PARTY_ROLE>buyer</PARTY_ROLE>
					<ADDRESS>
						<NAME xmlns="http://www.bmecat.org/bmecat/2005">Digitec Galaxus AG</NAME>
						<DEPARTMENT xmlns="http://www.bmecat.org/bmecat/2005">Accounting</DEPARTMENT>
						<STREET xmlns="http://www.bmecat.org/bmecat/2005">Pfingstweidstrasse 60b</STREET>
						<ZIP xmlns="http://www.bmecat.org/bmecat/2005">8005</ZIP>
						<CITY xmlns="http://www.bmecat.org/bmecat/2005">Zürich</CITY>
						<COUNTRY xmlns="http://www.bmecat.org/bmecat/2005">Schweiz</COUNTRY>
						<COUNTRY_CODED xmlns="http://www.bmecat.org/bmecat/2005">CH</COUNTRY_CODED>
						<EMAIL xmlns="http://www.bmecat.org/bmecat/2005">noreply@galaxus.ch</EMAIL>
						<VAT_ID xmlns="http://www.bmecat.org/bmecat/2005">CHE-109.049.266</VAT_ID>
					</ADDRESS>
				</PARTY>
				<PARTY>
					<PARTY_ID type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">407514</PARTY_ID>
					<PARTY_ROLE>supplier</PARTY_ROLE>
					<ADDRESS>
						<NAME xmlns="http://www.bmecat.org/bmecat/2005">Musterfirma AG</NAME>
						<STREET xmlns="http://www.bmecat.org/bmecat/2005">Teststrasse 17</STREET>
						<ZIP xmlns="http://www.bmecat.org/bmecat/2005">8000</ZIP>
						<CITY xmlns="http://www.bmecat.org/bmecat/2005">Zürich</CITY>
						<COUNTRY xmlns="http://www.bmecat.org/bmecat/2005">Schweiz</COUNTRY>
						<COUNTRY_CODED xmlns="http://www.bmecat.org/bmecat/2005">CH</COUNTRY_CODED>
						<EMAIL xmlns="http://www.bmecat.org/bmecat/2005">noreply@galaxus.ch</EMAIL>
					</ADDRESS>
				</PARTY>
				<PARTY>
					<PARTY_ID type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">3852157</PARTY_ID>
					<PARTY_ROLE>delivery</PARTY_ROLE>
					<ADDRESS>
						<NAME xmlns="http://www.bmecat.org/bmecat/2005">Muster AG AG</NAME>
						<NAME2 xmlns="http://www.bmecat.org/bmecat/2005">Max Müller</NAME2>
						<NAME3 xmlns="http://www.bmecat.org/bmecat/2005">Gebäude B</NAME3>
						<DEPARTMENT xmlns="http://www.bmecat.org/bmecat/2005">Finanzen</DEPARTMENT>
						<STREET xmlns="http://www.bmecat.org/bmecat/2005">Kaufmannstrasse 3</STREET>
						<ZIP xmlns="http://www.bmecat.org/bmecat/2005">8005</ZIP>
						<CITY xmlns="http://www.bmecat.org/bmecat/2005">Zürich</CITY>
						<COUNTRY xmlns="http://www.bmecat.org/bmecat/2005">Schweiz</COUNTRY>
						<COUNTRY_CODED xmlns="http://www.bmecat.org/bmecat/2005">CH</COUNTRY_CODED>
						<PHONE xmlns="http://www.bmecat.org/bmecat/2005">+41440000000</PHONE>
						<EMAIL xmlns="http://www.bmecat.org/bmecat/2005">noreply@galaxus.ch</EMAIL>
					</ADDRESS>
				</PARTY>
				<PARTY>
					<PARTY_ID type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">10709476</PARTY_ID>
					<PARTY_ROLE>marketplace</PARTY_ROLE>
					<ADDRESS>
						<NAME xmlns="http://www.bmecat.org/bmecat/2005">Digitec Galaxus AG</NAME>
						<STREET xmlns="http://www.bmecat.org/bmecat/2005">Pfingstweidstrasse 60b</STREET>
						<ZIP xmlns="http://www.bmecat.org/bmecat/2005">8005</ZIP>
						<CITY xmlns="http://www.bmecat.org/bmecat/2005">Zürich</CITY>
						<COUNTRY xmlns="http://www.bmecat.org/bmecat/2005">Schweiz</COUNTRY>
						<COUNTRY_CODED xmlns="http://www.bmecat.org/bmecat/2005">CH</COUNTRY_CODED>
						<EMAIL xmlns="http://www.bmecat.org/bmecat/2005">noreply@galaxus.ch</EMAIL>
					</ADDRESS>
				</PARTY>
			</PARTIES>
			<CUSTOMER_ORDER_REFERENCE>
				<ORDER_ID>61730219</ORDER_ID>
			</CUSTOMER_ORDER_REFERENCE>
			<ORDER_PARTIES_REFERENCE>
				<BUYER_IDREF type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">406802</BUYER_IDREF>
				<SUPPLIER_IDREF type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">407514</SUPPLIER_IDREF>
			</ORDER_PARTIES_REFERENCE>
			<CURRENCY xmlns="http://www.bmecat.org/bmecat/2005">CHF</CURRENCY>
			<HEADER_UDX>
				<UDX.DG.CUSTOMER_TYPE>company</UDX.DG.CUSTOMER_TYPE>
				<UDX.DG.DELIVERY_TYPE>direct_delivery</UDX.DG.DELIVERY_TYPE>
				<UDX.DG.IS_COLLECTIVE_ORDER>false</UDX.DG.IS_COLLECTIVE_ORDER>
				<UDX.DG.PHYSICAL_DELIVERY_NOTE_REQUIRED>true</UDX.DG.PHYSICAL_DELIVERY_NOTE_REQUIRED>
			</HEADER_UDX>
		</ORDER_INFO>
	</ORDER_HEADER>
	<ORDER_ITEM_LIST>
		<ORDER_ITEM>
			<LINE_ITEM_ID>1</LINE_ITEM_ID>
			<PRODUCT_ID>
				<SUPPLIER_PID type="supplierProductKey" xmlns="http://www.bmecat.org/bmecat/2005">4135378</SUPPLIER_PID>
				<INTERNATIONAL_PID type="gtin" xmlns="http://www.bmecat.org/bmecat/2005">00889842737240</INTERNATIONAL_PID>
				<BUYER_PID type="DgProductId" xmlns="http://www.bmecat.org/bmecat/2005">15395678</BUYER_PID>
				<DESCRIPTION_SHORT xmlns="http://www.bmecat.org/bmecat/2005">Microsoft Surface Laptop 4 (13.50 ", Intel Core i7-1185G7, 16 GB, 512 GB)</DESCRIPTION_SHORT>
			</PRODUCT_ID>
			<QUANTITY>1</QUANTITY>
			<ORDER_UNIT xmlns="http://www.bmecat.org/bmecat/2005">C62</ORDER_UNIT>
			<PRODUCT_PRICE_FIX>
				<PRICE_AMOUNT xmlns="http://www.bmecat.org/bmecat/2005">1390.33</PRICE_AMOUNT>
				<TAX_DETAILS_FIX>
					<TAX_AMOUNT>107.06</TAX_AMOUNT>
				</TAX_DETAILS_FIX>
			</PRODUCT_PRICE_FIX>
			<PRICE_LINE_AMOUNT>1390.33</PRICE_LINE_AMOUNT>
			<DELIVERY_DATE type="optional">
				<DELIVERY_START_DATE>2022-07-20T00:00:00</DELIVERY_START_DATE>
				<DELIVERY_END_DATE>2022-07-20T00:00:00</DELIVERY_END_DATE>
			</DELIVERY_DATE>
		</ORDER_ITEM>
		<ORDER_ITEM>
			<LINE_ITEM_ID>2</LINE_ITEM_ID>
			<PRODUCT_ID>
				<SUPPLIER_PID type="supplierProductKey" xmlns="http://www.bmecat.org/bmecat/2005">4347777</SUPPLIER_PID>
				<INTERNATIONAL_PID type="gtin" xmlns="http://www.bmecat.org/bmecat/2005">04064575370124</INTERNATIONAL_PID>
				<BUYER_PID type="DgProductId" xmlns="http://www.bmecat.org/bmecat/2005">17222687</BUYER_PID>
				<DESCRIPTION_SHORT xmlns="http://www.bmecat.org/bmecat/2005">Apple MacBook Pro – Late 2021 (14 ", M1 Max, 64 GB, 4000 GB)</DESCRIPTION_SHORT>
			</PRODUCT_ID>
			<QUANTITY>1</QUANTITY>
			<ORDER_UNIT xmlns="http://www.bmecat.org/bmecat/2005">C62</ORDER_UNIT>
			<PRODUCT_PRICE_FIX>
				<PRICE_AMOUNT xmlns="http://www.bmecat.org/bmecat/2005">4086.14</PRICE_AMOUNT>
				<TAX_DETAILS_FIX>
					<TAX_AMOUNT>314.63</TAX_AMOUNT>
				</TAX_DETAILS_FIX>
			</PRODUCT_PRICE_FIX>
			<PRICE_LINE_AMOUNT>4086.14</PRICE_LINE_AMOUNT>
			<DELIVERY_DATE type="optional">
				<DELIVERY_START_DATE>2022-04-05T00:00:00</DELIVERY_START_DATE>
				<DELIVERY_END_DATE>2022-04-05T00:00:00</DELIVERY_END_DATE>
			</DELIVERY_DATE>
		</ORDER_ITEM>
	</ORDER_ITEM_LIST>
	<ORDER_SUMMARY>
		<TOTAL_ITEM_NUM>2</TOTAL_ITEM_NUM>
		<TOTAL_AMOUNT>5476.47</TOTAL_AMOUNT>
	</ORDER_SUMMARY>
</ORDER>
```
4. warehouse delivery (Villmergen/wholen)
```<?xml version="1.0" encoding="utf-8"?>
<ORDER xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" version="2.1" type="standard" xmlns="http://www.opentrans.org/XMLSchema/2.1">
	<ORDER_HEADER>
		<CONTROL_INFO>
			<GENERATION_DATE>2022-03-28T13:13:16</GENERATION_DATE>
		</CONTROL_INFO>
		<ORDER_INFO>
			<ORDER_ID>61730224</ORDER_ID>
			<ORDER_DATE>2022-03-28T13:12:50</ORDER_DATE>
			<LANGUAGE xmlns="http://www.bmecat.org/bmecat/2005">ger</LANGUAGE>
			<PARTIES>
				<PARTY>
					<PARTY_ID type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">406802</PARTY_ID>
					<PARTY_ID type="gln" xmlns="http://www.bmecat.org/bmecat/2005">7640151820008</PARTY_ID>
					<PARTY_ROLE>buyer</PARTY_ROLE>
					<ADDRESS>
						<NAME xmlns="http://www.bmecat.org/bmecat/2005">Digitec Galaxus AG</NAME>
						<DEPARTMENT xmlns="http://www.bmecat.org/bmecat/2005">Accounting</DEPARTMENT>
						<STREET xmlns="http://www.bmecat.org/bmecat/2005">Pfingstweidstrasse 60b</STREET>
						<ZIP xmlns="http://www.bmecat.org/bmecat/2005">8005</ZIP>
						<CITY xmlns="http://www.bmecat.org/bmecat/2005">Zürich</CITY>
						<COUNTRY xmlns="http://www.bmecat.org/bmecat/2005">Schweiz</COUNTRY>
						<COUNTRY_CODED xmlns="http://www.bmecat.org/bmecat/2005">CH</COUNTRY_CODED>
						<EMAIL xmlns="http://www.bmecat.org/bmecat/2005">noreply@galaxus.ch</EMAIL>
						<VAT_ID xmlns="http://www.bmecat.org/bmecat/2005">CHE-109.049.266</VAT_ID>
					</ADDRESS>
				</PARTY>
				<PARTY>
					<PARTY_ID type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">407514</PARTY_ID>
					<PARTY_ROLE>supplier</PARTY_ROLE>
					<ADDRESS>
						<NAME xmlns="http://www.bmecat.org/bmecat/2005">Musterfirma AG</NAME>
						<STREET xmlns="http://www.bmecat.org/bmecat/2005">Teststrasse 17</STREET>
						<ZIP xmlns="http://www.bmecat.org/bmecat/2005">8000</ZIP>
						<CITY xmlns="http://www.bmecat.org/bmecat/2005">Zürich</CITY>
						<COUNTRY xmlns="http://www.bmecat.org/bmecat/2005">Schweiz</COUNTRY>
						<COUNTRY_CODED xmlns="http://www.bmecat.org/bmecat/2005">CH</COUNTRY_CODED>
						<EMAIL xmlns="http://www.bmecat.org/bmecat/2005">noreply@galaxus.ch</EMAIL>
					</ADDRESS>
				</PARTY>
				<PARTY>
					<PARTY_ID type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">406802</PARTY_ID>
					<PARTY_ID type="gln" xmlns="http://www.bmecat.org/bmecat/2005">7640151820015</PARTY_ID>
					<PARTY_ROLE>delivery</PARTY_ROLE>
					<ADDRESS>
						<NAME xmlns="http://www.bmecat.org/bmecat/2005">Digitec Galaxus AG</NAME>
						<NAME2 xmlns="http://www.bmecat.org/bmecat/2005">Receiving Wohlen</NAME2>
						<STREET xmlns="http://www.bmecat.org/bmecat/2005">Ferroring 23</STREET>
						<ZIP xmlns="http://www.bmecat.org/bmecat/2005">5612</ZIP>
						<CITY xmlns="http://www.bmecat.org/bmecat/2005">Villmergen</CITY>
						<COUNTRY xmlns="http://www.bmecat.org/bmecat/2005">Schweiz</COUNTRY>
						<COUNTRY_CODED xmlns="http://www.bmecat.org/bmecat/2005">CH</COUNTRY_CODED>
						<EMAIL xmlns="http://www.bmecat.org/bmecat/2005">noreply@galaxus.ch</EMAIL>
					</ADDRESS>
				</PARTY>
				<PARTY>
					<PARTY_ID type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">10709476</PARTY_ID>
					<PARTY_ROLE>marketplace</PARTY_ROLE>
					<ADDRESS>
						<NAME xmlns="http://www.bmecat.org/bmecat/2005">Digitec Galaxus AG</NAME>
						<STREET xmlns="http://www.bmecat.org/bmecat/2005">Pfingstweidstrasse 60b</STREET>
						<ZIP xmlns="http://www.bmecat.org/bmecat/2005">8005</ZIP>
						<CITY xmlns="http://www.bmecat.org/bmecat/2005">Zürich</CITY>
						<COUNTRY xmlns="http://www.bmecat.org/bmecat/2005">Schweiz</COUNTRY>
						<COUNTRY_CODED xmlns="http://www.bmecat.org/bmecat/2005">CH</COUNTRY_CODED>
						<EMAIL xmlns="http://www.bmecat.org/bmecat/2005">noreply@galaxus.ch</EMAIL>
					</ADDRESS>
				</PARTY>
			</PARTIES>
			<ORDER_PARTIES_REFERENCE>
				<BUYER_IDREF type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">406802</BUYER_IDREF>
				<SUPPLIER_IDREF type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">407514</SUPPLIER_IDREF>
			</ORDER_PARTIES_REFERENCE>
			<CURRENCY xmlns="http://www.bmecat.org/bmecat/2005">CHF</CURRENCY>
			<HEADER_UDX>
				<UDX.DG.CUSTOMER_TYPE>company</UDX.DG.CUSTOMER_TYPE>
				<UDX.DG.DELIVERY_TYPE>warehouse_delivery</UDX.DG.DELIVERY_TYPE>
				<UDX.DG.IS_COLLECTIVE_ORDER>false</UDX.DG.IS_COLLECTIVE_ORDER>
				<UDX.DG.PHYSICAL_DELIVERY_NOTE_REQUIRED>true</UDX.DG.PHYSICAL_DELIVERY_NOTE_REQUIRED>
			</HEADER_UDX>
		</ORDER_INFO>
	</ORDER_HEADER>
	<ORDER_ITEM_LIST>
		<ORDER_ITEM>
			<LINE_ITEM_ID>1</LINE_ITEM_ID>
			<PRODUCT_ID>
				<SUPPLIER_PID type="supplierProductKey" xmlns="http://www.bmecat.org/bmecat/2005">4135378</SUPPLIER_PID>
				<INTERNATIONAL_PID type="gtin" xmlns="http://www.bmecat.org/bmecat/2005">00889842737240</INTERNATIONAL_PID>
				<BUYER_PID type="DgProductId" xmlns="http://www.bmecat.org/bmecat/2005">15395678</BUYER_PID>
				<DESCRIPTION_SHORT xmlns="http://www.bmecat.org/bmecat/2005">Microsoft Surface Laptop 4 (13.50 ", Intel Core i7-1185G7, 16 GB, 512 GB)</DESCRIPTION_SHORT>
			</PRODUCT_ID>
			<QUANTITY>1</QUANTITY>
			<ORDER_UNIT xmlns="http://www.bmecat.org/bmecat/2005">C62</ORDER_UNIT>
			<PRODUCT_PRICE_FIX>
				<PRICE_AMOUNT xmlns="http://www.bmecat.org/bmecat/2005">1390.33</PRICE_AMOUNT>
				<TAX_DETAILS_FIX>
					<TAX_AMOUNT>107.06</TAX_AMOUNT>
				</TAX_DETAILS_FIX>
			</PRODUCT_PRICE_FIX>
			<PRICE_LINE_AMOUNT>1390.33</PRICE_LINE_AMOUNT>
			<DELIVERY_DATE type="optional">
				<DELIVERY_START_DATE>2022-07-21T00:00:00</DELIVERY_START_DATE>
				<DELIVERY_END_DATE>2022-07-21T00:00:00</DELIVERY_END_DATE>
			</DELIVERY_DATE>
		</ORDER_ITEM>
		<ORDER_ITEM>
			<LINE_ITEM_ID>2</LINE_ITEM_ID>
			<PRODUCT_ID>
				<SUPPLIER_PID type="supplierProductKey" xmlns="http://www.bmecat.org/bmecat/2005">4132674</SUPPLIER_PID>
				<INTERNATIONAL_PID type="gtin" xmlns="http://www.bmecat.org/bmecat/2005">08806092114470</INTERNATIONAL_PID>
				<BUYER_PID type="DgProductId" xmlns="http://www.bmecat.org/bmecat/2005">15459785</BUYER_PID>
				<DESCRIPTION_SHORT xmlns="http://www.bmecat.org/bmecat/2005">Samsung Galaxy A32 EE Enterprise Edition (128 GB, Black, 6.40 ", Dual SIM, 64 Mpx, 4G)</DESCRIPTION_SHORT>
			</PRODUCT_ID>
			<QUANTITY>1</QUANTITY>
			<ORDER_UNIT xmlns="http://www.bmecat.org/bmecat/2005">C62</ORDER_UNIT>
			<PRODUCT_PRICE_FIX>
				<PRICE_AMOUNT xmlns="http://www.bmecat.org/bmecat/2005">235.19</PRICE_AMOUNT>
				<TAX_DETAILS_FIX>
					<TAX_AMOUNT>18.11</TAX_AMOUNT>
				</TAX_DETAILS_FIX>
			</PRODUCT_PRICE_FIX>
			<PRICE_LINE_AMOUNT>235.19</PRICE_LINE_AMOUNT>
			<DELIVERY_DATE type="optional">
				<DELIVERY_START_DATE>2022-04-01T00:00:00</DELIVERY_START_DATE>
				<DELIVERY_END_DATE>2022-04-01T00:00:00</DELIVERY_END_DATE>
			</DELIVERY_DATE>
		</ORDER_ITEM>
	</ORDER_ITEM_LIST>
	<ORDER_SUMMARY>
		<TOTAL_ITEM_NUM>2</TOTAL_ITEM_NUM>
		<TOTAL_AMOUNT>1625.52</TOTAL_AMOUNT>
	</ORDER_SUMMARY>
</ORDER>
```
5. warehouse delivery (Dintikon)
```<?xml version="1.0" encoding="utf-8"?>
<ORDER xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" version="2.1" type="standard" xmlns="http://www.opentrans.org/XMLSchema/2.1">
	<ORDER_HEADER>
		<CONTROL_INFO>
			<GENERATION_DATE>2022-03-28T13:13:03</GENERATION_DATE>
		</CONTROL_INFO>
		<ORDER_INFO>
			<ORDER_ID>61730221</ORDER_ID>
			<ORDER_DATE>2022-03-28T13:12:35</ORDER_DATE>
			<LANGUAGE xmlns="http://www.bmecat.org/bmecat/2005">ger</LANGUAGE>
			<PARTIES>
				<PARTY>
					<PARTY_ID type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">406802</PARTY_ID>
					<PARTY_ID type="gln" xmlns="http://www.bmecat.org/bmecat/2005">7640151820008</PARTY_ID>
					<PARTY_ROLE>buyer</PARTY_ROLE>
					<ADDRESS>
						<NAME xmlns="http://www.bmecat.org/bmecat/2005">Digitec Galaxus AG</NAME>
						<DEPARTMENT xmlns="http://www.bmecat.org/bmecat/2005">Accounting</DEPARTMENT>
						<STREET xmlns="http://www.bmecat.org/bmecat/2005">Pfingstweidstrasse 60b</STREET>
						<ZIP xmlns="http://www.bmecat.org/bmecat/2005">8005</ZIP>
						<CITY xmlns="http://www.bmecat.org/bmecat/2005">Zürich</CITY>
						<COUNTRY xmlns="http://www.bmecat.org/bmecat/2005">Schweiz</COUNTRY>
						<COUNTRY_CODED xmlns="http://www.bmecat.org/bmecat/2005">CH</COUNTRY_CODED>
						<EMAIL xmlns="http://www.bmecat.org/bmecat/2005">noreply@galaxus.ch</EMAIL>
						<VAT_ID xmlns="http://www.bmecat.org/bmecat/2005">CHE-109.049.266</VAT_ID>
					</ADDRESS>
				</PARTY>
				<PARTY>
					<PARTY_ID type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">407514</PARTY_ID>
					<PARTY_ROLE>supplier</PARTY_ROLE>
					<ADDRESS>
						<NAME xmlns="http://www.bmecat.org/bmecat/2005">Musterfirma AG</NAME>
						<STREET xmlns="http://www.bmecat.org/bmecat/2005">Teststrasse 17</STREET>
						<ZIP xmlns="http://www.bmecat.org/bmecat/2005">8000</ZIP>
						<CITY xmlns="http://www.bmecat.org/bmecat/2005">Zürich</CITY>
						<COUNTRY xmlns="http://www.bmecat.org/bmecat/2005">Schweiz</COUNTRY>
						<COUNTRY_CODED xmlns="http://www.bmecat.org/bmecat/2005">CH</COUNTRY_CODED>
						<EMAIL xmlns="http://www.bmecat.org/bmecat/2005">noreply@galaxus.ch</EMAIL>
					</ADDRESS>
				</PARTY>
				<PARTY>
					<PARTY_ID type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">406802</PARTY_ID>
					<PARTY_ID type="gln" xmlns="http://www.bmecat.org/bmecat/2005">7640151820022</PARTY_ID>
					<PARTY_ROLE>delivery</PARTY_ROLE>
					<ADDRESS>
						<NAME xmlns="http://www.bmecat.org/bmecat/2005">Digitec Galaxus AG</NAME>
						<STREET xmlns="http://www.bmecat.org/bmecat/2005">Parallelstrasse 10</STREET>
						<ZIP xmlns="http://www.bmecat.org/bmecat/2005">5606</ZIP>
						<CITY xmlns="http://www.bmecat.org/bmecat/2005">Dintikon</CITY>
						<COUNTRY xmlns="http://www.bmecat.org/bmecat/2005">Schweiz</COUNTRY>
						<COUNTRY_CODED xmlns="http://www.bmecat.org/bmecat/2005">CH</COUNTRY_CODED>
						<EMAIL xmlns="http://www.bmecat.org/bmecat/2005">noreply@galaxus.ch</EMAIL>
					</ADDRESS>
				</PARTY>
				<PARTY>
					<PARTY_ID type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">10709476</PARTY_ID>
					<PARTY_ROLE>marketplace</PARTY_ROLE>
					<ADDRESS>
						<NAME xmlns="http://www.bmecat.org/bmecat/2005">Digitec Galaxus AG</NAME>
						<STREET xmlns="http://www.bmecat.org/bmecat/2005">Pfingstweidstrasse 60b</STREET>
						<ZIP xmlns="http://www.bmecat.org/bmecat/2005">8005</ZIP>
						<CITY xmlns="http://www.bmecat.org/bmecat/2005">Zürich</CITY>
						<COUNTRY xmlns="http://www.bmecat.org/bmecat/2005">Schweiz</COUNTRY>
						<COUNTRY_CODED xmlns="http://www.bmecat.org/bmecat/2005">CH</COUNTRY_CODED>
						<EMAIL xmlns="http://www.bmecat.org/bmecat/2005">noreply@galaxus.ch</EMAIL>
					</ADDRESS>
				</PARTY>
			</PARTIES>
			<ORDER_PARTIES_REFERENCE>
				<BUYER_IDREF type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">406802</BUYER_IDREF>
				<SUPPLIER_IDREF type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">407514</SUPPLIER_IDREF>
			</ORDER_PARTIES_REFERENCE>
			<CURRENCY xmlns="http://www.bmecat.org/bmecat/2005">CHF</CURRENCY>
			<HEADER_UDX>
				<UDX.DG.CUSTOMER_TYPE>company</UDX.DG.CUSTOMER_TYPE>
				<UDX.DG.DELIVERY_TYPE>warehouse_delivery</UDX.DG.DELIVERY_TYPE>
				<UDX.DG.IS_COLLECTIVE_ORDER>false</UDX.DG.IS_COLLECTIVE_ORDER>
				<UDX.DG.PHYSICAL_DELIVERY_NOTE_REQUIRED>true</UDX.DG.PHYSICAL_DELIVERY_NOTE_REQUIRED>
			</HEADER_UDX>
		</ORDER_INFO>
	</ORDER_HEADER>
	<ORDER_ITEM_LIST>
		<ORDER_ITEM>
			<LINE_ITEM_ID>1</LINE_ITEM_ID>
			<PRODUCT_ID>
				<SUPPLIER_PID type="supplierProductKey" xmlns="http://www.bmecat.org/bmecat/2005">4347777</SUPPLIER_PID>
				<INTERNATIONAL_PID type="gtin" xmlns="http://www.bmecat.org/bmecat/2005">04064575370124</INTERNATIONAL_PID>
				<BUYER_PID type="DgProductId" xmlns="http://www.bmecat.org/bmecat/2005">17222687</BUYER_PID>
				<DESCRIPTION_SHORT xmlns="http://www.bmecat.org/bmecat/2005">Apple MacBook Pro – Late 2021 (14 ", M1 Max, 64 GB, 4000 GB)</DESCRIPTION_SHORT>
			</PRODUCT_ID>
			<QUANTITY>1</QUANTITY>
			<ORDER_UNIT xmlns="http://www.bmecat.org/bmecat/2005">C62</ORDER_UNIT>
			<PRODUCT_PRICE_FIX>
				<PRICE_AMOUNT xmlns="http://www.bmecat.org/bmecat/2005">4086.14</PRICE_AMOUNT>
				<TAX_DETAILS_FIX>
					<TAX_AMOUNT>314.63</TAX_AMOUNT>
				</TAX_DETAILS_FIX>
			</PRODUCT_PRICE_FIX>
			<PRICE_LINE_AMOUNT>4086.14</PRICE_LINE_AMOUNT>
			<DELIVERY_DATE type="optional">
				<DELIVERY_START_DATE>2022-04-06T00:00:00</DELIVERY_START_DATE>
				<DELIVERY_END_DATE>2022-04-06T00:00:00</DELIVERY_END_DATE>
			</DELIVERY_DATE>
		</ORDER_ITEM>
	</ORDER_ITEM_LIST>
	<ORDER_SUMMARY>
		<TOTAL_ITEM_NUM>1</TOTAL_ITEM_NUM>
		<TOTAL_AMOUNT>4086.14</TOTAL_AMOUNT>
	</ORDER_SUMMARY>
</ORDER>
```
6. warehouse delivery (Roggwil)
```<?xml version="1.0" encoding="utf-8"?>
<ORDER xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" version="2.1" type="standard" xmlns="http://www.opentrans.org/XMLSchema/2.1">
	<ORDER_HEADER>
		<CONTROL_INFO>
			<GENERATION_DATE>2022-03-28T13:13:07</GENERATION_DATE>
		</CONTROL_INFO>
		<ORDER_INFO>
			<ORDER_ID>61730222</ORDER_ID>
			<ORDER_DATE>2022-03-28T13:12:36</ORDER_DATE>
			<LANGUAGE xmlns="http://www.bmecat.org/bmecat/2005">ger</LANGUAGE>
			<PARTIES>
				<PARTY>
					<PARTY_ID type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">406802</PARTY_ID>
					<PARTY_ID type="gln" xmlns="http://www.bmecat.org/bmecat/2005">7640151820008</PARTY_ID>
					<PARTY_ROLE>buyer</PARTY_ROLE>
					<ADDRESS>
						<NAME xmlns="http://www.bmecat.org/bmecat/2005">Digitec Galaxus AG</NAME>
						<DEPARTMENT xmlns="http://www.bmecat.org/bmecat/2005">Accounting</DEPARTMENT>
						<STREET xmlns="http://www.bmecat.org/bmecat/2005">Pfingstweidstrasse 60b</STREET>
						<ZIP xmlns="http://www.bmecat.org/bmecat/2005">8005</ZIP>
						<CITY xmlns="http://www.bmecat.org/bmecat/2005">Zürich</CITY>
						<COUNTRY xmlns="http://www.bmecat.org/bmecat/2005">Schweiz</COUNTRY>
						<COUNTRY_CODED xmlns="http://www.bmecat.org/bmecat/2005">CH</COUNTRY_CODED>
						<EMAIL xmlns="http://www.bmecat.org/bmecat/2005">noreply@galaxus.ch</EMAIL>
						<VAT_ID xmlns="http://www.bmecat.org/bmecat/2005">CHE-109.049.266</VAT_ID>
					</ADDRESS>
				</PARTY>
				<PARTY>
					<PARTY_ID type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">407514</PARTY_ID>
					<PARTY_ROLE>supplier</PARTY_ROLE>
					<ADDRESS>
						<NAME xmlns="http://www.bmecat.org/bmecat/2005">Musterfirma AG</NAME>
						<STREET xmlns="http://www.bmecat.org/bmecat/2005">Teststrasse 17</STREET>
						<ZIP xmlns="http://www.bmecat.org/bmecat/2005">8000</ZIP>
						<CITY xmlns="http://www.bmecat.org/bmecat/2005">Zürich</CITY>
						<COUNTRY xmlns="http://www.bmecat.org/bmecat/2005">Schweiz</COUNTRY>
						<COUNTRY_CODED xmlns="http://www.bmecat.org/bmecat/2005">CH</COUNTRY_CODED>
						<EMAIL xmlns="http://www.bmecat.org/bmecat/2005">noreply@galaxus.ch</EMAIL>
					</ADDRESS>
				</PARTY>
				<PARTY>
					<PARTY_ID type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">406802</PARTY_ID>
					<PARTY_ID type="gln" xmlns="http://www.bmecat.org/bmecat/2005">7640151820039</PARTY_ID>
					<PARTY_ROLE>delivery</PARTY_ROLE>
					<ADDRESS>
						<NAME xmlns="http://www.bmecat.org/bmecat/2005">Digitec Galaxus AG</NAME>
						<NAME3 xmlns="http://www.bmecat.org/bmecat/2005">Rampe 50-55</NAME3>
						<STREET xmlns="http://www.bmecat.org/bmecat/2005">Lagerhausstrasse 12</STREET>
						<ZIP xmlns="http://www.bmecat.org/bmecat/2005">4914</ZIP>
						<CITY xmlns="http://www.bmecat.org/bmecat/2005">Roggwil</CITY>
						<COUNTRY xmlns="http://www.bmecat.org/bmecat/2005">Schweiz</COUNTRY>
						<COUNTRY_CODED xmlns="http://www.bmecat.org/bmecat/2005">CH</COUNTRY_CODED>
						<EMAIL xmlns="http://www.bmecat.org/bmecat/2005">noreply@galaxus.ch</EMAIL>
					</ADDRESS>
				</PARTY>
				<PARTY>
					<PARTY_ID type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">10709476</PARTY_ID>
					<PARTY_ROLE>marketplace</PARTY_ROLE>
					<ADDRESS>
						<NAME xmlns="http://www.bmecat.org/bmecat/2005">Digitec Galaxus AG</NAME>
						<STREET xmlns="http://www.bmecat.org/bmecat/2005">Pfingstweidstrasse 60b</STREET>
						<ZIP xmlns="http://www.bmecat.org/bmecat/2005">8005</ZIP>
						<CITY xmlns="http://www.bmecat.org/bmecat/2005">Zürich</CITY>
						<COUNTRY xmlns="http://www.bmecat.org/bmecat/2005">Schweiz</COUNTRY>
						<COUNTRY_CODED xmlns="http://www.bmecat.org/bmecat/2005">CH</COUNTRY_CODED>
						<EMAIL xmlns="http://www.bmecat.org/bmecat/2005">noreply@galaxus.ch</EMAIL>
					</ADDRESS>
				</PARTY>
			</PARTIES>
			<ORDER_PARTIES_REFERENCE>
				<BUYER_IDREF type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">406802</BUYER_IDREF>
				<SUPPLIER_IDREF type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">407514</SUPPLIER_IDREF>
			</ORDER_PARTIES_REFERENCE>
			<CURRENCY xmlns="http://www.bmecat.org/bmecat/2005">CHF</CURRENCY>
			<HEADER_UDX>
				<UDX.DG.CUSTOMER_TYPE>company</UDX.DG.CUSTOMER_TYPE>
				<UDX.DG.DELIVERY_TYPE>warehouse_delivery</UDX.DG.DELIVERY_TYPE>
				<UDX.DG.IS_COLLECTIVE_ORDER>false</UDX.DG.IS_COLLECTIVE_ORDER>
				<UDX.DG.PHYSICAL_DELIVERY_NOTE_REQUIRED>true</UDX.DG.PHYSICAL_DELIVERY_NOTE_REQUIRED>
			</HEADER_UDX>
		</ORDER_INFO>
	</ORDER_HEADER>
	<ORDER_ITEM_LIST>
		<ORDER_ITEM>
			<LINE_ITEM_ID>1</LINE_ITEM_ID>
			<PRODUCT_ID>
				<SUPPLIER_PID type="supplierProductKey" xmlns="http://www.bmecat.org/bmecat/2005">3956859</SUPPLIER_PID>
				<INTERNATIONAL_PID type="gtin" xmlns="http://www.bmecat.org/bmecat/2005">04064575327616</INTERNATIONAL_PID>
				<BUYER_PID type="DgProductId" xmlns="http://www.bmecat.org/bmecat/2005">13949146</BUYER_PID>
				<DESCRIPTION_SHORT xmlns="http://www.bmecat.org/bmecat/2005">Apple iMac (27 ", Intel Core i9, 16 GB, 2000 GB, SSD)</DESCRIPTION_SHORT>
			</PRODUCT_ID>
			<QUANTITY>1</QUANTITY>
			<ORDER_UNIT xmlns="http://www.bmecat.org/bmecat/2005">C62</ORDER_UNIT>
			<PRODUCT_PRICE_FIX>
				<PRICE_AMOUNT xmlns="http://www.bmecat.org/bmecat/2005">3097.99</PRICE_AMOUNT>
				<TAX_DETAILS_FIX>
					<TAX_AMOUNT>238.55</TAX_AMOUNT>
				</TAX_DETAILS_FIX>
			</PRODUCT_PRICE_FIX>
			<PRICE_LINE_AMOUNT>3097.99</PRICE_LINE_AMOUNT>
			<DELIVERY_DATE type="optional">
				<DELIVERY_START_DATE>2022-03-31T00:00:00</DELIVERY_START_DATE>
				<DELIVERY_END_DATE>2022-03-31T00:00:00</DELIVERY_END_DATE>
			</DELIVERY_DATE>
		</ORDER_ITEM>
		<ORDER_ITEM>
			<LINE_ITEM_ID>2</LINE_ITEM_ID>
			<PRODUCT_ID>
				<SUPPLIER_PID type="supplierProductKey" xmlns="http://www.bmecat.org/bmecat/2005">4143052</SUPPLIER_PID>
				<INTERNATIONAL_PID type="gtin" xmlns="http://www.bmecat.org/bmecat/2005">00195908799108</INTERNATIONAL_PID>
				<BUYER_PID type="DgProductId" xmlns="http://www.bmecat.org/bmecat/2005">15678630</BUYER_PID>
				<DESCRIPTION_SHORT xmlns="http://www.bmecat.org/bmecat/2005">HP EliteBook 845 G8 (14 ", AMD Ryzen 3 5400U, 16 GB, 256 GB)</DESCRIPTION_SHORT>
			</PRODUCT_ID>
			<QUANTITY>1</QUANTITY>
			<ORDER_UNIT xmlns="http://www.bmecat.org/bmecat/2005">C62</ORDER_UNIT>
			<PRODUCT_PRICE_FIX>
				<PRICE_AMOUNT xmlns="http://www.bmecat.org/bmecat/2005">1066.69</PRICE_AMOUNT>
				<TAX_DETAILS_FIX>
					<TAX_AMOUNT>82.14</TAX_AMOUNT>
				</TAX_DETAILS_FIX>
			</PRODUCT_PRICE_FIX>
			<PRICE_LINE_AMOUNT>1066.69</PRICE_LINE_AMOUNT>
		</ORDER_ITEM>
	</ORDER_ITEM_LIST>
	<ORDER_SUMMARY>
		<TOTAL_ITEM_NUM>2</TOTAL_ITEM_NUM>
		<TOTAL_AMOUNT>4164.68</TOTAL_AMOUNT>
	</ORDER_SUMMARY>
</ORDER>
```
7. warehouse delivery (Oftringen)
```<?xml version="1.0" encoding="utf-8"?>
<ORDER xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" version="2.1" type="standard" xmlns="http://www.opentrans.org/XMLSchema/2.1">
	<ORDER_HEADER>
		<CONTROL_INFO>
			<GENERATION_DATE>2022-03-28T13:13:15</GENERATION_DATE>
		</CONTROL_INFO>
		<ORDER_INFO>
			<ORDER_ID>61730223</ORDER_ID>
			<ORDER_DATE>2022-03-28T13:12:45</ORDER_DATE>
			<LANGUAGE xmlns="http://www.bmecat.org/bmecat/2005">ger</LANGUAGE>
			<PARTIES>
				<PARTY>
					<PARTY_ID type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">406802</PARTY_ID>
					<PARTY_ID type="gln" xmlns="http://www.bmecat.org/bmecat/2005">7640151820008</PARTY_ID>
					<PARTY_ROLE>buyer</PARTY_ROLE>
					<ADDRESS>
						<NAME xmlns="http://www.bmecat.org/bmecat/2005">Digitec Galaxus AG</NAME>
						<DEPARTMENT xmlns="http://www.bmecat.org/bmecat/2005">Accounting</DEPARTMENT>
						<STREET xmlns="http://www.bmecat.org/bmecat/2005">Pfingstweidstrasse 60b</STREET>
						<ZIP xmlns="http://www.bmecat.org/bmecat/2005">8005</ZIP>
						<CITY xmlns="http://www.bmecat.org/bmecat/2005">Zürich</CITY>
						<COUNTRY xmlns="http://www.bmecat.org/bmecat/2005">Schweiz</COUNTRY>
						<COUNTRY_CODED xmlns="http://www.bmecat.org/bmecat/2005">CH</COUNTRY_CODED>
						<EMAIL xmlns="http://www.bmecat.org/bmecat/2005">noreply@galaxus.ch</EMAIL>
						<VAT_ID xmlns="http://www.bmecat.org/bmecat/2005">CHE-109.049.266</VAT_ID>
					</ADDRESS>
				</PARTY>
				<PARTY>
					<PARTY_ID type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">407514</PARTY_ID>
					<PARTY_ROLE>supplier</PARTY_ROLE>
					<ADDRESS>
						<NAME xmlns="http://www.bmecat.org/bmecat/2005">Musterfirma AG</NAME>
						<STREET xmlns="http://www.bmecat.org/bmecat/2005">Teststrasse 17</STREET>
						<ZIP xmlns="http://www.bmecat.org/bmecat/2005">8000</ZIP>
						<CITY xmlns="http://www.bmecat.org/bmecat/2005">Zürich</CITY>
						<COUNTRY xmlns="http://www.bmecat.org/bmecat/2005">Schweiz</COUNTRY>
						<COUNTRY_CODED xmlns="http://www.bmecat.org/bmecat/2005">CH</COUNTRY_CODED>
						<EMAIL xmlns="http://www.bmecat.org/bmecat/2005">noreply@galaxus.ch</EMAIL>
					</ADDRESS>
				</PARTY>
				<PARTY>
					<PARTY_ID type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">406802</PARTY_ID>
					<PARTY_ID type="gln" xmlns="http://www.bmecat.org/bmecat/2005">7640151820053</PARTY_ID>
					<PARTY_ROLE>delivery</PARTY_ROLE>
					<ADDRESS>
						<NAME xmlns="http://www.bmecat.org/bmecat/2005">Digitec Galaxus AG</NAME>
						<NAME2 xmlns="http://www.bmecat.org/bmecat/2005">c/o FIEGE</NAME2>
						<STREET xmlns="http://www.bmecat.org/bmecat/2005">Industriestr. 11</STREET>
						<ZIP xmlns="http://www.bmecat.org/bmecat/2005">4665</ZIP>
						<CITY xmlns="http://www.bmecat.org/bmecat/2005">Oftringen</CITY>
						<COUNTRY xmlns="http://www.bmecat.org/bmecat/2005">Schweiz</COUNTRY>
						<COUNTRY_CODED xmlns="http://www.bmecat.org/bmecat/2005">CH</COUNTRY_CODED>
						<EMAIL xmlns="http://www.bmecat.org/bmecat/2005">noreply@galaxus.ch</EMAIL>
					</ADDRESS>
				</PARTY>
				<PARTY>
					<PARTY_ID type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">10709476</PARTY_ID>
					<PARTY_ROLE>marketplace</PARTY_ROLE>
					<ADDRESS>
						<NAME xmlns="http://www.bmecat.org/bmecat/2005">Digitec Galaxus AG</NAME>
						<STREET xmlns="http://www.bmecat.org/bmecat/2005">Pfingstweidstrasse 60b</STREET>
						<ZIP xmlns="http://www.bmecat.org/bmecat/2005">8005</ZIP>
						<CITY xmlns="http://www.bmecat.org/bmecat/2005">Zürich</CITY>
						<COUNTRY xmlns="http://www.bmecat.org/bmecat/2005">Schweiz</COUNTRY>
						<COUNTRY_CODED xmlns="http://www.bmecat.org/bmecat/2005">CH</COUNTRY_CODED>
						<EMAIL xmlns="http://www.bmecat.org/bmecat/2005">noreply@galaxus.ch</EMAIL>
					</ADDRESS>
				</PARTY>
			</PARTIES>
			<ORDER_PARTIES_REFERENCE>
				<BUYER_IDREF type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">406802</BUYER_IDREF>
				<SUPPLIER_IDREF type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">407514</SUPPLIER_IDREF>
			</ORDER_PARTIES_REFERENCE>
			<CURRENCY xmlns="http://www.bmecat.org/bmecat/2005">CHF</CURRENCY>
			<HEADER_UDX>
				<UDX.DG.CUSTOMER_TYPE>company</UDX.DG.CUSTOMER_TYPE>
				<UDX.DG.DELIVERY_TYPE>warehouse_delivery</UDX.DG.DELIVERY_TYPE>
				<UDX.DG.IS_COLLECTIVE_ORDER>false</UDX.DG.IS_COLLECTIVE_ORDER>
				<UDX.DG.PHYSICAL_DELIVERY_NOTE_REQUIRED>true</UDX.DG.PHYSICAL_DELIVERY_NOTE_REQUIRED>
			</HEADER_UDX>
		</ORDER_INFO>
	</ORDER_HEADER>
	<ORDER_ITEM_LIST>
		<ORDER_ITEM>
			<LINE_ITEM_ID>1</LINE_ITEM_ID>
			<PRODUCT_ID>
				<SUPPLIER_PID type="supplierProductKey" xmlns="http://www.bmecat.org/bmecat/2005">2616514</SUPPLIER_PID>
				<INTERNATIONAL_PID type="gtin" xmlns="http://www.bmecat.org/bmecat/2005">05099206065086</INTERNATIONAL_PID>
				<BUYER_PID type="DgProductId" xmlns="http://www.bmecat.org/bmecat/2005">5891309</BUYER_PID>
				<DESCRIPTION_SHORT xmlns="http://www.bmecat.org/bmecat/2005">Logitech M720 Triathlon (Kabellos)</DESCRIPTION_SHORT>
			</PRODUCT_ID>
			<QUANTITY>1</QUANTITY>
			<ORDER_UNIT xmlns="http://www.bmecat.org/bmecat/2005">C62</ORDER_UNIT>
			<PRODUCT_PRICE_FIX>
				<PRICE_AMOUNT xmlns="http://www.bmecat.org/bmecat/2005">43.79</PRICE_AMOUNT>
				<TAX_DETAILS_FIX>
					<TAX_AMOUNT>3.37</TAX_AMOUNT>
				</TAX_DETAILS_FIX>
			</PRODUCT_PRICE_FIX>
			<PRICE_LINE_AMOUNT>43.79</PRICE_LINE_AMOUNT>
			<DELIVERY_DATE type="optional">
				<DELIVERY_START_DATE>2022-03-31T00:00:00</DELIVERY_START_DATE>
				<DELIVERY_END_DATE>2022-03-31T00:00:00</DELIVERY_END_DATE>
			</DELIVERY_DATE>
		</ORDER_ITEM>
		<ORDER_ITEM>
			<LINE_ITEM_ID>2</LINE_ITEM_ID>
			<PRODUCT_ID>
				<SUPPLIER_PID type="supplierProductKey" xmlns="http://www.bmecat.org/bmecat/2005">3581202</SUPPLIER_PID>
				<INTERNATIONAL_PID type="gtin" xmlns="http://www.bmecat.org/bmecat/2005">00193905265206</INTERNATIONAL_PID>
				<BUYER_PID type="DgProductId" xmlns="http://www.bmecat.org/bmecat/2005">12512043</BUYER_PID>
				<DESCRIPTION_SHORT xmlns="http://www.bmecat.org/bmecat/2005">HP 207X (M)</DESCRIPTION_SHORT>
			</PRODUCT_ID>
			<QUANTITY>1</QUANTITY>
			<ORDER_UNIT xmlns="http://www.bmecat.org/bmecat/2005">C62</ORDER_UNIT>
			<PRODUCT_PRICE_FIX>
				<PRICE_AMOUNT xmlns="http://www.bmecat.org/bmecat/2005">75.23</PRICE_AMOUNT>
				<TAX_DETAILS_FIX>
					<TAX_AMOUNT>5.79</TAX_AMOUNT>
				</TAX_DETAILS_FIX>
			</PRODUCT_PRICE_FIX>
			<PRICE_LINE_AMOUNT>75.23</PRICE_LINE_AMOUNT>
			<DELIVERY_DATE type="optional">
				<DELIVERY_START_DATE>2022-03-31T00:00:00</DELIVERY_START_DATE>
				<DELIVERY_END_DATE>2022-03-31T00:00:00</DELIVERY_END_DATE>
			</DELIVERY_DATE>
		</ORDER_ITEM>
	</ORDER_ITEM_LIST>
	<ORDER_SUMMARY>
		<TOTAL_ITEM_NUM>2</TOTAL_ITEM_NUM>
		<TOTAL_AMOUNT>119.02</TOTAL_AMOUNT>
	</ORDER_SUMMARY>
</ORDER>
```
8. EU-Hub dealer private customer
```<?xml version="1.0" encoding="utf-8"?>
<ORDER xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" version="2.1" type="standard" xmlns="http://www.opentrans.org/XMLSchema/2.1">
	<ORDER_HEADER>
		<CONTROL_INFO>
			<GENERATION_DATE>2022-05-24T12:55:21</GENERATION_DATE>
		</CONTROL_INFO>
		<ORDER_INFO>
			<ORDER_ID>69999999</ORDER_ID>
			<ORDER_DATE>2022-05-24T12:55:22</ORDER_DATE>
			<LANGUAGE xmlns="http://www.bmecat.org/bmecat/2005">ger</LANGUAGE>
			<PARTIES>
				<PARTY>
					<PARTY_ID type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">9999999</PARTY_ID>
					<PARTY_ROLE>buyer</PARTY_ROLE>
					<ADDRESS>
						<CONTACT_DETAILS>
							<TITLE xmlns="http://www.bmecat.org/bmecat/2005">Herr</TITLE>
							<FIRST_NAME xmlns="http://www.bmecat.org/bmecat/2005">Hans</FIRST_NAME>
							<CONTACT_NAME xmlns="http://www.bmecat.org/bmecat/2005">Müller</CONTACT_NAME>
						</CONTACT_DETAILS>
						<STREET xmlns="http://www.bmecat.org/bmecat/2005">Teststrasse 50</STREET>
						<ZIP xmlns="http://www.bmecat.org/bmecat/2005">8000</ZIP>
						<CITY xmlns="http://www.bmecat.org/bmecat/2005">Zürich</CITY>
						<COUNTRY xmlns="http://www.bmecat.org/bmecat/2005">Schweiz</COUNTRY>
						<COUNTRY_CODED xmlns="http://www.bmecat.org/bmecat/2005">CH</COUNTRY_CODED>
						<EMAIL xmlns="http://www.bmecat.org/bmecat/2005">noreply@galaxus.ch</EMAIL>
					</ADDRESS>
				</PARTY>
				<PARTY>
					<PARTY_ID type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">9999999</PARTY_ID>
					<PARTY_ROLE>supplier</PARTY_ROLE>
					<ADDRESS>
						<NAME xmlns="http://www.bmecat.org/bmecat/2005">Musterfirma AG</NAME>
						<STREET xmlns="http://www.bmecat.org/bmecat/2005">Teststrasse 112</STREET>
						<ZIP xmlns="http://www.bmecat.org/bmecat/2005">10115</ZIP>
						<CITY xmlns="http://www.bmecat.org/bmecat/2005">Berlin</CITY>
						<COUNTRY xmlns="http://www.bmecat.org/bmecat/2005">Deutschland</COUNTRY>
						<COUNTRY_CODED xmlns="http://www.bmecat.org/bmecat/2005">DE</COUNTRY_CODED>
						<EMAIL xmlns="http://www.bmecat.org/bmecat/2005">noreply@galaxus.ch</EMAIL>
					</ADDRESS>
				</PARTY>
				<PARTY>
					<PARTY_ID type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">999999</PARTY_ID>
					<PARTY_ROLE>delivery</PARTY_ROLE>
					<ADDRESS>
						<NAME xmlns="http://www.bmecat.org/bmecat/2005">Galaxus Deutschland GmbH</NAME>
						<NAME2 xmlns="http://www.bmecat.org/bmecat/2005">c/o Acito Logistics GmbH</NAME2>
						<STREET xmlns="http://www.bmecat.org/bmecat/2005">Im Schlöttle 6</STREET>
						<ZIP xmlns="http://www.bmecat.org/bmecat/2005">79588</ZIP>
						<CITY xmlns="http://www.bmecat.org/bmecat/2005">Efringen - Kirchen</CITY>
						<COUNTRY xmlns="http://www.bmecat.org/bmecat/2005">Deutschland</COUNTRY>
						<COUNTRY_CODED xmlns="http://www.bmecat.org/bmecat/2005">DE</COUNTRY_CODED>
						<PHONE xmlns="http://www.bmecat.org/bmecat/2005">+41790000000</PHONE>
						<EMAIL xmlns="http://www.bmecat.org/bmecat/2005">noreply@galaxus.ch</EMAIL>
					</ADDRESS>
				</PARTY>
				<PARTY>
					<PARTY_ID type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">CH</PARTY_ID>
					<PARTY_ROLE>marketplace</PARTY_ROLE>
					<ADDRESS>
						<NAME xmlns="http://www.bmecat.org/bmecat/2005">Digitec Galaxus AG</NAME>
						<STREET xmlns="http://www.bmecat.org/bmecat/2005">Pfingstweidstrasse 60b</STREET>
						<ZIP xmlns="http://www.bmecat.org/bmecat/2005">8005</ZIP>
						<CITY xmlns="http://www.bmecat.org/bmecat/2005">Zürich</CITY>
						<COUNTRY xmlns="http://www.bmecat.org/bmecat/2005">Schweiz</COUNTRY>
						<COUNTRY_CODED xmlns="http://www.bmecat.org/bmecat/2005">CH</COUNTRY_CODED>
						<EMAIL xmlns="http://www.bmecat.org/bmecat/2005">noreply@galaxus.ch</EMAIL>
					</ADDRESS>
				</PARTY>
			</PARTIES>
			<CUSTOMER_ORDER_REFERENCE>
				<ORDER_ID>69999999</ORDER_ID>
			</CUSTOMER_ORDER_REFERENCE>
			<ORDER_PARTIES_REFERENCE>
				<BUYER_IDREF type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">9999999</BUYER_IDREF>
				<SUPPLIER_IDREF type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">9999999</SUPPLIER_IDREF>
			</ORDER_PARTIES_REFERENCE>
			<CURRENCY xmlns="http://www.bmecat.org/bmecat/2005">EUR</CURRENCY>
			<HEADER_UDX>
				<UDX.DG.CUSTOMER_TYPE>private_customer</UDX.DG.CUSTOMER_TYPE>
				<UDX.DG.DELIVERY_TYPE>direct_delivery</UDX.DG.DELIVERY_TYPE>
				<UDX.DG.SATURDAY_DELIVERY_ALLOWED>true</UDX.DG.SATURDAY_DELIVERY_ALLOWED>
				<UDX.DG.IS_COLLECTIVE_ORDER>false</UDX.DG.IS_COLLECTIVE_ORDER>
				<UDX.DG.PHYSICAL_DELIVERY_NOTE_REQUIRED>false</UDX.DG.PHYSICAL_DELIVERY_NOTE_REQUIRED>
			</HEADER_UDX>
		</ORDER_INFO>
	</ORDER_HEADER>
	<ORDER_ITEM_LIST>
		<ORDER_ITEM>
			<LINE_ITEM_ID>1</LINE_ITEM_ID>
			<PRODUCT_ID>
				<SUPPLIER_PID type="supplierProductKey" xmlns="http://www.bmecat.org/bmecat/2005">9999999999</SUPPLIER_PID>
				<INTERNATIONAL_PID type="gtin" xmlns="http://www.bmecat.org/bmecat/2005">04031101771236</INTERNATIONAL_PID>
				<BUYER_PID type="DgProductId" xmlns="http://www.bmecat.org/bmecat/2005">15711958</BUYER_PID>
				<DESCRIPTION_SHORT xmlns="http://www.bmecat.org/bmecat/2005">uvex Halbschuh 65682 S1P Gr.41 PUR-Sohle W11 (S1, 41)</DESCRIPTION_SHORT>
			</PRODUCT_ID>
			<QUANTITY>1</QUANTITY>
			<ORDER_UNIT xmlns="http://www.bmecat.org/bmecat/2005">C62</ORDER_UNIT>
			<PRODUCT_PRICE_FIX>
				<PRICE_AMOUNT xmlns="http://www.bmecat.org/bmecat/2005">99.14</PRICE_AMOUNT>
				<TAX_DETAILS_FIX>
					<TAX_AMOUNT>0.00</TAX_AMOUNT>
				</TAX_DETAILS_FIX>
			</PRODUCT_PRICE_FIX>
			<PRICE_LINE_AMOUNT>99.14</PRICE_LINE_AMOUNT>
			<DELIVERY_DATE type="optional">
				<DELIVERY_START_DATE>2022-05-06T00:00:00</DELIVERY_START_DATE>
				<DELIVERY_END_DATE>2022-05-06T00:00:00</DELIVERY_END_DATE>
			</DELIVERY_DATE>
		</ORDER_ITEM>
		<ORDER_ITEM>
			<LINE_ITEM_ID>2</LINE_ITEM_ID>
			<PRODUCT_ID>
				<SUPPLIER_PID type="supplierProductKey" xmlns="http://www.bmecat.org/bmecat/2005">9999999999</SUPPLIER_PID>
				<INTERNATIONAL_PID type="gtin" xmlns="http://www.bmecat.org/bmecat/2005">04031101771229</INTERNATIONAL_PID>
				<BUYER_PID type="DgProductId" xmlns="http://www.bmecat.org/bmecat/2005">15711959</BUYER_PID>
				<DESCRIPTION_SHORT xmlns="http://www.bmecat.org/bmecat/2005">uvex Halbschuh 65682 S1P Gr.40 PUR-Sohle W11 (S1, 40)</DESCRIPTION_SHORT>
			</PRODUCT_ID>
			<QUANTITY>1</QUANTITY>
			<ORDER_UNIT xmlns="http://www.bmecat.org/bmecat/2005">C62</ORDER_UNIT>
			<PRODUCT_PRICE_FIX>
				<PRICE_AMOUNT xmlns="http://www.bmecat.org/bmecat/2005">99.14</PRICE_AMOUNT>
				<TAX_DETAILS_FIX>
					<TAX_AMOUNT>0.00</TAX_AMOUNT>
				</TAX_DETAILS_FIX>
			</PRODUCT_PRICE_FIX>
			<PRICE_LINE_AMOUNT>99.14</PRICE_LINE_AMOUNT>
			<DELIVERY_DATE type="optional">
				<DELIVERY_START_DATE>2022-05-06T00:00:00</DELIVERY_START_DATE>
				<DELIVERY_END_DATE>2022-05-06T00:00:00</DELIVERY_END_DATE>
			</DELIVERY_DATE>
		</ORDER_ITEM>
	</ORDER_ITEM_LIST>
	<ORDER_SUMMARY>
		<TOTAL_ITEM_NUM>2</TOTAL_ITEM_NUM>
		<TOTAL_AMOUNT>198.28</TOTAL_AMOUNT>
	</ORDER_SUMMARY>
</ORDER>
```
9.  private customer DE
```<?xml version="1.0" encoding="utf-8"?>
<ORDER xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" version="2.1" type="standard" xmlns="http://www.opentrans.org/XMLSchema/2.1">
	<ORDER_HEADER>
		<CONTROL_INFO>
			<GENERATION_DATE>2022-03-28T11:40:38</GENERATION_DATE>
		</CONTROL_INFO>
		<ORDER_INFO>
			<ORDER_ID>61730207</ORDER_ID>
			<ORDER_DATE>2022-03-28T11:40:35</ORDER_DATE>
			<LANGUAGE xmlns="http://www.bmecat.org/bmecat/2005">ger</LANGUAGE>
			<PARTIES>
				<PARTY>
					<PARTY_ID type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">2705624</PARTY_ID>
					<PARTY_ID type="gln" xmlns="http://www.bmecat.org/bmecat/2005">7640489630003</PARTY_ID>
					<PARTY_ROLE>buyer</PARTY_ROLE>
					<ADDRESS>
						<NAME xmlns="http://www.bmecat.org/bmecat/2005">Galaxus Deutschland GmbH</NAME>
						<STREET xmlns="http://www.bmecat.org/bmecat/2005">Schützenstraße 5</STREET>
						<ZIP xmlns="http://www.bmecat.org/bmecat/2005">22761</ZIP>
						<CITY xmlns="http://www.bmecat.org/bmecat/2005">Hamburg</CITY>
						<COUNTRY xmlns="http://www.bmecat.org/bmecat/2005">Deutschland</COUNTRY>
						<COUNTRY_CODED xmlns="http://www.bmecat.org/bmecat/2005">DE</COUNTRY_CODED>
						<EMAIL xmlns="http://www.bmecat.org/bmecat/2005">noreply@galaxus.de</EMAIL>
						<VAT_ID xmlns="http://www.bmecat.org/bmecat/2005">DE312684999</VAT_ID>
					</ADDRESS>
				</PARTY>
				<PARTY>
					<PARTY_ID type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">2934949</PARTY_ID>
					<PARTY_ROLE>supplier</PARTY_ROLE>
					<ADDRESS>
						<NAME xmlns="http://www.bmecat.org/bmecat/2005">Musterfirma GmbH</NAME>
						<ZIP xmlns="http://www.bmecat.org/bmecat/2005">33181</ZIP>
						<CITY xmlns="http://www.bmecat.org/bmecat/2005">Wünnenberg</CITY>
						<COUNTRY xmlns="http://www.bmecat.org/bmecat/2005">Deutschland</COUNTRY>
						<COUNTRY_CODED xmlns="http://www.bmecat.org/bmecat/2005">DE</COUNTRY_CODED>
						<EMAIL xmlns="http://www.bmecat.org/bmecat/2005">noreply@galaxus.de</EMAIL>
					</ADDRESS>
				</PARTY>
				<PARTY>
					<PARTY_ID type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">3486316</PARTY_ID>
					<PARTY_ROLE>delivery</PARTY_ROLE>
					<ADDRESS>
						<CONTACT_DETAILS>
							<TITLE xmlns="http://www.bmecat.org/bmecat/2005">Frau</TITLE>
							<FIRST_NAME xmlns="http://www.bmecat.org/bmecat/2005">Annelies &amp; Richard</FIRST_NAME>
							<CONTACT_NAME xmlns="http://www.bmecat.org/bmecat/2005">Kaufmann+Meiermann Fam.</CONTACT_NAME>
						</CONTACT_DETAILS>
						<STREET xmlns="http://www.bmecat.org/bmecat/2005">Teststrasse 17</STREET>
						<ZIP xmlns="http://www.bmecat.org/bmecat/2005">70180</ZIP>
						<BOXNO xmlns="http://www.bmecat.org/bmecat/2005">9876</BOXNO>
						<CITY xmlns="http://www.bmecat.org/bmecat/2005">Stuttgart</CITY>
						<COUNTRY xmlns="http://www.bmecat.org/bmecat/2005">Deutschland</COUNTRY>
						<COUNTRY_CODED xmlns="http://www.bmecat.org/bmecat/2005">DE</COUNTRY_CODED>
						<PHONE xmlns="http://www.bmecat.org/bmecat/2005">01234567890</PHONE>
						<EMAIL xmlns="http://www.bmecat.org/bmecat/2005">noreply@galaxus.de</EMAIL>
					</ADDRESS>
				</PARTY>
				<PARTY>
					<PARTY_ID type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">DE</PARTY_ID>
					<PARTY_ROLE>marketplace</PARTY_ROLE>
					<ADDRESS>
						<NAME xmlns="http://www.bmecat.org/bmecat/2005">Galaxus Deutschland GmbH</NAME>
						<STREET xmlns="http://www.bmecat.org/bmecat/2005">Schützenstraße 5</STREET>
						<ZIP xmlns="http://www.bmecat.org/bmecat/2005">22761</ZIP>
						<CITY xmlns="http://www.bmecat.org/bmecat/2005">Hamburg</CITY>
						<COUNTRY xmlns="http://www.bmecat.org/bmecat/2005">Deutschland</COUNTRY>
						<COUNTRY_CODED xmlns="http://www.bmecat.org/bmecat/2005">DE</COUNTRY_CODED>
						<EMAIL xmlns="http://www.bmecat.org/bmecat/2005">noreply@galaxus.de</EMAIL>
					</ADDRESS>
				</PARTY>
			</PARTIES>
			<CUSTOMER_ORDER_REFERENCE>
				<ORDER_ID>61730205</ORDER_ID>
			</CUSTOMER_ORDER_REFERENCE>
			<ORDER_PARTIES_REFERENCE>
				<BUYER_IDREF type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">2705624</BUYER_IDREF>
				<SUPPLIER_IDREF type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">2934949</SUPPLIER_IDREF>
			</ORDER_PARTIES_REFERENCE>
			<CURRENCY xmlns="http://www.bmecat.org/bmecat/2005">EUR</CURRENCY>
			<HEADER_UDX>
				<UDX.DG.CUSTOMER_TYPE>private_customer</UDX.DG.CUSTOMER_TYPE>
				<UDX.DG.DELIVERY_TYPE>direct_delivery</UDX.DG.DELIVERY_TYPE>
				<UDX.DG.IS_COLLECTIVE_ORDER>false</UDX.DG.IS_COLLECTIVE_ORDER>
				<UDX.DG.PHYSICAL_DELIVERY_NOTE_REQUIRED>false</UDX.DG.PHYSICAL_DELIVERY_NOTE_REQUIRED>
			</HEADER_UDX>
		</ORDER_INFO>
	</ORDER_HEADER>
	<ORDER_ITEM_LIST>
		<ORDER_ITEM>
			<LINE_ITEM_ID>1</LINE_ITEM_ID>
			<PRODUCT_ID>
				<SUPPLIER_PID type="supplierProductKey" xmlns="http://www.bmecat.org/bmecat/2005">4070644</SUPPLIER_PID>
				<INTERNATIONAL_PID type="gtin" xmlns="http://www.bmecat.org/bmecat/2005">04062319432909</INTERNATIONAL_PID>
				<BUYER_PID type="DgProductId" xmlns="http://www.bmecat.org/bmecat/2005">15249321</BUYER_PID>
				<DESCRIPTION_SHORT xmlns="http://www.bmecat.org/bmecat/2005">Apple Mac Mini (M1, 16 GB, 256 GB, SSD)</DESCRIPTION_SHORT>
			</PRODUCT_ID>
			<QUANTITY>1</QUANTITY>
			<ORDER_UNIT xmlns="http://www.bmecat.org/bmecat/2005">C62</ORDER_UNIT>
			<PRODUCT_PRICE_FIX>
				<PRICE_AMOUNT xmlns="http://www.bmecat.org/bmecat/2005">760.97</PRICE_AMOUNT>
				<TAX_DETAILS_FIX>
					<TAX_AMOUNT>144.58</TAX_AMOUNT>
				</TAX_DETAILS_FIX>
			</PRODUCT_PRICE_FIX>
			<PRICE_LINE_AMOUNT>760.97</PRICE_LINE_AMOUNT>
			<DELIVERY_DATE type="optional">
				<DELIVERY_START_DATE>2022-03-30T00:00:00</DELIVERY_START_DATE>
				<DELIVERY_END_DATE>2022-03-30T00:00:00</DELIVERY_END_DATE>
			</DELIVERY_DATE>
		</ORDER_ITEM>
	</ORDER_ITEM_LIST>
	<ORDER_SUMMARY>
		<TOTAL_ITEM_NUM>1</TOTAL_ITEM_NUM>
		<TOTAL_AMOUNT>760.97</TOTAL_AMOUNT>
	</ORDER_SUMMARY>
</ORDER>
```
10. private customer with company address AT
```<?xml version="1.0" encoding="utf-8"?>
<ORDER xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" version="2.1" type="standard" xmlns="http://www.opentrans.org/XMLSchema/2.1">
	<ORDER_HEADER>
		<CONTROL_INFO>
			<GENERATION_DATE>2022-03-28T11:51:43</GENERATION_DATE>
		</CONTROL_INFO>
		<ORDER_INFO>
			<ORDER_ID>61730210</ORDER_ID>
			<ORDER_DATE>2022-03-28T11:51:41</ORDER_DATE>
			<LANGUAGE xmlns="http://www.bmecat.org/bmecat/2005">ger</LANGUAGE>
			<PARTIES>
				<PARTY>
					<PARTY_ID type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">2705624</PARTY_ID>
					<PARTY_ID type="gln" xmlns="http://www.bmecat.org/bmecat/2005">7640489630003</PARTY_ID>
					<PARTY_ROLE>buyer</PARTY_ROLE>
					<ADDRESS>
						<NAME xmlns="http://www.bmecat.org/bmecat/2005">Galaxus Deutschland GmbH</NAME>
						<STREET xmlns="http://www.bmecat.org/bmecat/2005">Schützenstraße 5</STREET>
						<ZIP xmlns="http://www.bmecat.org/bmecat/2005">22761</ZIP>
						<CITY xmlns="http://www.bmecat.org/bmecat/2005">Hamburg</CITY>
						<COUNTRY xmlns="http://www.bmecat.org/bmecat/2005">Deutschland</COUNTRY>
						<COUNTRY_CODED xmlns="http://www.bmecat.org/bmecat/2005">DE</COUNTRY_CODED>
						<EMAIL xmlns="http://www.bmecat.org/bmecat/2005">noreply@galaxus.de</EMAIL>
						<VAT_ID xmlns="http://www.bmecat.org/bmecat/2005">ATU77168912</VAT_ID>
					</ADDRESS>
				</PARTY>
				<PARTY>
					<PARTY_ID type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">6503114</PARTY_ID>
					<PARTY_ROLE>supplier</PARTY_ROLE>
					<ADDRESS>
						<NAME xmlns="http://www.bmecat.org/bmecat/2005">Musterfirma GmbH</NAME>
						<STREET xmlns="http://www.bmecat.org/bmecat/2005">Teststrasse 17</STREET>
						<ZIP xmlns="http://www.bmecat.org/bmecat/2005">1010</ZIP>
						<CITY xmlns="http://www.bmecat.org/bmecat/2005">Wien</CITY>
						<COUNTRY xmlns="http://www.bmecat.org/bmecat/2005">Österreich</COUNTRY>
						<COUNTRY_CODED xmlns="http://www.bmecat.org/bmecat/2005">AT</COUNTRY_CODED>
						<EMAIL xmlns="http://www.bmecat.org/bmecat/2005">noreply@galaxus.de</EMAIL>
					</ADDRESS>
				</PARTY>
				<PARTY>
					<PARTY_ID type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">3486316</PARTY_ID>
					<PARTY_ROLE>delivery</PARTY_ROLE>
					<ADDRESS>
						<NAME xmlns="http://www.bmecat.org/bmecat/2005">Musterfirma AG</NAME>
						<CONTACT_DETAILS>
							<FIRST_NAME xmlns="http://www.bmecat.org/bmecat/2005">Richard Mayerhofer</FIRST_NAME>
							<CONTACT_NAME xmlns="http://www.bmecat.org/bmecat/2005">Kaufmann+Meiermann Fam.</CONTACT_NAME>
						</CONTACT_DETAILS>
						<STREET xmlns="http://www.bmecat.org/bmecat/2005">Teststrasse 17</STREET>
						<ZIP xmlns="http://www.bmecat.org/bmecat/2005">1010</ZIP>
						<BOXNO xmlns="http://www.bmecat.org/bmecat/2005">1234</BOXNO>
						<CITY xmlns="http://www.bmecat.org/bmecat/2005">Wien</CITY>
						<COUNTRY xmlns="http://www.bmecat.org/bmecat/2005">Österreich</COUNTRY>
						<COUNTRY_CODED xmlns="http://www.bmecat.org/bmecat/2005">AT</COUNTRY_CODED>
						<PHONE xmlns="http://www.bmecat.org/bmecat/2005">+43000000000</PHONE>
						<EMAIL xmlns="http://www.bmecat.org/bmecat/2005">noreply@galaxus.de</EMAIL>
					</ADDRESS>
				</PARTY>
				<PARTY>
					<PARTY_ID type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">AT</PARTY_ID>
					<PARTY_ROLE>marketplace</PARTY_ROLE>
					<ADDRESS>
						<NAME xmlns="http://www.bmecat.org/bmecat/2005">Galaxus Deutschland GmbH</NAME>
						<STREET xmlns="http://www.bmecat.org/bmecat/2005">Schützenstraße 5</STREET>
						<ZIP xmlns="http://www.bmecat.org/bmecat/2005">22761</ZIP>
						<CITY xmlns="http://www.bmecat.org/bmecat/2005">Hamburg</CITY>
						<COUNTRY xmlns="http://www.bmecat.org/bmecat/2005">Deutschland</COUNTRY>
						<COUNTRY_CODED xmlns="http://www.bmecat.org/bmecat/2005">DE</COUNTRY_CODED>
						<EMAIL xmlns="http://www.bmecat.org/bmecat/2005">noreply@galaxus.de</EMAIL>
					</ADDRESS>
				</PARTY>
			</PARTIES>
			<CUSTOMER_ORDER_REFERENCE>
				<ORDER_ID>61730209</ORDER_ID>
			</CUSTOMER_ORDER_REFERENCE>
			<ORDER_PARTIES_REFERENCE>
				<BUYER_IDREF type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">2705624</BUYER_IDREF>
				<SUPPLIER_IDREF type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">6503114</SUPPLIER_IDREF>
			</ORDER_PARTIES_REFERENCE>
			<CURRENCY xmlns="http://www.bmecat.org/bmecat/2005">EUR</CURRENCY>
			<HEADER_UDX>
				<UDX.DG.CUSTOMER_TYPE>private_customer</UDX.DG.CUSTOMER_TYPE>
				<UDX.DG.DELIVERY_TYPE>direct_delivery</UDX.DG.DELIVERY_TYPE>
				<UDX.DG.IS_COLLECTIVE_ORDER>false</UDX.DG.IS_COLLECTIVE_ORDER>
				<UDX.DG.PHYSICAL_DELIVERY_NOTE_REQUIRED>false</UDX.DG.PHYSICAL_DELIVERY_NOTE_REQUIRED>
			</HEADER_UDX>
		</ORDER_INFO>
	</ORDER_HEADER>
	<ORDER_ITEM_LIST>
		<ORDER_ITEM>
			<LINE_ITEM_ID>1</LINE_ITEM_ID>
			<PRODUCT_ID>
				<SUPPLIER_PID type="supplierProductKey" xmlns="http://www.bmecat.org/bmecat/2005">12345</SUPPLIER_PID>
				<INTERNATIONAL_PID type="gtin" xmlns="http://www.bmecat.org/bmecat/2005">04062319432909</INTERNATIONAL_PID>
				<BUYER_PID type="DgProductId" xmlns="http://www.bmecat.org/bmecat/2005">15249321</BUYER_PID>
				<DESCRIPTION_SHORT xmlns="http://www.bmecat.org/bmecat/2005">Apple Mac Mini (M1, 16 GB, 256 GB, SSD)</DESCRIPTION_SHORT>
			</PRODUCT_ID>
			<QUANTITY>1</QUANTITY>
			<ORDER_UNIT xmlns="http://www.bmecat.org/bmecat/2005">C62</ORDER_UNIT>
			<PRODUCT_PRICE_FIX>
				<PRICE_AMOUNT xmlns="http://www.bmecat.org/bmecat/2005">1.00</PRICE_AMOUNT>
				<TAX_DETAILS_FIX>
					<TAX_AMOUNT>0.20</TAX_AMOUNT>
				</TAX_DETAILS_FIX>
			</PRODUCT_PRICE_FIX>
			<PRICE_LINE_AMOUNT>1.00</PRICE_LINE_AMOUNT>
			<DELIVERY_DATE type="optional">
				<DELIVERY_START_DATE>2022-04-05T00:00:00</DELIVERY_START_DATE>
				<DELIVERY_END_DATE>2022-04-05T00:00:00</DELIVERY_END_DATE>
			</DELIVERY_DATE>
		</ORDER_ITEM>
	</ORDER_ITEM_LIST>
	<ORDER_SUMMARY>
		<TOTAL_ITEM_NUM>1</TOTAL_ITEM_NUM>
		<TOTAL_AMOUNT>1.00</TOTAL_AMOUNT>
	</ORDER_SUMMARY>
</ORDER>
```
11. warehouse delivery (Krefled Fichtenhain)
```<?xml version="1.0" encoding="utf-8"?>
<ORDER xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" version="2.1" type="standard" xmlns="http://www.opentrans.org/XMLSchema/2.1">
	<ORDER_HEADER>
		<CONTROL_INFO>
			<GENERATION_DATE>2022-03-28T13:28:48</GENERATION_DATE>
		</CONTROL_INFO>
		<ORDER_INFO>
			<ORDER_ID>61730225</ORDER_ID>
			<ORDER_DATE>2022-03-28T13:28:38</ORDER_DATE>
			<LANGUAGE xmlns="http://www.bmecat.org/bmecat/2005">ger</LANGUAGE>
			<PARTIES>
				<PARTY>
					<PARTY_ID type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">2705624</PARTY_ID>
					<PARTY_ID type="gln" xmlns="http://www.bmecat.org/bmecat/2005">7640489630003</PARTY_ID>
					<PARTY_ROLE>buyer</PARTY_ROLE>
					<ADDRESS>
						<NAME xmlns="http://www.bmecat.org/bmecat/2005">Galaxus Deutschland GmbH</NAME>
						<STREET xmlns="http://www.bmecat.org/bmecat/2005">Schützenstraße 5</STREET>
						<ZIP xmlns="http://www.bmecat.org/bmecat/2005">22761</ZIP>
						<CITY xmlns="http://www.bmecat.org/bmecat/2005">Hamburg</CITY>
						<COUNTRY xmlns="http://www.bmecat.org/bmecat/2005">Deutschland</COUNTRY>
						<COUNTRY_CODED xmlns="http://www.bmecat.org/bmecat/2005">DE</COUNTRY_CODED>
						<EMAIL xmlns="http://www.bmecat.org/bmecat/2005">noreply@galaxus.de</EMAIL>
						<VAT_ID xmlns="http://www.bmecat.org/bmecat/2005">DE312684999</VAT_ID>
					</ADDRESS>
				</PARTY>
				<PARTY>
					<PARTY_ID type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">2934949</PARTY_ID>
					<PARTY_ROLE>supplier</PARTY_ROLE>
					<ADDRESS>
						<NAME xmlns="http://www.bmecat.org/bmecat/2005">Musterfirma GmbH</NAME>
						<ZIP xmlns="http://www.bmecat.org/bmecat/2005">53111</ZIP>
						<CITY xmlns="http://www.bmecat.org/bmecat/2005">Bonn</CITY>
						<COUNTRY xmlns="http://www.bmecat.org/bmecat/2005">Deutschland</COUNTRY>
						<COUNTRY_CODED xmlns="http://www.bmecat.org/bmecat/2005">DE</COUNTRY_CODED>
						<EMAIL xmlns="http://www.bmecat.org/bmecat/2005">noreply@galaxus.de</EMAIL>
					</ADDRESS>
				</PARTY>
				<PARTY>
					<PARTY_ID type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">2705624</PARTY_ID>
					<PARTY_ID type="gln" xmlns="http://www.bmecat.org/bmecat/2005">7640489630010</PARTY_ID>
					<PARTY_ROLE>delivery</PARTY_ROLE>
					<ADDRESS>
						<NAME xmlns="http://www.bmecat.org/bmecat/2005">Galaxus Deutschland GmbH</NAME>
						<STREET xmlns="http://www.bmecat.org/bmecat/2005">Hans-Günther-Sohl-Straße 2-4</STREET>
						<ZIP xmlns="http://www.bmecat.org/bmecat/2005">47807</ZIP>
						<CITY xmlns="http://www.bmecat.org/bmecat/2005">Krefeld</CITY>
						<COUNTRY xmlns="http://www.bmecat.org/bmecat/2005">Deutschland</COUNTRY>
						<COUNTRY_CODED xmlns="http://www.bmecat.org/bmecat/2005">DE</COUNTRY_CODED>
						<EMAIL xmlns="http://www.bmecat.org/bmecat/2005">noreply@galaxus.de</EMAIL>
					</ADDRESS>
				</PARTY>
				<PARTY>
					<PARTY_ID type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">10515672</PARTY_ID>
					<PARTY_ROLE>marketplace</PARTY_ROLE>
					<ADDRESS>
						<NAME xmlns="http://www.bmecat.org/bmecat/2005">Galaxus Deutschland GmbH</NAME>
						<STREET xmlns="http://www.bmecat.org/bmecat/2005">Schützenstraße 5</STREET>
						<ZIP xmlns="http://www.bmecat.org/bmecat/2005">22761</ZIP>
						<CITY xmlns="http://www.bmecat.org/bmecat/2005">Hamburg</CITY>
						<COUNTRY xmlns="http://www.bmecat.org/bmecat/2005">Deutschland</COUNTRY>
						<COUNTRY_CODED xmlns="http://www.bmecat.org/bmecat/2005">DE</COUNTRY_CODED>
						<EMAIL xmlns="http://www.bmecat.org/bmecat/2005">noreply@galaxus.de</EMAIL>
					</ADDRESS>
				</PARTY>
			</PARTIES>
			<ORDER_PARTIES_REFERENCE>
				<BUYER_IDREF type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">2705624</BUYER_IDREF>
				<SUPPLIER_IDREF type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">2934949</SUPPLIER_IDREF>
			</ORDER_PARTIES_REFERENCE>
			<CURRENCY xmlns="http://www.bmecat.org/bmecat/2005">EUR</CURRENCY>
			<HEADER_UDX>
				<UDX.DG.CUSTOMER_TYPE>company</UDX.DG.CUSTOMER_TYPE>
				<UDX.DG.DELIVERY_TYPE>warehouse_delivery</UDX.DG.DELIVERY_TYPE>
				<UDX.DG.IS_COLLECTIVE_ORDER>false</UDX.DG.IS_COLLECTIVE_ORDER>
				<UDX.DG.PHYSICAL_DELIVERY_NOTE_REQUIRED>true</UDX.DG.PHYSICAL_DELIVERY_NOTE_REQUIRED>
			</HEADER_UDX>
		</ORDER_INFO>
	</ORDER_HEADER>
	<ORDER_ITEM_LIST>
		<ORDER_ITEM>
			<LINE_ITEM_ID>1</LINE_ITEM_ID>
			<PRODUCT_ID>
				<SUPPLIER_PID type="supplierProductKey" xmlns="http://www.bmecat.org/bmecat/2005">3362965</SUPPLIER_PID>
				<INTERNATIONAL_PID type="gtin" xmlns="http://www.bmecat.org/bmecat/2005">04710180195132</INTERNATIONAL_PID>
				<BUYER_PID type="DgProductId" xmlns="http://www.bmecat.org/bmecat/2005">10776901</BUYER_PID>
				<DESCRIPTION_SHORT xmlns="http://www.bmecat.org/bmecat/2005">Acer ED273URP (2560 x 1440 Pixels, 27 ")</DESCRIPTION_SHORT>
			</PRODUCT_ID>
			<QUANTITY>1</QUANTITY>
			<ORDER_UNIT xmlns="http://www.bmecat.org/bmecat/2005">C62</ORDER_UNIT>
			<PRODUCT_PRICE_FIX>
				<PRICE_AMOUNT xmlns="http://www.bmecat.org/bmecat/2005">276.66</PRICE_AMOUNT>
				<TAX_DETAILS_FIX>
					<TAX_AMOUNT>52.57</TAX_AMOUNT>
				</TAX_DETAILS_FIX>
			</PRODUCT_PRICE_FIX>
			<PRICE_LINE_AMOUNT>276.66</PRICE_LINE_AMOUNT>
			<DELIVERY_DATE type="optional">
				<DELIVERY_START_DATE>2022-03-30T00:00:00</DELIVERY_START_DATE>
				<DELIVERY_END_DATE>2022-03-30T00:00:00</DELIVERY_END_DATE>
			</DELIVERY_DATE>
		</ORDER_ITEM>
		<ORDER_ITEM>
			<LINE_ITEM_ID>2</LINE_ITEM_ID>
			<PRODUCT_ID>
				<SUPPLIER_PID type="supplierProductKey" xmlns="http://www.bmecat.org/bmecat/2005">4070644</SUPPLIER_PID>
				<INTERNATIONAL_PID type="gtin" xmlns="http://www.bmecat.org/bmecat/2005">04062319432909</INTERNATIONAL_PID>
				<BUYER_PID type="DgProductId" xmlns="http://www.bmecat.org/bmecat/2005">15249321</BUYER_PID>
				<DESCRIPTION_SHORT xmlns="http://www.bmecat.org/bmecat/2005">Apple Mac Mini (M1, 16 GB, 256 GB, SSD)</DESCRIPTION_SHORT>
			</PRODUCT_ID>
			<QUANTITY>1</QUANTITY>
			<ORDER_UNIT xmlns="http://www.bmecat.org/bmecat/2005">C62</ORDER_UNIT>
			<PRODUCT_PRICE_FIX>
				<PRICE_AMOUNT xmlns="http://www.bmecat.org/bmecat/2005">760.97</PRICE_AMOUNT>
				<TAX_DETAILS_FIX>
					<TAX_AMOUNT>144.58</TAX_AMOUNT>
				</TAX_DETAILS_FIX>
			</PRODUCT_PRICE_FIX>
			<PRICE_LINE_AMOUNT>760.97</PRICE_LINE_AMOUNT>
			<DELIVERY_DATE type="optional">
				<DELIVERY_START_DATE>2022-03-30T00:00:00</DELIVERY_START_DATE>
				<DELIVERY_END_DATE>2022-03-30T00:00:00</DELIVERY_END_DATE>
			</DELIVERY_DATE>
		</ORDER_ITEM>
	</ORDER_ITEM_LIST>
	<ORDER_SUMMARY>
		<TOTAL_ITEM_NUM>2</TOTAL_ITEM_NUM>
		<TOTAL_AMOUNT>1037.63</TOTAL_AMOUNT>
	</ORDER_SUMMARY>
</ORDER>
```
12. warehouse delivery (Krefled Huls)
```<?xml version="1.0" encoding="utf-8"?>
<ORDER xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" version="2.1" type="standard" xmlns="http://www.opentrans.org/XMLSchema/2.1">
	<ORDER_HEADER>
		<CONTROL_INFO>
			<GENERATION_DATE>2022-09-14T13:31:56</GENERATION_DATE>
		</CONTROL_INFO>
		<ORDER_INFO>
			<ORDER_ID>71234567</ORDER_ID>
			<ORDER_DATE>2022-09-14T13:31:56</ORDER_DATE>
			<LANGUAGE xmlns="http://www.bmecat.org/bmecat/2005">ger</LANGUAGE>
			<PARTIES>
				<PARTY>
					<PARTY_ID type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">2705624</PARTY_ID>
					<PARTY_ID type="gln" xmlns="http://www.bmecat.org/bmecat/2005">7640489630003</PARTY_ID>
					<PARTY_ROLE>buyer</PARTY_ROLE>
					<ADDRESS>
						<NAME xmlns="http://www.bmecat.org/bmecat/2005">Galaxus Deutschland GmbH</NAME>
						<STREET xmlns="http://www.bmecat.org/bmecat/2005">Schützenstraße 5</STREET>
						<ZIP xmlns="http://www.bmecat.org/bmecat/2005">22761</ZIP>
						<CITY xmlns="http://www.bmecat.org/bmecat/2005">Hamburg</CITY>
						<COUNTRY xmlns="http://www.bmecat.org/bmecat/2005">Deutschland</COUNTRY>
						<COUNTRY_CODED xmlns="http://www.bmecat.org/bmecat/2005">DE</COUNTRY_CODED>
						<EMAIL xmlns="http://www.bmecat.org/bmecat/2005">noreply@galaxus.de</EMAIL>
						<VAT_ID xmlns="http://www.bmecat.org/bmecat/2005">DE312684999</VAT_ID>
					</ADDRESS>
				</PARTY>
				<PARTY>
					<PARTY_ID type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">1234567</PARTY_ID>
					<PARTY_ROLE>supplier</PARTY_ROLE>
					<ADDRESS>
						<NAME xmlns="http://www.bmecat.org/bmecat/2005">Musterfirma AG</NAME>
						<STREET xmlns="http://www.bmecat.org/bmecat/2005">Teststrasse 17</STREET>
						<ZIP xmlns="http://www.bmecat.org/bmecat/2005">10115</ZIP>
						<CITY xmlns="http://www.bmecat.org/bmecat/2005">Berlin</CITY>
						<COUNTRY xmlns="http://www.bmecat.org/bmecat/2005">Deutschland</COUNTRY>
						<COUNTRY_CODED xmlns="http://www.bmecat.org/bmecat/2005">DE</COUNTRY_CODED>
						<EMAIL xmlns="http://www.bmecat.org/bmecat/2005">noreply@galaxus.de</EMAIL>
					</ADDRESS>
				</PARTY>
				<PARTY>
					<PARTY_ID type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">2705624</PARTY_ID>
					<PARTY_ID type="gln" xmlns="http://www.bmecat.org/bmecat/2005">7640489630027</PARTY_ID>
					<PARTY_ROLE>delivery</PARTY_ROLE>
					<ADDRESS>
						<NAME xmlns="http://www.bmecat.org/bmecat/2005">Galaxus Deutschland GmbH</NAME>
						<DEPARTMENT xmlns="http://www.bmecat.org/bmecat/2005">Receiving</DEPARTMENT>
						<STREET xmlns="http://www.bmecat.org/bmecat/2005">Odilia-von-Goch 15</STREET>
						<ZIP xmlns="http://www.bmecat.org/bmecat/2005">47839</ZIP>
						<CITY xmlns="http://www.bmecat.org/bmecat/2005">Krefeld</CITY>
						<COUNTRY xmlns="http://www.bmecat.org/bmecat/2005">Deutschland</COUNTRY>
						<COUNTRY_CODED xmlns="http://www.bmecat.org/bmecat/2005">DE</COUNTRY_CODED>
						<EMAIL xmlns="http://www.bmecat.org/bmecat/2005">noreply@galaxus.de</EMAIL>
					</ADDRESS>
				</PARTY>
				<PARTY>
					<PARTY_ID type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">DE</PARTY_ID>
					<PARTY_ROLE>marketplace</PARTY_ROLE>
					<ADDRESS>
						<NAME xmlns="http://www.bmecat.org/bmecat/2005">Galaxus Deutschland GmbH</NAME>
						<STREET xmlns="http://www.bmecat.org/bmecat/2005">Schützenstraße 5</STREET>
						<ZIP xmlns="http://www.bmecat.org/bmecat/2005">22761</ZIP>
						<CITY xmlns="http://www.bmecat.org/bmecat/2005">Hamburg</CITY>
						<COUNTRY xmlns="http://www.bmecat.org/bmecat/2005">Deutschland</COUNTRY>
						<COUNTRY_CODED xmlns="http://www.bmecat.org/bmecat/2005">DE</COUNTRY_CODED>
						<EMAIL xmlns="http://www.bmecat.org/bmecat/2005">noreply@galaxus.de</EMAIL>
					</ADDRESS>
				</PARTY>
			</PARTIES>
			<ORDER_PARTIES_REFERENCE>
				<BUYER_IDREF type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">2705624</BUYER_IDREF>
				<SUPPLIER_IDREF type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">7015688</SUPPLIER_IDREF>
			</ORDER_PARTIES_REFERENCE>
			<CURRENCY xmlns="http://www.bmecat.org/bmecat/2005">EUR</CURRENCY>
			<HEADER_UDX>
				<UDX.DG.CUSTOMER_TYPE>company</UDX.DG.CUSTOMER_TYPE>
				<UDX.DG.DELIVERY_TYPE>warehouse_delivery</UDX.DG.DELIVERY_TYPE>
				<UDX.DG.IS_COLLECTIVE_ORDER>false</UDX.DG.IS_COLLECTIVE_ORDER>
				<UDX.DG.PHYSICAL_DELIVERY_NOTE_REQUIRED>true</UDX.DG.PHYSICAL_DELIVERY_NOTE_REQUIRED>
			</HEADER_UDX>
		</ORDER_INFO>
	</ORDER_HEADER>
	<ORDER_ITEM_LIST>
		<ORDER_ITEM>
			<LINE_ITEM_ID>1</LINE_ITEM_ID>
			<PRODUCT_ID>
				<SUPPLIER_PID type="supplierProductKey" xmlns="http://www.bmecat.org/bmecat/2005">1234567</SUPPLIER_PID>
				<INTERNATIONAL_PID type="gtin" xmlns="http://www.bmecat.org/bmecat/2005">04710180195132</INTERNATIONAL_PID>
				<BUYER_PID type="DgProductId" xmlns="http://www.bmecat.org/bmecat/2005">10776901</BUYER_PID>
				<DESCRIPTION_SHORT xmlns="http://www.bmecat.org/bmecat/2005">Acer ED273URP (2560 x 1440 Pixels, 27 ")</DESCRIPTION_SHORT>
			</PRODUCT_ID>
			<QUANTITY>1</QUANTITY>
			<ORDER_UNIT xmlns="http://www.bmecat.org/bmecat/2005">C62</ORDER_UNIT>
			<PRODUCT_PRICE_FIX>
				<PRICE_AMOUNT xmlns="http://www.bmecat.org/bmecat/2005">276.66</PRICE_AMOUNT>
				<TAX_DETAILS_FIX>
					<TAX_AMOUNT>52.57</TAX_AMOUNT>
				</TAX_DETAILS_FIX>
			</PRODUCT_PRICE_FIX>
			<PRICE_LINE_AMOUNT>276.66</PRICE_LINE_AMOUNT>
			<DELIVERY_DATE type="optional">
                <DELIVERY_START_DATE>2022-09-29T00:00:00</DELIVERY_START_DATE>
                <DELIVERY_END_DATE>2022-09-29T00:00:00</DELIVERY_END_DATE>
			</DELIVERY_DATE>
		</ORDER_ITEM>
		<ORDER_ITEM>
			<LINE_ITEM_ID>2</LINE_ITEM_ID>
			<PRODUCT_ID>
				<SUPPLIER_PID type="supplierProductKey" xmlns="http://www.bmecat.org/bmecat/2005">2345678</SUPPLIER_PID>
				<INTERNATIONAL_PID type="gtin" xmlns="http://www.bmecat.org/bmecat/2005">04062319432909</INTERNATIONAL_PID>
				<BUYER_PID type="DgProductId" xmlns="http://www.bmecat.org/bmecat/2005">15249321</BUYER_PID>
				<DESCRIPTION_SHORT xmlns="http://www.bmecat.org/bmecat/2005">Apple Mac Mini (M1, 16 GB, 256 GB, SSD)</DESCRIPTION_SHORT>
			</PRODUCT_ID>
			<QUANTITY>1</QUANTITY>
			<ORDER_UNIT xmlns="http://www.bmecat.org/bmecat/2005">C62</ORDER_UNIT>
			<PRODUCT_PRICE_FIX>
				<PRICE_AMOUNT xmlns="http://www.bmecat.org/bmecat/2005">760.97</PRICE_AMOUNT>
				<TAX_DETAILS_FIX>
					<TAX_AMOUNT>144.58</TAX_AMOUNT>
				</TAX_DETAILS_FIX>
			</PRODUCT_PRICE_FIX>
			<PRICE_LINE_AMOUNT>760.97</PRICE_LINE_AMOUNT>
			<DELIVERY_DATE type="optional">
				<DELIVERY_START_DATE>2022-09-29T00:00:00</DELIVERY_START_DATE>
                <DELIVERY_END_DATE>2022-09-29T00:00:00</DELIVERY_END_DATE>
			</DELIVERY_DATE>
		</ORDER_ITEM>
	</ORDER_ITEM_LIST>
	<ORDER_SUMMARY>
		<TOTAL_ITEM_NUM>2</TOTAL_ITEM_NUM>
		<TOTAL_AMOUNT>1037.63</TOTAL_AMOUNT>
	</ORDER_SUMMARY>
</ORDER>
```

