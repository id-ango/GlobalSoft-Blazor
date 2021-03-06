using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace eSoft.Piutang.View
{
    public class ArTransDView
    {
        [Key]
        public int ArTransDId { get; set; }
        public DateTime Tanggal { get; set; }
        public string Bukti { get; set; }
        public Nullable<DateTime> DueDate { get; set; }
        public string KodeTran { get; set; }
        public string Lpb { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Jumlah { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Bayar { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Sisa { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Discount { get; set; }
        public string Keterangan { get; set; }
        public decimal UpdateSisa
        {
            get
            {
                return Jumlah - Bayar - Discount;
            }
        }
        public string DistCode { get; set; }
        public int ArTransHId { get; set; }
        public ArTransHView ArTransH { get; set; }
    }
}
