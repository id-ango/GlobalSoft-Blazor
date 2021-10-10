﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSoft.Order.View
{
    public class PoTransDView
    {
        private decimal discount;
        private decimal jumlah;

        [Key]
        public int PoTransDId { get; set; }
        [StringLength(2)]
        public string Kode { get; set; }
        public string NoLpb { get; set; }
        public DateTime Tanggal { get; set; }
        public string ItemCode { get; set; }
        public string NamaItem { get; set; }
        public string Satuan { get; set; }
        public string Lokasi { get; set; }
        public string Currency { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Kurs { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Harga { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Persen { get; set; }
        [Column(TypeName = "decimal(18,2)")]

        public decimal Discount
        {
            get
            {
                var price = Qty * Harga;

                if (Persen != 0)
                {
                    return price * Persen / 100;
                }
                else
                {
                    return discount;
                }


            }
            set
            {
                discount = value;
            }
        }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Qty { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal QtyBo { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal JumDpp { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Jumlah
        {
            get
            {
                if (jumlah != UpdateJumlah && jumlah != 0)
                {
                    return jumlah;
                }
                else
                {
                    return UpdateJumlah;
                }

            }

            set
            {
                jumlah = value;
            }
        }

        public decimal UpdateJumlah
        {
            get
            {
                var price = Qty * Harga;
                jumlah = price - Discount;
                return price - Discount;
            }
        }

        public string Vendor { get; set; }
        public string AcctSet { get; set; }
        public int PoTransHId { get; set; }
        public PoTransHView PoTransH { get; set; }
    }
}
