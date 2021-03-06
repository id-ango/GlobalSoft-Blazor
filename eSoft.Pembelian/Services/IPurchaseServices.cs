using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eSoft.Pembelian.Data;
using eSoft.Pembelian.Model;
using eSoft.Pembelian.View;
using eSoft.Hutang.Data;
using eSoft.Hutang.Model;
using eSoft.Persediaan.Data;
using eSoft.Persediaan.Model;

namespace eSoft.Pembelian.Services
{
    public interface IPurchaseServices
    {
        IrTransH GetIrTrans(int id);
        List<IrTransH> GetTransH();
        List<IrTransH> Get3TransH();
        List<IrTransD> GetTransD();
        IrTransH AddTransH(IrTransHView trans);
        Task<bool> DelTransH(int id);
        Task<bool> EditTransH(IrTransHView trans);
    }
}
