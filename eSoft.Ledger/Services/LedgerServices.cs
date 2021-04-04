
using eSoft.Ledger.Model;
using eSoft.Ledger.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eSoft.Ledger.Data;

namespace eSoft.Ledger.Services
{
   
    public class LedgerServices : ILedgerServices
    {

        private readonly DbContextLedger _context;

        public LedgerServices(DbContextLedger context)
        {
            _context = context;
        }

        public List<GlAccount> GetGlAccount()
        {
            return _context.GlAccounts.OrderBy(x => x.GlAcct).ToList();
        }

        public bool CekKdAkun(string kodeAkun)
        {
            string test = kodeAkun.ToUpper();
            var cekFirst = _context.GlAccounts.Where(x => x.GlAcct == test).ToList();
            if (cekFirst.Count == 0)
            {
                return false;
            }
            return true;
        }

        public GlAccount GetGlAccountId(int id)
        {
            return _context.GlAccounts.Where(x => x.GlAccountId == id).FirstOrDefault();
        }

        public bool AddGlAccount(GlAccountView glakun)
        {
            string test = glakun.GlAcct.ToUpper();
            var cekFirst = _context.GlAccounts.Where(x => x.GlAcct == test).ToList();
            if (cekFirst.Count == 0)
            {
                GlAccount Akun = new GlAccount()
                {
                    GlAcct = glakun.GlAcct.ToUpper(),
                    GlNama = glakun.GlNama,
                 
                    GlTipe = (int)glakun.GlStatus,
                    NamaLengkap = glakun.NamaLengkap



                };
                _context.GlAccounts.Add(Akun);
                 _context.SaveChanges();
                return true;
            }
            else
            {

                return false;
            }

        }

        public bool EditGlAccount(GlAccountView glakun)
        {
            try
            {
                var ExistingBank = _context.GlAccounts.Where(x => x.GlAccountId == glakun.GlAccountId).FirstOrDefault();
                if (ExistingBank != null)
                {
                    ExistingBank.GlNama = glakun.GlNama;
                    ExistingBank.TipeGl = glakun.TipeGL;
                    ExistingBank.NamaLengkap = glakun.NamaLengkap;

                    _context.GlAccounts.Update(ExistingBank);
                    _context.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return false;

        }

        public async Task<bool> DelGlAccount(int banks)
        {
            try
            {
                var ExistingBank = _context.GlAccounts.Where(x => x.GlAccountId == banks).FirstOrDefault();
                if (ExistingBank != null)
                {
                    _context.GlAccounts.Remove(ExistingBank);
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

        #region GlCode Class

        public List<GlCode> GetGlCode()
        {
            return _context.GlCodes.OrderBy(x => x.KodeGl).ToList();
        }

        public GlCode GetGlCodeId(int id)
        {
            return _context.GlCodes.Where(x => x.GlCodeId == id).FirstOrDefault();
        }

        public async Task<bool> AddGlCode(GlCodeView codeview)
        {
            string test = codeview.KodeGl.ToUpper();
            var cekFirst = _context.GlCodes.Where(x => x.KodeGl == test).ToList();
            if (cekFirst.Count == 0)
            {
                GlCode Division = new GlCode()
                {
                    KodeGl = codeview.KodeGl.ToUpper(),
                    NamaGl = codeview.NamaGl

                };
                _context.GlCodes.Add(Division);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {

                return false;
            }

        }

        public async Task<bool> EditGlCode(GlCodeView codeview)
        {
            try
            {
                var ExistingDiv = _context.GlCodes.Where(x => x.GlCodeId == codeview.GlCodeId).FirstOrDefault();
                if (ExistingDiv != null)
                {
                    ExistingDiv.NamaGl = codeview.NamaGl;


                    _context.GlCodes.Update(ExistingDiv);
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

        public async Task<bool> DelGlCode(int codeview)
        {
            try
            {
                var ExistingDiv = _context.GlCodes.Where(x => x.GlCodeId == codeview).FirstOrDefault();
                if (ExistingDiv != null)
                {
                    _context.GlCodes.Remove(ExistingDiv);
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

        #endregion GlCode Class
    }
}
