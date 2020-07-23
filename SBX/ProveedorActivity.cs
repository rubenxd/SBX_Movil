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

namespace SBX
{
    [Activity(Label = "ProveedorActivity")]
    public class ProveedorActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.activity_proveedor);

            Button btnSalir = FindViewById<Button>(Resource.Id.btn_salir);
            btnSalir.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
            };

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
            int Validado = 0;
            Button btnGuardar = FindViewById<Button>(Resource.Id.btn_guardar);
            btnGuardar.Click += (sender, e) =>
            {
                Validado = 0;
                TextView textViewDNI = FindViewById<TextView>(Resource.Id.text_DNI);
                if (editText_DNI.Text == "")
                {
                    Validado++;
                    textViewDNI.SetTextColor(Android.Graphics.Color.ParseColor("#E85434"));
                }
                else
                {
                    textViewDNI.SetTextColor(Android.Graphics.Color.ParseColor("#2C3E50"));
                }
                TextView textViewNombre = FindViewById<TextView>(Resource.Id.text_Nombre);
                if (editText_Nombre.Text == "")
                {
                    Validado++;
                    textViewNombre.SetTextColor(Android.Graphics.Color.ParseColor("#E85434"));
                }
                else
                {
                    textViewNombre.SetTextColor(Android.Graphics.Color.ParseColor("#2C3E50"));
                }               
                TextView textViewCelular = FindViewById<TextView>(Resource.Id.text_celular);
                if (editText_Celular.Text == "")
                {
                    Validado++;
                    textViewCelular.SetTextColor(Android.Graphics.Color.ParseColor("#E85434"));
                }
                else
                {
                    textViewCelular.SetTextColor(Android.Graphics.Color.ParseColor("#2C3E50"));
                }
            
            };
        }
    }
}