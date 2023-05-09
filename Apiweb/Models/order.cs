using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apiweb.Models
{
    public class order
    {
        public string inv { get; set; }
        public bool head_status { get; set; }
        public string baseNombre { get; set; }
        public string order_Id { get; set; }
        public string CUSTOMER_ID { get; set; }
        //public string CUSTOMER_PO_REF { get; set; }
        public string CONTACT_FIRST_NAME { get; set; }
        public string CONTACT_LAST_NAME { get; set; }
        //public string CONTACT_INITIAL { get; set; }
        //public string CONTACT_POSITION { get; set; }
        public string CONTACT_HONORIFIC { get; set; }
        public string CONTACT_SALUDATION { get; set; }
        public string CONTACT_PHONE { get; set; }
        public string CONTACT_FAX { get; set; }
        public string SHIP_TO_ADDR_NO { get; set; }
        public string TERRITORY { get; set; }
        public string SALESREP_ID { get; set; }
        public string SITE_ID { get; set; }
        public string TERMS_NET_TYPE { get; set; }
        public string TERMS_NET_DAYS { get; set; }
        public string TERMS_DISC_TYPE { get; set; }
        public string TERMS_DESCRIPTION { get; set; }
        public string ORDER_DATE { get; set; }
        public string DESIRED_SHIP_DATE { get; set; }
        public string LAST_SHIPPED_DATE { get; set; }
        public string TOTAL_AMT_ORDERED { get; set; }
        public string TOTAL_AMT_SHIPPED { get; set; }
        public string PROMISE_DATE { get; set; }
        //public string EDI_BLANKET_FLAG { get; set; }
        public string SHIPTO_ID { get; set; }
        public string CURRENCY_ID { get; set; }
        public string CARRIER_ID { get; set; }
        public string CONTACT_MOBILE { get; set; }
        public string CREATE_DATE { get; set; }
        public string USER_5 { get; set; }
        //public string USER_8 { get; set; }
        public string USER_9 { get; set; }
        public string STATUS_EFF_DATE { get; set; }
        public string TERMS_ID { get; set; }
        public string CONTACT_ID { get; set; }
        public string ENTERED_BY { get; set; }

        public List<orderDetail> oreder_detail { get; set; }


    }
}