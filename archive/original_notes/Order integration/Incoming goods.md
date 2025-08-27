this file contains automation incoming goods with SSCC codes 
this is just a addtion to the dispatch notification 
as this is more details of the logistics
like the pacakage number, the barcode
and the pacakge details as each package contains wich products and how many and so on 

also this is optional but prefferble section

this an example of it 
```xml
<?xml version="1.0" encoding="utf-8"?>
<DISPATCHNOTIFICATION xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns="http://www.opentrans.org/XMLSchema/2.1" version="2.1">	
	<!-- IMPLEMENTATION EXAMPLES OF DISPATCHNOTIFICATION <LOGISTIC_DETAILS>-->
	<!-- Digitec Galaxus: V1.2 / 02.07.2020 -->
	<DISPATCHNOTIFICATION_HEADER>
		<CONTROL_INFO>
			<GENERATION_DATE>2019-02-13T17:05:34</GENERATION_DATE>
		</CONTROL_INFO>
		<DISPATCHNOTIFICATION_INFO>
			<DISPATCHNOTIFICATION_ID>11720161201040841</DISPATCHNOTIFICATION_ID>
			<DISPATCHNOTIFICATION_DATE>2019-02-13T17:05:45</DISPATCHNOTIFICATION_DATE>
			<DELIVERY_DATE>
				<DELIVERY_START_DATE>2019-02-15</DELIVERY_START_DATE>
				<DELIVERY_END_DATE>2019-02-15</DELIVERY_END_DATE>
			</DELIVERY_DATE>
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
		<!-- 0) EDI mit 1 Position von 4 Items, alles auf derselben Palette
		<!-- 0) EDI with 1 position and 4 items, all on one pallet
		<DISPATCHNOTIFICATION_ITEM>
			<PRODUCT_ID>
				<SUPPLIER_PID xmlns="http://www.bmecat.org/bmecat/2005" type="supplierProductKey">NB-A4</SUPPLIER_PID>
				<INTERNATIONAL_PID xmlns="http://www.bmecat.org/bmecat/2005" type="gtin">04051528394221</INTERNATIONAL_PID>
				<BUYER_PID xmlns="http://www.bmecat.org/bmecat/2005" type="DgProductId">12345678</BUYER_PID>
			</PRODUCT_ID>
			<QUANTITY>4</QUANTITY>
			<ORDER_REFERENCE>
				<ORDER_ID>20266601</ORDER_ID>
			</ORDER_REFERENCE>		
			<LOGISTIC_DETAILS>
				<PACKAGE_INFO>
					<PACKAGE>
						<PACKAGE_ID>00001234560000000011</PACKAGE_ID>
						<PACKAGE_ORDER_UNIT_QUANTITY>4</PACKAGE_ORDER_UNIT_QUANTITY>
					</PACKAGE>
				</PACKAGE_INFO>
			</LOGISTIC_DETAILS>
		</DISPATCHNOTIFICATION_ITEM>
		<!-- 1) EDI mit einer Position von 4 Items auf 2 Pakete mit je 2 Items verteilt
		<!-- 1) EDI with one position of 4 items distributed in 2 packages with 2 items each
		<DISPATCHNOTIFICATION_ITEM>
			<PRODUCT_ID>
				<SUPPLIER_PID xmlns="http://www.bmecat.org/bmecat/2005" type="supplierProductKey">NB-A4</SUPPLIER_PID>
				<INTERNATIONAL_PID xmlns="http://www.bmecat.org/bmecat/2005" type="gtin">04051528394221</INTERNATIONAL_PID>
				<BUYER_PID xmlns="http://www.bmecat.org/bmecat/2005" type="DgProductId">12345678</BUYER_PID>
			</PRODUCT_ID>
			<QUANTITY>4</QUANTITY>
			<ORDER_REFERENCE>
				<ORDER_ID>20266601</ORDER_ID>
			</ORDER_REFERENCE>
			<LOGISTIC_DETAILS>
				<PACKAGE_INFO>
					<PACKAGE>
						<PACKAGE_ID>00001234560000000018</PACKAGE_ID>
						<PACKAGE_ORDER_UNIT_QUANTITY>2</PACKAGE_ORDER_UNIT_QUANTITY>
					</PACKAGE>
					<PACKAGE>
						<PACKAGE_ID>00001234560000000028</PACKAGE_ID>
						<PACKAGE_ORDER_UNIT_QUANTITY>2</PACKAGE_ORDER_UNIT_QUANTITY>
					</PACKAGE>
				</PACKAGE_INFO>
			</LOGISTIC_DETAILS>
		</DISPATCHNOTIFICATION_ITEM>
		<!-- 2) EDI mit 2 Positionen von je 4 Items, alles in gleichem Paket
		<!-- 2) EDI with 2 positions and 4 items of each product all in one package
		<DISPATCHNOTIFICATION_ITEM>
			<PRODUCT_ID>
				<SUPPLIER_PID xmlns="http://www.bmecat.org/bmecat/2005" type="supplierProductKey">NB-A4</SUPPLIER_PID>
				<INTERNATIONAL_PID xmlns="http://www.bmecat.org/bmecat/2005" type="gtin">04051528394221</INTERNATIONAL_PID>
				<BUYER_PID xmlns="http://www.bmecat.org/bmecat/2005" type="DgProductId">12345678</BUYER_PID>
			</PRODUCT_ID>
			<QUANTITY>4</QUANTITY>
			<ORDER_REFERENCE>
				<ORDER_ID>20266601</ORDER_ID>
			</ORDER_REFERENCE>		
			<LOGISTIC_DETAILS>
				<PACKAGE_INFO>
					<PACKAGE>
						<PACKAGE_ID>00001234560000000018</PACKAGE_ID>
						<PACKAGE_ORDER_UNIT_QUANTITY>4</PACKAGE_ORDER_UNIT_QUANTITY>
					</PACKAGE>
				</PACKAGE_INFO>
			</LOGISTIC_DETAILS>
		</DISPATCHNOTIFICATION_ITEM>
		<DISPATCHNOTIFICATION_ITEM>
			<PRODUCT_ID>
				<SUPPLIER_PID xmlns="http://www.bmecat.org/bmecat/2005" type="supplierProductKey">NB-A5</SUPPLIER_PID>
				<INTERNATIONAL_PID xmlns="http://www.bmecat.org/bmecat/2005" type="gtin">05420079900721</INTERNATIONAL_PID>
				<BUYER_PID xmlns="http://www.bmecat.org/bmecat/2005" type="DgProductId">11223344</BUYER_PID>
			</PRODUCT_ID>
			<QUANTITY>4</QUANTITY>
			<ORDER_REFERENCE>
				<ORDER_ID>20266601</ORDER_ID>
			</ORDER_REFERENCE>			
			<LOGISTIC_DETAILS>
				<PACKAGE_INFO>
					<PACKAGE>
						<PACKAGE_ID>00001234560000000018</PACKAGE_ID>
						<PACKAGE_ORDER_UNIT_QUANTITY>4</PACKAGE_ORDER_UNIT_QUANTITY>
					</PACKAGE>
				</PACKAGE_INFO>
			</LOGISTIC_DETAILS>
		</DISPATCHNOTIFICATION_ITEM>
		<!-- 3) EDI mit 2 Positionen von je 4 Items, gemischte Pakete
		<!-- 3) EDI with 2 positions and 4 items of each product, distributed in two packages
		<DISPATCHNOTIFICATION_ITEM>
			<PRODUCT_ID>
				<SUPPLIER_PID xmlns="http://www.bmecat.org/bmecat/2005" type="supplierProductKey">NB-A4</SUPPLIER_PID>
				<INTERNATIONAL_PID xmlns="http://www.bmecat.org/bmecat/2005" type="gtin">04051528394221</INTERNATIONAL_PID>
				<BUYER_PID xmlns="http://www.bmecat.org/bmecat/2005" type="DgProductId">12345678</BUYER_PID>
			</PRODUCT_ID>
			<QUANTITY>4</QUANTITY>
			<ORDER_REFERENCE>
				<ORDER_ID>20266601</ORDER_ID>
			</ORDER_REFERENCE>
			<LOGISTIC_DETAILS>
				<PACKAGE_INFO>
					<PACKAGE>
						<PACKAGE_ID>00001234560000000018</PACKAGE_ID>
						<PACKAGE_ORDER_UNIT_QUANTITY>2</PACKAGE_ORDER_UNIT_QUANTITY>
					</PACKAGE>
					<PACKAGE>
						<PACKAGE_ID>00001234560000000028</PACKAGE_ID>
						<PACKAGE_ORDER_UNIT_QUANTITY>2</PACKAGE_ORDER_UNIT_QUANTITY>
					</PACKAGE>
				</PACKAGE_INFO>
			</LOGISTIC_DETAILS>
		</DISPATCHNOTIFICATION_ITEM>
		<DISPATCHNOTIFICATION_ITEM>
			<PRODUCT_ID>
				<SUPPLIER_PID xmlns="http://www.bmecat.org/bmecat/2005" type="supplierProductKey">NB-A5</SUPPLIER_PID>
				<INTERNATIONAL_PID xmlns="http://www.bmecat.org/bmecat/2005" type="gtin">05420079900721</INTERNATIONAL_PID>
				<BUYER_PID xmlns="http://www.bmecat.org/bmecat/2005" type="DgProductId">11223344</BUYER_PID>
			</PRODUCT_ID>
			<QUANTITY>4</QUANTITY>
			<ORDER_REFERENCE>
				<ORDER_ID>20266601</ORDER_ID>
			</ORDER_REFERENCE>
			<LOGISTIC_DETAILS>
				<PACKAGE_INFO>
					<PACKAGE>
						<PACKAGE_ID>00001234560000000018</PACKAGE_ID>
						<PACKAGE_ORDER_UNIT_QUANTITY>2</PACKAGE_ORDER_UNIT_QUANTITY>
					</PACKAGE>
					<PACKAGE>
						<PACKAGE_ID>00001234560000000028</PACKAGE_ID>
						<PACKAGE_ORDER_UNIT_QUANTITY>2</PACKAGE_ORDER_UNIT_QUANTITY>
					</PACKAGE>
				</PACKAGE_INFO>
			</LOGISTIC_DETAILS>
		</DISPATCHNOTIFICATION_ITEM>
	</DISPATCHNOTIFICATION_ITEM_LIST>
</DISPATCHNOTIFICATION>
```
we will discuss the point of logistics details 
as each one have the package info and the id of the pacakge and the quantity it's carried

so this an example of one package carry four units of product of the same order

lets discuss another examples like 

1 proudct in four package will be like this
```xml
 <DISPATCHNOTIFICATION_ITEM>
      <PRODUCT_ID>
        <SUPPLIER_PID xmlns="http://www.bmecat.org/bmecat/2005">4251421947955</SUPPLIER_PID>
        <INTERNATIONAL_PID xmlns="http://www.bmecat.org/bmecat/2005">04251421947955</INTERNATIONAL_PID>
        <BUYER_PID xmlns="http://www.bmecat.org/bmecat/2005">38699770</BUYER_PID>
      </PRODUCT_ID>
      <QUANTITY>1</QUANTITY>
      <ORDER_REFERENCE>
        <ORDER_ID>96284599</ORDER_ID>
      </ORDER_REFERENCE>
      <LOGISTIC_DETAILS>
        <PACKAGE_INFO>
          <PACKAGE>
            <PACKAGE_ID>01145036553466</PACKAGE_ID>
            <PACKAGE_QUANTITY>1</PACKAGE_QUANTITY>
          </PACKAGE>
          <PACKAGE>
            <PACKAGE_ID>01145036553467</PACKAGE_ID>
            <PACKAGE_QUANTITY>1</PACKAGE_QUANTITY>
          </PACKAGE>
          <PACKAGE>
            <PACKAGE_ID>01395039296258</PACKAGE_ID>
            <PACKAGE_QUANTITY>1</PACKAGE_QUANTITY>
          </PACKAGE>
          <PACKAGE>
            <PACKAGE_ID>01465079795732</PACKAGE_ID>
            <PACKAGE_QUANTITY>1</PACKAGE_QUANTITY>
          </PACKAGE>
        </PACKAGE_INFO>
      </LOGISTIC_DETAILS>
    </DISPATCHNOTIFICATION_ITEM>
```

2 diffrent product in 2 pakages each
```xml
 <DISPATCHNOTIFICATION_ITEM_LIST>
    <DISPATCHNOTIFICATION_ITEM>
      <PRODUCT_ID>
        <SUPPLIER_PID xmlns="http://www.bmecat.org/bmecat/2005">4251421947955</SUPPLIER_PID>
        <INTERNATIONAL_PID xmlns="http://www.bmecat.org/bmecat/2005">04251421947955</INTERNATIONAL_PID>
        <BUYER_PID xmlns="http://www.bmecat.org/bmecat/2005">38699770</BUYER_PID>
      </PRODUCT_ID>
      <QUANTITY>1</QUANTITY>
      <ORDER_REFERENCE>
        <ORDER_ID>96284599</ORDER_ID>
      </ORDER_REFERENCE>
      <LOGISTIC_DETAILS>
        <PACKAGE_INFO>
          <PACKAGE>
            <PACKAGE_ID>01145036553466</PACKAGE_ID>
            <PACKAGE_QUANTITY>1</PACKAGE_QUANTITY>
          </PACKAGE>
          <PACKAGE>
            <PACKAGE_ID>01145036553467</PACKAGE_ID>
            <PACKAGE_QUANTITY>1</PACKAGE_QUANTITY>
          </PACKAGE>
        </PACKAGE_INFO>
      </LOGISTIC_DETAILS>
    </DISPATCHNOTIFICATION_ITEM>
	<DISPATCHNOTIFICATION_ITEM>
      <PRODUCT_ID>
        <SUPPLIER_PID xmlns="http://www.bmecat.org/bmecat/2005">4251421947956</SUPPLIER_PID>
        <INTERNATIONAL_PID xmlns="http://www.bmecat.org/bmecat/2005">04251421947956</INTERNATIONAL_PID>
        <BUYER_PID xmlns="http://www.bmecat.org/bmecat/2005">38699771</BUYER_PID>
      </PRODUCT_ID>
      <QUANTITY>1</QUANTITY>
      <ORDER_REFERENCE>
        <ORDER_ID>96284599</ORDER_ID>
      </ORDER_REFERENCE>
      <LOGISTIC_DETAILS>
        <PACKAGE_INFO>
          <PACKAGE>
            <PACKAGE_ID>01395039296258</PACKAGE_ID>
            <PACKAGE_QUANTITY>1</PACKAGE_QUANTITY>
          </PACKAGE>
          <PACKAGE>
            <PACKAGE_ID>01465079795732</PACKAGE_ID>
            <PACKAGE_QUANTITY>1</PACKAGE_QUANTITY>
          </PACKAGE>
        </PACKAGE_INFO>
      </LOGISTIC_DETAILS>
    </DISPATCHNOTIFICATION_ITEM>
  </DISPATCHNOTIFICATION_ITEM_LIST>
```
and last is 3 diffrent product in 3 diffrent packages
```xml
  <DISPATCHNOTIFICATION_ITEM_LIST>
    <DISPATCHNOTIFICATION_ITEM>
      <PRODUCT_ID>
        <SUPPLIER_PID xmlns="http://www.bmecat.org/bmecat/2005">4251421947955</SUPPLIER_PID>
        <INTERNATIONAL_PID xmlns="http://www.bmecat.org/bmecat/2005">04251421947955</INTERNATIONAL_PID>
        <BUYER_PID xmlns="http://www.bmecat.org/bmecat/2005">38699770</BUYER_PID>
      </PRODUCT_ID>
      <QUANTITY>1</QUANTITY>
      <ORDER_REFERENCE>
        <ORDER_ID>96284588</ORDER_ID>
      </ORDER_REFERENCE>
      <LOGISTIC_DETAILS>
        <PACKAGE_INFO>
          <PACKAGE>
            <PACKAGE_ID>01145036553466</PACKAGE_ID>
            <PACKAGE_QUANTITY>1</PACKAGE_QUANTITY>
          </PACKAGE>
        </PACKAGE_INFO>
      </LOGISTIC_DETAILS>
    </DISPATCHNOTIFICATION_ITEM>
	<DISPATCHNOTIFICATION_ITEM>
      <PRODUCT_ID>
        <SUPPLIER_PID xmlns="http://www.bmecat.org/bmecat/2005">4251421947956</SUPPLIER_PID>
        <INTERNATIONAL_PID xmlns="http://www.bmecat.org/bmecat/2005">04251421947956</INTERNATIONAL_PID>
        <BUYER_PID xmlns="http://www.bmecat.org/bmecat/2005">38699771</BUYER_PID>
      </PRODUCT_ID>
      <QUANTITY>1</QUANTITY>
      <ORDER_REFERENCE>
        <ORDER_ID>96284588</ORDER_ID>
      </ORDER_REFERENCE>
      <LOGISTIC_DETAILS>
        <PACKAGE_INFO>
          <PACKAGE>
            <PACKAGE_ID>01395039296258</PACKAGE_ID>
            <PACKAGE_QUANTITY>1</PACKAGE_QUANTITY>
          </PACKAGE>
        </PACKAGE_INFO>
      </LOGISTIC_DETAILS>
    </DISPATCHNOTIFICATION_ITEM>
	<DISPATCHNOTIFICATION_ITEM>
      <PRODUCT_ID>
        <SUPPLIER_PID xmlns="http://www.bmecat.org/bmecat/2005">4251421947957</SUPPLIER_PID>
        <INTERNATIONAL_PID xmlns="http://www.bmecat.org/bmecat/2005">04251421947957</INTERNATIONAL_PID>
        <BUYER_PID xmlns="http://www.bmecat.org/bmecat/2005">38699772</BUYER_PID>
      </PRODUCT_ID>
      <QUANTITY>1</QUANTITY>
      <ORDER_REFERENCE>
        <ORDER_ID>96284588</ORDER_ID>
      </ORDER_REFERENCE>
      <LOGISTIC_DETAILS>
        <PACKAGE_INFO>
          <PACKAGE>
            <PACKAGE_ID>01465079795732</PACKAGE_ID>
            <PACKAGE_QUANTITY>1</PACKAGE_QUANTITY>
          </PACKAGE>
        </PACKAGE_INFO>
      </LOGISTIC_DETAILS>
    </DISPATCHNOTIFICATION_ITEM>
  </DISPATCHNOTIFICATION_ITEM_LIST>```

|     |     |     |     |     |
| --- | --- | --- | --- | --- |
| **XML Element** | **M/C/S** | **Sample values** | [**Data type**](https://confdg.atlassian.net/wiki/spaces/MKT/pages/169133966273 "https://confdg.atlassian.net/wiki/spaces/MKT/pages/169133966273") **\[maxLength\]** | **Description** |
| . . . |     |     |     |     |
| . . LOGISTIC\_DETAILS | Must |     |     | **Warehouse deliveries (supplier)**  <br>\* = If this element can be sent, its sub-elements are must fields and the SSCC requirements described must be met.<br><br>**Direct deliveries (supplier & dealer)**  <br>\* = Basically this element and its sub-elements should not be sent. However, it can be used to specify different tracking codes per item. |
| . . . PACKAGE\_INFO | Must |     |     |     |
| . . . . . PACKAGE | Must |     |     |     |
| . . . . . . PACKAGE\_ID | Must | 00001234560000000028, 99.00.123456.12345678 | dtSTRING\[50\] | **Best**: SSCC  <br>**Minimum requirement**: barcode value that is present on the SSCC label on the pallet  <br>Alternatively, the shipping label of the courier or shipping company can be used (shipment number/barcode value as PACKAGE\_ID)  <br>Compatible code type: **GS1-128**  <br>The scannable code must correspond exactly to the PACKAGE\_ID  <br>The value must be unique and must not be repeated within one year<br><br>![Warning](https://pf-emoji-service--cdn.us-east-1.prod.public.atl-paas.net/atlassian/productivityEmojis/exclamation-32px.png) For direct deliveries (if LOGISTIC\_DETAILS are sent), the consignment number must be transmitted here  <br>![light bulb](https://pf-emoji-service--cdn.us-east-1.prod.public.atl-paas.net/standard/ef8b0642-7523-4e13-9fd3-01b65648acf6/32x32/1f4a1.png) Several PACKAGE elements can be used to transmit different consignment numbers (parcels) in a DELR |
| . . . . . . PACKAGE\_ORDER\_UNIT\_QUANTITY | Must | 3   | dtNUMBER | Number of pieces of the item present in the package / on the pallet |
| . . . |     |     |     |     |