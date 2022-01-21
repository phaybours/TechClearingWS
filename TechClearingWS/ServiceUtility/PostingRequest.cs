using System;

namespace TechClearingProject.Data.ServiceUtility
{
    public class PostingRequest
    {
        public int? ItbId { get; set; }
        public string TransactionRef { get; set; }
        public string DrAcctNo { get; set; }
        public string DrAcctType { get; set; }
        public int DrAcctTC { get; set; }
        public string DrAcctNarration { get; set; }
        public decimal TranAmount { get; set; }
        public string CrAcctNo { get; set; }
        public string CrAcctType { get; set; }
        public int CrAcctTC { get; set; }
        public string CrAcctNarration { get; set; }
        public string CurrencyISO { get; set; }
        public DateTime PostDate { get; set; }
        public DateTime ValueDate { get; set; }
        public string UserName { get; set; }
        public int FloatDays { get; set; }
        public string RoutingNo { get; set; }
        public string SupervisorName { get; set; }
        public int ChannelId { get; set; }
        public int ForcePostFlag { get; set; }
        public int Reversal { get; set; }
        public int? TranBatchID { get; set; }
        public string ParentTransRef { get; set; }
        public int Direction { get; set; }
        public int? RimNo { get; set; }
        public int? DrAcctChequeNo { get; set; }
        public int? DrAcctChargeCode { get; set; }
        public decimal? DrAcctChargeAmt { get; set; }
        public decimal? DrAcctTaxAmt { get; set; }
        public int? CrAcctChequeNo { get; set; }
        public int? CrAcctChargeCode { get; set; }
        public decimal? CrAcctChargeAmt { get; set; }
        public decimal? CrAcctTaxAmt { get; set; }
        public string TransTracer { get; set; }
        public string DrAcctChgDescr { get; set; }
        public string CrAcctChgDescr { get; set; }
        public decimal? DrAcctCashAmt { get; set; }
        public decimal? CrAcctCashAmt { get; set; }
        public decimal? EquivAmt { get; set; }
        public decimal? OrigExchRate { get; set; }
        public decimal? ExchRate { get; set; }
        public int? DrAcctChgBranch { get; set; }
        public int? CrAcctChgBranch { get; set; }
        public decimal? DrAcctOffshoreAmt { get; set; }
        public decimal? CrAcctOffshoreAmt { get; set; }

    }

}
