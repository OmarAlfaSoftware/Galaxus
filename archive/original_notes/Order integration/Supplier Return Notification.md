this file contains the return confirmation from the supplier

after the customer send a return registeration request we should respond on it 

so this file will be exist in the folder of partner2dg

and have the name of GSURN_SupplierId_OrderId_*_Timestamp.xml

as the * is the documnet number

after that we have the structure of the response and will be like this 

![Image](<../Images/Order/Spplier Retrun Notificaition.png>)

the header is discussed earlier

now for the return header we have the order id and the date

then we have the item list

each item carry the product details

and then we have the accept or reject and the quantity and the comment

and this an example for it 
```xml
<?xml version="1.0" encoding="utf-8"?>
<SUPPLIERRETURNNOTIFICATION xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns="http://www.opentrans.org/XMLSchema/2.1" version="2.1">
	<SUPPLIERRETURNNOTIFICATION_HEADER>
		<SUPPLIERRETURNNOTIFICATION_INFO>
			<ORDER_ID>9316271</ORDER_ID>
			<SUPPLIERRETURNNOTIFICATION_DATE>2017-06-15T16:57:33</SUPPLIERRETURNNOTIFICATION_DATE>
		</SUPPLIERRETURNNOTIFICATION_INFO>
	</SUPPLIERRETURNNOTIFICATION_HEADER>
	<SUPPLIERRETURNNOTIFICATION_ITEM_LIST>
		<SUPPLIERRETURNNOTIFICATION_ITEM>
			<PRODUCT_ID>
				<SUPPLIER_PID xmlns="http://www.bmecat.org/bmecat/2005">A375-129</SUPPLIER_PID>
				<INTERNATIONAL_PID xmlns="http://www.bmecat.org/bmecat/2005">09783404175109</INTERNATIONAL_PID>
				<BUYER_PID xmlns="http://www.bmecat.org/bmecat/2005">6406567</BUYER_PID>
			</PRODUCT_ID>
			<QUANTITY>2</QUANTITY>
			<REQUESTACCEPTED>false</REQUESTACCEPTED>
			<RESPONSECOMMENT>Beschädigt, deshalb abgelehnt.</RESPONSECOMMENT>
		</SUPPLIERRETURNNOTIFICATION_ITEM>
	</SUPPLIERRETURNNOTIFICATION_ITEM_LIST>
</SUPPLIERRETURNNOTIFICATION>
```
|     |     |     |     |     |
| --- | --- | --- | --- | --- |
| **XML Element** | **M/C/S** | **Sample values** | [**Data type**](https://confdg.atlassian.net/wiki/spaces/PI/pages/168689833771 "https://confdg.atlassian.net/wiki/spaces/PI/pages/168689833771") **\[maxLength\]** | **Description** |
| SUPPLIERRETURNNOTIFICATION | Must | \-  | \-  | See [Namespaces](https://confdg.atlassian.net/wiki/spaces/PI/pages/168689833747 "https://confdg.atlassian.net/wiki/spaces/PI/pages/168689833747") regarding the correct usage of namespaces. |
| . SUPPLIERRETURNNOTIFICATION\_HEADER | Must | \-  | \-  |     |
| . . SUPPLIERRETURNNOTIFICATION\_INFO | Must | \-  | \-  |     |
| . . . ORDER\_ID | Must | 9316271 | dtSTRING\[25\] | Galaxus Purchase Order ID |
| . . . SUPPLIERRETURNNOTIFICATION\_DATE | Must | 2017-06-15T16:57:33 | dtDATETIME | Return Notification timestamp |
| . SUPPLIERRETURNNOTIFICATION\_ITEM\_LIST | Must | \-  | \-  |     |
| . . SUPPLIERRETURNNOTIFICATION\_ITEM | Must | \-  | \-  |     |
| . . . PRODUCT\_ID | Must | \-  | \-  |     |
| . . . . SUPPLIER\_PID | Must | A375-129 | dtSTRING\[50\]<br><br>**BMEcat NS** | Partner product key |
| . . . . INTERNATIONAL\_PID | Must | 09783404175109 | dtSTRING\[14\]<br><br>**BMEcat NS** | GTIN-14 with leading zeros |
| . . . . BUYER\_PID | Can | 6406567 | dtSTRING\[50\]<br><br>**BMEcat NS** | Galaxus product key |
| . . . QUANTITY | Must | 2   | dtNUMBER | Return quantity |
| . . . REQUESTACCEPTED | Must | false | dtBOOLEAN | true: Accept the return  <br>false: Reject the return |
| . . . RESPONSECOMMENT | Must \* | Beschädigt, deshalb abgelehnt. | dtSTRING\[100\] | \* = You **must** send a RESPONSECOMMENT, if one or more items are declined  <br>![Warning](https://pf-emoji-service--cdn.us-east-1.prod.public.atl-paas.net/atlassian/productivityEmojis/exclamation-32px.png) Note the language of the end customer from the ORDP, RETP or [Partner Portal](https://confdg.atlassian.net/wiki/spaces/PI/pages/169009284340 "https://confdg.atlassian.net/wiki/spaces/PI/pages/169009284340") |