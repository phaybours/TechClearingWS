using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechClearingProject.Data.ServiceUtility
{


    public class AcctValResponse
    {
        public int nErrorCode {get; set;}
        public string sErrorText {get; set;}
        public decimal nBalance {get; set;}
        public string sName {get; set;}
        public string sStatus {get; set;}
        public string nBranch {get; set;}
        public string sCrncyIso {get; set;}
        public string sAddress {get; set;}
        public string sTransNature {get; set;}
        public string sChequeStatus {get; set;}
        public string sAccountType { get; set; }
        public string sProductCode { get; set; }
    }
    //public class PostingResponse
    //{
    //    public int nErrorCode { get; set; }
    //    public string sErrorText { get; set; }
    //    public decimal nBalance { get; set; }
    //    public string sName { get; set; }
    //    public string sStatus { get; set; }
    //    public string nBranch { get; set; }
    //    public string CbsTranId { get; set; }
    //}

    public class PostingResponse
    {
        public int nErrorCode { get; set; }
        public string sErrorText { get; set; }
        public decimal nDrAcctBalance { get; set; }
        public decimal nCrAcctBalance { get; set; }
        public string sName { get; set; }
        public string sStatus { get; set; }
        public string nBranch { get; set; }
        public string nCbsTranId { get; set; }
        public string sValueDate { get; set; }
    }



    public class ProcessingDateResponse
    {
        public int nErrorCode { get; set; }
        public string sErrorText { get; set; }
        public DateTime dDate { get; set; }

    }

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


    public class PostingRequest2
    {
        public string ItbId { get; set; }
        public string TransactionRef { get; set; }
        public string DrAcctNo { get; set; }
        public string DrAcctType { get; set; }
        public string DrAcctTC { get; set; }
        public string DrAcctNarration { get; set; }
        public string TranAmount { get; set; }
        public string CrAcctNo { get; set; }
        public string CrAcctType { get; set; }
        public string CrAcctTC { get; set; }
        public string CrAcctNarration { get; set; }
        public string CurrencyISO { get; set; }
        public string PostDate { get; set; }
        public string ValueDate { get; set; }
        public string UserName { get; set; }
        public string FloatDays { get; set; }
        public string RoutingNo { get; set; }
        public string SupervisorName { get; set; }
        public string ChannelId { get; set; }
        public string ForcePostFlag { get; set; }
        public string Reversal { get; set; }
        public string TranBatchID { get; set; }
        public string ParentTransRef { get; set; }
        public string Direction { get; set; }
        public string RimNo { get; set; }
        public string DrAcctChequeNo { get; set; }
        public string DrAcctChargeCode { get; set; }
        public string DrAcctChargeAmt { get; set; }
        public string DrAcctTaxAmt { get; set; }
        public string CrAcctChequeNo { get; set; }
        public string CrAcctChargeCode { get; set; }
        public string CrAcctChargeAmt { get; set; }
        public string CrAcctTaxAmt { get; set; }
        public string TransTracer { get; set; }
        public string DrAcctChgDescr { get; set; }
        public string CrAcctChgDescr { get; set; }
        public string DrAcctCashAmt { get; set; }
        public string CrAcctCashAmt { get; set; }
        public string EquivAmt { get; set; }
        public string OrigExchRate { get; set; }
        public string ExchRate { get; set; }
        public string DrAcctChgBranch { get; set; }
        public string CrAcctChgBranch { get; set; }
        public string DrAcctOffshoreAmt { get; set; }
        public string CrAcctOffshoreAmt { get; set; }

    }


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
