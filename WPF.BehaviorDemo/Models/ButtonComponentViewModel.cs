using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using WPF.BehaviorDemo.ViewComponents;

namespace WPF.BehaviorDemo.Models
{
    public partial class ButtonComponentViewModel : ComponentViewModelBase
    {
        public class ButtonOptions
        {
            public string ButtonText { get; set; }

            public double ButtonWidth { get; set; }

            public double ButtonHeight { get; set; }
            public System.Windows.Media.Color ForgroundColor { get; set; }

            public System.Windows.Media.Color BackgroundColor { get; set; }
        }
        [ObservableProperty]
        private string _buttonText;
        [ObservableProperty]
        private double _buttonWidth;
        [ObservableProperty]
        private double _buttonHeight;
        public override void InitializeComponent()
        {
            this.ViewOptions = new ButtonOptions();
        }

        public override void HandleViewOptionsChanged(object viewOptions)
        {
            //
            if (viewOptions is ButtonOptions options)
            {
                this.ButtonText = options.ButtonText;
                this.ButtonWidth = options.ButtonWidth;
                this.ButtonHeight = options.ButtonHeight;
                //this.ForgroundColor = options.ForgroundColor;
                //this.BackgroundColor = options.BackgroundColor;
            }
        }
    }
}
