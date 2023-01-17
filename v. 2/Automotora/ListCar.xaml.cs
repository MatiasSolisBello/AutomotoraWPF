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
        private AutomotoraCollection _collection = new AutomotoraCollection();

        public static ListCar window = null;

        public static ListCar GetInstance()
        {
            if (window == null)
            {
                window = new ListCar();
            }
            return window;
        }

        public AutomotoraCollection Collection
        {
            get { return _collection; }
            set { _collection = value; }
        }


        // Recargar grilla
        public void ReloadGrill()
        {
            Dispatcher.Invoke(() =>
            {
                dgCars.ItemsSource = this.Collection.ListAll();
                cboBrand.ItemsSource = _collection.ListBrands();
            });
        }

        public ListCar()
        {
            InitializeComponent();
            dgCars.ItemsSource = this.Collection.ListAll();

            // Cargar marcas en comboBox de Filtro
            cboBrand.ItemsSource = _collection.ListBrands();

            // Nos suscribimos
            NotificationCenter.Subscribe("car_changed", ReloadGrill);
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
            int brandId = int.Parse(cboBrand.SelectedValue.ToString());

            List<Car> cars = this.Collection.SearchByBrand(brandId);

            dgCars.ItemsSource = null;
            dgCars.ItemsSource = cars;

        }


        // Cerrar ventana
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        // Ir a XAML / Click MetroWindow / Propiedades / Rayo / Closing / DobleClick
        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            window = null;
        }

        // Notificamos seleccion de fila
        private void DgCars_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(dgCars.SelectedValue != null)
            {
                Car car = (Car)dgCars.SelectedValue;
                NotificationCenter.Notify("car_selected", car);
            }
        }
    }
}