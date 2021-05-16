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

using Microsoft.EntityFrameworkCore;


namespace eSoft.Pembelian.Services
{
    public class PurchaseServices : IPurchaseServices
    {
        private readonly DbContextBeli _context;
        private readonly DbContextHutang _contextAp;
        private readonly DbContextPersediaan _contextIc;

        public PurchaseServices(DbContextBeli context,DbContextHutang contextHutang,DbContextPersediaan contextPersediaan)
        {
            _context = context;
            _contextAp = contextHutang;
            _contextIc = contextPersediaan;
        }

        #region getclass

        private ApSuppl GetSupplierId(string id)
        {
            return _contextAp.ApSuppls.Where(x => x.Supplier == id).FirstOrDefault();
        }

        public ApHutang GetHutang(string bukti)
        {
            return _contextAp.ApHutangs.Where(x => x.Dokumen == bukti).FirstOrDefault();

        }

        #endregion getclass

        #region IrTransH class

        public IrTransH GetIrTrans(int id)
        {
            return _context.IrTransHs.Include(p => p.IrTransDs).Where(x => x.IrTransHId == id).FirstOrDefault();
        }


        public async Task<List<IrTransH>> GetTransH()
        {
            List<IrTransH> IrTrans = new List<IrTransH>();
            try
            {
                IrTrans = await _context.IrTransHs.OrderByDescending(x => x.Tanggal).Where(x => x.Kode == "82").ToListAsync();

            }
            catch (Exception)
            {
                throw;
            }
            return IrTrans;
            // return  _context.CbTransHs.Include(p =>p.CbTransDs).OrderByDescending(x =>x.Tanggal).ToListAsync();
            //  return await _context.ApTransHs.OrderByDescending(x => x.Tanggal).ToListAsync();
            //  return await _context.ApTransHs.ToListAsync();

        }

        public async Task<List<IrTransH>> Get3TransH()
        {
            List<IrTransH> IrTrans = new List<IrTransH>();

            IrTrans = await _context.IrTransHs.OrderByDescending(x => x.Tanggal).Where(x => x.Tanggal > DateTime.Today.AddMonths(-3) && x.Kode == "82").ToListAsync();

            return IrTrans;

            // return  _context.CbTransHs.Include(p =>p.CbTransDs).OrderByDescending(x =>x.Tanggal).ToListAsync();
            //   return _context.ApTransHs.OrderByDescending(x => x.Tanggal).Where(x => x.Tanggal > DateTime.Today.AddMonths(-3)).ToListAsync();

        }

        public async Task<List<IrTransD>> GetTransD()
        {
            return await _context.IrTransDs.ToListAsync();
        }

        public async Task<bool> AddTransH(IrTransHView trans)
        {
            //string test = codeview.SrcCode.ToUpper();
            //var cekFirst = _context.CbSrcCodes.Where(x => x.SrcCode == test).ToList();
            decimal mQty5 = 0;

            IrTransH transH = new IrTransH
            {
                NoLpb = GetNumber(),
                Supplier = trans.Supplier.ToUpper(),
                NamaSup = trans.NamaSup,
                Lokasi = trans.Lokasi.ToUpper(),
                Tanggal = trans.Tanggal,
                Keterangan = trans.Keterangan,
                Jumlah = trans.Jumlah,
                Ongkos = trans.Ongkos,
                Ppn = trans.Ppn,
                PpnPersen = trans.PpnPersen,
                TtlJumlah = trans.TtlJumlah,
                DPayment = trans.DPayment,
                Tagihan = trans.Tagihan,
                TotalQty = trans.TotalQty,
                Kode = "82",
                Cek = "1",

                IrTransDs = new List<IrTransD>()
            };

            foreach (var item in trans.IrTransDs)
            {
                if (item.Qty != 0)
                {
                    if (transH.TotalQty != 0)
                    {
                        mQty5 = (item.Jumlah - item.Discount) - (item.Qty / transH.TotalQty * transH.Ppn) + (item.Qty / transH.TotalQty * transH.Ongkos);
                    }
                    else
                    {
                        mQty5 = (item.Jumlah - item.Discount);
                    }

                    transH.IrTransDs.Add(new IrTransD()
                    {
                        ItemCode = item.ItemCode.ToUpper(),
                        NamaItem = item.NamaItem,
                        Satuan = item.Satuan,
                        Lokasi = transH.Lokasi,
                        Harga = item.Harga,
                        Qty = item.Qty,
                        Persen = item.Persen,
                        Discount = item.Discount,
                        Jumlah = item.Jumlah,
                        Kode = "82",
                        NoLpb = transH.NoLpb,
                        Tanggal = trans.Tanggal,
                        JumDpp = mQty5
                    });

                    IcItem cekItem = _contextIc.IcItems.Where(x => x.ItemCode == item.ItemCode).FirstOrDefault();

                    if (cekItem != null)
                    {
                        #region altitem

                        IcAltItem cekLokasi1 = _contextIc.IcAltItems.Where(x => x.ItemCode == item.ItemCode && x.Lokasi == item.Lokasi).FirstOrDefault();
                        if (cekLokasi1 == null)
                        {
                            IcAltItem Produk = new IcAltItem()
                            {
                                ItemCode = cekItem.ItemCode.ToUpper(),
                                NamaItem = cekItem.NamaItem,
                                Satuan = cekItem.Satuan,
                                Lokasi = item.Lokasi,
                                Qty = item.Qty
                            };
                            _contextIc.IcAltItems.Add(Produk);

                        }
                        else
                        {
                            cekLokasi1.Qty += item.Qty;
                            _contextIc.IcAltItems.Update(cekLokasi1);
                        }

                        #endregion altitem

                        cekItem.Harga = item.Harga;  // harga beli barang

                        if (cekItem.JnsBrng.Equals("Stock"))   // jika stock
                        {
                            cekItem.Qty += item.Qty;
                        }

                        if (cekItem.CostMethod.Equals("Moving Avarage"))  // jika moving avarage
                        {

                            cekItem.Cost += mQty5;
                        }

                        if (cekItem.Qty != 0)
                        {
                            cekItem.HrgNetto = cekItem.Cost / cekItem.Qty;
                        }
                        else
                        {
                            cekItem.HrgNetto = cekItem.Harga;
                        }

                        _contextIc.IcItems.Update(cekItem);

                    }
                }
                _context.IrTransHs.Add(transH);
            }

            ApHutang hutang = new ApHutang
            {
                Kode = "IR",
                Dokumen = transH.NoLpb,
                Tanggal = transH.Tanggal,
                DueDate = transH.Tanggal,
                Supplier = transH.Supplier,
                Keterangan = transH.Keterangan,
                Jumlah = transH.Jumlah,
                Sisa = transH.Jumlah,
                SldSisa = transH.Jumlah,
                KodeTran = transH.Kode
            };
            _contextAp.ApHutangs.Add(hutang);

            var supplier = GetSupplierId(transH.Supplier);
            supplier.Hutang += transH.Jumlah;

            _contextAp.ApSuppls.Update(supplier);

            await _context.SaveChangesAsync();
            await _contextAp.SaveChangesAsync();
            await _contextIc.SaveChangesAsync();
            return true;
        }


        public async Task<bool> DelTransH(int id)
        {
            try
            {
                var ExistingTrans = _context.IrTransHs.Where(x => x.IrTransHId == id).FirstOrDefault();

                if (ExistingTrans != null)
                {
                    foreach (var item in ExistingTrans.IrTransDs)
                    {
                        if (item.Qty != 0)
                        {

                            IcItem cekItem = _contextIc.IcItems.Where(x => x.ItemCode == item.ItemCode).FirstOrDefault();
                            if (cekItem != null)
                            {
                                IcAltItem cekLokasi1 = _contextIc.IcAltItems.Where(x => x.ItemCode == item.ItemCode && x.Lokasi == item.Lokasi).FirstOrDefault();
                                if (cekLokasi1 == null)
                                {
                                    IcAltItem Produk = new IcAltItem()
                                    {
                                        ItemCode = cekItem.ItemCode.ToUpper(),
                                        NamaItem = cekItem.NamaItem,
                                        Satuan = cekItem.Satuan,
                                        Lokasi = item.Lokasi,
                                        Qty = -1 * item.Qty
                                    };
                                    _contextIc.IcAltItems.Add(Produk);

                                }
                                else
                                {
                                    cekLokasi1.Qty -= item.Qty;
                                    _contextIc.IcAltItems.Update(cekLokasi1);
                                }
                                //   cekItem.Qty -= item.Qty;
                                //   cekItem.Cost -= item.JumDpp;
                                if (cekItem.JnsBrng.Equals("Stock"))   // jika stock
                                {
                                    cekItem.Qty -= item.Qty;
                                }

                                if (cekItem.CostMethod.Equals("Moving Avarage"))  // jika moving avarage
                                {

                                    cekItem.Cost -= item.JumDpp;
                                }
                                if (cekItem.Qty != 0)
                                {
                                    cekItem.HrgNetto = cekItem.Cost / cekItem.Qty;
                                }
                                else
                                {
                                    cekItem.HrgNetto = cekItem.Harga;
                                }

                                _contextIc.IcItems.Update(cekItem);

                            }
                        }

                    }
                    var supplier = GetSupplierId(ExistingTrans.Supplier);
                    var hutang = GetHutang(ExistingTrans.NoLpb);

                    supplier.Hutang -= ExistingTrans.Jumlah;

                    _contextAp.ApSuppls.Update(supplier);
                    _contextAp.ApHutangs.Remove(hutang);
                    _context.IrTransHs.Remove(ExistingTrans);
                    await _context.SaveChangesAsync();
                    await _contextAp.SaveChangesAsync();
                    await _contextIc.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return false;
        }

        public async Task<bool> EditTransH(IrTransHView trans)
        {
            decimal mQty5 = 0;

            var cekFirst = _contextAp.ApHutangs.Where(x => x.Dokumen == trans.NoLpb).FirstOrDefault();

            if (cekFirst != null)
            {
                try
                {
                    var ExistingTrans = _context.IrTransHs.Where(x => x.IrTransHId == trans.IrTransHId).FirstOrDefault();

                    if (ExistingTrans != null)
                    {
                        foreach (var item in ExistingTrans.IrTransDs)
                        {
                            if (item.Qty != 0)
                            {

                                IcItem cekItem = _contextIc.IcItems.Where(x => x.ItemCode == item.ItemCode).FirstOrDefault();
                                if (cekItem != null)
                                {
                                    IcAltItem cekLokasi1 = _contextIc.IcAltItems.Where(x => x.ItemCode == item.ItemCode && x.Lokasi == item.Lokasi).FirstOrDefault();
                                    if (cekLokasi1 == null)
                                    {
                                        IcAltItem Produk = new IcAltItem()
                                        {
                                            ItemCode = cekItem.ItemCode.ToUpper(),
                                            NamaItem = cekItem.NamaItem,
                                            Satuan = cekItem.Satuan,
                                            Lokasi = item.Lokasi,
                                            Qty = -1 * item.Qty
                                        };
                                        _contextIc.IcAltItems.Add(Produk);

                                    }
                                    else
                                    {
                                        cekLokasi1.Qty -= item.Qty;
                                        _contextIc.IcAltItems.Update(cekLokasi1);
                                    }
                                    if (cekItem.JnsBrng.Equals("Stock"))   // jika stock
                                    {
                                        cekItem.Qty -= item.Qty;
                                    }

                                    if (cekItem.CostMethod.Equals("Moving Avarage"))  // jika moving avarage
                                    {

                                        cekItem.Cost -= item.JumDpp;
                                    }

                                    if (cekItem.Qty != 0)
                                    {
                                        cekItem.HrgNetto = cekItem.Cost / cekItem.Qty;
                                    }
                                    else
                                    {
                                        cekItem.HrgNetto = cekItem.Harga;
                                    }

                                    _contextIc.IcItems.Update(cekItem);

                                }
                            }

                        }

                        var existingsupplier = GetSupplierId(ExistingTrans.Supplier);
                        existingsupplier.Hutang -= ExistingTrans.Jumlah;

                        _contextAp.ApSuppls.Update(existingsupplier);
                        _contextAp.ApHutangs.Remove(cekFirst);
                        _context.IrTransHs.Remove(ExistingTrans);

                        /* update nya */
                        IrTransH transH = new IrTransH
                        {
                            NoLpb = trans.NoLpb,
                            Supplier = trans.Supplier.ToUpper(),
                            NamaSup = trans.NamaSup,
                            Lokasi = trans.Lokasi.ToUpper(),
                            Tanggal = trans.Tanggal,
                            Keterangan = trans.Keterangan,
                            Jumlah = trans.Jumlah,
                            Ongkos = trans.Ongkos,
                            Ppn = trans.Ppn,
                            PpnPersen = trans.PpnPersen,
                            TtlJumlah = trans.TtlJumlah,
                            DPayment = trans.DPayment,
                            Tagihan = trans.Tagihan,
                            TotalQty = trans.TotalQty,
                            Kode = "82",
                            Cek = "1",

                            IrTransDs = new List<IrTransD>()
                        };

                        foreach (var item in trans.IrTransDs)
                        {
                            if (item.Qty != 0)
                            {
                                if (transH.TotalQty != 0)
                                {
                                    mQty5 = (item.Jumlah - item.Discount) - (item.Qty / transH.TotalQty * transH.Ppn) + (item.Qty / transH.TotalQty * transH.Ongkos);
                                }

                                transH.IrTransDs.Add(new IrTransD()
                                {
                                    ItemCode = item.ItemCode.ToUpper(),
                                    NamaItem = item.NamaItem,
                                    Satuan = item.Satuan,
                                    Lokasi = item.Lokasi,
                                    Harga = item.Harga,
                                    Qty = item.Qty,
                                    Persen = item.Persen,
                                    Discount = item.Discount,
                                    Jumlah = item.Jumlah,
                                    Kode = "82",
                                    NoLpb = transH.NoLpb,
                                    Tanggal = trans.Tanggal,
                                    JumDpp = mQty5
                                });


                                IcItem cekItem = _contextIc.IcItems.Where(x => x.ItemCode == item.ItemCode).FirstOrDefault();
                                if (cekItem != null)
                                {
                                    IcAltItem cekLokasi1 = _contextIc.IcAltItems.Where(x => x.ItemCode == item.ItemCode && x.Lokasi == item.Lokasi).FirstOrDefault();
                                    if (cekLokasi1 == null)
                                    {
                                        IcAltItem Produk = new IcAltItem()
                                        {
                                            ItemCode = cekItem.ItemCode.ToUpper(),
                                            NamaItem = cekItem.NamaItem,
                                            Satuan = cekItem.Satuan,
                                            Lokasi = item.Lokasi,
                                            Qty = item.Qty
                                        };
                                        _contextIc.IcAltItems.Add(Produk);

                                    }
                                    else
                                    {
                                        cekLokasi1.Qty += item.Qty;
                                        _contextIc.IcAltItems.Update(cekLokasi1);
                                    }

                                    if (cekItem.JnsBrng.Equals("Stock"))   // jika stock
                                    {
                                        cekItem.Qty += item.Qty;
                                    }

                                    if (cekItem.CostMethod.Equals("Moving Avarage"))  // jika moving avarage
                                    {

                                        cekItem.Cost += mQty5;
                                    }

                                    if (cekItem.Qty != 0)
                                    {
                                        cekItem.HrgNetto = cekItem.Cost / cekItem.Qty;
                                    }
                                    else
                                    {
                                        cekItem.HrgNetto = cekItem.Harga;
                                    }

                                    _contextIc.IcItems.Update(cekItem);

                                }
                            }

                        }

                        ApHutang hutang = new ApHutang
                        {
                            Kode = "IR",
                            Dokumen = transH.NoLpb,
                            Tanggal = transH.Tanggal,
                            DueDate = transH.Tanggal,
                            Supplier = transH.Supplier,
                            Keterangan = transH.Keterangan,
                            Jumlah = transH.Jumlah,
                            Sisa = transH.Jumlah,
                            SldSisa = transH.Jumlah,
                            KodeTran = transH.Kode
                        };


                        var supplier = GetSupplierId(transH.Supplier);
                        supplier.Hutang += transH.Jumlah;

                        _context.IrTransHs.Add(transH);
                        _contextAp.ApSuppls.Update(supplier);
                        _contextAp.ApHutangs.Add(hutang);

                        await _context.SaveChangesAsync();
                        await _contextAp.SaveChangesAsync();
                        await _contextIc.SaveChangesAsync();
                        return true;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }






            return false;
        }

        #endregion IrTransH Class

        public string GetNumber()
        {
            string kodeno = "BPB";
            string kodeurut = kodeno + '-';
            string thnbln = DateTime.Now.ToString("yyMM");
            string xbukti = kodeurut + thnbln.Substring(0, 2) + '2' + thnbln.Substring(2, 2) + '-';
            var maxvalue = "";
            var maxlist = _context.IrTransHs.Where(x => x.NoLpb.Substring(0, 10).Equals(xbukti)).ToList();
            if (maxlist != null)
            {
                maxvalue = maxlist.Max(x => x.NoLpb);

            }

            //            var maxvalue = (from e in db.CbTransHs where  e.Docno.Substring(0, 7) == kodeno + thnbln select e).Max();
            string nourut = "00000";
            if (maxvalue == null)
            {
                nourut = "00000";
            }
            else
            {
                nourut = maxvalue.Substring(10, 5);
            }

            //  nourut =Convert.ToString(Int32.Parse(nourut) + 1);


            string cAngNo = xbukti + (Int32.Parse(nourut) + 1).ToString("00000");
            // var maxvalue = (from e in db.AptTranss where e.NoRef.Substring(0, 7) == "ANG" + cAngNo select e.NoRef.Max()).FirstOrDefault();
            return cAngNo;

        }
    }
}
