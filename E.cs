using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Ext
{
	public class E
	{
		#region + Fields +

		public static readonly DependencyProperty IconProperty;
		public static readonly DependencyProperty IconSizeProperty;
		public static readonly DependencyProperty OrientationProperty;

		#endregion
		#region + Ctor +

		static E()
		{
			//register attached dependency property
			var metadata = new FrameworkPropertyMetadata(null);
			IconProperty = DependencyProperty.RegisterAttached("Icon",
																																																						typeof(FrameworkElement),
																																																						typeof(E), metadata);

			metadata = new FrameworkPropertyMetadata(default(Orientation));
			OrientationProperty = DependencyProperty.RegisterAttached("Orientation",
																																																						typeof(Orientation),
																																																						typeof(E), metadata);

			metadata = new FrameworkPropertyMetadata(16d);
			IconSizeProperty = DependencyProperty.RegisterAttached("IconSize",
																																																						typeof(double),
																																																						typeof(E), metadata);
		}

		#endregion
		#region + Logic +

		public static FrameworkElement GetIcon(DependencyObject obj)
		{
			return (FrameworkElement)obj.GetValue(IconProperty);
		}

		public static double GetIconSize(DependencyObject obj)
		{
			return (double)obj.GetValue(IconSizeProperty);
		}

		public static Orientation GetOrientation(DependencyObject obj)
		{
			return (Orientation)obj.GetValue(OrientationProperty);
		}

		public static void SetIcon(DependencyObject obj, object value)
		{
			obj.SetValue(IconProperty, value);
		}

		public static void SetIconSize(DependencyObject obj, double value)
		{
			obj.SetValue(IconSizeProperty, value);
		}

		public static void SetOrientation(DependencyObject obj, Orientation value)
		{
			obj.SetValue(OrientationProperty, value);
		}

		#endregion
	}
}