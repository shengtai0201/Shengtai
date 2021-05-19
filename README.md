## Shengtai
通用函式庫<br/>
Shengtai.Core => .NET 5.0<br/>
Shengtai.Net => .NET Framework 4.6.1<br/>
## Shengtai.IdentityServer
整合 Identity Server 4、ASP.NET Identity、AdminLTE/CoreUI 之 Razor 類別庫<br/>
可用於整合SSL加密之ASP.NET Core gRPC服務<br/>
已整合選單、麵包屑、權限，可外部設計專屬角色、樹狀選單<br/>
資料庫部分目前僅支援PostgreSQL<br/>
Shengtai.IdentityServer => .NET 5.0<br/>
Shengtai.IdentityServer.Builder => 初始化Identity Server 4資料之工具(.NET 5.0)<br/>
Shengtai.IdentityServer.Razor => Razor 類別庫(.NET 5.0)<br/>
## 目錄
- [如何設定]
- [其他注意事項]
- [已知缺陷]
- [待辦事項]
## 如何設定
## 其他注意事項
- 參考網站之appsettings.json必須用"具有BOM的UTF-8"的編碼儲存
- database first 語法
Scaffold-DbContext "Host=127.0.0.1;Database=Land;Username=postgres" Npgsql.EntityFrameworkCore.PostgreSQL -Tables "Menu","MenuItem" -UseDatabaseNames -Context NavDbContext -Force
## 已知缺陷
## 待辦事項
- 使用者管理
- 選單目錄設定
- 權限設定
- 選單之 Badge 功能: SignalR-samples/StockTickR
- legacy user menu
- profile
- 404
- 500
- layout 分 Header, Footer, Sidebar, Main
- messages: user to user
- notifications by system 所整理歸納
- 個人常用選單