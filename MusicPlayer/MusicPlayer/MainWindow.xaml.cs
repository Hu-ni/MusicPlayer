using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MusicPlayer
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            listView1.Items.Add(new CustomerInfo() 
            { Memo = "1", Subject = "2", Class = "3", Professor = "4", ScheDule = "5" });
            listView1.Items.Add(new CustomerInfo()
            { Memo = "1", Subject = "2", Class = "3", Professor = "4", ScheDule = "5" });
            listView1.Items.Add(new CustomerInfo()
            { Memo = "1", Subject = "2", Class = "3", Professor = "4", ScheDule = "5" });
            listView1.Items.Add(new CustomerInfo()
            { Memo = "1", Subject = "2", Class = "3", Professor = "4", ScheDule = "5" }); listView1.Items.Add(new CustomerInfo()
            { Memo = "1", Subject = "2", Class = "3", Professor = "4", ScheDule = "5" }); listView1.Items.Add(new CustomerInfo()
            { Memo = "1", Subject = "2", Class = "3", Professor = "4", ScheDule = "5" }); listView1.Items.Add(new CustomerInfo()
            { Memo = "1", Subject = "2", Class = "3", Professor = "4", ScheDule = "5" }); listView1.Items.Add(new CustomerInfo()
            { Memo = "1", Subject = "2", Class = "3", Professor = "4", ScheDule = "5" }); listView1.Items.Add(new CustomerInfo()
            { Memo = "1", Subject = "2", Class = "3", Professor = "4", ScheDule = "5" }); listView1.Items.Add(new CustomerInfo()
            { Memo = "1", Subject = "2", Class = "3", Professor = "4", ScheDule = "5" }); listView1.Items.Add(new CustomerInfo()
            { Memo = "1", Subject = "2", Class = "3", Professor = "4", ScheDule = "5" });
            listView1.SelectedIndex = 1;
        }


        //리스트뷰에서 선택한 항목의 정보를 얻어온다.
        private static CustomerInfo ListView_GetItem(RoutedEventArgs e)
        {
            DependencyObject dep = (DependencyObject)e.OriginalSource;

            while (!(dep is System.Windows.Controls.ListViewItem))
            {
                try
                {
                    dep = VisualTreeHelper.GetParent(dep);
                }
                catch
                {
                    return null;
                }
            }
            ListViewItem item = (ListViewItem)dep;
            CustomerInfo content = (CustomerInfo)item.Content;

            return content;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CustomerInfo info = ListView_GetItem(e);
        }

        private void listView1_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void listView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }

    public class CustomerInfo
    {
        private string _Memo;
        private string _Subject;
        private string _Class;
        private string _Professor;
        private string _ScheDule;

        public string Memo { get { return _Memo; } set { _Memo = value; } }
        public string Subject { get { return _Subject; } set { _Subject = value; } }
        public string Class { get { return _Class; } set { _Class = value; } }
        public string Professor { get { return _Professor; } set { _Professor = value; } }
        public string ScheDule { get { return _ScheDule; } set { _ScheDule = value; } }
    }
}
