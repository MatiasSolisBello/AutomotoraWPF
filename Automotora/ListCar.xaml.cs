using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AutomotoraLibrary;

namespace Automotora
{
    /// <summary>
    /// Lógica de interacción para ListCar.xaml
    /// </summary>
    public partial class ListCar
    {
        private AutomotoraCollection _collection;

        public AutomotoraCollection Collection
        {
            get { return _collection; }
            set { _collection = value; }
        }

        public ListCar()
        {
            InitializeComponent();
        }

        public ListCar(AutomotoraCollection collection)
        {
            this.Collection = collection;
            InitializeComponent();
            dgCars.ItemsSource = this.Collection.cars;

            // Cargar marcas en comboBox de Filtro
            cboBrand.ItemsSource = Enum.GetValues(typeof(Brand));
        }

        private void txtLicencePlate_KeyUp(object sender, KeyEventArgs e)
        {
            string licencePlate = txtLicencePlate.Text;
            List<Car> cars = this.Collection.SearchByLicencePlate(licencePlate);

            dgCars.ItemsSource = null;
            dgCars.ItemsSource = cars;
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            Brand brand = (Brand)cboBrand.SelectedIndex;

            List<Car> cars = this.Collection.SearchByBrand(brand);

            dgCars.ItemsSource = null;
            dgCars.ItemsSource = cars;

        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            dgCars.ItemsSource = null;
            dgCars.ItemsSource = this.Collection.cars;
        }

        // Cerrar ventana
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
