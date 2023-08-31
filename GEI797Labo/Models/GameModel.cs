using GEI797Labo.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEI797Labo.Models
{
    internal class GameModel
    {
        private IController controller;
        public GameModel(IController c) {

            controller = c;

        }
        
        private int[,] labyrinth = {
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}, // 0 = Aucune image (zone de déplacement)
                {1, 0, 0, 4, 0, 0, 0, 0, 0, 0, 1}, // 1 = Afficher un mur
                {1, 0, 1, 1, 1, 0, 1, 1, 1, 0, 1},  // 2 = Afficher une porte
                {1, 0, 0, 0, 0, 0, 1, 5, 1, 0, 1},  // 3 = Afficher Slimus
                {1, 0, 1, 0, 1, 1, 1, 2, 1, 0, 1},  // 4 = Afficher une gemme
                {1, 0, 1, 0, 1, 4, 0, 0, 0, 0, 1},  // 5 = Afficher un mini-slime
                {1, 0, 1, 0, 1, 1, 0, 1, 1, 0, 1},
                {1, 0, 0, 0, 3, 1, 0, 0, 4, 0, 1},
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                };

        public int[,] GetLabyrinth()
        {
            return labyrinth;
        }

    }
}
