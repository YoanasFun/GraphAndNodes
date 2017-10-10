using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.GraphViewerGdi;
using Microsoft.Msagl;


namespace Demo.WpfGraphApplication
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            GenerateGraph();

            gViewer.VisibleChanged +=
            new EventHandler(gViewer_SelectionChanged);

        }

        private static void CreateSourceNode(Node a)
        {
            a.Attr.Shape = Microsoft.Msagl.Drawing.Shape.Box;
            a.Attr.XRadius = 3;
            a.Attr.YRadius = 3;
            a.Attr.FillColor = Microsoft.Msagl.Drawing.Color.Green;
            a.Attr.LineWidth = 10;

            a.UserData = "UserData present";
        }

        private void CreateTargetNode(Node a)
        {
            a.Attr.Shape = Microsoft.Msagl.Drawing.Shape.DoubleCircle;
            a.Attr.FillColor = Microsoft.Msagl.Drawing.Color.LightGray;

            a.Attr.LabelMargin = -4;

            a.UserData = "UserData present";
        }


        private void GenerateGraph()
        {
            Graph graph = new Graph("graph");
            Edge edge = (Edge)graph.AddEdge("S24", "27");
            edge.LabelText = "Edge Label Test";

            graph.AddEdge("S24", "25");
            edge = graph.AddEdge("S1", "10") as Edge;

            edge.LabelText = "Init";
            edge.Attr.ArrowheadAtTarget = ArrowStyle.Tee;
            //  edge.Attr.Weight = 10;
            edge = graph.AddEdge("S1", "2") as Edge;
            // edge.Attr.Weight = 10;
            graph.AddEdge("S35", "36");
            graph.AddEdge("S35", "43");
            graph.AddEdge("S30", "31");
            graph.AddEdge("S30", "33");
            graph.AddEdge("9", "42");
            graph.AddEdge("9", "T1");
            graph.AddEdge("25", "T1");
            graph.AddEdge("25", "26");
            graph.AddEdge("27", "T24");
            graph.AddEdge("2", "3");
            graph.AddEdge("2", "16");
            graph.AddEdge("2", "17");
            graph.AddEdge("2", "T1");
            graph.AddEdge("2", "18");
            graph.AddEdge("10", "11");
            graph.AddEdge("10", "14");
            graph.AddEdge("10", "T1");
            graph.AddEdge("10", "13");
            graph.AddEdge("10", "12");
            graph.AddEdge("31", "T1");
            edge = (Edge)graph.AddEdge("31", "32");
            edge.Attr.ArrowheadAtTarget = ArrowStyle.Tee;
            edge.Attr.LineWidth = 10;

            edge = (Edge)graph.AddEdge("33", "T30");
            edge.Attr.LineWidth = 15;
            edge.Attr.AddStyle(Microsoft.Msagl.Drawing.Style.Dashed);
            graph.AddEdge("33", "34");
            graph.AddEdge("42", "4");
            graph.AddEdge("26", "4");
            graph.AddEdge("3", "4");
            graph.AddEdge("16", "15");
            graph.AddEdge("17", "19");
            graph.AddEdge("18", "29");
            graph.AddEdge("11", "4");
            graph.AddEdge("14", "15");
            graph.AddEdge("37", "39");
            graph.AddEdge("37", "41");
            graph.AddEdge("37", "38");
            graph.AddEdge("37", "40");
            graph.AddEdge("13", "19");
            graph.AddEdge("12", "29");
            graph.AddEdge("43", "38");
            graph.AddEdge("43", "40");
            graph.AddEdge("36", "19");
            graph.AddEdge("32", "23");
            graph.AddEdge("34", "29");
            graph.AddEdge("39", "15");
            graph.AddEdge("41", "29");
            graph.AddEdge("38", "4");
            graph.AddEdge("40", "19");
            graph.AddEdge("4", "5");
            graph.AddEdge("19", "21");
            graph.AddEdge("19", "20");
            graph.AddEdge("19", "28");
            graph.AddEdge("5", "6");
            graph.AddEdge("5", "T35");
            graph.AddEdge("5", "23");
            graph.AddEdge("21", "22");
            graph.AddEdge("20", "15");
            graph.AddEdge("28", "29");
            graph.AddEdge("6", "7");
            graph.AddEdge("15", "T1");
            graph.AddEdge("22", "23");
            graph.AddEdge("22", "T35");
            graph.AddEdge("29", "T30");
            graph.AddEdge("7", "T8");
            graph.AddEdge("23", "T24");
            graph.AddEdge("23", "T1");

            //node.LabelText = "Label Test";
            CreateSourceNode(graph.FindNode("S1") as Node);
            CreateSourceNode(graph.FindNode("S24") as Node);
            CreateSourceNode(graph.FindNode("S35") as Node);


            CreateTargetNode(graph.FindNode("T24") as Node);
            CreateTargetNode(graph.FindNode("T1") as Node);
            CreateTargetNode(graph.FindNode("T30") as Node);
            CreateTargetNode(graph.FindNode("T8") as Node);

            //layout the graph and draw it
            this.gViewer.Graph = graph;
            //this.propertyGrid1.SelectedObject = graph;

        }

        object selectedObjectAttr;
        object selectedObject;
        void gViewer_SelectionChanged(object sender, EventArgs e)
        {

            if (selectedObject != null)
            {
                if (selectedObject is Edge)
                    (selectedObject as Edge).Attr = selectedObjectAttr as EdgeAttr;
                else if (selectedObject is Node)
                    (selectedObject as Node).Attr = selectedObjectAttr as NodeAttr;

                selectedObject = null;
            }

            if (gViewer.SelectedObject == null)
            {
                label1.Content = "No object under the mouse";

            }
            else
            {
                selectedObject = gViewer.SelectedObject;
                Edge edge = selectedObject as Edge;
                if (edge != null)
                {
                    selectedObjectAttr = edge.Attr.Clone();
                    edge.Attr.Color = Microsoft.Msagl.Drawing.Color.Magenta;

                    label1.Content = edge.LabelText;
                }
                else if (selectedObject is Node)
                {
                    Node node = ((Node)selectedObject);

                    selectedObjectAttr = node.Attr.Clone();
                    node.Attr.Color = Microsoft.Msagl.Drawing.Color.Magenta;

                    label1.Content = node.LabelText;

                    if (node.UserData != null)
                    {
                        label1.Content = label1.Content + " " + node.UserData.ToString();
                    }
                }
            }
        }

        private void gViewer_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
