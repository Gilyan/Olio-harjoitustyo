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
        public int Trash { get; set; }
        public int Cleanliness { get; set; }

        public void RemoveTrash()
        {

        }

        public void AddTrash()
        {

        }
    }
}
