using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eSoft.Piutang.Data;
using eSoft.Piutang.Model;
using eSoft.Piutang.View;
using Microsoft.EntityFrameworkCore;

namespace eSoft.Piutang.Services
{
    public class ReceivableServices : IReceivableServices
    {
        private readonly DbContextPiutang _context;

        public ReceivableServices(DbContextPiutang context)
        {
            _context = context;
        }

        public bool CekKdCustomer(string customer)
        {
            string test = customer.ToUpper();
            var cekFirst = _context.ArCusts.Where(x => x.Customer == test).ToList();
            if (cekFirst.Count == 0)
            {
                return false;
            }
            return true;
        }


        public List<ArCust> GetCustomer()
        {
            return _context.ArCusts.OrderBy(x => x.NamaCust).ToList();
        }

        public ArCust GetCustomerId(int id)
        {
            return _context.ArCusts.Where(x => x.ArCustId == id).FirstOrDefault();
        }

        public ArCust GetCustomerCode(string xKode)
        {
            return _context.ArCusts.Where(x => x.Customer == xKode).FirstOrDefault();
        }

        public bool AddCustomer(CustomerView customers)
        {
            string test = customers.Customer.ToUpper();
            var cekFirst = _context.ArCusts.Where(x => x.Customer == test).ToList();
            if (cekFirst.Count == 0)
            {
                ArCust Customer = new ArCust()
                {
                    Customer = customers.Customer.ToUpper(),
                    NamaCust = customers.NamaCust,
                    Termin = customers.Termin,
                    Alamat = customers.Alamat,
                    Kota = customers.Kota,
                    Telpon = customers.Telpon,
                    NamaLengkap = customers.NamaLengkap,
                    AcctSet = customers.AcctSet,
                    //AcctPjk = customers.AcctPjk,
                    AlmtKrm = customers.AlmtKrm,
                    KotaKrm = customers.KotaKrm,
                    //ProvKirim = customers.ProvKirim,
                    Kontak = customers.Kontak



                };
                _context.ArCusts.Add(Customer);
                _context.SaveChanges();
                return true;
            }
            else
            {

                return false;
            }

        }

        public async Task<bool> EditCustomer(CustomerView customers)
        {
            try
            {
                var ExistingCustomer = _context.ArCusts.Where(x => x.ArCustId == customers.ArCustId).FirstOrDefault();
                if (ExistingCustomer != null)
                {
                    ExistingCustomer.NamaCust = customers.NamaCust;
                    ExistingCustomer.Alamat = customers.Alamat;
                    ExistingCustomer.Kota = customers.Kota;
                    ExistingCustomer.Telpon = customers.Telpon;
                    ExistingCustomer.Termin = customers.Termin;
                    ExistingCustomer.NamaLengkap = customers.NamaLengkap;
                    ExistingCustomer.AcctSet = customers.AcctSet;
                    ExistingCustomer.AcctPjk = customers.AcctPjk;
                    ExistingCustomer.AlmtKrm = customers.AlmtKrm;
                    ExistingCustomer.KotaKrm = customers.KotaKrm;
                    ExistingCustomer.ProvKirim = customers.ProvKirim;
                    ExistingCustomer.Kontak = customers.Kontak;

                    _context.ArCusts.Update(ExistingCustomer);
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

        public async Task<bool> DelCustomer(int customers)
        {
            try
            {
                var ExistingCustomer = _context.ArCusts.Where(x => x.ArCustId == customers).FirstOrDefault();
                if (ExistingCustomer != null)
                {
                    _context.ArCusts.Remove(ExistingCustomer);
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

        #region ArAcct Class

        public bool CekAcctSet(string customer)
        {
            string test = customer.ToUpper();
            var cekFirst = _context.ArAccts.Where(x => x.AcctSet == test).ToList();
            if (cekFirst.Count == 0)
            {
                return false;
            }
            return true;
        }


        public List<ArAcct> GetArAkunSet()
        {
            return _context.ArAccts.ToList();
        }

        public ArAcct GetArAkunSetId(int id)
        {
            return _context.ArAccts.Where(x => x.ArAcctId == id).FirstOrDefault();
        }

        public bool AddAkunSet(ArAcctView codeview)
        {
            string test = codeview.AcctSet.ToUpper();
            var cekFirst = _context.ArAccts.Where(x => x.AcctSet == test).ToList();
            if (cekFirst.Count == 0)
            {
                ArAcct AcctCode = new ArAcct()
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
                _context.ArAccts.Add(AcctCode);
                _context.SaveChanges();
                return true;
            }
            else
            {

                return false;
            }


        }

        public async Task<bool> EditAkunSet(ArAcctView codeview)
        {
            try
            {
                var ExistingAkunSet = _context.ArAccts.Where(x => x.ArAcctId == codeview.ArAcctId).FirstOrDefault();
                if (ExistingAkunSet != null)
                {
                    ExistingAkunSet.Description = codeview.Description;
                    ExistingAkunSet.Acct1 = codeview.Acct1;
                    ExistingAkunSet.Acct2 = codeview.Acct2;
                    ExistingAkunSet.Acct3 = codeview.Acct3;
                    ExistingAkunSet.Acct4 = codeview.Acct4;
                    ExistingAkunSet.Acct5 = codeview.Acct5;
                    ExistingAkunSet.Acct6 = codeview.Acct6;
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
                var ExistingAkunSet = _context.ArAccts.Where(x => x.ArAcctId == codeview).FirstOrDefault();
                if (ExistingAkunSet != null)
                {
                    _context.ArAccts.Remove(ExistingAkunSet);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception) { throw; }

            return false;

        }
        #endregion ArAcct Class

        #region ArDist Class

        public bool CekDistCode(string distcode)
        {
            string test = distcode.ToUpper();
            var cekFirst = _context.ArDists.Where(x => x.DistCode == test).ToList();
            if (cekFirst.Count == 0)
            {
                return false;
            }
            return true;
        }


        public List<ArDist> GetDist()
        {
            return _context.ArDists.ToList();
        }

        public ArDist GetDistId(int id)
        {
            return _context.ArDists.Where(x => x.ArDistId == id).FirstOrDefault();
        }

        public bool AddDist(ArDistView codeview)
        {
            string test = codeview.DistCode.ToUpper();
            var cekFirst = _context.ArDists.Where(x => x.DistCode == test).ToList();
            if (cekFirst.Count == 0)
            {
                ArDist AcctCode = new ArDist()
                {
                    DistCode = codeview.DistCode.ToUpper(),
                    Description = codeview.Description,
                    Dist1 = codeview.Dist1

                };
                _context.ArDists.Add(AcctCode);
                _context.SaveChanges();
                return true;
            }
            else
            {

                return false;
            }


        }

        public async Task<bool> EditDist(ArDistView codeview)
        {
            try
            {
                var ExistingDist = _context.ArDists.Where(x => x.ArDistId == codeview.ArDistId).FirstOrDefault();
                if (ExistingDist != null)
                {
                    ExistingDist.Description = codeview.Description;
                    ExistingDist.Dist1 = codeview.Dist1;
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception) { throw; }

            return false;

        }

        public async Task<bool> DelDist(int codeview)
        {
            try
            {
                var ExistingDist = _context.ArDists.Where(x => x.ArDistId == codeview).FirstOrDefault();
                if (ExistingDist != null)
                {
                    _context.ArDists.Remove(ExistingDist);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception) { throw; }

            return false;

        }
        #endregion ArDist Class



        #region Transaksi Piutang Class

        public ArTransH GetTrans(int id)
        {
            return _context.ArTransHs.Include(p => p.ArTransDs).Where(x => x.ArTransHId == id).FirstOrDefault();
        }

        public ArPiutng GetPiutang(string bukti)
        {
            return _context.ArPiutngs.Where(x => x.Dokumen == bukti).FirstOrDefault();

        }
        public List<ArTransH> GetTransH()
        {
            List<ArTransH> arTrans = new List<ArTransH>();
            try
            {
                arTrans = _context.ArTransHs.OrderByDescending(x => x.Tanggal).Where(x => x.Kode == "11").ToList();
                foreach (var item in arTrans)
                {
                    item.NamaCust = (from e in _context.ArCusts where e.ArCustId == item.ArCustId select e.NamaCust).FirstOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return arTrans;
            // return  _context.CbTransHs.Include(p =>p.CbTransDs).OrderByDescending(x =>x.Tanggal).ToListAsync();
            //  return await _context.ArTransHs.OrderByDescending(x => x.Tanggal).ToListAsync();
            //  return await _context.ArTransHs.ToListAsync();

        }

        public List<ArTransH> Get3TransH()
        {
            List<ArTransH> arTrans = new List<ArTransH>();

            //  arTrans = _context.ArTransHs.OrderByDescending(x => x.Tanggal).Where(x => x.Tanggal > DateTime.Today.AddMonths(-3) && x.Kode == "11").ToList();
            arTrans = _context.ArTransHs.OrderByDescending(x => x.Tanggal).Where(x => x.Tanggal > DateTime.Today.AddMonths(-3) && x.Kode == "11")
                .Select(x => new ArTransH
                {
                    ArTransHId = x.ArTransHId,
                    Bukti = x.Bukti,
                    Tanggal = x.Tanggal,
                    Keterangan = x.Keterangan,
                    Customer = x.Customer,
                    NamaCust = (from e in _context.ArCusts where e.Customer == x.Customer select e.NamaCust).SingleOrDefault(),
                    Jumlah = x.Jumlah,
                })
                .ToList();
            //foreach (var item in arTrans)
            //{
            //    item.NamaCust = (from e in _context.ArCusts where e.ArCustId == item.ArCustId select e.NamaCust).FirstOrDefault();
            //}

            return arTrans;

            // return  _context.CbTransHs.Include(p =>p.CbTransDs).OrderByDescending(x =>x.Tanggal).ToListAsync();
            //   return _context.ArTransHs.OrderByDescending(x => x.Tanggal).Where(x => x.Tanggal > DateTime.Today.AddMonths(-3)).ToListAsync();

        }

        public List<ArTransD> GetTransD()
        {
            return _context.ArTransDs.ToList();
        }

        public ArTransH AddTransH(ArTransHView trans)
        {
            //string test = codeview.SrcCode.ToUpper();
            //var cekFirst = _context.CbSrcCodes.Where(x => x.SrcCode == test).ToList();

            ArTransH transH = new ArTransH
            {
                Bukti = GetNumber(),
                Customer = trans.Customer.ToUpper(),
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
                Piutang = 0,
                Pajak = false,
                Unapplied = 0,
                Kode = "11",
                ArCustId = trans.ArCustId,

                ArTransDs = new List<ArTransD>()
            };
            foreach (var item in trans.ArTransDs)
            {
                transH.ArTransDs.Add(new ArTransD()
                {
                    DistCode = item.DistCode,
                    Keterangan = item.Keterangan,
                    Jumlah = item.Jumlah,
                    Kode = "11",
                    KodeTran = "11",
                    Bukti = transH.Bukti,
                    Sisa = item.Jumlah,
                    Discount = 0,
                    Bayar = 0,
                    Tanggal = trans.Tanggal
                });
            }
            ArPiutng transaksi = new ArPiutng
            {
                Kode = "IN",
                Dokumen = transH.Bukti,
                Tanggal = transH.Tanggal,
                Customer = transH.Customer,
                Keterangan = transH.Keterangan,
                KodeTran = "11",
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

            var customer = (from e in _context.ArCusts where e.Customer == trans.Customer select e).FirstOrDefault();
            customer.Piutang += trans.Jumlah;

            _context.ArCusts.Update(customer);
            _context.ArTransHs.Add(transH);
            _context.ArPiutngs.Add(transaksi);
            _context.SaveChanges();

            var TempTrans = GetTransDoc(transH.Bukti);

            return TempTrans;


        }

        public ArTransH EditTransH(ArTransHView trans)
        {
            //string test = codeview.SrcCode.ToUpper();
            //var cekFirst = _context.CbSrcCodes.Where(x => x.SrcCode == test).ToList();
            var cekFirst = _context.ArPiutngs.Where(x => x.Dokumen == trans.Bukti).FirstOrDefault();


            ArTransH transH = new ArTransH
            {

                Customer = trans.Customer.ToUpper(),
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
                Piutang = 0,
                Pajak = false,
                Unapplied = 0,
                Kode = "11",
                ArCustId = trans.ArCustId,

                ArTransDs = new List<ArTransD>()
            };
            foreach (var item in trans.ArTransDs)
            {
                transH.ArTransDs.Add(new ArTransD()
                {
                    DistCode = item.DistCode,
                    Keterangan = item.Keterangan,
                    Jumlah = item.Jumlah,
                    Kode = "11",
                    KodeTran = "11",
                    Bukti = transH.Bukti,
                    Sisa = item.Jumlah,
                    Discount = 0,
                    Bayar = 0,
                    Tanggal = trans.Tanggal
                });
            }

            ArPiutng transaksi = new ArPiutng
            {
                Kode = "IN",
                Tanggal = transH.Tanggal,
                Customer = transH.Customer,
                Keterangan = transH.Keterangan,
                KodeTran = "11",
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


            var ExistingTrans = _context.ArTransHs.Where(x => x.ArTransHId == trans.ArTransHId).FirstOrDefault();
            if (ExistingTrans != null)
            {
                transH.Bukti = ExistingTrans.Bukti;
                transaksi.Dokumen = ExistingTrans.Bukti;

                _context.ArTransHs.Remove(ExistingTrans);

                var customer = (from e in _context.ArCusts where e.Customer == trans.Customer select e).FirstOrDefault();

                customer.Piutang -= ExistingTrans.Jumlah;
                customer.Piutang += trans.Jumlah;

                _context.ArCusts.Update(customer);
                _context.ArPiutngs.Remove(cekFirst);

                _context.ArTransHs.Add(transH);
                _context.ArPiutngs.Add(transaksi);
                 _context.SaveChanges();

                var TempTrans = GetTransDoc(transH.Bukti);

                return TempTrans;

            }
            else
            {
                return ExistingTrans;
            }


            // return false;


        }
        public bool CekJual(string noLpb)
        {
            var cekFirst = _context.ArPiutngs.Where(x => x.Dokumen == noLpb && x.Bayar == 0).FirstOrDefault();

            if (cekFirst != null)
                return true;

            return false;
        }
        public bool CekAlreadyPayment(string dokumen)
        {
            var cekFirst = _context.ArPiutngs.Where(x => x.Dokumen == dokumen).FirstOrDefault();

            if (cekFirst.SldSisa != cekFirst.Sisa)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DelTransH(int id)
        {
            try
            {
                var ExistingTrans = _context.ArTransHs.Where(x => x.ArTransHId == id).FirstOrDefault();
                if (ExistingTrans != null)
                {
                    var cekFirst = _context.ArPiutngs.Where(x => x.Dokumen == ExistingTrans.Bukti).FirstOrDefault();
                    var customer = (from e in _context.ArCusts where e.Customer == ExistingTrans.Customer select e).FirstOrDefault();

                    customer.Piutang -= ExistingTrans.Jumlah;
                    

                    _context.ArCusts.Update(customer);
                    _context.ArTransHs.Remove(ExistingTrans);
                    _context.ArPiutngs.Remove(cekFirst);
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

        public ArTransH GetTransDoc(string docno)
        {
            return _context.ArTransHs.Include(p => p.ArTransDs).Where(x => x.Bukti == docno).FirstOrDefault();
        }

        #endregion Transaksi Piutang Class

        #region laporan piutang

        public List<ArPiutng> Detail1(string xKdHeader)
        {
            
            List<ArPiutng> trans = new List<ArPiutng>();

            trans = _context.ArPiutngs.Where(x => x.Customer == xKdHeader && (x.Sisa != 0)).ToList();
  
            return trans;
        }

        public List<ArPiutngView> GetUangMuka()
        {
            List<ArPiutngView> transView = new List<ArPiutngView>(); 
            var trans = _context.ArPiutngs.Where(x => x.Kode == "CA" && x.KodeTran=="13" && x.Sisa != 0).ToList();
            var customer = GetCustomer();


            if(trans != null && customer != null)
            {
                transView = (from header in trans
                         join detail in customer on header.Customer equals detail.Customer
                         select new ArPiutngView()
                         {
                             Sisa = header.Sisa,
                             Dokumen = header.Dokumen,
                             Tanggal = header.Tanggal,
                             Customer = header.Customer,
                             NamaCust = detail.NamaCust,     
                             KdBank = GetTransDoc(header.Dokumen).KdBank
                         }).ToList();
            }

            return transView;
        }
        #endregion

        public string GetNumber()
        {
            string kodeno = "ARI";
            string kodeurut = kodeno + '-';
            string thnbln = DateTime.Now.ToString("yyMM");
            string xbukti = kodeurut + thnbln.Substring(0, 2) + '2' + thnbln.Substring(2, 2) + '-';
            var maxvalue = "";
            var maxlist = _context.ArTransHs.Where(x => x.Bukti.Substring(0, 10).Equals(xbukti)).ToList();
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
