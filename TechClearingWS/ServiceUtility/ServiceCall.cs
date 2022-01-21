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

}
