//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Apiweb.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PART
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PART()
        {
            this.TRACE = new HashSet<TRACE>();
        }
    
        public int ROWID { get; set; }
        public string ID { get; set; }
        public string DESCRIPTION { get; set; }
        public string STOCK_UM { get; set; }
        public short PLANNING_LEADTIME { get; set; }
        public string ORDER_POLICY { get; set; }
        public Nullable<decimal> ORDER_POINT { get; set; }
        public Nullable<decimal> SAFETY_STOCK_QTY { get; set; }
        public Nullable<decimal> FIXED_ORDER_QTY { get; set; }
        public Nullable<short> DAYS_OF_SUPPLY { get; set; }
        public Nullable<decimal> MINIMUM_ORDER_QTY { get; set; }
        public Nullable<decimal> MAXIMUM_ORDER_QTY { get; set; }
        public string PRODUCT_CODE { get; set; }
        public string COMMODITY_CODE { get; set; }
        public string MFG_NAME { get; set; }
        public string MFG_PART_ID { get; set; }
        public string FABRICATED { get; set; }
        public string PURCHASED { get; set; }
        public string STOCKED { get; set; }
        public string DETAIL_ONLY { get; set; }
        public string DEMAND_HISTORY { get; set; }
        public string TOOL_OR_FIXTURE { get; set; }
        public string INSPECTION_REQD { get; set; }
        public Nullable<decimal> WEIGHT { get; set; }
        public string WEIGHT_UM { get; set; }
        public string DRAWING_ID { get; set; }
        public string DRAWING_REV_NO { get; set; }
        public string PREF_VENDOR_ID { get; set; }
        public string MRP_REQUIRED { get; set; }
        public string MRP_EXCEPTIONS { get; set; }
        public string PRIVATE_UM_CONV { get; set; }
        public string AUTO_BACKFLUSH { get; set; }
        public string PLANNER_USER_ID { get; set; }
        public string BUYER_USER_ID { get; set; }
        public string ABC_CODE { get; set; }
        public Nullable<decimal> ANNUAL_USAGE_QTY { get; set; }
        public string INVENTORY_LOCKED { get; set; }
        public string MAT_GL_ACCT_ID { get; set; }
        public string LAB_GL_ACCT_ID { get; set; }
        public string BUR_GL_ACCT_ID { get; set; }
        public string SER_GL_ACCT_ID { get; set; }
        public decimal QTY_ON_HAND { get; set; }
        public decimal QTY_AVAILABLE_ISS { get; set; }
        public decimal QTY_AVAILABLE_MRP { get; set; }
        public decimal QTY_ON_ORDER { get; set; }
        public decimal QTY_IN_DEMAND { get; set; }
        public string USER_1 { get; set; }
        public string USER_2 { get; set; }
        public string USER_3 { get; set; }
        public string USER_4 { get; set; }
        public string USER_5 { get; set; }
        public string USER_6 { get; set; }
        public string USER_7 { get; set; }
        public string USER_8 { get; set; }
        public string USER_9 { get; set; }
        public string USER_10 { get; set; }
        public string NMFC_CODE_ID { get; set; }
        public string PACKAGE_TYPE { get; set; }
        public string MRP_EXCEPTION_INFO { get; set; }
        public Nullable<decimal> MULTIPLE_ORDER_QTY { get; set; }
        public string ADD_FORECAST { get; set; }
        public string UDF_LAYOUT_ID { get; set; }
        public string PIECE_TRACKED { get; set; }
        public string LENGTH_REQD { get; set; }
        public string WIDTH_REQD { get; set; }
        public string HEIGHT_REQD { get; set; }
        public string DIMENSIONS_UM { get; set; }
        public string SHIP_DIMENSIONS { get; set; }
        public string DRAWING_FILE { get; set; }
        public string TARIFF_CODE { get; set; }
        public string TARIFF_TYPE { get; set; }
        public string ORIG_COUNTRY_ID { get; set; }
        public Nullable<decimal> NET_WEIGHT_2 { get; set; }
        public Nullable<decimal> GROSS_WEIGHT_2 { get; set; }
        public string WEIGHT_UM_2 { get; set; }
        public Nullable<decimal> VOLUME { get; set; }
        public string VOLUME_UM { get; set; }
        public string VAT_CODE { get; set; }
        public Nullable<int> DEMAND_FENCE_1 { get; set; }
        public Nullable<int> DEMAND_FENCE_2 { get; set; }
        public string ROLL_FORECAST { get; set; }
        public string CONSUMABLE { get; set; }
        public string PRIMARY_SOURCE { get; set; }
        public string LABEL_UM { get; set; }
        public string HTS_CODE { get; set; }
        public string DEF_ORIG_COUNTRY { get; set; }
        public string MATERIAL_CODE { get; set; }
        public string DEF_LBL_FORMAT_ID { get; set; }
        public string VOLATILE_LEADTIME { get; set; }
        public Nullable<int> LT_PLUS_DAYS { get; set; }
        public Nullable<int> LT_MINUS_DAYS { get; set; }
        public string STATUS { get; set; }
        public string USE_SUPPLY_BEF_LT { get; set; }
        public decimal QTY_COMMITTED { get; set; }
        public string intrastat_exempt { get; set; }
        public Nullable<decimal> CASE_QTY { get; set; }
        public Nullable<decimal> PALLET_QTY { get; set; }
        public Nullable<short> MINIMUM_LEADTIME { get; set; }
        public Nullable<short> LEADTIME_BUFFER { get; set; }
        public Nullable<int> EMERGENCY_STOCKPCT { get; set; }
        public Nullable<decimal> REPLENISH_LEVEL { get; set; }
        public Nullable<decimal> MIN_BATCH_SIZE { get; set; }
        public string EFF_DATE_PRICE { get; set; }
        public string ECN_REVISION { get; set; }
        public string REVISION_ID { get; set; }
        public string STAGE_ID { get; set; }
        public string ECN_REV_CONTROL { get; set; }
        public string IS_KIT { get; set; }
        public Nullable<int> YELLOW_STOCKPCT { get; set; }
        public string UNIV_PLAN_MATERIAL { get; set; }
        public Nullable<short> RLS_NEAR_DAYS { get; set; }
        public Nullable<short> SUGG_RLS_NEAR_DAYS { get; set; }
        public Nullable<decimal> ORDER_UP_TO_QTY { get; set; }
        public Nullable<System.DateTime> LAST_IMPLODE_DATE { get; set; }
        public System.DateTime STATUS_EFF_DATE { get; set; }
        public string CONTROLLED_BY_ICS { get; set; }
        public string PRICE_GROUP { get; set; }
        public Nullable<decimal> DEF_PACKAGE_QTY { get; set; }
        public Nullable<decimal> DEF_PACKAGE_CAP { get; set; }
        public string DEF_SLS_TAX_GRP_ID { get; set; }
        public string MRO_CLASS { get; set; }
        public string BUFFER_PROFILE_ID { get; set; }
        public Nullable<short> ADU_HORIZON { get; set; }
        public Nullable<short> ASR_LEADTIME { get; set; }
        public Nullable<short> ONHAND_ALERT_RED_PCT { get; set; }
        public string PROCESS_TYPE { get; set; }
        public string BID_RATE_CATEGORY_ID { get; set; }
        public System.DateTime CREATE_DATE { get; set; }
        public Nullable<System.DateTime> MODIFY_DATE { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TRACE> TRACE { get; set; }
    }
}
