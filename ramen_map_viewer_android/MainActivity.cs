using System;
using Android.App;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace ramen_map_viewer_android
{

    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    //public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    public class MainActivity : AppCompatActivity, IOnMapReadyCallback

    {

        GoogleMap gMap;

        public void OnMapReady(GoogleMap map)
        {
            gMap = map;
            // 初期設定
            gMap.UiSettings.MyLocationButtonEnabled = true;
            gMap.UiSettings.ZoomControlsEnabled = true;
            gMap.MoveCamera(CameraUpdateFactory.NewLatLngZoom(new LatLng(35.6586d, 139.7454d), 15f));
        }

        [Obsolete]
#pragma warning disable CS0809 // 旧形式のメンバーが、旧形式でないメンバーをオーバーライドします
        protected override void OnCreate(Bundle savedInstanceState)
#pragma warning restore CS0809 // 旧形式のメンバーが、旧形式でないメンバーをオーバーライドします
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.Main);

            var mapFragment = (MapFragment)FragmentManager.FindFragmentById(Resource.Id.map);
            mapFragment.GetMapAsync(this);

            //var map = mapFragment.GetMapAsync(this);  // <--1

            FindViewById<Button>(Resource.Id.buttonCenter).Click += (sender, e) =>
            {
                gMap.AnimateCamera(CameraUpdateFactory.NewLatLng(
                  new LatLng(35.68d, 139.76d)));  // 東京駅付近 <--2
            };

            FindViewById<Button>(Resource.Id.buttonBounds).Click += (sender, e) =>
            {
                var bounds = LatLngBounds.InvokeBuilder()
                  .Include(new LatLng(35.4661d, 139.6227d)) // 横浜駅
                  .Include(new LatLng(35.4713d, 139.6274d)) // 神奈川駅
                  .Build(); //<--1

                gMap.AnimateCamera(CameraUpdateFactory.NewLatLngBounds(bounds, 100)); //<--2
            };

            FindViewById<Button>(Resource.Id.buttonCamera).Click += (sender, e) =>
            {
                gMap.AnimateCamera(CameraUpdateFactory.NewCameraPosition(
                  new CameraPosition(
                    new LatLng(35.710063d, 139.8107d), // 東京スカイツリー（中心位置）
                    17f,     // ズームレベル
                    45f,     // 方位
                    30f)));  // 傾き
            };
        }
    }
}

