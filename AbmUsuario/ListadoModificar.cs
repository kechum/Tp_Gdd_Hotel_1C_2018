﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrbaHotel.AbmUsuario
{
    public partial class ListadoModificar : Form
    {
        String username;

        public ListadoModificar()
        {
            InitializeComponent();
        }
      
        private void lblFiltrar_Click(object sender, EventArgs e)
        {
            dataGridModificar.DataSource = Repositorios.Repo_usuario.getInstancia().getTablaUsuariosFiltradosConInactivos(filtroNombre.Text, filtroApellido.Text, filtroUsername.Text, numericUpDown1.Value);
        }

        private void dataGridModificar_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            username = Convert.ToString(dataGridModificar.Rows[e.RowIndex].Cells["Username"].Value);
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (username != null)
            {
                MessageBox.Show("Por favor, seleccione de la grilla un cliente a modificar clickeando en cualquiera de sus atributos", "Seleccione cliente", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
               // new AbmCliente.Modificar_Cliente(idClienteSeleccionado, mailClienteSeleccionado).ShowDialog();
            }
        }



        /*
         * Username nvarchar(255) PRIMARY KEY,
            Pass nvarchar(255)  NOT NULL,
            Direccion int FOREIGN KEY REFERENCES TRAEME_LA_COPA_MESSI.Direccion(IdDir) NULL,
            Nombre nvarchar(255) NULL,
            Apellido nvarchar(255) NULL,
            TipoDoc int FOREIGN KEY REFERENCES TRAEME_LA_COPA_MESSI.TipoDoc(IdTipo),
            NroDocumento numeric(18,0) DEFAULT 0,
            Email nvarchar(255) UNIQUE NULL,
            Telefono numeric(18,0) DEFAULT 0,
            FechaNacimiento datetime NULL,
            LogsFallidos int NOT NULL DEFAULT 0,
            Estado bit DEFAULT 0
         * 
         * 
         * */


    }
}