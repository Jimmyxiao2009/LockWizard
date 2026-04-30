using System;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace LockWizard
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            StatusTextBox.TextChanged += StatusTextBox_TextChanged;

            // 读取上次保存的文字
            var saved = Windows.Storage.ApplicationData.Current.LocalSettings.Values["lockText"] as string;
            if (!string.IsNullOrEmpty(saved))
                StatusTextBox.Text = saved;
        }

        private void StatusTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CharCountText.Text = $"{StatusTextBox.Text.Length} / 160";
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            var text = StatusTextBox.Text.Trim();

            if (string.IsNullOrEmpty(text))
            {
                ShowStatus("请输入文字后再应用", isError: true);
                return;
            }

            try
            {
                UpdateLockScreenText(text);

                // 保存到本地设置
                Windows.Storage.ApplicationData.Current.LocalSettings.Values["lockText"] = text;

                ShowStatus("✓ 已更新锁屏详细状态文字", isError: false);
            }
            catch (Exception ex)
            {
                ShowStatus($"更新失败：{ex.Message}", isError: true);
            }
        }

        private void UpdateLockScreenText(string text)
        {
            // 使用 TileWide310x150SmallImageAndText04 模板
            // hint-lock-detailed-status 让系统把第一条 text 显示在锁屏详细状态
            string tileXml =
                $@"<tile>
                    <visual version=""2"">
                        <binding template=""TileWide"" branding=""none"">
                            <text hint-style=""body"" hint-lock-detailed-status=""1"">{EscapeXml(text)}</text>
                        </binding>
                        <binding template=""TileMedium"" branding=""none"">
                            <text hint-style=""body"">{EscapeXml(text)}</text>
                        </binding>
                    </visual>
                </tile>";

            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(tileXml);

            var notification = new TileNotification(xmlDoc);
            TileUpdateManager.CreateTileUpdaterForApplication().Update(notification);
        }

        private static string EscapeXml(string text)
        {
            return text
                .Replace("&", "&amp;")
                .Replace("<", "&lt;")
                .Replace(">", "&gt;")
                .Replace("\"", "&quot;")
                .Replace("'", "&apos;");
        }

        private void ShowStatus(string message, bool isError)
        {
            StatusLabel.Text = message;
            StatusLabel.Foreground = isError
                ? new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.OrangeRed)
                : new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.MediumSeaGreen);
        }
    }
}
