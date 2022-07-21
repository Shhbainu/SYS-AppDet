using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls; 
using MahApps.Metro.IconPacks;

namespace SYS_AppDet.UserControls
{
    /// <summary>
    /// Interaction logic for Menubutton.xaml
    /// </summary>
    public partial class Menubutton : UserControl
    {
        public Menubutton()
        {
            InitializeComponent();
        }

        public PackIconMaterialKind Icon
        {
            get { return (PackIconMaterialKind)GetValue(IconProperty); }
            set {SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty IconProperty = 
            DependencyProperty.Register("Icon", typeof(PackIconMaterialKind), typeof(Menubutton));


        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        public static readonly DependencyProperty IsActiveProperty = 
            DependencyProperty.Register("IsActive", typeof(bool), typeof(Menubutton));
    }
}
