# 🍔 GastroMaster

**SignalR ile Anlık Güncellenen, Yapay Zekâ Destekli Restoran Yönetim Platformu**

![.NET](https://img.shields.io/badge/.NET%206-512BD4?style=flat&logo=dotnet&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-512BD4?style=flat&logo=dotnet&logoColor=white)
![Entity Framework](https://img.shields.io/badge/Entity%20Framework%20Core-68217A?style=flat)
![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=flat&logo=microsoftsqlserver&logoColor=white)
![SignalR](https://img.shields.io/badge/SignalR-0078D4?style=flat)
![JWT](https://img.shields.io/badge/JWT-000000?style=flat&logo=jsonwebtokens&logoColor=white)
![OpenAI](https://img.shields.io/badge/OpenAI-412991?style=flat&logo=openai&logoColor=white)
![Claude](https://img.shields.io/badge/Claude-D97757?style=flat&logo=anthropic&logoColor=white)

Menü, sipariş, masa, rezervasyon ve sepet işlemlerini yöneten; SignalR ile anlık güncellenen, JWT ve rol bazlı yetkilendirme, yapay zekâ analizleri, masaya özel QR kod ve PDF/Excel raporlama içeren katmanlı mimaride ASP.NET Core Web API & MVC restoran projesi.

---

## 📋 İçindekiler

- [Genel Bakış](#-genel-bakış)
- [Öne Çıkan Özellikler](#-öne-çıkan-özellikler)
- [Kullanılan Teknolojiler](#️-kullanılan-teknolojiler)
- [Proje Yapısı](#️-proje-yapısı)
- [Ekran Görüntüleri](#-ekran-görüntüleri)
- [Kurulum](#-kurulum)
- [Kimlik Doğrulama & Roller](#-kimlik-doğrulama--roller)

---

## 📖 Genel Bakış

GastroMaster, bir restoranın günlük operasyonlarını tek bir çatı altında yönetebilmesi için geliştirilmiş bir web platformudur. Proje iki ana uygulamadan oluşur:

- **Web API**: Tüm iş mantığı ve veri erişimi burada çalışır. JWT ile korunur, SignalR hub'ı buradan yayın yapar.
- **Web UI (MVC)**: Müşterilerin kullandığı genel site ve yöneticilerin kullandığı Admin paneli. Veritabanına doğrudan erişmez, tüm işlemleri `IHttpClientFactory` ile API üzerinden yapar.

Masa doluluk durumları, kasa, günlük kazanç ve sipariş istatistikleri SignalR sayesinde sayfa yenilenmeden anlık olarak güncellenir.

---

## ✨ Öne Çıkan Özellikler

### 🌐 Genel Site

- Slider, indirimler, hakkımızda ve yol haritası (timeline) bölümleri
- Kategoriye göre filtrelenebilen menü
- Giriş yapmış kullanıcılar için sepete ürün ekleme ve sepetten ürün silme
- Rezervasyon formu
- Müşteri yorumları ve bülten aboneliği
- SignalR tabanlı canlı destek
- Giriş yapmış kullanıcılar için geri bildirim formu (OpenAI Moderation ile uygunsuz içerik kontrolü)
- Özel tasarlanmış 401 / 403 / 404 hata sayfaları

### 🛠️ Yönetim Paneli

- SignalR ile canlı Dashboard (kasa, bugünün kazancı, aktif sipariş ve masa istatistikleri)
- Masaların anlık doluluk durumu, masa bazlı sipariş detayı ve **masaya özel QR kod üretimi**
- Ürün, kategori, indirim, slider, hakkımızda, yol haritası, referans ve sosyal medya yönetimi (CRUD)
- Rezervasyon onaylama ve müşteriye **otomatik onay maili** gönderme (MailKit)
- Geri bildirimleri okundu / okunmadı olarak işaretleme
- Canlı destek üzerinden ziyaretçilere cevap verme
- Rol oluşturma, düzenleme ve kullanıcılara **dinamik rol atama**
- Profil ve şifre ayarları
- Rezervasyon, ürün, kullanıcı, geri bildirim, bülten ve masa listelerini **Excel (ClosedXML)** ve **PDF (QuestPDF)** olarak dışa aktarma

### 🤖 Yapay Zekâ Entegrasyonları

| Özellik | Kullanılan Servis | Açıklama |
|---|---|---|
| AI Chat | Anthropic Claude | Yönetici için restoran operasyonlarına yönelik sohbet asistanı |
| Geri Bildirim Analizi | OpenAI (GPT-4o-mini) | Tüm geri bildirimleri analiz ederek genel memnuniyet, öne çıkan sorunlar ve öneriler sunar |
| Rezervasyon Analizi | OpenAI (GPT-4o-mini) | Rezervasyon verilerinden yoğun gün/saat ve kapasite yorumları çıkarır |
| Menü Analizi | OpenAI (GPT-4o-mini) | Ürün ve fiyatları inceleyerek menü dengesi ve fiyatlandırma önerileri sunar |
| İçerik Kontrolü | OpenAI Moderation | Sitedeki formlardan gelen metinleri hakaret, nefret ve şiddet içeriğine karşı kontrol eder |

---

## 🛠️ Kullanılan Teknolojiler

| Katman | Teknolojiler |
|---|---|
| Backend | ASP.NET Core 6 Web API, Entity Framework Core, MS SQL Server |
| Frontend | ASP.NET Core MVC, Razor, Bootstrap, jQuery, Ready Bootstrap Dashboard (Admin) |
| Kimlik Doğrulama | ASP.NET Core Identity, JWT Bearer, Cookie Authentication, Rol Bazlı Yetkilendirme |
| Gerçek Zamanlı | SignalR |
| Doğrulama | FluentValidation |
| Nesne Eşleme | AutoMapper |
| Yapay Zekâ | OpenAI Chat Completions & Moderation API, Anthropic Claude Messages API |
| Raporlama | ClosedXML (Excel), QuestPDF (PDF) |
| Diğer | QRCoder, MailKit, Swagger, IHttpClientFactory, View Components, Areas |

### Mimari Desenler

- **N-Tier Architecture**: Entity, DataAccess, Business, Dto, WebApi ve WebUI katmanları
- **Generic Repository & Service** yapısı
- **DTO Pattern**: Create / Update / Result varyantları
- **ViewComponent** mimarisi ile parçalara ayrılmış sayfalar
- **Area-based** modülerleştirme (Admin)

---

## 🏗️ Proje Yapısı

```
GastroMaster
├── GastroMaster.EntityLayer        → Entity sınıfları
├── GastroMaster.DataAccessLayer    → DbContext, repository'ler, migration'lar
├── GastroMaster.BusinessLayer      → Servisler ve FluentValidation kuralları
├── GastroMaster.DtoLayer           → Katmanlar arası veri transfer nesneleri
├── GastroMaster.WebApi             → Controller'lar, JWT doğrulama, SignalR Hub
└── GastroMaster.WebUI              → MVC arayüz (API'yi HttpClient ile tüketir)
    ├── Areas/Admin                 → Yönetim paneli
    ├── ViewComponents              → Genel site bölümleri
    └── Services                    → JWT üretimi ve token'ı API isteklerine ekleyen handler
```

---

## 🖼 Ekran Görüntüleri

### 🌐 Genel Site

| Ana Sayfa | Menü |
|---|---|
| ![Ana Sayfa](GastroMaster.WebUI/wwwroot/images/Ana%20Sayfa.png) | ![Menü](GastroMaster.WebUI/wwwroot/images/Ana%20Sayfa%20Men%C3%BC.png) |

| Rezervasyon | Sepet |
|---|---|
| ![Rezervasyon](GastroMaster.WebUI/wwwroot/images/Ana%20Sayfa%20Rezervasyon.png) | ![Sepet](GastroMaster.WebUI/wwwroot/images/Sepet%20Ekran%C4%B1.png) |

| Canlı Destek | Geri Bildirim İçerik Kontrolü |
|---|---|
| ![Canlı Destek](GastroMaster.WebUI/wwwroot/images/Ana%20Sayfa%20Canl%C4%B1%20Destek.png) | ![Geri Bildirim İçerik Kontrolü](GastroMaster.WebUI/wwwroot/images/Ana%20Sayfa%20Geri%20Bildirim%20%C4%B0%C3%A7erik%20Kontrol%C3%BC.png) |

| Footer |
|---|
| ![Footer](GastroMaster.WebUI/wwwroot/images/Ana%20Sayfa%20Footer.png) |

### 🛠️ Yönetim Paneli

| Dashboard | SignalR Anlık İstatistikler |
|---|---|
| ![Dashboard](GastroMaster.WebUI/wwwroot/images/Admin%20Dashboard.png) | ![SignalR Anlık İstatistikler](GastroMaster.WebUI/wwwroot/images/Admin%20SignalR%20Anl%C4%B1k%20%C4%B0statistikler.png) |

| Masalar ve QR Kod | Rezervasyonlar |
|---|---|
| ![Masalar ve QR Kod](GastroMaster.WebUI/wwwroot/images/Admin%20Masa%20Ve%20Qr%20Kod%20Listesi.png) | ![Rezervasyonlar](GastroMaster.WebUI/wwwroot/images/Admin%20Rezervasyon%20Ekran%C4%B1.png) |

| Geri Bildirimler | Canlı Destek |
|---|---|
| ![Geri Bildirimler](GastroMaster.WebUI/wwwroot/images/Admin%20Geri%20Bildirim%20Ekran%C4%B1.png) | ![Canlı Destek](GastroMaster.WebUI/wwwroot/images/Admin%20Canl%C4%B1%20Destek.png) |

### 🤖 Yapay Zekâ Özellikleri

| AI Chat (Claude) | Geri Bildirim Analizi |
|---|---|
| ![AI Chat](GastroMaster.WebUI/wwwroot/images/Admin%20AI%20Chat.png) | ![Geri Bildirim Analizi](GastroMaster.WebUI/wwwroot/images/Admin%20AI%20Geri%20Bildirim%20Analizi.png) |

| Rezervasyon Analizi | Menü Analizi |
|---|---|
| ![Rezervasyon Analizi](GastroMaster.WebUI/wwwroot/images/Admin%20AI%20Rezervasyon%20Analizi.png) | ![Menü Analizi](GastroMaster.WebUI/wwwroot/images/Admin%20AI%20Men%C3%BC%20Analizi.png) |

### ⚠️ Hata Sayfaları

| 401 | 403 | 404 |
|---|---|---|
| ![401](GastroMaster.WebUI/wwwroot/images/401%20Sayfas%C4%B1.png) | ![403](GastroMaster.WebUI/wwwroot/images/403%20Sayfas%C4%B1.png) | ![404](GastroMaster.WebUI/wwwroot/images/404%20Sayfas%C4%B1.png) |

---

## 🚀 Kurulum

### Gereksinimler

- .NET 6 SDK
- MS SQL Server
- Visual Studio 2022
- Yapay zekâ özellikleri için OpenAI ve Anthropic API anahtarları
- Mail gönderimi için Gmail Uygulama Şifresi

### 1. Projeyi klonlayın

```bash
git clone https://github.com/EmreHaci03/GastroMaster.git
```

### 2. Veritabanını oluşturun

```bash
dotnet ef database update --project GastroMaster.DataAccessLayer --startup-project GastroMaster.WebApi
```

### 3. Gizli ayarları girin (User Secrets)

Gizli bilgiler `appsettings.json` içinde tutulmaz. Visual Studio'da projeye sağ tıklayıp **Manage User Secrets** ile ekleyin. `Jwt:Key` her iki projede **aynı** olmalıdır.

**GastroMaster.WebApi**

```json
{
  "Jwt": {
    "Key": "en-az-32-karakterlik-gizli-anahtariniz"
  }
}
```

**GastroMaster.WebUI**

```json
{
  "Jwt": {
    "Key": "en-az-32-karakterlik-gizli-anahtariniz"
  },
  "OpenAI": {
    "ApiKey": "sk-..."
  },
  "Anthropic": {
    "ApiKey": "sk-ant-..."
  },
  "MailSettings": {
    "SenderMail": "ornek@gmail.com",
    "Password": "uygulama-sifresi"
  }
}
```

> ⚠️ API anahtarlarını asla kaynak kontrolüne (Git) dahil etmeyin. Gmail SMTP için normal hesap şifresi değil, Google hesabında oluşturulan **Uygulama Şifresi** kullanılmalıdır.

### 4. Çalıştırın

Solution'a sağ tıklayıp **Configure Startup Projects** üzerinden `GastroMaster.WebApi` ve `GastroMaster.WebUI` projelerini birlikte başlatın.

- Web API: `https://localhost:7139`
- Web UI: `https://localhost:7065`

### 📱 Masaya Özel QR Kod

Admin panelindeki **Masalar** sayfasından her masa için, masanın numarasını içeren masaya özel bir QR kod oluşturulabilir ve PNG olarak indirilebilir.

---

## 🔐 Kimlik Doğrulama & Roller

- Kullanıcılar ASP.NET Core Identity ile kayıt olur ve giriş yapar. Başarısız denemelerde hesap geçici olarak kilitlenir.
- Giriş başarılı olduğunda WebUI bir **JWT** üretir ve `HttpOnly` cookie içinde saklar.
- Bir `DelegatingHandler`, API'ye giden her isteğe bu token'ı **Bearer** olarak ekler.
- API tarafında controller'lar `[Authorize(Roles = "Admin")]` ile korunur; herkese açık işlemler (menü, rezervasyon vb.) `[AllowAnonymous]` ile açılır.
- WebUI'da global yetkilendirme politikası ve otomatik CSRF (Antiforgery) doğrulaması uygulanır.

| Rol | Erişim |
|---|---|
| Admin | Yönetim paneli: tüm içerik, masa, rezervasyon, rol, rapor ve AI ekranları |
| Üye | Genel site + sepet işlemleri ve geri bildirim gönderme |
| Ziyaretçi | Genel site: menü, rezervasyon ve canlı destek |

