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
    [Activity(Label = "ProveedorActivity")]
    public class ProveedorActivity : Activity
    {
        int Validado = 0;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your application here
            SetContentView(Resource.Layout.activity_proveedor);
            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar1);
            SetActionBar(toolbar);
            ActionBar.Title = "Proveedor";

            //Button btnSalir = FindViewById<Button>(Resource.Id.btn_salir);
            //btnSalir.Click += (sender, e) =>
            //{
            //    var intent = new Intent(this, typeof(MainActivity));
            //    StartActivity(intent);
            //};        
        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.top_menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            TextView textViewDNI = FindViewById<TextView>(Resource.Id.text_DNI);
            textViewDNI.SetTextColor(Android.Graphics.Color.ParseColor("#2C3E50"));
            TextView textViewNombre = FindViewById<TextView>(Resource.Id.text_Nombre);
            textViewNombre.SetTextColor(Android.Graphics.Color.ParseColor("#2C3E50"));
            TextView textViewCiudad = FindViewById<TextView>(Resource.Id.text_Ciudad);
            textViewCiudad.SetTextColor(Android.Graphics.Color.ParseColor("#2C3E50"));
            TextView textViewDireccion = FindViewById<TextView>(Resource.Id.text_Direccion);
            textViewDireccion.SetTextColor(Android.Graphics.Color.ParseColor("#2C3E50"));
            TextView textViewTelefono = FindViewById<TextView>(Resource.Id.text_telefono);
            textViewTelefono.SetTextColor(Android.Graphics.Color.ParseColor("#2C3E50"));
            TextView textViewCelular = FindViewById<TextView>(Resource.Id.text_celular);
            textViewCelular.SetTextColor(Android.Graphics.Color.ParseColor("#2C3E50"));
            TextView textViewEmail = FindViewById<TextView>(Resource.Id.text_Email);
            textViewEmail.SetTextColor(Android.Graphics.Color.ParseColor("#2C3E50"));
            TextView textViewSitioWeb = FindViewById<TextView>(Resource.Id.text_sitioWeb);
            textViewSitioWeb.SetTextColor(Android.Graphics.Color.ParseColor("#2C3E50"));

            EditText editText_DNI = FindViewById<EditText>(Resource.Id.editText_DNI);
            EditText editText_Nombre = FindViewById<EditText>(Resource.Id.editText_Nombre);
            EditText editText_Ciudad = FindViewById<EditText>(Resource.Id.editText_Ciudad);
            EditText editText_Direccion = FindViewById<EditText>(Resource.Id.editText_Direccion);
            EditText editText_Telefono = FindViewById<EditText>(Resource.Id.editText_telefono);
            EditText editText_Celular = FindViewById<EditText>(Resource.Id.editText_Celular);
            EditText editText_Email = FindViewById<EditText>(Resource.Id.editText_Email);
            EditText editText_SitioWeb = FindViewById<EditText>(Resource.Id.editText_StiioWeb);
            AdoProveedor adoProveedor = new AdoProveedor();
            switch (item.TitleFormatted.ToString())
            {
                case "Save":
                    Validado = 0;
                    //TextView textViewDNI = FindViewById<TextView>(Resource.Id.text_DNI);
                    if (editText_DNI.Text == "")
                    {
                        Validado++;
                        textViewDNI.SetTextColor(Android.Graphics.Color.ParseColor("#E85434"));
                    }
                    else
                    {
                        textViewDNI.SetTextColor(Android.Graphics.Color.ParseColor("#2C3E50"));
                    }
                    //TextView textViewNombre = FindViewById<TextView>(Resource.Id.text_Nombre);
                    if (editText_Nombre.Text == "")
                    {
                        Validado++;
                        textViewNombre.SetTextColor(Android.Graphics.Color.ParseColor("#E85434"));
                    }
                    else
                    {
                        textViewNombre.SetTextColor(Android.Graphics.Color.ParseColor("#2C3E50"));
                    }
                    //TextView textViewCelular = FindViewById<TextView>(Resource.Id.text_celular);
                    if (editText_Celular.Text == "")
                    {
                        Validado++;
                        textViewCelular.SetTextColor(Android.Graphics.Color.ParseColor("#E85434"));
                    }
                    else
                    {
                        textViewCelular.SetTextColor(Android.Graphics.Color.ParseColor("#2C3E50"));
                    }

                    if (Validado == 0)
                    {
                        adoProveedor.DNI = editText_DNI.Text;
                        adoProveedor.Nombre = editText_Nombre.Text;
                        adoProveedor.Ciudad = editText_Ciudad.Text;
                        adoProveedor.Direccion = editText_Direccion.Text;
                        adoProveedor.Telefono = editText_Telefono.Text;
                        adoProveedor.Celular = editText_Celular.Text;
                        adoProveedor.Email = editText_Email.Text;
                        adoProveedor.SitioWeb = editText_SitioWeb.Text;
                        var toast = adoProveedor.AdoCreate();
                        if (toast == "Proveedor creado correctamente")
                        {
                            editText_DNI.Text = "";
                            editText_Nombre.Text = "";
                            editText_Ciudad.Text = "";
                            editText_Direccion.Text = "";
                            editText_Telefono.Text = "";
                            editText_Celular.Text = "";
                            editText_Email.Text = "";
                            editText_SitioWeb.Text = "";
                            Toast.MakeText(this, toast, ToastLength.Long).Show();
                        }
                        else
                        {
                            Toast.MakeText(this, "Ingrese informacion requerida", ToastLength.Long).Show();
                        }
                    }
                    break;
                case "Edit":
                    Toast.MakeText(this, "edit", ToastLength.Long).Show();
                    break;
                default:
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}