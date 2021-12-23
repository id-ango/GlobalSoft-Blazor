﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eSoft.Asset.Data;
using eSoft.Asset.Model;
using eSoft.Asset.View;

using Microsoft.EntityFrameworkCore;


namespace eSoft.Asset.Services
{
    public class AssetServices : IAssetServices
    {
        private readonly DbContextAssets _context;

        public AssetServices(DbContextAssets context)
        {
            _context = context;
        }

        #region AsAcct Class

        public bool CekAcctSet(string supplier)
        {
            string test = supplier.ToUpper();
            var cekFirst = _context.AsAcctsets.Where(x => x.AcctSet == test).ToList();
            if (cekFirst.Count == 0)
            {
                return false;
            }
            return true;
        }

        public List<AsAcctset> GetAsAkunSet()
        {
            return _context.AsAcctsets.ToList();
        }

        public AsAcctset GetAsAkunSetId(int id)
        {
            return _context.AsAcctsets.Where(x => x.AsAcctId == id).FirstOrDefault();
        }

        public bool AddAkunSet(AsAcctsetView codeview)
        {
            string test = codeview.AcctSet.ToUpper();
            var cekFirst = _context.AsAcctsets.Where(x => x.AcctSet == test).ToList();
            if (cekFirst.Count == 0)
            {
                AsAcctset AcctCode = new AsAcctset()
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
                _context.AsAcctsets.Add(AcctCode);
                _context.SaveChanges();
                return true;
            }
            else
            {

                return false;
            }


        }

        public async Task<bool> EditAkunSet(AsAcctsetView codeview)
        {
            try
            {
                var ExistingAkunSet = _context.AsAcctsets.Where(x => x.AsAcctId == codeview.AsAcctId).FirstOrDefault();
                if (ExistingAkunSet != null)
                {
                    ExistingAkunSet.Description = codeview.Description;
                    ExistingAkunSet.Acct1 = codeview.Acct1;
                    ExistingAkunSet.Acct2 = codeview.Acct2;
                    ExistingAkunSet.Acct3 = codeview.Acct3;
                    ExistingAkunSet.Acct4 = codeview.Acct4;
                    ExistingAkunSet.Acct5 = codeview.Acct5;
                    ExistingAkunSet.Acct6 = codeview.Acct6;

                    _context.AsAcctsets.Update(ExistingAkunSet);
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
                var ExistingAkunSet = _context.AsAcctsets.Where(x => x.AsAcctId == codeview).FirstOrDefault();
                if (ExistingAkunSet != null)
                {
                    _context.AsAcctsets.Remove(ExistingAkunSet);
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
            var cekFirst = _context.AsDistSets.Where(x => x.DistCode == test).ToList();
            if (cekFirst.Count == 0)
            {
                return false;
            }
            return true;
        }

        public List<AsDistSet> GetDist()
        {
            return _context.AsDistSets.ToList();
        }

        public AsDistSet GetDistId(int id)
        {
            return _context.AsDistSets.Where(x => x.AsDistId == id).FirstOrDefault();
        }

        public bool AddDist(AsDistSetView codeview)
        {
            string test = codeview.DistCode.ToUpper();
            var cekFirst = _context.AsDistSets.Where(x => x.DistCode == test).ToList();
            if (cekFirst.Count == 0)
            {
                AsDistSet AcctCode = new AsDistSet()
                {
                    DistCode = codeview.DistCode.ToUpper(),
                    Description = codeview.Description,
                    Dist1 = codeview.Dist1

                };
                _context.AsDistSets.Add(AcctCode);
                _context.SaveChanges();
                return true;
            }
            else
            {

                return false;
            }


        }

        public async Task<bool> EditDist(AsDistSetView codeview)
        {
            try
            {
                var ExistingDist = _context.AsDistSets.Where(x => x.AsDistId == codeview.AsDistId).FirstOrDefault();
                if (ExistingDist != null)
                {
                    ExistingDist.Description = codeview.Description;
                    ExistingDist.Dist1 = codeview.Dist1;

                    _context.AsDistSets.Update(ExistingDist);
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
                var ExistingDist = _context.AsDistSets.Where(x => x.AsDistId == codeview).FirstOrDefault();
                if (ExistingDist != null)
                {
                    _context.AsDistSets.Remove(ExistingDist);
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
    }
}
