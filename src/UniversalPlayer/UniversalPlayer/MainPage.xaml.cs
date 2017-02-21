﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using UniversalPlayer.Managers;
using UniversalPlayer.Pages;
using UniversalPlayer.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UniversalPlayer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainViewModel MainViewModel { get; set; } = new MainViewModel();

        public MainPage()
        {
            this.InitializeComponent();

            DataContext = MainViewModel;
            Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!loaded)
            {
                Frame.Navigate(typeof(LoadingPage));
                loaded = true;
            }
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is List<SongViewModel>)
            {
                foreach (SongViewModel song in e.Parameter as List<SongViewModel>)
                {
                    MainViewModel.Songs.Add(song);
                    MainViewModel.MediaPlaybackList.Items.Add(await MediaManager.CreateMediaPlaybackItemAsync(song.FileHandle));
                }

                player.Source = MainViewModel.MediaPlaybackList;
            }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainViewModel.MediaPlaybackList.MoveTo((uint)(sender as ListView).SelectedIndex);
            player.Play();
        }

        private static bool loaded;
        private MediaPlayer player = new MediaPlayer();
    }
}
