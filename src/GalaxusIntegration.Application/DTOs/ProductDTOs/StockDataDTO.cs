using System;
using System.Diagnostics.CodeAnalysis;

namespace GalaxusIntegration.Application.DTOs.ProductDTOs
{
    public class StockDataDto
    {
        [NotNull]
        public string ProviderKey { get; set; }
        [NotNull]
        public int QuantityOnStock { get; set; }
        public DateTime RestockTime { get; set; }
        public DateTime RestockDate { get; set; }
        public int MinimumOrderQuantity { get; set; }
        public int OrderQuantitySteps { get; set; }
        public int TradeUnit { get; set; }
        public int LogisticUnit { get; set; }
        [NotNull]
        public string WarehouseCountry { get; set; }
        public bool DirectDeliverySupported { get; set; }

        // Private constructor for the builder to use
        private StockDataDto(string providerKey, int quantityOnStock, DateTime restockTime, DateTime restockDate, int minimumOrderQuantity, int orderQuantitySteps, int tradeUnit, int logisticUnit, string warehouseCountry, bool directDeliverySupported)
        {
            ProviderKey = providerKey;
            QuantityOnStock = quantityOnStock;
            RestockTime = restockTime;
            RestockDate = restockDate;
            MinimumOrderQuantity = minimumOrderQuantity;
            OrderQuantitySteps = orderQuantitySteps;
            TradeUnit = tradeUnit;
            LogisticUnit = logisticUnit;
            WarehouseCountry = warehouseCountry;
            DirectDeliverySupported = directDeliverySupported;
        }

        // Fluent Builder
        public class StockDataDtoBuilder
        {
            private string _providerKey;
            private int _quantityOnStock;
            private DateTime _restockTime;
            private DateTime _restockDate;
            private int _minimumOrderQuantity;
            private int _orderQuantitySteps;
            private int _tradeUnit;
            private int _logisticUnit;
            private string _warehouseCountry;
            private bool _directDeliverySupported;

            private bool _restockDateSet;
            private bool _restockTimeSet;

            public StockDataDtoBuilder WithProviderKey(string providerKey)
            {
                _providerKey = providerKey;
                return this;
            }

            public StockDataDtoBuilder WithQuantityOnStock(int quantityOnStock)
            {
                _quantityOnStock = quantityOnStock;
                return this;
            }

            // Setting RestockTime (and disallowing setting RestockDate if RestockTime is set)
            public StockDataDtoBuilder WithRestockTime(DateTime restockTime)
            {
                if (_restockDateSet)
                {
                    throw new InvalidOperationException("Cannot set both RestockTime and RestockDate. Choose one.");
                }

                _restockTime = restockTime;
                _restockTimeSet = true;
                return this;
            }

            // Setting RestockDate (and disallowing setting RestockTime if RestockDate is set)
            public StockDataDtoBuilder WithRestockDate(DateTime restockDate)
            {
                if (_restockTimeSet)
                {
                    throw new InvalidOperationException("Cannot set both RestockTime and RestockDate. Choose one.");
                }

                _restockDate = restockDate;
                _restockDateSet = true;
                return this;
            }

            public StockDataDtoBuilder WithMinimumOrderQuantity(int minimumOrderQuantity)
            {
                _minimumOrderQuantity = minimumOrderQuantity;
                return this;
            }

            public StockDataDtoBuilder WithOrderQuantitySteps(int orderQuantitySteps)
            {
                _orderQuantitySteps = orderQuantitySteps;
                return this;
            }

            public StockDataDtoBuilder WithTradeUnit(int tradeUnit)
            {
                _tradeUnit = tradeUnit;
                return this;
            }

            public StockDataDtoBuilder WithLogisticUnit(int logisticUnit)
            {
                _logisticUnit = logisticUnit;
                return this;
            }

            public StockDataDtoBuilder WithWarehouseCountry(string warehouseCountry)
            {
                _warehouseCountry = warehouseCountry;
                return this;
            }

            public StockDataDtoBuilder WithDirectDeliverySupported(bool directDeliverySupported)
            {
                _directDeliverySupported = directDeliverySupported;
                return this;
            }

            // Build method to create the final object
            public StockDataDto Build()
            {
                // Ensure that either RestockTime or RestockDate is set
                if (!_restockTimeSet && !_restockDateSet)
                {
                    throw new InvalidOperationException("Either RestockTime or RestockDate must be set.");
                }

                return new StockDataDto(_providerKey, _quantityOnStock, _restockTime, _restockDate, _minimumOrderQuantity, _orderQuantitySteps, _tradeUnit, _logisticUnit, _warehouseCountry, _directDeliverySupported);
            }
        }

        public List<string> DataToString()
        {
            List<string> result = new();
            result.Add(ProviderKey);
            result.Add(QuantityOnStock.ToString());
            if(RestockTime!=DateTime.MinValue)
            result.Add(RestockTime != default ? RestockTime.ToString("yyyy-MM-dd HH:mm:ss") : "");
            else if(RestockDate!=DateTime.MinValue)
                result.Add(RestockDate != default ? RestockDate.ToString("yyyy-MM-dd") : "");
            result.Add(MinimumOrderQuantity.ToString());
            result.Add(OrderQuantitySteps.ToString());
            result.Add(TradeUnit.ToString());
            result.Add(LogisticUnit.ToString());
            result.Add(WarehouseCountry);
            result.Add(DirectDeliverySupported ? "1" : "0");
            return result;
        }

        public List<string> HeadList()
        {
            List<string> result = new();
            result.Add("ProviderKey");
            result.Add("QuantityOnStock");
            if(RestockTime!=DateTime.MinValue)
            result.Add("RestockTime");
            if(RestockDate!=DateTime.MinValue)
            result.Add("RestockDate");
            result.Add("MinimumOrderQuantity");
            result.Add("OrderQuantitySteps");
            result.Add("TradeUnit");
            result.Add("LogisticUnit");
            result.Add("WarehouseCountry");
            result.Add("DirectDeliverySupported");
            return result;
        }
    }
}
