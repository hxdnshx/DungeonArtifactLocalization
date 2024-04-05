# DungeonArtifactLocalization
[DungeonArtifact](https://store.steampowered.com/app/2144220/_/) 本地化模组

[下载链接](../../releases)

## 安装（整合包）
1. 通过下载链接下载 `DAT_整合.zip`，解压到游戏的本体目录，即 `steamapps\common\DUNGEON_ARTIFACT`，
你可以通过Steam页面指定游戏上的 右键菜单->管理->浏览本地文件 进入到这个目录。成功进入后，你应当可以见到 `DUNGEON_ARTIFACT.exe`。
成功解压后你应当可以看到 `winhttp.dll` 与 `DUNGEON_ARTIFACT.exe` 在同一层目录上。

## 安装
### 1. 安装模组框架
- 从[BepInEx](https://github.com/BepInEx/BepInEx)下载最新版本的模组框架(5.4.x版本)

- 在Steam主界面右键游戏点击管理 浏览本地文件夹 将压缩包内所有东西**不 要 新 建 文 件 夹**的解压到上述弹出的文件夹中
### 2. 安装汉化
- 从[最新版](../../releases)下载 `DAT.zip` 非整合包后解压到上述文件夹中


## 构建
1. 使用 `git clone` 克隆本项目
2. 按照上面 `安装` 所述步骤在游戏目录安装 BepInEx 框架。
3. 修改 `DungeonArtifactTrans\GameFolder.props`，将里面的 `GameFolder` 替换为当前环境下的游戏目录。
4. 启动任意 IDE，进行构建


# 免责声明
- **使用本模组完全免费。**
- **我们使用了MIT许可证。** 请前往[`LICENSE`](./LICENSE)了解相关许可。

# 贡献者
## 本仓库
<a href="https://github.com/hxdnshx/DungeonArtifactLocalization/graphs/contributors">
  <img src="https://contrib.rocks/image?repo=hxdnshx/DungeonArtifactLocalization" />
</a>

## [译文ParaTranz项目](https://paratranz.cn/projects/9841/leaderboard)