this file contains the information of the Invoices 
first the Naming of the file it will as this 

GINVO_<SupplierId>_<OrderId>_<*>_<Timestamp>.xml

the * mean the doucment number 
like this GINVO_<SupplierId>_71696740_RG7654321_202103311537.xml

next we have the strucure of the file will be like this ![Image](<../Images/Order/invoices.png>)

now to disuss the structure 
first is the heading as mentioned before

second we have InvoiceInfo that carry the ids of the invoice, date, deliveres
then the date and the party section

then we have the remarks type=qrr
this the qr refrence in the invoice
 type=qriban for the ame
 type=scor this is for international payment transactions

then the currency like euro, dollar but in enumration shape

then the order history

then the Invoices Item list
 for this each invoice item contain
1. the product details
2. quantity
3. product price fix (before tax and tax and total)

then the delivery details


next we have the Invoice summary
1. net value goods
2. total amount
3. Allow or chanrges fix
    * Only the type «surcharge» for surcharges added to the invoice can be used (excl. VAT)
    * this has value like allow_or_charge_type that enum between Allowed values (in lower case) are:

      * express

      * freight

      * handling

      * insurance

      * small_order
    * Allow or change value
       *  amount
    * Total tax
      * tax details
        * tax percent
        * amount


this an example of the xml 
```xml
<?xml version="1.0" encoding="utf-8"?>
<INVOICE xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns="http://www.opentrans.org/XMLSchema/2.1" version="2.1">
	<INVOICE_HEADER>
		<CONTROL_INFO>
			<GENERATION_DATE>2017-10-09T03:26:39</GENERATION_DATE>
		</CONTROL_INFO>
		<INVOICE_INFO>
			<INVOICE_ID>65496816584</INVOICE_ID>
			<INVOICE_DATE>2017-10-09T03:26:39</INVOICE_DATE>
			<DELIVERYNOTE_ID>11720161201040841</DELIVERYNOTE_ID>
			<DELIVERY_DATE>
				<DELIVERY_START_DATE>2017-10-11</DELIVERY_START_DATE>
				<DELIVERY_END_DATE>2017-10-11</DELIVERY_END_DATE>
			</DELIVERY_DATE>
			<PARTIES>
				<PARTY>
					<PARTY_ROLE>buyer</PARTY_ROLE>
					<ADDRESS>
						<NAME xmlns="http://www.bmecat.org/bmecat/2005">Digitec Galaxus AG</NAME>
						<STREET xmlns="http://www.bmecat.org/bmecat/2005">Pfingsweidstrasse 60b</STREET>
						<ZIP xmlns="http://www.bmecat.org/bmecat/2005">8005</ZIP>
						<CITY xmlns="http://www.bmecat.org/bmecat/2005">Zürich</CITY>
						<COUNTRY xmlns="http://www.bmecat.org/bmecat/2005">Schweiz</COUNTRY>
					</ADDRESS>
				</PARTY>
				<PARTY>
					<PARTY_ROLE>invoice_issuer</PARTY_ROLE>
					<ADDRESS>
						<NAME xmlns="http://www.bmecat.org/bmecat/2005">Supplier company name</NAME>
						<STREET xmlns="http://www.bmecat.org/bmecat/2005">Supplier street</STREET>
						<ZIP xmlns="http://www.bmecat.org/bmecat/2005">8000</ZIP>
						<CITY xmlns="http://www.bmecat.org/bmecat/2005">Supplier city</CITY>
						<COUNTRY xmlns="http://www.bmecat.org/bmecat/2005">Schweiz</COUNTRY>
						<VAT_ID xmlns="http://www.bmecat.org/bmecat/2005">CHE-123.456.789 MWST</VAT_ID>
					</ADDRESS>
				</PARTY>
				<PARTY>
					<PARTY_ROLE>delivery</PARTY_ROLE>
					<ADDRESS>
						<NAME xmlns="http://www.bmecat.org/bmecat/2005">Mustermann GmbH</NAME>
						<NAME2 xmlns="http://www.bmecat.org/bmecat/2005">Ulla Mustermann</NAME2>
						<CONTACT_DETAILS>
							<FIRST_NAME xmlns="http://www.bmecat.org/bmecat/2005">Ulla</FIRST_NAME>
							<CONTACT_NAME xmlns="http://www.bmecat.org/bmecat/2005">Mustermann</CONTACT_NAME>
						</CONTACT_DETAILS>
						<STREET xmlns="http://www.bmecat.org/bmecat/2005">Musterstrasse 200b</STREET>
						<ZIP xmlns="http://www.bmecat.org/bmecat/2005">8000</ZIP>
						<CITY xmlns="http://www.bmecat.org/bmecat/2005">Zürich</CITY>
						<COUNTRY xmlns="http://www.bmecat.org/bmecat/2005">Schweiz</COUNTRY>
					</ADDRESS>
				</PARTY>
			</PARTIES>
			<REMARKS type="qrr">949643000000081595108367731</REMARKS>
			<REMARKS type="qriban">CH1230808001234567890</REMARKS>
			<CURRENCY xmlns="http://www.bmecat.org/bmecat/2005">CHF</CURRENCY>
		</INVOICE_INFO>
		<ORDER_HISTORY>
			<ORDER_ID>14609982</ORDER_ID>
			<SUPPLIER_ORDER_ID>B2393234</SUPPLIER_ORDER_ID>
		</ORDER_HISTORY>
	</INVOICE_HEADER>
	<INVOICE_ITEM_LIST>
		<INVOICE_ITEM>
			<PRODUCT_ID>
				<SUPPLIER_PID xmlns="http://www.bmecat.org/bmecat/2005">26355917130453</SUPPLIER_PID>
				<INTERNATIONAL_PID xmlns="http://www.bmecat.org/bmecat/2005">05052197017458</INTERNATIONAL_PID>
				<BUYER_PID xmlns="http://www.bmecat.org/bmecat/2005">5952921</BUYER_PID>
			</PRODUCT_ID>
			<QUANTITY>2</QUANTITY>
			<PRODUCT_PRICE_FIX>
				<PRICE_AMOUNT xmlns="http://www.bmecat.org/bmecat/2005">31.3</PRICE_AMOUNT>
				<TAX_DETAILS_FIX>
					<TAX xmlns="http://www.bmecat.org/bmecat/2005">0.081</TAX>
					<TAX_AMOUNT>5.07</TAX_AMOUNT>
				</TAX_DETAILS_FIX>
			</PRODUCT_PRICE_FIX>
			<PRICE_LINE_AMOUNT>62.6</PRICE_LINE_AMOUNT>
			<ORDER_REFERENCE>
				<ORDER_ID>14609982</ORDER_ID>
			</ORDER_REFERENCE>
			<DELIVERY_REFERENCE>
				<DELIVERYNOTE_ID>11720161201040841</DELIVERYNOTE_ID>
				<DELIVERY_DATE>
					<DELIVERY_START_DATE>2017-10-11</DELIVERY_START_DATE>
					<DELIVERY_END_DATE>2017-10-11</DELIVERY_END_DATE>
				</DELIVERY_DATE>
			</DELIVERY_REFERENCE>
		</INVOICE_ITEM>
	</INVOICE_ITEM_LIST>
	<INVOICE_SUMMARY>
		<NET_VALUE_GOODS>62.6</NET_VALUE_GOODS>
		<TOTAL_AMOUNT>78.48</TOTAL_AMOUNT>
		<ALLOW_OR_CHARGES_FIX>
			<ALLOW_OR_CHARGE type="surcharge">
				<ALLOW_OR_CHARGE_TYPE>freight</ALLOW_OR_CHARGE_TYPE>
				<ALLOW_OR_CHARGE_VALUE>
					<AOC_MONETARY_AMOUNT>10</AOC_MONETARY_AMOUNT>
				</ALLOW_OR_CHARGE_VALUE>
			</ALLOW_OR_CHARGE>
			<ALLOW_OR_CHARGES_TOTAL_AMOUNT>10</ALLOW_OR_CHARGES_TOTAL_AMOUNT>
		</ALLOW_OR_CHARGES_FIX>
		<TOTAL_TAX>
			<TAX_DETAILS_FIX>
				<TAX xmlns="http://www.bmecat.org/bmecat/2005">0.081</TAX>
				<TAX_AMOUNT>5.88</TAX_AMOUNT>
			</TAX_DETAILS_FIX>
		</TOTAL_TAX>
	</INVOICE_SUMMARY>
</INVOICE>
```

|     |     |     |     |     |
| --- | --- | --- | --- | --- |
| **XML Element** | **M/C/S** | **Sample values** | [**Data type**](https://confdg.atlassian.net/wiki/spaces/PI/pages/168689833771 "https://confdg.atlassian.net/wiki/spaces/PI/pages/168689833771") **\[maxLength\]** | **Description** |
| INVOICE | Must | \-  | \-  | See [Namespaces](https://confdg.atlassian.net/wiki/spaces/PI/pages/168689833747 "https://confdg.atlassian.net/wiki/spaces/PI/pages/168689833747") regarding the correct usage of namespaces. |
| . INVOICE\_HEADER | Must | \-  | \-  |     |
| . . CONTROL\_INFO | Can | \-  | \-  |     |
| . . . GENERATION\_DATE | Can | 2017-10-09T03:26:39 | dtDATETIME | Document creation timestamp |
| . . INVOICE\_INFO | Must | \-  | \-  |     |
| . . . INVOICE\_ID | Must | 65496816584 | dtSTRING\[250\] | Unique Invoice number |
| . . . INVOICE\_DATE | Must | 2017-10-09T03:26:39 | dtDATETIME | Invoice date |
| . . . DELIVERYNOTE\_ID | Must \*  <br>(see ![balance scale](https://pf-emoji-service--cdn.us-east-1.prod.public.atl-paas.net/standard/ef8b0642-7523-4e13-9fd3-01b65648acf6/32x32/2696.png) [Info](https://confdg.atlassian.net/wiki/spaces/PI/pages/168689834002/Invoice+INVO#VAT-compliance "https://confdg.atlassian.net/wiki/spaces/PI/pages/168689834002/Invoice+INVO#VAT-compliance")) | 11720161201040841 | dtSTRING\[250\] | Delivery note reference |
| . . . DELIVERY\_DATE | Must \*  <br>(see ![balance scale](https://pf-emoji-service--cdn.us-east-1.prod.public.atl-paas.net/standard/ef8b0642-7523-4e13-9fd3-01b65648acf6/32x32/2696.png) [Info](https://confdg.atlassian.net/wiki/spaces/PI/pages/168689834002/Invoice+INVO#VAT-compliance "https://confdg.atlassian.net/wiki/spaces/PI/pages/168689834002/Invoice+INVO#VAT-compliance")) | \-  | \-  |     |
| . . . . DELIVERY\_START\_DATE | Must \*  <br>(see ![balance scale](https://pf-emoji-service--cdn.us-east-1.prod.public.atl-paas.net/standard/ef8b0642-7523-4e13-9fd3-01b65648acf6/32x32/2696.png) [Info](https://confdg.atlassian.net/wiki/spaces/PI/pages/168689834002/Invoice+INVO#VAT-compliance "https://confdg.atlassian.net/wiki/spaces/PI/pages/168689834002/Invoice+INVO#VAT-compliance")) | 2017-10-11 | dtDATETIME | Delivery date |
| . . . . DELIVERY\_END\_DATE | Must \*  <br>(see ![balance scale](https://pf-emoji-service--cdn.us-east-1.prod.public.atl-paas.net/standard/ef8b0642-7523-4e13-9fd3-01b65648acf6/32x32/2696.png) [Info](https://confdg.atlassian.net/wiki/spaces/PI/pages/168689834002/Invoice+INVO#VAT-compliance "https://confdg.atlassian.net/wiki/spaces/PI/pages/168689834002/Invoice+INVO#VAT-compliance")) | 2017-10-11 | dtDATETIME | Delivery date |
| . . . PARTIES | Must | \-  | \-  |     |
| . . . . PARTY | Must | \-  | \-  |     |
| . . . . . PARTY\_ROLE | Must | buyer, invoice\_issuer\*, delivery | dtSTRING\[20\] | buyer = buyer  <br>invoice\_issuer = invoice issuer (\*mandatory)  <br>delivery = recipient of goods |
| . . . . . ADDRESS | Must | \-  | \-  | The element ADDRESS in the  PARTY\_ROLE **delivery** must be present, even without any sub-elements. |
| . . . . . . NAME | Should | Mustermann GmbH | dtMLSTRING\[50\]<br><br>**BMEcat NS** | Company name |
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
| . . . . . . VAT\_ID | Must | CHE-123.456.789 MWST | dtSTRING\[50\]<br><br>**BMEcat NS** | Company identification number (UID)  <br>Sales tax identification number (USt-ID) in Germany<br><br>is mandatory for PARTY\_ROLE=invoice\_issuer to be VAT compliant  <br>see also our payment information: [https://confdg.atlassian.net/wiki/spaces/PH/pages/169096677378](https://confdg.atlassian.net/wiki/spaces/PH/pages/169096677378) |
| . . . REMARKS type="qrr" | Can | 949643000000081595108367731 | typeMLSTRING64000\[27\] | **Only for payment transactions in CH**  <br>QR reference number for QR invoices  <br>(Total of 27 characters)  <br>Also compare [payment standards p. 13 and 53](https://www.six-group.com/dam/download/banking-services/standardization/qr-bill/ig-qr-bill-v2.2-en.pdf "https://www.six-group.com/dam/download/banking-services/standardization/qr-bill/ig-qr-bill-v2.2-en.pdf") |
| . . . REMARKS type="qriban" | Can | CH1233000001234567890 | typeMLSTRING64000\[21\] | **Only for payment transactions in CH**  <br>QR-IBAN for QR invoices (specific QR account of the bank only for incoming payments)  <br>Requirement: QR-IID in the range 30000 - 31999 (positions 5-9)  <br>See [payment standard](https://www.six-group.com/dam/download/banking-services/standardization/qr-bill/ig-qr-bill-v2.2-en.pdf "https://www.six-group.com/dam/download/banking-services/standardization/qr-bill/ig-qr-bill-v2.2-en.pdf") page 13: 2.11 QR-IBAN |
| . . . REMARKS type="scor" | Can | RF18000000000539007547034 | typeMLSTRING64000\[25\] | **For international payment transactions**  <br>Structured Creditor Reference (SCOR) acording to ISO 11649  <br>![Warning](https://pf-emoji-service--cdn.us-east-1.prod.public.atl-paas.net/atlassian/productivityEmojis/exclamation-32px.png) Cannot be used in combination with QR-IBAN cf. also [payment standards p. 36 and 48](https://www.six-group.com/dam/download/banking-services/standardization/qr-bill/ig-qr-bill-v2.2-en.pdf "https://www.six-group.com/dam/download/banking-services/standardization/qr-bill/ig-qr-bill-v2.2-en.pdf") |
| . . . CURRENCY | Must | CHF, EUR | dtCURRENCIES\[3\]<br><br>**BMEcat NS** | Transaction currency according to ISO 4217:1995 |
| . . ORDER\_HISTORY | Must \* | \-  | \-  | \* = Mandatory if ALLOW\_OR\_CHARGES\_FIX is used |
| . . . ORDER\_ID | Must \* | 14609982 | dtSTRING\[25\] | Galaxus purchase Order ID of the [Order Placement](https://confdg.atlassian.net/wiki/spaces/PI/pages/168689833798 "https://confdg.atlassian.net/wiki/spaces/PI/pages/168689833798")<br><br>In the case of collective invoices, one of the affected order numbers must be specified, e.g. the first |
| . . . SUPPLIER\_ORDER\_ID | Can | B2393234 | dtSTRING\[250\] | Order ID from your ERP |
| . INVOICE\_ITEM\_LIST | Must | \-  | \-  |     |
| . . INVOICE\_ITEM | Must | \-  | \-  |     |
| . . . PRODUCT\_ID | Must | \-  | \-  |     |
| . . . . SUPPLIER\_PID | Must | 26355917130453 | dtSTRING\[50\]<br><br>**BMEcat NS** | Partner product key |
| . . . . INTERNATIONAL\_PID | Must | 05052197017458 | dtSTRING\[14\]<br><br>**BMEcat NS** | GTIN-14 with leading zeros |
| . . . . BUYER\_PID | Can | 5952921 | dtSTRING\[50\]<br><br>**BMEcat NS** | Galaxus product key |
| . . . QUANTITY | Must | 2   | dtNUMBER | Item position quantity |
| . . . PRODUCT\_PRICE\_FIX | Must | \-  | \-  |     |
| . . . . PRICE\_AMOUNT | Must | 31.30 | dtNUMBER<br><br>**BMEcat NS** | Unit price excl. VAT incl. eventual recycling fees (e.g. vRG / WEEE) or position discounts |
| . . . . TAX\_DETAILS\_FIX | Must | \-  | \-  |     |
| . . . . . TAX | Must | 0.081, 0.19 | dtNUMBER<br><br>**BMEcat NS** | VAT-rate as decimal number |
| . . . . . TAX\_AMOUNT | Should | 4.82 | dtNUMBER | VAT amount \* quantity |
| . . . PRICE\_LINE\_AMOUNT | Must | 62.60 | dtNUMBER | Net price for position:  <br>Quantity \* Unit Price excl. VAT plus eventual recycling fees (e.g. vRG / WEEE) or rebates  <br>\= QUANTITY \* PRICE\_AMOUNT |
| . . . ORDER\_REFERENCE | Must | \-  | \-  |     |
| . . . . ORDER\_ID | Must | 14609982 | dtSTRING\[250\] | Galaxus Purchase Order ID |
| . . . DELIVERY\_REFERENCE | Must \*  <br>(see ![balance scale](https://pf-emoji-service--cdn.us-east-1.prod.public.atl-paas.net/standard/ef8b0642-7523-4e13-9fd3-01b65648acf6/32x32/2696.png) [Info](https://confdg.atlassian.net/wiki/spaces/PI/pages/168689834002/Invoice+INVO#VAT-compliance "https://confdg.atlassian.net/wiki/spaces/PI/pages/168689834002/Invoice+INVO#VAT-compliance")) | \-  | \-  |     |
| . . . . DELIVERYNOTE\_ID | Must \*  <br>(see ![balance scale](https://pf-emoji-service--cdn.us-east-1.prod.public.atl-paas.net/standard/ef8b0642-7523-4e13-9fd3-01b65648acf6/32x32/2696.png) [Info](https://confdg.atlassian.net/wiki/spaces/PI/pages/168689834002/Invoice+INVO#VAT-compliance "https://confdg.atlassian.net/wiki/spaces/PI/pages/168689834002/Invoice+INVO#VAT-compliance")) | 11720161201040841 | dtSTRING\[250\] | Delivery note reference |
| . . . . DELIVERY\_DATE | Must \*  <br>(see ![balance scale](https://pf-emoji-service--cdn.us-east-1.prod.public.atl-paas.net/standard/ef8b0642-7523-4e13-9fd3-01b65648acf6/32x32/2696.png) [Info](https://confdg.atlassian.net/wiki/spaces/PI/pages/168689834002/Invoice+INVO#VAT-compliance "https://confdg.atlassian.net/wiki/spaces/PI/pages/168689834002/Invoice+INVO#VAT-compliance")) | \-  | \-  |     |
| . . . . . DELIVERY\_START\_DATE | Must \*  <br>(see ![balance scale](https://pf-emoji-service--cdn.us-east-1.prod.public.atl-paas.net/standard/ef8b0642-7523-4e13-9fd3-01b65648acf6/32x32/2696.png) [Info](https://confdg.atlassian.net/wiki/spaces/PI/pages/168689834002/Invoice+INVO#VAT-compliance "https://confdg.atlassian.net/wiki/spaces/PI/pages/168689834002/Invoice+INVO#VAT-compliance")) | 2017-10-11 | dtDATETIME | Delivery date |
| . . . . . DELIVERY\_END\_DATE | Must \*  <br>(see ![balance scale](https://pf-emoji-service--cdn.us-east-1.prod.public.atl-paas.net/standard/ef8b0642-7523-4e13-9fd3-01b65648acf6/32x32/2696.png) [Info](https://confdg.atlassian.net/wiki/spaces/PI/pages/168689834002/Invoice+INVO#VAT-compliance "https://confdg.atlassian.net/wiki/spaces/PI/pages/168689834002/Invoice+INVO#VAT-compliance")) | 2017-10-11 | dtDATETIME | Delivery date |
| . INVOICE\_SUMMARY | Must | \-  | \-  |     |
| . . NET\_VALUE\_GOODS | Must | 62.60 | dtNUMBER | Total net sum of invoice for all positions excl. VAT. without additional fees like freight etc. |
| . . TOTAL\_AMOUNT | Must | 78.20 | dtNUMBER | Invoice Total - Sum of all Price Line Amounts and additional fees (freight, recycling fees etc.) incl. VAT |
| . . ALLOW\_OR\_CHARGES\_FIX | Can | \-  | \-  | Block for charging delivery fees / surcharges (according to the agreed delivery cost model) |
| . . . ALLOW\_OR\_CHARGE | Can | \-  | \-  | Only the type «surcharge» for surcharges added to the invoice can be used (excl. VAT) |
| . . . . ALLOW\_OR\_CHARGE\_TYPE | Can | freight | dtSTRING\[30\] | Allowed values (in lower case) are:<br><br>*   **express**<br>    <br>*   **freight**<br>    <br>*   **handling**<br>    <br>*   **insurance**<br>    <br>*   **small\_order**<br>    <br><br>Recycling fees must be included in the purchase price PRICE\_AMOUNT in each position. |
| . . . . ALLOW\_OR\_CHARGE\_VALUE | Can | \-  | \-  |     |
| . . . . . AOC\_MONETARY\_AMOUNT | Can | 10.00 | dtFLOAT | The surcharge is specified by an absolute value excl. VAT.  <br>The currency of this value corresponds to the currency of the product price. |
| . . . ALLOW\_OR\_CHARGES\_TOTAL\_AMOUNT | Can | 10.00 | dtFLOAT | Sum of all surcharges (excl. VAT):<br><br>Within the calculation, all surcharges are considered, which contain a money amount in ALLOW\_OR\_CHARGE\_VALUE/AOC\_MONETARY\_AMOUNT |
| . . TOTAL\_TAX | Should | \-  | \-  |     |
| . . . TAX\_DETAILS\_FIX | Should | \-  | \-  | This element and its child-elements are iterated, if INVOICE\_ITEMs with different VAT rates are present |
| . . . . TAX | Should | 0.081, 0.19 | dtNUMBER<br><br>**BMEcat NS** | VAT rate |
| . . . . TAX\_AMOUNT | Should | 5.59 | dtNUMBER | VAT amount of all positions and fees (freight, recycling fees etc.) |