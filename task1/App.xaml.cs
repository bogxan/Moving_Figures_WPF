namespace Task1
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    
    public partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();
            App.LanguageChanged += this.App_LanguageChanged;
            languages.Clear();
            languages.Add(new CultureInfo("en-US"));
            languages.Add(new CultureInfo("ru-RU"));
            languages.Add(new CultureInfo("uk-UA"));
            Language = Task1.Properties.Settings1.Default.DefaultLanguage;
        }

        public static event EventHandler LanguageChanged;

        private static readonly List<CultureInfo> languages = new();

        public static List<CultureInfo> Languages
        {
            get
            {
                return languages;
            }
        }

        public static CultureInfo Language
        {
            get
            {
                return System.Threading.Thread.CurrentThread.CurrentUICulture;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                if (value == System.Threading.Thread.CurrentThread.CurrentUICulture)
                {
                    return;
                }

                System.Threading.Thread.CurrentThread.CurrentUICulture = value;
                ResourceDictionary dict = new();
                try
                {
                    if (value.Name == "en-US")
                    {
                        dict.Source = new Uri("Resources/lang.xaml", UriKind.Relative);
                    }
                    else
                    {
                        dict.Source = new Uri(string.Format("Resources/lang.{0}.xaml", value.Name), UriKind.Relative);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Unsupported language!");
                    return;
                }

                ResourceDictionary oldDict = (from d in Application.Current.Resources.MergedDictionaries
                                              where d.Source != null && d.Source.OriginalString.StartsWith("Resources/lang.")
                                              select d).First();
                if (oldDict != null)
                {
                    int ind = Application.Current.Resources.MergedDictionaries.IndexOf(oldDict);
                    Application.Current.Resources.MergedDictionaries.Remove(oldDict);
                    Application.Current.Resources.MergedDictionaries.Insert(ind, dict);
                }
                else
                {
                    Application.Current.Resources.MergedDictionaries.Add(dict);
                }

                LanguageChanged(Application.Current, new EventArgs());
            }
        }

        private void App_LanguageChanged(object sender, EventArgs e)
        {
            Task1.Properties.Settings1.Default.DefaultLanguage = Language;
            Task1.Properties.Settings1.Default.Save();
        }
    }
}
