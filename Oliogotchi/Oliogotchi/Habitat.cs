/* **********************************************************
Oliogotchin elintilasivu.

Toiminta: Täällä määritellään olion elintilan ominaisuudet ja metodit.

Luotu 29.3.2017

Minttu Mäkäläinen K8517
Kioto Hiirola K8252
Joona Hautamäki K1647
@ JAMK 
********************************************************** */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oliogotchi
{
    [Serializable]          // Sarjallisestaan luokka, jotta voidaan kirjoittaa tiedostoon
    class Habitat
    {
        private int habitatTrash;
        public int Trash { get { return habitatTrash; } set {; } }

        private int habitatCleanliness;
        public int Cleanliness { get { return habitatCleanliness; } set {; } }

        public void SetDefault()
        {
            habitatTrash = 0;               // Alustetaan arvot
            habitatCleanliness = 100;
        }

        public void RemoveTrash()
        {
            habitatTrash -= 1;
        }

        public void AddTrash()
        {
            habitatTrash += 1;
        }

        public void GetMessy()
        {
            habitatCleanliness -= 1;
        }

        public void Clean()
        {
            habitatCleanliness += 1;
        }
    }
}
