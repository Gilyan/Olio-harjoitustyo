/* **********************************************************
Oliogotchin animaatiosivu.

Luotu 27.3.2017

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
using WpfAnimatedGif;

namespace Oliogotchi
{
    class Animation
    {
        // tänne tulee kaikki animaatioihin liittyvät jutut
    }

    class CreatureAnimation
    {
        private int i { get; set; }
        public string FrameLocation { get; set; }
        // https://programmingwithkinect.wordpress.com/2013/04/09/sprite-animation-in-wpf-c/

        public void CreatureFetch(Creature olio)
        {
            for (i = 0; i < 4; i++)
            {
                string FrameLocation = olio.Image + "Run/" + i + ".png"; // kuvan polku

            }
        }
    }
}
