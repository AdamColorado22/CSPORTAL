using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apiweb.Models
{
    public class orderDetail
    {
        //public string CUST_ORDER_ID { get; set; }
        public int LINE_NO { get; set; }
        public string PART_ID { get; set; }
        public string ORDER_QTY { get; set; }
        public string USER_ORDER_QTY { get; set; }
        public string SELLING_UM { get; set; }
        public string UNIT_PRICE { get; set; }
        public string MISC_REFERENCE { get; set; }
        public string PRODUCT_CODE { get; set; }
        public string COMMODITY_CODE { get; set; }
        public string GL_REVENUE_ACCT_ID { get; set; }
        public string LAST_SHIPPED_DATE { get; set; }
        public string TOTAL_SHIPPED_QTY { get; set; }
        public string TOTAL_USR_SHIP_QTY { get; set; }
        public string TOTAL_AMT_ORDERED { get; set; }
        public string TOTAL_AMT_SHIPPED { get; set; }
        public string SERVICE_CHARGE_ID { get; set; }
        public string VAT_CODE { get; set; }
        public string WAREHOUSE_ID { get; set; }
        //public string WIP_VAS_UNIT_PRICE { get; set; }
        //public string ACCEPTED_EARLY { get; set; }
        //public string DAYS_EARLY { get; set; }
        public string USER_1 { get; set; }
        public string ORIGIN_STAGE_REVISION_ID { get; set; }
        public string STATUS_EFF_DATE { get; set; }
        public string ENTERED_BY { get; set; }

    }
}