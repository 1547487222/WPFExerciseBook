using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace WPF.BehaviorDemo.Models
{
    public enum ShapeType
    {
        Ellipse,
        Rectangle,
        Triangle
    }
   public class ShapeModel
    {
        public string ShapeName { get; set; }
        public ShapeType  ShapeType { get; set; }

        public Brush ShapeColor  { get; set; }

        public Size Size { get; set; } = new Size(100, 100);
    }
}
