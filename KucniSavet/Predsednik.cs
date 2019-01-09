using System;

namespace KucniSavet
{
    class Predsednik : Stanar
    {
        public override void UnosGodinaRodjenja()
        {
            base.UnosGodinaRodjenja();
            if (DateTime.Now.Year - GodinaRodjenja < 18)
                throw new ArgumentException("Predsednik mora biti punoletan");
        }

        public override void UnosNosilac()
        {
            base.UnosNosilac();
            if (!Nosilac)
                throw new ArgumentException("Predsednik mora biti nosilac stana");
        }

        public void UnosPredsednik()
        {
            UnosIme();
            UnosPrezime();
            while (true)
            {
                try
                {
                    UnosGodinaRodjenja();
                    break;
                }
                catch
                {
                    continue;
                }
            }
            UnosEmail();
            UnosKorisnickoIme();
            UnosLozinka();
            UnosBrojStana();
            while (true)
            {
                try
                {
                    UnosNosilac();
                    break;
                }
                catch
                {
                    continue;
                }
            }
        }
    }
}
