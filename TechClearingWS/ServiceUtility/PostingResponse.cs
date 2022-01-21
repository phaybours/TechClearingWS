namespace TechClearingProject.Data.ServiceUtility
{
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

}
