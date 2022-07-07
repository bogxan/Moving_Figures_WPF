namespace Task1
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Resources;
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

    public partial class MainWindow : Window
    {
        private readonly List<MyFigure> figuresInProgram = new();
        private Point sizeOfCanvas = new();
        private readonly Random rnd = new();
        private int countTria = 0, countRect = 0, countCirc = 0;
        private TreeViewItem selectedNode = new();

        public MainWindow()
        {
            this.InitializeComponent();
            this.sizeOfCanvas = new Point
            {
                X = canvasFigures.ActualWidth,
                Y = canvasFigures.ActualHeight
            };
            App.LanguageChanged += LanguageChanged;
            CultureInfo currLang = App.Language;
            menuLanguage.Items.Clear();
            foreach (var lang in App.Languages)
            {
                MenuItem menuLang = new MenuItem();
                menuLang.Header = lang.DisplayName;
                menuLang.Tag = lang;
                menuLang.IsChecked = lang.Equals(currLang);
                menuLang.Click += ChangeLanguageClick;
                menuLanguage.Items.Add(menuLang);
            }
            startBtn.IsEnabled = false;
            stopBtn.IsEnabled = false;
        }

        private void LanguageChanged(Object sender, EventArgs e)
        {
            CultureInfo currLang = App.Language;
            foreach (MenuItem i in menuLanguage.Items)
            {
                CultureInfo ci = i.Tag as CultureInfo;
                i.IsChecked = ci != null && ci.Equals(currLang);
            }
        }

        private void ChangeLanguageClick(Object sender, EventArgs e)
        {
            MenuItem mi = sender as MenuItem;
            if (mi != null)
            {
                CultureInfo lang = mi.Tag as CultureInfo;
                if (lang != null)
                {
                    App.Language = lang;
                }
            }
            for (int i = 0; i < treeViewItemTriangle.Items.Count; i++)
            {
                treeViewItemTriangle.Items[i] = new TreeViewItem
                {
                    Header = FindResource("m_treeViewItemTria").ToString() + $" {i + 1}",
                    Name = $"Triangle_{i + 1}"
                };
            }
            for (int i = 0; i < treeViewItemRectangle.Items.Count; i++)
            {
                treeViewItemRectangle.Items[i] = new TreeViewItem
                {
                    Header = FindResource("m_treeViewItemRect").ToString() + $" {i + 1}",
                    Name = $"Rectangle_{i + 1}"
                };
            }
            for (int i = 0; i < treeViewItemCircle.Items.Count; i++)
            {
                treeViewItemCircle.Items[i] = new TreeViewItem
                {
                    Header = FindResource("m_treeViewItemCirc").ToString() + $" {i + 1}",
                    Name = $"Circle_{i + 1}"
                };
            }
            stopBtn.IsEnabled = false;
            startBtn.IsEnabled = false;
        }

        private async Task RandomAutoMove()
        {
            while (true)
            {
                foreach (var item in this.figuresInProgram)
                {
                    if (item.IsMoving)
                    {
                        double newLeft = this.rnd.Next(Convert.ToInt32(this.sizeOfCanvas.X - item.Shape.ActualWidth));
                        double newTop = this.rnd.Next(Convert.ToInt32(this.sizeOfCanvas.Y - item.Shape.ActualHeight));
                        DoubleAnimation animLeft = new(Canvas.GetLeft(item.Shape), newLeft, new Duration(TimeSpan.FromSeconds(0.5)));
                        DoubleAnimation animTop = new(Canvas.GetTop(item.Shape), newTop, new Duration(TimeSpan.FromSeconds(0.5)));
                        animLeft.EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseOut };
                        animTop.EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseOut };
                        item.Shape.BeginAnimation(Canvas.LeftProperty, animLeft, HandoffBehavior.SnapshotAndReplace);
                        item.Shape.BeginAnimation(Canvas.TopProperty, animTop, HandoffBehavior.SnapshotAndReplace);
                    }
                }

                await Task.Delay(500);
            }
        }

        private void AddTriangleBtn_Click(object sender, RoutedEventArgs e)
        {
            this.countTria++;
            TreeViewItem addNode = new();
            addNode.Header = FindResource("m_treeViewItemTria").ToString()+$" {this.countTria}";
            addNode.Name = $"Triangle_{this.countTria}";
            treeViewItemTriangle.Items.Add(addNode);
            MyTriangle triangle = new(50, 150, 150, 50, 250, 150);
            triangle.Name = $"Triangle_{this.countTria}";
            triangle.IsMoving = true;
            triangle.Draw();
            this.figuresInProgram.Add(triangle);
            Canvas.SetLeft(triangle.Shape, triangle.X1);
            Canvas.SetTop(triangle.Shape, triangle.Y1);
            canvasFigures.Children.Add(triangle.Shape);
            treeViewItemFigures.Items.Refresh();
        }

        private void AddCircleBtn_Click(object sender, RoutedEventArgs e)
        {
            this.countCirc++;
            TreeViewItem addNode = new();
            addNode.Header = FindResource("m_treeViewItemCirc").ToString()+$" {this.countCirc}";
            addNode.Name = $"Circle_{this.countCirc}";
            treeViewItemCircle.Items.Add(addNode);
            MyCircle circle = new(50, 100, 100);
            circle.Name = $"Circle_{this.countCirc}";
            circle.IsMoving = true;
            circle.Draw();
            this.figuresInProgram.Add(circle);
            Canvas.SetLeft(circle.Shape, circle.X1);
            Canvas.SetTop(circle.Shape, circle.Y1);
            canvasFigures.Children.Add(circle.Shape);
            treeViewItemFigures.Items.Refresh();
        }

        private void AddRectangleBtn_Click(object sender, RoutedEventArgs e)
        {
            this.countRect++;
            TreeViewItem addNode = new();
            addNode.Header = FindResource("m_treeViewItemRect").ToString()+$" {this.countRect}";
            addNode.Name = $"Rectangle_{this.countRect}";
            treeViewItemRectangle.Items.Add(addNode);
            MyRectangle rectangle = new(200, 200, 300, 300);
            rectangle.Name = $"Rectangle_{this.countRect}";
            rectangle.IsMoving = true;
            rectangle.Draw();
            this.figuresInProgram.Add(rectangle);
            Canvas.SetLeft(rectangle.Shape, rectangle.X1);
            Canvas.SetTop(rectangle.Shape, rectangle.Y1);
            canvasFigures.Children.Add(rectangle.Shape);
            treeViewItemFigures.Items.Refresh();
        }

        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            if (selectedNode != null)
            {
                for (int i = 0; i < figuresInProgram.Count; i++)
                {
                    if (figuresInProgram[i].Name == selectedNode.Name)
                    {
                        if (!figuresInProgram[i].IsMoving)
                        {
                            figuresInProgram[i].IsMoving = true;
                        }
                        stopBtn.IsEnabled = true;
                        startBtn.IsEnabled = false;
                    }
                }
            }
        }

        private void StopBtn_Click(object sender, RoutedEventArgs e)
        {
            if (selectedNode != null)
            {
                for (int i = 0; i < figuresInProgram.Count; i++)
                {
                    if (figuresInProgram[i].Name == selectedNode.Name)
                    {
                        if (figuresInProgram[i].IsMoving)
                        {
                            figuresInProgram[i].IsMoving = false;
                        }
                        stopBtn.IsEnabled = false;
                        startBtn.IsEnabled = true;
                    }
                }
            }
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            for (int i = 0; i < figuresInProgram.Count; i++)
            {
                figuresInProgram[i].Shape.Stroke = new SolidColorBrush(Colors.Black);
                figuresInProgram[i].Shape.StrokeThickness = 1;
            }
            selectedNode = e.NewValue as TreeViewItem;
            if (selectedNode != null)
            {
                for (int i = 0; i < figuresInProgram.Count; i++)
                {
                    if (figuresInProgram[i].Name == selectedNode.Name)
                    {
                        figuresInProgram[i].Shape.Stroke = new SolidColorBrush(Colors.Yellow);
                        figuresInProgram[i].Shape.StrokeThickness = 5;
                        if (figuresInProgram[i].IsMoving)
                        {
                            stopBtn.IsEnabled = true;
                            startBtn.IsEnabled = false;
                        }
                        else
                        {
                            stopBtn.IsEnabled = false;
                            startBtn.IsEnabled = true;
                        }
                    }
                }
            }
        }

        private void CanvasFigures_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.sizeOfCanvas = new Point
            {
                X = canvasFigures.ActualWidth,
                Y = canvasFigures.ActualHeight
            };
        }

        private async void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            await this.RandomAutoMove();
        }
    }
}