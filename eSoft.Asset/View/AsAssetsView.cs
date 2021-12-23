using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSoft.Asset.View
{
    public class AsAssetsView
    {
        public int AsAssetsId { get; set; }
        public string BarcodeAssets { get; set; }
        public string AsItemCode { get; set; }
        public string Merek { get; set; }
        public string NamaBarang { get; set; }
        public string NoMesin { get; set; }
        public string NoRangka { get; set; }
        public string NoPol { get; set; }
        public int Qty { get; set; }
        public decimal Nilai { get; set; }
        public decimal PPn { get; set; }
        public decimal Asuransi { get; set; }
        public decimal Bunga { get; set; }
        public decimal Administrasi { get; set; }
        public decimal Provisi { get; set; }
        public decimal Termin { get; set; }
        public decimal Penyusutan { get; set; }
        public decimal SisaNilai { get; set; }
        public DateTime TglBeli { get; set; }
        public DateTime JatuhTempo { get; set; }
        public decimal NilaiTerjual { get; set; }
        public DateTime TglJual { get; set; }

        public string Acctset { get; set; }
        public string DistCode { get; set; }
    }
}
