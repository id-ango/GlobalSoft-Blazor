﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSoft.Order.Model
{
    public class PoTransH
    {
        [Key]
        public int PoTransHId { get; set; }
        [StringLength(2)]
        public string Kode { get; set; }
        public string NoLpb { get; set; }
        public string NoSJ { get; set; }
        public string NoPrj { get; set; }
        public string Lokasi { get; set; }
        public string Kurs { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Currency { get; set; }
        public DateTime Tanggal { get; set; }
        public DateTime JthTempo { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal TtlJumlah { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal DPayment { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Ongkos { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal PpnPersen { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Ppn { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Jumlah { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Tagihan { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal TotalQty { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal QtyTerima { get; set; }
        public string Vendor { get; set; }
        public string NamaVendor { get; set; }
        public string Keterangan { get; set; }
        public string Cek { get; set; }
        public bool Pajak { get; set; }
        public string Status { get; set; }
        public List<PoTransD> PoTransDs { get; set; }

    }
}
