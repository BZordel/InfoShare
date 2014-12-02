using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tingle_WebForms.Models
{
    public class ExpeditedOrderForm
    {
        [Key]
        public int RecordId { get; set; }

        public DateTime Timestamp { get; set; }

        [MaxLength(100)]
        public string OowOrderNumber { get; set; }

        [MaxLength(100)]
        public string Customer { get; set; }

        [MaxLength(6)]
        public string AccountNumber { get; set; }

        [Required]
        public virtual ExpediteCode ExpediteCode { get; set; }

        [MaxLength(100)]
        public string PurchaseOrderNumber { get; set; }

        [MaxLength(100)]
        public string MaterialSku { get; set; }

        public string QuantityOrdered { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-M-dd hh:mm tt}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> InstallDate { get; set; }

        [MaxLength(100)]
        public string SM { get; set; }

        [MaxLength(100)]
        public string ContactName { get; set; }

        [MaxLength(100)]
        public string PhoneNumber { get; set; }

        [MaxLength(100)]
        public string ShipToName { get; set; }

        [MaxLength(100)]
        public string ShipToAddress { get; set; }

        [MaxLength(100)]
        public string ShipToCity { get; set; }

        [MaxLength(100)]
        public string ShipToState { get; set; }

        [MaxLength(100)]
        public string ShipToZip { get; set; }

        [MaxLength(2000)]
        public string AdditionalInfo { get; set; }

        [MaxLength(100)]
        public string RequestHandler { get; set; }

        [MaxLength(10)]
        public string Company { get; set; }

        public virtual Status Status { get; set; }

        [MaxLength(100)]
        public string SubmittedByUser { get; set; }

        [MaxLength(100)]
        public string ModifiedByUser { get; set; }

        [MaxLength(1000)]
        public string CCFormToEmail { get; set; }

        [MaxLength(4000)]
        public string CompletedNotes { get; set; }

        [MaxLength(1000)]
        public string CCCompletedFormToEmail { get; set; }

    }


    
}