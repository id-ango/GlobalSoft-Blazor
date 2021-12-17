﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace eSoft.Piutang.View
{
    public class ArAgingView
    {
        public int ArAgingId { get; set; }

        public string Kode { get; set; }
        public string Dokumen { get; set; }
        public DateTime Tanggal { get; set; }
        public DateTime Duedate { get; set; }
        public string Customer { get; set; }
        public string NoFaktur { get; set; }
        public string Keterangan { get; set; }
        public string NamaCust { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Jumlah { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Sisa { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Jumlah1 { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Jumlah2 { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Jumlah3 { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Jumlah4 { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Jumlah5 { get; set; }
    }
}
