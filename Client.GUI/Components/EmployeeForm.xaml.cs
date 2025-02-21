using System.Windows;
using System.Windows.Controls;

namespace Client.GUI.Components
{
    public partial class EmployeeForm : UserControl
    {
        public EmployeeForm()
        {
            InitializeComponent();
        }
        public static readonly DependencyProperty FormTitleProperty =
            DependencyProperty.Register(nameof(FormTitle), typeof(string), typeof(EmployeeForm), new PropertyMetadata(string.Empty));

        public string FormTitle
        {
            get => (string)GetValue(FormTitleProperty);
            set => SetValue(FormTitleProperty, value);
        }
    }
}
