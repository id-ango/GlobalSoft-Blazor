using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eSoft.CashBank.Data;
using eSoft.CashBank.Model;
using eSoft.CashBank.View;
using Microsoft.EntityFrameworkCore;

namespace eSoft.CashBank.Services
{
    public class CashBankServices : ICashBankServices
    {
        private readonly DbContextBank _context;

        public CashBankServices(DbContextBank context)
        {
            _context = context;
        }

        #region Bank Class

        public List<CbBank> GetBank()
        {
            return _context.Banks.ToList();
        }

        public CbBank GetBankId(int id)
        {
            return _context.Banks.Where(x => x.CbBankId == id).FirstOrDefault();
        }
        public CbBank GetBankKd(string id)
        {
            return _context.Banks.Where(x => x.KodeBank == id).FirstOrDefault();
        }

        public bool CekKdBank(string kodeBank)
        {
            string test = kodeBank.ToUpper();
            var cekFirst = _context.Banks.Where(x => x.KodeBank == test).ToList();
            if (cekFirst.Count == 0)
            {
                return false;
            }
            return true;
        }

        public bool AddBank(BankView banks)
        {
            string test = banks.Kdbank.ToUpper();
            var cekFirst = _context.Banks.Where(x => x.KodeBank == test).ToList();
            if (cekFirst.Count == 0)
            {
                CbBank Bank = new ()
                {
                    KodeBank = banks.Kdbank.ToUpper(),
                    NmBank = banks.Namabank,
                    Kurs = banks.Kurs,
                    Acctset = banks.Acctset,
                    ClrDate = banks.ClrDate,
                    SldAwal = banks.SldAwal,
                    KSldAwal = banks.KSldAwal,
                    Saldo = banks.SldAwal,
                    KSaldo = banks.KSldAwal,
                    Status = banks.Status,
                    Pajak = banks.Pajak

                };
                _context.Banks.Add(Bank);
                 _context.SaveChanges();
                return true;
            }
            else
            {

                return false;
            }

        }

        public async Task<bool> EditBank(BankView banks)
        {
            try
            {
                var ExistingBank = _context.Banks.Where(x => x.CbBankId == banks.BankId).FirstOrDefault();
                if (ExistingBank != null)
                {
                    ExistingBank.NmBank = banks.Namabank;
                    ExistingBank.Kurs = banks.Kurs;
                    ExistingBank.Acctset = banks.Acctset;
                    ExistingBank.ClrDate = banks.ClrDate;
                    ExistingBank.SldAwal = banks.SldAwal;
                    ExistingBank.KSldAwal = banks.KSldAwal;
                    ExistingBank.Saldo += banks.SldAwal;
                    ExistingBank.KSaldo += banks.KSldAwal;
                    ExistingBank.Pajak = banks.Pajak;
                    ExistingBank.Kurs = banks.Kurs;

                    _context.Banks.Update(ExistingBank);
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

        public async Task<bool> DelBank(int banks)
        {
            try
            {
                var ExistingBank = _context.Banks.Single(item => item.CbBankId == banks);
              //  var ExistingBank = _context.Banks.Where(x => x.CbBankId == banks).FirstOrDefault();
                if (ExistingBank != null && ExistingBank.Saldo == 0)
                {
                    _context.Banks.Remove(ExistingBank);
                    await _context.SaveChangesAsync();
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
        #endregion Bank Class

        #region SrcCode Class

        public List<CbSrcCode> GetSrcCode()
        {
            return _context.CbSrcCodes.ToList();
        }

        public CbSrcCode GetSrcCodeId(int id)
        {
            return _context.CbSrcCodes.Where(x => x.CbSrcCodeId == id).FirstOrDefault();
        }

        public async Task<bool> AddSrcCode(SrcCodeView codeview)
        {
            string test = codeview.SrcCode.ToUpper();
            var cekFirst = _context.CbSrcCodes.Where(x => x.SrcCode == test).ToList();
            if (cekFirst.Count == 0)
            {
                CbSrcCode BankCode = new()
                {
                    SrcCode = codeview.SrcCode.ToUpper(),
                    NamaSrc = codeview.NamaSrc,
                    GlAcct = codeview.GlAcct

                };
                _context.CbSrcCodes.Add(BankCode);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {

                return false;
            }


        }

        public async Task<bool> EditSrcCode(SrcCodeView codeview)
        {
            try
            {
                var ExistingSrcCode = _context.CbSrcCodes.Where(x => x.CbSrcCodeId == codeview.SrcCodeId).FirstOrDefault();
                if (ExistingSrcCode != null)
                {
                    ExistingSrcCode.NamaSrc = codeview.NamaSrc;
                    ExistingSrcCode.GlAcct = codeview.GlAcct;

                    _context.CbSrcCodes.Update(ExistingSrcCode);
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

        public async Task<bool> DelSrcCode(int codeview)
        {
            try
            {
                var ExistingSrcCode = _context.CbSrcCodes.Where(x => x.CbSrcCodeId == codeview).FirstOrDefault();
                if (ExistingSrcCode != null)
                {
                    _context.CbSrcCodes.Remove(ExistingSrcCode);
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
        #endregion srcode Class

        #region Transfer Antar Bank
        public async Task<CbTransfer> GetTransferDoc(string docno)
        {
            return await _context.CbTransfers.Where(x => x.DocNo == docno).FirstOrDefaultAsync();
        }

        public async Task<CbTransfer> GetTransferId(int id)
        {
            return await _context.CbTransfers.Where(x => x.CbTransferId == id).FirstOrDefaultAsync();
        }

        public List<CbTransfer> GetTransfer()
        {

            return _context.CbTransfers.OrderByDescending(x => x.Tanggal).ToList();

        }


        public async Task<CbTransfer> AddTransfer(TransferView trans)
        {
            //string test = codeview.SrcCode.ToUpper();
            //var cekFirst = _context.CbSrcCodes.Where(x => x.SrcCode == test).ToList();

            CbTransfer transfer = new()
            {
                DocNo = GetNumberTrf("TRF"),
                Tanggal = trans.Tanggal,
                Keterangan = trans.Keterangan,
                Kurs = trans.Kurs,
                Saldo = trans.Saldo,
                KSaldo = trans.KSaldo,
                KodeBank1 = trans.KodeBank1.ToUpper(),
                KodeBank2 = trans.KodeBank2.ToUpper()
            };

            _context.CbTransfers.Add(transfer);

            CbTransH transH = new()
            {
                DocNo = GetNumberTr2('T' + trans.KodeBank1.ToUpper().Trim()),
                KodeBank = trans.KodeBank1.ToUpper(),
                Refno = transfer.DocNo,
                Tanggal = trans.Tanggal,
                Keterangan = trans.Keterangan,
                Kurs = trans.Kurs,
                Saldo = -1 * trans.Saldo,
                KSaldo = -1 * trans.KSaldo,
                CbTransDs = new List<CbTransD>()
            };

            transH.CbTransDs.Add(new CbTransD()
            {
                SrcCode = "CB",
                Keterangan = trans.Keterangan,
                Terima = 0,
                Bayar = trans.Saldo,
                KTerima = 0,
                KBayar = trans.KSaldo,
                KValue = trans.KValue,
                Jumlah = -1 * trans.Saldo,
                KJumlah = -1 * trans.KSaldo,
                Kurs = trans.Kurs
            });

            var bank = (from e in _context.Banks where e.KodeBank == trans.KodeBank1 select e).FirstOrDefault();
            bank.Saldo -= trans.Saldo;
            bank.KSaldo -= trans.KSaldo;
            _context.Banks.Update(bank);
            _context.CbTransHs.Add(transH);

            /* ke bank */
            CbTransH transHd = new()
            {
                DocNo = GetNumberTr2('T' + trans.KodeBank2.ToUpper().Trim()),
                KodeBank = trans.KodeBank2.ToUpper(),
                Refno = transfer.DocNo,
                Tanggal = trans.Tanggal,
                Keterangan = trans.Keterangan,
                Kurs = trans.Kurs,
                Saldo = trans.Saldo,
                KSaldo = trans.KSaldo,
                CbTransDs = new List<CbTransD>()
            };

            transHd.CbTransDs.Add(new CbTransD()
            {
                SrcCode = "CB",
                Keterangan = trans.Keterangan,
                Terima = trans.Saldo,
                Bayar = 0,
                KTerima = trans.KSaldo,
                KBayar = 0,
                KValue = trans.KValue,
                Jumlah = trans.Saldo,
                KJumlah = trans.KSaldo,
                Kurs = trans.Kurs
            });

            var bankd = (from e in _context.Banks where e.KodeBank == trans.KodeBank2 select e).FirstOrDefault();
            bankd.Saldo += trans.Saldo;
            bankd.KSaldo += trans.KSaldo;
            _context.Banks.Update(bankd);
            _context.CbTransHs.Add(transHd);

            await _context.SaveChangesAsync();

            var TempTrans = GetTransferDoc(transfer.DocNo);

            return await TempTrans;
            // return true;


        }

        public async Task<CbTransfer> EditTransfer(TransferView trans)
        {
            //string test = codeview.SrcCode.ToUpper();
            //var cekFirst = _context.CbSrcCodes.Where(x => x.SrcCode == test).ToList();
            var ExistingTrans = _context.CbTransfers.Where(x => x.CbTransferId == trans.CbTransferId).FirstOrDefault();
            if (ExistingTrans != null)
            {
                _context.CbTransfers.Remove(ExistingTrans);

                var listTrans1 = _context.CbTransHs.Where(x => x.Refno == ExistingTrans.DocNo && x.KodeBank == ExistingTrans.KodeBank1).FirstOrDefault();
                if (listTrans1 != null)
                {
                    _context.CbTransHs.Remove(listTrans1);
                    var bank1 = (from e in _context.Banks where e.KodeBank == ExistingTrans.KodeBank1 select e).FirstOrDefault();
                    bank1.Saldo += ExistingTrans.Saldo;
                    bank1.KSaldo += ExistingTrans.KSaldo;
                    _context.Banks.Update(bank1);
                }

                var listTrans2 = _context.CbTransHs.Where(x => x.Refno == ExistingTrans.DocNo && x.KodeBank == ExistingTrans.KodeBank2).FirstOrDefault();
                if (listTrans2 != null)
                {
                    _context.CbTransHs.Remove(listTrans2);
                    var bank2 = (from e in _context.Banks where e.KodeBank == ExistingTrans.KodeBank2 select e).FirstOrDefault();
                    bank2.Saldo -= ExistingTrans.Saldo;
                    bank2.KSaldo -= ExistingTrans.KSaldo;
                    _context.Banks.Update(bank2);
                }

                CbTransfer transfer = new()
                {
                    DocNo = ExistingTrans.DocNo,
                    Tanggal = trans.Tanggal,
                    Keterangan = trans.Keterangan,
                    Kurs = trans.Kurs,
                    KValue = trans.KValue,
                    Saldo = trans.Saldo,
                    KSaldo = trans.KSaldo,
                    KodeBank1 = trans.KodeBank1.ToUpper(),
                    KodeBank2 = trans.KodeBank2.ToUpper()
                };

                _context.CbTransfers.Add(transfer);

                CbTransH transH = new()
                {
                    DocNo = GetNumberTr2('T' + trans.KodeBank1.ToUpper().Trim()),
                    KodeBank = trans.KodeBank1.ToUpper(),
                    Tanggal = trans.Tanggal,
                    Refno = transfer.DocNo,
                    Keterangan = trans.Keterangan,
                    Kurs = trans.Kurs,
                    Saldo = -1 * trans.Saldo,
                    KSaldo = -1 * trans.KSaldo,
                    CbTransDs = new List<CbTransD>()
                };

                transH.CbTransDs.Add(new CbTransD()
                {
                    SrcCode = "CB",
                    Keterangan = trans.Keterangan,
                    Terima = 0,
                    Bayar = trans.Saldo,
                    KTerima = 0,
                    KBayar = trans.KSaldo,
                    KValue = trans.KValue,
                    Jumlah = -1 * trans.Saldo,
                    KJumlah = -1 * trans.KSaldo,
                    Kurs = trans.Kurs
                });

                var bank = (from e in _context.Banks where e.KodeBank == trans.KodeBank1 select e).FirstOrDefault();
                bank.Saldo -= trans.Saldo;
                bank.KSaldo -= trans.KSaldo;
                _context.Banks.Update(bank);
                _context.CbTransHs.Add(transH);

                /* ke bank */
                CbTransH transHd = new()
                {
                    DocNo = GetNumberTr2('T' + trans.KodeBank2.ToUpper().Trim()),
                    KodeBank = trans.KodeBank2.ToUpper(),
                    Refno = transfer.DocNo,
                    Tanggal = trans.Tanggal,
                    Keterangan = trans.Keterangan,
                    Kurs = trans.Kurs,
                    Saldo = trans.Saldo,
                    KSaldo = trans.KSaldo,
                    CbTransDs = new List<CbTransD>()
                };

                transHd.CbTransDs.Add(new CbTransD()
                {
                    SrcCode = "CB",
                    Keterangan = trans.Keterangan,
                    Terima = trans.Saldo,
                    Bayar = 0,
                    KTerima = trans.KSaldo,
                    KBayar = 0,
                    KValue = trans.KValue,
                    Jumlah = trans.Saldo,
                    KJumlah = trans.KSaldo,
                    Kurs = trans.Kurs
                });

                var bankd = (from e in _context.Banks where e.KodeBank == trans.KodeBank2 select e).FirstOrDefault();
                bankd.Saldo += trans.Saldo;
                bankd.KSaldo += trans.KSaldo;
                _context.Banks.Update(bankd);
                _context.CbTransHs.Add(transHd);

                await _context.SaveChangesAsync();

                var TempTrans = GetTransferDoc(transfer.DocNo);

                return await TempTrans;


            }
            else
            {
                return ExistingTrans;
            }


            // return true;


        }

        public async Task<bool> DelTransfer(int id)
        {
            try
            {

                var ExistingTrans = _context.CbTransfers.Where(x => x.CbTransferId == id).FirstOrDefault();
                if (ExistingTrans != null)
                {
                    _context.CbTransfers.Remove(ExistingTrans);

                    var listTrans1 = _context.CbTransHs.Where(x => x.Refno == ExistingTrans.DocNo && x.KodeBank == ExistingTrans.KodeBank1).FirstOrDefault();
                    if (listTrans1 != null)
                    {
                        _context.CbTransHs.Remove(listTrans1);
                        var bank1 = (from e in _context.Banks where e.KodeBank == ExistingTrans.KodeBank1 select e).FirstOrDefault();
                        bank1.Saldo += ExistingTrans.Saldo;
                        bank1.KSaldo += ExistingTrans.KSaldo;
                        _context.Banks.Update(bank1);
                    }

                    var listTrans2 = _context.CbTransHs.Where(x => x.Refno == ExistingTrans.DocNo && x.KodeBank == ExistingTrans.KodeBank2).FirstOrDefault();
                    if (listTrans2 != null)
                    {
                        _context.CbTransHs.Remove(listTrans2);
                        var bank2 = (from e in _context.Banks where e.KodeBank == ExistingTrans.KodeBank2 select e).FirstOrDefault();
                        bank2.Saldo -= ExistingTrans.Saldo;
                        bank2.KSaldo -= ExistingTrans.KSaldo;
                        _context.Banks.Update(bank2);
                    }

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
        #endregion Transfer Antar Bank

        #region Transaksi Bank Class
        public async Task<CbTransH> GetTransDoc(string docno)
        {
            return await _context.CbTransHs.Include(p => p.CbTransDs).Where(x => x.DocNo == docno).FirstOrDefaultAsync();
        }
        public CbTransH GetTrans(int id)
        {
            return _context.CbTransHs.Include(p => p.CbTransDs).Where(x => x.CbTransHId == id).FirstOrDefault();
        }

        public Task<List<CbTransH>> GetTransH()
        {
            // return  _context.CbTransHs.Include(p =>p.CbTransDs).OrderByDescending(x =>x.Tanggal).ToListAsync();
            return _context.CbTransHs.OrderByDescending(x => x.Tanggal).ToListAsync();

        }

        public Task<List<CbTransH>> Get3TransH()
        {
            // return  _context.CbTransHs.Include(p =>p.CbTransDs).OrderByDescending(x =>x.Tanggal).ToListAsync();
            return _context.CbTransHs.OrderByDescending(x => x.Tanggal).Where(x => x.Tanggal > DateTime.Today.AddMonths(-3)).ToListAsync();

        }

        public async Task<List<CbTransD>> GetTransD()
        {
            return await _context.CbTransDs.ToListAsync();
        }

        public async Task<CbTransH> AddTransH(TransHView trans)
        {
            //string test = codeview.SrcCode.ToUpper();
            //var cekFirst = _context.CbSrcCodes.Where(x => x.SrcCode == test).ToList();

            CbTransH transH = new()
            {
                DocNo = GetNumber(trans.KodeDoc.ToUpper()),
                KodeBank = trans.KodeBank.ToUpper(),
                Tanggal = trans.Tanggal,
                Keterangan = trans.Keterangan,
                Kurs = trans.Kurs,
                Saldo = trans.Saldo,
                KSaldo = trans.KSaldo,
                CbTransDs = new List<CbTransD>()
            };
            foreach (var item in trans.TransDs)
            {
                transH.CbTransDs.Add(new CbTransD()
                {
                    SrcCode = item.SrcCode,
                    Keterangan = item.Keterangan,
                    Terima = item.Terima,
                    Bayar = item.Bayar,
                    KTerima = item.KTerima,
                    KBayar = item.KBayar,
                    KValue = item.KValue,
                    Jumlah = item.Jumlah,
                    KJumlah = item.KJumlah,
                    Kurs = item.Kurs
                });
            }
            var bank = (from e in _context.Banks where e.KodeBank == trans.KodeBank select e).FirstOrDefault();
            bank.Saldo += trans.Saldo;
            bank.KSaldo += trans.KSaldo;
            _context.Banks.Update(bank);
            _context.CbTransHs.Add(transH);
            await _context.SaveChangesAsync();

            var TempTrans = GetTransDoc(transH.DocNo);

            return await TempTrans;
            // return true;


        }

        public CbTransH EditTransH(TransHView trans)
        {
            //string test = codeview.SrcCode.ToUpper();
            //var cekFirst = _context.CbSrcCodes.Where(x => x.SrcCode == test).ToList();



            try
            {
                var ExistingTrans = _context.CbTransHs.Where(x => x.CbTransHId == trans.CbTransHId).FirstOrDefault();
                if (ExistingTrans != null)
                {

                    _context.CbTransHs.Remove(ExistingTrans);

                    var bank = (from e in _context.Banks where e.KodeBank == trans.KodeBank select e).FirstOrDefault();
                    bank.Saldo -= ExistingTrans.Saldo;
                    bank.KSaldo -= ExistingTrans.KSaldo;
                    _context.Banks.Update(bank);

                    /* update */

                    CbTransH transH = new()
                    {
                        //  transH.DocNo = ExistingTrans.DocNo;
                        DocNo = ExistingTrans.DocNo,
                        KodeBank = trans.KodeBank.ToUpper(),
                        Tanggal = trans.Tanggal,
                        Keterangan = trans.Keterangan,
                        Kurs = trans.Kurs,
                        Saldo = trans.Saldo,
                        KSaldo = trans.KSaldo,
                        CbTransDs = new List<CbTransD>()
                    };
                    foreach (var item in trans.TransDs)
                    {
                        transH.CbTransDs.Add(new CbTransD()
                        {
                            SrcCode = item.SrcCode,
                            Keterangan = item.Keterangan,
                            Terima = item.Terima,
                            Bayar = item.Bayar,
                            KTerima = item.KTerima,
                            KBayar = item.KBayar,
                            KValue = item.KValue,
                            Jumlah = item.Jumlah,
                            KJumlah = item.KJumlah,
                            Kurs = item.Kurs
                        });
                    }
                    bank.Saldo += trans.Saldo;
                    bank.KSaldo += trans.KSaldo;

                    _context.Banks.Update(bank);
                    _context.CbTransHs.Add(transH);
                    _context.SaveChanges();

                    return transH;
                    //   return true;
                }
                else
                {
                    return ExistingTrans;
                }
            }
            catch (Exception)
            {
                throw;
            }




        }

        public async Task<bool> DelTransH(int id)
        {
            try
            {
                var ExistingTrans = _context.CbTransHs.Where(x => x.CbTransHId == id).FirstOrDefault();
                if (ExistingTrans != null)
                {
                    var bank = (from e in _context.Banks where e.KodeBank == ExistingTrans.KodeBank select e).FirstOrDefault();
                    bank.Saldo -= ExistingTrans.Saldo;
                    bank.KSaldo -= ExistingTrans.KSaldo;
                    _context.Banks.Update(bank);
                    _context.CbTransHs.Remove(ExistingTrans);
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

        #endregion Transaksi Bank Class

        public string GetNumber(string kodeno)
        {
            string kodeurut = kodeno + " -";
            string thnbln = DateTime.Now.ToString("yyMM");
            string xbukti = kodeurut + thnbln.Substring(0, 2) + '2' + thnbln.Substring(2, 2) + '-';
            var maxvalue = "";
            var maxlist = _context.CbTransHs.Where(x => x.DocNo.Substring(0, 10).Equals(xbukti)).ToList();
            if (maxlist != null)
            {
                maxvalue = maxlist.Max(x => x.DocNo);

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

        public string GetNumberTrf(string kodeno)
        {
            string kodeurut = kodeno + "-";
            string thnbln = DateTime.Now.ToString("yyMM");
            string xbukti = kodeurut + thnbln.Substring(0, 2) + '2' + thnbln.Substring(2, 2) + '-';
            var maxvalue = "";
            var maxlist = _context.CbTransfers.Where(x => x.DocNo.Substring(0, 10).Equals(xbukti)).ToList();
            if (maxlist != null)
            {
                maxvalue = maxlist.Max(x => x.DocNo);

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

        public string GetNumberTr2(string kodeno)
        {
            string kodeurut = kodeno + "-";
            string thnbln = DateTime.Now.ToString("yyMM");
            string xbukti = kodeurut + thnbln.Substring(0, 2) + '2' + thnbln.Substring(2, 2) + '-';
            var maxvalue = "";
            var maxlist = _context.CbTransHs.Where(x => x.DocNo.Substring(0, 10).Equals(xbukti)).ToList();
            if (maxlist != null)
            {
                maxvalue = maxlist.Max(x => x.DocNo);

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
