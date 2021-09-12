# Lottery

## 部屬

### 前端

```
cd Lottery/ClientApp
ng build --prod
```

### 後端

1. 修改 `Lottery/appsettings.json` 的 `Key`、`Email`、`UserName`、`Password`

2. 將 `Lottery/Controllers/AuthController.cs` 第 83 行 `SecurityAlgorithms.HmacSha256` 改為 `SecurityAlgorithms.HmacSha256Signature`

3. 將前端編譯出來的 `dist` 資料夾 改為 `wwwroot` 放到 `Lottery` 底下