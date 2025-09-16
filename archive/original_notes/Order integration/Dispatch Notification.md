this file contains the informations that related to shipping process 
like how it will be shipping and the shipping ids.
this file is exist in  partner2dg as it coming from our side
if it wrong  the order will be cancelled
we have only two way of the reponse on this 
we will set this file to tell that these are the information of the shipping process details
the name of the file should be named as GDELR_<SupplierId>_<OrderId>_<*>_<Timestamp>.xml
and have this structure ![image](<../Images/Order/shipping.png>)

first is the header mention before in readme file
then the control info contain the date
and then the dispatching info contain id ,date then the parties as mentioned before
shipment_id and shipment carrier 
these info are the critical because if it wrong the order will be cancelled

then list of shipping products each one has the product details and quantity and order id
 
 we have to possible structure
 like 
 direct delivery to end customer
```xml
<?xml version="1.0" encoding="utf-8"?>
<DISPATCHNOTIFICATION xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns="http://www.opentrans.org/XMLSchema/2.1" version="2.1">
	<DISPATCHNOTIFICATION_HEADER>
		<CONTROL_INFO>
			<GENERATION_DATE>2017-06-14T17:05:34</GENERATION_DATE>
		</CONTROL_INFO>
		<DISPATCHNOTIFICATION_INFO>
			<DISPATCHNOTIFICATION_ID>11720161201040841</DISPATCHNOTIFICATION_ID>
			<DISPATCHNOTIFICATION_DATE>2017-06-14T17:15:01</DISPATCHNOTIFICATION_DATE>
			<PARTIES>
				<PARTY>
					<PARTY_ROLE>delivery</PARTY_ROLE>
					<ADDRESS>
						<NAME xmlns="http://www.bmecat.org/bmecat/2005">Mustermann GmbH</NAME>
						<CONTACT_DETAILS>
							<FIRST_NAME xmlns="http://www.bmecat.org/bmecat/2005">Ulla</FIRST_NAME>
							<CONTACT_NAME xmlns="http://www.bmecat.org/bmecat/2005">Mustermann</CONTACT_NAME>
						</CONTACT_DETAILS>
						<STREET xmlns="http://www.bmecat.org/bmecat/2005">Musterstrasse 200b</STREET>
						<ZIP xmlns="http://www.bmecat.org/bmecat/2005">8000</ZIP>
						<CITY xmlns="http://www.bmecat.org/bmecat/2005">Zürich</CITY>
						<COUNTRY xmlns="http://www.bmecat.org/bmecat/2005">Schweiz</COUNTRY>
						<COUNTRY_CODED xmlns="http://www.bmecat.org/bmecat/2005">CH</COUNTRY_CODED>
					</ADDRESS>
				</PARTY>
			</PARTIES>
			<SHIPMENT_ID>99.00.123456.12345678</SHIPMENT_ID>
			<SHIPMENT_CARRIER>swisspost</SHIPMENT_CARRIER>
		</DISPATCHNOTIFICATION_INFO>
	</DISPATCHNOTIFICATION_HEADER>
	<DISPATCHNOTIFICATION_ITEM_LIST>
		<DISPATCHNOTIFICATION_ITEM>
			<PRODUCT_ID>
				<SUPPLIER_PID xmlns="http://www.bmecat.org/bmecat/2005">A375-129</SUPPLIER_PID>
				<INTERNATIONAL_PID xmlns="http://www.bmecat.org/bmecat/2005">09783404175109</INTERNATIONAL_PID>
				<BUYER_PID xmlns="http://www.bmecat.org/bmecat/2005">6406567</BUYER_PID>
			</PRODUCT_ID>
			<QUANTITY>2</QUANTITY>
			<ORDER_REFERENCE>
				<ORDER_ID>9316271</ORDER_ID>
			</ORDER_REFERENCE>
		</DISPATCHNOTIFICATION_ITEM>
	</DISPATCHNOTIFICATION_ITEM_LIST>
</DISPATCHNOTIFICATION>
```
2. example warehouse delivery
```xml
<?xml version="1.0" encoding="utf-8"?>
<DISPATCHNOTIFICATION xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns="http://www.opentrans.org/XMLSchema/2.1" version="2.1">
	<DISPATCHNOTIFICATION_HEADER>
		<CONTROL_INFO>
			<GENERATION_DATE>2019-02-13T17:05:34</GENERATION_DATE>
		</CONTROL_INFO>
		<DISPATCHNOTIFICATION_INFO>
			<DISPATCHNOTIFICATION_ID>11720161201040841</DISPATCHNOTIFICATION_ID>
			<DISPATCHNOTIFICATION_DATE>2019-02-13T17:05:45</DISPATCHNOTIFICATION_DATE>
			<PARTIES>
				<PARTY>
					<PARTY_ROLE>delivery</PARTY_ROLE>
					<ADDRESS>
						<NAME xmlns="http://www.bmecat.org/bmecat/2005">Digitec Galaxus AG</NAME>
						<NAME2 xmlns="http://www.bmecat.org/bmecat/2005">Receiving Wohlen</NAME2>
						<STREET xmlns="http://www.bmecat.org/bmecat/2005">Ferroring 23</STREET>
						<ZIP xmlns="http://www.bmecat.org/bmecat/2005">5612</ZIP>
						<CITY xmlns="http://www.bmecat.org/bmecat/2005">Villmergen</CITY>
						<COUNTRY xmlns="http://www.bmecat.org/bmecat/2005">Schweiz</COUNTRY>
						<COUNTRY_CODED xmlns="http://www.bmecat.org/bmecat/2005">CH</COUNTRY_CODED>
					</ADDRESS>
				</PARTY>
			</PARTIES>
		</DISPATCHNOTIFICATION_INFO>
	</DISPATCHNOTIFICATION_HEADER>
	<DISPATCHNOTIFICATION_ITEM_LIST>
		<DISPATCHNOTIFICATION_ITEM>
			<PRODUCT_ID>
				<SUPPLIER_PID xmlns="http://www.bmecat.org/bmecat/2005">A375-129</SUPPLIER_PID>
				<INTERNATIONAL_PID xmlns="http://www.bmecat.org/bmecat/2005">09783404175109</INTERNATIONAL_PID>
				<BUYER_PID xmlns="http://www.bmecat.org/bmecat/2005">6406567</BUYER_PID>
			</PRODUCT_ID>
			<QUANTITY>2</QUANTITY>
			<ORDER_REFERENCE>
				<ORDER_ID>9316271</ORDER_ID>
			</ORDER_REFERENCE>
			<LOGISTIC_DETAILS>
				<PACKAGE_INFO>
					<PACKAGE>
						<PACKAGE_ID>00001234560000000018</PACKAGE_ID>
						<PACKAGE_ORDER_UNIT_QUANTITY>2</PACKAGE_ORDER_UNIT_QUANTITY>
					</PACKAGE>
				</PACKAGE_INFO>
			</LOGISTIC_DETAILS>
		</DISPATCHNOTIFICATION_ITEM>
		<DISPATCHNOTIFICATION_ITEM>
			<PRODUCT_ID>
				<SUPPLIER_PID xmlns="http://www.bmecat.org/bmecat/2005">A475-177</SUPPLIER_PID>
				<INTERNATIONAL_PID xmlns="http://www.bmecat.org/bmecat/2005">09783404658123</INTERNATIONAL_PID>
				<BUYER_PID xmlns="http://www.bmecat.org/bmecat/2005">6406987</BUYER_PID>
			</PRODUCT_ID>
			<QUANTITY>5</QUANTITY>
			<ORDER_REFERENCE>
				<ORDER_ID>9316271</ORDER_ID>
			</ORDER_REFERENCE>
			<LOGISTIC_DETAILS>
				<PACKAGE_INFO>
					<PACKAGE>
						<PACKAGE_ID>00001234560000000028</PACKAGE_ID>
						<PACKAGE_ORDER_UNIT_QUANTITY>3</PACKAGE_ORDER_UNIT_QUANTITY>
					</PACKAGE>
					<PACKAGE>
						<PACKAGE_ID>00001234560000000038</PACKAGE_ID>
						<PACKAGE_ORDER_UNIT_QUANTITY>2</PACKAGE_ORDER_UNIT_QUANTITY>
					</PACKAGE>
				</PACKAGE_INFO>
			</LOGISTIC_DETAILS>
		</DISPATCHNOTIFICATION_ITEM>
	</DISPATCHNOTIFICATION_ITEM_LIST>
</DISPATCHNOTIFICATION>
```
|     |     |     |     |     |
| --- | --- | --- | --- | --- |
| **XML Element** | **M/C/S** | **Sample values** | [**Data type**](https://confdg.atlassian.net/wiki/spaces/PI/pages/168689833771 "https://confdg.atlassian.net/wiki/spaces/PI/pages/168689833771") **\[maxLength\]** | **Description** |
| DISPATCHNOTIFICATION | Must |     |     | See [Namespaces](https://confdg.atlassian.net/wiki/spaces/PI/pages/168689833747 "https://confdg.atlassian.net/wiki/spaces/PI/pages/168689833747") regarding the correct usage of namespaces. |
| . DISPATCHNOTIFICATION\_HEADER | Must |     |     |     |
| . . CONTROL\_INFO | Must |     |     |     |
| . . . GENERATION\_DATE | Must | 2019-01-14T17:05:34 | dtDATETIME | Document creation timestamp |
| . . DISPATCHNOTIFICATION\_INFO | Must |     |     |     |
| . . . DISPATCHNOTIFICATION\_ID | Must | 1172016120104084 | dtSTRING\[250\] | ![Warning](https://pf-emoji-service--cdn.us-east-1.prod.public.atl-paas.net/atlassian/productivityEmojis/exclamation-32px.png) Reference of the [paper delivery note](https://confdg.atlassian.net/wiki/spaces/PH/pages/170305294741 "https://confdg.atlassian.net/wiki/spaces/PH/pages/170305294741") (delivery note number) - must be unique and not repeated |
| . . . DISPATCHNOTIFICATION\_DATE | Must | 2017-06-14T17:15:01 | dtDATETIME | Dispatch date |
| . . . PARTIES | Must |     |     |     |
| . . . . PARTY | Must |     |     |     |
| . . . . . PARTY\_ROLE | Must | delivery | dtSTRING\[20\] | Recipient of delivery |
| . . . . . ADDRESS | Must |     |     | The element ADDRESS in the  PARTY\_ROLE **delivery** must be present, even without any sub-elements. |
| . . . . . . NAME | Should | Mustermann GmbH | dtMLSTRING\[50\]<br><br>**BMEcat NS** | Company name  <br>![Warning](https://pf-emoji-service--cdn.us-east-1.prod.public.atl-paas.net/atlassian/productivityEmojis/exclamation-32px.png) The XML entities `&amp;`, `&lt;`, `&gt;`, `&apos;` and `&quot;` must be escaped |
| . . . . . . NAME3 | Should | Block A | dtMLSTRING\[50\]<br><br>**BMEcat NS** | Building |
| . . . . . . DEPARTMENT | Should | Accounting | dtMLSTRING\[50\]<br><br>**BMEcat NS** | Department |
| . . . . . . CONTACT\_DETAILS | Can | \-  | \-  |     |
| . . . . . . . TITLE | Should | Mr., Ms. | dtMLSTRING\[20\]<br><br>**BMEcat NS** | Salutation |
| . . . . . . . FIRST\_NAME | Should | Ulla | dtMLSTRING\[50\]<br><br>**BMEcat NS** | First name |
| . . . . . . . CONTACT\_NAME | Should | Mustermann | dtMLSTRING\[50\]<br><br>**BMEcat NS** | Last name |
| . . . . . . STREET | Should | Musterstrasse 200b | dtMLSTRING\[50\]<br><br>**BMEcat NS** | Street incl. No. |
| . . . . . . ZIP | Should | 8000 | dtMLSTRING\[20\]<br><br>**BMEcat NS** | ZIP code of address or post office box |
| . . . . . . BOXNO | Should | 7   | dtMLSTRING\[20\]<br><br>**BMEcat NS** | P.O. Box number |
| . . . . . . CITY | Should | Zürich | dtMLSTRING\[50\]<br><br>**BMEcat NS** | City |
| . . . . . . COUNTRY | Should | Schweiz | dtMLSTRING\[50\]<br><br>**BMEcat NS** | Country |
| . . . . . . COUNTRY\_CODED | Can | CH, DE, AT | dtCOUNTRIES<br><br>**BMEcat NS** | Country code according ISO 3166-1 ALPHA-2 |
| . . . SHIPMENT\_ID | Should \* | 99.00.123456.12345678,  <br>1234.123.123456  <br>per Briefpost,  <br>per Spedition | dtSTRING\[250\] | \* = **Must** for direct deliveries to end customers:<br><br>*   Consignment number/package number of the shipping service provider for tracking purposes  <br>    ![light bulb](https://pf-emoji-service--cdn.us-east-1.prod.public.atl-paas.net/standard/ef8b0642-7523-4e13-9fd3-01b65648acf6/32x32/1f4a1.png) If possible for parcels to the warehouse, if the consignment number is available as a barcode (even if SSCC has not been implemented), transmit it here<br>    <br>*   In the case of goods shipment, the shipment number/operation number of the freight forwarder should be transmitted, even if no shipment tracking is possible via this number<br>    <br>*   If the shipment is made by letter post, then _«per Briefpost»_ |
| . . . SHIPMENT\_CARRIER | Should \* | swisspost, postlogistics, dhl, dhlfreight, planzer, streck-de, hermes, tnt, dpd, ups, gls, fedex, dachser, transoflex, gebruederweiss, galliker, schoeni, quickpac, austria-post, dsv, schenker, sidler, noerpel, emons, hellmann, asendia, amm, camiontransport, fba, chronopost, pickwings, papyrus | dtSTRING\[50\] | \* = **Must** for direct deliveries to end customers  <br>![Question Mark](https://pf-emoji-service--cdn.us-east-1.prod.public.atl-paas.net/atlassian/productivityEmojis/question-32px.png) Currently, only the values listed under sample values are permitted. If a different logistics service provider is used, the responsible Integration Manager needs to be contacted.<br><br>Attention: please note the following for Planzer as a shipping service provider:<br><br>![warning](https://pf-emoji-service--cdn.us-east-1.prod.public.atl-paas.net/standard/ef8b0642-7523-4e13-9fd3-01b65648acf6/32x32/26a0.png) Warning In the case of Planzer, we need the eight-digit shipment number as SHIPMENT\_ID (not the package number). Example:<br><br><SHIPMENT\_ID>40172372</SHIPMENT\_ID>  <br><SHIPMENT\_CARRIER>planzer</SHIPMENT\_CARRIER> |
| . DISPATCHNOTIFICATION\_ITEM\_LIST | Must |     |     |     |
| . . DISPATCHNOTIFICATION\_ITEM | Must |     |     |     |
| . . . PRODUCT\_ID | Must |     |     |     |
| . . . . SUPPLIER\_PID | Must | A375-129 | dtSTRING\[50\]<br><br>**BMEcat NS** | Partner product key |
| . . . . INTERNATIONAL\_PID | Must | 09783404175109 | dtSTRING\[14\]<br><br>**BMEcat NS** | GTIN-14 with leading zeros |
| . . . . BUYER\_PID | Can | 6406567 | dtSTRING\[50\]<br><br>**BMEcat NS** | Galaxus product key |
| . . . . SERIAL\_NUMBER | Can | 0021010000 | dtSTRING\[50\]<br><br>**BMEcat NS** | Serial number: Unique identification of a single product (product instance).  <br>The element can occur accordingly several times for QUANTITY > 1. |
| . . . QUANTITY | Must | 5   | dtNUMBER | Quantity (Integer > 0) |
| . . . ORDER\_REFERENCE | Must |     |     |     |
| . . . . ORDER\_ID | Must | 9316271 | dtSTRING\[25\] | Reference Order ID – indicates the corresponding order for the delivery |
| . . . LOGISTIC\_DETAILS | Should \* |     |     | **Warehouse deliveries (supplier)**  <br>\* = If this element can be sent, its sub-elements are must fields and the conditions from [Incoming goods automation with SSCC](https://confdg.atlassian.net/wiki/spaces/PI/pages/168693622611 "https://confdg.atlassian.net/wiki/spaces/PI/pages/168693622611") must be met.<br><br>**Direct deliveries (supplier & merchant)**  <br>\* = Basically this element and its sub-elements should not be sent. However, it can be used to specify different tracking codes per item and piece. |
| . . . . PACKAGE\_INFO | Should \* |     |     |     |
| . . . . . PACKAGE | Should \* |     |     |     |
| . . . . . . PACKAGE\_ID | Should \* | 00001234560000000028, 99.00.123456.12345678 | dtSTRING\[50\] | **Best**: SSCC  <br>**Minimum requirement**: barcode value present on a label on the pallet or package  <br>Compatible code type: **GS1-128**  <br>The scannable code must correspond exactly to the PACKAGE\_ID  <br>The value must be unique and must not be repeated within one year<br><br>![Warning](https://pf-emoji-service--cdn.us-east-1.prod.public.atl-paas.net/atlassian/productivityEmojis/exclamation-32px.png) For direct deliveries (if LOGISTIC\_DETAILS are sent), the consignment number must be transmitted here  <br>![light bulb](https://pf-emoji-service--cdn.us-east-1.prod.public.atl-paas.net/standard/ef8b0642-7523-4e13-9fd3-01b65648acf6/32x32/1f4a1.png) Several PACKAGE elements can be used to transmit different consignment numbers (parcels) in one DELR |
| . . . . . . PACKAGE\_ORDER\_UNIT\_QUANTITY | Should \* | 3   | dtNUMBER | Number of pieces of an item available in the package / on the pallet |