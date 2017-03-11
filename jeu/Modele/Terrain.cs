using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jeu.Modele
{
    class Terrain
    {
        /* TERRAIN */
        public static Terrain OCEAN = new Terrain("ocean", 12, RessourcesDico.Ocean_dispo(),
            RessourcesDico.Ocean_possible());
        public static Terrain PLAINE = new Terrain("plaine", 1, RessourcesDico.Plaine_dispo(),
            RessourcesDico.Plaine_possible());
        public static Terrain FORET = new Terrain("foret", 2, RessourcesDico.Foret_dispo(),
            RessourcesDico.Foret_possible());
        public static Terrain MONTAGNE = new Terrain("montagne", 2, RessourcesDico.Montagne_dispo(),
            RessourcesDico.Montagne_possible());
        public static Terrain PLAGE = new Terrain("plage", 2, RessourcesDico.Plage_dispo(),
            RessourcesDico.Plage_possible());

        /* Constantes */
        private int TRANCHE_CONSTANTE = 10;

        /* Attributs */
        private String nom;
        public String Nom
        {
            get { return nom; }
        }
        private IDictionary<Ressource, int> ressources_disponnible;
        public IDictionary<Ressource, int> Ressources_disponnible
        {
            get { return ressources_disponnible; }
        }
        private IDictionary<Ressource, int> resources_possible;
        public IDictionary<Ressource, int> Resources_possible
        {
            get { return resources_possible; }
        }
        private int difficulte;     // difficulte à se déplacer sur le terrain
        public int Difficulte
        {
            get { return difficulte; }
        }

        /* Constructeurs */
        private Terrain(String nom, int difficulte)
        {
            this.nom = nom;
            this.difficulte = difficulte;
            this.resources_possible = new Dictionary<Ressource, int>();
            this.ressources_disponnible = new Dictionary<Ressource, int>();
        }
        private Terrain(String nom, int difficulte, Dictionary<Ressource, int> dispo, Dictionary<Ressource, int> possible)
        {
            this.nom = nom;
            this.difficulte = difficulte;
            this.resources_possible = possible;
            this.ressources_disponnible = dispo;
        }

        /* Ajout au collection */
        private void AddRessourceDispo(Ressource r, int max)
        {
            ressources_disponnible.Add(r, max);
        }
        private void AddRessourcePossible(Ressource r, int max)
        {
            ressources_disponnible.Add(r, max);
        }

        /* Génère le dictionnaire des ressources et de leur montant pour une cellule */
        public IDictionary<Ressource, int> GenererDictionnaire(Random r)
        {
            IDictionary<Ressource, int> result = new Dictionary<Ressource, int>();
            foreach (KeyValuePair<Ressource, int> p in ressources_disponnible)
            {
                int nb = (p.Value / TRANCHE_CONSTANTE) * r.Next(TRANCHE_CONSTANTE);
                result.Add(p.Key, nb);
            }
            foreach (KeyValuePair<Ressource, int> p in Resources_possible)
            {
                double l = r.NextDouble();
                if (l <= p.Key.Ratio)
                {
                    int nb = (p.Value / TRANCHE_CONSTANTE) * r.Next(TRANCHE_CONSTANTE);
                    result.Add(p.Key, nb);
                }
            }
            return result;
        }
        public IDictionary<Ressource, int> GenererDictionnaire()
        {
            return GenererDictionnaire(null);
        }

        public static Terrain get_Terrain(int i)
        {
            switch (i)
            {
                case 0: return OCEAN;
                case 1: return PLAINE;
                case 2:return FORET;
                case 3: return MONTAGNE;
                case 4: return PLAGE;
                default: return FORET;
            }
        }
    }
}
