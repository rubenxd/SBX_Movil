using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SBX.Ado;

namespace SBX
{
    [Activity(Label = "InventarioActivity")]
    public class InventarioActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.activity_inventario);

            Spinner spinner = FindViewById<Spinner>(Resource.Id.spinner);

            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.planets_array, Android.Resource.Layout.SimpleSpinnerItem);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;

            Button btnSalir = FindViewById<Button>(Resource.Id.btn_salir);
            btnSalir.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
            };
            //EditText outputText = FindViewById<EditText>(Resource.Id.outputText);
            //ADO
            Button btnGuardar = FindViewById<Button>(Resource.Id.btn_guardar);
            AdoInventario adoInventario = new AdoInventario();
            EditText editText_Item = FindViewById<EditText>(Resource.Id.editText_Item);
            EditText editText_Nombre = FindViewById<EditText>(Resource.Id.editText_Nombre);
            EditText editText_Referencia = FindViewById<EditText>(Resource.Id.editText_Referencia);
            EditText editText_iva = FindViewById<EditText>(Resource.Id.editText_iva);
            Spinner planet_prompt = FindViewById<Spinner>(Resource.Id.spinner);
            EditText editText_costo = FindViewById<EditText>(Resource.Id.editText_costo);
            EditText editText_PrecioVenta = FindViewById<EditText>(Resource.Id.editText_PrecioVenta);
            btnGuardar.Click += (sender, e) =>
            {            
                adoInventario.Item = editText_Item.Text;
                adoInventario.Nombre = editText_Nombre.Text;
                adoInventario.Referencia = editText_Referencia.Text;
                adoInventario.IVA = editText_iva.Text;
                adoInventario.proveedor = planet_prompt.SelectedItem.ToString();
                adoInventario.costo = editText_costo.Text;
                adoInventario.precioventa = editText_PrecioVenta.Text;
                var toast = adoInventario.AdoCreate();
                Toast.MakeText(this, toast, ToastLength.Long).Show();       
            };
        }

        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string toast = string.Format("The planet is {0}", spinner.GetItemAtPosition(e.Position));
            Toast.MakeText(this, toast, ToastLength.Long).Show();
        }
    }
}