using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KolcsonzoGUI
{
    internal class Kolcsonzes
    {
        public string Nev { get; set; }
        public byte HajoAzonosito { get; set; }
        public string HajoTipus { get; set; }
        public byte SzemelyekSzama { get; set; }
        public byte ElvitelOraja { get; set; }
        public byte ElvitelPerce { get; set; }
        public byte VisszahozatalOraja { get; set; }
        public byte VisszahozatalPerce { get; set; }

        public TimeSpan elvitelIdoben => TimeSpan.Parse($"{this.ElvitelOraja}:{this.ElvitelPerce}");
        public TimeSpan visszahozatalIdoben => TimeSpan.Parse($"{this.VisszahozatalOraja}:{this.VisszahozatalPerce}");

        //public bool vizenVanAHajo()
        //{
        //    return 
        //}

        public Kolcsonzes(string sor)
        {
            var x = sor.Split(";");
            Nev = x[0];
            HajoAzonosito = byte.Parse(x[1]);
            HajoTipus = x[2];
            SzemelyekSzama = byte.Parse(x[3]);
            ElvitelOraja = byte.Parse(x[4]);
            ElvitelPerce = byte.Parse(x[5]);
            VisszahozatalOraja = byte.Parse(x[6]);
            VisszahozatalPerce = byte.Parse(x[7]);
        }
    }
}
