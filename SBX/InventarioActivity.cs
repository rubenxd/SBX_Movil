using System;
using System.Collections.Concurrent;
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
        int Validado = 0;
        string ProductoModificar = "";
        string[] Producto;
        Boolean Guardar = true;
        string toast = "";
        AdoInventario adoInventario = new AdoInventario();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your application here
            SetContentView(Resource.Layout.activity_inventario);
            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar1);
            SetActionBar(toolbar);
            ActionBar.Title = "Inventario";

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

            var recibir = Intent;
            ProductoModificar = recibir.GetStringExtra("Item");
            if (ProductoModificar != null)
            {
                Guardar = false;
                Producto = ProductoModificar.Split("-");
                EditText editText_Item = FindViewById<EditText>(Resource.Id.editText_Item);
                editText_Item.Text = Producto[0];
                editText_Item.Enabled = false;
                EditText editText_Nombre = FindViewById<EditText>(Resource.Id.editText_Nombre);
                editText_Nombre.Text = Producto[1];
                adoInventario.Producto = Producto[0].Trim();
                var Productoss = adoInventario.AdoSelectIDTodos();
                string Cli = Productoss[0];
                string[] Cli2 = Cli.Split("--");
                EditText editText_Referencia = FindViewById<EditText>(Resource.Id.editText_Referencia);
                editText_Referencia.Text = Cli2[2];
                EditText editText_iva = FindViewById<EditText>(Resource.Id.editText_iva);
                editText_iva.Text = Cli2[3];
                int IdSeleccionProveedor = 0;
                string Seleccion = "";
                for (int i = 0; i < spinner.Count; i++)
                {
                    spinner.SetSelection(i);
                    Seleccion = spinner.SelectedItem.ToString().Substring(0, 1);
                    if (Seleccion == Cli2[4].Trim().Substring(0, 1))
                    {
                        IdSeleccionProveedor = Convert.ToInt32(Seleccion);
                    }
                }
                spinner.SetSelection(IdSeleccionProveedor);
                EditText editText_costo = FindViewById<EditText>(Resource.Id.editText_costo);
                editText_costo.Text = Cli2[5];
                EditText editText_PrecioVenta = FindViewById<EditText>(Resource.Id.editText_PrecioVenta);
                editText_PrecioVenta.Text = Cli2[6];                             
            }

            RadioButton radioButtonEntrada = FindViewById<RadioButton>(Resource.Id.rb_entradas);
            radioButtonEntrada.Checked = true;
            RadioButton radioButtonSalida = FindViewById<RadioButton>(Resource.Id.rb_Salidas);
            //Cargar maximo Item

            if (Guardar == true)
            {
                var Productos = adoInventario.AdoSelectMaxItem();
                EditText editText = FindViewById<EditText>(Resource.Id.editText_Item);

                if (Productos[0] != "")
                {
                    var ItemMax = Convert.ToDouble(Productos[0]) + 1;
                    editText.Text = ItemMax.ToString();
                }
                else
                {
                    editText.Text = "1";
                }
            }
                        
            //Button btnSalir = FindViewById<Button>(Resource.Id.btn_salir);
            //btnSalir.Click += (sender, e) =>
            //{
            //    var intent = new Intent(this, typeof(MainActivity));
            //    StartActivity(intent);
            //};
                     
            radioButtonEntrada.Click += (sender, e) =>
             {
                 radioButtonSalida.Checked = false;
             };
            radioButtonSalida.Click += (sender, e) =>
            {
                radioButtonEntrada.Checked = false;
            };

            //btnGuardar.Click += (sender, e) =>
            //{                      
            //};
        }
        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string toast = string.Format("The planet is {0}", spinner.GetItemAtPosition(e.Position));
            Toast.MakeText(this, toast, ToastLength.Long).Show();
        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.top_menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            RadioButton radioButtonEntrada = FindViewById<RadioButton>(Resource.Id.rb_entradas);
            RadioButton radioButtonSalida = FindViewById<RadioButton>(Resource.Id.rb_Salidas);
            EditText editText_Item = FindViewById<EditText>(Resource.Id.editText_Item);
            EditText editText_Nombre = FindViewById<EditText>(Resource.Id.editText_Nombre);
            EditText editText_Referencia = FindViewById<EditText>(Resource.Id.editText_Referencia);
            EditText editText_iva = FindViewById<EditText>(Resource.Id.editText_iva);
            Spinner planet_prompt = FindViewById<Spinner>(Resource.Id.spinner);
            EditText editText_costo = FindViewById<EditText>(Resource.Id.editText_costo);
            EditText editText_PrecioVenta = FindViewById<EditText>(Resource.Id.editText_PrecioVenta);
            EditText editText_Cantidad = FindViewById<EditText>(Resource.Id.editText_Cantidad);
            int Validado = 0;
            switch (item.TitleFormatted.ToString())
            {
                case "Save":
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
                        if (radioButtonEntrada.Checked)
                        {
                            adoInventario.movimiento = radioButtonEntrada.Text;
                        }
                        else
                        {
                            adoInventario.movimiento = radioButtonSalida.Text;
                        }

                        if (Guardar == true)
                        {
                            toast = adoInventario.AdoCreate();
                        }
                        else
                        {
                            toast = adoInventario.AdoEditar();
                        }                    

                        if (toast == "Producto creado correctamente" || toast == "Producto Editado correctamente")
                        {
                            AdoInventario adoInventario = new AdoInventario();
                            var Productos2 = adoInventario.AdoSelectMaxItem();
                            var ItemMax2 = Convert.ToDouble(Productos2[0]) + 1;
                            editText_Item.Text = ItemMax2.ToString();
                            editText_Nombre.Text = "";
                            editText_Referencia.Text = "";
                            editText_iva.Text = "";
                            editText_costo.Text = "";
                            editText_PrecioVenta.Text = "";
                            editText_Cantidad.Text = "";
                            radioButtonEntrada.Checked = true;
                            Toast.MakeText(this, toast, ToastLength.Long).Show();
                        }
                        else
                        {
                            Toast.MakeText(this, toast, ToastLength.Long).Show();
                        }                

                        if (toast == "Producto Editado correctamente")
                        {
                            this.FinishAndRemoveTask();
                            var intent2 = new Intent(this, typeof(ProductoActivity));
                            StartActivity(intent2);
                        }
                    }
                    else
                    {
                        Toast.MakeText(this, "Ingrese informacion requerida", ToastLength.Long).Show();
                    }

                    break;
                case "Consulta":
                    this.FinishAndRemoveTask();
                    var intent = new Intent(this, typeof(ProductoActivity));
                    StartActivity(intent);
                    break;
                case "Delete":
                    if (Guardar == false)
                    {
                        if (editText_Item.Text != "" || editText_Nombre.Text != "")
                        {
                            adoInventario.Item = editText_Item.Text;
                            toast = adoInventario.AdoEliminar();
                            Toast.MakeText(this, toast, ToastLength.Long).Show();
                            if (toast == "Producto eliminado correctamente")
                            {
                                this.FinishAndRemoveTask();
                                var intent2 = new Intent(this, typeof(ProductoActivity));
                                StartActivity(intent2);
                            }
                        }
                        else
                        {
                            Toast.MakeText(this, "Elije un Producto", ToastLength.Long).Show();
                        }
                    }
                    else
                    {
                        Toast.MakeText(this, "Elije un Producto", ToastLength.Long).Show();
                    }
                    break;
                case "Clear":                 
                    var Productos = adoInventario.AdoSelectMaxItem();
                    var ItemMax = Convert.ToDouble(Productos[0]) + 1;
                    editText_Item.Text = ItemMax.ToString();
                    editText_Nombre.Text = "";
                    editText_Referencia.Text = "";
                    editText_iva.Text = "";
                    editText_costo.Text = "";
                    editText_PrecioVenta.Text = "";
                    editText_Cantidad.Text = "";
                    radioButtonEntrada.Checked = true;
                    Guardar = true;
                    break;
                default:
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}