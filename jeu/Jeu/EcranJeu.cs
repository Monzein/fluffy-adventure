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
        private Image[,] plateau;
        private int HAUTEUR = 22*1, LARGEUR = 46*1;
        private int WIDTH_IMG = 25, HEIGHT_IMG = 25;
        private int SPACE_TILE = 2;
        private int SEED = 0;

        public EcranJeu()
        {
            InitializeComponent();

            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            //modele = new Grille(LONGUEUR, LARGEUR, 0);
            modele = Createur_grille.createur_1(HAUTEUR, LARGEUR, SEED);
            plateau = new Image[HAUTEUR, LARGEUR];

            panel1.Height = (int)(this.Height * 0.2 - 5);
            panel1.Width = (int)(this.Width - 10);
            panel1.Location = new Point(5, (int)(this.Height * 0.8));

            pictureBox1.Height = (int)(this.Height * 0.8 - 10);
            pictureBox1.Width = (int)(this.Width - 10);
            pictureBox1.Location = new Point(5, 5);

            Tile_size();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            for (int i = 0; i < HAUTEUR; i++)
            {
                for (int j = 0; j < LARGEUR; j++)
                {
                    Image original = new Bitmap("../../images/terrains/" + modele.GetTerrainName(i, j) + ".png");
                    Image image = new Bitmap(original, WIDTH_IMG, HEIGHT_IMG);

                    plateau[i, j] = image;
                    imageList1.Images.Add(image);
                    Point pt = new Point(2 * SPACE_TILE + j * (image.Width + SPACE_TILE), 2* SPACE_TILE + i * (image.Height + SPACE_TILE));

                    g.DrawImage(image, pt);

                }
            }
        }

        private void Tile_size()
        {
            WIDTH_IMG = (pictureBox1.Width) / LARGEUR - SPACE_TILE;
            HEIGHT_IMG = (pictureBox1.Height) / HAUTEUR - SPACE_TILE;

            WIDTH_IMG = Math.Min(WIDTH_IMG, HEIGHT_IMG);
            HEIGHT_IMG = WIDTH_IMG;
        }

        private void EcranJeu_Load(object sender, EventArgs e)
        {

        }
    }
}
