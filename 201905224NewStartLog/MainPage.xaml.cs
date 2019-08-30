using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace _201905224NewStartLog
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            //初始化时将返回按钮隐藏
            Back.Visibility = Visibility.Collapsed;
            //菜单的解释文字隐藏
            sousuo.Visibility = Visibility.Collapsed;
            xinzeng.Visibility = Visibility.Collapsed;
            shanchu.Visibility = Visibility.Collapsed;
            shouye.Visibility = Visibility.Collapsed;
            xiugai.Visibility = Visibility.Collapsed;
            //frame框架，导航菜单的基础
            MyFrame.Navigate(typeof(Welcome));
        }

        private void ZhanKai_Click(object sender, RoutedEventArgs e)
        {
            //如果为未隐藏，则隐藏
            if (sousuo.Visibility == Visibility.Visible)
            {
                sousuo.Visibility = Visibility.Collapsed;
                xinzeng.Visibility = Visibility.Collapsed;
                shanchu.Visibility = Visibility.Collapsed;
                shouye.Visibility = Visibility.Collapsed;
                xiugai.Visibility = Visibility.Collapsed;
            }
            else
            {
                sousuo.Visibility = Visibility.Visible;
                xinzeng.Visibility = Visibility.Visible;
                shanchu.Visibility = Visibility.Visible;
                shouye.Visibility = Visibility.Visible;
                xiugai.Visibility = Visibility.Visible;
            }
        }

        private void IconListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //点击菜单后必须让返回按钮可见
            if (ShouYe.IsSelected)
            {
                MyFrame.Navigate(typeof(Welcome));
                Back.Visibility = Visibility.Collapsed;
            }
            else if(SouSuo.IsSelected)
            {
                MyFrame.Navigate(typeof(Search));
                Back.Visibility = Visibility.Visible;
            }
            else if(LuRu.IsSelected)
            {
                //在相应的区域内跳转相应的页面
                MyFrame.Navigate(typeof(LuRuXinXi));
                //让返回按钮可见
                Back.Visibility = Visibility.Visible;
            }else  if(XiuGai.IsSelected)
            {
                MyFrame.Navigate(typeof(ChangeData));
                Back.Visibility = Visibility.Visible;
            }
            else if (ShanChu.IsSelected)
            {
                MyFrame.Navigate(typeof(Delete));
                Back.Visibility = Visibility.Visible;
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            //如果能返回才显示返回按钮
            if(MyFrame.CanGoBack)
            {
                MyFrame.GoBack();
                if(!MyFrame.CanGoBack)
                {
                    Back.Visibility = Visibility.Collapsed;
                }
            }
            
        }

        public void VoiceTest()
        {
            Frame frame = Window.Current.Content as Frame;
            Welcome mainPage = frame.Content as Welcome;
            if (mainPage == null)
            {
                return;
            }
            mainPage.test();
        }

        //重载方法
        private async void Page_Loaded(Object sender, RoutedEventArgs e)
        {
            var storagefile = await Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx://VoiceFile.xml"));
            await Windows.ApplicationModel.VoiceCommands.VoiceCommandDefinitionManager.InstallCommandDefinitionsFromStorageFileAsync(storagefile);
        }
    }
}
