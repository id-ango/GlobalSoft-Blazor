using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eSoft.Penjualan.Data;
using eSoft.Penjualan.Model;
using eSoft.Penjualan.View;
using eSoft.Piutang.Data;
using eSoft.Piutang.Model;
using eSoft.Persediaan.Data;
using eSoft.Persediaan.Model;
using eSoft.Persediaan.View;

namespace eSoft.Penjualan.Services
{
    public interface ISalesServices
    {
        OeTransH GetOeTrans(int id);
        List<OeTransH> GetTransH();
        List<OeTransH> GetTransHNon();
        List<OeTransH> Get3TransH();
        List<OeTransD> GetTransD();
        OeTransH AddTransH(OeTransHView trans,bool pajak);
        Task<bool> DelTransH(int id);
        Task<bool> EditTransH(OeTransHView trans);
        List<OeTransH> Laporan1(DateTime tgl1, DateTime tgl2);
        List<OeTransD> Detail1(int xKdHeader);
    }
}

