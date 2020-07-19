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
    [Activity(Label = "Producto")]
    public class ProductoActivity : Activity
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
        }
    }
}