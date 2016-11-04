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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace WpfAppCleanerCheckBox
{
    /// <summary>
    /// 为了binding
    /// </summary>
    public class BindingTest { public string Image { set; get; } }

    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        static string PicPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Image");

        /// <summary>
        /// 性别
        /// </summary>
        string gender { set; get; }

        /// <summary>
        /// 衣服类型
        /// </summary>
        string type { set; get; }

        /// <summary>
        /// 图片 对应 图片路径
        /// </summary>
        private Dictionary<string, string> picDictionary = new Dictionary<string, string>();

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 处理性别点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GenderRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            gender = (sender as RadioButton).Tag.ToString();
            Update();
        }

        /// <summary>
        /// 处理衣服类型点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TypeRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            type = (sender as RadioButton).Tag.ToString();
            Update();
        }
        /// <summary>
        /// 加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //初始化
            gender = "0";
            type = "0";

            LoadPicture();
            Update();
        }
        /// <summary>
        /// 更新现在点击的按钮 
        /// </summary>
        void Update()
        {
            if (gender != null && type!= null)
            {
                string temp = gender + type + ".jpg";
                //知道是哪个图片名称 picDictionary[temp];
                //pic = "image/00.jpg";

                //datacontext 是那个类 path是那个属性
                
                 
                //初级的方式
                BindingTest tBinding = null;
                try
                {
                   tBinding  = new BindingTest() { Image = picDictionary[temp] };
                }
                catch (Exception e)
                { }
                //简化的方式
                try
                {
                    img.DataContext = picDictionary[temp];
                }
                catch (Exception e)
                { }
                //this.DataContext = this;
                //img.DataContext = tBinding;
            }
        }
        /// <summary>
        /// 将本地图图片读入程序
        /// </summary>
        public void LoadPicture()
        {
            List<string> tempList = null;
            try
            {
                //如果本地没有图片会抛异常的
                tempList = Directory.GetFiles(PicPath).ToList();
            }
            catch (Exception e)
            { }

            //对得到的路径判断
            foreach (var item in tempList)
            {
                FileInfo a = new FileInfo(item);
                if (a.Extension != ".jpg")
                {
                    tempList.Remove(item);
                }
            }
            //得到了图片路径了 
            //读入吧

            foreach (var item in tempList)
            {
                string fileName = new FileInfo(item).Name;
                string filePath = item;
                picDictionary.Add(fileName, filePath);
            }
        }
    }
}
