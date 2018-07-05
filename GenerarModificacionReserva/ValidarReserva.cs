﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrbaHotel.GenerarModificacionReserva
{
    public partial class ValidarReserva : Form
    {
        public ValidarReserva()
        {
            InitializeComponent();
        }

        private void btnValidar_Click(object sender, EventArgs e)
        {
            int retorno = Repositorios.Repo_Reserva.getInstancia().comprobarSiReservaNoPasoFecha(numReserva.Value);
            
            if (retorno == 1)
            {
                new Generar_Reserva_Guest().ShowDialog();
            }
            else if (retorno == 2)
            {
                MessageBox.Show("Ya no se puede modificar la reserva, es tiempo maximo es hasta un dia antes de la fecha elegida", "Fecha de modificacion de Reserva superada", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("La reserva ingresada no existe Reserva", "No existe Reserva", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
