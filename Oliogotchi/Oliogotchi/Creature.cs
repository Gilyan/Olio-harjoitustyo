﻿/* **********************************************************
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
        private int hunger;
        private int happiness;
        private int cleanliness;

        public string Ani; // polku kuviin

        // Käytetään kapselointia, jotta olion arvot pysyvät olion sisällä
        public int Age { get; set; }        
        public int Hunger { get { return hunger; } set {; } }
        public int Happiness { get { return happiness; } set {; } }
        public int Cleanliness { get { return cleanliness; } set {; } }

        public void FillDefault()        // Luo uuden lemmikkiolion
        {
            vegeCounter = 0;                // Alustetaan arvot
            meatCounter = 0;
            cleanliness = 50;
            hunger = 50;
            happiness = 50;

            Age = 0;
            Ani = "slimeAni";
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
            cleanliness += 1;
        }

        public void Brush()
        {
            happiness += 1;
        }
        public void GamePoints(int gamePoints)
        {
            if (happiness + gamePoints > 0 && happiness + gamePoints < 100)
            {
                happiness += gamePoints;
            }
            else if (happiness + gamePoints >= 100)
            {
                happiness = 100;
            }
            else
            {
                happiness = 0;
            }
        }
        public void HungerPoints(int hungerPoints)
        {
            if (hunger + hungerPoints > 0 && hunger + hungerPoints < 100)
            {
                hunger += hungerPoints;
            }
            else
            {
                hunger = 0;
            }
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

            hunger += 1;
        }

        public void Evolve()
        {
            if (vegeCounter * 1.5 > vegeCounter + meatCounter)
            { Ani = "vegeAni"; }

            else if (meatCounter * 1.5 > vegeCounter + meatCounter)
            { Ani = "meatAni"; }

            else
            { Ani = "omniAni"; }
        }
    }
}
