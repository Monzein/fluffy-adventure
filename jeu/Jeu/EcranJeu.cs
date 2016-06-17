using jeu.Modele;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace jeu.Jeu
{
    public partial class EcranJeu : Form
    {
        private Grille modele;
        private PictureBox[,] plateau;
        private int LONGUEUR = 20, LARGEUR = 20;

        public EcranJeu()
        {
            InitializeComponent();
            modele = new Grille(LONGUEUR, LARGEUR, 0);
            plateau = new PictureBox[LONGUEUR, LARGEUR];
        }

        private void EcranJeu_Load(object sender, EventArgs e)
        {
            Init_grille();
        }

        private void Init_grille()
        {
            
            for (int i = 0; i < LONGUEUR; i++)
            {
                for (int j = 0; j < LARGEUR; j++)
                {
                    PictureBox image = new PictureBox();
                    image.ImageLocation = "../../images/terrains/"+modele.GetTerrainName(i,j)+".png";
                    plateau[i, j] = image;
                }
            }
        }
    }
}
