﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrbaHotel.AbmHotel
{
    public partial class Abm_hotel : Form
    {
        public Abm_hotel()
        {
            InitializeComponent();
        }

        private void button_volver_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Login.SeleccionarFuncionalidad_admin().ShowDialog();
            this.Close();
        }

        private void boton_crear_hotel_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Crear_Hotel().ShowDialog();
            this.Close();
        }

        private void boton_eliminar_Click(object sender, EventArgs e)
        {
            this.Hide();
            new List_Select_Hotel_Inhabilitar().ShowDialog();
            this.Close();
        }

        private void boton_listar_Click(object sender, EventArgs e)
        {
            this.Hide();
            new AbmHotel.List_Select_Hotel_Listar().ShowDialog();
            this.Close();
        }

        private void button_Listado_seleccion_sucursales_Click(object sender, EventArgs e)
        {
            this.Hide();
            new AbmHotel.List_Select_Hotel_Modificar().ShowDialog();
            this.Close();
        }
    }
}
