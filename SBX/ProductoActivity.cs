using System;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using SBX.Ado;

namespace SBX
{
    [Activity(Label = "Producto")]
    public class ProductoActivity : ListActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.activity_producto);

            AdoInventario adoInventario = new AdoInventario();
            AutoCompleteTextView textView = FindViewById<AutoCompleteTextView>(Resource.Id.autoCompleteProducto);
            var Productos = adoInventario.AdoSelect();
           
            var adapter = new ArrayAdapter<String>(this, Resource.Layout.list_item, Productos);
            textView.Adapter = adapter;

            textView.ItemClick += delegate (object sender, AdapterView.ItemClickEventArgs args)
            {
                Toast.MakeText(Application,"hola", ToastLength.Short).Show();
            };
      
            ListAdapter = new ArrayAdapter<string>(this, Resource.Layout.list_item, Productos);
            ListView.TextFilterEnabled = true;

            ListView.ItemClick += delegate (object sender, AdapterView.ItemClickEventArgs args)
            {
                Toast.MakeText(Application, ((TextView)args.View).Text, ToastLength.Short).Show();
            };

            Button btnBuscar = FindViewById<Button>(Resource.Id.btn_buscar);
            btnBuscar.Click += ButtonClick;
        }
        private void ButtonClick(object sender, EventArgs e)
        {
            AdoInventario adoInventarioID = new AdoInventario();
            adoInventarioID.Item = "1";
            var Productos = adoInventarioID.AdoSelectID();
            AutoCompleteTextView textView = FindViewById<AutoCompleteTextView>(Resource.Id.autoCompleteProducto);
            var adapter = new ArrayAdapter<String>(this, Resource.Layout.list_item, Productos);
            textView.Adapter = adapter;
        }
    }
}