using AutomotoraLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Automotora
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        // Inicializa Coleccion
        private AutomotoraCollection _collection = new AutomotoraCollection();
        public MainWindow()
        {
            InitializeComponent();

            // Carga combobox con datos de Enum Brand y por defecto es 0
            //cboBrand.ItemsSource = Enum.GetValues(typeof(Brands));
            //cboBrand.SelectedIndex = 0;
        }

        // Llevar los datos de menu -> listado
        public MainWindow(AutomotoraCollection collection)
        {
            this._collection = collection;
            InitializeComponent();

            // Carga combobox con datos de Brand y por defecto es 0
            cboBrand.ItemsSource = collection.ListBrands();
            cboBrand.SelectedIndex = 0;

            //cboModel.ItemsSource = collection.ListModelsData();
            //cboModel.SelectedIndex = 0;
        }


        // Boton Guardar
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //recolectar data
            string licencePlate = txtLicencePlate.Text;
            //Brand brand = (Brand)cboBrand.SelectedIndex;
            Brand brand = new Brand()
            {
                Id = int.Parse(cboBrand.SelectedValue.ToString())
            };

            Model model = new Model()
            {
                Id = int.Parse(cboModel.SelectedValue.ToString())
            };

            //validacion de tipo de dato year
            int year = 0;

            if (int.TryParse(txtYear.Text, out year) == false)
            {
                MessageBox.Show("El año debe ser numerico", "Error");
                return;
            }

            bool cnew = chkNew.IsChecked.Value;

            // validacion rediobutton
            Transmissions transmission = Transmissions.Automatica;

            if (rbtOption1.IsChecked == true)
            {
                transmission = Transmissions.Mecanica;
            }

            if (dtpDate.SelectedDate == null)
            {
                MessageBox.Show("Debes seleccionar una fecha de fabricacion");
                return;
            }

            DateTime date = dtpDate.SelectedDate.Value;


            try
            {
                // crear instancia
                Car car = new Car();
                car.LicencePlate = licencePlate;
                car.Brand = brand;
                car.Models = model;
                car.Year = year;
                car.New = cnew;
                car.Date = date;
                car.Transmissions = transmission;



                if (_collection.SaveCar(car))
                {
                    MessageBox.Show("Guardado correctamente");
                }
                else
                {
                    MessageBox.Show("La patente ya existe");
                }

                LoadTable();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void LoadTable()
        {
            dgCar.ItemsSource = null;
            dgCar.ItemsSource = _collection.cars;
        }


        // Programar boton buscar
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string licencePlate = txtLicencePlate.Text;

            if (licencePlate.Trim() == "")
            {
                MessageBox.Show("Debes ingresar una patente");
                return;
            }

            Car car = _collection.SearchCar(licencePlate);
            if (car == null)
            {
                MessageBox.Show("No se ha encontrado la patente");
                return;
            }

            cboBrand.SelectedValue = car.Models.Brand.Id;

            cboModel.ItemsSource = _collection.ListModelsByBrand(car.Models.Brand.Id);
            cboModel.SelectedValue = car.Models.Id;

            txtYear.Text = car.Year.ToString();
            chkNew.IsChecked = car.New;
            dtpDate.SelectedDate = car.Date;

            if (car.Transmissions == Transmissions.Automatica)
            {
                rbtOption2.IsChecked = true;
            }
            else
            {
                rbtOption1.IsChecked = true;
            }
        }

        // Programar boton Eliminar
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            string licencePlate = txtLicencePlate.Text;
            if (licencePlate.Trim() == "")
            {
                MessageBox.Show("Debes ingresar una patente");
                return;
            }

            if (_collection.DeleteCar(licencePlate))
            {
                MessageBox.Show("Eliminado correctamente");
                LoadTable();
            }
            else
            {
                MessageBox.Show("No se ha encontrado la patente");
            }
        }

        // Programar boton Actualizar
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            // recolect data
            string licencePlate = txtLicencePlate.Text;

            //Brand brand = (Brands)cboBrand.SelectedIndex; 
            Brand brand = new Brand()
            {
                Id = int.Parse(cboBrand.SelectedValue.ToString())
            };
            int year = 0;
            if (int.TryParse(txtYear.Text, out year) == false)
            {
                MessageBox.Show("El año debe ser numerico", "Error");
                return;
            }
            bool cnew = chkNew.IsChecked.Value;
            Transmissions transmission = Transmissions.Automatica;
            if (rbtOption1.IsChecked == true)
            {
                transmission = Transmissions.Mecanica;
            }

            Model model = new Model()
            {
                Id = int.Parse(cboModel.SelectedValue.ToString())
            };

            try
            {
                // crear instancia
                Car car = _collection.SearchCar(licencePlate);

                if (car == null)
                {
                    MessageBox.Show("No se ha encontrado la patente");
                    return;
                }

                car.LicencePlate = licencePlate;
                car.Models = model;
                car.Brand = brand;
                car.Year = year;
                car.New = cnew;
                car.Transmissions = transmission;

                if (_collection.UpdateCar(car))
                {
                    MessageBox.Show("Modificado correctamente");
                }
                else
                {
                    MessageBox.Show("No se ha podido modificar");
                }
                LoadTable();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnList_Click(object sender, RoutedEventArgs e)
        {
            ListCar list = new ListCar(this._collection);
            list.Show();
        }

        private void CboBrand_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // ¿Existe una opcion seleccionada?
            if(cboBrand.SelectedValue != null)
            {
                int brandId = int.Parse(cboBrand.SelectedValue.ToString());

                cboModel.ItemsSource = _collection.ListModelsByBrand(brandId);
                cboModel.SelectedIndex = 0;
                cboModel.IsEnabled = true;
            }
        }
    }
}