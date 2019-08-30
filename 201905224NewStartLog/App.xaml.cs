using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace _201905224NewStartLog
{
    /// <summary>
    /// 提供特定于应用程序的行为，以补充默认的应用程序类。
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// 初始化单一实例应用程序对象。这是执行的创作代码的第一行，
        /// 已执行，逻辑上等同于 main() 或 WinMain()。
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        /// <summary>
        /// 在应用程序由最终用户正常启动时进行调用。
        /// 将在启动应用程序以打开特定文件等情况下使用。
        /// </summary>
        /// <param name="e">有关启动请求和过程的详细信息。</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            // 不要在窗口已包含内容时重复应用程序初始化，
            // 只需确保窗口处于活动状态
            if (rootFrame == null)
            {
                // 创建要充当导航上下文的框架，并导航到第一页
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: 从之前挂起的应用程序加载状态
                }

                // 将框架放在当前窗口中
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // 当导航堆栈尚未还原时，导航到第一页，
                    // 并通过将所需信息作为导航参数传入来配置
                    // 参数
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }
                // 确保当前窗口处于活动状态
                Window.Current.Activate();

                //新增加的那个函数要在这里调用
                ExtendAcrylicIntoTitleBar();
            }
        }

        /// <summary>
        /// 导航到特定页失败时调用
        /// </summary>
        ///<param name="sender">导航失败的框架</param>
        ///<param name="e">有关导航失败的详细信息</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// 在将要挂起应用程序执行时调用。  在不知道应用程序
        /// 无需知道应用程序会被终止还是会恢复，
        /// 并让内存内容保持不变。
        /// </summary>
        /// <param name="sender">挂起的请求的源。</param>
        /// <param name="e">有关挂起请求的详细信息。</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: 保存应用程序状态并停止任何后台活动
            deferral.Complete();
        }


        //将顶部的那一部分隐藏，实现了一体化的效果
        private void ExtendAcrylicIntoTitleBar()
        {
            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
            ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.ButtonBackgroundColor = Colors.Transparent;
            titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
            //设置窗口的最小的尺寸，现在还有点问题，不太好用，先注释掉，万一真要用了呢
            //ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(1080 / 2,1920 / 2));
        }


        //重写的方法，用于语音启动，并搜索
        protected override void OnActivated(IActivatedEventArgs args)
        {
            //这一句是自动生成的，继承重写，嗯
            base.OnActivated(args);
            //判断是否是语音启动
            if(args.Kind == Windows.ApplicationModel.Activation.ActivationKind.VoiceCommand)
            {
                //不是语音启动就退出
                return;
            }
            //转换命令
            var command = args as Windows.ApplicationModel.Activation.VoiceCommandActivatedEventArgs;
            var commandresult = command.Result;
            //防止多输入
            string firstcommand = commandresult.RulePath[0];
            string finalcommand = commandresult.Text;
            switch(firstcommand)
            {
                case "张金龙":
                    Search();
                    break;
                case "李玉龙":
                    Search();
                    break;
            }
        }


      //这个函数中的一些内容是参考，比如在本页面操作其他页面的一些东西
      private void Search()
        {
            Frame frame = Window.Current.Content as Frame;
            MainPage mainPage = frame.Content as MainPage;
            if(mainPage == null)
            {
                return;
            }
            //这里到时候，在ItemResult里添加一个函数用于向绑定的数据模板列表内添加数据，因为是通过名筛选，所以可能会有多个，这里先做一个页面的跳转
            mainPage.VoiceTest();
        }
    }
}
