this file contains the order response details 
first this file is only exists at partner2dg as this file is only came from our side
this file purpose is to confirm that this order is accepted 
we can send any number of order resonses only if they carry the same order id 
the structure of the file name is GORDR_<SupplierId>_<OrderId>_<*>_<Timestamp>.xml
so the order response could set the delivery time or not also accept some and reject some or reject them all
 
 the way of repsond is to mention the qunatity if need to accept an exact number so the total number will not be all satistfied like if the customer order 10 and only we have 4 so we reponse with qunatity 4 and this mean that this order reject 6 and accept 4 
 then if it not compitatble with the customer he can cancel the order then

 Note:
 in the backorder delivery for marketplace irect delivery if it take than 30 days the order must be cancelled 
 now for the structure of the request
 we can respose like this 
```<?xml version="1.0" encoding="utf-8"?>
<ORDERRESPONSE xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns="http://www.opentrans.org/XMLSchema/2.1" version="2.1">
	<ORDERRESPONSE_HEADER>
		<ORDERRESPONSE_INFO>
			<ORDER_ID>9316271</ORDER_ID>
			<ORDERRESPONSE_DATE>2017-06-14T15:53:18</ORDERRESPONSE_DATE>
			<SUPPLIER_ORDER_ID>191919</SUPPLIER_ORDER_ID>
		</ORDERRESPONSE_INFO>
	</ORDERRESPONSE_HEADER>
	<ORDERRESPONSE_ITEM_LIST>
		<ORDERRESPONSE_ITEM>
			<PRODUCT_ID>
				<SUPPLIER_PID xmlns="http://www.bmecat.org/bmecat/2005">A375-129</SUPPLIER_PID>
				<INTERNATIONAL_PID xmlns="http://www.bmecat.org/bmecat/2005">09783404175109</INTERNATIONAL_PID>
				<BUYER_PID xmlns="http://www.bmecat.org/bmecat/2005">6406567</BUYER_PID>
			</PRODUCT_ID>
			<QUANTITY>2</QUANTITY>
			<DELIVERY_DATE>
				<DELIVERY_START_DATE>2017-06-21</DELIVERY_START_DATE>
				<DELIVERY_END_DATE>2017-06-21</DELIVERY_END_DATE>
			</DELIVERY_DATE>
		</ORDERRESPONSE_ITEM>
	</ORDERRESPONSE_ITEM_LIST>
</ORDERRESPONSE>
```
or like this 
```<?xml version="1.0" encoding="utf-8"?>
<ORDERRESPONSE xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns="http://www.opentrans.org/XMLSchema/2.1" version="2.1">
	<ORDERRESPONSE_HEADER>
		<ORDERRESPONSE_INFO>
			<ORDER_ID>9316271</ORDER_ID>
			<ORDERRESPONSE_DATE>2017-06-14T15:53:18</ORDERRESPONSE_DATE>
			<SUPPLIER_ORDER_ID>191919</SUPPLIER_ORDER_ID>
		</ORDERRESPONSE_INFO>
	</ORDERRESPONSE_HEADER>
	<ORDERRESPONSE_ITEM_LIST>
		<ORDERRESPONSE_ITEM>
			<PRODUCT_ID>
				<SUPPLIER_PID xmlns="http://www.bmecat.org/bmecat/2005">A375-129</SUPPLIER_PID>
				<INTERNATIONAL_PID xmlns="http://www.bmecat.org/bmecat/2005">09783404175109</INTERNATIONAL_PID>
				<BUYER_PID xmlns="http://www.bmecat.org/bmecat/2005">6406567</BUYER_PID>
			</PRODUCT_ID>
			<QUANTITY>2</QUANTITY>
			<DELIVERY_DATE>
				<DELIVERY_START_DATE>2017-06-21</DELIVERY_START_DATE>
				<DELIVERY_END_DATE>2017-06-21</DELIVERY_END_DATE>
			</DELIVERY_DATE>
		</ORDERRESPONSE_ITEM>
	</ORDERRESPONSE_ITEM_LIST>
</ORDERRESPONSE>
```

there are minimum structure of the response of the order and it will be like this
```<?xml version="1.0" encoding="utf-8"?>
<ORDERRESPONSE xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns="http://www.opentrans.org/XMLSchema/2.1" version="2.1">
	<ORDERRESPONSE_HEADER>
		<ORDERRESPONSE_INFO>
			<ORDER_ID>9316271</ORDER_ID>
			<ORDERRESPONSE_DATE>2017-06-14T15:53:18</ORDERRESPONSE_DATE>
			<SUPPLIER_ORDER_ID>191919</SUPPLIER_ORDER_ID>
		</ORDERRESPONSE_INFO>
	</ORDERRESPONSE_HEADER>
</ORDERRESPONSE>
```
 now the structure of the file have this ![image](<../Images/Order/order response.png>)
 we have the order header and the info of the order like the id and the supplier and buyer ids

 then the orderitem in the orderlist with product details and the quantity (set 0 if rejected)
 then the delivery info like start date and ending date

|     |     |     |     |     |     |
| --- | --- | --- | --- | --- | --- |
| **XML Element**<br><br>Minimum  <br>With arrival date |     | **M/C/S** | **Sample values** | [**Data type**](https://confdg.atlassian.net/wiki/spaces/PI/pages/168689833771 "https://confdg.atlassian.net/wiki/spaces/PI/pages/168689833771") **\[maxLength\]** | **Description** |
| ORDERRESPONSE |     | Must | \-  | \-  | See [Namespaces](https://confdg.atlassian.net/wiki/spaces/PI/pages/168689833747 "https://confdg.atlassian.net/wiki/spaces/PI/pages/168689833747") regarding the correct usage of namespaces. |
| . ORDERRESPONSE\_HEADER | Must | \-  | \-  |     |
| . . ORDERRESPONSE\_INFO | Must | \-  | \-  |     |
| . . . ORDER\_ID | Must | 9316271 | dtSTRING\[25\] | Galaxus Purchase Order ID |
| . . . ORDERRESPONSE\_DATE | Must | 2017-06-14T15:53:18 | dtDATETIME | Order Response timestamp |
| . . . SUPPLIER\_ORDER\_ID | Should | 191919 | dtSTRING\[250\] | Order ID from your ERP<br><br>**Important for merchants:** The ID will be printed onto our return labels as Code-39-Barcode and must comply with ISO/IEC 16388 norm.  <br>The SUPPLIER\_ORDER\_ID is also displayed on the marketplace sales fee reports. |
| . ORDERRESPONSE\_ITEM\_LIST |     | Should | \-  | \-  |     |
| . . ORDERRESPONSE\_ITEM | Should | \-  | \-  |     |
| . . . PRODUCT\_ID | Must | \-  | \-  |     |
| . . . . SUPPLIER\_PID | Must | A375-129 | dtSTRING\[50\]<br><br>**BMEcat NS** | Partner product key |
| . . . . INTERNATIONAL\_PID | Must | 09783404175109 | dtSTRING\[14\]<br><br>**BMEcat NS** | GTIN-14 with leading zeros |
| . . . . BUYER\_PID | Can | 6406567 | dtSTRING\[50\]<br><br>**BMEcat NS** | Galaxus product key |
| . . . QUANTITY | Must | 2   | dtNUMBER | Quantity as integer > 0 |
| . . . DELIVERY\_DATE | Should | \-  | \-  | Date of arrival at the recipient of the goods:<br><br>*   If the arrival date cannot be met, we require an update of the date via Order Response or [Partner Portal](https://confdg.atlassian.net/wiki/spaces/PI/pages/169009284340 "https://confdg.atlassian.net/wiki/spaces/PI/pages/169009284340")<br>    <br>*   If this date can not be sent, the ORDERRESPONSE\_ITEM\_LIST must be omitted completely |
| . . . . DELIVERY\_START\_DATE | Should | 2017-06-21 | dtDATETIME | Estimated arrival date:<br><br>*   If the arrival date is unknown, the segment must be left blank or should be sent without see "Minimum: Order confirmation WITHOUT arrival date"  <br>    ![Warning](https://pf-emoji-service--cdn.us-east-1.prod.public.atl-paas.net/atlassian/productivityEmojis/exclamation-32px.png) «unknown» or «fake» delivery dates are not allowed<br>    <br>*   A date in the past does not trigger date updates<br>    <br>*   ![Warning](https://pf-emoji-service--cdn.us-east-1.prod.public.atl-paas.net/atlassian/productivityEmojis/exclamation-32px.png) missing DELIVERY\_DATE element or no value sets the availability to _unknown_<br>    <br>*   For orders with DELIVERY\_DATE type="fixed" this must be confirmed |
| . . . . DELIVERY\_END\_DATE | Should | 2017-06-21 | dtDATETIME | Same value as DELIVERY\_START\_DATE |
