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
```<?xml version="1.0" encoding="utf-8"?>
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
```<?xml version="1.0" encoding="utf-8"?>
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
