using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Text.Style;
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
            TextView textViewItem = FindViewById<TextView>(Resource.Id.text_item);
            TextView textViewNombre = FindViewById<TextView>(Resource.Id.text_Nombre);
            TextView textViewReferencia = FindViewById<TextView>(Resource.Id.text_Referencia);
            TextView textViewIVA = FindViewById<TextView>(Resource.Id.text_iva);
            TextView textViewProveedor = FindViewById<TextView>(Resource.Id.text_proveedor);
            TextView textViewCosto = FindViewById<TextView>(Resource.Id.text_costo);
            TextView textViewPrecio = FindViewById<TextView>(Resource.Id.text_PrecioVenta);
            textViewItem.SetTextColor(Android.Graphics.Color.ParseColor("#2C3E50"));
            textViewNombre.SetTextColor(Android.Graphics.Color.ParseColor("#2C3E50"));
            textViewReferencia.SetTextColor(Android.Graphics.Color.ParseColor("#2C3E50"));
            textViewIVA.SetTextColor(Android.Graphics.Color.ParseColor("#2C3E50"));
            textViewProveedor.SetTextColor(Android.Graphics.Color.ParseColor("#2C3E50"));
            textViewCosto.SetTextColor(Android.Graphics.Color.ParseColor("#2C3E50"));
            textViewPrecio.SetTextColor(Android.Graphics.Color.ParseColor("#2C3E50"));
            //Cargar maximo Item
            AdoInventario adoInventario = new AdoInventario();
            var Productos = adoInventario.AdoSelectMaxItem();
            EditText editText = FindViewById<EditText>(Resource.Id.editText_Item);
            var ItemMax = Convert.ToDouble(Productos[0]) + 1;
            editText.Text = ItemMax.ToString();
            //Cargar proveedor  en spinner      
            var Proveedores = adoInventario.AdoSelectProveedores();
            Spinner spinner = FindViewById<Spinner>(Resource.Id.spinner);
            List<string> Proveedor = new List<string>();
            foreach (var item in Proveedores)
            {
                var resl = item;
                Proveedor.Add(resl);
            }
            var adapter = new ArrayAdapter<string>(this,
            Android.Resource.Layout.SimpleSpinnerItem, Proveedor);
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
            
            EditText editText_Item = FindViewById<EditText>(Resource.Id.editText_Item);
            EditText editText_Nombre = FindViewById<EditText>(Resource.Id.editText_Nombre);
            EditText editText_Referencia = FindViewById<EditText>(Resource.Id.editText_Referencia);
            EditText editText_iva = FindViewById<EditText>(Resource.Id.editText_iva);
            Spinner planet_prompt = FindViewById<Spinner>(Resource.Id.spinner);
            EditText editText_costo = FindViewById<EditText>(Resource.Id.editText_costo);
            EditText editText_PrecioVenta = FindViewById<EditText>(Resource.Id.editText_PrecioVenta);
            int Validado = 0;
            btnGuardar.Click += (sender, e) =>
            {
                Validado = 0;
                TextView textViewNombre = FindViewById<TextView>(Resource.Id.text_Nombre);
                TextView textViewIVA = FindViewById<TextView>(Resource.Id.text_iva);
                TextView textViewCosto = FindViewById<TextView>(Resource.Id.text_costo);
                TextView textViewPrecio = FindViewById<TextView>(Resource.Id.text_PrecioVenta);
                if (editText_Nombre.Text == "")
                {
                    Validado++;               
                    textViewNombre.SetTextColor(Android.Graphics.Color.ParseColor("#E85434"));
                }
                else 
                {
                    textViewNombre.SetTextColor(Android.Graphics.Color.ParseColor("#2C3E50"));
                }
                if (editText_iva.Text == "")
                {
                    Validado++;            
                    textViewIVA.SetTextColor(Android.Graphics.Color.ParseColor("#E85434"));
                }
                else 
                {
                    textViewIVA.SetTextColor(Android.Graphics.Color.ParseColor("#2C3E50"));
                }
                if (editText_costo.Text == "")
                {
                    Validado++;                   
                    textViewCosto.SetTextColor(Android.Graphics.Color.ParseColor("#E85434"));
                }
                else 
                {
                    textViewCosto.SetTextColor(Android.Graphics.Color.ParseColor("#2C3E50"));
                }
                if (editText_PrecioVenta.Text == "")
                {
                    Validado++;                 
                    textViewPrecio.SetTextColor(Android.Graphics.Color.ParseColor("#E85434"));
                }
                else 
                {
                    textViewPrecio.SetTextColor(Android.Graphics.Color.ParseColor("#2C3E50"));
                }

                if (Validado == 0)
                {
                    adoInventario.Item = editText_Item.Text;
                    adoInventario.Nombre = editText_Nombre.Text;
                    adoInventario.Referencia = editText_Referencia.Text;
                    adoInventario.IVA = editText_iva.Text;
                    adoInventario.proveedor = planet_prompt.SelectedItem.ToString();
                    adoInventario.costo = editText_costo.Text;
                    adoInventario.precioventa = editText_PrecioVenta.Text;
                    var toast = adoInventario.AdoCreate();
                    if (toast == "Producto creado correctamente")
                    {
                        AdoInventario adoInventario = new AdoInventario();
                        var Productos = adoInventario.AdoSelectMaxItem();
                        var ItemMax = Convert.ToDouble(Productos[0]) + 1;
                        editText_Item.Text = ItemMax.ToString();
                        editText_Nombre.Text = "";
                        editText_Referencia.Text = "";
                        editText_iva.Text = "";
                        editText_costo.Text = "";
                        editText_PrecioVenta.Text = "";
                    }
                    Toast.MakeText(this, toast, ToastLength.Long).Show();
                }
                else
                {
                    Toast.MakeText(this, "Ingrese informacion requerida", ToastLength.Long).Show();
                }          
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