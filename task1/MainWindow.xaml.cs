namespace Task1
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Resources;
    using System.Runtime.Serialization.Formatters.Binary;
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
    using System.Xml.Serialization;
    using Task1.Models;

    public partial class MainWindow : Window
    {
        private readonly List<MyFigure> figuresInProgram = new();
        private Point sizeOfCanvas = new();
        private readonly Random rnd = new();
        private int countTria = 0, countRect = 0, countCirc = 0, countAll = 0;
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
                        //item.Move(this.sizeOfCanvas);
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
            this.countAll++;
            TreeViewItem addNode = new();
            addNode.Header = FindResource("m_treeViewItemTria").ToString()+$" {this.countTria}";
            addNode.Name = $"Triangle_{this.countTria}";
            treeViewItemTriangle.Items.Add(addNode);
            MyTriangle triangle = new(50, 150, 150, 50, 250, 150);
            triangle.Id = countAll;
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
            this.countAll++;
            TreeViewItem addNode = new();
            addNode.Header = FindResource("m_treeViewItemCirc").ToString()+$" {this.countCirc}";
            addNode.Name = $"Circle_{this.countCirc}";
            treeViewItemCircle.Items.Add(addNode);
            MyCircle circle = new(50, 100, 100);
            circle.Id = countAll;
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
            this.countAll++;
            TreeViewItem addNode = new();
            addNode.Header = FindResource("m_treeViewItemRect").ToString()+$" {this.countRect}";
            addNode.Name = $"Rectangle_{this.countRect}";
            treeViewItemRectangle.Items.Add(addNode);
            MyRectangle rectangle = new(200, 200, 300, 300);
            rectangle.Id = countAll;
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

        private void btnLoadBin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream fs = File.Open("figures.dat", FileMode.Open);
                object obj = formatter.Deserialize(fs);
                List<MyFigure> figures = (List<MyFigure>)obj;
                if (figures.Count > 0)
                {
                    countAll = countCirc = countRect = countTria = 0;
                    treeViewItemCircle.Items.Clear();
                    treeViewItemRectangle.Items.Clear();
                    treeViewItemTriangle.Items.Clear();
                    canvasFigures.Children.Clear();
                    figuresInProgram.Clear();
                    figuresInProgram.AddRange(figures);
                    for (int i = 0; i < figuresInProgram.Count; i++)
                    {
                        if (figuresInProgram[i].Name.Contains("Triangle"))
                        {
                            MyTriangle triangle = figuresInProgram[i] as MyTriangle;
                            triangle.Draw();
                            TreeViewItem addNode = new();
                            string[] prts = triangle.Name.Split("_");
                            countTria = int.Parse(prts[1]);
                            countAll = triangle.Id;
                            addNode.Header = FindResource("m_treeViewItemTria").ToString() + $" {countTria}";
                            addNode.Name = triangle.Name;
                            Canvas.SetLeft(triangle.Shape, triangle.X1);
                            Canvas.SetTop(triangle.Shape, triangle.Y1);
                            canvasFigures.Children.Add(triangle.Shape);
                            treeViewItemTriangle.Items.Add(addNode);
                            treeViewItemTriangle.Items.Refresh();
                            treeViewItemFigures.Items.Refresh();
                        }
                        else if (figuresInProgram[i].Name.Contains("Circle"))
                        {
                            MyCircle circle = figuresInProgram[i] as MyCircle;
                            circle.Draw();
                            TreeViewItem addNode = new();
                            string[] prts = circle.Name.Split("_");
                            countCirc = int.Parse(prts[1]);
                            countAll = circle.Id;
                            addNode.Header = FindResource("m_treeViewItemCirc").ToString() + $" {countCirc}";
                            addNode.Name = $"Circle_{this.countCirc}";
                            Canvas.SetLeft(circle.Shape, circle.X1);
                            Canvas.SetTop(circle.Shape, circle.Y1);
                            canvasFigures.Children.Add(circle.Shape);
                            treeViewItemCircle.Items.Add(addNode);
                            treeViewItemCircle.Items.Refresh();
                            treeViewItemFigures.Items.Refresh();
                        }
                        else
                        {
                            MyRectangle rectangle = figuresInProgram[i] as MyRectangle;
                            rectangle.Draw();
                            TreeViewItem addNode = new();
                            string[] prts = rectangle.Name.Split("_");
                            countRect = int.Parse(prts[1]);
                            countAll = rectangle.Id;
                            addNode.Header = FindResource("m_treeViewItemRect").ToString() + $" {countRect}";
                            addNode.Name = $"Rectangle_{this.countRect}";
                            Canvas.SetLeft(rectangle.Shape, rectangle.X1);
                            Canvas.SetTop(rectangle.Shape, rectangle.Y1);
                            canvasFigures.Children.Add(rectangle.Shape);
                            treeViewItemRectangle.Items.Add(addNode);
                            treeViewItemRectangle.Items.Refresh();
                            treeViewItemFigures.Items.Refresh();
                        }
                    }
                    fs.Flush();
                    fs.Close();
                    fs.Dispose();
                    MessageBox.Show("Successfully loaded!");
                }
                else
                {
                    throw new Exception();
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Error!");
            }
        }

        private void btnLoadJSON_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var fileContent = File.ReadAllText("figures.json");
                if (fileContent.Length <= 0)
                {
                    throw new Exception();
                }
                else
                {
                    countAll = countCirc = countRect = countTria = 0;
                    treeViewItemCircle.Items.Clear();
                    treeViewItemRectangle.Items.Clear();
                    treeViewItemTriangle.Items.Clear();
                    canvasFigures.Children.Clear();
                    figuresInProgram.Clear();
                    fileContent = fileContent.Trim('"');
                    string[] words = fileContent.Split(@"""");
                    for (int i = 0; i < words.Length; i += 2)
                    {
                        Console.WriteLine(words[i]);
                        string[] parts = words[i].Split(" ");
                        if (parts[1].Contains("Triangle"))
                        {
                            TreeViewItem addNode = new();
                            string[] parts_name = parts[1].Split("_");
                            countTria = int.Parse(parts_name[1]);
                            countAll = int.Parse(parts[0]);
                            addNode.Header = FindResource("m_treeViewItemTria").ToString() + $" {parts_name[1]}";
                            addNode.Name = parts[1];
                            treeViewItemTriangle.Items.Add(addNode);
                            treeViewItemTriangle.Items.Refresh();
                            treeViewItemFigures.Items.Refresh();
                            MyTriangle triangle = new(int.Parse(parts[3]), int.Parse(parts[4]), int.Parse(parts[5]), int.Parse(parts[6]), int.Parse(parts[7]), int.Parse(parts[8]));
                            triangle.Id = int.Parse(parts[0]);
                            triangle.Name = parts[1];
                            triangle.IsMoving = bool.Parse(parts[2]);
                            triangle.Draw();
                            this.figuresInProgram.Add(triangle);
                            Canvas.SetLeft(triangle.Shape, triangle.X1);
                            Canvas.SetTop(triangle.Shape, triangle.Y1);
                            canvasFigures.Children.Add(triangle.Shape);
                        }
                        else if (parts[1].Contains("Circle"))
                        {
                            TreeViewItem addNode = new();
                            string[] parts_name = parts[1].Split("_");
                            countCirc = int.Parse(parts_name[1]);
                            countAll = int.Parse(parts[0]);
                            addNode.Header = FindResource("m_treeViewItemCirc").ToString() + $" {parts_name[1]}";
                            addNode.Name = parts[1];
                            treeViewItemCircle.Items.Add(addNode);
                            treeViewItemCircle.Items.Refresh();
                            treeViewItemFigures.Items.Refresh();
                            MyCircle circle = new(int.Parse(parts[5]), int.Parse(parts[3]), int.Parse(parts[4]));
                            circle.Id = int.Parse(parts[0]);
                            circle.Name = parts[1];
                            circle.IsMoving = bool.Parse(parts[2]);
                            circle.Draw();
                            this.figuresInProgram.Add(circle);
                            Canvas.SetLeft(circle.Shape, circle.X1);
                            Canvas.SetTop(circle.Shape, circle.Y1);
                            canvasFigures.Children.Add(circle.Shape);
                        }
                        else
                        {
                            TreeViewItem addNode = new();
                            string[] parts_name = parts[1].Split("_");
                            countRect = int.Parse(parts_name[1]);
                            countAll = int.Parse(parts[0]);
                            addNode.Header = FindResource("m_treeViewItemRect").ToString() + $" {parts_name[1]}";
                            addNode.Name = parts[1];
                            treeViewItemRectangle.Items.Add(addNode);
                            treeViewItemRectangle.Items.Refresh();
                            treeViewItemFigures.Items.Refresh();
                            MyRectangle rectangle = new(int.Parse(parts[5]), int.Parse(parts[6]), int.Parse(parts[3]), int.Parse(parts[4]));
                            rectangle.Id = int.Parse(parts[0]);
                            rectangle.Name = parts[1];
                            rectangle.IsMoving = bool.Parse(parts[2]);
                            rectangle.Draw();
                            this.figuresInProgram.Add(rectangle);
                            Canvas.SetLeft(rectangle.Shape, rectangle.X1);
                            Canvas.SetTop(rectangle.Shape, rectangle.Y1);
                            canvasFigures.Children.Add(rectangle.Shape);
                        }
                    }
                    MessageBox.Show("Successfully loaded!");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error!");
            }
        }

        private void btnLoadXML_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (FileStream fs = new FileStream("figures.xml", FileMode.OpenOrCreate))
                {
                    var serializer = new XmlSerializer(typeof(List<MyFigure>));
                    List<MyFigure> figures = serializer.Deserialize(fs) as List<MyFigure>;
                    if (figures.Count > 0)
                    {
                        countAll = countCirc = countRect = countTria = 0;
                        treeViewItemCircle.Items.Clear();
                        treeViewItemRectangle.Items.Clear();
                        treeViewItemTriangle.Items.Clear();
                        canvasFigures.Children.Clear();
                        figuresInProgram.Clear();
                        figuresInProgram.AddRange(figures);
                        for (int i = 0; i < figuresInProgram.Count; i++)
                        {
                            if (figuresInProgram[i].Name.Contains("Triangle"))
                            {
                                MyTriangle triangle = figuresInProgram[i] as MyTriangle;
                                triangle.Draw();
                                TreeViewItem addNode = new();
                                string[] prts = triangle.Name.Split("_");
                                countTria = int.Parse(prts[1]);
                                countAll = triangle.Id;
                                addNode.Header = FindResource("m_treeViewItemTria").ToString() + $" {countTria}";
                                addNode.Name = triangle.Name;
                                Canvas.SetLeft(triangle.Shape, triangle.X1);
                                Canvas.SetTop(triangle.Shape, triangle.Y1);
                                canvasFigures.Children.Add(triangle.Shape);
                                treeViewItemTriangle.Items.Add(addNode);
                                treeViewItemTriangle.Items.Refresh();
                                treeViewItemFigures.Items.Refresh();
                            }
                            else if (figuresInProgram[i].Name.Contains("Circle"))
                            {
                                MyCircle circle = figuresInProgram[i] as MyCircle;
                                circle.Draw();
                                TreeViewItem addNode = new();
                                string[] prts = circle.Name.Split("_");
                                countCirc = int.Parse(prts[1]);
                                countAll = circle.Id;
                                addNode.Header = FindResource("m_treeViewItemCirc").ToString() + $" {countCirc}";
                                addNode.Name = $"Circle_{this.countCirc}";
                                Canvas.SetLeft(circle.Shape, circle.X1);
                                Canvas.SetTop(circle.Shape, circle.Y1);
                                canvasFigures.Children.Add(circle.Shape);
                                treeViewItemCircle.Items.Add(addNode);
                                treeViewItemCircle.Items.Refresh();
                                treeViewItemFigures.Items.Refresh();
                            }
                            else
                            {
                                MyRectangle rectangle = figuresInProgram[i] as MyRectangle;
                                rectangle.Draw();
                                TreeViewItem addNode = new();
                                string[] prts = rectangle.Name.Split("_");
                                countRect = int.Parse(prts[1]);
                                countAll = rectangle.Id;
                                addNode.Header = FindResource("m_treeViewItemRect").ToString() + $" {countRect}";
                                addNode.Name = $"Rectangle_{this.countRect}";
                                Canvas.SetLeft(rectangle.Shape, rectangle.X1);
                                Canvas.SetTop(rectangle.Shape, rectangle.Y1);
                                canvasFigures.Children.Add(rectangle.Shape);
                                treeViewItemRectangle.Items.Add(addNode);
                                treeViewItemRectangle.Items.Refresh();
                                treeViewItemFigures.Items.Refresh();
                            }
                        }
                        MessageBox.Show("Successfully loaded!");
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error!");
            }
        }

        private void btnSaveBin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (figuresInProgram.Count > 0)
                {
                    Stream ms = File.OpenWrite("figures.dat");
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(ms, figuresInProgram);
                    ms.Flush();
                    ms.Close();
                    ms.Dispose();
                    MessageBox.Show("Successfully saved!");
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error!");
            }
        }

        private void btnSaveJSON_Click(object sender, RoutedEventArgs e)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
            try
            {
                using (StreamWriter sw = new StreamWriter("figures.json"))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    if (figuresInProgram.Count <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        for (int i = 0; i < figuresInProgram.Count; i++)
                        {
                            figuresInProgram[i].SerializeJSON(serializer, writer);
                        }
                    }
                    MessageBox.Show("Successfully saved!");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error!");
            }
        }

        private void btnSaveXML_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(List<MyFigure>));
                using (TextWriter writer = new StreamWriter("figures.xml"))
                {
                    if (figuresInProgram.Count <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        serializer.Serialize(writer, figuresInProgram);
                    }
                }
                MessageBox.Show("Successfully saved!");
            }
            catch (Exception)
            {
                MessageBox.Show("Error!");
            }
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (figuresInProgram.Count > 0)
            {
                for (int i = 0; i < figuresInProgram.Count; i++)
                {
                    if (figuresInProgram[i].Shape != null)
                    {
                        figuresInProgram[i].Shape.Stroke = new SolidColorBrush(Colors.Black);
                        figuresInProgram[i].Shape.StrokeThickness = 1;
                    }
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