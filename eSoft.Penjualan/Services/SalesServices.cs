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

using Microsoft.EntityFrameworkCore;


namespace eSoft.Penjualan.Services
{
    public class SalesServices : ISalesServices
    {
        private readonly DbContextJual _context;
        private readonly DbContextPiutang _contextAr;
        private readonly DbContextPersediaan _contextIc;

        public SalesServices(DbContextJual context, DbContextPiutang contextPiutang, DbContextPersediaan contextPersediaan)
        {
            _context = context;
            _contextAr = contextPiutang;
            _contextIc = contextPersediaan;
        }

        #region laporanpenjualan

        public List<OeTransH> Laporan1(DateTime tgl1, DateTime tgl2)
        {
            List<OeTransH> transH = new List<OeTransH>();

            transH = _context.OeTransHs.Where(x => x.Tanggal >= tgl1 && x.Tanggal <= tgl2).OrderByDescending(t => t.Tanggal).ToList();

            foreach (var item in transH)
            {

                item.Jumlah = (item.Kode == "94" ? item.Jumlah : -1 * item.Jumlah);
                item.TtlJumlah = (item.Kode == "94" ? item.TtlJumlah : -1 * item.TtlJumlah);
                item.Ongkos = (item.Kode == "94" ? item.Ongkos : -1 * item.Ongkos);
                item.Ppn = (item.Kode == "94" ? item.Ppn : -1 * item.Ppn);
            }

            return transH;
        }

        public List<OeTransD> Detail1(int xKdHeader)
        {
            List<OeTransD> transD = new List<OeTransD>();

            transD = _context.OeTransDs.Where(x => x.OeTransHId == xKdHeader).ToList();

            return transD;
        }


        #endregion

        #region getclass

        private ArCust GetCustomerId(string id)
        {
            return _contextAr.ArCusts.Where(x => x.Customer == id).FirstOrDefault();
        }

        public ArPiutng GetPiutang(string bukti)
        {
            return _contextAr.ArPiutngs.Where(x => x.Dokumen == bukti).FirstOrDefault();

        }

        #endregion getclass

        #region OeTransH class

        public OeTransH GetOeTrans(int id)
        {
            return _context.OeTransHs.Include(p => p.OeTransDs).Where(x => x.OeTransHId == id).FirstOrDefault();
        }


        public List<OeTransH> GetTransH()
        {
            List<OeTransH> OeTrans = new List<OeTransH>();


            try
            {
                OeTrans = (from e in _context.OeTransHs
                           orderby e.Tanggal descending
                           where ((e.Kode == "94" || e.Kode == "95") && e.Pajak == true)
                           select new OeTransH
                           {
                               OeTransHId = e.OeTransHId,
                               NoLpb = e.NoLpb,
                               Customer = e.Customer,
                               NamaCust =e.NamaCust,

                               Tanggal = e.Tanggal,
                               Keterangan = e.Keterangan,

                               Jumlah = (e.Kode == "94" ? e.Jumlah : -1 * e.Jumlah),
                               TtlJumlah = (e.Kode == "94" ? e.TtlJumlah : -1 * e.TtlJumlah),
                               Ongkos = (e.Kode == "94" ? e.Ongkos : -1 * e.Ongkos),
                               Ppn = (e.Kode == "94" ? e.Ppn : -1 * e.Ppn),

                               PpnPersen = e.PpnPersen,

                               DPayment = e.DPayment,
                               Tagihan = e.Tagihan,
                               TotalQty = e.TotalQty,
                               Kode = e.Kode,
                               Cek = e.Cek,
                               Pajak = e.Pajak
                               

                           }).ToList();

                //    OeTrans = _context.OeTransHs.OrderByDescending(x => x.Tanggal).Where(x => (x.Kode == "94" || x.Kode == "95") && x.Pajak == true).ToList();


                // foreach (var item in OeTrans)
                //  {
                //      item.NamaCust = _contextAr.ArCusts.Where(x => x.Customer == item.Customer).FirstOrDefault().NamaLengkap;
                //  }

            }
            catch (Exception)
            {
                throw;
            }
            return OeTrans;
            // return  _context.CbTransHs.Include(p =>p.CbTransDs).OrderByDescending(x =>x.Tanggal).ToListAsync();
            //  return await _context.ApTransHs.OrderByDescending(x => x.Tanggal).ToListAsync();
            //  return await _context.ApTransHs.ToListAsync();

        }

        public List<OeTransH> GetTransHNon()
        {
            List<OeTransH> OeTrans = new List<OeTransH>();


            try
            {
                OeTrans = _context.OeTransHs.OrderByDescending(x => x.Tanggal).Where(x => (x.Kode == "94" || x.Kode == "95") && x.Pajak == false).ToList();

                // foreach (var item in OeTrans)
                //  {
                //      item.NamaCust = _contextAr.ArCusts.Where(x => x.Customer == item.Customer).FirstOrDefault().NamaLengkap;
                //  }

            }
            catch (Exception)
            {
                throw;
            }
            return OeTrans;
            // return  _context.CbTransHs.Include(p =>p.CbTransDs).OrderByDescending(x =>x.Tanggal).ToListAsync();
            //  return await _context.ApTransHs.OrderByDescending(x => x.Tanggal).ToListAsync();
            //  return await _context.ApTransHs.ToListAsync();

        }

        public List<OeTransH> Get3TransH()
        {
            List<OeTransH> OeTrans = new List<OeTransH>();

            OeTrans = _context.OeTransHs.OrderByDescending(x => x.Tanggal).Where(x => x.Tanggal > DateTime.Today.AddMonths(-3) && (x.Kode == "94" || x.Kode == "95")).ToList();

            return OeTrans;

            // return  _context.CbTransHs.Include(p =>p.CbTransDs).OrderByDescending(x =>x.Tanggal).ToListAsync();
            //   return _context.ApTransHs.OrderByDescending(x => x.Tanggal).Where(x => x.Tanggal > DateTime.Today.AddMonths(-3)).ToListAsync();

        }

        public List<OeTransD> GetTransD()
        {
            return _context.OeTransDs.ToList();
        }

        public OeTransH AddTransH(OeTransHView trans, bool pajak)
        {
            //string test = codeview.SrcCode.ToUpper();
            //var cekFirst = _context.CbSrcCodes.Where(x => x.SrcCode == test).ToList();
            decimal mQty5 = 0;

            OeTransH transH = new OeTransH
            {

                NoLpb = (pajak ? GetNumberTax() : GetNumber()),
                Customer = trans.Customer.ToUpper(),
                NamaCust = trans.NamaCust,
                AlamatKirim = trans.AlamatKirim,
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
                Kode = "94",
                Cek = "1",
                Pajak = pajak,
                OeTransDs = new List<OeTransD>()
            };

            foreach (var item in trans.OeTransDs)
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

                    transH.OeTransDs.Add(new OeTransD()
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
                        Kode = "94",
                        NoLpb = transH.NoLpb,
                        Tanggal = trans.Tanggal,
                        HrgCost = item.HrgCost,
                        Cost = item.Cost,
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
                                Qty = -1* item.Qty
                            };
                            _contextIc.IcAltItems.Add(Produk);

                        }
                        else
                        {
                            cekLokasi1.Qty -= item.Qty;
                            _contextIc.IcAltItems.Update(cekLokasi1);
                        }

                        #endregion altitem
                        if (item.Harga != 0)
                            cekItem.HrgJual = item.Harga;  // harga jual barang

                        if (cekItem.JnsBrng == (int)jnsBrng.Stock)   // jika stock
                        {
                            cekItem.Qty -= item.Qty;
                        }

                        if (cekItem.CostMethod == (int)costMethod.Moving_Avg)  // jika moving avarage
                        {

                            cekItem.Cost -= item.Cost;
                        }

                        //if (cekItem.Qty != 0)
                        //{
                        //    cekItem.HrgNetto = cekItem.Cost / cekItem.Qty;
                        //}
                        //else
                        //{
                        //    cekItem.HrgNetto = cekItem.Harga;
                        //}

                        _contextIc.IcItems.Update(cekItem);

                    }
                }
                _context.OeTransHs.Add(transH);
            }
            var Customer = GetCustomerId(transH.Customer);

            ArPiutng piutang = new ArPiutng
            {
                Kode = "OE",
                Dokumen = transH.NoLpb,
                Tanggal = transH.Tanggal,
                DueDate = transH.Tanggal.AddDays(Customer.Termin),
                Customer = transH.Customer,
                Keterangan = transH.Keterangan,
                Jumlah = transH.Jumlah,
                Sisa = transH.Jumlah,
                SldSisa = transH.Jumlah,
                KodeTran = transH.Kode
            };
            _contextAr.ArPiutngs.Add(piutang);

          
            Customer.Piutang += transH.Jumlah;

            _contextAr.ArCusts.Update(Customer);

            _context.SaveChanges();
            _contextAr.SaveChanges();
            _contextIc.SaveChanges();

            var TempTrans = GetTransDoc(transH.NoLpb);

            return TempTrans;

        }

        public OeTransH GetTransDoc(string docno)
        {
            return _context.OeTransHs.Include(p => p.OeTransDs).Where(x => x.NoLpb == docno).FirstOrDefault();
        }

        public async Task<bool> DelTransH(int id)
        {
            try
            {
                var ExistingTrans = _context.OeTransHs.Where(x => x.OeTransHId == id).FirstOrDefault();

                if (ExistingTrans != null)
                {
                    foreach (var item in ExistingTrans.OeTransDs)
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
                                        Qty = item.Qty
                                    };
                                    _contextIc.IcAltItems.Add(Produk);

                                }
                                else
                                {
                                    cekLokasi1.Qty += item.Qty;
                                    _contextIc.IcAltItems.Update(cekLokasi1);
                                }
                                //   cekItem.Qty -= item.Qty;
                                //   cekItem.Cost -= item.JumDpp;
                                if (cekItem.JnsBrng == (int)jnsBrng.Stock)   // jika stock
                                {
                                    cekItem.Qty += item.Qty;
                                }

                                if (cekItem.CostMethod == (int)costMethod.Moving_Avg)  // jika moving avarage
                                {

                                    cekItem.Cost += item.Cost;
                                }
                                //if (cekItem.Qty != 0)
                                //{
                                //    cekItem.HrgNetto = cekItem.Cost / cekItem.Qty;
                                //}
                                //else
                                //{
                                //    cekItem.HrgNetto = cekItem.Harga;
                                //}

                                _contextIc.IcItems.Update(cekItem);

                            }
                        }

                    }
                    var Customer = GetCustomerId(ExistingTrans.Customer);
                    var piutang = GetPiutang(ExistingTrans.NoLpb);

                    Customer.Piutang -= ExistingTrans.Jumlah;

                    _contextAr.ArCusts.Update(Customer);
                    _contextAr.ArPiutngs.Remove(piutang);
                    _context.OeTransHs.Remove(ExistingTrans);
                    await _context.SaveChangesAsync();
                    await _contextAr.SaveChangesAsync();
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

        public async Task<bool> EditTransH(OeTransHView trans)
        {
            decimal mQty5 = 0;

            var cekFirst = _contextAr.ArPiutngs.Where(x => x.Dokumen == trans.NoLpb && x.Bayar == 0).FirstOrDefault();

            if (cekFirst != null)
            {
                try
                {

                    var ExistingTrans = _context.OeTransHs.Where(x => x.OeTransHId == trans.OeTransHId).FirstOrDefault();

                    if (ExistingTrans != null)
                    {
                        foreach (var item in ExistingTrans.OeTransDs)
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
                                            Qty =  item.Qty
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

                                    if (cekItem.CostMethod == (int)costMethod.Moving_Avg)  // jika moving avarage
                                    {

                                        cekItem.Cost += item.Cost;
                                    }

                                    //if (cekItem.Qty != 0)
                                    //{
                                    //    cekItem.HrgNetto = cekItem.Cost / cekItem.Qty;
                                    //}
                                    //else
                                    //{
                                    //    cekItem.HrgNetto = cekItem.Harga;
                                    //}

                                    _contextIc.IcItems.Update(cekItem);

                                }
                            }

                        }

                        var existingCustomer = GetCustomerId(ExistingTrans.Customer);
                        existingCustomer.Piutang -= ExistingTrans.Jumlah;

                        _contextAr.ArCusts.Update(existingCustomer);
                        _contextAr.ArPiutngs.Remove(cekFirst);
                        _context.OeTransHs.Remove(ExistingTrans);

                        /* update nya */
                        OeTransH transH = new OeTransH
                        {
                            NoLpb = trans.NoLpb,
                            Customer = trans.Customer.ToUpper(),
                            NamaCust = GetCustomerId(trans.Customer.ToUpper()).NamaLengkap,
                            Tanggal = trans.Tanggal,
                            Keterangan = trans.Keterangan,
                            AlamatKirim = trans.AlamatKirim,
                            Jumlah = trans.Jumlah,
                            Ongkos = trans.Ongkos,
                            Ppn = trans.Ppn,
                            PpnPersen = trans.PpnPersen,
                            TtlJumlah = trans.TtlJumlah,
                            DPayment = trans.DPayment,
                            Tagihan = trans.Tagihan,
                            TotalQty = trans.TotalQty,
                            Kode = "94",
                            Cek = "1",
                            Pajak = trans.Pajak,

                            OeTransDs = new List<OeTransD>()
                        };

                        foreach (var item in trans.OeTransDs)
                        {
                            if (item.Qty != 0)
                            {
                                if (transH.TotalQty != 0)
                                {
                                    mQty5 = (item.Jumlah - item.Discount) - (item.Qty / transH.TotalQty * transH.Ppn) + (item.Qty / transH.TotalQty * transH.Ongkos);
                                }

                                transH.OeTransDs.Add(new OeTransD()
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
                                    Kode = "94",
                                    NoLpb = transH.NoLpb,
                                    Tanggal = trans.Tanggal,
                                    HrgCost = item.HrgCost,
                                    Cost = item.Cost,
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
                                            Qty = -1*item.Qty
                                        };
                                        _contextIc.IcAltItems.Add(Produk);

                                    }
                                    else
                                    {
                                        cekLokasi1.Qty -= item.Qty;
                                        _contextIc.IcAltItems.Update(cekLokasi1);
                                    }

                                    if (cekItem.JnsBrng == (int)jnsBrng.Stock)   // jika stock
                                    {
                                        cekItem.Qty -= item.Qty;
                                    }

                                    if (cekItem.CostMethod == (int)costMethod.Moving_Avg)  // jika moving avarage
                                    {

                                        cekItem.Cost -= item.Cost;
                                    }

                                    //if (cekItem.Qty != 0)
                                    //{
                                    //    cekItem.HrgNetto = cekItem.Cost / cekItem.Qty;
                                    //}
                                    //else
                                    //{
                                    //    cekItem.HrgNetto = cekItem.Harga;
                                    //}

                                    _contextIc.IcItems.Update(cekItem);

                                }
                            }

                        }
                        var Customer = GetCustomerId(transH.Customer);

                        ArPiutng hutang = new ArPiutng
                        {
                            Kode = "OE",
                            Dokumen = transH.NoLpb,
                            Tanggal = transH.Tanggal,
                            DueDate = transH.Tanggal.AddDays(Customer.Termin),
                            Customer = transH.Customer,
                            Keterangan = transH.Keterangan,
                            Jumlah = transH.Jumlah,
                            Sisa = transH.Jumlah,
                            SldSisa = transH.Jumlah,
                            KodeTran = transH.Kode
                        };


                       
                        Customer.Piutang += transH.Jumlah;

                        _context.OeTransHs.Add(transH);
                        _contextAr.ArCusts.Update(Customer);
                        _contextAr.ArPiutngs.Add(hutang);


                        await _contextAr.SaveChangesAsync();
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

        #endregion OeTransH Class

        public string GetNumber()
        {
            string kodeno = "SLS";
            string kodeurut = kodeno + '-';
            string thnbln = DateTime.Now.ToString("yyMM");
            string xbukti = kodeurut + thnbln.Substring(0, 2) + '2' + thnbln.Substring(2, 2) + '-';
            var maxvalue = "";
            var maxlist = _context.OeTransHs.Where(x => x.NoLpb.Substring(0, 10).Equals(xbukti)).ToList();
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

        public string GetNumberTax()
        {
            string kodeno = "PJL";
            string kodeurut = kodeno + '-';
            string thnbln = DateTime.Now.ToString("yyMM");
            string xbukti = kodeurut + thnbln.Substring(0, 2) + '2' + thnbln.Substring(2, 2) + '-';
            var maxvalue = "";
            var maxlist = _context.OeTransHs.Where(x => x.NoLpb.Substring(0, 10).Equals(xbukti)).ToList();
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

        #region retur jual

        public OeTransH AddTransHRetur(OeTransHView trans, bool pajak)
        {
            //string test = codeview.SrcCode.ToUpper();
            //var cekFirst = _context.CbSrcCodes.Where(x => x.SrcCode == test).ToList();
            decimal mQty5 = 0;

            OeTransH transH = new OeTransH
            {

                NoLpb = (pajak ? GetNumberTaxRetur() : GetNumberRetur()),
                Customer = trans.Customer.ToUpper(),
                NamaCust = GetCustomerId(trans.Customer.ToUpper()).NamaLengkap,

                Tanggal = trans.Tanggal,
                Keterangan = trans.Keterangan,
                AlamatKirim = trans.AlamatKirim,
                Jumlah = trans.Jumlah,
                Ongkos = trans.Ongkos,
                Ppn = trans.Ppn,
                PpnPersen = trans.PpnPersen,
                TtlJumlah = trans.TtlJumlah,
                DPayment = trans.DPayment,
                Tagihan = trans.Tagihan,
                TotalQty = trans.TotalQty,
                Kode = "95",
                Cek = "1",
                Pajak = pajak,
                OeTransDs = new List<OeTransD>()
            };

            foreach (var item in trans.OeTransDs)
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

                    transH.OeTransDs.Add(new OeTransD()
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
                        Kode = "95",
                        NoLpb = transH.NoLpb,
                        Tanggal = trans.Tanggal,
                        HrgCost = item.HrgCost,
                        Cost = item.Cost,
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

                            cekItem.Cost += item.Cost;
                        }

                        //if (cekItem.Qty != 0)
                        //{
                        //    cekItem.HrgNetto = cekItem.Cost / cekItem.Qty;
                        //}
                        //else
                        //{
                        //    cekItem.HrgNetto = cekItem.Harga;
                        //}

                        _contextIc.IcItems.Update(cekItem);

                    }
                }
                _context.OeTransHs.Add(transH);
            }

            ArPiutng piutang = new ArPiutng
            {
                Kode = "OE",
                Dokumen = transH.NoLpb,
                Tanggal = transH.Tanggal,
                DueDate = transH.Tanggal,
                Customer = transH.Customer,
                Keterangan = transH.Keterangan,
                Jumlah = -1 * transH.Jumlah,
                Sisa = -1 * transH.Jumlah,
                SldSisa = -1 * transH.Jumlah,
                KodeTran = transH.Kode
            };
            _contextAr.ArPiutngs.Add(piutang);

            var Customer = GetCustomerId(transH.Customer);
            Customer.Piutang -= transH.Jumlah;

            _contextAr.ArCusts.Update(Customer);

            _context.SaveChanges();
            _contextAr.SaveChanges();
            _contextIc.SaveChanges();

            var TempTrans = GetTransDoc(transH.NoLpb);

            return TempTrans;

        }

        public string GetNumberRetur()
        {
            string kodeno = "R/J";
            string kodeurut = kodeno + '-';
            string thnbln = DateTime.Now.ToString("yyMM");
            string xbukti = kodeurut + thnbln.Substring(0, 2) + '2' + thnbln.Substring(2, 2) + '-';
            var maxvalue = "";
            var maxlist = _context.OeTransHs.Where(x => x.NoLpb.Substring(0, 10).Equals(xbukti)).ToList();
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

        public string GetNumberTaxRetur()
        {
            string kodeno = "RTJ";
            string kodeurut = kodeno + '-';
            string thnbln = DateTime.Now.ToString("yyMM");
            string xbukti = kodeurut + thnbln.Substring(0, 2) + '2' + thnbln.Substring(2, 2) + '-';
            var maxvalue = "";
            var maxlist = _context.OeTransHs.Where(x => x.NoLpb.Substring(0, 10).Equals(xbukti)).ToList();
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

       
    }
}
