this file contains the supplier cancel notification

this mean how can the supplier cancel the order after started

the cancel operation is will happen like the supplier request order cancellation


so now we will discuss the structure

![Image](<../Images/Order/supplier cancel notification.png>)

the header discussed earlier

the the cancellation header
  * info like
    * order id 
    * cancel date

then we have request item list
have the details of the product
and the quantity that needed to cancelled

this an example for the XML

```xml
<?xml version="1.0" encoding="utf-8"?>
<SUPPLIERCANCELNOTIFICATION xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns="http://www.opentrans.org/XMLSchema/2.1" version="2.1">
	<SUPPLIERCANCELNOTIFICATION_HEADER>
		<SUPPLIERCANCELNOTIFICATION_INFO>
			<ORDER_ID>9316271</ORDER_ID>
			<SUPPLIERCANCELNOTIFICATION_DATE>2017-06-13T15:49:49</SUPPLIERCANCELNOTIFICATION_DATE>
		</SUPPLIERCANCELNOTIFICATION_INFO>
	</SUPPLIERCANCELNOTIFICATION_HEADER>
	<SUPPLIERCANCELNOTIFICATION_ITEM_LIST>
		<SUPPLIERCANCELNOTIFICATION_ITEM>
			<PRODUCT_ID>
				<SUPPLIER_PID xmlns="http://www.bmecat.org/bmecat/2005">A375-129</SUPPLIER_PID>
				<INTERNATIONAL_PID xmlns="http://www.bmecat.org/bmecat/2005">09783404175109</INTERNATIONAL_PID>
				<BUYER_PID xmlns="http://www.bmecat.org/bmecat/2005">6406567</BUYER_PID>
			</PRODUCT_ID>
			<QUANTITY>1</QUANTITY>
		</SUPPLIERCANCELNOTIFICATION_ITEM>
	</SUPPLIERCANCELNOTIFICATION_ITEM_LIST>
</SUPPLIERCANCELNOTIFICATION>
```
|     |     |     |     |     |
| --- | --- | --- | --- | --- |
| **XML Element** | **M/C/S** | **Sample values** | [**Data type**](https://confdg.atlassian.net/wiki/spaces/PI/pages/168689833771 "https://confdg.atlassian.net/wiki/spaces/PI/pages/168689833771") **\[maxLength\]** | **Description** |
| SUPPLIERCANCELNOTIFICATION | Must | \-  | \-  | See [Namespaces](https://confdg.atlassian.net/wiki/spaces/PI/pages/168689833747 "https://confdg.atlassian.net/wiki/spaces/PI/pages/168689833747") regarding the correct usage of namespaces. |
| . SUPPLIERCANCELNOTIFICATION\_HEADER | Must | \-  | \-  |     |
| . . SUPPLIERCANCELNOTIFICATION\_INFO | Must | \-  | \-  |     |
| . . . ORDER\_ID | Must | 9316271 | dtSTRING\[25\] | Galaxus Purchase Order ID |
| . . . SUPPLIERCANCELNOTIFICATION\_DATE | Should | 2017-06-13T15:49:49 | dtDATETIME | Document creation timestamp |
| . SUPPLIERCANCELNOTIFICATION\_ITEM\_LIST | Must | \-  | \-  |     |
| . . SUPPLIERCANCELNOTIFICATION\_ITEM | Must | \-  | \-  |     |
| . . . PRODUCT\_ID | Must | \-  | \-  |     |
| . . . . SUPPLIER\_PID | Must | A375-129 | dtSTRING\[50\]<br><br>**BMEcat NS** | Partner product key |
| . . . . INTERNATIONAL\_PID | Must | 09783404175109 | dtSTRING\[14\]<br><br>**BMEcat NS** | GTIN-14 with leading zeros |
| . . . . BUYER\_PID | Can | 6406567 | dtSTRING\[50\]<br><br>**BMEcat NS** | Galaxus product key |
| . . . QUANTITY | Must | 1   | dtNUMBER | Cancellation quantity |