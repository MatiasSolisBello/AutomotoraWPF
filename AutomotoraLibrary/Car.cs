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
        private Transmissions _transmissions;

        public Car()
        {

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
            set { _year = value; }
        }


        public string Model
        {
            get { return _model; }
            set { _model = value; }
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
