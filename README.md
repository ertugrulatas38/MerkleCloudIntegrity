# MerkleCloudIntegrity

MerkleCloudIntegrity, SHA-256 tabanlı Merkle Tree yapısını kullanarak dosya bütünlüğü doğrulama, bulut veri denetimi, traversal optimizasyonu ve Merkle tabanlı imza simülasyonu yapan bir .NET 8 konsol uygulamasıdır.

Proje, özellikle akademik deneylerde kullanılabilecek ölçümler üretmek amacıyla hazırlanmıştır. Uygulama farklı dosya boyutları ve farklı kimlik doğrulama yolu üretim yaklaşımları için çalışma süresi, bellek kullanımı ve doğrulama performansı gibi verileri CSV formatında dışa aktarır.

## Amaç

Bu çalışmanın temel amacı, Merkle Tree veri yapısının bulut veri bütünlüğü ve hash tabanlı imza sistemleri üzerindeki kullanımını deneysel olarak incelemektir.

Proje şu konulara odaklanır:

- Dosyaları bloklara ayırarak Merkle Tree oluşturma
- SHA-256 ile Merkle Root üretme
- Seçilen bloklar için Merkle Proof üretme
- Orijinal ve değiştirilmiş bloklar üzerinde bütünlük doğrulama
- Klasik ve önbellekli traversal yaklaşımlarını karşılaştırma
- Merkle tabanlı imza üretimi ve doğrulamasını simüle etme
- Deney sonuçlarını CSV dosyası olarak kaydetme

## Kullanılan Teknolojiler

- C#
- .NET 8
- Console Application
- SHA-256
- CSV tabanlı deney çıktıları

## Proje Yapısı

```text
MerkleCloudIntegrity/
|-- Benchmarks/
|   `-- BenchmarkService.cs
|-- Models/
|   |-- BenchmarkResult.cs
|   |-- CloudIntegritySimulationResult.cs
|   |-- DataBlock.cs
|   |-- MerkleNode.cs
|   |-- MerkleSignature.cs
|   |-- ProofItem.cs
|   |-- SignatureSimulationResult.cs
|   `-- TraversalBenchmarkResult.cs
|-- Services/
|   |-- CloudIntegrityService.cs
|   |-- HashService.cs
|   |-- MerkleTreeService.cs
|   |-- SignatureSimulationService.cs
|   |-- TraversalService.cs
|   `-- VerificationService.cs
|-- Program.cs
|-- MerkleCloudIntegrity.csproj
`-- MerkleCloudIntegrity.sln
```

## Temel Bileşenler

### Models

Model sınıfları, Merkle Tree düğümleri, veri blokları, proof elemanları, benchmark sonuçları ve imza simülasyonu çıktıları için kullanılır.

Öne çıkan modeller:

- `DataBlock`: Dosyadan ayrılan veri bloğunu temsil eder.
- `MerkleNode`: Merkle Tree içindeki düğümü temsil eder.
- `ProofItem`: Merkle Proof içindeki kardeş hash bilgisini tutar.
- `MerkleSignature`: Merkle tabanlı imza simülasyonunda kullanılan yapıdır.
- `BenchmarkResult`: Ölçeklenebilirlik benchmark sonuçlarını tutar.
- `TraversalBenchmarkResult`: Klasik ve önbellekli traversal karşılaştırma sonuçlarını tutar.
- `SignatureSimulationResult`: İmza simülasyonu ölçümlerini tutar.

### Services

Uygulamadaki asıl iş mantığı servis sınıflarında yer alır.

- `HashService`: SHA-256 hash üretimi yapar.
- `MerkleTreeService`: Dosya bloklarını Merkle Tree yapısına dönüştürür, root ve proof üretir.
- `VerificationService`: Merkle Proof doğrulaması yapar.
- `CloudIntegrityService`: Dosya bloklarının bulutta saklanmasını ve bozulma tespitini simüle eder.
- `TraversalService`: Klasik ve önbellekli authentication path üretimini yönetir.
- `SignatureSimulationService`: Merkle tabanlı imza üretimi ve doğrulamasını simüle eder.

### Benchmarks

`BenchmarkService`, deneysel ölçüm altyapısını içerir. Dosya boyutlarına göre ölçeklenebilirlik testi yapabilir, traversal yöntemlerini karşılaştırabilir ve sonuçları CSV dosyası olarak dışa aktarabilir.

## Uygulama Nasıl Çalıştırılır?

Projeyi derlemek için:

```bash
dotnet build MerkleCloudIntegrity.sln
```

Uygulamayı çalıştırmak için:

```bash
dotnet run --project MerkleCloudIntegrity.csproj
```

Varsayılan çalıştırmada Merkle tabanlı imza simülasyonu yapılır. Program tamamlandığında CSV çıktısının dosya yolunu ekrana yazar.

Örnek çıktı:

```text
Merkle Cloud Integrity
----------------------
Signature simulation completed
CSV path: C:\Users\...\AppData\Local\Temp\merkle-cloud-integrity-signature\signature-simulation-results.csv
```

## Üretilen CSV Çıktıları

### İmza Simülasyonu

İmza simülasyonu çıktısı aşağıdaki kolonları içerir:

```text
TraversalMode,LeafIndex,AuthenticationPathLength,SignatureGenerationTimeMs,SignatureVerificationTimeMs,MemoryUsageBytes
```

Bu çıktı, klasik traversal ve önbellekli traversal yaklaşımlarını imza üretimi/doğrulaması açısından karşılaştırmak için kullanılır.

### Traversal Benchmark

Traversal karşılaştırması için kullanılan kolonlar:

```text
TraversalMode,FileName,BlockCount,ProofGenerationTimeMs,VerificationTimeMs,MemoryUsageBytes
```

### Ölçeklenebilirlik Benchmark

Dosya boyutlarına göre ölçeklenebilirlik deneylerinde kullanılan kolonlar:

```text
FileName,BlockSize,BlockCount,BuildTimeMs,ProofGenerationTimeMs,VerificationTimeMs,MemoryUsageBytes,MerkleRoot
```

## Deney Senaryoları

Projede aşağıdaki deney senaryoları desteklenir:

1. **Dosya Bütünlüğü Simülasyonu**
   - Dosya 1 KB bloklara ayrılır.
   - Merkle Tree oluşturulur.
   - Seçilen blok için Merkle Proof üretilir.
   - Blok içeriği değiştirildiğinde doğrulamanın başarısız olduğu gösterilir.

2. **Ölçeklenebilirlik Benchmark**
   - Rastgele örnek dosyalar oluşturulur.
   - 64 KB, 256 KB, 1 MB, 5 MB ve 10 MB dosya boyutları test edilir.
   - Her dosya 1 KB bloklara ayrılır.
   - Ağaç oluşturma, proof üretme, doğrulama ve bellek kullanımı ölçülür.

3. **Traversal Optimizasyonu**
   - Klasik traversal her proof isteğinde authentication path'i baştan üretir.
   - Önbellekli traversal daha önce üretilen authentication path bilgisini tekrar kullanır.
   - Proof üretim süresi, doğrulama süresi ve bellek kullanımı karşılaştırılır.

4. **Merkle Tabanlı İmza Simülasyonu**
   - Bir leaf index, özel anahtar konumu gibi ele alınır.
   - Seçilen leaf için authentication path üretilir.
   - İmza üretimi ve doğrulaması simüle edilir.
   - Klasik ve önbellekli traversal yaklaşımları karşılaştırılır.

## Akademik Kullanım

Bu proje, Merkle Tree tabanlı veri bütünlüğü ve hash tabanlı imza sistemleri üzerine yapılacak deneysel çalışmalar için temel bir uygulama altyapısı sunar.

Elde edilen CSV çıktıları:

- Makalelerde tablo oluşturmak,
- Traversal optimizasyonlarını karşılaştırmak,
- Dosya boyutu arttıkça performans değişimini incelemek,
- Bellek kullanımı ve doğrulama süresi ilişkisini değerlendirmek

için kullanılabilir.

## Notlar

- Varsayılan blok boyutu 1 KB olarak belirlenmiştir.
- Hash algoritması olarak SHA-256 kullanılır.
- GUI bulunmamaktadır; proje konsol uygulaması olarak tasarlanmıştır.
- Benchmark dosyaları geçici klasörde oluşturulur.
- CSV çıktıları çalışma sonunda ekrana yazılan dosya yolunda bulunabilir.
