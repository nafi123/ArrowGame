# ŞEKİLGEÇ — Neon Shape Rush

Mobil, hyper/hybrid-casual bir **şekil-eşleştirme koşu oyunu**. Tek dosya (`web/index.html`),
harici bağımlılık yok (görsel/font/ses dahil hepsi gömülü). Doğrudan tarayıcıda çalışır ve
Capacitor ile Play Store / App Store'a native uygulama olarak paketlenebilir.

## Oynanış
- **4 şerit** var. Karakter altta durur, engeller yukarıdan hızlanarak iner.
- **Sağa/sola kaydır** (masaüstü: ← →) → şerit değiştir.
- Alttaki **2 şekil seçeneğinden** birine dokun (masaüstü: `1` / `2`) → karakterinin şekli değişir.
- Her engel duvarında, her şeridin bir **şekil deliği** (yuvarlak/kare/üçgen/dikdörtgen) ya da
  ölümcül **✕** vardır. Bulunduğun şeridin deliği **senin şeklinle aynıysa içinden süzülürsün**.
- Yanlış şekil ya da ✕ = çarpma. **Kıl payı geçiş** = slow-motion + bonus puan. **◆ altın** topla.
- Her engel her zaman çözülebilir üretilir (adalet garantisi — otomasyonla doğrulandı, skor 290+).

## Uygulanan özellikler (seçilenler)
- ✅ **Tam Juice + Haptics**: ekran sarsıntısı, parçacık patlaması, karakter izi, `navigator.vibrate`, WebAudio sesleri.
- ✅ **Near-miss slow-mo**: kıl payı geçişte 0.35x slow-mo + `+15` bonus.
- ✅ **Coin + Kostüm/Tema**: 6 neon skin, altınla açılır; seçim kalıcı (`localStorage`).
- ✅ **Günlük görev + Skor**: her gün rastgele görev (altın/skor/near-miss), rekor kaydı.
- ✅ **Hibrit gelir**: ödüllü reklamla devam + interstitial + "Reklamsız" IAP + kozmetik ekonomi.
- ✅ **Neon/minimal** görsel dil.

## Yerelde çalıştırma
```bash
# herhangi bir statik sunucu yeterli
cd web && python3 -m http.server 8080
# tarayıcıda: http://localhost:8080
```
Ya da `web/index.html` dosyasını doğrudan tarayıcıda açın (mobil için Chrome DevTools cihaz modu önerilir).

## Production: reklamla para kazanma (yapılacaklar)
Reklamlar şu an **stub** (`AdManager` içinde sahte sayaç). Gerçek gelir için:

1. **Native sarma (Capacitor):**
   ```bash
   npm i -g @capacitor/cli
   npx cap init sekilgec com.senindomainin.sekilgec
   # webDir olarak "web" klasörünü göster (capacitor.config: webDir: "web")
   npx cap add android && npx cap add ios
   ```
2. **AdMob entegrasyonu** (`admob-plus-capacitor` veya `@capacitor-community/admob`):
   - `AdManager.showRewarded()` → gerçek **ödüllü video** (öldüğünde devam etme). En yüksek eCPM burada.
   - `AdManager.showInterstitial()` → oyun bitiminde geçiş reklamı (sıklığı sınırla; 2–3 oyunda 1).
   - `web/index.html` içinde `AdManager` nesnesi tek değiştirme noktasıdır; arayüz aynı kalır.
3. **IAP** (`@capacitor-community/in-app-purchases`):
   - "Reklamsız" butonu → tek seferlik satın alma; `save.noAds = true`.
   - İstenirse kozmetik paketleri IAP olarak da satılabilir (şu an altın ekonomisiyle çalışıyor).
4. **Mediation/waterfall**: AdMob mediation ile 8–15 katman + in-app bidding → gelir %10–20 artışı.

## Sektör/gelir notları (araştırma özeti)
- Ödüllü video eCPM (tier-1): **$15–40**; interstitial'dan 2–3x, banner'dan ~10x yüksek.
- Bu tür (şekil-eşleştirme) kanıtlanmış ama doygun → **cila + near-miss + revive** ile farklılaşır.
- Gerçekçi tutma hedefi (iyi cila ile): **D1 %35–45, D7 %10–15**.

## Sonraki adım fikirleri
- Gerçek **haptik desenler** (iOS Taptic), **online skor tablosu** (Firebase), **A/B ilk-oturum akışı**,
  **tema/mevsim etkinlikleri**, **combo çarpanı UI'ı**, **kısa video reklam creative'leri** (UA için).
