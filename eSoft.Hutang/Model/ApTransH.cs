﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSoft.Hutang.Model
{
    public class ApTransH
    {
        [Key]
        public int ApTransHId { get; set; }
        [StringLength(2)]
        public string Kode { get; set; }
        public string Bukti { get; set; }
        public DateTime Tanggal { get; set; }
        public string KdBank { get; set; }
        public string Supplier { get; set; }
        public string NoFaktur { get; set; }
        public string Keterangan { get; set; }
        public Nullable<DateTime> JthTempo { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal PPn { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal PPh { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal JumPPh { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal JumPPn { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Bruto { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Netto { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Jumlah { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Discount { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Hutang { get; set; }
        public bool Pajak { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Unapplied { get; set; }
        public string AcctSet { get; set; }
        public List<ApTransD> ApTransDs { get; set; }
        public int ApSupplId { get; set; }
        public string NamaSup { get; set; }
        //     public ArCust ArCust { get; set; }
    }
}
