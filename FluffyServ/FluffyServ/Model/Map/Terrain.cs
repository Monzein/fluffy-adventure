using FluffyServ.Model.GameItems;
using System;
using System.Collections.Generic;

namespace FluffyServ.Model
{
    /// <summary>
    /// The terrain determine the resources available, the movement type an the energy require to cross a cell.
    /// TODO: Fill it from a config file.
    /// </summary>
    public class Terrain
    {
        /* TERRAIN */
        public static Terrain OCEAN = new Terrain("ocean", 12, Resources.OceanAvailable(),
            Resources.OceanPossible());
        public static Terrain PLAIN = new Terrain("plaine", 1, Resources.PlainAvailable(),
            Resources.PlainPossible());
        public static Terrain FOREST = new Terrain("foret", 2, Resources.ForestAvailable(),
            Resources.ForestPossible());
        public static Terrain MOUNTAIN = new Terrain("montagne", 4, Resources.MountainAvailable(),
            Resources.MountainPossible());
        public static Terrain BEACH = new Terrain("plage", 1, Resources.BeachAvailable(),
            Resources.BeachPossible());
        public static Terrain DESOLATION = new Terrain("desolation", 1, Resources.DesolationAvailable(),
            Resources.DesolationPossible());
        public static Terrain VOID = new Terrain("void", -1, null, null);

        /* Constantes */
        private int DEFAULT_SLICE = 10;

        /* Attributs */
        private string name;
        public string Name
        {
            get { return name; }
        }
        private IDictionary<Resource, int> resourcesAvailable;
        public IDictionary<Resource, int> ResourcesAvailable
        {
            get { return resourcesAvailable; }
        }
        private IDictionary<Resource, int> resourcesPossible;
        public IDictionary<Resource, int> ResourcesPossible
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
            this.resourcesPossible = new Dictionary<Resource, int>();
            this.resourcesAvailable = new Dictionary<Resource, int>();
        }
        private Terrain(string name, int difficulty, Dictionary<Resource, int> available, Dictionary<Resource, int> possible)
        {
            this.name = name;
            this.difficulty = difficulty;
            this.resourcesPossible = possible;
            this.resourcesAvailable = available;
        }

        /* Ajout au collection */
        private void AddRessourceAvailable(Resource r, int max)
        {
            resourcesAvailable.Add(r, max);
        }
        private void AddRessourcePossible(Resource r, int max)
        {
            resourcesAvailable.Add(r, max);
        }

        /* Génère le dictionnaire des ressources et de leur montant pour une cellule */
        internal IDictionary<Resource, int> GenerateDictionary(Random r = null)
        {
            IDictionary<Resource, int> result = new Dictionary<Resource, int>();
            foreach (KeyValuePair<Resource, int> p in resourcesAvailable)
            {
                int nb = (p.Value / DEFAULT_SLICE) * r.Next(DEFAULT_SLICE);
                if (nb > 0)
                {
                    result.Add(p.Key, nb);
                }
            }
            foreach (KeyValuePair<Resource, int> p in resourcesPossible)
            {
                double l = r.NextDouble();
                if (l <= p.Key.Ratio)
                {
                    int nb = (p.Value / DEFAULT_SLICE) * r.Next(DEFAULT_SLICE);
                    if (nb > 0)
                    {
                        result.Add(p.Key, nb);
                    }
                }
            }
            return result;
        }

        public static Terrain GetTerrain(int i)
        {
            switch (i)
            {
                case 0: return OCEAN;
                case 1: return DESOLATION;
                case 2: return PLAIN;
                case 3: return FOREST;
                case 4: return MOUNTAIN;
                case 5: return BEACH;
                default: return FOREST;
            }
        }
    }
}
