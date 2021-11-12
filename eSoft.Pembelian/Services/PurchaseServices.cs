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
using eSoft.Persediaan.View;

using Microsoft.EntityFrameworkCore;


namespace eSoft.Pembelian.Services
{
    public class PurchaseServices : IPurchaseServices
    {
        private readonly DbContextBeli _context;
        private readonly DbContextHutang _contextAp;
        private readonly DbContextPersediaan _contextIc;
      

        public PurchaseServices(DbContextBeli context, DbContextHutang contextHutang, DbContextPersediaan contextPersediaan)
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


        public List<IrTransH> GetTransH()
        {
            List<IrTransH> IrTrans = new List<IrTransH>();


          //  try
          //  {
                IrTrans = _context.IrTransHs.OrderByDescending(x => x.Tanggal).Where(x => x.Kode == "82" || x.Kode == "83").ToList();

                foreach (var item in IrTrans)
                {
                    item.NamaSup = _contextAp.ApSuppls.Where(x => x.Supplier == item.Supplier).FirstOrDefault().NamaLengkap;
                   item.Jumlah = (item.Kode == "82" ? item.Jumlah : -1 * item.Jumlah);
                    item.TtlJumlah = (item.Kode == "82" ? item.TtlJumlah : -1 * item.TtlJumlah);
                    item.Ongkos = (item.Kode == "82" ? item.Ongkos : -1 * item.Ongkos);
                    item.Ppn = (item.Kode == "82" ? item.Ppn : -1 * item.Ppn);
                    
                }

        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
            return IrTrans;
            // return  _context.CbTransHs.Include(p =>p.CbTransDs).OrderByDescending(x =>x.Tanggal).ToListAsync();
            //  return await _context.ApTransHs.OrderByDescending(x => x.Tanggal).ToListAsync();
            //  return await _context.ApTransHs.ToListAsync();

        }

        public List<IrTransH> Get3TransH()
        {
            List<IrTransH> IrTrans = new List<IrTransH>();

            IrTrans = _context.IrTransHs.OrderByDescending(x => x.Tanggal).Where(x => x.Tanggal > DateTime.Today.AddMonths(-3) && x.Kode == "82" || x.Kode == "83").ToList();

            return IrTrans;

            // return  _context.CbTransHs.Include(p =>p.CbTransDs).OrderByDescending(x =>x.Tanggal).ToListAsync();
            //   return _context.ApTransHs.OrderByDescending(x => x.Tanggal).Where(x => x.Tanggal > DateTime.Today.AddMonths(-3)).ToListAsync();

        }

        public List<IrTransD> GetTransD()
        {
            return _context.IrTransDs.ToList();
        }

        public IrTransH AddTransH(IrTransHView trans)
        {
            //string test = codeview.SrcCode.ToUpper();
            //var cekFirst = _context.CbSrcCodes.Where(x => x.SrcCode == test).ToList();
            decimal mQty5 = 0;

            IrTransH transH = new IrTransH
            {
                NoLpb = GetNumber(),
                Supplier = trans.Supplier.ToUpper(),
                NamaSup = trans.NamaSup,
                Kurs = trans.Kurs,
                Nilai = trans.Nilai,
                Tanggal = trans.Tanggal,
                NoPrj = trans.NoPrj,
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
                        if(trans.Kurs != 0)
                        {
                            mQty5 = trans.Kurs * ((item.Jumlah - item.Discount) - (item.Qty / transH.TotalQty * transH.Ppn) + (item.Qty / transH.TotalQty * transH.Ongkos));
                        }
                        else
                        {
                            mQty5 = (item.Jumlah - item.Discount) - (item.Qty / transH.TotalQty * transH.Ppn) + (item.Qty / transH.TotalQty * transH.Ongkos);
                        }
                       
                    }
                    else
                    {
                        if(trans.Kurs != 0)
                        {
                            mQty5 = trans.Kurs * (item.Jumlah - item.Discount);
                        }
                        else
                        {
                            mQty5 = (item.Jumlah - item.Discount);
                        }
                     
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
                        if(trans.Kurs != 0)
                        {
                            cekItem.Harga = trans.Kurs * item.Harga;
                        }
                        else
                        {
                            cekItem.Harga = item.Harga;  // harga beli barang
                        }
                       

                        if (cekItem.JnsBrng == (int)jnsBrng.Stock)   // jika stock
                        {
                            cekItem.Qty += item.Qty;
                        }

                        if (cekItem.CostMethod == (int)costMethod.Moving_Avg)  // jika moving avarage
                        {
                            
                                cekItem.Cost += mQty5;
                        }
                        else
                        {
                            cekItem.Cost += item.Qty * cekItem.StdPrice;
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
                Kurs = transH.Kurs,
                Currency = trans.Currency,
                Nilai = transH.Nilai,
                KodeTran = transH.Kode
            };
            _contextAp.ApHutangs.Add(hutang);

            var supplier = GetSupplierId(transH.Supplier);
            supplier.Hutang += transH.Jumlah;

            _contextAp.ApSuppls.Update(supplier);

            _context.SaveChanges();
            _contextAp.SaveChanges();
            _contextIc.SaveChanges();

            var TempTrans = GetTransDoc(transH.NoLpb);

            return TempTrans;

        }


        public IrTransH GetTransDoc(string docno)
        {
            return _context.IrTransHs.Include(p => p.IrTransDs).Where(x => x.NoLpb == docno).FirstOrDefault();
        }

        public async Task<bool> DelTransH(int id)
        {
            string cKode = "82";


            try
            {
                var ExistingTrans = _context.IrTransHs.Where(x => x.IrTransHId == id).FirstOrDefault();

                if (ExistingTrans != null)
                {

                    cKode = ExistingTrans.Kode;

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
                                        Qty = (cKode == "82" ? -1 * item.Qty : item.Qty)
                                    };
                                    _contextIc.IcAltItems.Add(Produk);

                                }
                                else
                                {
                                    if (cKode == "82")
                                        cekLokasi1.Qty -= item.Qty;
                                    else
                                        cekLokasi1.Qty += item.Qty;

                                    _contextIc.IcAltItems.Update(cekLokasi1);
                                }
                                //   cekItem.Qty -= item.Qty;
                                //   cekItem.Cost -= item.JumDpp;
                                if (cekItem.JnsBrng == (int)jnsBrng.Stock)   // jika stock
                                {
                                    if (cKode == "82")
                                        cekItem.Qty -= item.Qty;
                                    else
                                        cekItem.Qty += item.Qty;
                                }

                                if (cekItem.CostMethod == (int)costMethod.Moving_Avg)  // jika moving avarage
                                {
                                    if (cKode == "82")
                                        cekItem.Cost -= item.JumDpp;
                                    else
                                        cekItem.Cost += item.JumDpp;
                                }
                                else
                                {
                                    if (cKode == "82")
                                        cekItem.Cost -= item.Qty*cekItem.StdPrice;
                                    else
                                        cekItem.Cost += item.Qty * cekItem.StdPrice;
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
                    if (cKode == "82")
                        supplier.Hutang -= ExistingTrans.Jumlah;
                    else
                        supplier.Hutang += ExistingTrans.Jumlah;

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
            string cKode = "82";

            cKode = trans.Kode;

            var cekFirst = _contextAp.ApHutangs.Where(x => x.Dokumen == trans.NoLpb && x.Bayar == 0).FirstOrDefault();

            if (cekFirst != null)
            {
                try
                {

                    var ExistingTrans = _context.IrTransHs.Where(x => x.IrTransHId == trans.IrTransHId).FirstOrDefault();

                    if (ExistingTrans != null)
                    {
                        cKode = ExistingTrans.Kode;

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
                                            Qty = (ExistingTrans.Kode == "82" ? -1 * item.Qty : item.Qty)
                                        };
                                        _contextIc.IcAltItems.Add(Produk);

                                    }
                                    else
                                    {
                                        if (ExistingTrans.Kode == "82")
                                            cekLokasi1.Qty -= item.Qty;
                                        else
                                            cekLokasi1.Qty += item.Qty;

                                        _contextIc.IcAltItems.Update(cekLokasi1);
                                    }
                                    if (cekItem.JnsBrng == (int)jnsBrng.Stock)   // jika stock
                                    {
                                        if (ExistingTrans.Kode == "82")
                                            cekItem.Qty -= item.Qty;
                                        else
                                            cekItem.Qty += item.Qty;
                                    }

                                    if (cekItem.CostMethod == (int)costMethod.Moving_Avg)  // jika moving avarage
                                    {
                                        if (ExistingTrans.Kode == "82")
                                            cekItem.Cost -= item.JumDpp;
                                        else
                                            cekItem.Cost += item.JumDpp;
                                    }
                                    else
                                    {
                                        if (ExistingTrans.Kode == "82")
                                            cekItem.Cost -= item.Qty*cekItem.StdPrice;
                                        else
                                            cekItem.Cost += item.Qty*cekItem.StdPrice;
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

                        if (ExistingTrans.Kode == "82")
                            existingsupplier.Hutang -= ExistingTrans.Jumlah;
                        else
                            existingsupplier.Hutang += ExistingTrans.Jumlah;

                        _contextAp.ApSuppls.Update(existingsupplier);
                        _contextAp.ApHutangs.Remove(cekFirst);
                        _context.IrTransHs.Remove(ExistingTrans);

                        /* update nya */
                        IrTransH transH = new IrTransH
                        {
                            NoLpb = trans.NoLpb,
                            Supplier = trans.Supplier.ToUpper(),
                            NamaSup = trans.NamaSup,
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
                            Kode = cKode,
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
                                    Kode = cKode,
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
                                            Qty = (cKode == "82" ? item.Qty : -1 * item.Qty)
                                        };
                                        _contextIc.IcAltItems.Add(Produk);

                                    }
                                    else
                                    {
                                        if (cKode == "82")
                                            cekLokasi1.Qty += item.Qty;
                                        else
                                            cekLokasi1.Qty -= item.Qty;

                                        _contextIc.IcAltItems.Update(cekLokasi1);
                                    }

                                    if (cekItem.JnsBrng == (int)jnsBrng.Stock)   // jika stock
                                    {
                                        if (cKode == "82")
                                            cekItem.Qty += item.Qty;
                                        else
                                            cekItem.Qty -= item.Qty;
                                    }

                                    if (cekItem.CostMethod == (int)costMethod.Moving_Avg)  // jika moving avarage
                                    {
                                        if (cKode == "82")
                                            cekItem.Cost += mQty5;
                                        else
                                            cekItem.Cost -= mQty5;
                                    }
                                    else
                                    {
                                        if (ExistingTrans.Kode == "82")
                                            cekItem.Cost -= item.Qty * cekItem.StdPrice;
                                        else
                                            cekItem.Cost += item.Qty * cekItem.StdPrice;
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
                        var supplier = GetSupplierId(transH.Supplier);

                        ApHutang hutang = new ApHutang
                        {
                            Kode = "IR",
                            Dokumen = transH.NoLpb,
                            Tanggal = transH.Tanggal,
                            DueDate = transH.Tanggal.AddDays(supplier.Termin),
                            Supplier = transH.Supplier,
                            Keterangan = transH.Keterangan,
                            Jumlah = (cKode == "82" ? transH.Jumlah : -1 * transH.Jumlah),
                            Sisa = (cKode == "82" ? transH.Jumlah : -1 * transH.Jumlah),
                            SldSisa = (cKode == "82" ? transH.Jumlah : -1 * transH.Jumlah),
                            KodeTran = transH.Kode
                        };


                       
                        if (cKode == "82")
                            supplier.Hutang += transH.Jumlah;
                        else
                            supplier.Hutang -= transH.Jumlah;

                        _context.IrTransHs.Add(transH);
                        _contextAp.ApSuppls.Update(supplier);
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

        #region retur Pembelian
        public IrTransH AddTransHRetur(IrTransHView trans)
        {
            //string test = codeview.SrcCode.ToUpper();
            //var cekFirst = _context.CbSrcCodes.Where(x => x.SrcCode == test).ToList();
            decimal mQty5 = 0;

            IrTransH transH = new IrTransH
            {
                NoLpb = GetNumberRetur(),
                Supplier = trans.Supplier.ToUpper(),
                NamaSup = trans.NamaSup,

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
                Kode = "83",
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
                        Lokasi = item.Lokasi,
                        Harga = item.Harga,
                        Qty = item.Qty,
                        Persen = item.Persen,
                        Discount = item.Discount,
                        Jumlah = item.Jumlah,
                        Kode = "83",
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
                                Qty = -1 * item.Qty
                            };
                            _contextIc.IcAltItems.Add(Produk);

                        }
                        else
                        {
                            cekLokasi1.Qty -= item.Qty;
                            _contextIc.IcAltItems.Update(cekLokasi1);
                        }

                        #endregion altitem

                        cekItem.Harga = item.Harga;  // harga beli barang

                        if (cekItem.JnsBrng == (int)jnsBrng.Stock)   // jika stock
                        {
                            cekItem.Qty -= item.Qty;
                        }

                        if (cekItem.CostMethod == (int)costMethod.Moving_Avg)  // jika moving avarage
                        {

                            cekItem.Cost -= mQty5;
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
                Jumlah = -1 * transH.Jumlah,
                Sisa = -1 * transH.Jumlah,
                SldSisa = -1 * transH.Jumlah,
                KodeTran = transH.Kode
            };
            _contextAp.ApHutangs.Add(hutang);

            var supplier = GetSupplierId(transH.Supplier);
            supplier.Hutang -= transH.Jumlah;

            _contextAp.ApSuppls.Update(supplier);

            _context.SaveChanges();
            _contextAp.SaveChanges();
            _contextIc.SaveChanges();

            var TempTrans = GetTransDoc(transH.NoLpb);

            return TempTrans;

        }



        public string GetNumberRetur()
        {
            string kodeno = "R/B";
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
        #endregion

        #region laporan
        public List<IrTransH> Laporan1(DateTime tgl1, DateTime tgl2)
        {
            List<IrTransH> transH = new List<IrTransH>();

            transH = _context.IrTransHs.Where(x => x.Tanggal >= tgl1 && x.Tanggal <= tgl2).OrderByDescending(t => t.Tanggal).ToList();

            foreach (var item in transH)
            {

                item.Jumlah = (item.Kode == "82" ? item.Jumlah : -1 * item.Jumlah);
                item.TtlJumlah = (item.Kode == "82" ? item.TtlJumlah : -1 * item.TtlJumlah);
                item.Ongkos = (item.Kode == "82" ? item.Ongkos : -1 * item.Ongkos);
                item.Ppn = (item.Kode == "82" ? item.Ppn : -1 * item.Ppn);
            }

            return transH;
        }

        public List<IrTransD> Detail1(int xKdHeader)
        {
            List<IrTransD> transD = new List<IrTransD>();

            transD = _context.IrTransDs.Where(x => x.IrTransHId == xKdHeader).ToList();

            return transD;
        }

        public List<IrTrans> Detail2(string xKdHeader, DateTime tgl1, DateTime tgl2)
        {
            List<IrTransH> transH = new List<IrTransH>();
            List<IrTransD> transD = new List<IrTransD>();
            List<IrTrans> trans = new List<IrTrans>();

            transH = _context.IrTransHs.Include(p => p.IrTransDs).Where(x => x.Tanggal >= tgl1 && x.Tanggal <= tgl2).ToList();
            transD = _context.IrTransDs.Where(x => x.ItemCode == xKdHeader && (x.Tanggal >= tgl1 && x.Tanggal <= tgl2)).ToList();

            if (transH != null && transD != null)
            {
                trans = (from header in transH
                         join detail in transD on header.IrTransHId equals detail.IrTransHId
                         select new IrTrans()
                         {
                             ItemCode = detail.ItemCode,
                             NamaItem = detail.NamaItem,
                             Harga = detail.Harga,
                             Persen = detail.Persen,
                             Discount = detail.Discount,
                             Satuan = detail.Satuan,
                             Ppn = header.Ppn,
                             PpnPersen = header.PpnPersen,
                             Ongkos = header.Ongkos,
                             Qty = detail.Qty,
                             Jumlah = detail.Jumlah,
                             Lokasi = detail.Lokasi,
                             NoLpb = header.NoLpb,
                             Supplier = header.Supplier,
                             NamaSuppl = header.NamaSup,
                             Tanggal = header.Tanggal,
                             NoPrj = header.NoPrj
                         }).ToList();
            }




            return trans;
        }
        #endregion
    }
}
