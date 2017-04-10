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
    class Creature // : INotifyPropertyChanged
    {
        private int happiness;
        public int Happiness
        {
            get { return happiness; }
            set
            {
                if (value >= 0 && value <= 100) { happiness = value; }
                else if (value < 0)
                {
                    happiness = 0;
                }
                else happiness = 100;
            }
        }
        public int Age { get; set; }
        public int Hunger { get; set; }
        public string Image { get; set; }
        public int Cleanliness { get; set; }

        public void Evolve()
        {

        }

        public void Die()
        {

        }

        public void ChangeName()
        {

        }

        //public event PropertyChangedEventHandler PropertyChanged;       // Event
        //private void RaisePropertyChanged(string property)              // Metodi
        //{
        //    if (PropertyChanged != null)
        //    {
        //        PropertyChanged(this, new PropertyChangedEventArgs(property));
        //    }
        //}
    }
}
