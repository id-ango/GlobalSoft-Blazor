using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eSoft.Hutang.Data;
using eSoft.Hutang.Model;
using eSoft.Hutang.View;

using Microsoft.EntityFrameworkCore;

namespace eSoft.Hutang.Services
{
    public class PayableServices : IPayableServices
    {
        private readonly DbContextHutang _context;

        public PayableServices(DbContextHutang context)
        {
            _context = context;
        }

        public bool CekKdSupplier(string supplier)
        {
            string test = supplier.ToUpper();
            var cekFirst = _context.ApSuppls.Where(x => x.Supplier == test).ToList();
            if (cekFirst.Count == 0)
            {
                return false;
            }
            return true;
        }

        public List<ApSuppl> GetSupplier()
        {
            return _context.ApSuppls.OrderBy(x => x.NamaSup).ToList();
        }

        public ApSuppl GetSupplierId(int id)
        {
            return _context.ApSuppls.Where(x => x.ApSupplId == id).FirstOrDefault();
        }

        public ApSuppl GetSupplierKode(string kode)
        {
            return _context.ApSuppls.Where(x => x.Supplier == kode).FirstOrDefault();
        }

        public bool AddSupplier(SupplierView suppliers)
        {
            string test = suppliers.Supplier.ToUpper();
            var cekFirst = _context.ApSuppls.Where(x => x.Supplier == test).ToList();
            if (cekFirst.Count == 0)
            {
                ApSuppl Supplier = new ApSuppl()
                {
                    Supplier = suppliers.Supplier.ToUpper(),
                    NamaSup = suppliers.NamaSup,
                    Alamat = suppliers.Alamat,
                    Kota = suppliers.Kota,
                    Telpon = suppliers.Telpon,
                    Kontak = suppliers.Kontak,
                    NamaLengkap = suppliers.NamaLengkap,
                    Kurs = suppliers.Kurs,
                    AcctSet = suppliers.AcctSet

                };
                _context.ApSuppls.Add(Supplier);
                _context.SaveChanges();
                return true;
            }
            else
            {

                return false;
            }

        }

        public async Task<bool> EditSupplier(SupplierView suppliers)
        {
            try
            {
                var ExistingSupplier = _context.ApSuppls.Where(x => x.ApSupplId == suppliers.ApSupplId).FirstOrDefault();
                if (ExistingSupplier != null)
                {
                    ExistingSupplier.NamaSup = suppliers.NamaSup;
                    ExistingSupplier.Alamat = suppliers.Alamat;
                    ExistingSupplier.Kota = suppliers.Kota;
                    ExistingSupplier.Telpon = suppliers.Telpon;
                    ExistingSupplier.Kontak = suppliers.Kontak;
                    ExistingSupplier.NamaLengkap = suppliers.NamaLengkap;
                    ExistingSupplier.Kurs = suppliers.Kurs;
                    ExistingSupplier.AcctSet = suppliers.AcctSet;

                    _context.ApSuppls.Update(ExistingSupplier);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return false;

        }

        public async Task<bool> DelSupplier(int suppliers)
        {
            try
            {
                var ExistingSupplier = _context.ApSuppls.Where(x => x.ApSupplId == suppliers).FirstOrDefault();
                if (ExistingSupplier != null)
                {
                    _context.ApSuppls.Remove(ExistingSupplier);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return false;

        }

        #region ApAcct Class

        public bool CekAcctSet(string supplier)
        {
            string test = supplier.ToUpper();
            var cekFirst = _context.ApAccts.Where(x => x.AcctSet == test).ToList();
            if (cekFirst.Count == 0)
            {
                return false;
            }
            return true;
        }

        public List<ApAcct> GetApAkunSet()
        {
            return _context.ApAccts.ToList();
        }

        public ApAcct GetApAkunSetId(int id)
        {
            return _context.ApAccts.Where(x => x.ApAcctId == id).FirstOrDefault();
        }

        public bool AddAkunSet(ApAcctView codeview)
        {
            string test = codeview.AcctSet.ToUpper();
            var cekFirst = _context.ApAccts.Where(x => x.AcctSet == test).ToList();
            if (cekFirst.Count == 0)
            {
                ApAcct AcctCode = new ApAcct()
                {
                    AcctSet = codeview.AcctSet.ToUpper(),
                    Description = codeview.Description,
                    Acct1 = codeview.Acct1,
                    Acct2 = codeview.Acct2,
                    Acct3 = codeview.Acct3,
                    Acct4 = codeview.Acct4,
                    Acct5 = codeview.Acct5,
                    Acct6 = codeview.Acct6

                };
                _context.ApAccts.Add(AcctCode);
                _context.SaveChanges();
                return true;
            }
            else
            {

                return false;
            }


        }

        public async Task<bool> EditAkunSet(ApAcctView codeview)
        {
            try
            {
                var ExistingAkunSet = _context.ApAccts.Where(x => x.ApAcctId == codeview.ApAcctId).FirstOrDefault();
                if (ExistingAkunSet != null)
                {
                    ExistingAkunSet.Description = codeview.Description;
                    ExistingAkunSet.Acct1 = codeview.Acct1;
                    ExistingAkunSet.Acct2 = codeview.Acct2;
                    ExistingAkunSet.Acct3 = codeview.Acct3;
                    ExistingAkunSet.Acct4 = codeview.Acct4;
                    ExistingAkunSet.Acct5 = codeview.Acct5;
                    ExistingAkunSet.Acct6 = codeview.Acct6;

                    _context.ApAccts.Update(ExistingAkunSet);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return false;

        }

        public async Task<bool> DelAkunSet(int codeview)
        {
            try
            {
                var ExistingAkunSet = _context.ApAccts.Where(x => x.ApAcctId == codeview).FirstOrDefault();
                if (ExistingAkunSet != null)
                {
                    _context.ApAccts.Remove(ExistingAkunSet);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return false;

        }
        #endregion ApAcct Class

        #region ApDist Class

        public bool CekDistCode(string distcode)
        {
            string test = distcode.ToUpper();
            var cekFirst = _context.ApDists.Where(x => x.DistCode == test).ToList();
            if (cekFirst.Count == 0)
            {
                return false;
            }
            return true;
        }

        public List<ApDist> GetDist()
        {
            return _context.ApDists.ToList();
        }

        public ApDist GetDistId(int id)
        {
            return _context.ApDists.Where(x => x.ApDistId == id).FirstOrDefault();
        }

        public bool AddDist(ApDistView codeview)
        {
            string test = codeview.DistCode.ToUpper();
            var cekFirst = _context.ApDists.Where(x => x.DistCode == test).ToList();
            if (cekFirst.Count == 0)
            {
                ApDist AcctCode = new ApDist()
                {
                    DistCode = codeview.DistCode.ToUpper(),
                    Description = codeview.Description,
                    Dist1 = codeview.Dist1

                };
                _context.ApDists.Add(AcctCode);
                _context.SaveChanges();
                return true;
            }
            else
            {

                return false;
            }


        }

        public async Task<bool> EditDist(ApDistView codeview)
        {
            try
            {
                var ExistingDist = _context.ApDists.Where(x => x.ApDistId == codeview.ApDistId).FirstOrDefault();
                if (ExistingDist != null)
                {
                    ExistingDist.Description = codeview.Description;
                    ExistingDist.Dist1 = codeview.Dist1;

                    _context.ApDists.Update(ExistingDist);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return false;

        }

        public async Task<bool> DelDist(int codeview)
        {
            try
            {
                var ExistingDist = _context.ApDists.Where(x => x.ApDistId == codeview).FirstOrDefault();
                if (ExistingDist != null)
                {
                    _context.ApDists.Remove(ExistingDist);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return false;

        }
        #endregion ApDist Class

        #region Transaksi Hutang Class

        public ApTransH GetTrans(int id)
        {
            return _context.ApTransHs.Include(p => p.ApTransDs).Where(x => x.ApTransHId == id).FirstOrDefault();
        }

        public ApHutang GetHutang(string bukti)
        {
            return _context.ApHutangs.Where(x => x.Dokumen == bukti).FirstOrDefault();

        }
        public List<ApTransH> GetTransH()
        {
            List<ApTransH> ApTrans = new List<ApTransH>();
            try
            {
                ApTrans = _context.ApTransHs.OrderByDescending(x => x.Tanggal).Where(x => x.Kode == "21").ToList();
                foreach (var item in ApTrans)
                {
                    item.NamaSup = (from e in _context.ApSuppls where e.ApSupplId == item.ApSupplId select e.NamaSup).FirstOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ApTrans;
            // return  _context.CbTransHs.Include(p =>p.CbTransDs).OrderByDescending(x =>x.Tanggal).ToListAsync();
            //  return await _context.ApTransHs.OrderByDescending(x => x.Tanggal).ToListAsync();
            //  return await _context.ApTransHs.ToListAsync();

        }

        public List<ApTransH> Get3TransH()
        {
            List<ApTransH> ApTrans = new List<ApTransH>();

            ApTrans = _context.ApTransHs.OrderByDescending(x => x.Tanggal).Where(x => x.Tanggal > DateTime.Today.AddMonths(-3) && x.Kode == "21").ToList();
            foreach (var item in ApTrans)
            {
                item.NamaSup = (from e in _context.ApSuppls where e.ApSupplId == item.ApSupplId select e.NamaSup).FirstOrDefault();
            }

            return ApTrans;

            // return  _context.CbTransHs.Include(p =>p.CbTransDs).OrderByDescending(x =>x.Tanggal).ToListAsync();
            //   return _context.ApTransHs.OrderByDescending(x => x.Tanggal).Where(x => x.Tanggal > DateTime.Today.AddMonths(-3)).ToListAsync();

        }

        public List<ApTransD> GetTransD()
        {
            return  _context.ApTransDs.ToList();
        }

        public ApTransH AddTransH(ApTransHView trans)
        {
            //string test = codeview.SrcCode.ToUpper();
            //var cekFirst = _context.CbSrcCodes.Where(x => x.SrcCode == test).ToList();

            ApTransH transH = new ApTransH
            {
                Bukti = GetNumber(),
                Supplier = trans.Supplier.ToUpper(),
                Tanggal = trans.Tanggal,
                Keterangan = trans.Keterangan,
                Jumlah = trans.Jumlah,
                PPn = 0,
                PPh = 0,
                JumPPh = 0,
                JumPPn = 0,
                Bruto = trans.Jumlah,
                Netto = 0,
                Discount = 0,
                Hutang = 0,
                Pajak = false,
                Unapplied = 0,
                Kode = "21",
                ApSupplId = trans.ApSupplId,

                ApTransDs = new List<ApTransD>()
            };
            foreach (var item in trans.ApTransDs)
            {
                transH.ApTransDs.Add(new ApTransD()
                {
                    DistCode = item.DistCode,
                    Keterangan = item.Keterangan,
                    Jumlah = item.Jumlah,
                    Kode = "21",
                    KodeTran = "21",
                    Lpb = transH.Bukti,
                    Sisa = item.Jumlah,
                    Discount = 0,
                    Bayar = 0,
                    Tanggal = trans.Tanggal
                });
            }
            ApHutang transaksi = new ApHutang
            {
                Kode = "IN",
                Dokumen = transH.Bukti,
                Tanggal = transH.Tanggal,
                Supplier = transH.Supplier,
                Keterangan = transH.Keterangan,
                KodeTran = "21",
                Jumlah = transH.Jumlah,
                Bayar = 0,
                Discount = 0,
                UnApplied = 0,
                Sisa = transH.Jumlah,
                SldSisa = transH.Jumlah,
                Dpp = transH.Jumlah,
                PPn = 0,
                PPh = 0,
                SldBayar = 0,
                SldDisc = 0,
                SldUnpl = 0
            };

            var Supplier = (from e in _context.ApSuppls where e.Supplier == trans.Supplier select e).FirstOrDefault();
            Supplier.Hutang += trans.Jumlah;

            _context.ApSuppls.Update(Supplier);
            _context.ApTransHs.Add(transH);
            _context.ApHutangs.Add(transaksi);
            _context.SaveChanges();
            var TempTrans = GetTransDoc(transH.Bukti);

            return TempTrans;


        }

        public ApTransH EditTransH(ApTransHView trans)
        {
            //string test = codeview.SrcCode.ToUpper();
            //var cekFirst = _context.CbSrcCodes.Where(x => x.SrcCode == test).ToList();
            var cekFirst = _context.ApHutangs.Where(x => x.Dokumen == trans.Bukti).FirstOrDefault();


            ApTransH transH = new ApTransH
            {

                Supplier = trans.Supplier.ToUpper(),
                Tanggal = trans.Tanggal,
                Keterangan = trans.Keterangan,
                Jumlah = trans.Jumlah,
                PPn = 0,
                PPh = 0,
                JumPPh = 0,
                JumPPn = 0,
                Bruto = trans.Jumlah,
                Netto = 0,
                Discount = 0,
                Hutang = 0,
                Pajak = false,
                Unapplied = 0,
                Kode = "21",
                ApSupplId = trans.ApSupplId,

                ApTransDs = new List<ApTransD>()
            };
            foreach (var item in trans.ApTransDs)
            {
                transH.ApTransDs.Add(new ApTransD()
                {
                    DistCode = item.DistCode,
                    Keterangan = item.Keterangan,
                    Jumlah = item.Jumlah,
                    Kode = "21",
                    KodeTran = "21",
                    Lpb = transH.Bukti,
                    Sisa = item.Jumlah,
                    Discount = 0,
                    Bayar = 0,
                    Tanggal = trans.Tanggal
                });
            }

            ApHutang transaksi = new ApHutang
            {
                Kode = "IN",
                Tanggal = transH.Tanggal,
                Supplier = transH.Supplier,
                Keterangan = transH.Keterangan,
                KodeTran = "21",
                Jumlah = transH.Jumlah,
                Bayar = 0,
                Discount = 0,
                UnApplied = 0,
                Sisa = transH.Jumlah,
                SldSisa = transH.Jumlah,
                Dpp = transH.Jumlah,
                PPn = 0,
                PPh = 0,
                SldBayar = 0,
                SldDisc = 0,
                SldUnpl = 0
            };

            
                var ExistingTrans = _context.ApTransHs.Where(x => x.ApTransHId == trans.ApTransHId).FirstOrDefault();
                if (ExistingTrans != null)
                {
                    transH.Bukti = ExistingTrans.Bukti;
                    transaksi.Dokumen = ExistingTrans.Bukti;

                    _context.ApTransHs.Remove(ExistingTrans);

                    var Supplier = (from e in _context.ApSuppls where e.Supplier == trans.Supplier select e).FirstOrDefault();

                    Supplier.Hutang -= ExistingTrans.Jumlah;
                    Supplier.Hutang += trans.Jumlah;

                    _context.ApSuppls.Update(Supplier);
                    _context.ApHutangs.Remove(cekFirst);

                    _context.ApTransHs.Add(transH);
                    _context.ApHutangs.Add(transaksi);
                    _context.SaveChanges();

                    var TempTrans = GetTransDoc(transH.Bukti);

                    return TempTrans;
            }
            else
            {
                return ExistingTrans;
            }



        }

        public async Task<bool> DelTransH(int id)
        {
            try
            {
                var ExistingTrans = _context.ApTransHs.Where(x => x.ApTransHId == id).FirstOrDefault();
                if (ExistingTrans != null)
                {
                    var cekFirst = _context.ApHutangs.Where(x => x.Dokumen == ExistingTrans.Bukti).FirstOrDefault();
                    var Supplier = (from e in _context.ApSuppls where e.Supplier == ExistingTrans.Supplier select e).FirstOrDefault();

                    Supplier.Hutang -= ExistingTrans.Jumlah;


                    _context.ApSuppls.Update(Supplier);
                    _context.ApTransHs.Remove(ExistingTrans);
                    _context.ApHutangs.Remove(cekFirst);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return false;

        }
        public bool CekAlreadyPayment(string dokumen)
        {
            var cekFirst = _context.ApHutangs.Where(x => x.Dokumen == dokumen).FirstOrDefault();

            if (cekFirst.SldSisa != cekFirst.Sisa)
            {
                return true;
            }
            return false;
        }

        #endregion Transaksi Hutang Class

        public ApTransH GetTransDoc(string docno)
        {
            return _context.ApTransHs.Include(p => p.ApTransDs).Where(x => x.Bukti == docno).FirstOrDefault();
        }

        public string GetNumber()
        {
            string kodeno = "API";
            string kodeurut = kodeno + '-';
            string thnbln = DateTime.Now.ToString("yyMM");
            string xbukti = kodeurut + thnbln.Substring(0, 2) + '2' + thnbln.Substring(2, 2) + '-';
            var maxvalue = "";
            var maxlist = _context.ApTransHs.Where(x => x.Bukti.Substring(0, 10).Equals(xbukti)).ToList();
            if (maxlist != null)
            {
                maxvalue = maxlist.Max(x => x.Bukti);

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
