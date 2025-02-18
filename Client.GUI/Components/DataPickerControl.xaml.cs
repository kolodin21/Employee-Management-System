using System.Windows;
using System.Windows.Controls;

namespace Client.GUI.Components
{
    public partial class DataPickerControl : UserControl
    {
        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register(nameof(Label), typeof(string), typeof(DataPickerControl));

        public static readonly DependencyProperty DatePickerProperty =
            DependencyProperty.Register(nameof(SelectedDate), typeof(DateTime), typeof(DataPickerControl));

        public string Label
        {
            get => (string)GetValue(LabelProperty);
            set => SetValue(LabelProperty, value);
        }
        public DateTime? SelectedDate
        {
            get => (DateTime?)GetValue(DatePickerProperty);
            set => SetValue(DatePickerProperty, value);
        }

        public DataPickerControl()
        {
            InitializeComponent();
        }
    }
}
