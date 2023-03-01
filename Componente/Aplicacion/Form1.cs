using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Aplicacion
{
    public partial class Form1 : Form
    {
        Bitmap[] imagenes;
        int cont = 0;
        string path;

        public Form1()
        {
            InitializeComponent();

            for(int i = 1; i <= 20; i++)
            {
                comboBox1.Items.Add(i.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                path = folderBrowserDialog1.SelectedPath;
                label1.Text = "";
            }

            try
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(path);

                FileInfo[] imagenesJPG = directoryInfo.GetFiles("*.jpg");
                FileInfo[] imagenesPNG = directoryInfo.GetFiles("*.png");

                FileInfo[] rutas = imagenesJPG.Concat(imagenesPNG).ToArray();

                imagenes = new Bitmap[rutas.Length];

                for (int i = 0; i < rutas.Length; i++)
                {
                    try
                    {
                        imagenes[i] = new Bitmap(rutas[i].FullName);
                    }
                    catch (ArgumentException ex)
                    {
                        label1.Text = "No es posible visualizar la imagen " + i;
                    }
                }

                if (reproductor2.TextBtn == "Play" || reproductor2.TextBtn == "Iniciar")
                {
                    pictureBox1.Image = imagenes[0];
                }

            }catch (Exception exc) when (exc is ArgumentNullException || exc is NullReferenceException || exc is IndexOutOfRangeException) {
                label1.Text = exc.Message;
            }
        }

        private void reproductor2_PlayClick_1(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                timer1.Stop();
            }
            else
            {
                timer1.Start();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            timer1.Interval = Convert.ToInt32(comboBox1.SelectedItem)*1000;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(path != null)
            {
                cont++;
                try
                {
                    pictureBox1.Image = imagenes[cont];

                }catch(IndexOutOfRangeException)
                {
                    pictureBox1.Image = imagenes[0];
                    cont = 0;
                }
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            reproductor2.SS++;
        }

        private void reproductor2_DesbordaTiempo(object sender, EventArgs e)
        {
            reproductor2.MM++;
        }
    }
}
