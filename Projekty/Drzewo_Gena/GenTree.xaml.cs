using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Drzewo_Gena
{
    /// <summary>
    /// Interaction logic for GenTree.xaml
    /// </summary>
    public partial class GenTree : Window
    {
        private TreeViewItem treeItem(Person person)
        {
            var tree = new TreeViewItem() { Header = person.Imie };
            foreach (var child in person.Children)
            {
                tree.Items.Add(treeItem(child));
            }
            return tree;
        }
        public GenTree(Person Data)
        {
            {
                InitializeComponent();
                Tree.Items.Add(treeItem(Data));
            }
        }
    }
}
