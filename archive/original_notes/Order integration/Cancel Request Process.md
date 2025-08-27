this file contains the cancellation request that came from Galaxus

this notify us that this order has been cancelled and if there any objection from our side we can contact them to tell the problem 

this file is exist in dg2Partner folder

so now in the part of naming the file will be named like this GCANP_SupplierId_OrderId_Timestamp.xml

then we will response with accept and reject the request 

as the * is the document number in our DB

If the Cancel Request is implemented, it is mandatory that the Cancel Confirmation is also implemented.

now for the structure of the request

![](<../Images/Order/Cancellation process.png>)

the structure of the request contains

the header discussed earlier

the the cancellation header
  * info like
    * order id 
    * cancel date
  * language
  * party
  * order party refrence

then we have request item list
have the details of the product
and the quantity that needed to cancelled even it sub order or full order

this the xml of this 
```<?xml version="1.0" encoding="utf-8"?>
<CANCELREQUEST xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns="http://www.opentrans.org/XMLSchema/2.1" version="2.1">
	<CANCELREQUEST_HEADER>
		<CANCELREQUEST_INFO>
			<ORDER_ID>9316271</ORDER_ID>
			<CANCELREQUEST_DATE>2017-06-15T16:57:33</CANCELREQUEST_DATE>
			<LANGUAGE xmlns="http://www.bmecat.org/bmecat/2005">ger</LANGUAGE>
			<PARTIES>
				<PARTY>
					<PARTY_ID xmlns="http://www.bmecat.org/bmecat/2005" type="buyer_specific">2045638</PARTY_ID>
					<PARTY_ROLE>buyer</PARTY_ROLE>
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
					<PARTY_ID xmlns="http://www.bmecat.org/bmecat/2005" type="buyer_specific">1928008</PARTY_ID>
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
					<PARTY_ID type="buyer_specific" xmlns="http://www.bmecat.org/bmecat/2005">2165966</PARTY_ID>
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
			</PARTIES>
			<ORDER_PARTIES_REFERENCE>
				<BUYER_IDREF xmlns="http://www.bmecat.org/bmecat/2005" type="buyer_specific">2045638</BUYER_IDREF>
				<SUPPLIER_IDREF xmlns="http://www.bmecat.org/bmecat/2005" type="buyer_specific">1928008</SUPPLIER_IDREF>
			</ORDER_PARTIES_REFERENCE>
		</CANCELREQUEST_INFO>
	</CANCELREQUEST_HEADER>
	<CANCELREQUEST_ITEM_LIST>
		<CANCELREQUEST_ITEM>
			<LINE_ITEM_ID>1</LINE_ITEM_ID>
			<PRODUCT_ID>
				<SUPPLIER_PID xmlns="http://www.bmecat.org/bmecat/2005" type="supplierProductKey">A375-129</SUPPLIER_PID>
				<INTERNATIONAL_PID xmlns= "http://www.bmecat.org/bmecat/2005" type="gtin">09783404175109</INTERNATIONAL_PID>
				<BUYER_PID xmlns="http://www.bmecat.org/bmecat/2005" type="DgProductId">6406567</BUYER_PID>
			</PRODUCT_ID>
			<QUANTITY>2</QUANTITY>
			<ORDER_UNIT xmlns="http://www.bmecat.org/bmecat/2005">C62</ORDER_UNIT>
		</CANCELREQUEST_ITEM>
	</CANCELREQUEST_ITEM_LIST>
	<CANCELREQUEST_SUMMARY>
		<TOTAL_ITEM_NUM>2</TOTAL_ITEM_NUM>
	</CANCELREQUEST_SUMMARY>
</CANCELREQUEST>
``` 



|     |     |     |     |     |
| --- | --- | --- | --- | --- |
| **XML Element** | **M/C/S** | **Sample values** | [**Data type**](https://confdg.atlassian.net/wiki/spaces/PI/pages/168689833771 "https://confdg.atlassian.net/wiki/spaces/PI/pages/168689833771") **\[maxLength\]** | **Description** |
| CANCELREQUEST | Must | \-  | \-  | See [Namespaces](https://confdg.atlassian.net/wiki/spaces/PI/pages/168689833747 "https://confdg.atlassian.net/wiki/spaces/PI/pages/168689833747") regarding the correct usage of namespaces |
| . CANCELREQUEST\_HEADER | Must | \-  | \-  |     |
| . . CANCELREQUEST\_INFO | Must | \-  | \-  |     |
| . . . ORDER\_ID | Must | 9316271 | dtSTRING\[25\] | Galaxus Purchase Order ID |
| . . . CANCELREQUEST\_DATE | Must | 2017-06-15T16:57:33 | dtDATETIME | Cancel request timestamp |
| . . . LANGUAGE | Must | ger | dtLANG<br><br>**BMEcat NS** | Language end customer  <br>![Warning](https://pf-emoji-service--cdn.us-east-1.prod.public.atl-paas.net/atlassian/productivityEmojis/exclamation-32px.png) Important for the RESPONSECOMMENT in the Cancel confirmation |
| . . . PARTIES | Must | \-  | \-  |     |
| . . . . PARTY | Must | \-  | \-  |     |
| . . . . . PARTY\_ID | Must | 2045638 | dtSTRING\[250\]<br><br>**BMEcat NS** | Galaxus internal customer number |
| . . . . . PARTY\_ROLE | Must | buyer, supplier | dtSTRING\[20\] | buyer & supplier (info relevant for billing) |
| . . . . . ADDRESS | Must | \-  | \-  | ![Warning](https://pf-emoji-service--cdn.us-east-1.prod.public.atl-paas.net/atlassian/productivityEmojis/exclamation-32px.png) All address fields/combinations can occur with both UDX.DG.CUSTOMER\_TYPE=**private\_customer** and UDX.DG.CUSTOMER\_TYPE=**company** |
| . . . . . . NAME | Can \* | Mustermann GmbH | dtMLSTRING\[50\]<br><br>**BMEcat NS** | Company name<br><br>For **EU-Hub partners** with the PARTY\_ROLE = delivery, always Galaxus Deutschland GmbH is transmitted (also for private customers) |
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
| . . . . . . COUNTRY | Must | Schweiz, Deutschland, Liechtenstein, Österreich | dtMLSTRING\[50\]<br><br>**BMEcat NS** | Country (transmitted in German only) |
| . . . . . . COUNTRY\_CODED | Must | CH, DE, LI, AT | dtCOUNTRIES<br><br>**BMEcat NS** | Country code according ISO 3166-1 ALPHA-2 |
| . . . . . . PHONE | Can | 0448447700 | dtMLSTRING\[50\]<br><br>**BMEcat NS** | Phone number |
| . . . . . . EMAIL | Must | noreply@galaxus.ch, noreply@galaxus.de | dtSTRING\[255\]<br><br>**BMEcat NS** | Galaxus Email Address (hardcoded)  <br>![Warning](https://pf-emoji-service--cdn.us-east-1.prod.public.atl-paas.net/atlassian/productivityEmojis/exclamation-32px.png) Please _do not_ send any emails to this address, the mailbox is not maintained  <br>![Question Mark](https://pf-emoji-service--cdn.us-east-1.prod.public.atl-paas.net/atlassian/productivityEmojis/question-32px.png) The email address of the end customer can be viewed in the [Partner Portal](https://confdg.atlassian.net/wiki/spaces/PI/pages/169009284340 "https://confdg.atlassian.net/wiki/spaces/PI/pages/169009284340") if required (carrier notification, service case):<br><br>Click here to expand<br><br>Open image-20220913-140718.png<br><br>![](blob:https://confdg.atlassian.net/8989a3a3-5d06-4bec-b970-23f9b4f1fe52#media-blob-url=true&id=83946d1b-c60b-423d-888c-d1c81d47afab&collection=contentId-168689834080&contextId=168689834080&width=404&height=67&alt=) |
| . . . ORDER\_PARTIES\_REFERENCE | Must | \-  | \-  |     |
| . . . . BUYER\_IDREF | Must | 2045638 | dtSTRING\[250\]<br><br>**BMEcat NS** | Galaxus internal customer number |
| . . . . SUPPLIER\_IDREF | Must | 1928008 | dtSTRING\[250\]<br><br>**BMEcat NS** | Galaxus internal partner ID |
| . CANCELREQUEST\_ITEM\_LIST | Must | \-  | \-  |     |
| . . CANCELREQUEST\_ITEM | Must | \-  | \-  |     |
| . . . LINE\_ITEM\_ID | Must | 1, 2, 3 |     | Continuous document line numbering, starting at «1» |
| . . . PRODUCT\_ID | Must | \-  | \-  |     |
| . . . . SUPPLIER\_PID | Must | A375-129 | dtSTRING\[50\]<br><br>**BMEcat NS** | Partner product key |
| . . . . INTERNATIONAL\_PID | Must | 09783404175109 | dtSTRING\[14\]<br><br>**BMEcat NS** | GTIN-14 with leading zeros |
| . . . . BUYER\_PID | Can | 6406567 | dtSTRING\[50\]<br><br>**BMEcat NS** | Galaxus product key |
| . . . QUANTITY | Must | 2   | dtNUMBER | Cancellation quantity |
| . . . ORDER\_UNIT | Must | C62 | dtPUNIT<br><br>**BMEcat NS** | «C62» (hardcoded), stands for the unit piece |
| . CANCELREQUEST\_SUMMARY | Must | \-  | \-  |     |
| . . TOTAL\_ITEM\_NUM | Must | 2   | dtNUMBER | Total quantity |