using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Util;
using System.Threading.Tasks;
using Android.Content;
using Android.Widget;
using System;
using SBX.Ado;

namespace SBX
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            Button btnProducto = FindViewById<Button>(Resource.Id.btn_producto);
            btnProducto.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(ProductoActivity));              
                StartActivity(intent);
            };

            Button btnInventario = FindViewById<Button>(Resource.Id.btn_inventario);
            btnInventario.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(InventarioActivity));
                StartActivity(intent);
            };
            //crear base de datos
            AdoDataBaseSBX adoDataBaseSBX = new AdoDataBaseSBX();
            string toast = adoDataBaseSBX.CreateDataBase();
        }   
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }


    }
}