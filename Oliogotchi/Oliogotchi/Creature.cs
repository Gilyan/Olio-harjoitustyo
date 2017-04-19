/* **********************************************************
Oliogotchin oliosivu.

Toiminta: Täällä määritellään itse olion (lemmikin) ominaisuudet ja metodit.

Luotu 29.3.2017

Minttu Mäkäläinen K8517
Kioto Hiirola K8252
Joona Hautamäki K1647
@ JAMK 
********************************************************** */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oliogotchi
{
    [Serializable]          // Sarjallisestaan luokka, jotta voidaan kirjoittaa tiedostoon
    class Creature
    {
        private int vegeCounter;
        private int meatCounter;
        // kapselointi käytössä ja olion statsiarvot pysyvät olion sisällä! tehkää itsellenne tarvittavat metodit jotta pääsette käsiksi niihin tai käyttäkää/muokatkaa olemassaolevia
        public int Age { get; set; }
        private int hunger;
        public int Hunger { get { return hunger; } set {; } }
        private int happiness;
        public int Happiness { get { return happiness; } set {; } }
        public string Image { get; set; }
        private int cleanliness;
        public int Cleanliness { get { return cleanliness; } set {; } }

        public void FillDefault()        // Luo uuden lemmikkiolion
        {
            vegeCounter = 0;                // Alustetaan arvot
            meatCounter = 0;
            cleanliness = 50;
            hunger = 50;
            happiness = 50;

            Age = 0;
            Image = "Resources/slime/sheet.png";
        }

        public void GetDirty()
        {
            cleanliness -= 1;
        }

        public void GetSad()
        {
            happiness -= 1;
        }

        public void GetHungry()
        {
            hunger -= 1;
        }

        public void Wash()
        {
            cleanliness += 10;
        }

        public void Brush()
        {
            happiness += 10;
        }

        public void Feed(bool isMeat)
        {
            if (isMeat == true)
            {
                meatCounter += 1;
            }

            else
            {
                vegeCounter += 1;
            }

            hunger += 5;
        }

        public void Evolve()
        {
            if (vegeCounter * 1.5 > vegeCounter + meatCounter)
            { Image = "Resources/vege/sheet.png"; }

            else if (meatCounter * 1.5 > vegeCounter + meatCounter)
            { Image = "Resources/meat/sheet.png"; }

            else
            { Image = "Resources/omni/sheet.png"; }
        }
    }
}
