﻿using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Views;
using Android.Widget;
using MoneyManager.Foundation;

namespace MoneyManager.Droid.Activities
{
    [Activity(Label = "MoneyManager", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private string title;
        private string drawerTitle;
        private SlidingPaneLayout slidingLayout;
        private ListView menuListView;

        private readonly List<string> menuItems = new List<string>
        {
            Strings.AccountsLabel,
            Strings.StatisticTitle,
            Strings.BackupLabel,
            Strings.SettingsLabel,
            Strings.AboutLabel
        };

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            slidingLayout = FindViewById<SlidingPaneLayout>(Resource.Id.main_layout);
            menuListView = FindViewById<ListView>(Resource.Id.left_pane);

            slidingLayout.PanelOpened += (sender, e) =>
            {
                ActionBar.SetHomeButtonEnabled(false);
                ActionBar.SetDisplayHomeAsUpEnabled(false);
                ActionBar.Title = drawerTitle;
            };

            slidingLayout.PanelClosed += (sender, e) =>
            {
                ActionBar.SetDisplayHomeAsUpEnabled(true);
                ActionBar.SetHomeButtonEnabled(true);
                ActionBar.Title = title;
            };

            menuListView.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, menuItems);
            menuListView.ItemClick += NavigationClick;

            ActionBar.SetDisplayHomeAsUpEnabled(true);
            ActionBar.SetHomeButtonEnabled(true);
            title = menuItems[0];
            drawerTitle = Strings.MenuTitle;

            slidingLayout.ViewTreeObserver.GlobalLayout += FirstLayoutListener;
        }

        private void NavigationClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            title = menuItems[e.Position];

            switch (e.Position)
            {
                case 0:
                    break;

                case 1:
                    break;

                case 2:
                    break;
            }
            slidingLayout.ClosePane();
        }

        private void FirstLayoutListener(object sender, EventArgs e)
        {
            if (slidingLayout.IsSlideable && !slidingLayout.IsOpen)
            {
                ActionBar.SetDisplayHomeAsUpEnabled(true);
                ActionBar.SetHomeButtonEnabled(true);
                ActionBar.Title = title;
            }
            else
            {
                ActionBar.SetDisplayHomeAsUpEnabled(false);
                ActionBar.SetHomeButtonEnabled(false);
                ActionBar.Title = drawerTitle;
            }

            slidingLayout.ViewTreeObserver.GlobalLayout -= FirstLayoutListener;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home && !slidingLayout.IsOpen)
            {
                slidingLayout.OpenPane();
                return true;
            }

            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    slidingLayout.OpenPane();
                    return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

    }
}