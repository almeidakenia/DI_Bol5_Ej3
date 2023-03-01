using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.SqlServer.Server;

namespace Componente
{
    public partial class Reproductor : UserControl
    {

        public Reproductor()
        {
            InitializeComponent();
            lbl.Text = String.Format("{0,2:D2}:{1,2:D2}", MM, SS);
        }   

        public string TextBtn
        {
            set
            {
                btn.Text = value;
            }
            get
            {
                return btn.Text;
            }
        }

        private int mm = 0;
        [Category("Propiedades")]
        [Description("Minutos")]
        public int MM
        {
            set
            {
                if(value >= 0)
                {
                    if(value <= 59)
                    {
                        mm = value;
                    }
                    else
                    {
                        mm = 0;
                    }

                    lbl.Text = String.Format("{0,2:D2}:{1,2:D2}", mm, ss);
                }
                else
                {
                    throw new ArgumentException();
                }
            }
            get
            {
                return mm;
            }
        }

        private int ss = 0;
        [Category("Propiedades")]
        [Description("Segundos")]
        public int SS
        {
            set
            {
                if(value >= 0)
                {
                    if(value <= 59)
                    {
                        ss = value;
                    }
                    else
                    {
                        ss = value % 60;

                        OnDesbordaTiempo(EventArgs.Empty);
                    }
                    lbl.Text = String.Format("{0,2:D2}:{1,2:D2}", mm, ss);
                }
                else
                {
                    throw new ArgumentException();
                }
            }
            get
            {
                return ss;
            }
        }

        [Category("Categoria")]
        [Description("Se lanza cuando ...")]
        public event System.EventHandler PlayClick;
        protected virtual void OnPlayClick(EventArgs e)
        {
            if (PlayClick != null)
            {
                PlayClick(this, e);
            }
        }

        [Category("Categoria")]
        [Description("Se lanza para actualizar los minutos")]
        public event System.EventHandler DesbordaTiempo;
        protected virtual void OnDesbordaTiempo(EventArgs e)
        {
            if(DesbordaTiempo != null)
            {
                DesbordaTiempo(this, e);
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            if(btn.Text == "Play" || btn.Text == "Iniciar")
            {
                btn.Text = "Pause";
            }
            else
            {
                btn.Text = "Play";
            }

            OnPlayClick(EventArgs.Empty);
        }
    }
}
