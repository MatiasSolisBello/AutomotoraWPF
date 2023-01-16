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
        private Brand _brand;

        public Brand Brand
        {
            get { return _brand; }
            set { _brand = value; }
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
