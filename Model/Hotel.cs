﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrbaHotel.Model
{
 
    public class Hotel
    {
        public Int32 idHotel { get; set; }
        public String nombre { get; set; }
        public String mail { get; set; }
        public Int32 telefono { get; set; }
        public String ciudad { get; set; }
        public String pais { get; set; }
        public String calle { get; set; }
        public Decimal nroCalle { get; set; }
        public Decimal porcEstrella { get; set; }
        public Int32 estrellas { get; set; }
        public List<Model.Regimen> regimenes;
        public DateTime fechaCreacion { get; set; }


        public int IdHotel
        {

            get
            {
                return idHotel;
            }
        }


        public Hotel Objeto
        {

            get
            {
                return this;
            }
        }


        public Hotel(){}
    
    
    }

}
