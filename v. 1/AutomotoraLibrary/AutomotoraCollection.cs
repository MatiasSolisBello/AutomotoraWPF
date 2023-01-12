using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace AutomotoraLibrary
{
    public class AutomotoraCollection
    {
        public List<Car> cars = new List<Car>();

        
        // Guardar Carro
        public bool SaveCar(Car car)
        {   
            // validacion de existencia de patente
            foreach (Car a in cars)
            {
                if (a.LicencePlate == car.LicencePlate)
                {
                    return false;
                }
            }

            this.cars.Add(car);
            return true;
        }


        // Buscar Carro
        public Car SearchCar(string licencePlate)
        {
            foreach(Car a in cars)
            {
                if(a.LicencePlate == licencePlate)
                {
                    return a;
                }
            }
            return null;
        }


        // Eliminar Carro
        public bool DeleteCar(string licencePlate)
        {
            Car car = this.SearchCar(licencePlate);
            
            if(car== null)
            {
                return false;
            }

            this.cars.Remove(car);
            return true;
        }


        //  Primer filtro: Buscar por licencia
        public List<Car> SearchByLicencePlate(string licencePlate)
        {
            List<Car> cars = (from a in this.cars

                              //igual a Patente
                              //where a.LicencePlate == licencePlate

                              // si contiene parte de la patente
                              where a.LicencePlate.ToLower().Contains(licencePlate)
                              select a).ToList();
            return cars;
        }


        // Segundo filtro: Buscar por marca
        public List<Car> SearchByBrand(Brand brand)
        {
            List<Car> cars = (from a in this.cars
                              where a.Brand == brand
                              select a).ToList();
            return cars;
        }
    }
}
