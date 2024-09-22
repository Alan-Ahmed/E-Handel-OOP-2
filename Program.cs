namespace E_Handel_OOP_2
{
    // Klass för Kund
    public class Kund
    {
        public string Namn { get; set; }
        public string Epost { get; set; }
        private string Lösenord { get; set; }
        public List<Beställning> Beställningar { get; set; }

        public Kund(string namn, string epost, string lösenord)
        {
            Namn = namn;
            Epost = epost;
            Lösenord = lösenord;
            Beställningar = new List<Beställning>();
        }

        public string GetNamn()
        {
            return Namn;
        }

        public string GetEpost()
        {
            return Epost;
        }

        public string GetLösenord()
        {
            return Lösenord;
        }

        public void LäggTillBeställning(Beställning beställning)
        {
            Beställningar.Add(beställning);
        }
    }

    // Klass för Produkt
    public class Produkt
    {
        public string Namn { get; set; }
        public int Pris { get; set; }
        public string Kategori { get; set; }
        public int LagerAntal { get; set; }

        public Produkt(string namn, int pris, string kategori, int lagerAntal)
        {
            Namn = namn;
            Pris = pris;
            Kategori = kategori;
            LagerAntal = lagerAntal;
        }

        public string GetProduktNamn()
        {
            return Namn;
        }

        public int GetPris()
        {
            return Pris;
        }

        public int GetLagerAntal()
        {
            return LagerAntal;
        }

        public void MinskaLager(int antal)
        {
            if (antal <= LagerAntal)
            {
                LagerAntal -= antal;
            }
            else
            {
                throw new Exception("Otillräckligt lager");
            }
        }
    }

    // Klass för Kundvagn
    public class Kundvagn
    {
        public List<(Produkt produkt, int antal)> Produkter { get; set; }
        public int Kostnad { get; private set; }
        public string KundId { get; set; }

        public Kundvagn(string kundId)
        {
            Produkter = new List<(Produkt, int)>();
            KundId = kundId;
            Kostnad = 0;
        }

        public void LäggTillProdukt(Produkt produkt, int antal = 1)
        {
            if (produkt.GetLagerAntal() >= antal)
            {
                produkt.MinskaLager(antal);
                Produkter.Add((produkt, antal));
                BeräknaTotalKostnad();
            }
            else
            {
                Console.WriteLine($"{produkt.GetProduktNamn()} finns inte tillräckligt i lager.");
            }
        }

        public void TaBortProdukt(Produkt produkt)
        {
            Produkter.RemoveAll(p => p.produkt == produkt);
            BeräknaTotalKostnad();
        }

        public void BeräknaTotalKostnad()
        {
            Kostnad = 0;
            foreach (var (produkt, antal) in Produkter)
            {
                Kostnad += produkt.GetPris() * antal;
            }
        }
    }

    // Klass för Beställning
    public class Beställning
    {
        public string KundNamn { get; set; }
        public List<(Produkt produkt, int antal)> Produkter { get; set; }
        public DateTime Datum { get; set; }
        public int TotalaPriset { get; set; }

        public Beställning(Kund kund, List<(Produkt produkt, int antal)> produkter)
        {
            KundNamn = kund.GetNamn();
            Produkter = produkter;
            Datum = DateTime.Now;
            TotalaPriset = 0;
            foreach (var (produkt, antal) in produkter)
            {
                TotalaPriset += produkt.GetPris() * antal;
            }
        }

        public string GetInfo()
        {
            string produktInfo = "";
            foreach (var (produkt, antal) in Produkter)
            {
                produktInfo += $"{produkt.GetProduktNamn()} x {antal}\n";
            }
            return $"Beställning för {KundNamn}\nDatum: {Datum}\nProdukter:\n{produktInfo}Totalt pris: {TotalaPriset} SEK";
        }
    }

    // Klass för Lager
    public class Lager
    {
        public List<Produkt> Produkter { get; set; }

        public Lager()
        {
            Produkter = new List<Produkt>();
        }

        public void LäggTillProdukt(Produkt produkt)
        {
            Produkter.Add(produkt);
        }

        public string GetLagerInfo()
        {
            string lagerInfo = "";
            foreach (var produkt in Produkter)
            {
                lagerInfo += $"{produkt.GetProduktNamn()} - Lager: {produkt.GetLagerAntal()}\n";
            }
            return lagerInfo;
        }
    }
}
