﻿using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    public class MenuViewModel : ViewModelBase
    {
        private readonly MenuModel _menu;
        public MenuViewModel(MenuModel menu)
        {
            _menu = menu;
        }

        public string Id => _menu.Id;
        public string Title => _menu.Title;
        public string Description => _menu.Description;
        public string Source => _menu.Source;

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    RaisePropertyChanged(() => IsSelected);
                }
            }
        }

        private bool _expanded;
        public bool Expanded
        {
            get { return _expanded; }
            set
            {
                if (_expanded != value)
                {
                    _expanded = value;
                    RaisePropertyChanged(() => Expanded);
                }
            }
        }

        public ObservableCollection<MenuViewModel> Children
        {
            get
            {
                if (_menu.Children == null) return null;

                var children = _menu.Children.Select(i => new MenuViewModel(i));
                return new ObservableCollection<MenuViewModel>(children);
            }
        }
    }
}
