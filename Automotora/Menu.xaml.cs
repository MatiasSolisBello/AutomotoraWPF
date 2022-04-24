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
    /// Lógica de interacción para Menu.xaml
    /// </summary>
    public partial class Menu
    {

        private AutomotoraCollection _collection = new AutomotoraCollection();
        public Menu()
        {
            InitializeComponent();
        }

        // Rediridir a otras ventanas
        private void TileCars_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow(this._collection);
            main.Show();
        }

        private void TileList_Click(object sender, RoutedEventArgs e)
        {
            ListCar list = new ListCar(this._collection);
            list.Show();
        }

        // Salir de la aplicación
        private void TileExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
