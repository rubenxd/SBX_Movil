using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using SBX.Ado;

namespace SBX
{
    [Activity(Label = "ViewClienteActivity")]
    public class ViewClienteActivity : ListActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your application here
            SetContentView(Resource.Layout.activity_viewCliente);
            Button button = FindViewById<Button>(Resource.Id.btn_buscar_cliente);         
            Button btnBuscar = FindViewById<Button>(Resource.Id.btn_buscar_cliente);
            btnBuscar.Click += ButtonClick;          
        }
        private void ButtonClick(object sender, EventArgs e)
        {
            AutoCompleteTextView textView = FindViewById<AutoCompleteTextView>(Resource.Id.autoCompleteCliente);        
            AdoCliente adoCliente = new AdoCliente();
            string cliente = "";
            if (textView.Text != "")
            {
                cliente = textView.Text;
            }
            adoCliente.Cliente = cliente;
            var Clientes = adoCliente.AdoSelectID();
            var adapter = new ArrayAdapter<String>(this, Resource.Layout.list_item, Clientes);
            textView.Adapter = adapter;           
            ListAdapter = new ArrayAdapter<string>(this, Resource.Layout.list_item, Clientes);
            ListView.TextFilterEnabled = true;
            ListView.ItemClick += delegate (object sender, AdapterView.ItemClickEventArgs args)
            {
                Toast.MakeText(Application, ((TextView)args.View).Text, ToastLength.Short).Show();
                var intent = new Intent(this, typeof(ClienteActivity));
                intent.PutExtra("Cliente", ((TextView)args.View).Text);
                StartActivity(intent);
            };
        }
    }
}