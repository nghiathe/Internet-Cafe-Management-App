using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Maintainance
    {
        public Maintainance(DataRow row) 
        {
            this.MaintainId = (int)row["maintainanceid"];
            this.BillingId = (int)row["BillingId"];
            this.Component = row["componentname"].ToString();
            var descriptionTemp = row["description"];
            if (descriptionTemp.ToString() != "")
            {
                this.Description = descriptionTemp.ToString();
            }
            
        }
        private int maintainId;
        private int billingId;
        private string component;
        private string description;


        public int BillingId { get => billingId; set => billingId = value; }
        public string Component { get => component; set => component = value; }
        public string Description { get => description; set => description = value; }
        public int MaintainId { get => maintainId; set => maintainId = value; }
    }
}
