
using log4net;
//using Lsa.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Apiweb.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Mvc;

namespace Apiweb.Controllers
{
    public class orderController : Controller
    {

        ILog log = log4net.LogManager.GetLogger(typeof(orderController));


     
        public ActionResult Pedido( order order)
        {

            //    Lsa.Shared.GetSingleSignOnData SSOData = new Lsa.Shared.GetSingleSignOnData();
            //    string inv = "";
            //     //log.Debug("Se reporta orden para: " + order.baseNombre);

            //    try
            //    {
            //        LoginDB(order.baseNombre);
            //        Lsa.Vmfg.Sales.CustomerOrder Cust_Order = new Lsa.Vmfg.Sales.CustomerOrder(order.baseNombre);
            //        Cust_Order.Load("");
            //        // Console.WriteLine(inv);
            //        Lsa.Data.DataRow orderRow = null;
            //        if (order.head_status)
            //        {
            //            // Lsa.Data.DataRow orderRow = null;
            //            orderRow = Cust_Order.NewOrderRow(order.inv);
            //            orderRow["CUSTOMER_ID"] = order.CUSTOMER_ID;
            //            orderRow["CONTACT_FIRST_NAME"] = order.CONTACT_FIRST_NAME;
            //            orderRow["CONTACT_LAST_NAME"] = order.CONTACT_LAST_NAME;
            //            orderRow["CONTACT_HONORIFIC"] = order.CONTACT_HONORIFIC;
            //            //orderRow["CONTACT_SALUDATION"] = order.CONTACT_SALUDATION;
            //            orderRow["CONTACT_PHONE"] = order.CONTACT_PHONE;
            //            //orderRow["CONTACT_FAX"] = order.CONTACT_FAX;
            //            orderRow["SHIP_TO_ADDR_NO"] = order.SHIP_TO_ADDR_NO; // el valor es entero-
            //            orderRow["TERRITORY"] = order.TERRITORY;
            //            orderRow["SALESREP_ID"] = order.SALESREP_ID;
            //            orderRow["SITE_ID"] = "TERMO";// order.SITE_ID;
            //            orderRow["TERMS_NET_TYPE"] = order.TERMS_NET_TYPE;
            //            orderRow["TERMS_NET_DAYS"] = order.TERMS_NET_DAYS;
            //            orderRow["TERMS_DISC_TYPE"] = order.TERMS_DISC_TYPE;
            //            orderRow["TERMS_DESCRIPTION"] = order.TERMS_DESCRIPTION;
            //            //orderRow["CUSTOMER_PO_REF"] = order.CUSTOMER_PO_REF;
            //            orderRow["FREIGHT_TERMS"] = "B";
            //            orderRow["ORDER_DATE"] = DateTime.Now;// order.ORDER_DATE;
            //                                                  // orderRow["DESIRED_SHIP_DATE"] = order.DESIRED_SHIP_DATE; //falla al convertir la fecha
            //            orderRow["BACK_ORDER"] = "N";
            //            orderRow["STATUS"] = "R";
            //            orderRow["SELL_RATE"] = "1";
            //            orderRow["BUY_RATE"] = "1";
            //            //orderRow["LAST_SHIPPED_DATE"] = order.LAST_SHIPPED_DATE;
            //            orderRow["POSTING_CANDIDATE"] = "N";
            //            //orderRow["TOTAL_AMT_ORDERED"] = order.TOTAL_AMT_ORDERED;// no se puede usar
            //            // orderRow["TOTAL_AMT_SHIPPED"] = order.TOTAL_AMT_SHIPPED; //campo protegido
            //            orderRow["MARKED_FOR_PURGE"] = "N";
            //            orderRow["EDI_FLAG"] = "N";
            //            orderRow["EXCH_RATE_FIXED"] = "N";
            //            //orderRow["PROMISE_DATE"] = order.PROMISE_DATE;
            //            orderRow["EDI_BLANKET_FLAG"] = "N";
            //            // orderRow["SHIPTO_ID"] = order.SHIPTO_ID;
            //            orderRow["CURRENCY_ID"] = order.CURRENCY_ID;
            //            //orderRow["CARRIER_ID"] = order.CARRIER_ID;
            //            //orderRow["ACCEPTED_EARLY"] = "Y";
            //            orderRow["DAYS_EARLY"] = "9999";
            //            //orderRow["CONTACT_MOBILE"] = order.CONTACT_MOBILE;
            //            orderRow["CREATE_DATE"] = DateTime.Now;//order.CREATE_DATE;
            //            orderRow["USER_1"] = order.order_Id;
            //            //orderRow["USER_8"] = "Cobros B2D/B2C";
            //            //orderRow["USER_9"] = order.USER_9;
            //            orderRow["REVISION_ID"] = "1";
            //            orderRow["VSCP_ORDER"] = "N";
            //            //orderRow["CONSINGMENT"] = "9999"; // no encuentra la columna
            //            // orderRow["STATUS_EFF_DATE"] = order.STATUS_EFF_DATE;
            //            orderRow["TERMS_ID"] = order.TERMS_ID;
            //            // orderRow["CONTACT_ID"] = order.CONTACT_ID; //no encuentra el contact
            //            orderRow["ENTERED_BY"] = order.ENTERED_BY;
            //            orderRow["EMAIL_NOTIFICATION"] = "N";
            //            orderRow["EMAIL_CUST_ON_NEW_ORDER"] = "N";
            //            orderRow["EMAIL_CUST_ON_CHG_ORDER"] = "N";
            //            orderRow["EMAIL_CUST_ON_SHIPMENT"] = "N";
            //            orderRow["EMAIL_EMPL_ON_NEW_ORDER"] = "N";
            //            //orderRow["EMAIL_EMPL_ON_GHG_ORDER"] = "N"; //no encuentra
            //            orderRow["EMAIL_EMPL_ON_SHIPMENT"] = "N";
            //            orderRow["EMAIL_EMPL_ON_INV_PAID"] = "N";
            //        }


            //        //Lsa.Data.DataRow orderlineRow = null;

            //        foreach (orderDetail od in order.oreder_detail)
            //        {
            //            Lsa.Data.DataRow orderlineRow = null;
            //            if (od.PART_ID == null)
            //            {

            //                if (od.GL_REVENUE_ACCT_ID != null)
            //                {
            //                    orderlineRow = Cust_Order.NewOrderLineRow(order.inv, od.LINE_NO);
            //                    orderlineRow["CUST_ORDER_ID"] = order.inv;
            //                    orderlineRow["LINE_NO"] = od.LINE_NO;
            //                    orderlineRow["ORDER_QTY"] = od.ORDER_QTY;
            //                    orderlineRow["USER_ORDER_QTY"] = od.USER_ORDER_QTY;
            //                    orderlineRow["UNIT_PRICE"] = od.UNIT_PRICE;
            //                    orderlineRow["MISC_REFERENCE"] = od.MISC_REFERENCE;
            //                    orderlineRow["VAT_CODE"] = "IVAV";// od.VAT_CODE;
            //                    orderlineRow["GL_REVENUE_ACCT_ID"] = od.GL_REVENUE_ACCT_ID;
            //                    orderlineRow["ENTERED_BY"] = od.ENTERED_BY;
            //                    if (order.head_status)
            //                    {
            //                        orderRow["USER_1"] = order.order_Id;
            //                    }
            //                }
            //                else
            //                {

            //                    orderlineRow = Cust_Order.NewOrderLineRow(order.inv, od.LINE_NO);
            //                    orderlineRow["CUST_ORDER_ID"] = order.inv;
            //                    orderlineRow["LINE_NO"] = od.LINE_NO;
            //                    //orderlineRow["PART_ID"] = od.PART_ID;
            //                    orderlineRow["LINE_STATUS"] = "A";
            //                    orderlineRow["ORDER_QTY"] = od.ORDER_QTY;
            //                    orderlineRow["USER_ORDER_QTY"] = od.USER_ORDER_QTY;
            //                    //orderlineRow["SELLING_UM"] = od.SELLING_UM;
            //                    orderlineRow["UNIT_PRICE"] = od.UNIT_PRICE;
            //                    orderlineRow["TRADE_DISC_PERCENT"] = "0";

            //                    //orderlineRow["MISC_REFERENCE"] = od.MISC_REFERENCE;
            //                    //orderlineRow["PRODUCT_CODE"] = od.PRODUCT_CODE;
            //                    //orderlineRow["COMMODITY_CODE"] = od.COMMODITY_CODE;

            //                    //orderlineRow["SERVICE_CHARGE_ID"] = od.SERVICE_CHARGE_ID;//no nulls
            //                    //orderlineRow["VAT_CODE"] = od.VAT_CODE;

            //                    orderlineRow["WIP_VAS_UNIT_PRICE"] = "0";

            //                    orderlineRow["ACCEPT_EARLY"] = "Y";//od.ACCEPTED_EARLY;
            //                    orderlineRow["DAYS_EARLY"] = "9999";//od.DAYS_EARLY;
            //                    if (order.head_status)
            //                    {
            //                        orderRow["USER_1"] = order.order_Id;
            //                    }

            //                    orderlineRow["SITE_ID"] = "TERMO";
            //                    orderlineRow["ENTERED_BY"] = od.ENTERED_BY;
            //                    orderlineRow["EMAIL_NOTIFICATION"] = "N";
            //                    orderlineRow["POSTING_CANDIDATE"] = "N";
            //                }
            //            }
            //            if (od.PART_ID != null)
            //            {
            //                orderlineRow = Cust_Order.NewOrderLineRow(order.inv, od.LINE_NO);
            //                orderlineRow["CUST_ORDER_ID"] = order.inv;
            //                orderlineRow["LINE_NO"] = od.LINE_NO;
            //                orderlineRow["PART_ID"] = od.PART_ID;
            //                orderlineRow["LINE_STATUS"] = "A";
            //                orderlineRow["ORDER_QTY"] = od.ORDER_QTY;
            //                orderlineRow["USER_ORDER_QTY"] = od.USER_ORDER_QTY;
            //                orderlineRow["SELLING_UM"] = od.SELLING_UM;
            //                orderlineRow["UNIT_PRICE"] = od.UNIT_PRICE;
            //                orderlineRow["TRADE_DISC_PERCENT"] = "0";

            //                orderlineRow["MISC_REFERENCE"] = od.MISC_REFERENCE;
            //                orderlineRow["PRODUCT_CODE"] = od.PRODUCT_CODE;
            //                orderlineRow["COMMODITY_CODE"] = od.COMMODITY_CODE;

            //                //orderlineRow["SERVICE_CHARGE_ID"] = od.SERVICE_CHARGE_ID;//no nulls
            //                orderlineRow["VAT_CODE"] = od.VAT_CODE;
            //                orderlineRow["WAREHOUSE_ID"] = od.WAREHOUSE_ID;
            //                orderlineRow["WIP_VAS_UNIT_PRICE"] = "0";

            //                orderlineRow["ACCEPT_EARLY"] = "Y";//od.ACCEPTED_EARLY;
            //                orderlineRow["DAYS_EARLY"] = "9999";//od.DAYS_EARLY;
            //                if (order.head_status)
            //                {
            //                    orderRow["USER_1"] = order.order_Id;
            //                }

            //                orderlineRow["SITE_ID"] = "TERMO";
            //                orderlineRow["ENTERED_BY"] = od.ENTERED_BY;
            //                orderlineRow["EMAIL_NOTIFICATION"] = "N";
            //                orderlineRow["POSTING_CANDIDATE"] = "N";

            //            }


            //        }
            //        //Cust_Order.Save();


            //        try
            //        {
            //            log.Debug("Guardando orden");
            //            Cust_Order.Save();
            //            String ordn =order.inv;



            //            //--------fin actualizcion------------------------------------------------

            //        }
            //        catch (Exception ex)
            //        {
            //            Console.WriteLine(ex.StackTrace);
            //          log.Debug("1.. " + ex.Message);
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(ex.StackTrace);
            //       log.Debug("2... " + ex.Message);
            //    }
            //    finally
            //    {
            //        Dbms.Close(order.baseNombre);
            //    }


            return View("Index");
            //}


            //static private string GetConnectionString(string dbname)
            //{
            //    //    // To avoid storing the connection string in your code,
            //    //    // you can retrieve it from a configuration file, using the
            //    //    // System.Configuration.ConfigurationSettings.AppSettings property
            //    return "Data Source=192.168.60.12;Initial Catalog=TERMCAR9;User ID=sa;Password=sa;";
            //}

            //private void LoginDB(string dbname)
            //{
            //    try
            //    {
            //        if (!Dbms.InstanceIsOpen(dbname))
            //        {
            //            switch (dbname)
            //            {

            //                case "TERMO":
            //                    Dbms.OpenDirect(dbname, "SQLSERVER", "", "192.168.60.19/TERMO", "dsanchez", "GH21$pos", "dsanchez", "GH21$pos");
            //                    break;
            //                case "TERMCAR9":
            //                    Dbms.OpenDirect(dbname, "SQLSERVER", "", "192.168.60.12/TERMCAR9", "sa", "Visual90", "sa", "Visual90");
            //                    break;
            //                case "TERMO9":
            //                    Dbms.OpenDirect(dbname, "SQLSERVER", "", "192.168.60.12/TERMO9", "sa", "Visual90", "sa", "Visual90");
            //                    break;
            //                case "TERMGUA9":
            //                    Dbms.OpenDirect(dbname, "SQLSERVER", "", "192.168.60.12/TERMGUA9", "sa", "Visual90", "sa", "Visual90");
            //                    break;
            //                case "TERMCR9":
            //                    Dbms.OpenDirect(dbname, "SQLSERVER", "", "192.168.60.12/TERMCR9", "sa", "Visual90", "sa", "Visual90");
            //                    break;
            //                case "TERMNIC9":
            //                    Dbms.OpenDirect(dbname, "SQLSERVER", "", "192.168.60.12/TERMNIC9", "sa", "Visual90", "sa", "Visual90");
            //                    break;
            //                case "TERMHON9":
            //                    Dbms.OpenDirect(dbname, "SQLSERVER", "", "192.168.60.12/TERMHON9", "sa", "Visual90", "sa", "Visual90");
            //                    break;
            //            }
            //        }
            //    }
            //    catch (Exception e)
            //    {
            //        Dbms.Close(dbname);
            //        log.Debug("Error al conectarse a la base" + e.Message);
            //    }
        }


    }
}
