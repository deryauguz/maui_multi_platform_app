# Çok Platformlu Yardımcı ve Bilgi Yönetim Sistemi (.NET MAUI)

## Proje Hakkında

Bu proje, modern yazılım geliştirme standartlarına uygun olarak .NET MAUI (Multi-platform App UI) mimarisi üzerinde inşa edilmiş çok platformlu bir masaüstü ve mobil uygulamadır. Çalışmanın temel amacı; kullanıcıların günlük yaşamlarında sıklıkla ihtiyaç duydukları finansal takip, güncel bilgi edinme, hava durumu analizi ve kişisel zaman yönetimi gibi temel süreçleri tek bir merkezi arayüz üzerinden kesintisiz bir deneyimle sunmaktır.

Proje mimarisi; modüler yazılım tasarımı, gevşek bağlılık (loose coupling) ve yüksek yeniden kullanılabilirlik ilkeleri gözetilerek kurgulanmıştır. Arayüz tasarımı ile iş mantığı katmanları birbirinden izole edilmiş, harici veri kaynakları asenkron mimari üzerinden sisteme entegre edilmiştir.

---

## Sistem Modülleri ve İşlevsel Özellikler

Uygulama, her biri bağımsız veri kaynaklarını işleyen ve kullanıcı etkileşimini yöneten dört temel işlevsel modülden oluşmaktadır:

* **Döviz ve Finans Yönetimi Modülü:**
  * Güncel döviz kurlarını harici finansal servisler ve API uç noktaları üzerinden asenkron olarak çeker.
  * Alınan finansal verileri gerçek zamanlı olarak arayüzde dinamik listeleme bileşenlerine bağlar (Data Binding).
  * Kullanıcıların temel para birimleri arasındaki pariteleri ve anlık kur değişimlerini kolayca izlemesine olanak tanır.

* **Haber ve İçerik Akışı (RSS) Modülü:**
  * Güvenilir haber ajanslarının RSS/XML veri beslemelerine HTTP istekleri gönderir.
  * Ham XML veri bloklarını model sınıflarına ayrıştırarak (XML Parsing) başlık, özet ve yayın tarihi bazında yapılandırılmış veri haline getirir.
  * Kullanıcılara kategorize edilmiş haber bültenlerini okuma ve detay içeriklere yönlenme imkânı sunar.

* **Meteoroloji ve Hava Durumu Modülü:**
  * Konum bazlı hava durumu tahmin verilerini harici RESTful servisler aracılığıyla sorgular.
  * Sıcaklık, nem oranı, rüzgâr durumu ve anlık hava koşullarını JSON formatında tüketip kullanıcı dostu grafiksel bileşenlere dönüştürür.
  * Ağ bağlantısı hatalarına ve veri gecikmelerine karşı hata yakalama (exception handling) mekanizmaları barındırır.

* **Kişisel Görev ve Zaman Yönetimi (To-Do) Modülü:**
  * Kullanıcıların günlük görevlerini, planlarını ve iş listelerini yerel veri yapıları üzerinde depolamasını sağlar.
  * Görev ekleme, tamamlama durumu güncelleme ve liste filtreleme gibi temel CRUD operasyonlarını destekler.
  * Görevlerin durumunu görsel arayüzde anlık olarak güncelleyerek dinamik geri bildirim sunar.

---

## Mimari Yapı ve Teknik Detaylar

Uygulamanın geliştirilmesinde kurumsal kodlama standartlarına ve endüstri standardı mimari desenlere sadık kalınmıştır:

* **Model-View-ViewModel (MVVM) Deseni:** Arayüz (View) katmanı ile iş mantığı (ViewModel) katmanı birbirinden tamamen ayrılmıştır. Bu ayrım, birim testlerin yazılabilirliğini artırmakta ve bakım maliyetlerini düşürmektedir.
* **Veri Bağlama (Data Binding) ve Bildirimler:** Arayüz bileşenleri ile veri modelleri arasında çift yönlü (Two-Way) ve tek yönlü (One-Way) veri bağlama kullanılmıştır. Model durumundaki değişimler `INotifyPropertyChanged` arabirimi üzerinden anlık olarak ekrana yansıtılır.
* **Asenkron Programlama Modeli (TAP):** Harici servis çağrıları ve dosya okuma/yazma işlemleri `async` ve `await` anahtar kelimeleri ile thread bloklaması yapılmadan yürütülür; böylece kullanıcı arayüzünün donması engellenir.
* **Çoklu Platform Uyumluluğu:** Tek bir kod tabanı üzerinden hem Windows masaüstü ortamına hem de mobil (Android) platformlara uyumlu yerel bileşen çıktısı üretilir.

---

## Kullanılan Teknolojiler ve Mimariler

* **Geliştirme Çerçevesi:** .NET MAUI
* **Programlama Dili:** C#
* **Arayüz Tasarımı:** XAML
* **Veri İletişimi:** REST API entegrasyonu, XML/RSS ayrıştırma ve JSON işleme
* **Uygulama Mimarisi:** Model-View-ViewModel (MVVM) prensipleri ve Shell navigasyon yapısı

---

## Proje Dizin Yapısı

```text
GorselProgramlamaOdev3/
├── Pages/             # Görünüm katmanını oluşturan XAML sayfaları ve code-behind dosyaları
├── Models/            # Uygulama genelinde taşınan veri yapıları ve nesne modelleri
├── Services/          # Harici servis ve API bağlantılarını yöneten iş mantığı sınıfları
├── Resources/         # Yazı tipleri, stiller, ikonlar ve görsel varlıklar
└── AppShell.xaml      # Sayfalar arası geçiş ve menü hiyerarşisi
