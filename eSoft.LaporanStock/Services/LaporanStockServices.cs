using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eSoft.Persediaan.Data;
using eSoft.Persediaan.Model;
using eSoft.Persediaan.View;
using eSoft.Penjualan.Data;
using eSoft.Penjualan.Model;
using eSoft.Penjualan.View;
using eSoft.Pembelian.Data;
using eSoft.Pembelian.Model;
using eSoft.Pembelian.View;

using Microsoft.EntityFrameworkCore;


namespace eSoft.LaporanStock.Services
{
    public class LaporanStockServices : ILaporanStockServices
    {
        private readonly DbContextPersediaan _context;
        private readonly DbContextBeli _contextIR;
        private readonly DbContextJual _contextOE;

        public LaporanStockServices(DbContextPersediaan context, DbContextBeli contextBeli, DbContextJual contextJual)
        {
            _context = context;
            _contextIR = contextBeli;
            _contextOE = contextJual;
        }

        #region Laporan
        public List<IcStockCardView> CetakMutasi(DateTime Tanggal1, DateTime Tanggal2, string kodeBank)
        {
            decimal SldAwalJual = 0;
            decimal SldAwalBeli = 0;
            decimal SldAwalIC = 0;
            decimal SaldoAwal = 0;

            List<IcStockCardView> Transaksi = new List<IcStockCardView>();
            List<OeTransD> TransAwalJual = new List<OeTransD>();
            List<IrTransD> TransAwalBeli = new List<IrTransD>();
            List<IcTransD> TransAwalIC = new List<IcTransD>();
            List<OeTransH> transHOE = new List<OeTransH>();
            List<OeTransD> transDOE = new List<OeTransD>();
            List<IrTransH> transHIR = new List<IrTransH>();
            List<IrTransD> transDIR = new List<IrTransD>();
            List<IcTransH> transHIC = new List<IcTransH>();
            List<IcTransD> transDIC = new List<IcTransD>();



            TransAwalJual = _contextOE.OeTransDs.Where(x => x.ItemCode == kodeBank && x.Tanggal.Date < Tanggal1.Date)
                .ToList();
            TransAwalBeli = _contextIR.IrTransDs.Where(x => x.ItemCode == kodeBank && x.Tanggal.Date < Tanggal1.Date)
               .ToList();
            TransAwalIC = _context.IcTransDs.Where(x => x.ItemCode == kodeBank && x.Tanggal.Date < Tanggal1.Date)
               .ToList();


            if (TransAwalJual != null)
            {
                SldAwalJual = TransAwalJual.Sum(x => (x.Kode == "94" ? -1 * x.Qty : x.Qty));
            }
            else
            {
                SldAwalJual = 0;
            }

            if (TransAwalBeli != null)
            {
                SldAwalBeli = TransAwalBeli.Sum(x => (x.Kode == "82" ? x.Qty : -1 * x.Qty));
            }
            else
            {
                SldAwalBeli = 0;
            }

            if (TransAwalIC != null)
            {
                SldAwalIC = TransAwalIC.Sum(x => (x.Kode == "81" || x.Kode == "72" ? x.QtyShp : 0));
            }
            else
            {
                SldAwalIC = 0;
            }

            SaldoAwal = SldAwalBeli + SldAwalIC + SldAwalJual;



            Transaksi.Add(new IcStockCardView
            {
                ItemCode = kodeBank,
                Tanggal = Tanggal1,
                Keterangan = "Saldo Awal",
                Qty = SaldoAwal

            }
                );

            transHIR = _contextIR.IrTransHs.Include(p => p.IrTransDs).Where(x => x.Tanggal.Date >= Tanggal1.Date && x.Tanggal.Date <= Tanggal2.Date).ToList();
            transDIR = _contextIR.IrTransDs.Where(x => x.ItemCode == kodeBank && (x.Tanggal.Date >= Tanggal1.Date && x.Tanggal.Date <= Tanggal2.Date)).ToList();

            if (transHIR != null && transDIR != null)
            {
                var Rincian1 = (from e in transHIR
                                join f in transDIR on e.IrTransHId equals f.IrTransHId
                                select new IcStockCardView()
                                {
                                    Kode = e.Kode,
                                    ItemCode = f.ItemCode,
                                    Keterangan = e.NamaSup + ", " + e.Keterangan,
                                    Tanggal = f.Tanggal.Date,
                                    Dokumen = e.NoLpb,
                                    Qty = (e.Kode == "82" ? f.Qty : -1 * f.Qty),
                                    Cost = f.JumDpp,
                                    HrgCost = f.Harga
                                }).ToList();

                Transaksi.AddRange(Rincian1);
            }

            transHIC = _context.IcTransHs.Include(p => p.IcTransDs).Where(x => (x.Tanggal.Date >= Tanggal1.Date && x.Tanggal.Date <= Tanggal2.Date) && (x.Kode == "81" || x.Kode == "72")).ToList();
            transDIC = _context.IcTransDs.Where(x => x.ItemCode == kodeBank && (x.Tanggal.Date >= Tanggal1.Date && x.Tanggal.Date <= Tanggal2.Date)).ToList();

            if (transHIC != null && transDIC != null)
            {
                var Rincian2 = (from e in transHIC
                                join f in transDIC on e.IcTransHId equals f.IcTransHId
                                select new IcStockCardView()
                                {
                                    Kode = e.Kode,
                                    ItemCode = f.ItemCode,
                                    Keterangan = e.Keterangan,
                                    Tanggal = f.Tanggal.Date,
                                    Dokumen = e.NoFaktur,
                                    Qty = f.QtyShp,
                                    Cost = f.Jumlah,
                                    HrgCost = f.Harga
                                }).ToList();

                Transaksi.AddRange(Rincian2);
            }


            transHOE = _contextOE.OeTransHs.Include(p => p.OeTransDs).Where(x => x.Tanggal.Date >= Tanggal1.Date && x.Tanggal.Date <= Tanggal2.Date).ToList();
            transDOE = _contextOE.OeTransDs.Where(x => x.ItemCode == kodeBank && (x.Tanggal.Date >= Tanggal1.Date && x.Tanggal.Date <= Tanggal2.Date)).ToList();

            if (transHOE != null && transDOE != null)
            {
                var Rincian3 = (from e in transHOE
                                join f in transDOE on e.OeTransHId equals f.OeTransHId
                                select new IcStockCardView()
                                {
                                    Kode = e.Kode,
                                    ItemCode = f.ItemCode,
                                    Keterangan = e.NamaCust + ", " + e.Keterangan,
                                    Tanggal = f.Tanggal.Date,
                                    Dokumen = e.NoLpb,
                                    Qty = (e.Kode == "94" ? -1 * f.Qty : f.Qty),
                                    Cost = (e.Kode == "94" ? -1 * f.Cost : f.Cost),
                                    HrgCost = f.HrgCost
                                }).ToList();

                Transaksi.AddRange(Rincian3);
            }

            Transaksi = Transaksi.OrderBy(x => x.Tanggal).ToList();

            SaldoAwal = 0;

            Transaksi = Transaksi.Select(i => { SaldoAwal += i.Qty; i.Jumlah = SaldoAwal; return i; }).ToList();

            //foreach (var trans in Transaksi)
            //{
            //    SaldoAwal = SaldoAwal + trans.Qty;
            //    trans.Jumlah = SaldoAwal;

            //}


            return Transaksi;
        }
        #endregion

        #region prosesStock

        public void prosesStock()
        {

            List<IcItem> MasterStock = _context.IcItems.ToList();
            List<IcAltItem> AltStock = _context.IcAltItems.ToList();
            List<OeTransD> TransJual = new List<OeTransD>();
            List<IrTransD> TransBeli = new List<IrTransD>();
            List<IcTransD> TransIC = new List<IcTransD>();
            List<IcStockCardView> Transaksi = new List<IcStockCardView>();

            MasterStock.ForEach(i => { i.Qty = 0; i.Cost = 0; });
            AltStock.ForEach(i => { i.Qty = 0; i.Cost = 0; });

            MasterStock.ForEach(i => { i.Qty = i.SaldoAwal; i.Cost = i.CostAwal; });
            MasterStock.ForEach(i => i.HrgNetto = (i.SaldoAwal != 0 ? i.CostAwal / i.SaldoAwal : i.Harga));

            AltStock.ForEach(i => { i.Qty = i.SaldoAwal; i.Cost = i.CostAwal; });


            TransJual = _contextOE.OeTransDs.OrderBy(x => x.Tanggal)
                .ToList();
            TransBeli = _contextIR.IrTransDs.OrderBy(x => x.Tanggal)
               .ToList();
            TransIC = _context.IcTransDs.OrderBy(x => x.Tanggal)
               .ToList();

            foreach (var trans in TransBeli)
            {
                Transaksi.Add(new IcStockCardView()
                {
                    Tanggal = trans.Tanggal,
                    Kode = trans.Kode,
                    ItemCode = trans.ItemCode,
                    Dokumen = trans.NoLpb,
                    Qty = trans.Qty,
                    Jumlah = trans.JumDpp,
                    Lokasi = trans.Lokasi,
                    IcCardId = trans.IrTransDId

                });
            }

            foreach (var trans in TransIC)
            {
                Transaksi.Add(new IcStockCardView()
                {
                    Tanggal = trans.Tanggal,
                    Kode = trans.Kode,
                    ItemCode = trans.ItemCode,
                    Dokumen = trans.NoFaktur,
                    Qty = trans.QtyShp,
                    Jumlah = trans.Jumlah,
                    Lokasi = trans.Lokasi,
                    Lokasi2 = trans.Lokasi2,
                    IcCardId = trans.IcTransDId

                });
            }

            foreach (var trans in TransJual)
            {
                Transaksi.Add(new IcStockCardView()
                {
                    Tanggal = trans.Tanggal,
                    Kode = trans.Kode,
                    ItemCode = trans.ItemCode,
                    Dokumen = trans.NoLpb,
                    Qty = trans.Qty,
                    Jumlah = trans.Jumlah,
                    Lokasi = trans.Lokasi,
                    IcCardId = trans.OeTransDId

                });
            }


            foreach (var trans in Transaksi)
            {
                IcItem item = MasterStock.Find(x => x.ItemCode == trans.ItemCode);

                if (item.JnsBrng == 1)
                {

                    //if ((trans.Tanggal > item.TglPost || item.TglPost == null) && trans.Tanggal <= DateTime.Today.Date)
                    //{
                        switch (trans.Kode)
                        {
                            case "81":
                                MasterStock.Find(x => x.ItemCode == trans.ItemCode).Cost += trans.Jumlah;
                                MasterStock.Find(x => x.ItemCode == trans.ItemCode).Qty += trans.Qty;
                                MasterStock.Find(x => x.ItemCode == trans.ItemCode).HrgNetto = (MasterStock.Find(x => x.ItemCode == trans.ItemCode).Qty != 0 ? (MasterStock.Find(x => x.ItemCode == trans.ItemCode).Cost / MasterStock.Find(x => x.ItemCode == trans.ItemCode).Qty) : item.Harga);
                                break;

                            case "82":
                                MasterStock.Find(x => x.ItemCode == trans.ItemCode).Cost += trans.Jumlah;
                                MasterStock.Find(x => x.ItemCode == trans.ItemCode).Qty += trans.Qty;
                                MasterStock.Find(x => x.ItemCode == trans.ItemCode).HrgNetto = (MasterStock.Find(x => x.ItemCode == trans.ItemCode).Qty != 0 ? (MasterStock.Find(x => x.ItemCode == trans.ItemCode).Cost / MasterStock.Find(x => x.ItemCode == trans.ItemCode).Qty) : MasterStock.Find(x => x.ItemCode == trans.ItemCode).Harga);
                                break;

                            case "83":
                                MasterStock.Find(x => x.ItemCode == trans.ItemCode).Cost -= trans.Jumlah;
                                MasterStock.Find(x => x.ItemCode == trans.ItemCode).Qty -= trans.Qty;
                                MasterStock.Find(x => x.ItemCode == trans.ItemCode).HrgNetto = (MasterStock.Find(x => x.ItemCode == trans.ItemCode).Qty != 0 ? (MasterStock.Find(x => x.ItemCode == trans.ItemCode).Cost / MasterStock.Find(x => x.ItemCode == trans.ItemCode).Qty) : MasterStock.Find(x => x.ItemCode == trans.ItemCode).Harga);
                                break;

                            case "94":
                                trans.Jumlah = MasterStock.Find(x => x.ItemCode == trans.ItemCode).HrgNetto * trans.Qty;
                                trans.Harga = MasterStock.Find(x => x.ItemCode == trans.ItemCode).HrgNetto;
                                MasterStock.Find(x => x.ItemCode == trans.ItemCode).Cost -= MasterStock.Find(x => x.ItemCode == trans.ItemCode).HrgNetto * trans.Qty;
                                MasterStock.Find(x => x.ItemCode == trans.ItemCode).Qty -= trans.Qty;
                                if (MasterStock.Find(x => x.ItemCode == trans.ItemCode).Qty < 0)
                                    MasterStock.Find(x => x.ItemCode == trans.ItemCode).Cost = 0;
                            //  item.HrgNetto = (item.Qty != 0 ? (item.Cost / item.Qty) : item.Harga);
                            TransJual.Find(x => x.OeTransDId == trans.IcCardId).Cost = trans.Jumlah ;
                            TransJual.Find(x => x.OeTransDId == trans.IcCardId).HrgCost = trans.Harga;
                                break;

                            case "95":
                                trans.Jumlah = MasterStock.Find(x => x.ItemCode == trans.ItemCode).HrgNetto * trans.Qty;
                                //  trans.Harga = item.HrgNetto;
                                MasterStock.Find(x => x.ItemCode == trans.ItemCode).Cost += MasterStock.Find(x => x.ItemCode == trans.ItemCode).HrgNetto * trans.Qty;
                                MasterStock.Find(x => x.ItemCode == trans.ItemCode).Qty += trans.Qty;
                                //if (item.Qty < 0)
                                //    item.Cost = 0;
                                MasterStock.Find(x => x.ItemCode == trans.ItemCode).HrgNetto = (MasterStock.Find(x => x.ItemCode == trans.ItemCode).Qty != 0 ? (MasterStock.Find(x => x.ItemCode == trans.ItemCode).Cost / item.Qty) : MasterStock.Find(x => x.ItemCode == trans.ItemCode).Harga);
                            TransJual.Find(x => x.OeTransDId == trans.IcCardId).Cost = trans.Jumlah;
                            TransJual.Find(x => x.OeTransDId == trans.IcCardId).HrgCost = trans.Harga;
                            break;
                        }
                 //   }

                }
                //   _context.Update(item);
            }

           
            _context.UpdateRange(MasterStock);
            _contextOE.UpdateRange(TransJual);

            _context.SaveChanges();
            _contextOE.SaveChanges();
            

            // return Transaksi;

        }

        #endregion
    }


}
