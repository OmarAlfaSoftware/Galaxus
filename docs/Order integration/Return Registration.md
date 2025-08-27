this file contains the return registeration request details

this mean that after the user recive the file he needs to return this order so he sends a requests so we can see how it will work

so this file will exists in the dg2partner folder

and will be named like this GRETP_SupplierId_OrderId_Timestamp.xml

and the structure of this file will be like this 
![image](<../Images/Order/registred return.png>)

the header disccused earlier

now for registration header that carry the order id and the reruen registred id and the date

then the language and the parties

then the order parties refernce that carry the buyer and supplier identification 

then the shipment id and tracking tracing url

then the registertion list 

each item carry the product details 

then the return reason for it

after that the total number of units returned

this an example of the XML
```
<?xml version="1.0" encoding="utf-8"?>
<RETURNREGISTRATION xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns="http://www.opentrans.org/XMLSchema/2.1" version="2.1">
	<RETURNREGISTRATION_HEADER>
		<RETURNREGISTRATION_INFO>
			<ORDER_ID>9316271</ORDER_ID>
			<RETURNREGISTRATION_ID>67773882</RETURNREGISTRATION_ID>
			<RETURNREGISTRATION_DATE>2017-06-13T15:49:49</RETURNREGISTRATION_DATE>
			<LANGUAGE xmlns="http://www.bmecat.org/bmecat/2005">ger</LANGUAGE>
			<PARTIES>
				<PARTY>
					<PARTY_ID xmlns="http://www.bmecat.org/bmecat/2005" type="buyer_specific">2045638</PARTY_ID>
					<PARTY_ROLE>buyer</PARTY_ROLE>
					<ADDRESS>
						<CONTACT_DETAILS>
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
					<PARTY_ID xmlns="http://www.bmecat.org/bmecat/2005" type="buyer_specific">1928008</PARTY_ID>
					<PARTY_ROLE>supplier</PARTY_ROLE>
					<ADDRESS>
						<NAME xmlns="http://www.bmecat.org/bmecat/2005">Musterfirma AG</NAME>
						<STREET xmlns="http://www.bmecat.org/bmecat/2005">Teststrasse 112</STREET>
						<ZIP xmlns="http://www.bmecat.org/bmecat/2005">8000</ZIP>
						<CITY xmlns="http://www.bmecat.org/bmecat/2005">Zürich</CITY>
						<COUNTRY xmlns="http://www.bmecat.org/bmecat/2005">Schweiz</COUNTRY>
						<COUNTRY_CODED xmlns="http://www.bmecat.org/bmecat/2005">CH</COUNTRY_CODED>
						<EMAIL xmlns="http://www.bmecat.org/bmecat/2005">noreply@galaxus.ch</EMAIL>
					</ADDRESS>
				</PARTY>
				<PARTY>
					<PARTY_ID xmlns="http://www.bmecat.org/bmecat/2005" type="buyer_specific">2045638</PARTY_ID>
					<PARTY_ROLE>delivery</PARTY_ROLE>
					<ADDRESS>
						<CONTACT_DETAILS>
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
			</PARTIES>
			<ORDER_PARTIES_REFERENCE>
				<BUYER_IDREF xmlns="http://www.bmecat.org/bmecat/2005" type="buyer_specific">2045638</BUYER_IDREF>
				<SUPPLIER_IDREF xmlns="http://www.bmecat.org/bmecat/2005" type="buyer_specific">1928008</SUPPLIER_IDREF>
			</ORDER_PARTIES_REFERENCE>
			<SHIPMENT_ID>996011434800119018</SHIPMENT_ID>
			<TRACKING_TRACING_URL>https://service.post.ch/EasyTrack/submitParcelData.do?formattedParcelCodes=996011434800119018</TRACKING_TRACING_URL>
		</RETURNREGISTRATION_INFO>
	</RETURNREGISTRATION_HEADER>
	<RETURNREGISTRATION_ITEM_LIST>
		<RETURNREGISTRATION_ITEM>
			<LINE_ITEM_ID>1</LINE_ITEM_ID>
			<PRODUCT_ID>
				<SUPPLIER_PID xmlns="http://www.bmecat.org/bmecat/2005" type="supplierProductKey">A375-129</SUPPLIER_PID>
				<INTERNATIONAL_PID xmlns="http://www.bmecat.org/bmecat/2005" type="gtin">09783404175109</INTERNATIONAL_PID>
				<BUYER_PID xmlns="http://www.bmecat.org/bmecat/2005" type="DgProductId">6406567</BUYER_PID>
			</PRODUCT_ID>
			<QUANTITY>2</QUANTITY>
			<ORDER_UNIT xmlns="http://www.bmecat.org/bmecat/2005">C62</ORDER_UNIT>
			<RETURNREASON>2</RETURNREASON>
		</RETURNREGISTRATION_ITEM>
	</RETURNREGISTRATION_ITEM_LIST>
	<RETURNREGISTRATION_SUMMARY>
		<TOTAL_ITEM_NUM>2</TOTAL_ITEM_NUM>
	</RETURNREGISTRATION_SUMMARY>
</RETURNREGISTRATION>
```

|     |     |     |     |     |
| --- | --- | --- | --- | --- |
| **XML Element** | **M/C/S** | **Sample values** | [**Data type**](https://confdg.atlassian.net/wiki/spaces/PI/pages/168689833771 "https://confdg.atlassian.net/wiki/spaces/PI/pages/168689833771") **\[maxLength\]** | **Description** |
| RETURNREGISTRATION | Must | \-  | \-  | See [Namespaces](https://confdg.atlassian.net/wiki/spaces/PI/pages/168689833747 "https://confdg.atlassian.net/wiki/spaces/PI/pages/168689833747") regarding the correct usage of namespaces. |
| . RETURNREGISTRATION\_HEADER | Must | \-  | \-  |     |
| . . RETURNREGISTRATION\_INFO | Must | \-  | \-  |     |
| . . . ORDER\_ID | Must | 9316271 | dtSTRING\[25\] | Galaxus Purchase Order ID |
| . . . RETURNREGISTRATION\_ID | Must | 67773882 | dtSTRING\[250\] | Return Registration ID |
| . . . RETURNREGISTRATION\_DATE | Must | 2017-06-13T15:49:49 | dtDATETIME | Return Registration timestamp |
| . . . LANGUAGE | Must | ger, eng, fra, ita | dtLANG\[n/a\]<br><br>**BMEcat NS** | End customer: language |
| . . . PARTIES | Must | \-  | \-  |     |
| . . . . PARTY | Must | \-  | \-  |     |
| . . . . . PARTY\_ID | Must | 2045638 | dtSTRING\[250\]<br><br>**BMEcat NS** | Galaxus internal customer ID |
| . . . . . PARTY\_ROLE | Must | buyer, supplier | dtSTRING\[20\] | buyer & supplier |
| . . . . . ADDRESS | Must | \-  | \-  | ![Warning](https://pf-emoji-service--cdn.us-east-1.prod.public.atl-paas.net/atlassian/productivityEmojis/exclamation-32px.png) All address fields/combinations can occur with both UDX.DG.CUSTOMER\_TYPE=**private\_customer** and UDX.DG.CUSTOMER\_TYPE=**company** |
| . . . . . . NAME | Can \* | Mustermann GmbH | dtMLSTRING\[50\]<br><br>**BMEcat NS** | Company name |
| . . . . . . NAME2 | Can | Ulla Mustermann | dtMLSTRING\[50\]<br><br>**BMEcat NS** | For the attention of |
| . . . . . . NAME3 | Can | Block A | dtMLSTRING\[50\]<br><br>**BMEcat NS** | Building |
| . . . . . . DEPARTMENT | Can | Accounting | dtMLSTRING\[50\]<br><br>**BMEcat NS** | Department |
| . . . . . . CONTACT\_DETAILS | Can | \-  | \-  |     |
| . . . . . . . TITLE | Can | Mr., Ms. | dtMLSTRING\[20\]<br><br>**BMEcat NS** | Salutation (depending on the language of the customer) |
| . . . . . . . FIRST\_NAME | Can \* | Ulla | dtMLSTRING\[50\]<br><br>**BMEcat NS** | First name<br><br>*   \= Either company name or first name and surname are mandatory |
| . . . . . . . CONTACT\_NAME | Can \* | Mustermann | dtMLSTRING\[50\]<br><br>**BMEcat NS** | Last name<br><br>*   \= Either company name or first name and surname are mandatory |
| . . . . . . STREET | Can | Musterstrasse 200b | dtMLSTRING\[50\]<br><br>**BMEcat NS** | Street and house number |
| . . . . . . ZIP | Must | 8000 | dtMLSTRING\[20\]<br><br>**BMEcat NS** | ZIP code |
| . . . . . . BOXNO | Can | 7   | dtMLSTRING\[20\]<br><br>**BMEcat NS** | P.O. box number (the text _P.O. box_ must be added to the address label) |
| . . . . . . CITY | Must | Zürich | dtMLSTRING\[50\]<br><br>**BMEcat NS** | City |
| . . . . . . COUNTRY | Must | Schweiz, Deutschland | dtMLSTRING\[50\]<br><br>**BMEcat NS** | Country (transmitted in German only) |
| . . . . . . COUNTRY\_CODED | Must | CH, DE | dtCOUNTRIES<br><br>**BMEcat NS** | Country code according ISO 3166-1 ALPHA-2 |
| . . . . . . PHONE | Can | 0448447700 | dtMLSTRING\[50\]<br><br>**BMEcat NS** | Phone number |
| . . . . . . EMAIL | Must | [noreply@galaxus.ch](mailto:noreply@galaxus.ch "mailto:noreply@galaxus.ch"), [noreply@galaxus.de](mailto:noreply@galaxus.de "mailto:noreply@galaxus.de") | dtSTRING\[255\]<br><br>**BMEcat NS** | Galaxus Email Address (hardcoded)  <br>![Warning](https://pf-emoji-service--cdn.us-east-1.prod.public.atl-paas.net/atlassian/productivityEmojis/exclamation-32px.png) Please _do not_ send any emails to this address, the mailbox is not maintained |
| . . . ORDER\_PARTIES\_REFERENCE | Must | \-  | \-  |     |
| . . . . BUYER\_IDREF | Must | 2045638 | dtSTRING\[250\]<br><br>**BMEcat NS** | Galaxus internal customer ID |
| . . . . SUPPLIER\_IDREF | Must | 1928008 | dtSTRING\[250\]<br><br>**BMEcat NS** | Galaxus internal partner ID |
| . . . SHIPMENT\_ID | Must | 9960114348.00119018 | dtSTRING\[250\] | Package number of the physical return |
| . . . TRACKING\_TRACING\_URL | Must | https://service.post.ch/EasyTrack/submitParcelData.do?formattedParcelCodes=  <br>99.60.114348.00119018&  <br>lang=de&shortcut=swisspost-tracking | dtSTRING\[255\] | URL of the shipment number, so that the return shipment can be tracked |
| . RETURNREGISTRATION\_ITEM\_LIST | Must | \-  | \-  |     |
| . . RETURNREGISTRATION\_ITEM | Must | \-  | \-  |     |
| . . . LINE\_ITEM\_ID | Must | 1   | dtSTRING\[50\] | Continuous document line numbering, starting at «1» |
| . . . PRODUCT\_ID | Must | \-  | \-  |     |
| . . . . SUPPLIER\_PID | Must | A375-129 | dtSTRING\[50\]<br><br>**BMEcat NS** | Partner product key |
| . . . . INTERNATIONAL\_PID | Must | 09783404175109 | dtSTRING\[14\]<br><br>**BMEcat NS** | GTIN-14 with leading zeros |
| . . . . BUYER\_PID | Can | 6406567 | dtSTRING\[50\]<br><br>**BMEcat NS** | Galaxus product key |
| . . . QUANTITY | Must | 2   | dtNUMBER\[n/a\] | Item position quantity |
| . . . ORDER\_UNIT | Must | C62 | dtPUNIT\[3\] | «C62» (hardcoded), stands for the unit piece |
| . . . RETURNREASON | Must | 2   | dtINTEGER\[n/a\] | Return reason:<br><br>«1» (Don't like)  <br>«2» (Wrong size)  <br>«3» (Wrong product)  <br>«4» (Delivery too late)  <br>«5» (Does not match the description)  <br>«6» (Wrong order) |
| . RETURNREGISTRATION\_SUMMARY | Must | \-  | \-  |     |
| . . TOTAL\_ITEM\_NUM | Must | 2   | dtCOUNT\[n/a\] | Sum of all quantities |