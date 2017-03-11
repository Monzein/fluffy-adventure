using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jeu.Modele
{
    class RessourcesDico
    {
        /* OCEAN */
        public static Dictionary<Ressource, int> Ocean_dispo()
        {
            Dictionary<Ressource, int> result = new Dictionary<Ressource, int>();

            return result;
        }
        public static Dictionary<Ressource, int> Ocean_possible()
        {
            Dictionary<Ressource, int> result = new Dictionary<Ressource, int>();

            return result;
        }

        /* GRASSLAND */
        public static Dictionary<Ressource, int> Plaine_dispo()
        {
            Dictionary<Ressource, int> result = new Dictionary<Ressource, int>();
            result.Add(Ressource.FEUILLAGE, 50);
            return result;
        }
        public static Dictionary<Ressource, int> Plaine_possible()
        {
            Dictionary<Ressource, int> result = new Dictionary<Ressource, int>();
            result.Add(Ressource.FRUIT, 50);
            return result;
        }

        /* WOODS */
        public static Dictionary<Ressource, int> Foret_dispo()
        {
            Dictionary<Ressource, int> result = new Dictionary<Ressource, int>();
            result.Add(Ressource.BOIS, 250);
            result.Add(Ressource.FEUILLAGE, 400);
            return result;
        }
        public static Dictionary<Ressource, int> Foret_possible()
        {
            Dictionary<Ressource, int> result = new Dictionary<Ressource, int>();
            result.Add(Ressource.FRUIT, 100);
            return result;
        }

        /* MONTAGNE */
        public static Dictionary<Ressource, int> Montagne_dispo()
        {
            Dictionary<Ressource, int> result = new Dictionary<Ressource, int>();

            return result;
        }
        public static Dictionary<Ressource, int> Montagne_possible()
        {
            Dictionary<Ressource, int> result = new Dictionary<Ressource, int>();

            return result;
        }

        /* PLAGE */
        public static Dictionary<Ressource, int> Plage_dispo()
        {
            Dictionary<Ressource, int> result = new Dictionary<Ressource, int>();

            return result;
        }
        public static Dictionary<Ressource, int> Plage_possible()
        {
            Dictionary<Ressource, int> result = new Dictionary<Ressource, int>();

            return result;
        }
    }
}
