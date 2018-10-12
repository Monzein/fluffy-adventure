using System;
using System.Collections.Generic;

namespace FluffyServ.Model
{
    class Terrain
    {
        /* TERRAIN */
        public static Terrain OCEAN = new Terrain("ocean", 12, Ressources.OceanAvailable(),
            Ressources.OceanPossible());
        public static Terrain PLAIN = new Terrain("plaine", 1, Ressources.PlainAvailable(),
            Ressources.PlainPossible());
        public static Terrain FOREST = new Terrain("foret", 2, Ressources.ForestAvailable(),
            Ressources.ForestPossible());
        public static Terrain MOUNTAIN = new Terrain("montagne", 2, Ressources.MountainAvailable(),
            Ressources.MountainPossible());
        public static Terrain BEACH = new Terrain("plage", 2, Ressources.BeachAvailable(),
            Ressources.BeachPossible());
        public static Terrain VOID = new Terrain("void", -1, null, null);

        /* Constantes */
        private int DEFAULT_SLICE = 10;

        /* Attributs */
        private string name;
        public string Name
        {
            get { return name; }
        }
        private IDictionary<Ressource, int> resourcesAvailable;
        public IDictionary<Ressource, int>ResourcesAvailable
        {
            get { return resourcesAvailable; }
        }
        private IDictionary<Ressource, int> resourcesPossible;
        public IDictionary<Ressource, int> ResourcesPossible
        {
            get { return resourcesPossible; }
        }
        private int difficulty;     // difficulte à se déplacer sur le terrain
        public int Difficulty
        {
            get { return difficulty; }
        }

        /* Constructeurs */
        private Terrain(string name, int difficulty)
        {
            this.name = name;
            this.difficulty = difficulty;
            this.resourcesPossible = new Dictionary<Ressource, int>();
            this.resourcesAvailable = new Dictionary<Ressource, int>();
        }
        private Terrain(string name, int difficulty, Dictionary<Ressource, int> available, Dictionary<Ressource, int> possible)
        {
            this.name = name;
            this.difficulty = difficulty;
            this.resourcesPossible = possible;
            this.resourcesAvailable = available;
        }

        /* Ajout au collection */
        private void AddRessourceAvailable(Ressource r, int max)
        {
            resourcesAvailable.Add(r, max);
        }
        private void AddRessourcePossible(Ressource r, int max)
        {
            resourcesAvailable.Add(r, max);
        }

        /* Génère le dictionnaire des ressources et de leur montant pour une cellule */
        public IDictionary<Ressource, int> GenerateDictionary(Random r = null)
        {
            IDictionary<Ressource, int> result = new Dictionary<Ressource, int>();
            foreach (KeyValuePair<Ressource, int> p in resourcesAvailable)
            {
                int nb = (p.Value / DEFAULT_SLICE) * r.Next(DEFAULT_SLICE);
                result.Add(p.Key, nb);
            }
            foreach (KeyValuePair<Ressource, int> p in resourcesPossible)
            {
                double l = r.NextDouble();
                if (l <= p.Key.Ratio)
                {
                    int nb = (p.Value / DEFAULT_SLICE) * r.Next(DEFAULT_SLICE);
                    result.Add(p.Key, nb);
                }
            }
            return result;
        }

        public static Terrain GetTerrain(int i)
        {
            switch (i)
            {
                case 0: return OCEAN;
                case 1: return PLAIN;
                case 2:return FOREST;
                case 3: return MOUNTAIN;
                case 4: return BEACH;
                default: return FOREST;
            }
        }
    }
}
