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

        public TimeSpan ElvitelIdoben => TimeSpan.Parse($"{this.ElvitelOraja}:{this.ElvitelPerce}");
        public TimeSpan VisszahozatalIdoben => TimeSpan.Parse($"{this.VisszahozatalOraja}:{this.VisszahozatalPerce}");

        public bool vizenVanAHajo()
        {
            TimeSpan currentTime = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

            return currentTime >= ElvitelIdoben.Add(TimeSpan.FromMinutes(5)) && currentTime <= VisszahozatalIdoben;
        }

        public double kolcsonzesIdotartama() => (VisszahozatalIdoben - ElvitelIdoben).TotalMinutes;

        public int hanyMegkezdettFelOra() => (int)(Math.Ceiling(kolcsonzesIdotartama() / 30));

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

        public override string ToString()
        {
            return $"{this.HajoAzonosito}\t{this.kolcsonzesIdotartama()}\t{this.hanyMegkezdettFelOra()}";
        }
    }
}
