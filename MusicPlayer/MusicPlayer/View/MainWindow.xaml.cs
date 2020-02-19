using Microsoft.Win32;
using MusicPlayer.Model;
using MusicPlayer.ViewModel;
using Svg;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace MusicPlayer
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private MusicViewModel music = new MusicViewModel();
        private bool isTrackBarScroling = false;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            lv_Music.ItemsSource = music.GetPlayList();

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lv_Music.ItemsSource);
            lv_Music.Items.Filter = UserFilter;

            DispatcherTimer timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(0.01)
            };
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private bool UserFilter(object item)
        {
            if (String.IsNullOrEmpty(tb_Search.Text))
                return true;
            else
                return ((item as Music).Title.IndexOf(tb_Search.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        #region 초기 값 설정
        public ImageSource svgPlayImg
        {
            get
            {
                var svgDoc = SvgDocument.Open(Environment.CurrentDirectory + @"\Image\play.svg");

                return ImageSourceFromBitmap(svgDoc.Draw());
            }
        }
        public ImageSource svgSoundImg
        {
            get
            {
                SvgDocument svgDoc;
                svgDoc = SvgDocument.Open(Environment.CurrentDirectory + @"\Image\sound_control.svg");

                return ImageSourceFromBitmap(svgDoc.Draw());
            }
        }
        public ImageSource svgSoundMinImg
        {
            get
            {
                var svgDoc = SvgDocument.Open(Environment.CurrentDirectory + @"\Image\no_sound.svg");
                return ImageSourceFromBitmap(svgDoc.Draw());
            }

        }
        public ImageSource svgSoundMaxImg
        {
            get
            {
                var svgDoc = SvgDocument.Open(Environment.CurrentDirectory + @"\Image\volume_up.svg");
                return ImageSourceFromBitmap(svgDoc.Draw());
            }
        }
        public ImageSource svgPrevImg
        {
            get
            {
                var svgDoc = SvgDocument.Open(Environment.CurrentDirectory + @"\Image\previous.svg");
                return ImageSourceFromBitmap(svgDoc.Draw());
            }
        }
        public ImageSource svgNextImg
        {
            get
            {
                var svgDoc = SvgDocument.Open(Environment.CurrentDirectory + @"\Image\next.svg");
                return ImageSourceFromBitmap(svgDoc.Draw());
            }
        }
        public ImageSource svgAddImg
        {
            get
            {
                var svgDoc = SvgDocument.Open(Environment.CurrentDirectory + @"\Image\add.svg");
                return ImageSourceFromBitmap(svgDoc.Draw());
            }
        }
        public ImageSource svgDeleteImg
        {
            get
            {
                var svgDoc = SvgDocument.Open(Environment.CurrentDirectory + @"\Image\delete.svg");
                return ImageSourceFromBitmap(svgDoc.Draw());
            }
        }
        public ImageSource svgPauseImg
        {
            get
            {
                var svgDoc = SvgDocument.Open(Environment.CurrentDirectory + @"\Image\pause.svg");
                return ImageSourceFromBitmap(svgDoc.Draw());
            }
        }

        public ImageSource svgStopImg
        {
            get
            {
                var svgDoc = SvgDocument.Open(Environment.CurrentDirectory + @"\Image\stop.svg");
                return ImageSourceFromBitmap(svgDoc.Draw());
            }
        }

        /// <summary>
        /// Bitmap에서 ImageSource로 변경해주는 메소드 입니다.
        /// </summary>
        /// <param name="bmp">변경할 Bitmap 파일</param>
        /// <returns>ImageSource</returns>
        [DllImport("gdi32.dll")]
        public static extern bool DeleteObject([In] IntPtr hObject);
        public ImageSource ImageSourceFromBitmap(Bitmap bmp)
        {
            var handle = bmp.GetHbitmap();
            try
            {
                return Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
            finally { DeleteObject(handle); }
        }
        #endregion

        #region 이벤트 처리

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (music.Status == Status.Play || music.Status == Status.Pause)
            {
                if(isTrackBarScroling != true)
                {
                    sl_Music.Value = music.GetPosition();
                }
            }
            else if(music.Status == Status.Stop)
            {
                sl_Music.Value = 0;
            }
        }

        private void sl_volume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (sl_Volume.Value == 0)
                img_Volume.Source = svgSoundMinImg;
            else if (sl_Volume.Value <= 500)
                img_Volume.Source = svgSoundImg;
            else
                img_Volume.Source = svgSoundMaxImg;

            music.ChangeVolume((int)sl_Volume.Value);
        }

        private void btn_Next_Click(object sender, RoutedEventArgs e)
        {
            Music curMusic = music.PlayMusic(music.curMusicIndex + 1, true);
            lb_Title.Content = curMusic.Title;
        }

        private void btn_Prev_Click(object sender, RoutedEventArgs e)
        {
            Music curMusic = music.PlayMusic(music.curMusicIndex - 1, true);
            lb_Title.Content = curMusic.Title;
        }

        private void btn_Play_Click(object sender, RoutedEventArgs e)
        {
            Music curMusic = music.MusicControl(lv_Music.SelectedIndex);
            if(music.Status == Status.Play)
            {
                img_Play.Source = svgPauseImg;
                sl_Music.Maximum = curMusic.Length;
            }
            else if (music.Status == Status.Pause)
            {
                img_Play.Source = svgPlayImg;
            }
            if (curMusic != null)
                lb_Title.Content = curMusic.Title;
        }

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog
            {
                Filter = "Music Files (*.mp3)|*.mp3|All Files (*.*)|(*.*)"
            };
            if (open.ShowDialog() == true)
            {
                string filePath = open.FileName;
                string[] temp = filePath.Split('\\');
                string title = temp[temp.Length-1];

                music.AddMusic(filePath, title);
            }

            CollectionViewSource.GetDefaultView(lv_Music.ItemsSource).Refresh();
        }

        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            music.DeleteMusic(lv_Music.SelectedIndex);
            CollectionViewSource.GetDefaultView(lv_Music.ItemsSource).Refresh();
        }

        private void tb_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(lv_Music.ItemsSource).Refresh();
        }

        private void btn_Stop_Click(object sender, RoutedEventArgs e)
        {
            music.StopMusic();
            img_Play.Source = svgPlayImg;
        }

        #endregion

        private void sl_Music_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            isTrackBarScroling = true;
        }

        private void sl_Music_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            isTrackBarScroling = false;
            Music curMusic = music.PlayMusic(music.curMusicIndex, true, (int)sl_Music.Value);

            lb_Title.Content = curMusic.Title;
        }
    }
}
