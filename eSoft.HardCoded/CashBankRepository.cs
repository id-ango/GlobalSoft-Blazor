using eSoft.CashBank.Model;
using System;
using System.Collections.Generic;
using eSoft.UseCases.PluginInterfaces.DataStore;


namespace eSoft.HardCoded
{
    public class CashBankRepository 
    {
        private List<CbBank> Banks;

        public CashBankRepository()
        {
            Banks = new List<CbBank>
            {
                new CbBank {CbBankId = 1, KodeBank="B1",NmBank="BCA 2001"},
                new CbBank {CbBankId = 2, KodeBank="B2",NmBank="BCA 2018"},
                new CbBank {CbBankId = 3, KodeBank="K1",NmBank="Kas Kecil"}

            };

           
        }

        public IEnumerable<CbBank> GetBanks()
        {
            return Banks;
        }
    }
}
