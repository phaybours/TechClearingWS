namespace TechClearingProject.Data.ServiceUtility
{
    //public class PostingRequest
    //{
    //    public string TransRef { get; set; }
    //    public string DrAccountNo {get; set;}
    //    public string DrAcctType { get; set; }
    //    public int DrAcctTC {get; set;}
    //    public string DrAcctNarration { get; set; }
    //    public decimal TranAmount {get; set;}
    //    public string CrAcctNo { get; set; }
    //    public string CrAcctType { get; set; }
    //    public int CrAcctTC { get; set; }
    //    public string CrAcctNarration {get; set;}
    //    public string CurrencyISO { get; set; }
    //    public DateTime PostDate {get; set;}
    //    public DateTime ValueDate {get; set;}
    //    public string UserName {get; set;}
    //    public int? ChequeNo {get; set;}
    //    public string SupervisorName {get; set;}
    //    public short? ChannelId {get; set;}
    //    public short? RemoveOldFloat { get; set; }
    //    public short? ForcePostFlag { get; set; }
    //    public short? Reversal {get; set;}
    //    public int? TranBatchID {get; set;}
    //    public int? ChargeCode {get; set;}
    //    public decimal? ChargeAmt {get; set;}
    //    public decimal? TaxAmt {get; set;}
    //    public string ParentTransRef { get; set; }
    //    public string RoutingNo { get; set; }
    //    public int? FloatDays { get; set; }

    //}

    // psTransactionRef,PnDirection,psAccountType,psCurrencyISO,psUserName

    public class ReleaseFundRequest
    {
        public string psTransactionRef { get; set; }
        public int pnInstrument { get; set; }
        public int pnDirection { get; set; }
        public string psAccountNo { get; set; }
        public string psAccountType { get; set; }
    }

}
