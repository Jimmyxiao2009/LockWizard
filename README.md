# LockWizard

**Windows 锁屏详细状态文字自定义工具** — 在锁屏界面显示你想要的任何文字。

利用 Windows 磁贴通知的 `hint-lock-detailed-status` 特性，将自定义文本推送至锁屏详细状态区域。

![GitHub release)](https://img.shields.io/github/v/release/Jimmyxiao2009/LockWizard)
![GitHub GPL-3.0](https://img.shields.io/badge/license-GPL%203.0-blue)

---

## ✨ 功能

- ✏️ 输入自定义文字（最长 160 字符）
- 🔒 一键应用到 Windows 锁屏详细状态
- 💾 自动保存上次输入的文字
- 🖥️ 支持 Windows 10/11

## 📸 截图

（待补充）

## 🚀 安装

### 方法一：侧载安装

1. 前往 [Releases](https://github.com/Jimmyxiao2009/LockWizard/releases) 下载最新版本
2. 解压 `*_Test.zip`，运行 `Add-AppDevPackage.ps1`
3. 前往 **设置 → 个性化 → 锁屏界面**
4. 在「详细状态」下拉菜单选择 **LockWizard**

### 方法二：开发者运行

```powershell
# 需要 Visual Studio 2017+ 
git clone https://github.com/Jimmyxiao2009/LockWizard.git
cd LockWizard
# 用 VS 打开 LockWizard.sln，按 F5
```

## 📖 使用

1. 启动 LockWizard
2. 在文本框输入你想在锁屏上显示的文字（如日程提醒、名言、联系方式等）
3. 点击「应用到锁屏」
4. 前往 **系统设置 → 个性化 → 锁屏界面**
5. 将「详细状态」改为 **LockWizard**
6. 锁定电脑（`Win + L`）即可看到效果

## 🧠 技术原理

通过 Windows 磁贴通知 API（`TileNotification`）发送带有 `hint-lock-detailed-status="1"` 属性的磁贴文本，使该文本显示在锁屏的详细状态区域。

## 📄 许可证

GNU General Public License v3.0
