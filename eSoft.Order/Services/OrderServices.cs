using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eSoft.Order.Data;
using eSoft.Order.Model;
using eSoft.Order.View;
using eSoft.Hutang.Data;
using eSoft.Hutang.Model;
using eSoft.Persediaan.Data;
using eSoft.Persediaan.Model;
using eSoft.Persediaan.View;

using Microsoft.EntityFrameworkCore;


namespace eSoft.Order.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly DbContextOrder _context;
        private readonly DbContextHutang _contextAp;
        private readonly DbContextPersediaan _contextIc;

        public OrderServices(DbContextOrder context,DbContextHutang contextHutang,DbContextPersediaan contextPersediaan)
        {
            _context = context;
            _contextAp = contextHutang;
            _contextIc = contextPersediaan;
        }

        #region getclass

        private ApSuppl GetVendorId(string id)
        {
            return _contextAp.ApSuppls.Where(x => x.Supplier == id).FirstOrDefault();
        }

        public ApHutang GetHutang(string bukti)
        {
            return _contextAp.ApHutangs.Where(x => x.Dokumen == bukti).FirstOrDefault();

        }

        #endregion getclass

        #region PoTransH class

        public PoTransH GetPoTrans(int id)
        {
            return _context.PoTransHs.Include(p => p.PoTransDs).Where(x => x.PoTransHId == id).FirstOrDefault();
        }


        public List<PoTransH> GetTransH()
        {
            List<PoTransH> PoTrans = new List<PoTransH>();
           
           
            try
            {
                PoTrans = _context.PoTransHs.OrderByDescending(x => x.Tanggal).Where(x => x.Kode == "82").ToList();

                foreach (var item in PoTrans)
                {
                    item.NamaVendor = _contextAp.ApSuppls.Where(x => x.Supplier == item.Vendor).FirstOrDefault().NamaLengkap;
                }

            }
            catch (Exception)
            {
                throw;
            }
            return PoTrans;
            // return  _context.CbTransHs.Include(p =>p.CbTransDs).OrderByDescending(x =>x.Tanggal).ToListAsync();
            //  return await _context.ApTransHs.OrderByDescending(x => x.Tanggal).ToListAsync();
            //  return await _context.ApTransHs.ToListAsync();

        }

        public List<PoTransH> Get3TransH()
        {
            List<PoTransH> PoTrans = new List<PoTransH>();

            PoTrans = _context.PoTransHs.OrderByDescending(x => x.Tanggal).Where(x => x.Tanggal > DateTime.Today.AddMonths(-3) && x.Kode == "82").ToList();

            return PoTrans;

            // return  _context.CbTransHs.Include(p =>p.CbTransDs).OrderByDescending(x =>x.Tanggal).ToListAsync();
            //   return _context.ApTransHs.OrderByDescending(x => x.Tanggal).Where(x => x.Tanggal > DateTime.Today.AddMonths(-3)).ToListAsync();

        }

        public List<PoTransD> GetTransD()
        {
            return  _context.PoTransDs.ToList();
        }

        public PoTransH AddTransH(PoTransHView trans)
        {
            //string test = codeview.SrcCode.ToUpper();
            //var cekFirst = _context.CbSrcCodes.Where(x => x.SrcCode == test).ToList();
            decimal mQty5 = 0;

            PoTransH transH = new PoTransH
            {
                NoLpb = GetNumber(),
                Vendor = trans.Vendor.ToUpper(),
                NamaVendor = trans.NamaVendor,
                
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

                PoTransDs = new List<PoTransD>()
            };

            foreach (var item in trans.PoTransDs)
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

                    transH.PoTransDs.Add(new PoTransD()
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

                        if (cekItem.JnsBrng == (int)jnsBrng.Stock)   // jika stock
                        {
                            cekItem.Qty += item.Qty;
                        }

                        if (cekItem.CostMethod == (int)costMethod.Moving_Avg)  // jika moving avarage
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
                _context.PoTransHs.Add(transH);
            }

            ApHutang hutang = new ApHutang
            {
                Kode = "IR",
                Dokumen = transH.NoLpb,
                Tanggal = transH.Tanggal,
                DueDate = transH.Tanggal,
                Supplier = transH.Vendor,
                Keterangan = transH.Keterangan,
                Jumlah = transH.Jumlah,
                Sisa = transH.Jumlah,
                SldSisa = transH.Jumlah,
                KodeTran = transH.Kode
            };
            _contextAp.ApHutangs.Add(hutang);

            var Vendor = GetVendorId(transH.Vendor);
            Vendor.Hutang += transH.Jumlah;

            _contextAp.ApSuppls.Update(Vendor);

            _context.SaveChanges();
             _contextAp.SaveChanges();
            _contextIc.SaveChanges();

            var TempTrans = GetTransDoc(transH.NoLpb);

            return TempTrans;
           
        }

        public PoTransH GetTransDoc(string docno)
        {
            return _context.PoTransHs.Include(p => p.PoTransDs).Where(x => x.NoLpb == docno).FirstOrDefault();
        }

        public async Task<bool> DelTransH(int id)
        {
            try
            {
                var ExistingTrans = _context.PoTransHs.Where(x => x.PoTransHId == id).FirstOrDefault();

                if (ExistingTrans != null)
                {
                    foreach (var item in ExistingTrans.PoTransDs)
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
                                if (cekItem.JnsBrng == (int)jnsBrng.Stock)   // jika stock
                                {
                                    cekItem.Qty -= item.Qty;
                                }

                                if (cekItem.CostMethod  == (int)costMethod.Moving_Avg)  // jika moving avarage
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
                    var Vendor = GetVendorId(ExistingTrans.Vendor);
                    var hutang = GetHutang(ExistingTrans.NoLpb);

                    Vendor.Hutang -= ExistingTrans.Jumlah;

                    _contextAp.ApSuppls.Update(Vendor);
                    _contextAp.ApHutangs.Remove(hutang);
                    _context.PoTransHs.Remove(ExistingTrans);
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

        public async Task<bool> EditTransH(PoTransHView trans)
        {
            decimal mQty5 = 0;

            var cekFirst = _contextAp.ApHutangs.Where(x => x.Dokumen == trans.NoLpb && x.Bayar == 0).FirstOrDefault();

            if (cekFirst != null)
            {
                try
                {

                    var ExistingTrans = _context.PoTransHs.Where(x => x.PoTransHId == trans.PoTransHId).FirstOrDefault();

                    if (ExistingTrans != null)
                    {
                        foreach (var item in ExistingTrans.PoTransDs)
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
                                    if (cekItem.JnsBrng== (int)jnsBrng.Stock)   // jika stock
                                    {
                                        cekItem.Qty -= item.Qty;
                                    }

                                    if (cekItem.CostMethod == (int)costMethod.Moving_Avg)  // jika moving avarage
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

                        var existingVendor = GetVendorId(ExistingTrans.Vendor);
                        existingVendor.Hutang -= ExistingTrans.Jumlah;

                        _contextAp.ApSuppls.Update(existingVendor);
                        _contextAp.ApHutangs.Remove(cekFirst);
                        _context.PoTransHs.Remove(ExistingTrans);

                        /* update nya */
                        PoTransH transH = new PoTransH
                        {
                            NoLpb = trans.NoLpb,
                            Vendor = trans.Vendor.ToUpper(),
                            NamaVendor = trans.NamaVendor,                           
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

                            PoTransDs = new List<PoTransD>()
                        };

                        foreach (var item in trans.PoTransDs)
                        {
                            if (item.Qty != 0)
                            {
                                if (transH.TotalQty != 0)
                                {
                                    mQty5 = (item.Jumlah - item.Discount) - (item.Qty / transH.TotalQty * transH.Ppn) + (item.Qty / transH.TotalQty * transH.Ongkos);
                                }

                                transH.PoTransDs.Add(new PoTransD()
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

                                    if (cekItem.JnsBrng == (int)jnsBrng.Stock)   // jika stock
                                    {
                                        cekItem.Qty += item.Qty;
                                    }

                                    if (cekItem.CostMethod  == (int)costMethod.Moving_Avg )  // jika moving avarage
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
                            Supplier = transH.Vendor,
                            Keterangan = transH.Keterangan,
                            Jumlah = transH.Jumlah,
                            Sisa = transH.Jumlah,
                            SldSisa = transH.Jumlah,
                            KodeTran = transH.Kode
                        };


                        var Vendor = GetVendorId(transH.Vendor);
                        Vendor.Hutang += transH.Jumlah;

                        _context.PoTransHs.Add(transH);
                        _contextAp.ApSuppls.Update(Vendor);
                        _contextAp.ApHutangs.Add(hutang);

                        
                        await _contextAp.SaveChangesAsync();
                        await _contextIc.SaveChangesAsync();
                        await _context.SaveChangesAsync();

                        //  var TempTrans = GetTransDoc(transH.NoLpb);

                        //   return transH;
                        return true;
                       
                    }
                    else
                    {
                        return false;
                    }

                }
                catch (Exception)
                {
                    throw;
                }
            }
            return false;



        }

        #endregion PoTransH Class

        public string GetNumber()
        {
            string kodeno = "BPB";
            string kodeurut = kodeno + '-';
            string thnbln = DateTime.Now.ToString("yyMM");
            string xbukti = kodeurut + thnbln.Substring(0, 2) + '2' + thnbln.Substring(2, 2) + '-';
            var maxvalue = "";
            var maxlist = _context.PoTransHs.Where(x => x.NoLpb.Substring(0, 10).Equals(xbukti)).ToList();
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
