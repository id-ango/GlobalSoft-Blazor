using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Accounting.Data;

namespace Accounting.Services
{
    public  class AdministrationServices : IAdministrationServices
    {
        private readonly ApplicationDbContext _context;
        public AdministrationServices(ApplicationDbContext context)
        {
            _context = context;
        }

        #region Bank Class

        public List<IdentityRole> GetRoles()
        {
            return _context.Roles.ToList();
        }

        public IdentityRole GetRoleId(string id)
        {
            return _context.Roles.Where(x => x.Id == id).First();
        }

        public IdentityRole GetRoleName(string id)
        {
            return _context.Roles.Where(x => x.NormalizedName == id).First();
        }

        public bool CekNameRole(string kodeBank)
        {
            string test = kodeBank.ToUpper();
            var cekFirst = _context.Roles.Where(x => x.NormalizedName == test).ToList();
            if (cekFirst.Count == 0)
            {
                return false;
            }
            return true;
        }

        public bool AddRoles(IdentityRole banks)
        {
            string test = banks.Name.ToUpper();
            var cekFirst = _context.Roles.Where(x => x.NormalizedName == test).ToList();
            if (cekFirst.Count == 0)
            {
                IdentityRole Bank = new()
                {
                    Name = banks.Name,
                   NormalizedName = banks.Name.ToUpper(),
                    

                };
                _context.Roles.Add(Bank);
                _context.SaveChanges();
                return true;
            }
            else
            {

                return false;
            }

        }

        public async Task<bool> EditRoles(IdentityRole banks)
        {
            try
            {
                var ExistingBank = _context.Roles.Where(x =>x.Id == banks.Id).FirstOrDefault();
                if (ExistingBank != null && banks.Name != String.Empty)
                {
                    ExistingBank.Name = banks.Name;
                    ExistingBank.NormalizedName = banks.Name.ToUpper();
                    

                    _context.Roles.Update(ExistingBank);
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

        public bool DelRoles(string banks)
        {
            try
            {
                var ExistingBank = _context.Roles.Single(item => item.Id == banks);
                //  var ExistingBank = _context.Banks.Where(x => x.CbBankId == banks).FirstOrDefault();
                if (ExistingBank != null)
                {
                    _context.Roles.Remove(ExistingBank);
                    _context.SaveChanges();
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
    }
}
