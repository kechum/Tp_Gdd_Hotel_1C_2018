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
    public partial class Generar_Reserva_Guest : Form, GenerarReserva
    {
        Model.Hotel hotelSeleccionado;
        Model.TipoHabitacion tipoHabitacionSeleccionado;

        public Generar_Reserva_Guest()
        {
            InitializeComponent();
            configuarComboBoxHotel();
            configuarComboBoxTipoHabitacion();
            hotelSeleccionado = (Model.Hotel)listadoHoteles.SelectedValue;
            tipoHabitacionSeleccionado = (Model.TipoHabitacion)listadoTipoHabitacion.SelectedValue;
        }

        private void btnRegimen_Click(object sender, EventArgs e)
        {
            new GenerarModificacionReserva.ListadoRegimenes(this, hotelSeleccionado.idHotel).ShowDialog();
        }

        public void configuarComboBoxHotel()
        {
            this.listadoHoteles.ValueMember = "Objeto";
            this.listadoHoteles.DisplayMember = "IdHotel";
            this.listadoHoteles.DataSource = Repositorios.Repo_hotel.getInstancia().getHoteles();
            this.listadoHoteles.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        public void configuarComboBoxTipoHabitacion()
        {
            this.listadoTipoHabitacion.ValueMember = "Objeto";
            this.listadoTipoHabitacion.DisplayMember = "Descripcion";
            this.listadoTipoHabitacion.DataSource = Repositorios.Repo_habitacion.getInstancia().getTiposHabitaciones();
            this.listadoTipoHabitacion.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        public void actualizarRegimen(Model.Regimen regimen)
        {
            this.actualizarTbRegimen(regimen.descripcion);
            
            decimal costoPorDia = (regimen.precioBase * tipoHabitacionSeleccionado.porcentual) + hotelSeleccionado.porcEstrella;
            this.actualizarCostoDeHabitacion(costoPorDia.ToString());
        }

        private void actualizarTbRegimen(String regimenDescripcion)
        {
            tbRegimen.Text = regimenDescripcion;
        }

        private void actualizarCostoDeHabitacion(String costo)
        {
            lblCostoHabitacion.Text = costo;
        }


    }
}
