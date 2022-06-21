using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace task1
{
    public partial class MainWindow : Window
    {
        List<MyFigure> figuresInProgram = new List<MyFigure>();
        int counterCircles = 0, counterRectangles = 0, counterTriangles = 0;
        Random rnd = new Random();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void addTriangleBtn_Click(object sender, RoutedEventArgs e)
        {
            counterTriangles++;
            TreeViewItem addNode = new TreeViewItem();
            addNode.Header = "Triangle " + counterTriangles;
            treeViewItemTriangle.Items.Add(addNode);
            MyTriangle triangle = new MyTriangle(50, 150, 150, 50, 250, 150);
            triangle.Draw();
            figuresInProgram.Add(triangle);
            Canvas.SetLeft(triangle.shape, triangle.x1);
            Canvas.SetTop(triangle.shape, triangle.y1);
            canvasFigures.Children.Add(triangle.shape);
        }

        private void addCircleBtn_Click(object sender, RoutedEventArgs e)
        {
            counterCircles++;
            TreeViewItem addNode = new TreeViewItem();
            addNode.Header = "Circle " + counterCircles;
            treeViewItemCircle.Items.Add(addNode);
            MyCircle circle = new MyCircle(50, 100, 100);
            circle.Draw();
            figuresInProgram.Add(circle);
            Canvas.SetLeft(circle.shape, circle.x1);
            Canvas.SetTop(circle.shape, circle.y1);
            canvasFigures.Children.Add(circle.shape);
        }

        private void addRectangleBtn_Click(object sender, RoutedEventArgs e)
        {
            counterRectangles++;
            TreeViewItem addNode = new TreeViewItem();
            addNode.Header = "Rectangle " + counterRectangles;
            treeViewItemRectangle.Items.Add(addNode);
            MyRectangle rectangle = new MyRectangle(200, 200, 300, 300);
            rectangle.Draw();
            figuresInProgram.Add(rectangle);
            Canvas.SetLeft(rectangle.shape, rectangle.x1);
            Canvas.SetTop(rectangle.shape, rectangle.y1);
            canvasFigures.Children.Add(rectangle.shape);
        }

        private async Task RandomAutoMove()
        {
            while (true)
            {
                foreach (var item in figuresInProgram)
                {
                    double newLeft = rnd.Next(Convert.ToInt32(canvasFigures.ActualWidth - item.shape.ActualWidth));
                    double newTop = rnd.Next(Convert.ToInt32(canvasFigures.ActualHeight - item.shape.ActualHeight));
                    DoubleAnimation animLeft = new DoubleAnimation(Canvas.GetLeft(item.shape), newLeft, new Duration(TimeSpan.FromSeconds(0.7)));
                    DoubleAnimation animTop = new DoubleAnimation(Canvas.GetTop(item.shape), newTop, new Duration(TimeSpan.FromSeconds(0.7)));
                    animLeft.EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseOut };
                    animTop.EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseOut };
                    item.shape.BeginAnimation(Canvas.LeftProperty, animLeft, HandoffBehavior.SnapshotAndReplace);
                    item.shape.BeginAnimation(Canvas.TopProperty, animTop, HandoffBehavior.SnapshotAndReplace);
                }
                await Task.Delay(500);
            }
        }

        private async void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            await RandomAutoMove();
        }
    }
}