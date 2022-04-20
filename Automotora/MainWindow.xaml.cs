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
    public partial class MainWindow : Window
    {
        // Inicializa Coleccion
        private AutomotoraCollection _collection = new AutomotoraCollection();
        public MainWindow()
        {
            InitializeComponent();

            // Carga combobox con datos de Enum Brand y por defecto es 0
            cboBrand.ItemsSource = Enum.GetValues(typeof(Brand));
            cboBrand.SelectedIndex = 0;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //recolectar data
            string licencePlate = txtLicencePlate.Text;
            Brand brand = (Brand)cboBrand.SelectedIndex;
            string model = txtModel.Text;

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

            // crear instancia
            Car car = new Car();
            car.LicencePlate = licencePlate;
            car.Brand = brand;
            car.Model = model;
            car.Year = year;
            car.New = cnew;
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

        private void LoadTable()
        {
            dgCar.ItemsSource = null;
            dgCar.ItemsSource = _collection.car;
        }
    }
}
