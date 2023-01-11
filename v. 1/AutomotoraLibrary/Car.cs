using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomotoraLibrary
{
    public class Car
    {
        private string _licencePlate;
        private Brand _brand;
        private string _model;
        private int _year;
        private bool _new;
        private DateTime _date;
        private Transmissions _transmissions;

        public Car()
        {

        }

        public DateTime Date
        {
            get { return _date; }
            set
            {
                if (value > DateTime.Now)
                {
                    throw new ArgumentException("La fecha de fabricacion debe ser menor o igual a hoy");
                }
                _date = value;
            }
        }

        public Transmissions Transmissions
        {
            get { return _transmissions; }
            set { _transmissions = value; }
        }


        public bool New
        {
            get { return _new; }
            set { _new = value; }
        }


        public int Year
        {
            get { return _year; }
            set { 
                //validar si año es menor a 1950
                if(value < 1950)
                {
                    throw new ArgumentException("El año debe ser como minimo 1950");
                }
                _year = value; 
            }
        }


        public string Model
        {
            get { return _model; }
            set {
                //validar que el minimo de largo sea 3
                if (value.Length < 3)
                {
                    throw new ArgumentException("El modelo debe tener al menos 3 caracteres");
                }
                _model = value; 
            }
        }


        public Brand Brand
        {
            get { return _brand; }
            set { _brand = value; }
        }


        public string LicencePlate
        {
            get { return _licencePlate; }
            set { _licencePlate = value;  }
        }

    }
}
