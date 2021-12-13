using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSoft.Persediaan.View
{
    public class IcStockCardView
    {
        public int IcStockCardId { get; set; }
        public string Kode { get; set; }
        public DateTime Tanggal { get; set; }
        public string ItemCode { get; set; }
        public string Dokumen { get; set; }
        public string Divisi { get; set; }
        public string KodeDivisi { get; set; }
        public string NamaItem { get; set; }
        public string Satuan { get; set; }
        public string Keterangan { get; set; }
        public decimal Harga { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Persen { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Discount { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Qty { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal HrgCost { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Cost { get; set; }
        public string Lokasi { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal QtyShp { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Qtybo { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Jumfps { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal JumDpp { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Jumlah { get; set; }
        
    }
}
