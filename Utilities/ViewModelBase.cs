using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Blazorify.Bootstrap.Utilities {
	internal abstract class ViewModelBase : INotifyPropertyChanged {

		public event PropertyChangedEventHandler? PropertyChanged;

		public void OnPropertyChanged([CallerMemberName] String? propertyName = null) {
			this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public Boolean SetProperty<T>(ref T storage, T value, [CallerMemberName] String? propertyName = null) {
			if (Equals(storage, value)) {
				return false;
			}

			storage = value;

			this.OnPropertyChanged(propertyName);

			return true;
		}
	}
}
