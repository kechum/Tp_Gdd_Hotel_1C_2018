﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace FrbaHotel.Repositorios
{
    class Repo_Reserva
    {
        public static Repo_Reserva instancia;
        public Utils.DBhelper DBhelper = Utils.DBhelper.getInstancia();
        public static Repo_Reserva getInstancia()
        {

            if (instancia != null)
            {
                return instancia;
            }
            else
            {
                instancia = new Repo_Reserva();
                return instancia;
            }
        }

        public int validarCancelacion(Model.ReservaCancelada reserva)
        {
            DateTime fechaReserva;
            DBhelper.crearConexion();
            DBhelper.abrirConexion();
            SqlCommand cmd = DBhelper.crearCommand("TRAEME_LA_COPA_MESSI.validarCancelacion");
            cmd.Parameters.Add("@idReserva", SqlDbType.Int).Value = reserva.numeroreserva;
            DataTable fechaRes = DBhelper.obtenerTabla(cmd);
            DBhelper.cerrarConexion();


            if (fechaRes.Rows.Count==0)
            {
                return 1;// No existe tal reserva
            }

            foreach (DataRow row in fechaRes.Rows)
            {
                fechaReserva = ((DateTime)row["FechaReserva"]);
                
                if ((DateTime.Compare(reserva.fechaCancelacion, fechaReserva) >= 0))
                {
                    return 2; // La cancelacion es demasiado cercana a la fecha de la reserva
                }
                
            }

            return 0; // todo correcto
                               
        }

        public void cancelarReserva(Model.ReservaCancelada reserva){
            DBhelper.crearConexion();
            DBhelper.abrirConexion();
            SqlCommand cmd = DBhelper.crearCommand("TRAEME_LA_COPA_MESSI.cancelarReserva");
            cmd.Parameters.Add("@idReserva", SqlDbType.Int).Value = reserva.numeroreserva;
            cmd.Parameters.Add("@fechaDeCancelacion", SqlDbType.DateTime).Value = reserva.fechaCancelacion;
            cmd.Parameters.Add("@motivo", SqlDbType.NVarChar).Value = reserva.motivo;
            cmd.Parameters.Add("@username", SqlDbType.NVarChar).Value = Repositorios.Repo_usuario.getInstancia().getUsuarioIngresado().username;
            DBhelper.ejecutarProcedure(cmd);
            DBhelper.cerrarConexion();
        }

        public Int32 comprobarNumReserva(Int32 idHotel, Int32 numeroRes, String fecha)
        {

            DBhelper.crearConexion();
            SqlCommand cmd = DBhelper.crearCommand("TRAEME_LA_COPA_MESSI.comprobarNumReserva");
            cmd.Parameters.Add("@idHotel", SqlDbType.Int).Value = idHotel;
            cmd.Parameters.Add("@idReserva", SqlDbType.Int).Value = numeroRes;
            cmd.Parameters.Add("@fecha", SqlDbType.NVarChar).Value = fecha;

            var valorDeRetorno = cmd.Parameters.Add("@ReturnVal", SqlDbType.Int);
            valorDeRetorno.Direction = ParameterDirection.ReturnValue;

            DBhelper.abrirConexion();

            DBhelper.ejecutarProcedure(cmd);

            DBhelper.cerrarConexion();

            return (int)valorDeRetorno.Value;

        }

        public Model.Cliente getClienteReserva(Int32 idHotel, Int32 numReserva)
        {
            DBhelper.crearConexion();

            DBhelper.abrirConexion();

            SqlCommand cmd = DBhelper.crearCommand("TRAEME_LA_COPA_MESSI.getClienteReserva");
            cmd.Parameters.Add("@idHotel", SqlDbType.Int).Value = idHotel;
            cmd.Parameters.Add("@numReserva", SqlDbType.Int).Value = numReserva;


            DataTable tablaCliente = DBhelper.obtenerTabla(cmd);

            Model.Cliente cliente = new Model.Cliente();

            foreach (DataRow row in tablaCliente.Rows)
            {

                cliente.id = (Int32)row["IdCliente"];
                cliente.nombre = (String)row["Nombre"];
                cliente.apellido = (String)row["Apellido"];
                cliente.numDoc = (Decimal)row["NumDoc"];
                cliente.descripcionDoc = (String)row["Descripcion"];
                cliente.mail = (String)row["Email"];

            }

            DBhelper.cerrarConexion();

            return cliente;
        }


        public int crearReservaReturnId(Model.Reserva reserva, String fecha)
        {
            DBhelper.crearConexion();
            SqlCommand cmd = DBhelper.crearCommand("TRAEME_LA_COPA_MESSI.newReservaReturnId");
            cmd.Parameters.Add("@desde", SqlDbType.DateTime).Value = reserva.fechaDesde;
            cmd.Parameters.Add("@hasta", SqlDbType.DateTime).Value = reserva.fechaHasta;
            cmd.Parameters.Add("@mailCliente", SqlDbType.NVarChar).Value = reserva.cliente.mail;
            cmd.Parameters.Add("@idCliente", SqlDbType.Int).Value = reserva.cliente.id;
            cmd.Parameters.Add("@idHotel", SqlDbType.Int).Value = reserva.hotel.idHotel;
            cmd.Parameters.Add("@idRegimen", SqlDbType.Int).Value = reserva.regimen.idRegimen;
            cmd.Parameters.Add("@fecha", SqlDbType.NVarChar).Value = fecha;

            var valorDeRetorno = cmd.Parameters.Add("@ReturnVal", SqlDbType.Int);
            valorDeRetorno.Direction = ParameterDirection.ReturnValue;

            DBhelper.abrirConexion();

            DBhelper.ejecutarProcedure(cmd);

            foreach (Model.Habitacion hab in reserva.habitaciones)
            {
                this.crearHabitacionPorReserva(hab.idHotel, hab.numero, (int)valorDeRetorno.Value);
            }

            DBhelper.cerrarConexion();

            return (int)valorDeRetorno.Value;
        }


        public void crearHabitacionPorReserva(int idHotel, int numHab, int idReserva)
        {
            DBhelper.crearConexion();
            DBhelper.abrirConexion();

            SqlCommand cmd = DBhelper.crearCommand("TRAEME_LA_COPA_MESSI.newHabitacionPorReserva");
            cmd.Parameters.Add("@idHotel", SqlDbType.Int).Value = idHotel;
            cmd.Parameters.Add("@numero", SqlDbType.Int).Value = numHab;
            cmd.Parameters.Add("@idReserva", SqlDbType.Int).Value = idReserva;

            DBhelper.ejecutarProcedure(cmd);

            DBhelper.cerrarConexion();

        }


        public void modificarReserva(Model.Reserva reserva, String fecha)
        {
            DBhelper.crearConexion();
            DBhelper.abrirConexion();

            SqlCommand cmd = DBhelper.crearCommand("TRAEME_LA_COPA_MESSI.modificarReserva");
            cmd.Parameters.Add("@id", SqlDbType.Decimal).Value = Convert.ToDecimal(reserva.id);
            cmd.Parameters.Add("@desde", SqlDbType.DateTime).Value = reserva.fechaDesde;
            cmd.Parameters.Add("@hasta", SqlDbType.DateTime).Value = reserva.fechaHasta;
            cmd.Parameters.Add("@idHotel", SqlDbType.Int).Value = reserva.hotel.idHotel;
            cmd.Parameters.Add("@idRegimen", SqlDbType.Int).Value = reserva.regimen.idRegimen;
            cmd.Parameters.Add("@fecha", SqlDbType.NVarChar).Value = fecha;

            DBhelper.ejecutarProcedure(cmd);

            foreach (Model.Habitacion hab in reserva.habitaciones)
            {
                this.eliminarHabitacionPorReserva(hab.idHotel, hab.numero, reserva.id);
            }

            foreach (Model.Habitacion hab in reserva.habitaciones)
            {
                this.crearHabitacionPorReserva(hab.idHotel, hab.numero, reserva.id);
            }

            DBhelper.cerrarConexion();
        }


        public void eliminarHabitacionPorReserva(int idHotel, int numHab, int idReserva)
        {
            DBhelper.crearConexion();
            DBhelper.abrirConexion();

            SqlCommand cmd = DBhelper.crearCommand("TRAEME_LA_COPA_MESSI.eliminarHabitacionPorReserva");
            cmd.Parameters.Add("@idHotel", SqlDbType.Int).Value = idHotel;
            cmd.Parameters.Add("@numero", SqlDbType.Int).Value = numHab;
            cmd.Parameters.Add("@idReserva", SqlDbType.Int).Value = idReserva;

            DBhelper.ejecutarProcedure(cmd);

            DBhelper.cerrarConexion();

        }


        public void hacerCheckIn(Model.Cliente cliente, Int32 numReserva) {
              
            DBhelper.crearConexion();

            DBhelper.abrirConexion();

            SqlCommand cmd = DBhelper.crearCommand("TRAEME_LA_COPA_MESSI.hacerCheckIn");
            cmd.Parameters.Add("@idCliente", SqlDbType.Int).Value = cliente.id;
            cmd.Parameters.Add("@mailCliente", SqlDbType.NVarChar).Value = cliente.mail;
            cmd.Parameters.Add("@numeroDoc", SqlDbType.NVarChar).Value = cliente.numDoc;
            cmd.Parameters.Add("@idReserva", SqlDbType.Int).Value = numReserva;

            DBhelper.ejecutarProcedure(cmd);

            DBhelper.cerrarConexion();
        
        }
        
        public void generarLogEstadia(String username, Int32 numReserva, String fecha)
        {

            DBhelper.crearConexion();

            DBhelper.abrirConexion();

            SqlCommand cmd = DBhelper.crearCommand("TRAEME_LA_COPA_MESSI.generarLogEstadia");
            cmd.Parameters.Add("@idReserva", SqlDbType.Int).Value = numReserva;
            cmd.Parameters.Add("@usuario", SqlDbType.NVarChar).Value = username;
            cmd.Parameters.Add("@fecha", SqlDbType.NVarChar).Value = fecha;

            DBhelper.ejecutarProcedure(cmd);

            DBhelper.cerrarConexion();

        }


        public int comprobarDisponibilidad(DateTime desde, DateTime hasta)
        {
            DBhelper.crearConexion();
            SqlCommand cmd = DBhelper.crearCommand("TRAEME_LA_COPA_MESSI.comprobarDisponibilidadReserva");
            cmd.Parameters.Add("@desde", SqlDbType.DateTime).Value = desde;
            cmd.Parameters.Add("@hasta", SqlDbType.DateTime).Value = hasta;

            var valorDeRetorno = cmd.Parameters.Add("@ReturnVal", SqlDbType.Int);
            valorDeRetorno.Direction = ParameterDirection.ReturnValue;

            DBhelper.abrirConexion();

            DBhelper.ejecutarProcedure(cmd);

            DBhelper.cerrarConexion();

            return (int)valorDeRetorno.Value;
        }


        public void hacerCheckOut(String usuario ,Int32 numReserva, String fecha)
        {

            DBhelper.crearConexion();

            DBhelper.abrirConexion();

            SqlCommand cmd = DBhelper.crearCommand("TRAEME_LA_COPA_MESSI.hacerCheckOut");
            cmd.Parameters.Add("@idReserva", SqlDbType.Int).Value = numReserva;
            cmd.Parameters.Add("@username", SqlDbType.NVarChar).Value = usuario;
            cmd.Parameters.Add("@fecha", SqlDbType.NVarChar).Value = fecha;

            DBhelper.ejecutarProcedure(cmd);

            DBhelper.cerrarConexion();

        }

        public void calcularTotalFactura(Int32 numFactura) 
        {

            DBhelper.crearConexion();

            DBhelper.abrirConexion();

            SqlCommand cmd = DBhelper.crearCommand("TRAEME_LA_COPA_MESSI.calcularTotalFactura");
            cmd.Parameters.Add("@numFactura", SqlDbType.Int).Value = numFactura;

            DBhelper.ejecutarProcedure(cmd);

            DBhelper.cerrarConexion();
        
        }

        public int comprobarNumReservaCheckout(Int32 numReserva)
        {
            
         DBhelper.crearConexion();
            SqlCommand cmd = DBhelper.crearCommand("TRAEME_LA_COPA_MESSI.comprobarNumReservaCheckout");
            cmd.Parameters.Add("@reservaId", SqlDbType.Int).Value = numReserva;

            var valorDeRetorno = cmd.Parameters.Add("@ReturnVal", SqlDbType.Int);
            valorDeRetorno.Direction = ParameterDirection.ReturnValue;

            DBhelper.abrirConexion();

            DBhelper.ejecutarProcedure(cmd);

            DBhelper.cerrarConexion();

            return (int)valorDeRetorno.Value;
        
        }

        public int comprobarSiReservaNoPasoFecha(decimal numReserva, String fecha)
        {

            DBhelper.crearConexion();
            SqlCommand cmd = DBhelper.crearCommand("TRAEME_LA_COPA_MESSI.comprobarReservaNoPasoFecha");
            cmd.Parameters.Add("@idReserva", SqlDbType.Decimal).Value = numReserva;
            cmd.Parameters.Add("@fecha", SqlDbType.NVarChar).Value = fecha;

            var valorDeRetorno = cmd.Parameters.Add("@ReturnVal", SqlDbType.Int);
            valorDeRetorno.Direction = ParameterDirection.ReturnValue;

            DBhelper.abrirConexion();

            DBhelper.ejecutarProcedure(cmd);

            DBhelper.cerrarConexion();

            return (int)valorDeRetorno.Value;

        }


        public List<Model.Habitacion> getHabitacionesEnFecha(DateTime desde, DateTime hasta, int idHotel)
        {
            DBhelper.crearConexion();
            DBhelper.abrirConexion();

            SqlCommand cmd = DBhelper.crearCommand("TRAEME_LA_COPA_MESSI.getHabitacionesEnFecha");
            cmd.Parameters.Add("@desde", SqlDbType.DateTime).Value = desde;
            cmd.Parameters.Add("@hasta", SqlDbType.DateTime).Value = hasta;
            cmd.Parameters.Add("@idHotel", SqlDbType.Int).Value = idHotel;
            
            DataTable tabla = DBhelper.obtenerTabla(cmd);

            List<Model.Habitacion> habitaciones = new List<Model.Habitacion>();

            foreach (DataRow row in tabla.Rows)
            {
                Model.Habitacion habitacion = new Model.Habitacion();

                habitacion.idHotel = ((Int32)row["IdHotel"]);
                habitacion.numero = ((Int32)row["Numero"]);
                habitacion.piso = ((Int32)row["Piso"]);
                habitacion.ubicacion = ((String)row["Ubicacion"]);
                habitacion.estado = (Convert.ToInt16(row["Estado"]));
                habitacion.ubicacion = ((String)row["Descripcion"]);

                habitacion.tipoHab = Repo_habitacion.getInstancia().getTipoHabitacion(((Int32)row["CodigoTipo"]));

                habitaciones.Add(habitacion);
            }

            DBhelper.cerrarConexion();

            return habitaciones;
        }

        public List<Model.Habitacion> getHabitacionesEnFechaModificacion(DateTime desde, DateTime hasta, int idHotel, decimal idReserva)
        {
            DBhelper.crearConexion();
            DBhelper.abrirConexion();

            SqlCommand cmd = DBhelper.crearCommand("TRAEME_LA_COPA_MESSI.getHabitacionesEnFechaModificacion");
            cmd.Parameters.Add("@desde", SqlDbType.DateTime).Value = desde;
            cmd.Parameters.Add("@hasta", SqlDbType.DateTime).Value = hasta;
            cmd.Parameters.Add("@idHotel", SqlDbType.Int).Value = idHotel;
            cmd.Parameters.Add("@idReserva", SqlDbType.Decimal).Value = idReserva;
            
            DataTable tabla = DBhelper.obtenerTabla(cmd);

            List<Model.Habitacion> habitaciones = new List<Model.Habitacion>();

            foreach (DataRow row in tabla.Rows)
            {
                Model.Habitacion habitacion = new Model.Habitacion();

                habitacion.idHotel = ((Int32)row["IdHotel"]);
                habitacion.numero = ((Int32)row["Numero"]);
                habitacion.piso = ((Int32)row["Piso"]);
                habitacion.ubicacion = ((String)row["Ubicacion"]);
                habitacion.estado = (Convert.ToInt16(row["Estado"]));
                habitacion.ubicacion = ((String)row["Descripcion"]);

                habitacion.tipoHab = Repo_habitacion.getInstancia().getTipoHabitacion(((Int32)row["CodigoTipo"]));

                habitaciones.Add(habitacion);
            }

            DBhelper.cerrarConexion();

            return habitaciones;
        }

        

        public void registrarCreacion(Model.Usuario usuario, int idReserva, String fecha)
        {
            DBhelper.crearConexion();
            DBhelper.abrirConexion();

            SqlCommand cmd = new SqlCommand();

            if (usuario == null)
            {
                cmd = DBhelper.crearCommand("TRAEME_LA_COPA_MESSI.registrarCreacionReservaConGuest");
                cmd.Parameters.Add("@idReserva", SqlDbType.Decimal).Value = Convert.ToDecimal(idReserva);
                cmd.Parameters.Add("@fecha", SqlDbType.NVarChar).Value = fecha;
            }
            else
            {
                cmd = DBhelper.crearCommand("TRAEME_LA_COPA_MESSI.registrarCreacionReserva");
                cmd.Parameters.Add("@user", SqlDbType.NVarChar).Value = usuario.username;
                cmd.Parameters.Add("@idReserva", SqlDbType.Decimal).Value = Convert.ToDecimal(idReserva);
                cmd.Parameters.Add("@fecha", SqlDbType.NVarChar).Value = fecha;
            }


            DBhelper.ejecutarProcedure(cmd);

            DBhelper.cerrarConexion();
        }


        public void registrarModificacion(Model.Usuario usuario, int idReserva, String fecha)
        {
            DBhelper.crearConexion();
            DBhelper.abrirConexion();

            SqlCommand cmd = new SqlCommand();

            if (usuario == null)
            {
                cmd = DBhelper.crearCommand("TRAEME_LA_COPA_MESSI.registrarModificacionReservaConGuest");
                cmd.Parameters.Add("@idReserva", SqlDbType.Decimal).Value = Convert.ToDecimal(idReserva);
                cmd.Parameters.Add("@fecha", SqlDbType.NVarChar).Value = fecha;
            }
            else
            {
                cmd = DBhelper.crearCommand("TRAEME_LA_COPA_MESSI.registrarModificacionReserva");
                cmd.Parameters.Add("@user", SqlDbType.NVarChar).Value = usuario.username;
                cmd.Parameters.Add("@idReserva", SqlDbType.Decimal).Value = Convert.ToDecimal(idReserva);
                cmd.Parameters.Add("@fecha", SqlDbType.NVarChar).Value = fecha;
            }


            DBhelper.ejecutarProcedure(cmd);

            DBhelper.cerrarConexion();
        }

        public int getIdClienteDeReserva(decimal idReserva)
        {
            DBhelper.crearConexion();
            SqlCommand cmd = DBhelper.crearCommand("TRAEME_LA_COPA_MESSI.getIdClienteDeReserva");
            cmd.Parameters.Add("@idReserva", SqlDbType.Decimal).Value = idReserva;

            var valorDeRetorno = cmd.Parameters.Add("@ReturnVal", SqlDbType.Int);
            valorDeRetorno.Direction = ParameterDirection.ReturnValue;

            DBhelper.abrirConexion();

            DBhelper.ejecutarProcedure(cmd);

            DBhelper.cerrarConexion();

            return (int)valorDeRetorno.Value;
        }

        public void EliminarReservasNoEfectivizadasDeCliente(String fecha, int idCliente)
        {
            DBhelper.crearConexion();
            DBhelper.abrirConexion();


            SqlCommand cmd = DBhelper.crearCommand("TRAEME_LA_COPA_MESSI.EliminarReservasNoEfectivizadasDeCliente");
            cmd.Parameters.Add("@fecha", SqlDbType.NVarChar).Value = fecha;
            cmd.Parameters.Add("@idCliente", SqlDbType.Int).Value = idCliente;

            DBhelper.ejecutarProcedure(cmd);

            DBhelper.cerrarConexion();


        }


        public Int32 comprobarEstadoHotel(DateTime desde, DateTime hasta, Int32 hotel)
        {

            DBhelper.crearConexion();
            SqlCommand cmd = DBhelper.crearCommand("TRAEME_LA_COPA_MESSI.comprobarEstadoHotel");
            cmd.Parameters.Add("@idHotel", SqlDbType.Decimal).Value = hotel;
            cmd.Parameters.Add("@fechaDesde", SqlDbType.DateTime).Value = desde;
            cmd.Parameters.Add("@fechaHasta", SqlDbType.DateTime).Value = hasta;

            var valorDeRetorno = cmd.Parameters.Add("@ReturnVal", SqlDbType.Int);
            valorDeRetorno.Direction = ParameterDirection.ReturnValue;

            DBhelper.abrirConexion();

            DBhelper.ejecutarProcedure(cmd);

            DBhelper.cerrarConexion();

            return (int)valorDeRetorno.Value;

        }


    }
}
    

