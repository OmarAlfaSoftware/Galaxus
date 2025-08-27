this file contains the cancel confirmation from our side to Galaxus to confirm or regject the cancellation request

so this file will always be from our side so it will be exists in partner2dg folder

with this name GCANR_SupplierId_OrderId_*_Timestamp.xml

as the * is the document number

now for the structure of this file
![Image](<../Images/Order/cancel confirmation.png>)

we have the header that is mentioned earlier 

then we have the confirmation header that contain the order id and confirmation date

then we have the confirmation item list that carry the confirmation item

and the item contain on the product details then the quantity the boolean requested accepted

and a comment  if there 

so the XML will be like this 

```xml
<?xml version="1.0" encoding="utf-8"?>
<CANCELCONFIRMATION xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns="http://www.opentrans.org/XMLSchema/2.1" version="2.1">
	<CANCELCONFIRMATION_HEADER>
		<CANCELCONFIRMATION_INFO>
			<ORDER_ID>9316271</ORDER_ID>
			<CANCELCONFIRMATION_DATE>2017-06-25T16:57:33</CANCELCONFIRMATION_DATE>
		</CANCELCONFIRMATION_INFO>
	</CANCELCONFIRMATION_HEADER>
	<CANCELCONFIRMATION_ITEM_LIST>
		<CANCELCONFIRMATION_ITEM>
			<PRODUCT_ID>
				<SUPPLIER_PID xmlns="http://www.bmecat.org/bmecat/2005">A375-129</SUPPLIER_PID>
				<INTERNATIONAL_PID xmlns="http://www.bmecat.org/bmecat/2005">09783404175109</INTERNATIONAL_PID>
				<BUYER_PID xmlns="http://www.bmecat.org/bmecat/2005">6406567</BUYER_PID>
			</PRODUCT_ID>
			<QUANTITY>1</QUANTITY>
			<REQUESTACCEPTED>false</REQUESTACCEPTED>
			<RESPONSECOMMENT>Bereits versendet</RESPONSECOMMENT>
		</CANCELCONFIRMATION_ITEM>
	</CANCELCONFIRMATION_ITEM_LIST>
</CANCELCONFIRMATION>
```
|     |     |     |     |     |
| --- | --- | --- | --- | --- |
| **XML Element** | **M/C/S** | **Sample values** | [**Data type**](https://confdg.atlassian.net/wiki/spaces/PI/pages/168689833771 "https://confdg.atlassian.net/wiki/spaces/PI/pages/168689833771") **\[maxLength\]** | **Description** |
| CANCELCONFIRMATION | Must | \-  | \-  | See [Namespaces](https://confdg.atlassian.net/wiki/spaces/PI/pages/168689833747 "https://confdg.atlassian.net/wiki/spaces/PI/pages/168689833747") regarding the correct usage of namespaces. |
| . CANCELCONFIRMATION\_HEADER | Must | \-  | \-  |     |
| . . CANCELCONFIRMATION\_INFO | Must | \-  | \-  |     |
| . . . ORDER\_ID | Must | 9316271 | dtSTRING\[25\] | Galaxus purchase Order ID |
| . . . CANCELCONFIRMATION\_DATE | Can | 2017-06-25T16:57:33 | dtDATETIME | Date of the cancellation confirmation or response to the cancellation request |
| . CANCELCONFIRMATION\_ITEM\_LIST | Must | \-  | \-  |     |
| . . CANCELCONFIRMATION\_ITEM | Must | \-  | \-  |     |
| . . . PRODUCT\_ID | Must | \-  | \-  |     |
| . . . . SUPPLIER\_PID | Must | A375-129 | dtSTRING\[50\]<br><br>**BMEcat NS** | Partner product key |
| . . . . INTERNATIONAL\_PID | Must | 09783404175109 | dtSTRING\[14\]<br><br>**BMEcat NS** | GTIN-14 with leading zeros |
| . . . . BUYER\_PID | Can | 6406567 | dtSTRING\[50\]<br><br>**BMEcat NS** | Galaxus product key |
| . . . QUANTITY | Must | 1   | dtNUMBER | Cancellation quantity |
| . . . REQUESTACCEPTED | Must | false | dtBOOLEAN | true: Cancellation accepted  <br>false: Cancellation rejected |
| . . . RESPONSECOMMENT | Must \* | Bereits versendet | dtSTRING\[100\] | \* = Must be transmitted in case of rejection (REQUESTACCEPTED = false) |