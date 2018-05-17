using Android.App;
using Android.OS;
using Android.Util;

namespace App4
{
    [Activity(Label = "App4", MainLauncher = true)]
    public class MainActivity : Activity
    {
        public const string TAG = "MainActivity";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            if (Intent.Extras != null)
            {
                foreach (var key in Intent.Extras.KeySet())
                {
                    if (key != null)
                    {
                        var value = Intent.Extras.GetString(key);
                        Log.Debug(TAG, "Key: {0} Value: {1}", key, value);
                    }
                }
            }

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
        }
    }
}

