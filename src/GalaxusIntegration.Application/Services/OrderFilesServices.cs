using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using GalaxusIntegration.Application.DTOs.Order_Coming_Requests;
using GalaxusIntegration.Application.DTOs.PartialDTOs;
using GalaxusIntegration.Application.XML;

namespace GalaxusIntegration.Application.Services
{
    public class OrderFilesServices
    {
        public OrderFilesServices() { }
        // Methods to handle order files will be implemented here in the future
        public async Task<string> XMLGenerateTest()
        {/*
            PartyIdDTO partyId = new PartyIdDTO
            {
                Id = "12345",
                Type = "GLN"
            };
            PartyIdDTO partyId2 = new PartyIdDTO
            {
                Id = "54321",
                Type = "NLG"
            };
            ContactDetailsDTO contact = new ContactDetailsDTO
            {
                ContactName = "Ahmed Ali",
                FirstName = "Ahmed",
                Title = "Ali"
            };
            AddressDTO address = new AddressDTO
            {
                Street = "Main Street",
                BoxNo = "123",
                City = "Metropolis",
                Zip = "12345",
                Country = "US",
                Department = "Sales",
                CountryCoded = "NY",
                Email = "aa@bb.com",
                Phone = "1234567890",
                VATID = "0987654321",
                Name = "ahmed",
                Name2 = "ali",
                Name3 = "hassan",
                ContactDetails = contact

            };
            var party = new PartyDTO
            {
                Party_Role = "12345",
                Party_ID=new List<PartyIdDTO>(),
                Address = address
            };
            party.Party_ID.Add(partyId);
            party.Party_ID.Add(partyId2);
            ControlInfo controlInfo = new()
            {
              GenrationDate = DateTime.Parse("2022-03-28T11:31:27")
            };
            List<PartyDTO>listParties = new();
            listParties.Add(party);
            CustomerOrderRefernce customerOrderRefernce = new()
            {
                OrderId = 61730203
            };
            OrderPartiesRefrence orderPartiesRefrence = new()
            {
                ByuerIdRef = "406802",
                SupplierIdRef = "407514"
            };
            HeaderUDX headerUDX = new()
            {
                CustomerType = "private_customer",
                DeliveryType = "direct_delivery",
                IsCollectiveOrder = false,
                PhysicalDeliveryNoteRequired = false
            };
            OrderInfo orderInfo = new()
            {
                Parties = listParties,
                OrderId = 61730204,
                OrderDate = DateTime.Parse("2022-03-28T11:31:19"),
                Language = "ger",
                Currency = "CHF",
                CustomerOrderRefernce = customerOrderRefernce,
                OrderPartiesRefrence = orderPartiesRefrence,
                HeaderUDX= headerUDX


            };
            OrderHeader header = new()
            {
                ControlInfo = controlInfo,
                OrderInfo = orderInfo

            };
            OrderSummary summary = new()
            {
                TotalItemNum = 1,
                TotalAmount = 1390.33,
            };
            ProductDetails productDetails = new()
            {
                SupplierPId = "4135378",
                InternationalPId = "00889842737240",
                BuyerPId = "15395678",
                DescriptionShort = "Microsoft Surface Laptop 4 (13.50 \", Intel Core i7-1185G7, 16 GB, 512 GB)"
            };
            TaxDetailsFix taxDetailsFix = new()
            {
                TaxAmount = 107.06
            };
            ProductPriceFix productPriceFix = new()
            {
                PriceAmount = 1390.33,
                TaxDetailsFix = taxDetailsFix,

            };
            DeliveryDate deliveryDate = new()
            {
                DeliveryStartDate = DateTime.Parse("2022-07-20"),
                DeliveryEndDate =  DateTime.Parse("2022-07-20"),
            };
            OrderItem orderItem1= new()
            {
                LineItemId = 1,
                OrderUnit = "C62",
                PriceLineAmount = 1390.33,
                Quantity = 1,
                ProductId = productDetails,
                ProductPriceFix = productPriceFix,
                DeliveryDate = deliveryDate,

            };
            List<OrderItem> orderItems = new();
            orderItems.Add(orderItem1);
            ReceiveOrderDTO order = new ReceiveOrderDTO
            {
                OrderHeader = header,
                OrderItemList = orderItems,
               OrderSummary = summary,
             
            };
         
            var xmlBuilder = new XMLBuilder("Order");
            var xmlString = xmlBuilder.BuildXML(order);

            return xmlString;
            */ return "test";

        }
    }
}
