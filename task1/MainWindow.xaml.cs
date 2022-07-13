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
                MenuItem menuLang = new();
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
                        item.Move(this.sizeOfCanvas);
                    }
                }

                await Task.Delay(500);
            }
        }

        private void AddLoadedFiguresToProgram(List<MyFigure> figures)
        {
            if (figures.Count > 0)
            {
                countAll = countCirc = countRect = countTria = 0;
                treeViewItemCircle.Items.Clear();
                treeViewItemRectangle.Items.Clear();
                treeViewItemTriangle.Items.Clear();
                canvasFigures.Children.Clear();
                figuresInProgram.Clear();
                for (int i = 0; i < figures.Count; i++)
                {
                    if (figures[i].Name.Contains("Triangle"))
                    {
                        MyTriangle triangle = figures[i] as MyTriangle;
                        triangle.Draw();
                        if (triangle.X + triangle.BaseTriangle.ActualWidth >= sizeOfCanvas.X || triangle.Y + triangle.BaseTriangle.ActualHeight >= sizeOfCanvas.Y)
                        {
                            triangle.Move(sizeOfCanvas);
                        }
                        else
                        {
                            Canvas.SetLeft(triangle.BaseTriangle, triangle.X);
                            Canvas.SetTop(triangle.BaseTriangle, triangle.Y);
                        }
                        TreeViewItem addNode = new();
                        string[] prts = triangle.Name.Split("_");
                        countTria = int.Parse(prts[1]);
                        countAll = triangle.Id;
                        addNode.Header = FindResource("m_treeViewItemTria").ToString() + $" {countTria}";
                        addNode.Name = triangle.Name;
                        figuresInProgram.Add(triangle);
                        canvasFigures.Children.Add(triangle.BaseTriangle);
                        treeViewItemTriangle.Items.Add(addNode);
                        treeViewItemTriangle.Items.Refresh();
                        treeViewItemFigures.Items.Refresh();
                    }
                    else if (figures[i].Name.Contains("Circle"))
                    {
                        MyCircle circle = figures[i] as MyCircle;
                        circle.Draw();
                        if (circle.X + circle.BaseCircle.ActualWidth >= sizeOfCanvas.X || circle.Y + circle.BaseCircle.ActualHeight >= sizeOfCanvas.Y)
                        {
                            circle.Move(sizeOfCanvas);
                        }
                        else
                        {
                            Canvas.SetLeft(circle.BaseCircle, circle.X);
                            Canvas.SetTop(circle.BaseCircle, circle.Y);
                        }
                        TreeViewItem addNode = new();
                        string[] prts = circle.Name.Split("_");
                        countCirc = int.Parse(prts[1]);
                        countAll = circle.Id;
                        addNode.Header = FindResource("m_treeViewItemCirc").ToString() + $" {countCirc}";
                        addNode.Name = $"Circle_{this.countCirc}";
                        figuresInProgram.Add(circle);
                        canvasFigures.Children.Add(circle.BaseCircle);
                        treeViewItemCircle.Items.Add(addNode);
                        treeViewItemCircle.Items.Refresh();
                        treeViewItemFigures.Items.Refresh();
                    }
                    else
                    {
                        MyRectangle rectangle = figures[i] as MyRectangle;
                        rectangle.Draw();
                        if (rectangle.X + rectangle.BaseRectangle.ActualWidth >= sizeOfCanvas.X || rectangle.Y + rectangle.BaseRectangle.ActualHeight >= sizeOfCanvas.Y)
                        {
                            rectangle.Move(sizeOfCanvas);
                        }
                        else
                        {
                            Canvas.SetLeft(rectangle.BaseRectangle, rectangle.X);
                            Canvas.SetTop(rectangle.BaseRectangle, rectangle.Y);
                        }
                        Canvas.SetLeft(rectangle.BaseRectangle, rectangle.X);
                        Canvas.SetTop(rectangle.BaseRectangle, rectangle.Y);
                        TreeViewItem addNode = new();
                        string[] prts = rectangle.Name.Split("_");
                        countRect = int.Parse(prts[1]);
                        countAll = rectangle.Id;
                        addNode.Header = FindResource("m_treeViewItemRect").ToString() + $" {countRect}";
                        addNode.Name = $"Rectangle_{this.countRect}";
                        figuresInProgram.Add(rectangle);
                        canvasFigures.Children.Add(rectangle.BaseRectangle);
                        treeViewItemRectangle.Items.Add(addNode);
                        treeViewItemRectangle.Items.Refresh();
                        treeViewItemFigures.Items.Refresh();
                    }
                }
                MessageBox.Show(FindResource("m_msgBoxLoad").ToString());
            }
            else
            {
                throw new Exception();
            }
        }

        private void AddTriangleBtn_Click(object sender, RoutedEventArgs e)
        {
            this.countTria++;
            this.countAll++;
            TreeViewItem addNode = new();
            addNode.Header = FindResource("m_treeViewItemTria").ToString() + $" {this.countTria}";
            addNode.Name = $"Triangle_{this.countTria}";
            treeViewItemTriangle.Items.Add(addNode);
            MyTriangle triangle = new(150, 150, 200, 200);
            triangle.Id = countAll;
            triangle.Name = $"Triangle_{this.countTria}";
            triangle.IsMoving = true;
            triangle.Draw();
            this.figuresInProgram.Add(triangle);
            canvasFigures.Children.Add(triangle.BaseTriangle);
            Canvas.SetLeft(triangle.BaseTriangle, triangle.X);
            Canvas.SetTop(triangle.BaseTriangle, triangle.Y);
            treeViewItemFigures.Items.Refresh();
        }

        private void AddCircleBtn_Click(object sender, RoutedEventArgs e)
        {
            this.countCirc++;
            this.countAll++;
            TreeViewItem addNode = new();
            addNode.Header = FindResource("m_treeViewItemCirc").ToString() + $" {this.countCirc}";
            addNode.Name = $"Circle_{this.countCirc}";
            treeViewItemCircle.Items.Add(addNode);
            MyCircle circle = new(100, 100, 300, 300);
            circle.Id = countAll;
            circle.Name = $"Circle_{this.countCirc}";
            circle.IsMoving = true;
            circle.Draw();
            this.figuresInProgram.Add(circle);
            canvasFigures.Children.Add(circle.BaseCircle);
            Canvas.SetLeft(circle.BaseCircle, circle.X);
            Canvas.SetTop(circle.BaseCircle, circle.Y);
            treeViewItemFigures.Items.Refresh();
        }

        private void AddRectangleBtn_Click(object sender, RoutedEventArgs e)
        {
            this.countRect++;
            this.countAll++;
            TreeViewItem addNode = new();
            addNode.Header = FindResource("m_treeViewItemRect").ToString() + $" {this.countRect}";
            addNode.Name = $"Rectangle_{this.countRect}";
            treeViewItemRectangle.Items.Add(addNode);
            MyRectangle rectangle = new(300, 200, 400, 400);
            rectangle.Id = countAll;
            rectangle.Name = $"Rectangle_{this.countRect}";
            rectangle.IsMoving = true;
            rectangle.Draw();
            this.figuresInProgram.Add(rectangle);
            canvasFigures.Children.Add(rectangle.BaseRectangle);
            Canvas.SetLeft(rectangle.BaseRectangle, rectangle.X);
            Canvas.SetTop(rectangle.BaseRectangle, rectangle.Y);
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

        private void BtnLoadBin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BinaryFormatter formatter = new();
                FileStream fs = File.Open("figures.dat", FileMode.Open);
                object obj = formatter.Deserialize(fs);
                List<MyFigure> figures = (List<MyFigure>)obj;
                fs.Flush();
                fs.Close();
                fs.Dispose();
                AddLoadedFiguresToProgram(figures);
            }
            catch (Exception)
            {
                MessageBox.Show(FindResource("m_msgBoxError").ToString());
            }
        }

        private void BtnLoadJSON_Click(object sender, RoutedEventArgs e)
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
                    var settings = new JsonSerializerSettings()
                    {
                        TypeNameHandling = TypeNameHandling.Objects
                    };
                    List<MyFigure> figures = JsonConvert.DeserializeObject<List<MyFigure>>(fileContent, settings);
                    AddLoadedFiguresToProgram(figures);
                }
            }
            catch (Exception)
            {
                MessageBox.Show(FindResource("m_msgBoxError").ToString());
            }
        }

        private void BtnLoadXML_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using FileStream fs = new("figures.xml", FileMode.OpenOrCreate);
                var serializer = new XmlSerializer(typeof(List<MyFigure>));
                List<MyFigure> figures = serializer.Deserialize(fs) as List<MyFigure>;
                AddLoadedFiguresToProgram(figures);
            }
            catch (Exception)
            {
                MessageBox.Show(FindResource("m_msgBoxError").ToString());
            }
        }

        private void BtnSaveBin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (figuresInProgram.Count > 0)
                {
                    Stream ms = File.OpenWrite("figures.dat");
                    BinaryFormatter formatter = new();
                    formatter.Serialize(ms, figuresInProgram);
                    ms.Flush();
                    ms.Close();
                    ms.Dispose();
                    MessageBox.Show(FindResource("m_msgBoxSave").ToString());
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                MessageBox.Show(FindResource("m_msgBoxError").ToString());
            }
        }

        private void BtnSaveJSON_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (figuresInProgram.Count > 0)
                {
                    using (StreamWriter sw = new("figures.json"))
                    {
                        var settings = new JsonSerializerSettings()
                        {
                            TypeNameHandling = TypeNameHandling.Objects
                        };
                        string str = "[";
                        for (int i = 0; i < figuresInProgram.Count; i++)
                        {
                            str += JsonConvert.SerializeObject(figuresInProgram[i], settings);
                            if (i < figuresInProgram.Count - 1)
                            {
                                str += ",";
                            }
                        }
                        str += "]";
                        sw.Write(str);
                    }
                    MessageBox.Show(FindResource("m_msgBoxSave").ToString());
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                MessageBox.Show(FindResource("m_msgBoxError").ToString());
            }
        }

        private void BtnSaveXML_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (figuresInProgram.Count > 0)
                {
                    var serializer = new XmlSerializer(typeof(List<MyFigure>));
                    using TextWriter writer = new StreamWriter("figures.xml");
                    serializer.Serialize(writer, figuresInProgram);
                    MessageBox.Show(FindResource("m_msgBoxSave").ToString());
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                MessageBox.Show(FindResource("m_msgBoxError").ToString());
            }
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (figuresInProgram.Count > 0)
            {
                for (int i = 0; i < figuresInProgram.Count; i++)
                {
                    if (figuresInProgram[i].Name.Contains("Triangle"))
                    {
                        MyTriangle triangle = figuresInProgram[i] as MyTriangle;
                        triangle.BaseTriangle.Stroke = new SolidColorBrush(Colors.Black);
                        triangle.BaseTriangle.StrokeThickness = 1;
                    }
                    else if (figuresInProgram[i].Name.Contains("Circle"))
                    {
                        MyCircle circle = figuresInProgram[i] as MyCircle;
                        circle.BaseCircle.Stroke = new SolidColorBrush(Colors.Black);
                        circle.BaseCircle.StrokeThickness = 1;
                    }
                    else
                    {
                        MyRectangle rectangle = figuresInProgram[i] as MyRectangle;
                        rectangle.BaseRectangle.Stroke = new SolidColorBrush(Colors.Black);
                        rectangle.BaseRectangle.StrokeThickness = 1;
                    }
                }
                selectedNode = e.NewValue as TreeViewItem;
                if (selectedNode != null)
                {
                    for (int i = 0; i < figuresInProgram.Count; i++)
                    {
                        if (figuresInProgram[i].Name == selectedNode.Name)
                        {
                            if (figuresInProgram[i].Name.Contains("Triangle"))
                            {
                                MyTriangle triangle = figuresInProgram[i] as MyTriangle;
                                triangle.BaseTriangle.Stroke = new SolidColorBrush(Colors.Yellow);
                                triangle.BaseTriangle.StrokeThickness = 5;
                            }
                            else if (figuresInProgram[i].Name.Contains("Circle"))
                            {
                                MyCircle circle = figuresInProgram[i] as MyCircle;
                                circle.BaseCircle.Stroke = new SolidColorBrush(Colors.Yellow);
                                circle.BaseCircle.StrokeThickness = 5;
                            }
                            else
                            {
                                MyRectangle rectangle = figuresInProgram[i] as MyRectangle;
                                rectangle.BaseRectangle.Stroke = new SolidColorBrush(Colors.Yellow);
                                rectangle.BaseRectangle.StrokeThickness = 5;
                            }
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