using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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
using System.Windows.Threading;
using ICSharpCode.SharpZipLib.Zip;
using TagLib;
using Color = System.Windows.Media.Color;
using File = System.IO.File;
using Image = System.Windows.Controls.Image;
using Rectangle = System.Windows.Shapes.Rectangle;

namespace WinAmpClone
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {

        MediaPlayer mediaPlayer = new MediaPlayer();
        private SolidColorBrush selectedBG;
        private SolidColorBrush currentForeground;
        private SolidColorBrush fontColorX;
        String targetDir = @"c:\files\";
        private int currentplaying = 0;
        private ObservableCollection<string> listofPathsToMp3;

        //H:\Windows7\Documents\Visual Studio 2013\Projects
        public MainWindow()
        {
            InitializeComponent();
            // TestLoadFromFolder();
            listofPathsToMp3 = new ObservableCollection<string>();
            PlaylistContentListView.ItemsSource = listofPathsToMp3;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            if ((mediaPlayer.Source != null) )
            {
                if (mediaPlayer.Source != null)
                    TimerLabel.Text = String.Format("{0}", mediaPlayer.Position.ToString(@"mm\:ss"));//, mediaPlayer.NaturalDuration.TimeSpan.ToString(@"mm\:ss"));
                else
                    TimerLabel.Text = "No file selected...";
            }
        }
        
        private void TestLoadFromFolder()
        {
            LoadButtons();
            LoadMainGrid();
            LoadEqualizerGrid();
            LoadPlaylistGrid();
            LoadTxtFile();

        }
        private void LoadMainGrid()
        {
            string bitmapPath = Directory.GetFiles(targetDir, "main.bmp", SearchOption.AllDirectories).Single();
            BitmapImage src = new BitmapImage();
            src.BeginInit();
            src.CacheOption = BitmapCacheOption.OnLoad;
            src.UriSource = new Uri(bitmapPath, UriKind.Absolute);
            src.EndInit();
            CroppedBitmap cropped = new CroppedBitmap(src, new Int32Rect(0, 0, 275, 116));
            Image img = new Image();
            img.Stretch = Stretch.UniformToFill;
            img.Source = cropped;
            ImageBrush bg = new ImageBrush(cropped);
            MainGrid.Background = new SolidColorBrush(Colors.Transparent);
            MainGrid.Background = bg;
        }
        private void LoadButtons()
        {
            string bitmapPath = Directory.GetFiles(targetDir, "cButtons.bmp", SearchOption.AllDirectories).Single();
            //TO poniżej musi być takie długie żeby załadować obrazy jako cache a nie plik - inaczej nie da się usunąć z folderu tymczasowego!
            BitmapImage src = new BitmapImage();
            src.BeginInit();
            src.CacheOption = BitmapCacheOption.OnLoad;
            src.UriSource = new Uri(bitmapPath, UriKind.Absolute);
            src.EndInit();
            CroppedBitmap cropped = new CroppedBitmap(src, new Int32Rect(0, 0, 23, 18));
            Button1.Background = new ImageBrush(cropped);

            cropped = new CroppedBitmap(src, new Int32Rect(23, 0, 23, 18));
            Button2.Background = new ImageBrush(cropped);

            cropped = new CroppedBitmap(src, new Int32Rect(46, 0, 23, 18));
            Button3.Background = new ImageBrush(cropped);

            cropped = new CroppedBitmap(src, new Int32Rect(69, 0, 23, 18));
            Button4.Background = new ImageBrush(cropped);

            cropped = new CroppedBitmap(src, new Int32Rect(92, 0, 23, 18));
            Button5.Background = new ImageBrush(cropped);

            cropped = new CroppedBitmap(src, new Int32Rect(114, 0, 22, 16));
            Button6.Background = new ImageBrush(cropped);

        }
        private void LoadEqualizerGrid()
        {
            string bitmapPath = Directory.GetFiles(targetDir, "eqMain.bmp", SearchOption.AllDirectories).Single();

            BitmapImage src = new BitmapImage();
            src.BeginInit();
            src.CacheOption = BitmapCacheOption.OnLoad;
            src.UriSource = new Uri(bitmapPath, UriKind.Absolute);
            src.EndInit();

            CroppedBitmap cropped = new CroppedBitmap(src, new Int32Rect(0, 0, 275, 115));
            Image img = new Image();
            img.Source = cropped;
            // EqualizerGrid.Children.Clear();
            EqualizerGrid.Children.Add(img);
        }
        private void LoadPlaylistGrid()
        {
            string bitmapPath = Directory.GetFiles(targetDir, "plEdit.bmp", SearchOption.AllDirectories).Single();
            BitmapImage src = new BitmapImage();
            src.BeginInit();
            src.CacheOption = BitmapCacheOption.OnLoad;
            src.UriSource = new Uri(bitmapPath, UriKind.Absolute);
            src.EndInit();
            LoadFirstPlaylistRow(src);
            LoadSecondPlaylistRow(src);
            LoadThirdPlaylistRow(src);
        }
        private void LoadFirstPlaylistRow(BitmapImage src)
        {
            CroppedBitmap cropped = new CroppedBitmap(src, new Int32Rect(0, 0, 25, 20));
            Image img = new Image();
            img.Source = cropped;
            Row1Col1.Children.Add(img);

            cropped = new CroppedBitmap(src, new Int32Rect(125, 0, 25, 20));
            Image img2 = new Image();
            img2.Source = cropped;
            img2.Stretch = Stretch.Fill;
            Row1Col2.Children.Add(img2);

            cropped = new CroppedBitmap(src, new Int32Rect(25, 0, 100, 20));
            Image img3 = new Image();
            img3.Source = cropped;
            Row1Col3.Children.Add(img3);

            cropped = new CroppedBitmap(src, new Int32Rect(125, 0, 25, 20));
            Image img4 = new Image();
            img4.Source = cropped;
            img4.Stretch = Stretch.Fill;
            Row1Col4.Children.Add(img4);

            cropped = new CroppedBitmap(src, new Int32Rect(155, 0, 25, 20));
            Image img5 = new Image();
            img5.Source = cropped;
            img5.Stretch = Stretch.Fill;
            Row1Col5.Children.Add(img5);

        }
        private void LoadSecondPlaylistRow(BitmapImage src)
        {
            CroppedBitmap cropped = new CroppedBitmap(src, new Int32Rect(0, 42, 25, 29));

            Image img = new Image();
            img.Source = cropped;
            img.Stretch = Stretch.Fill;
            Row2Col1.Children.Clear();
            Row2Col1.Children.Add(img);

            //PlaylistContentGrid.Background = new ImageBrush(cropped);
            //cropped = new CroppedBitmap(src, new Int32Rect(125, 0, 25, 20));
            //Image img2 = new Image();
            //img2.Source = cropped;
            //img2.Stretch = Stretch.Fill;
            //Row2Col2.Children.Add(img2);

            cropped = new CroppedBitmap(src, new Int32Rect(25, 42, 25, 29));
            Image img3 = new Image();
            img3.Source = cropped;
            img3.Stretch = Stretch.Fill;
            Row2Col3.Children.Clear();
            Row2Col3.Children.Add(img3);
        }
        private void LoadThirdPlaylistRow(BitmapImage src)
        {
            //todo rotating images
            BitmapSource cropped = new CroppedBitmap(src, new Int32Rect(0, 0, 25, 20));

            Image img = new Image();
            img.Source = cropped;
            //img.Margin = new Thickness(15, 10, 0, 0);
            //img.RenderTransform = new RotateTransform()
            //{
            //    Angle = 180
            //}; 
            //img.Stretch = Stretch.UniformToFill;
            Row3Col1.Children.Add(img);


            cropped = new CroppedBitmap(src, new Int32Rect(25, 0, 100, 20));
            Image img2 = new Image();
            img2.Source = cropped;
            Row3Col2.Children.Add(img2);

            cropped = new CroppedBitmap(src, new Int32Rect(125, 0, 25, 20));
            Image img3 = new Image();
            //img3.RenderTransform = new RotateTransform()
            //{
            //    Angle = 180
            //};
            img3.Source = cropped;
            img3.Stretch = Stretch.Fill;
            Row3Col3.Children.Add(img3);

            cropped = new CroppedBitmap(src, new Int32Rect(155, 0, 25, 20));
            Image img4 = new Image();
            img4.Source = cropped;
            img4.Stretch = Stretch.Fill;

            Row3Col4.Children.Add(img4);
        }
        private void LoadTxtFile()
        {
            string bitmapPath = Directory.GetFiles(targetDir, "plEdit.txt", SearchOption.AllDirectories).Single();
            string text = File.ReadAllText(bitmapPath);
            string[] parameters = text.Split('\n');
            var normalBG = parameters.First(x => x.Contains("NormalBG")).Split('=')[1];
            var selectedBGName = parameters.First(x => x.Contains("SelectedBG")).Split('=')[1];
            var fontName = parameters.First(x => x.Contains("Font")).Split('=')[1];
            var fontColor = parameters.First(x => x.Contains("Normal")).Split('=')[1];
            var currentname = parameters.First(x => x.Contains("Current")).Split('=')[1];
            currentForeground = new SolidColorBrush(Color.FromRgb(ColorTranslator.FromHtml(currentname).R, ColorTranslator.FromHtml(currentname).G, ColorTranslator.FromHtml(currentname).B));
            selectedBG = new SolidColorBrush(Color.FromRgb(ColorTranslator.FromHtml(selectedBGName).R, ColorTranslator.FromHtml(selectedBGName).G, ColorTranslator.FromHtml(selectedBGName).B));
            fontColorX = new SolidColorBrush(Color.FromRgb(ColorTranslator.FromHtml(fontColor).R, ColorTranslator.FromHtml(fontColor).G, ColorTranslator.FromHtml(fontColor).B));
            PlaylistContentListView.Background = new SolidColorBrush(Color.FromRgb(ColorTranslator.FromHtml(normalBG).R, ColorTranslator.FromHtml(normalBG).G, ColorTranslator.FromHtml(normalBG).B));
            PlaylistContentListView.Foreground = fontColorX;
            
            PlaylistContentListView.FontFamily = new System.Windows.Media.FontFamily(fontName);
            TagLabel.FontFamily = new System.Windows.Media.FontFamily(fontName);
            //Overall.Background = new SolidColorBrush(Color.FromRgb(ColorTranslator.FromHtml(bgColor).R, ColorTranslator.FromHtml(bgColor).G, ColorTranslator.FromHtml(bgColor).B));
            TimerLabel.Foreground = fontColorX;
            TagLabel.Foreground = fontColorX;
        }
        private void ImagePanel_OnDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                //Note that you can have more than one file.
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                OpenSkin(files[0]);
            }
        }
        private void OpenSkin(string p)
        {
            FastZip fastZip = new FastZip();
            string fileFilter = null;

            try
            {

                //Clear everything what was before
                Array.ForEach(Directory.GetFiles(targetDir, "*", SearchOption.AllDirectories), File.Delete);
                // Will always overwrite if target filenames already exist
                fastZip.ExtractZip(p, targetDir, fileFilter);
                TestLoadFromFolder();

                ImagePanel.Visibility = Visibility.Collapsed;
                Overall.Visibility = Visibility.Visible;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error with acces");
            }
        }
        private void LoadMp3File(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                //Note that you can have more than one file.
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                //TagLib.File file = TagLib.File.Create(File.Open(files[0],FileMode.Open));
                listofPathsToMp3.Add(files[0]);
            }
        }
        private void Previous_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var songindex = currentplaying - 1;
                var file = listofPathsToMp3[songindex];
                mediaPlayer.Open(new Uri(file));
                mediaPlayer.Play();
                currentplaying = songindex;
            }
            catch (Exception)
            {
                Console.WriteLine("End of list");
            }
        }
        private void Play_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var songindex = PlaylistContentListView.SelectedIndex;
                var file = listofPathsToMp3[songindex];
                if (mediaPlayer.CanPause)
                {
                    mediaPlayer.Play();
                }
                else
                {
                    mediaPlayer.Open(new Uri(file));
                    mediaPlayer.Play();
                }
                LoadTags(file);
                //string command = "open \"" + file + "\" type MPEGVideo alias MyMp3";
                //mciSendString(command, null, 0, 0);
                //string command2 = "play MyMp3";
                //mciSendString(command2, null, 0, 0);
                currentplaying = songindex;
            }
            catch (Exception exception)
            {
                Console.WriteLine("Tried to run player without file, no action needed");
            }

        }
        private void LoadTags(string filePath)
        {
            var tags = TagLib.File.Create(filePath);
            TagLabel.Text = tags.Tag.Title + "-" + tags.Tag.Album;
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Pause();
        }

        private void Stop_Clicl(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Stop();
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var songindex = currentplaying + 1;
                var file = listofPathsToMp3[songindex];

                mediaPlayer.Open(new Uri(file));
                mediaPlayer.Play();
                currentplaying = songindex;
            }
            catch (Exception exception)
            {
                Console.WriteLine("End of list");
            }
        }

        private void Eject_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void MainGrid_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void PlaylistContentListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PlaylistContentListView.SelectedIndex >0)
            {
                foreach (ListViewItem item in PlaylistContentListView.Items)
                {
                    if (item.IsSelected)
                    {
                        item.Background = selectedBG;
                        item.Foreground = currentForeground;
                    }
                    else
                    {
                        
                    }
                }
            }
        }
    }
}
