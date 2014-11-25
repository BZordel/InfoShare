using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tingle_WebForms.Models
{
    public class OrderCancellation
    {
        [Key]
        public int RecordId { get; set; }

        public DateTime Timestamp { get; set; }

        public virtual Status Status { get; set; }

        [MaxLength(100)]
        public string OrderNumber { get; set; }

        [MaxLength(100)]
        public string ArmstrongReference { get; set; }

        [MaxLength(100)]
        public string Customer { get; set; }

        [MaxLength(100)]
        public string PO { get; set; }

        public virtual PurchaseOrderStatus POStatus { get; set; }

        [MaxLength(100)]
        public string SKU { get; set; }

        [MaxLength(100)]
        public string Line { get; set; }

        [MaxLength(100)]
        public string LineOfPO { get; set; }

        [MaxLength(100)]
        public string Size { get; set; }

        public DateTime DateRequired { get; set; }

        [MaxLength(100)]
        public string ShipVia { get; set; }

        [MaxLength(100)]
        public string Serial { get; set; }

        [MaxLength(100)]
        public string TruckRoute { get; set; }

        [MaxLength(2000)]
        public string AdditionalInfo { get; set; }

        [MaxLength(10)]
        public string Company { get; set; }

        [MaxLength(1000)]
        public string CCFormToEmail { get; set; }

        [MaxLength(100)]
        public string RequestHandler { get; set; }

        [MaxLength(100)]
        public string SubmittedByUser { get; set; }

        [MaxLength(100)]
        public string ModifiedByUser { get; set; }


    }
}