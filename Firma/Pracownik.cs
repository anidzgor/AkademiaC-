using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma
{
    public class Pracownik
    {
        public int ID { get; private set; }
        public string Imie { get; private set; }
        public string Nazwisko { get; private set; }
        public int Wiek { get; private set; }
        public double Pensja { get; private set; }
        public enum Plec { Mezczyzna, Kobieta };
        public Plec _Plec { get; private set; }
        public Pracownik(int id, string i, string n, Plec p, int w, double pensja)
        {
            ID = id;
            Imie = i;
            Nazwisko = n;
            _Plec = p;
            Wiek = w;
            Pensja = pensja;
        }

        public virtual double pensjaRoczna()
        {
            return Pensja * 12;
        }

        public override string ToString()
        {
            return ID + ". " + Imie + " " + Nazwisko;
        }

    }
}
