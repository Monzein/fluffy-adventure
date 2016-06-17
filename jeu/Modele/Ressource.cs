using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jeu.Modele
{
    class Ressource
    {
        /* RESSOURCES */
        public static Ressource BOIS = new Ressource("bois", 1, 2, 1, 3);
        public static Ressource PIERRE = new Ressource("pierre", 1, 4, 1, 6);
        public static Ressource FER = new Ressource("fer", 0.5, 6, 1, 10);
        public static Ressource FEUILLAGE = new Ressource("feuilllage", 1, 1, 1, 1);
        public static Ressource SABLE = new Ressource("sable", 0.8, 1, 1, 8);
        public static Ressource FRUIT = new Ressource("fruit", 0.2, 1, 1, 1);

        /* Attributs */
        private String nom;
        public String Nom
        {
            get { return nom; }
        }
        private double ratio;        // Les chances d'en trouver
        public double Ratio
        {
            get { return ratio; }
        }
        private double difficulte;   // Les chances d'en recuperer
        public double Difficulte
        {
            get { return difficulte; }
        }
        private double taille;
        public double Taille
        {
            get { return taille; }
        }
        private double masse;
        public double Masse
        {
            get { return masse; }
        }

        /* Constructeurs */
        private Ressource(String nom, double ratio, double difficulte, double taille, double masse)
        {
            this.nom = nom;
            this.ratio = ratio;
            this.difficulte = difficulte;
            this.taille = taille;
            this.masse = masse;
        }
    }
}
