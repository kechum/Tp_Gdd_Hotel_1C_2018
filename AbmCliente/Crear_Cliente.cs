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
    public partial class Crear_Cliente : Form
    {
        public Crear_Cliente()
        {
            InitializeComponent();
        }

        //Falta hacer validacion para que no meta mail repetido
        private void btnCrear_Click(object sender, EventArgs e) //Configurar ComboBox
        {
            if (
                string.IsNullOrWhiteSpace(tbMail.Text) || 
                string.IsNullOrEmpty(tbNombre.Text) || 
                string.IsNullOrEmpty(tbApellido.Text) || 
                numericNumeroIdentificacion.Value == decimal.Zero || 
                numericTelefono.Value == decimal.Zero || 
                string.IsNullOrWhiteSpace(tbPaisOrigen.Text) || 
                string.IsNullOrWhiteSpace(tbNacionalidad.Text) || 
                string.IsNullOrEmpty(textBox1.Text) || 
                string.IsNullOrEmpty(tbCalle.Text) || 
                numericNumero.Value == decimal.Zero || 
                numericPiso.Value == decimal.Zero || 
                string.IsNullOrWhiteSpace(tbDpto.Text) || 
                string.IsNullOrEmpty(tbLocalidad.Text) || 
                string.IsNullOrWhiteSpace(tbPais.Text))
            {
                MessageBox.Show("Por favor complete todos los campos", "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (Repositorios.Repo_Cliente.getInstancia().validarMail(tbMail.Text) == 1)
            {
                MessageBox.Show("Por favor ingrese un mail que no registrado", "Mail ya existente", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Repositorios.Repo_Cliente.getInstancia().crearCliente(
                    tbMail.Text,
                    tbNombre.Text,
                    tbApellido.Text,
                    "", //combobox
                    numericNumeroIdentificacion.Value,
                    numericTelefono.Value,
                    tbPaisOrigen.Text,
                    tbNacionalidad.Text,
                    dtFechaNacimiento.Value,
                    textBox1.Text,
                    tbCalle.Text,
                    numericNumero.Value,
                    numericPiso.Value,
                    tbDpto.Text,
                    tbLocalidad.Text,
                    tbPais.Text);

                MessageBox.Show("Cliente creado correctamente");
            }

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
                    tbMail.Text = "";
                    tbNombre.Text  = "";
                    tbApellido.Text  = "";
                    //combobox
                    numericNumeroIdentificacion.Value = decimal.Zero;
                    numericTelefono.Value = decimal.Zero;
                    tbPaisOrigen.Text = "";
                    tbNacionalidad.Text = "";
                    textBox1.Text = "";
                    tbCalle.Text = "";
                    numericNumero.Value = decimal.Zero;
                    numericPiso.Value = decimal.Zero;
                    tbDpto.Text = "";
                    tbLocalidad.Text = "";
                    tbPais.Text = "";
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            new AbmCliente.Abm_Cliente().ShowDialog();
        }

 
    }
}
