using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
 
namespace AutomotoraLibrary
{
    public class AutomotoraCollection
    {

        AutomotoraEntities db = new AutomotoraEntities();

        // Listado de Marcas en combobox con sintaxis LINQ
        public List<Brand> ListBrands()
        {
            List<Brand> brands = (from m in db.Brands
                                   select new Brand
                                   {
                                       Id = m.Id,
                                       Name = m.Name
                                   }).ToList();

            return brands;
        }

        // Listado de Modelos en combobox con sintaxis LINQ
        public List<Model> ListModelsData()
        {
            List<Model> modelCar = (from m in db.Models
                                    select new Model
                                    {
                                        Id = m.Id,
                                        Name = m.Name
                                    }).ToList();

            return modelCar;
        }


        public List<Model> ListModelsByBrand(int brandId)
        {
            return (from m in db.Models
                    where m.BrandId == brandId
                    select new Model
                    {
                        Id = m.Id,
                        Name = m.Name
                    }).ToList();
        }

        // Listado para personalizacion de grilla
        public List<Car> ListAll()
        {
            return (from a in db.Cars
                    select new Car
                    {
                        Id = a.Id,
                        LicencePlate = a.LicencePlate,
                        Year = a.Year,
                        Date = a.DateFabrication,
                        New = a.New,
                        Transmissions = (a.Transmissions==true)?Transmissions.Automatica:Transmissions.Mecanica,
                        Models = new Model
                        {
                            Id = a.Models.Id,
                            Name = a.Models.Name,
                            Brand = new Brand
                            {
                                Id = a.Models.Brands.Id,
                                Name = a.Models.Brands.Name,
                            }
                            
                        }
                    }).ToList();
        }


        public List<Car> cars = new List<Car>();


        // Guardar Automovil
        public bool SaveCar(Car car)
        {
            // validacion de existencia de patente
            if (this.SearchCar(car.LicencePlate) != null)
            {
                return false;
            }

            try
            {
                Data.Cars a = new Cars();
                a.LicencePlate = car.LicencePlate;
                a.Year = car.Year;
                a.ModelId = car.Models.Id;
                a.DateFabrication = car.Date;
                a.New = car.New;
                a.Transmissions = (Transmissions.Automatica == car.Transmissions) ? true : false;

                db.Cars.Add(a);
                db.SaveChanges();
            }
            catch(Exception)
            {
                return false;
            }
            return true;
        }
        

        // Modificar Automovil
        public bool UpdateCar(Car car)
        {
            try
            {
                Data.Cars a = (from at in db.Cars where at.LicencePlate == car.LicencePlate select at).FirstOrDefault();
                if(a == null)
                {
                    return false;
                }
                a.Year = car.Year;
                a.ModelId = car.Models.Id;
                a.DateFabrication = car.Date;
                a.New = car.New;
                a.Transmissions = (Transmissions.Automatica == car.Transmissions) ? true : false;

                db.Entry(a).State = System.Data.EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        // Buscar Automovil
        public Car SearchCar(string licencePlate)
        {
            Car car = (from a in db.Cars where a.LicencePlate == licencePlate select new Car {
                Id = a.Id,
                LicencePlate = a.LicencePlate,
                Models = new Model()
                {
                    Id = a.Models.Id,
                    Name = a.Models.Name,
                    Brand = new Brand
                    {
                        Id = a.Models.Brands.Id,
                        Name = a.Models.Brands.Name,
                    }
                },
                Year = a.Year,
                Date = a.DateFabrication,
                New = a.New,
                Transmissions = (a.Transmissions == true)?Transmissions.Automatica:Transmissions.Mecanica
        
            }).FirstOrDefault();

            return car;
        }


        // Eliminar Automovil
        public bool DeleteCar(string licencePlate)
        {
            Data.Cars car = (from a in db.Cars where a.LicencePlate == licencePlate select a).FirstOrDefault();
            
            if(car== null)
            {
                return false;
            }

            try
            {
                db.Cars.Remove(car);
                db.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        // Primer filtro
        public List<Car> SearchByLicencePlate(string licencePlate)
        {
            List<Car> cars = (
                from a in db.Cars
                where a.LicencePlate.ToLower().Contains(licencePlate)
                select new Car
                {
                    Id = a.Id,
                    LicencePlate = a.LicencePlate,
                    Year = a.Year,
                    Date = a.DateFabrication,
                    New = a.New,
                    Transmissions = (a.Transmissions == true) ? Transmissions.Automatica : Transmissions.Mecanica,
                    Models = new Model
                    {
                        Id = a.Models.Id,
                        Name = a.Models.Name,
                        Brand = new Brand
                        {
                            Id = a.Models.Brands.Id,
                            Name = a.Models.Brands.Name,
                        }

                    }
                }).ToList();

            return cars;
        }

        // Segundo filtro
        public List<Car> SearchByBrand(int brandId)
        {
            List<Car> cars = (
                from a in db.Cars
                where a.Models.BrandId == brandId
                select new Car
                {
                    Id = a.Id,
                    LicencePlate = a.LicencePlate,
                    Year = a.Year,
                    Date = a.DateFabrication,
                    New = a.New,
                    Transmissions = (a.Transmissions == true) ? Transmissions.Automatica : Transmissions.Mecanica,
                    Models = new Model
                    {
                        Id = a.Models.Id,
                        Name = a.Models.Name,
                        Brand = new Brand
                        {
                            Id = a.Models.Brands.Id,
                            Name = a.Models.Brands.Name,
                        }

                    }
                }).ToList();

            return cars;
        }
    }
}
