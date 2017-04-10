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
        public int Age { get; set; }
        public int Hunger { get; set; }
        public int Happiness { get; set; }
        public string Image { get; set; }
        public int Cleanliness { get; set; }
    }
}
