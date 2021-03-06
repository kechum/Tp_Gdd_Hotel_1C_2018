﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrbaHotel.AbmCliente
{
    public partial class Abm_Cliente : Form
    {
        public Abm_Cliente()
        {
            InitializeComponent();
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Crear_Cliente().ShowDialog();
            this.Close();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            this.Hide();
            new List_Select_Cliente_Modificar().ShowDialog();
            this.Close();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            this.Hide();
            new List_Select_Cliente_Eliminar().ShowDialog();
            this.Close();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Login.SeleccionarFuncionalidad().ShowDialog();
            this.Close();
        }
    }
}
