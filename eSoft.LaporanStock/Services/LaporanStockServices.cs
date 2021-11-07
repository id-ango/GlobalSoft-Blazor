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

        #region Laporan
        public List<IcStockCardView> CetakMutasi(DateTime Tanggal1, DateTime Tanggal2, string kodeBank)
        {

            List<IcStockCardView> Transaksi = new List<IcStockCardView>();
            //     TransHView Transh = new TransHView() { TransDs = new List<TransDView>() };

         //   var bankawal = _context.IcItems
         //       .Where(x => x.ItemCode == kodeBank).FirstOrDefault();

            var TransAwalJual = _contextOE.OeTransDs.Where(x =>x.ItemCode == kodeBank && x.Tanggal < Tanggal1)                
                .ToList();
            var TransAwalBeli = _contextIR.IrTransDs.Where(x => x.ItemCode == kodeBank && x.Tanggal < Tanggal1)
                .ToList();
            var TransAwalIC = _context.IcTransDs.Where(x => x.ItemCode == kodeBank && x.Tanggal < Tanggal1)
                .ToList();

            //Transaksi   
            // .Where(x => x.ItemCode == kodeBank && ( x.Tanggal < Tanggal1))
            //.Select(x => new RekeningView
            //{
            //    KodeBank = x.KodeBank,
            //    DocNo = x.DocNo,
            //    Tanggal = x.Tanggal,
            //    Keterangan = x.Keterangan,
            //    Kurs = x.Kurs,
            //    Saldo = (string.IsNullOrEmpty(bankawal.Kurs) ? x.Saldo : x.KSaldo)

            //})
            // .ToList();

            var SaldoAwal = TransAwalJual.Sum(x => (x.Kode == "94" ? -1 * x.Qty : x.Qty))
                            + TransAwalBeli.Sum(x => (x.Kode == "82" ? x.Qty : -1 * x.Qty))
                            + TransAwalBeli.Sum(x => (x.Kode == "81" ? x.Qty : 0));
               

            Transaksi.Add(new IcStockCardView
            {
                ItemCode = kodeBank,               
                Tanggal = Tanggal1,
                Keterangan = "Saldo Awal",
                Jumlah = SaldoAwal

            }
                );


            //var Rincian = _context.CbTransHs
            //    .Where(x => x.KodeBank == kodeBank && (Tanggal1.Date <= x.Tanggal.Date && x.Tanggal.Date <= Tanggal2.Date))
            //    .OrderBy(x => x.Tanggal)
            //   .Select(x => new RekeningView
            //   {
            //       KodeBank = x.KodeBank,
            //       DocNo = x.DocNo,
            //       Tanggal = x.Tanggal,
            //       Keterangan = x.Keterangan,
            //       Kurs = x.Kurs,
            //       Saldo = string.IsNullOrEmpty(bankawal.Kurs) ? x.Saldo : x.KSaldo


            //   })
            //    .ToList();

            //Transaksi.AddRange(Rincian);
            //SaldoAwal = 0;

            //Transaksi = Transaksi.Select(i => { SaldoAwal += i.Saldo; i.Balance = SaldoAwal; return i; }).ToList();


            return Transaksi;
        }
        #endregion
    }


}
