﻿using eSoft.Piutang.Model;
using eSoft.Piutang.View;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace eSoft.Piutang.Services
{
    public interface IReceivableServices
    {
        bool CekKdCustomer(string customer);
        List<ArCust> GetCustomer();
        ArCust GetCustomerId(int id);
        ArCust GetCustomerCode(string xKode);
        bool AddCustomer(CustomerView customers);
        Task<bool> EditCustomer(CustomerView customers);
        Task<bool> DelCustomer(int customers);
        bool CekAcctSet(string acctset);
        List<ArAcct> GetArAkunSet();
        ArAcct GetArAkunSetId(int id);
        bool AddAkunSet(ArAcctView codeview);
        Task<bool> EditAkunSet(ArAcctView codeview);
        Task<bool> DelAkunSet(int codeview);
        bool CekDistCode(string distcode);
        List<ArDist> GetDist();
        ArDist GetDistId(int id);
        bool AddDist(ArDistView codeview);
        Task<bool> EditDist(ArDistView codeview);
        Task<bool> DelDist(int codeview);

        ArTransH GetTrans(int id);
        List<ArTransH> GetTransH();
        List<ArTransH> Get3TransH();
        List<ArTransD> GetTransD();
        ArTransH AddTransH(ArTransHView transH);
        ArTransH EditTransH(ArTransHView transH);
        Task<bool> DelTransH(int id);
        ArPiutng GetPiutang(string bukti);
        bool CekAlreadyPayment(string dokumen);
        List<ArPiutng> Detail1(string xKdHeader);
        List<ArPiutngView> GetUangMuka();
    }
}
