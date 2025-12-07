# 🌐 Complete Website Downloader

[![CodeFactor](https://www.codefactor.io/repository/github/badrshs/complete-website-downloader/badge)](https://www.codefactor.io/repository/github/badrshs/complete-website-downloader)
[![GitHub release](https://img.shields.io/github/release/badrshs/Complete-Website-Downloader.svg?logo=github)](https://github.com/badrshs/Complete-Website-Downloader/releases/latest)
[![Downloads](https://img.shields.io/github/downloads/badrshs/Complete-Website-Downloader/total.svg)](https://github.com/badrshs/Complete-Website-Downloader/releases)
[![License](https://img.shields.io/github/license/badrshs/Complete-Website-Downloader.svg)](LICENSE)
[![Windows](https://img.shields.io/badge/platform-Windows-blue.svg)](https://github.com/badrshs/Complete-Website-Downloader/releases)

> **Download any website for offline viewing with a single click.** A powerful, user-friendly Windows desktop application that saves complete websites including HTML, CSS, JavaScript, images, and all assets.

<!-- MAIN SCREENSHOT - Replace with your screenshot -->
![Website Downloader Screenshot](https://github.com/user-attachments/assets/35d9638e-6fa7-4e47-bb56-0d63383c162a)

<!-- /MAIN SCREENSHOT -->

---

## ✨ Features

### 🚀 Core Features
- **One-Click Website Download** - Enter URL, select folder, download
- **Complete Offline Copy** - Downloads HTML, CSS, JS, images, fonts, and all assets
- **Link Conversion** - Automatically converts links for offline browsing
- **Recursive Download** - Follows links to download entire site structure

### 🎛️ Advanced Options
- **Customizable Depth** - Control how deep the crawler goes
- **Rate Limiting** - Be polite to servers with bandwidth throttling
- **User Agent Spoofing** - Bypass basic bot detection
- **Resume Support** - Don't re-download existing files

### 📊 User Experience
- **Real-time Progress Log** - See exactly what's being downloaded
- **Error Tracking** - Dedicated errors tab for troubleshooting
- **Download History** - Quick access to previously downloaded sites
- **Persistent Settings** - Your preferences are saved between sessions

---

## 📥 Download & Installation

### Direct Download
👉 **[Download Latest Release (WebsiteDownloader.exe)](https://github.com/badrshs/Complete-Website-Downloader/releases/latest)**

### Requirements
- Windows 7 / 8 / 10 / 11
- .NET Framework 4.7.2 or higher (pre-installed on Windows 10+)

### Installation
1. Download `WebsiteDownloader.exe` from the releases page
2. Run the executable - **no installation required!**
3. Windows Defender may show a warning (click "More info" → "Run anyway")

---

## 🖥️ Screenshots

<!-- SCREENSHOT: Main Interface -->
![Main Interface](https://github.com/user-attachments/assets/fcf8038f-90c9-4ff7-acf4-4eb218056a11)
 
<!-- SCREENSHOT: Settings Panel -->
![Settings](https://github.com/user-attachments/assets/6f24ea78-7b6a-40cb-be9d-7547c72fe48a)

<!-- SCREENSHOT: Download Progress with Errors Tab -->
![Download Progress](https://github.com/user-attachments/assets/d6b9f7c0-e432-4edc-81b0-19322fdb4390)

---

## 🎯 Use Cases

| Use Case | Description |
|----------|-------------|
| 📚 **Archive Websites** | Save sites before they go offline forever |
| 🎨 **Study Web Design** | Learn from beautifully designed websites |
| 📖 **Offline Documentation** | Read docs without internet access |
| ✈️ **Travel Preparation** | Download guides and references for offline use |
| 🔬 **Web Development** | Analyze site structures and assets |
| 📰 **Backup Blogs** | Preserve your content or favorite blogs |

---

## ⚙️ Settings Explained

| Setting | wget Flag | Description |
|---------|-----------|-------------|
| **Convert Links** | `-k` | Makes downloaded links work offline |
| **Adjust Extensions** | `-E` | Adds .html to pages without extensions |
| **Max Depth** | `-l` | How many link levels deep to follow (0 = unlimited) |
| **Wait Between Requests** | `-w` | Seconds to wait between downloads (be polite!) |
| **Rate Limit** | `--limit-rate` | Maximum download speed (e.g., `500k`, `2m`) |
| **No Clobber** | `-nc` | Skip files that already exist |

---

## 🛠️ Building from Source

### Prerequisites
- Visual Studio 2019 / 2022
- .NET Framework 4.7.2 SDK
---

## 🏗️ Project Structure

```
WebsiteDownloader/
├── MainForm.cs              # Main UI and download orchestration
├── SettingsForm.cs          # User preferences dialog
├── HistoryForm.cs           # Download history viewer
├── Services/
│   └── WgetDownloader.cs    # Core download engine with events
├── Helpers/
│   └── ResourceExtractor.cs # Embedded wget.exe extraction
├── Models/
│   ├── AppSettings.cs       # Persisted application settings
│   └── DownloadHistory.cs   # Download history storage
└── wget.exe                 # Embedded GNU Wget binary
```

---

## 🤝 Contributing

Contributions are welcome! Here's how you can help:

- 🐛 **Report Bugs** - [Open an issue](https://github.com/badrshs/Complete-Website-Downloader/issues) with steps to reproduce
- 💡 **Suggest Features** - Share your ideas in discussions
- 🔧 **Submit PRs** - Fork, make changes, and submit a pull request

---

## ❓ FAQ

<details>
<summary><b>Is this legal?</b></summary>
<br>
Downloading websites for personal offline use is generally legal. However, always respect copyright and terms of service. Don't redistribute copyrighted content.
</details>

<details>
<summary><b>Why does Windows Defender flag this?</b></summary>
<br>
The app bundles wget.exe and extracts it at runtime. Some antivirus software flags this behavior. The source code is fully open for inspection.
</details>

<details>
<summary><b>Can I download password-protected sites?</b></summary>
<br>
Currently, no. The app downloads publicly accessible pages only.
</details>

<details>
<summary><b>Why are some images missing?</b></summary>
<br>
Some sites load images via JavaScript or block automated downloads. Check the Errors tab for details on what failed.
</details>

<details>
<summary><b>How do I view downloaded sites?</b></summary>
<br>
Open the downloaded folder and double-click the main HTML file (usually <code>index.html</code>) to view in your browser.
</details>

---

## 📋 Changelog

### v2.0.1 (2025)
- ✨ Complete UI redesign with modern dark-themed interface
- 📊 Added Errors tab for troubleshooting failed downloads
- 📜 Download history feature with URL reuse
- ⚙️ Comprehensive settings with persistence
- 🚫 Cancel button for in-progress downloads
- 📝 Real-time download logging

### v1.1.0
- Initial public release
- Basic wget wrapper with simple UI

---

## 📄 License

This project is open source and available under the [MIT License](LICENSE).

---

## 🙏 Acknowledgments

- Powered by [GNU Wget](https://www.gnu.org/software/wget/) - The non-interactive network downloader
- Built with .NET Framework and Windows Forms
---

## 📬 Contact

**Author:** Badr Aldeen Shek Salim  
**GitHub:** [@badrshs](https://github.com/badrshs)  
**Issues:** [Report a bug](https://github.com/badrshs/Complete-Website-Downloader/issues)

---

<p align="center">
  <b>⭐ Star this repository if you find it useful! ⭐</b>
</p>

<p align="center">
  <img src="https://user-images.githubusercontent.com/26596347/136268899-0096141d-8b68-4179-9af9-be24dae9c44e.png" alt="Complete Website Downloader Logo" width="80"/>
</p>

---

<p align="center">
  <sub>
    <b>Keywords:</b> website downloader, offline browser, site ripper, web scraper, wget gui, website copier, 
    html downloader, save website, archive website, offline reading, website backup, mirror site, 
    download webpage, windows app, desktop application, save webpage offline, website archiver, 
    web page saver, site downloader, httrack alternative
  </sub>
</p>
 
