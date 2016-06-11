using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma
{
    public class Programista : Pracownik
    {
        public enum Specjalnosc { C, JAVA, JavaScript};
        public Specjalnosc _Specjalnosc { get; private set; }
        public Programista(int id, string i, string n, Plec p, int w, double pensja, Specjalnosc s) : base(id, i, n, p, w, pensja)
        {
            _Specjalnosc = s;
        }
        public override double pensjaRoczna()
        {
            return base.pensjaRoczna() * 0.78;
        }
    }
}
