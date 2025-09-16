# Galaxus Integration Documentation - Comprehensive Analysis

## 📋 Executive Summary

**Galaxus** is a major Swiss marketplace (part of Digitec Galaxus AG) that operates through **FTP-based file exchange** rather than APIs. The integration follows a structured 3-stage product listing process and XML-based order management system.

### Key Points:
- **No API Available** (as of 8/22/2025) - Only FTP/SFTP integration
- **File-based communication** - CSV/XLSX for products, XML for orders
- **3-Stage Product Process** - Progressive data enrichment
- **Bidirectional folders** - `dg2Partner` (incoming) and `Partner2DG` (outgoing)

---

## 🏗️ System Architecture Overview

```
┌─────────────────────────────────────────────────────────────┐
│                     YOUR SYSTEM                              │
├─────────────────────────────────────────────────────────────┤
│  Products DB  │  Orders DB  │  Inventory  │  Price Engine   │
└────────┬──────────────┬──────────────┬──────────────┬───────┘
         │              │              │              │
         └──────────────┴──────────────┴──────────────┘
                                │
                         [CSV/XML Files]
                                │
                    ┌───────────┴───────────┐
                    │    FTP/SFTP Server    │
                    │ ftp-partner.digitec... │
                    └───────────┬───────────┘
                                │
                ┌───────────────┴───────────────┐
                │                               │
           ┌────▼────┐                    ┌────▼────┐
           │dg2Partner│                    │Partner2DG│
           │ (FROM)  │                    │  (TO)   │
           └─────────┘                    └─────────┘
                │                               │
      Orders, Cancellations,          Responses, Confirmations,
      Returns (XML)                   Shipments, Invoices (XML)
```

---

## 📁 Stage-Based Product Integration Process

### 🔹 **Stage 1: Product Master Data**
**Purpose:** Create product listings with basic information

**Key Fields:**
- `ProviderKey` - Your unique SKU (max 100 chars, ASCII 32-126)
- `Gtin` - EAN/UPC code (8, 12, 13, or 14 digits with valid check digit)
- `BrandName` - Must be capitalized properly
- `ProductCategory` - Their category system
- `ProductTitle_de` - German title (required, max 100 chars)
- `LongDescription_de` - German description (max 4000 chars)
- `MainImageUrl` - Primary product image (HTTPS, max 300 chars)

**Important Rules:**
- ProviderKey must be consistent across ALL files
- No special characters outside ASCII 32-126
- Images must be permanently accessible (not cloud/temporary URLs)
- Descriptions should NOT include HTML, links, or brand names

### 🔹 **Stage 2: Commercial Data (Price & Stock)**
**Purpose:** Set pricing, availability, and delivery terms

**Key Fields:**
- `ProviderKey` - Must match Stage 1
- `Price` - Product price (must be > 0)
- `StockQuantity` - Available inventory (>= 0)
- `DeliveryTimeDays` - Shipping time
- `MOQ` - Minimum Order Quantity (default: 1)
- `OQS` - Order Quantity Step (default: 1)
- `Currency` - CHF for Switzerland
- `VAT_Rate` - 7.7% for Switzerland

**Critical Notes:**
- Updates every 15 minutes recommended
- Remove End-of-Life (EoL) products
- MOQ/OQS important for bulk items

### 🔹 **Stage 3: Product Specifications**
**Purpose:** Add detailed attributes and variants

**Structure:**
```
ProviderKey | SpecificationKey | SpecificationValue
ABC123      | Color           | Red
ABC123      | Size            | Large
ABC123      | Material        | Cotton
```

**Requirements:**
- File MUST be sorted by ProviderKey
- SpecificationKey max 200 characters
- Used for filters and product variants
- Optional but improves product visibility

---

## 📦 Order Processing Workflow

### Order Flow Sequence:

```
1. ORDER RECEIVED (GORDP_*.xml)
   ↓
2. ORDER RESPONSE (GORDRE_*.xml)
   ↓
3. DISPATCH NOTIFICATION (GDISPN_*.xml)
   ↓
4. INVOICE (GINVCE_*.xml)
   ↓
5. [Optional] CANCELLATION/RETURN flows
```

### XML File Naming Convention:

| Document Type | Direction | Naming Pattern | Example |
|---|---|---|---|
| Order | dg2Partner → You | `GORDP_OrderId_Timestamp.xml` | GORDP_9316271_20240115.xml |
| Order Response | You → Partner2DG | `GORDRE_SupplierId_OrderId_Timestamp.xml` | GORDRE_1928008_9316271_20240115143022.xml |
| Dispatch Notice | You → Partner2DG | `GDISPN_SupplierId_ShipmentId_Timestamp.xml` | GDISPN_1928008_SH123_20240116.xml |
| Invoice | You → Partner2DG | `GINVCE_SupplierId_InvoiceId_Timestamp.xml` | GINVCE_1928008_INV456_20240116.xml |
| Cancel Request | dg2Partner → You | `GCANP_SupplierId_OrderId_Timestamp.xml` | GCANP_1928008_9316271_20240115.xml |
| Cancel Confirm | You → Partner2DG | `GCANCO_SupplierId_OrderId_Timestamp.xml` | GCANCO_1928008_9316271_20240115.xml |

### XML Standards:
- OpenTRANS 2.1 namespace: `http://www.opentrans.org/XMLSchema/2.1`
- BMEcat namespace: `http://www.bmecat.org/bmecat/2005`
- UTF-8 encoding required
- All addresses use German country names

---

## 🔌 FTP/SFTP Connection Details

### Configuration:
```
Host: ftp-partner.digitecgalaxus.ch
Port: 22
Protocol: SFTP (SSH File Transfer Protocol)
Max Parallel Sessions: 5

IP Addresses (for whitelisting):
- 88.198.35.84
- 85.10.200.14
- 116.203.25.19
```

### Critical FTP Rules:
1. **Max 5 parallel connections** - Exceeding causes connection drops
2. **Wait for success confirmation** - Prevent data loss
3. **Empty files** - Prefix with `*` (e.g., `*.csv`)
4. **Delete after processing** - Clean up to avoid reprocessing
5. **Never rename files** - Always replace with same name
6. **No directory structure changes** - Keep folder structure intact

---

## ⚠️ Data Quality Requirements

### File Format Rules:

| Requirement | Details | Impact if Violated |
|---|---|---|
| **No Excel formatting** | No colors, styles, or formatting | File parsing errors |
| **UTF-8 encoding** | Must be UTF-8 without BOM | Character corruption |
| **No line breaks in headers** | Single line headers only | Import failure |
| **Consistent separators** | Use configured separator throughout | Parsing errors |
| **Quote text fields** | Enclose descriptions in quotes | Data truncation |
| **No scientific notation** | Keep numbers as text if needed | GTIN/SKU corruption |

### Provider Type Considerations:

| Type | Description | Special Requirements |
|---|---|---|
| **Marketplace - CH** | Swiss merchant | Standard requirements |
| **Marketplace EU-Hub (CH)** | EU merchant with CH representation | + Weight, TARIC codes |
| **Marketplace EU-Hub (EU)** | EU merchant without CH representation | + Weight, TARIC, Country of Origin |
| **Retail (CH)** | Swiss supplier | Different pricing model |
| **Retail (Non-CH)** | International supplier | Additional customs data |

---

## 🛠️ Validation Tools & Error Handling

### Galaxus Feedchecker Tool:
- Desktop application for pre-validation
- Checks file structure and data formats
- Identifies errors before upload
- Validates image URLs accessibility

### Common Errors to Avoid:

| Error Type | Cause | Prevention |
|---|---|---|
| **Duplicate ProviderKey** | Same SKU multiple times | Implement deduplication |
| **Invalid GTIN** | Wrong check digit | Validate GTIN algorithm |
| **Missing columns** | Changed file structure | Never modify headers |
| **Parsing errors** | Unescaped quotes/delimiters | Proper CSV escaping |
| **File not sorted** | Stage 3 not sorted by ProviderKey | Sort before upload |
| **Scientific notation** | Excel auto-formatting | Format as text |

### Error Notification Flow:
1. Galaxus processes file
2. If errors found → Email sent to configured address
3. File contains error details and line numbers
4. Fix and re-upload same filename
5. Processing resumes automatically

---

## 📊 Business Process Considerations

### Product Lifecycle:
```
1. NEW PRODUCT
   ├─ Stage 1: Create listing
   ├─ Stage 2: Set price/stock
   └─ Stage 3: Add specifications
   
2. ACTIVE PRODUCT
   ├─ Update price (every 15 min)
   ├─ Update stock (every 15 min)
   └─ Update images (as needed)
   
3. END OF LIFE
   └─ Remove from price/stock file (auto-delists)
```

### Order Fulfillment Timeline:
- **T+0**: Order received → Send response within 30 min
- **T+1**: Prepare shipment → Send dispatch notification
- **T+1**: Ship package → Include tracking number
- **T+2**: Send invoice → PDF attachment required

### Cancellation Handling:
- Galaxus can cancel until shipment
- You must respond to cancel requests
- Can accept or reject with reason
- Must implement if cancel requests are enabled

---

## 💡 Best Practices & Recommendations

### Data Management:
1. **Version control your mappings** - Keep field mapping documentation
2. **Implement data validation** - Check before uploading
3. **Archive processed files** - Keep 90-day history
4. **Log all transactions** - For audit trail
5. **Monitor error emails** - Set up alerts

### Performance Optimization:
1. **Batch updates efficiently** - Group similar operations
2. **Use all 5 FTP sessions** - For parallel processing
3. **Compress large files** - If supported
4. **Schedule during off-peak** - 2-5 AM Swiss time
5. **Implement retry logic** - Handle transient failures

### Integration Strategy:
1. **Start with Stage 1 only** - Get products listed
2. **Add Stage 2 after validation** - Enable purchasing
3. **Implement Stage 3 selectively** - For key products
4. **Test with few products first** - Validate process
5. **Scale gradually** - Monitor performance

---

## 📝 Implementation Checklist

### Phase 1: Setup & Testing
- [ ] Receive FTP credentials from Galaxus
- [ ] Configure FTP client (FileZilla)
- [ ] Download Feedchecker tool
- [ ] Understand file structures from templates
- [ ] Map internal data to Galaxus format
- [ ] Create test files with 5-10 products

### Phase 2: Product Integration
- [ ] Implement Stage 1 file generation
- [ ] Validate with Feedchecker
- [ ] Upload and verify listing
- [ ] Implement Stage 2 (price/stock)
- [ ] Set up automated updates
- [ ] Add Stage 3 specifications

### Phase 3: Order Management
- [ ] Parse incoming orders (XML)
- [ ] Generate order responses
- [ ] Implement dispatch notifications
- [ ] Create invoice generation
- [ ] Handle cancellations
- [ ] Process returns

### Phase 4: Automation
- [ ] Schedule regular updates
- [ ] Implement error handling
- [ ] Set up monitoring
- [ ] Create alerting system
- [ ] Document procedures
- [ ] Train operations team

---

## 🔍 Key Insights & Gotchas

### Critical Success Factors:
1. **ProviderKey consistency** - This links everything together
2. **Image accessibility** - Galaxus retries 5 times then gives up
3. **File naming** - Must follow exact patterns
4. **Timing** - Quick response to orders expected
5. **Data quality** - Poor data = poor sales

### Hidden Complexities:
- **Character encoding issues** - Excel can corrupt UTF-8
- **GTIN validation** - Not just length, check digit matters
- **Address formatting** - Different for B2B vs B2C
- **VAT handling** - Different rates for different products
- **Multi-language** - DE required, FR/IT/EN optional but recommended

### Cost-Benefit Analysis:
- **Investment**: 2-3 weeks development, ongoing maintenance
- **Benefits**: Access to Swiss market, automated order flow
- **Risks**: Data quality issues, inventory synchronization
- **ROI**: Depends on product margin and volume

---

## 📚 Next Steps

1. **Request access** - Contact Galaxus partnership team
2. **Prepare data mapping** - Document field transformations
3. **Design architecture** - Plan system integration points
4. **Build prototype** - Start with minimal viable integration
5. **Test thoroughly** - Use Feedchecker and test environment
6. **Deploy gradually** - Start with subset of products
7. **Monitor & optimize** - Track performance metrics

---

## 🤝 Support Resources

- **Partner Portal**: Access after onboarding
- **Technical Contact**: pdi@digitecgalaxus.ch
- **Feedchecker Tool**: Provided by Galaxus
- **Documentation**: Confluence (access provided)
- **Error Reports**: Sent to configured email

---

*This documentation represents a comprehensive understanding of the Galaxus integration requirements based on the provided materials. Implementation should follow these guidelines while adapting to specific business needs.*