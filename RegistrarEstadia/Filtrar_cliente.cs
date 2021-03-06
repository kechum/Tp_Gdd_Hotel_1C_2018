﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrbaHotel.RegistrarEstadia
{
    public partial class Filtrar_cliente : Form
    {
        const Int32 VACIO = 0;
        int idClienteSeleccionado;
        string mailClienteSeleccionado;
        string nombreClienteSeleccionado;
        string apellidoClienteSeleccionado;
        int numeroDocClienteSeleccionado;
        int telefonoClienteSeleccionado;
        string paisClienteSeleccionado;
        string nacionalidadClienteSeleccionado;
        DateTime fechaNacimientoClienteSeleccionado;
        String estadoClienteSeleccionado;

        private RegistrarEstadia.Check_In controler;

        public Filtrar_cliente(RegistrarEstadia.Check_In controlerCheckIn)
        {
            controler = controlerCheckIn;
            InitializeComponent();
            configuarComboBoxTipoDoc();
        }

        public void configuarComboBoxTipoDoc()
        {
            this.listadoTipoIdentificacion.ValueMember = "Objeto";
            this.listadoTipoIdentificacion.DisplayMember = "Descripcion";
            this.listadoTipoIdentificacion.DataSource = Repositorios.RepoTipoDocumento.getInstancia().getTipoDocumentos();
            this.listadoTipoIdentificacion.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            idClienteSeleccionado = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["IdCliente"].Value);
            mailClienteSeleccionado = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["Email"].Value);
            nombreClienteSeleccionado = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["Nombre"].Value);
            apellidoClienteSeleccionado = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["Apellido"].Value);
            numeroDocClienteSeleccionado = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["NumDoc"].Value);
            telefonoClienteSeleccionado = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Telefono"].Value);
            paisClienteSeleccionado = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["PaisOrigen"].Value);
            nacionalidadClienteSeleccionado = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["Nacionalidad"].Value);
            fechaNacimientoClienteSeleccionado = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells["FechaNacimiento"].Value);
            estadoClienteSeleccionado = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["Estado"].Value);
            
        }



        private void lblFiltrar_Click(object sender, EventArgs e)
        {
            Model.TipoDocumento tipoDoc = (Model.TipoDocumento)listadoTipoIdentificacion.SelectedValue;

            dataGridView1.DataSource = Repositorios.Repo_Cliente.getInstancia().getTablaClientesFiltradosActivos(filtroNombre.Text, filtroApellido.Text, filtroMail.Text, tipoDoc.id, filtroNumeroIdentificacion.Value);
        }

        public void vaciarDataGrid()
        {
            Model.TipoDocumento tipoDoc = (Model.TipoDocumento)listadoTipoIdentificacion.SelectedValue;
            dataGridView1.DataSource = Repositorios.Repo_Cliente.getInstancia().getTablaClientesFiltradosActivos(filtroNombre.Text, filtroApellido.Text, filtroMail.Text, tipoDoc.id, filtroNumeroIdentificacion.Value);
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {

        }

        private void boton_agregar_Click(object sender, EventArgs e)
        {
            
            Model.Cliente cliente = new Model.Cliente();
            cliente.apellido = apellidoClienteSeleccionado;
            cliente.nombre = nombreClienteSeleccionado;
            cliente.id = idClienteSeleccionado;
            cliente.mail = mailClienteSeleccionado;
            cliente.nacionalidad = nacionalidadClienteSeleccionado;
            cliente.fechaNac = fechaNacimientoClienteSeleccionado;
            cliente.numDoc = numeroDocClienteSeleccionado;

            controler.clienteFiltrado = cliente;

            this.Hide();
            controler.Show();
            this.Close();
        }
    }
}
