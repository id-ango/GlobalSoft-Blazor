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
    public class LaporanStockServices: ILaporanStockServices
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

           

            TransAwalJual = _contextOE.OeTransDs.Where(x =>x.ItemCode == kodeBank && x.Tanggal < Tanggal1)                
                .ToList();
             TransAwalBeli = _contextIR.IrTransDs.Where(x => x.ItemCode == kodeBank && x.Tanggal < Tanggal1)
                .ToList();
             TransAwalIC = _context.IcTransDs.Where(x => x.ItemCode == kodeBank && x.Tanggal < Tanggal1)
                .ToList();

           
            if (TransAwalJual != null)
            {
                SldAwalJual = TransAwalJual.Sum(x => (x.Kode == "94" ? -1 * x.Qty : x.Qty));
            } else
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
                SldAwalIC = TransAwalIC.Sum(x => (x.Kode == "81" ? x.QtyShp : 0));
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

            transHIR = _contextIR.IrTransHs.Include(p => p.IrTransDs).Where(x => x.Tanggal >= Tanggal1 && x.Tanggal <= Tanggal2).ToList();
            transDIR = _contextIR.IrTransDs.Where(x => x.ItemCode == kodeBank).ToList();

            if (transHIR != null && transDIR != null)
            {
                var Rincian1 = (from e in transHIR
                               join f in transDIR on e.IrTransHId equals f.IrTransHId
                               select new IcStockCardView()
                               {
                                   ItemCode = f.ItemCode,
                                   Keterangan = e.NamaSup+", "+e.Keterangan,
                                   Tanggal = f.Tanggal.Date,
                                   Dokumen = e.NoLpb,
                                   Qty = (e.Kode == "82" ? f.Qty : -1*f.Qty),
                                   Cost = f.JumDpp
                               }).ToList();

                Transaksi.AddRange(Rincian1);
            }

            transHIC = _context.IcTransHs.Include(p => p.IcTransDs).Where(x => (x.Tanggal >= Tanggal1 && x.Tanggal <= Tanggal2) && x.Kode == "81").ToList();
            transDIC = _context.IcTransDs.Where(x => x.ItemCode == kodeBank).ToList();

            if (transHIC != null && transDIC != null)
            {
                var Rincian2 = (from e in transHIC
                               join f in transDIC on e.IcTransHId equals f.IcTransHId
                               select new IcStockCardView()
                               {
                                   ItemCode = f.ItemCode,
                                   Keterangan = e.Keterangan,
                                   Tanggal = f.Tanggal.Date,
                                   Dokumen = e.NoFaktur,
                                   Qty = f.QtyShp,
                                   Cost = f.Jumlah
                               }).ToList();

                Transaksi.AddRange(Rincian2);
            }


            transHOE = _contextOE.OeTransHs.Include(p => p.OeTransDs).Where(x => x.Tanggal >= Tanggal1 && x.Tanggal <= Tanggal2).ToList();
            transDOE = _contextOE.OeTransDs.Where(x => x.ItemCode == kodeBank).ToList();

            if (transHOE != null && transDOE != null)
            {
                var Rincian3 = (from e in transHOE
                               join f in transDOE on e.OeTransHId equals f.OeTransHId
                               select new IcStockCardView()
                               {
                                   ItemCode = f.ItemCode,
                                   Keterangan = e.NamaCust+", "+e.Keterangan,
                                   Tanggal = f.Tanggal.Date,
                                   Dokumen = e.NoLpb,
                                   Qty = (e.Kode == "94" ? -1*f.Qty :  f.Qty),
                                   Cost = f.JumDpp
                               }).ToList();

                Transaksi.AddRange(Rincian3);
            }

            Transaksi = Transaksi.OrderBy(x =>x.Tanggal).ToList();
           
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
    }


}
