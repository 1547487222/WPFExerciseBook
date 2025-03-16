using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF.BehaviorDemo.ViewComponents;

namespace WPF.BehaviorDemo.ComponentViews
{
    /// <summary>
    /// TableComponentView.xaml 的交互逻辑
    /// </summary>
    public partial class TableComponentView : IComponentView
    {
        public TableComponentView()
        {
            InitializeComponent();
            DataContext = this;


        }


        public ObservableCollection<TableItem> TableData { get; set; } = new ObservableCollection<TableItem>() 
        {
            new TableItem(){Name="张三",Age="18",Sex="男",Score=100 },
            new TableItem(){Name="李四",Age="19",Sex="男",Score=90 },
            new TableItem(){Name="王五",Age="20",Sex="男",Score=80 },
            new TableItem(){Name="赵六",Age="21",Sex="男",Score=70 },
            new TableItem(){Name="钱七",Age="22",Sex="男",Score=60 },
        };

        public class TableItem
        {
            public string Name { get; set; }

            public string Age { get; set; }

            public string Sex { get; set; }

            public double Score { get; set; }
        }
    }
}
