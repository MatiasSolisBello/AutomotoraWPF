using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomotoraLibrary
{
    public class AutomotoraCollection
    {
        public List<Car> car = new List<Car>();

        public bool SaveCar(Car car)
        {
            this.car.Add(car);
            return true;
        }
    }
}
