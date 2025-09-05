using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RRappointmentsWebApplication.TreatmentInstructionLocalWebReference;

namespace RRappointmentsWebApplication.BuisnessLogicLayer
{
    [Serializable]
    public class SiteInfo
    {
        public string SiteName { get; set; }
        public string Address { get; set; }
        public string IsActive { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Mobile { get; set; }
        public string FaxNum { get; set; }
        public string DocName { get; set; }
        public string Email { get; set; }

        public static explicit operator SiteInfo(TreatmentInstructionLocalWebReference.SiteInfo v)
        {
            throw new NotImplementedException();
        }

        //public SiteInfo() { }
    }
}