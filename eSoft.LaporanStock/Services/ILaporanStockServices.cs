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
    public interface ILaporanStockServices
    {
        List<IcStockCardView> CetakMutasi(DateTime Tanggal1, DateTime Tanggal2, string kodeBank);
    }
}
