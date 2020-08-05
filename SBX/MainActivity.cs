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
using System.IO;

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
                AdoDataBaseSBX adoDataBaseSBX = new AdoDataBaseSBX();
                string toast = adoDataBaseSBX.CreateDataBase();
                var intent = new Intent(this, typeof(ProductoActivity));              
                StartActivity(intent);
            };
            Button btn_EliminarDB = FindViewById<Button>(Resource.Id.btn_eliminarDB);
            btn_EliminarDB.Click += (sender, e) =>
            {
                string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "adoDB_SBX.db3");
                File.Delete(dbPath);
            };

            Button btnInventario = FindViewById<Button>(Resource.Id.btn_inventario);
            btnInventario.Click += (sender, e) =>
            {
                ////crear base de datos
                AdoDataBaseSBX adoDataBaseSBX = new AdoDataBaseSBX();
                string toast = adoDataBaseSBX.CreateDataBase();
                var intent = new Intent(this, typeof(InventarioActivity));
                StartActivity(intent);
            };

            Button btnProveedor = FindViewById<Button>(Resource.Id.btn_proveedor);
            btnProveedor.Click += (sender, e) =>
            {
                ////crear base de datos
                AdoDataBaseSBX adoDataBaseSBX = new AdoDataBaseSBX();
                string toast = adoDataBaseSBX.CreateDataBase();
                var intent = new Intent(this, typeof(ProveedorActivity));
                StartActivity(intent);
            };

            Button btnCliente = FindViewById<Button>(Resource.Id.btn_cliente);
            btnCliente.Click += (sender, e) =>
            {
                ////crear base de datos
                AdoDataBaseSBX adoDataBaseSBX = new AdoDataBaseSBX();
                string toast = adoDataBaseSBX.CreateDataBase();
                var intent = new Intent(this, typeof(ClienteActivity));
                StartActivity(intent);
            };
        }   
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }


    }
}