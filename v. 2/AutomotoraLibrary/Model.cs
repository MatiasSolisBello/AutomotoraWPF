using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomotoraLibrary
{
    public class Model
    {
        private int _id;
        private string _name;
        private Brands _brands;

        public Brands Brands
        {
            get { return _brands; }
            set { _brands = value; }
        }


        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }


        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

    }
}
