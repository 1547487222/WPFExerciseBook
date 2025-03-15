using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using WPF.BehaviorDemo.Models;

namespace WPF.BehaviorDemo
{
  public  class MainWindowModel
    {
        public ObservableCollection<ShapeModel> Shapes { get; set; } = [];

        public MainWindowModel()
        {
            Shapes.Add(new ShapeModel { ShapeName="Ellipse", ShapeColor= Brushes.Purple, ShapeType= ShapeType.Ellipse });
            Shapes.Add(new ShapeModel { ShapeName = "Rectangle", ShapeColor = Brushes.Blue, ShapeType = ShapeType.Rectangle });
            Shapes.Add(new ShapeModel { ShapeName="Triangle", ShapeColor= Brushes.Red, ShapeType= ShapeType.Triangle });

        }
    }
}
