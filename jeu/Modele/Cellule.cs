using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jeu.Modele
{
    class Cellule
    {
        /* Attributs */
        private Terrain type;
        public Terrain Type
        {
            get { return type; }
            set { type = Type; }
        }
        private IDictionary<Ressource, int> ressources;
        public IDictionary<Ressource, int> Ressources
        {
            get { return ressources; }
        }
        /* Constructeurs */
        public Cellule(Terrain type)
        {
            this.type = type;
            this.ressources = null;
        }
        public Cellule(Terrain type, int seed)
        {
            this.type = type;
            this.ressources = null;
        }

        public void init(Random r)
        {
            this.ressources = type.GenererDictionnaire(r);
        }
    }
}
