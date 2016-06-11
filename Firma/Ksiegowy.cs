using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma
{
    public class Ksiegowy : Pracownik, Zarobki
    {
        public enum Stanowisko { GlownyKsiegowy, KontrolerFinansowy, DyrektorFinansowy };
        public Stanowisko _Stanowisko { get; private set; }

        public Ksiegowy(int id, string i, string n, Plec p, int w, double pensja, Stanowisko st) : base(id, i, n, p, w, pensja)
        {
            _Stanowisko = st;
        }

        public double oszacujPremie()
        {
            double dodatek = 0;
            if (Wiek > 25 && _Stanowisko == Stanowisko.GlownyKsiegowy)
                dodatek = 6500.0;
            else if (Wiek > 30 && _Stanowisko == Stanowisko.DyrektorFinansowy)
                dodatek = 12000.0;

            return dodatek;
        }
    }
}
