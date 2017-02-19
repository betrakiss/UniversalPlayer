﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace UniversalPlayer.ViewModels
{
    public class MainViewModel
    {
        public ObservableCollection<SongViewModel> Songs { get; set; } = new ObservableCollection<SongViewModel>();
    }
}
