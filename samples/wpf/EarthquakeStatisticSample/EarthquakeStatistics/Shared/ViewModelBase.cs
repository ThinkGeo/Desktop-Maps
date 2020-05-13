using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace ThinkGeo.MapSuite.EarthquakeStatistics
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ViewModelBase()
        {
        }

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected virtual void RaisePropertyChanged<T>(Expression<Func<T>> expreesion)
        {
            RaisePropertyChanged(((MemberExpression)expreesion.Body).Member.Name);
        }
    }
}