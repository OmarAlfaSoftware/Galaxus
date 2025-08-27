first of all we work with orders in diffrent way than the integration of the data of the proudcts

we work with XML files shared in two folder first one named dg2partner and this mean that this folder contains the files that are sended from the galaxus system to our system like orders to inform us that there is a new order or response for specefic request we send like cancel order or confirm cancellation or shipping or anything that related to us or actions required from the galaxus system.
and also we have the folder of partner2dg, and this folder contains the files that we send to galaxus system like order confirmation,
order status, or any action that outgoing form us to Galaxus system 
also there is the live folder of reciving and sending but the document until now has no much information about its mecanism.

for each file send or recived we have a standard way for the structure of it starting from the name of the file as the should be named as 
GORDP and this mean Galaxus Order Processing and should also pass in the name of the file some info and its diffrent parameters for each file like,
once it order id , shipmment id, ...etc all these details will shown in each file of the requested 

also  there is a common header that must exist in any of them to confirm the standards like 
xmlns:xsd="http://www.w3.org/2001/XMLSchema"

xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"

xmlns="http://www.opentrans.org/XMLSchema/2.1"

version="2.1"
and must been like this
```xml

<?xml version="1.0" encoding="utf-8"?>
<ORDERRESPONSE xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns="http://www.opentrans.org/XMLSchema/2.1" version="2.1">
	<ORDERRESPONSE_HEADER>
		...
	</ORDERRESPONSE_HEADER>
	<ORDERRESPONSE_ITEM_LIST>
		...
	</ORDERRESPONSE_ITEM_LIST>
</ORDERRESPONSE>
```
also in the body of the file there are some standards like the way of write the attibute like this 
```xml

1. <?xml version="1.0" encoding="utf-8"?>
<ORDERRESPONSE xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns="http://www.opentrans.org/XMLSchema/2.1" version="2.1">
...
	<PRODUCT_ID>
		<SUPPLIER_PID xmlns="http://www.bmecat.org/bmecat/2005">A375-129</SUPPLIER_PID>
		<INTERNATIONAL_PID xmlns="http://www.bmecat.org/bmecat/2005">09783404175109</INTERNATIONAL_PID>
		<BUYER_PID xmlns="http://www.bmecat.org/bmecat/2005">6406567</BUYER_PID>
	</PRODUCT_ID>
...
</ORDERRESPONSE>
```
1. or this 
```xml
 <?xml version="1.0" encoding="utf-8"?>
<ORDERRESPONSE xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns="http://www.opentrans.org/XMLSchema/2.1" xmlns:bmecat="http://www.bmecat.org/bmecat/2005" version="2.1">
...
			<PRODUCT_ID>
				<bmecat:SUPPLIER_PID>A375-129</bmecat:SUPPLIER_PID>
				<bmecat:INTERNATIONAL_PID>09783404175109</bmecat:INTERNATIONAL_PID>
				<bmecat:BUYER_PID>6406567</bmecat:BUYER_PID>
			</PRODUCT_ID>
...	
</ORDERRESPONSE>
```
and both are right 

after that we have the datatypes and its standards also that use in all files

like this 
Basic Data Types:
| **Description**              | **Data type**   | **Explanation**                                                                                                                                   | **Format**                |
|------------------------------|-----------------|---------------------------------------------------------------------------------------------------------------------------------------------------|---------------------------|
| **Boolean**                   | dtBOOLEAN       | The specification of the values "true" or "false" is not case-sensitive.                                                                         | Sample values: TRUE, true, True |
| **Positive Integer**          | dtCOUNT         | No fractions. No floating point numbers. No negative numbers. "0" is permitted. No separator is allowed to delimit digits of 1000.                | Sample values: 0,1, 2      |
| **Date and time**             | dtDATETIME      | Date and optional time                                                                                                                                 | jjjj-mm-ttThh:mm:ss+zz:00 |
| **Float**                     | dtFLOAT         | Floating point number in 64-bit according to IEEE Standard 754 Decimal separator is the dot. No separator is allowed to delimit digits of 1000.     | Sample values: 15.4, 4164.750 |
| **Integer**                   | dtINTEGER       | Integer with optional sign. No fractions. No floating point numbers. No separator is allowed to delimit digits of 1000.                           | Sample values: 1, 58502    |
| **Multilingual string**       | dtMLSTRING      | This data type differs from the data type dtSTRING only by the additional attribute "lang", which it adds to the elements of the data type dtMLSTRING. |                           |
| **Numeric value**             | dtNUMBER        | To be used when a more specific numeric format is not needed or is not practical. There is no restriction on minimum and maximum values, number of digits or decimal places. | Sample values: 15, 3.14   |
| **String**                    | dtSTRING        | Character string according to the encoding standard specified in the document. Digitec Galaxus uses UTF-8 throughout.                              | Sample values: Text, &lt;b&gt;text&lt;/b&gt; |

Enumrate Data
| **Description**         | **Data type** | **Explanation**                                                                                                                                                                          | **Format**            |
|-------------------------|---------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|-----------------------|
| **Binary coded data**   | base64Binary  | This data type is used to transport a file in coded form within the XML file. The file is not linked, but is contained in the XML document. The file must be encoded in the 'base64' procedure. Further information on the base64 encoding method can be found at [IETF RFC](http://www.ietf.org/rfc/rfc2045.txt). The data type base64Binary is provided by the W3C. Additional information on embedding files in XML can be found at [W3C Describing Media Content](http://www.w3.org/TR/xml-media-types/.) |                       |
| **Country codes**       | dtCOUNTRIES   | Country codes to indicate areas of availability (TERRITORY) according to ISO 3166 ([ISO 3166 Country Codes](https://www.iso.org/iso-3166-country-codes.html)) a further subdivision of country codes, for example according to regions, the "Country Subdivision Codes" can be used. | Sample values: DE (Germany), US (USA), DE-NW (Nordrhein-Westfalen in Germany), DK-025 (Kreis Roskilde in Denmark) |
| **Currency codes**      | dtCURRENCIES  | Currency codes to indicate currencies according to ISO 4217 ([ISO 4217 Currency Codes](https://www.iso.org/iso-4217-currency-codes.html)).                                                    | Sample values: EUR (Euro), USD (US-Dollar) |
| **Language codes**      | dtLANG        | Language codes to indicate the language used in texts or in images according to ISO 639 ([ISO 639 Language Codes](https://www.iso.org/iso-639-language-codes.html)).                       | Sample values: deu (deutsch) |
| **Order unit codes**    | dtPUNIT       | This enumeration contains the permissible order units                                                                                                                                     | Sample values: C62 (=Item) |

and lastely special Data
| **Description**               | **Data type** | **Explanation**                                                                                                                                                   | **Format** |
|-------------------------------|---------------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------|-----------|
| **Extensions header**          | udxHEADER     | This data type is defined empty and is used to define user-defined non-openTRANS elements for the header.                                                          |           |
| **Extensions position level**  | udxITEM       | This data type is defined empty and is used to define user-defined non-openTRANS elements for item-level description.                                             |           |


now we  have these files, each file discuss an important section with the details about it 

1. Order processing
   1. this file discuss how the order is sent to us [Order processing](<./Order Processing.md>)
2. Order Response
   1. this file discuss how we respond to an coming order [Order Response](<./Order Response.md>)
3. Shipping notification (Dispatch Notification)
   1. this file disucss the shipping operation [Shipping](<./Dispatch Notification.md>)
   2. next we have the shipping packaging details so this file discuss this[ packaging](<./Incoming goods.md>) 
4. Invoices
   1. this file contain how we send the Invoice [Invoice](<./Invoices.md>)
   2. then this file discuss the style of the pdfs of the invoices [Invoice Pdf](<./Export Invoices.md>)
5. Cancellation
   1. the cancelation from our side [Cancelation from us](<./Supplier Cancel Order Notification.md>)
   2. cancellation from Galaxus side [Galaxus Cancellation](<./Cancel Request Process.md>) 
   3. respond on Galaxus request [Cancellation respose](<./Cancel Confirmation.md>)
6. Return the order
   1. after recive the order the customer need to return it so he send a request for this [Return request](<./Return Registration.md>)
   2. the response from our side [return response](<./Supplier Return Notification.md>)
   
these files cover all dimensions of the order operations

