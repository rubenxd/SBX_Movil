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
    [Activity(Label = "ViewProveedorActivity")]
    public class ViewProveedorActivity : ListActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.activity_viewProveedor);
            Button button = FindViewById<Button>(Resource.Id.btn_buscar_Proveedor);
            Button btnBuscar = FindViewById<Button>(Resource.Id.btn_buscar_Proveedor);
            btnBuscar.Click += ButtonClick;
        }
        private void ButtonClick(object sender, EventArgs e)
        {
            AutoCompleteTextView textView = FindViewById<AutoCompleteTextView>(Resource.Id.autoCompleteProveedor);
            AdoProveedor adoProveedor = new AdoProveedor();
            string Proveedor = "";
            if (textView.Text != "")
            {
                Proveedor = textView.Text;
            }
            adoProveedor.Proveedor = Proveedor;
            var Proveedores = adoProveedor.AdoSelectID();
            var adapter = new ArrayAdapter<String>(this, Resource.Layout.list_item, Proveedores);
            textView.Adapter = adapter;
            ListAdapter = new ArrayAdapter<string>(this, Resource.Layout.list_item, Proveedores);
            ListView.TextFilterEnabled = true;
            ListView.ItemClick += delegate (object sender, AdapterView.ItemClickEventArgs args)
            {
                Toast.MakeText(Application, ((TextView)args.View).Text, ToastLength.Short).Show();
                this.FinishAndRemoveTask();
                var intent = new Intent(this, typeof(ProveedorActivity));
                intent.PutExtra("Proveedor", ((TextView)args.View).Text);
                StartActivity(intent);
            };
        }
    }
}