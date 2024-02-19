# infera-test-case
 
Uygulamayı geliştirmek için Volosoft tarafından geliştirilen kütüphaneleri kullanmaya karar verdim. 

Volosoft Abp Framework Açık kaynaklı(open source) bir web uygulama framework'üdür. Amacı sürdürülebilir kod yazılmasını sağlamaktır. Aynı zamanda developerların kod tekrarını engeller ve high level kod yazılmasını sağlar.

abp cli kurmak istediğimde aşağıdaki hatayı aldım.

```console
C:\Users\PC>dotnet tool install -g Volo.Abp.Cli
Unhandled exception: Microsoft.DotNet.Cli.NuGetPackageDownloader.NuGetPackageNotFoundException: volo.abp.cli::[*, ), C:\Program Files (x86)\Microsoft SDKs\NuGetPackages\ NuGet akışlarında bulunamadı.
   at Microsoft.DotNet.Cli.NuGetPackageDownloader.NuGetPackageDownloader.GetMatchingVersionInternalAsync(String packageIdentifier, IEnumerable`1 packageSources, VersionRange versionRange, CancellationToken cancellationToken)
   at Microsoft.DotNet.Cli.NuGetPackageDownloader.NuGetPackageDownloader.GetBestPackageVersionAsync(PackageId packageId, VersionRange versionRange, PackageSourceLocation packageSourceLocation)
   at Microsoft.DotNet.Cli.ToolPackage.ToolPackageDownloader.<>c__DisplayClass8_0.<InstallPackage>b__0()
   at Microsoft.DotNet.Cli.TransactionalAction.Run[T](Func`1 action, Action commit, Action rollback)
   at Microsoft.DotNet.Tools.Tool.Install.ToolInstallGlobalOrToolPathCommand.Execute()
   at System.CommandLine.Invocation.InvocationPipeline.Invoke(ParseResult parseResult)
   at Microsoft.DotNet.Cli.Program.ProcessArgs(String[] args, TimeSpan startupTime, ITelemetry telemetryClient)
```

Sorunun .net 8 versiyonunun eski olmasından kaynaklandığını konu ile ilgili olarak issue oluşturulduğunu gördüm. 
https://github.com/dotnet/sdk/issues/35566

İlk olarak Windows update ve VS updatelerini tamamladım. Projeyi .Net 8 ile geliştirmeyi planlıyorum.
Sorunun Nuget package soruce'unun offline olarak ayarlanmasından kaynaklandığını belirledim.


![image](https://github.com/hasanbinboga/infera-test-case/assets/27738643/c99780df-1e4f-4695-afb8-826e180f0f33)

nuget source url: https://api.nuget.org/v3/index.json olarak güncellendi. 

```console
C:\Repos\infera-test-case\Infera>dotnet tool install --global Volo.Abp.Cli --version 8.1.0-rc.1
NuGet paket imzası doğrulaması atlanıyor.
Şu komutu kullanarak aracı çağırabilirsiniz: abp
'volo.abp.cli' aracı (sürüm '8.1.0-rc.1') başarıyla yüklendi.
```

Ayrıca Visual Studio 2022 güncellemeleri de yapıldı.

```console
C:\Users\PC>dotnet --version
8.0.200
```

Veritabanı olarak SqlServer kullanmayı planlıyorum. Bu nedenle Sql Server developer edition (https://go.microsoft.com/fwlink/p/?linkid=2215158&clcid=0x41f&culture=tr-tr&country=tr) ve SSMS indiridip kurdum.

![image](https://github.com/hasanbinboga/infera-test-case/assets/27738643/02ea0c76-9f34-4a2e-9bd0-bf4c7b099a0b)

Ardından Infera adında bir db ve infera_app isminde bir db user ı oluşturdum. Uygulamanın veritabanına bu user ile bağlanmasını planlıyorum.

Daha sonra CLI komutlarını (https://docs.abp.io/en/abp/latest/CLI) kullanarak yeni projeyi oluşturdum.
```console
abp new Infera.TestCase --template app --ui angular --separate-auth-server --database-provider ef --theme leptonx-lite --create-solution-folder --with-public-website -dbms SqlServer --connection-string "Server=localhost;Database=Infera;User Id=infera_app;Password=123;Trusted_Connection=True;"
```

Devamında Migrator uygulaması çalıştırılarak veritabanı initilize yapıldı.
![image](https://github.com/hasanbinboga/infera-test-case/assets/27738643/507070cf-87b1-4833-869b-09887bf997bf)


Cache-Redis için free bir instance oluşturuldu ve app settings güncellendi. 

Infera.TestCase.HttpApi.Host projesi IIS Express üzerinden çalıştırıldığında default swagger ui erişim sağlandı.

![image](https://github.com/hasanbinboga/infera-test-case/assets/27738643/db2fc585-4975-4266-9cf6-4404bddc3105)

Auth Server'ın da HostApp ile birlikte ayağa kalkması için Solution özelliklerinde gerekli güncellemeler yapıldı.

![image](https://github.com/hasanbinboga/infera-test-case/assets/27738643/b7e1bd95-4fa3-4cdc-8d29-6a291dbfb81c)

Solution çalıştırıldığında her iki projenin de sorunsuz şekilde ayağa kalktığı görüldü.

![image](https://github.com/hasanbinboga/infera-test-case/assets/27738643/df4db05b-174e-469b-8bae-cca8cae17134)

Auth server default admin bilgileri ile swagger üzerinden authorize olduk.
![image](https://github.com/hasanbinboga/infera-test-case/assets/27738643/619bebbe-d3cf-442d-850f-a05725d8540c)

Authorize işlemi için kullanıcı bilgileri;
```console
User:admin
Password:1q2w3E*
```

Ardından test amaçlı olarak swagger üzerinden rolleri sorguladım;

![image](https://github.com/hasanbinboga/infera-test-case/assets/27738643/e7637e9c-0538-4144-a7b0-87e875160db7)

Böylece Backend scaffold hazırlanmış oldu.

Frontend projesini ayağa kaldırmak için solution klasöründeki daki angular klasörü içerisinde komut satırı açarak npm paketlerini yüklüyoruz.

```console
npm install -g @angular/cli
npm i --legacy-peer-deps
ng update @angular/cli @angular/core --force
```

Ardından uygulamayı çalıştırıyoruz

```console
npm start
```
![image](https://github.com/hasanbinboga/infera-test-case/assets/27738643/b2fdaabd-3dd7-4cc0-8c82-4b8114ca305a)

Scaffold'un düzgün şekilde çalıştığından emin olduktan sonra test case için verilen bilgilere göre ER Diyagramı oluşturdum.

![image](https://github.com/hasanbinboga/infera-test-case/blob/main/er-diagram.jpg)

Gereksinimler şu şekildedir;
>· Bina, Oda, Depo olacak. Binaya bağlı birden fazla oda ve depo olabilir. Bir odanın birden fazla bina ile bağlantısı olamaz.

>· Envanter/Ürün olacak. Envanter/Ürün depoda saklanacak. Bir envanter/ürün birden fazla depoda olabilir.

>· (Opsiyonel +) Depo Giriş ve Depo Çıkış olacak. Kullanıcı bir ürün eklediğinde o hangi depoya eklediyse orada görünecek. Çıkış yaptığında hangi binaya ve odaya ürünün gittiğini görebilecek.

>· (Opsiyonel +) İş Emri tanımı olacak. Bu tanımı bina/oda/depo için veya envanter/ürün için bakım talepleri gibi düşünebilirsiniz. İş Emri tanımları xxx binadaki xxx odada bulunan xxx kodlu ürünün bakımı/temizliği/kontrolü şeklinde bir yapıda olacak. 



ER diagramında belirtilen entity ler ile ilgili geliştirimeler tamamlanarak migration oluşturuldu;

```console
dotnet ef migrations add InitialMigration 
```

Uygulama başarılı olarak derlendiği halde aşağıdaki uyarıları verdi;

```console
PS C:\Repos\infera-test-case\Infera.TestCase\aspnet-core\src\Infera.TestCase.EntityFrameworkCore> dotnet ef Migrations Add InferaDbInit
Build started...
Build succeeded.
The foreign key property 'Accounting.ProductInventoryId1' was created in shadow state because a conflicting property with the simple name 'ProductInventoryId' exists in the entity type, but is either not mapped, is already used for another relationship, or is incompatible with the associated primary key type. See https://aka.ms/efcore-relationships for information on mapping relationships in EF Core.
The foreign key property 'Accounting.SaleOrderId1' was created in shadow state because a conflicting property with the simple name 'SaleOrderId' exists in the entity type, but is either not mapped, is already used for another relationship, or is incompatible with the associated primary key type. See https://aka.ms/efcore-relationships for information on mapping relationships in EF Core.
The foreign key property 'BuildingWarehouse.BuildingId1' was created in shadow state because a conflicting property with the simple name 'BuildingId' exists in the entity type, but is either not mapped, is already used for another relationship, or is incompatible with the associated primary key type. See https://aka.ms/efcore-relationships for information on mapping relationships in EF Core.
The foreign key property 'BuildingWarehouse.WarehouseId1' was created in shadow state because a conflicting property with the simple name 'WarehouseId' exists in the entity type, but is either not mapped, is already used for another relationship, or is incompatible with the associated primary key type. See https://aka.ms/efcore-relationships for information on mapping relationships in EF Core.
The foreign key property 'Issue.BuildingId1' was created in shadow state because a conflicting property with the simple name 'BuildingId' exists in the entity type, but is either not mapped, is already used for another relationship, or is incompatible with the associated primary key type. See https://aka.ms/efcore-relationships for information on mapping relationships in EF Core.
The foreign key property 'Issue.ProductInventoryId1' was created in shadow state because a conflicting property with the simple name 'ProductInventoryId' exists in the entity type, but is either not mapped, is already used for another relationship, or is incompatible with the associated primary key type. See https://aka.ms/efcore-relationships for information on mapping relationships in EF Core.
The foreign key property 'Issue.RoomId1' was created in shadow state because a conflicting property with the simple name 'RoomId' exists in the entity type, but is either not mapped, is already used for another relationship, or is incompatible with the associated primary key type. See https://aka.ms/efcore-relationships for information on mapping relationships in EF Core.
The foreign key property 'Issue.WarehouseInventoryId1' was created in shadow state because a conflicting property with the simple name 'WarehouseInventoryId' exists in the entity type, but is either not mapped, is already used for another relationship, or is incompatible with the associated primary key type. See https://aka.ms/efcore-relationships for information on mapping relationships in EF Core.
The foreign key property 'Room.BuildingId1' was created in shadow state because a conflicting property with the simple name 'BuildingId' exists in the entity type, but is either not mapped, is already used for another relationship, or is incompatible with the associated primary key type. See https://aka.ms/efcore-relationships for information on mapping relationships in EF Core.
The foreign key property 'SaleOrder.RoomId1' was created in shadow state because a conflicting property with the simple name 'RoomId' exists in the entity type, but is either not mapped, is already used for another relationship, or is incompatible with the associated primary key type. See https://aka.ms/efcore-relationships for information on mapping relationships in EF Core.
The foreign key property 'SaleOrder.WarehouseInventoryId1' was created in shadow state because a conflicting property with the simple name 'WarehouseInventoryId' exists in the entity type, but is either not mapped, is already used for another relationship, or is incompatible with the associated primary key type. See https://aka.ms/efcore-relationships for information on mapping relationships in EF Core.
The foreign key property 'Warehouse.BuildingId1' was created in shadow state because a conflicting property with the simple name 'BuildingId' exists in the entity type, but is either not mapped, is already used for another relationship, or is incompatible with the associated primary key type. See https://aka.ms/efcore-relationships for information on mapping relationships in EF Core.
The foreign key property 'WarehouseInventory.ProductInventoryId1' was created in shadow state because a conflicting property with the simple name 'ProductInventoryId' exists in the entity type, but is either not mapped, is already used for another relationship, or is incompatible with the associated primary key type. See https://aka.ms/efcore-relationships for information on mapping relationships in EF Core.
The foreign key property 'WarehouseInventory.WarehouseId1' was created in shadow state because a conflicting property with the simple name 'WarehouseId' exists in the entity type, but is either not mapped, is already used for another relationship, or is incompatible with the associated primary key type. See https://aka.ms/efcore-relationships for information on mapping relationships in EF Core.
Done. To undo this action, use 'ef migrations remove'
PS C:\Repos\infera-test-case\Infera.TestCase\aspnet-core\src\Infera.TestCase.EntityFrameworkCore>
```

Yapılan incelemede veri tipi uyumsuzlukları nedeniyle ekstra property oluşturduğu tespit edildi. Oluşturulan migration kaldırıldı.

```console
dotnet ef migrations remove
```

Db Context registrationdaki relation tanımları kaldırıldığında uyarıların düzeldiği görüldü. Sadece Issue.Assignee property si için IdentityUser ilişkisi tanımlandı.

```console
PS C:\Repos\infera-test-case\Infera.TestCase\aspnet-core\src\Infera.TestCase.EntityFrameworkCore> dotnet ef Migrations Add InferaDbInit
Build started...
Build succeeded.
Done. To undo this action, use 'ef migrations remove'
```


Devamında migration'ı db'ye yansıtmak amacıyal DbMigrator projesini çalıştırdım.
![image](https://github.com/hasanbinboga/infera-test-case/assets/27738643/cb3975c0-2c11-432c-9bd1-44aec6282862)


```console
[20:48:31 INF] Started database migrations...
[20:48:31 INF] Migrating schema for host database...
[20:48:32 INF] Executing host database seed...
[20:48:35 INF] Successfully completed host database migrations.
[20:48:36 INF] Successfully completed all database migrations.
[20:48:36 INF] You can safely end this process...

C:\Repos\infera-test-case\Infera.TestCase\aspnet-core\src\Infera.TestCase.DbMigrator\bin\Debug\net8.0\Infera.TestCase.DbMigrator.exe (process 10296) exited with code 0.
Press any key to close this window . . .
```

Ardından gerekli index tanımlarını oluşturarak yeniden db migration yaptım.

```console
dotnet ef migrations add CreateIndexes 
```

Migration başarılı şekilde oluştu.

```console
PS C:\Repos\infera-test-case\Infera.TestCase\aspnet-core\src\Infera.TestCase.EntityFrameworkCore> dotnet ef migrations add CreateIndexes
Build started...
Build succeeded.
An operation was scaffolded that may result in the loss of data. Please review the migration for accuracy.
Done. To undo this action, use 'ef migrations remove'
```

Ardından DbMigrator uygulaması ile veri tabanını güncelledim.

![image](https://github.com/hasanbinboga/infera-test-case/assets/27738643/cb3975c0-2c11-432c-9bd1-44aec6282862)

```console
[21:20:19 INF] Started database migrations...
[21:20:19 INF] Migrating schema for host database...
[21:20:20 INF] Executing host database seed...
[21:20:23 INF] Successfully completed host database migrations.
[21:20:23 INF] Successfully completed all database migrations.
[21:20:23 INF] You can safely end this process...

C:\Repos\infera-test-case\Infera.TestCase\aspnet-core\src\Infera.TestCase.DbMigrator\bin\Debug\net8.0\Infera.TestCase.DbMigrator.exe (process 25956) exited with code 0.
Press any key to close this window . . .
```

![image](https://github.com/hasanbinboga/infera-test-case/assets/27738643/628b001e-68f1-4410-95fb-a416a4e7d229)


Artık her bir entity için domain service ve custom repository sınıflarını oluşturabiliriz.

Yaptığım geliştirmeler neticesinde domain service ve custom repo sınıflarını oluşturup, EF Core module'e custom repoları register ettim.

```csharp
 public override void ConfigureServices(ServiceConfigurationContext context)
 {
     context.Services.AddAbpDbContext<TestCaseDbContext>(options =>
     {
         /* Remove "includeAllEntities: true" to create
          * default repositories only for aggregate roots */
         options.AddDefaultRepositories(includeAllEntities: true);

         options.AddRepository<Building, EfCoreBuildingRepository>();
         options.AddRepository<BuildingWarehouse, EfCoreBuildingWarehouseRepository>();
         options.AddRepository<Issue, EfCoreIssueRepository>();
         options.AddRepository<ProductInventory, EfCoreProductInventoryRepository>();
         options.AddRepository<Room, EfCoreRoomRepository>();
         options.AddRepository<SaleOrder, EfCoreSaleOrderRepository>();
         options.AddRepository<WarehouseInventory, EfCoreWarehouseInventoryRepository>();
         options.AddRepository<Warehouse, EfCoreWarehouseRepository>();
     });

     Configure<AbpDbContextOptions>(options =>
     {
         /* The main point to change your DBMS.
          * See also TestCaseMigrationsDbContextFactory for EF Core tooling. */
         options.UseSqlServer();
     });

 }
```


Devamında Building için Application Service katmanı için gerekli geliştirmeleri yaptım. İlk olarak gerekli DTO tanımlarını oluşturdum. Genelde listeleme amaçlı kullandığım DTO'lara ilişkili child entity'lerinin sayılarını ve Parent objelerin lookup propertylerini Datatable da göstermeyi tercih ederim. Building DTO için de child entity sayılarını gösterir propertyleri ekledim. Datatable entity si şu şekilde oldu;

```csharp
using System;
using Volo.Abp.Application.Dtos;

namespace Infera.TestCase.Buildings;

public class BuildingDto: AuditedEntityDto<Guid>
{

    public string Name { get; set; } = null!;
    public string No { get; set; } = null!;
    public string? Addres { get; set; }
    public int? RoomCount { get; set; }
    public int? WarehouseCount { get; set; }
    public int? IssueCount { get; set; } 
}
```

CRUD işlemleri için sadece gerekli property'leri içeren DTO tanımlarım. Burada her bir input için gerekli BE validasyonları da implemente ederim.
```csharp
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Validation;

namespace Infera.TestCase.Buildings;

public class BuildingCreateUpdateDto : EntityDto<Guid>
{
    [Required]
    [DynamicStringLength(typeof(BuildingConsts), nameof(BuildingConsts.MaxNameLength), nameof(BuildingConsts.MinNameLength))]
    public string Name { get; set; } = null!;

    [Required]
    [DynamicStringLength(typeof(BuildingConsts), nameof(BuildingConsts.MaxNoLength))]
    public string No { get; set; } = null!;
    
    [DynamicStringLength(typeof(BuildingConsts), nameof(BuildingConsts.MaxAddresLength))]
    public string? Addres { get; set; } 
}
```
Ayrıca FE selectlerde kullanmak amacıyla lookup dto da geliştirdim.
```csharp
using System;
using Volo.Abp.Application.Dtos;

namespace Infera.TestCase.Buildings;
public class BuildingLookupDto : EntityDto<Guid>
{
    public string Name { get; set; } = null!; 
}
```

Ardından BuildingAppService class ını geliştirdim. Kullandığım framework'ün beklediği şekilde gerekli implemantasyonları yaptığım için AppService class ında geliştirdiğim ve base'den gelen metotları içeren endpointleri otomatik generate etti. Yine burada yetkilendirme için gerekli rol tanımlarını oluşturdum.

![image](https://github.com/hasanbinboga/infera-test-case/assets/27738643/c37cb750-1220-47c2-84ae-cdae97c73df4)


Uygulama aya kalktıktan sonra default parolayı değiştirmemi istedi. Account manage ekranından gerekli değişikliği yaptım.

Yeni parola: Aa123456.


![image](https://github.com/hasanbinboga/infera-test-case/assets/27738643/1bb77f0b-52bf-4ef8-9ad3-3813849fe1ce)

BE tarafındaki temel geliştirmeleri tamamladıktan sonra FE ile ilgili implemantasyonlara başlamaya karar verdim. UI geliştirmeleri ilerledikçe ihtiyaç olacak yeni metotları geliştirerek BE/FE birlikte devam etmeyi planlıyorum.


ilk olarak BE api metotlarını kullanabilmek amacıyla gerekli proxy service implemantasyonları için abp cli dan yararlandım. Böylece swagger tarafındaki implemantasyonları doğrudan angular class ve servislerine çevirdi.

```console 
PS C:\Repos\infera-test-case\Infera.TestCase\angular>abp generate-proxy -t n
```

Ardından Building modülünü oluşturdum.

```console 
C:\Repos\infera-test-case\Infera.TestCase\angular> ng generate module building --module app --routing --route buildings
CREATE src/app/building/building-routing.module.ts (362 bytes)
CREATE src/app/building/building.module.ts (386 bytes)
CREATE src/app/building/building.component.html (24 bytes)
CREATE src/app/building/building.component.spec.ts (638 bytes)
CREATE src/app/building/building.component.ts (218 bytes)
CREATE src/app/building/building.component.scss (0 bytes)
UPDATE src/app/app-routing.module.ts (1033 bytes)
```

Devamında Oda ve depo tanımları için de gerekli FE modülleri geliştirildi.


![image](https://github.com/hasanbinboga/infera-test-case/assets/27738643/84b52a8d-2ca7-4fd5-a69b-2da3dc675964)

![image](https://github.com/hasanbinboga/infera-test-case/assets/27738643/98f900e2-68f0-418a-8f08-409983b929d2)

![image](https://github.com/hasanbinboga/infera-test-case/assets/27738643/ab778161-30a9-4a1b-9258-30f7a6330339)

![image](https://github.com/hasanbinboga/infera-test-case/assets/27738643/263031cc-506a-48f8-8609-1c6de556e925)

Ardından diğer entity'ler için gerekli arayüzler ve BE servisleri geliştirildi.
![image](https://github.com/hasanbinboga/infera-test-case/assets/27738643/04b10192-b0ef-4041-8023-9cec87c80be2)
![image](https://github.com/hasanbinboga/infera-test-case/assets/27738643/4ebe503f-826b-4711-b8d3-d40c5d2ff86b)
![image](https://github.com/hasanbinboga/infera-test-case/assets/27738643/3b175680-1a1b-48a5-b78a-6740171636d9)
![image](https://github.com/hasanbinboga/infera-test-case/assets/27738643/7daa2433-53b2-4a05-976e-b82a9dd87451)
![image](https://github.com/hasanbinboga/infera-test-case/assets/27738643/6b058b57-0e7f-4a6c-9534-27e9debcc490)
![image](https://github.com/hasanbinboga/infera-test-case/assets/27738643/5cf37f59-8a14-4098-bab1-6409d05f26b7)
![image](https://github.com/hasanbinboga/infera-test-case/assets/27738643/5fd5b65e-5627-4262-aac9-b7d62b0c70fc)
![image](https://github.com/hasanbinboga/infera-test-case/assets/27738643/0fd8ca69-8ea2-47ef-8e02-551de6cf4dff)
![image](https://github.com/hasanbinboga/infera-test-case/assets/27738643/aa220a0f-459a-4015-baeb-7f5b6f4994c3)
![image](https://github.com/hasanbinboga/infera-test-case/assets/27738643/10b6a278-2dcc-4939-9377-877a65544d56)
![image](https://github.com/hasanbinboga/infera-test-case/assets/27738643/9175fed7-9081-47e0-96d5-9fd14acb7531)

Ayrıca ABP Framework tarafından sağlanan yetkilendirme ve Tenant management modülleri ile gelen FE arayüzleri üzerinden Kullanıcı, müşteri, rol ve rol yetkilerine ilişkin tanımlar yapılabilmektedir.

![image](https://github.com/hasanbinboga/infera-test-case/assets/27738643/0135648e-5c7d-4f44-ac2a-e3941bb81201)
![image](https://github.com/hasanbinboga/infera-test-case/assets/27738643/882e1966-678a-4dec-bb0c-248312220e34)
![image](https://github.com/hasanbinboga/infera-test-case/assets/27738643/11902678-59a7-434d-af19-a0e296594b64)
![image](https://github.com/hasanbinboga/infera-test-case/assets/27738643/64d5c5a0-e012-4803-843d-0e04630e13a0)

ABP Framework Localization ile ilgili temel altyapıya sahip olduğundan arayüzde ve BE mesajlarındaki mesajların tamamı Domain.Shared projesi altında Localization klasöründe her bir dil için gerekli json dosyasını barındırmaktadır.
Örneğin Türkçe tanımlar şu şekildedir;
![image](https://github.com/hasanbinboga/infera-test-case/assets/27738643/436e4114-8c1e-4762-a8a5-907dcfcda30d)

```json
{
    "culture": "tr",
    "texts": {
        "Menu:Home": "Ana sayfa",
        "Menu:Buildings": "Binalar",
        "Menu:Rooms": "Odalar",
        "Menu:Warehouses": "Depolar",
        "Menu:Products": "Ürünler",
        "Menu:WarehouseInventories": "Depo Envanterleri",
        "Menu:Issues": "Görevler",
        "Menu:BuildingIssues": "Bina görevleri",
        "Menu:RoomIssues": "Oda görevleri",
        "Menu:WarehouseInventoryIssues": "Depo Envanter görevleri",
        "Menu:ProductIssues": "Ürün görevleri",
        "Menu:SaleOrders": "Satış",
        "Menu:Accounting": "Muhasebe",
        "Welcome": "Hoşgeldiniz",
        "CreateIssue": "Görev Oluştur",
        "NewIssue": "Yeni Görev",
        "LongWelcomeMessage": "Uygulamaya hoşgeldiniz. Bu, ABP framework'ü üzerine bina edilmiş bir başlangıç projesidir. Daha fazla bilgi için abp.io adresini ziyaret edebilirsiniz.",
        "Infera.TestCase:00001": "{Name} isimli bina zaten var.",
        "Infera.TestCase:00002": "Bu bina zaten bu depo ile ilişkilidir.",
        "Infera.TestCase:00003": "Bu binada {No} numaralı oda zaten var.",
        "Infera.TestCase:00004": "{Manifacturer} tarafından üretilen {Size} boyutlarındaki {Name} markalı ürün zaten var.",
        "Infera.TestCase:00005": "Belirtilen ürün bu depoda zaten var.",
        "Infera.TestCase:00006": "Bu binada zaten {No} numaralı ve {Name} isimli depo vardır.",
        "Infera.TestCase:00007": "{Date} tarih ve {InvoiceNo} numaralı fatura zaten var.",
        "Permission:Buildings": "Bina Modülü",
        "Enum:IssueType.0": "Bilinmiyor",
        "Enum:IssueType.1": "Temizlik",
        "Enum:IssueType.2": "Onarım",
        "Enum:IssueType.3": "Eksikleri tamamla",
        "Enum:ProductInventoryType.0": "Bilinmiyor",
        "Enum:ProductInventoryType.1": "İçecek",
        "Enum:ProductInventoryType.2": "Atıştırmalık",
        "Enum:ProductInventoryType.3": "Çikolata",
        "Enum:ProductInventoryType.4": "Alkollü içecek",
        "Enum:AccountingType.0": "Bilinmiyor",
        "Enum:AccountingType.1": "Giriş",
        "Enum:AccountingType.2": "Çıkış",
        "ListWarehouses": "Depoları listele",
        "LinkToWarehouse": "Depo ile ilişkilendir",
        "WarehouseList": "Depo Listesi",
        "NewBuildingWarehouse": "Yeni Depo Bina ilişkisi",
        "NewWarehouse": "Yeni Depo",
        "ListBuildings": "Binaları Listele",
        "LinkToBuilding": "Bina ile İlişkilendir"
    }
}
```


Ayrıca yetkilendirmeye ilişkin tanımlar Application.Contracts projesi altında Permissions klasörü altında yer almaktadır. Kullanıcı ve rollerin yetkilendirilmesi amacıyla kullanılan temel tanımlar burada yer almaktadır.

![image](https://github.com/hasanbinboga/infera-test-case/assets/27738643/2802699c-ed96-4c69-b19e-a6705bc4e7eb)

```csharp
namespace Infera.TestCase.Permissions;

public static class TestCasePermissions
{
    public const string GroupName = "TestCase";

    //Add your own permission names. Example:
    
    public static class Buildings
    {
        public const string Default = GroupName + ".Buildings";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
        public const string GetListOfWarehouseAsync = Default + ".GetListOfWarehouseAsync";
    }

    public static class BuildingWarehouses
    {
        public const string Default = GroupName + ".BuildingWarehouses";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }
    
    public static class Rooms
    {
        public const string Default = GroupName + ".Rooms";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }


    public static class Warehouses
    {
        public const string Default = GroupName + ".Warehouses";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
        public const string GetListOfBuildingAsync = Default + ".GetListOfBuildingAsync";
    }

    public static class WarehouseInventories
    {
        public const string Default = GroupName + ".WarehouseInventories";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
        public const string GetListOfBuildingAsync = Default + ".GetListOfBuildingAsync";
    }

    public static class ProductInventories
    {
        public const string Default = GroupName + ".ProductInventories";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public static class Issues
    {
        public const string Default = GroupName + ".Issues";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }
    public static class Accountings
    {
        public const string Default = GroupName + ".Accountings";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public static class SaleOrders
    {
        public const string Default = GroupName + ".SaleOrders";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
        public const string GetListOfBuildingAsync = Default + ".GetListOfBuildingAsync";
    }
}

```

```csharp
using Infera.TestCase.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Infera.TestCase.Permissions;

public class TestCasePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var inferaTestGroup = context.AddGroup(TestCasePermissions.GroupName);

        var buildingsPermission = inferaTestGroup.AddPermission(TestCasePermissions.Buildings.Default, L("Permission:Buildings"));
        buildingsPermission.AddChild(TestCasePermissions.Buildings.Create, L("Permission:Buildings.Create"));
        buildingsPermission.AddChild(TestCasePermissions.Buildings.Edit, L("Permission:Buildings.Edit"));
        buildingsPermission.AddChild(TestCasePermissions.Buildings.Delete, L("Permission:Buildings.Delete"));
        buildingsPermission.AddChild(TestCasePermissions.Buildings.GetListOfWarehouseAsync, L("Permission:Buildings.GetListOfWarehouseAsync"));


        var buildingWarehousesPermission = inferaTestGroup.AddPermission(TestCasePermissions.BuildingWarehouses.Default, L("Permission:BuildingWarehouses"));
        buildingWarehousesPermission.AddChild(TestCasePermissions.BuildingWarehouses.Create, L("Permission:BuildingWarehouses.Create"));
        buildingWarehousesPermission.AddChild(TestCasePermissions.BuildingWarehouses.Edit, L("Permission:BuildingWarehouses.Edit"));
        buildingWarehousesPermission.AddChild(TestCasePermissions.BuildingWarehouses.Delete, L("Permission:BuildingWarehouses.Delete"));

        var roomsPermission = inferaTestGroup.AddPermission(TestCasePermissions.Rooms.Default, L("Permission:Rooms"));
        roomsPermission.AddChild(TestCasePermissions.Rooms.Create, L("Permission:Rooms.Create"));
        roomsPermission.AddChild(TestCasePermissions.Rooms.Edit, L("Permission:Rooms.Edit"));
        roomsPermission.AddChild(TestCasePermissions.Rooms.Delete, L("Permission:Rooms.Delete"));


        var warehousesPermission = inferaTestGroup.AddPermission(TestCasePermissions.Warehouses.Default, L("Permission:Warehouses"));
        warehousesPermission.AddChild(TestCasePermissions.Warehouses.Create, L("Permission:Warehouses.Create"));
        warehousesPermission.AddChild(TestCasePermissions.Warehouses.Edit, L("Permission:Warehouses.Edit"));
        warehousesPermission.AddChild(TestCasePermissions.Warehouses.Delete, L("Permission:Warehouses.Delete"));
        warehousesPermission.AddChild(TestCasePermissions.Warehouses.GetListOfBuildingAsync, L("Permission:Warehouses.GetListOfBuildingAsync"));

        var warehouseInventoriesPermission = inferaTestGroup.AddPermission(TestCasePermissions.WarehouseInventories.Default, L("Permission:WarehouseInventories"));
        warehouseInventoriesPermission.AddChild(TestCasePermissions.WarehouseInventories.Create, L("Permission:WarehouseInventories.Create"));
        warehouseInventoriesPermission.AddChild(TestCasePermissions.WarehouseInventories.Edit, L("Permission:WarehouseInventories.Edit"));
        warehouseInventoriesPermission.AddChild(TestCasePermissions.WarehouseInventories.Delete, L("Permission:WarehouseInventories.Delete"));
        warehouseInventoriesPermission.AddChild(TestCasePermissions.WarehouseInventories.GetListOfBuildingAsync, L("Permission:WarehouseInventories.GetListOfBuildingAsync"));


        var productsPermission = inferaTestGroup.AddPermission(TestCasePermissions.ProductInventories.Default, L("Permission:ProductInventories"));
        productsPermission.AddChild(TestCasePermissions.ProductInventories.Create, L("Permission:ProductInventories.Create"));
        productsPermission.AddChild(TestCasePermissions.ProductInventories.Edit, L("Permission:ProductInventories.Edit"));
        productsPermission.AddChild(TestCasePermissions.ProductInventories.Delete, L("Permission:ProductInventories.Delete"));

        var IssuesPermission = inferaTestGroup.AddPermission(TestCasePermissions.Issues.Default, L("Permission:Issues"));
        IssuesPermission.AddChild(TestCasePermissions.Issues.Create, L("Permission:Issues.Create"));
        IssuesPermission.AddChild(TestCasePermissions.Issues.Edit, L("Permission:Issues.Edit"));
        IssuesPermission.AddChild(TestCasePermissions.Issues.Delete, L("Permission:Issues.Delete"));

        var AccountingPermission = inferaTestGroup.AddPermission(TestCasePermissions.Accountings.Default, L("Permission:Accountings"));
        AccountingPermission.AddChild(TestCasePermissions.Accountings.Create, L("Permission:Accountings.Create"));
        AccountingPermission.AddChild(TestCasePermissions.Accountings.Edit, L("Permission:Accountings.Edit"));
        AccountingPermission.AddChild(TestCasePermissions.Accountings.Delete, L("Permission:Accountings.Delete"));


        var saleOrdersPermission = inferaTestGroup.AddPermission(TestCasePermissions.SaleOrders.Default, L("Permission:SaleOrders"));
        saleOrdersPermission.AddChild(TestCasePermissions.SaleOrders.Create, L("Permission:SaleOrders.Create"));
        saleOrdersPermission.AddChild(TestCasePermissions.SaleOrders.Edit, L("Permission:SaleOrders.Edit"));
        saleOrdersPermission.AddChild(TestCasePermissions.SaleOrders.Delete, L("Permission:SaleOrders.Delete"));

    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<TestCaseResource>(name);
    }
}

```


Bu yetkilendirme tanımlarını Application Service katmanında ilgili metoda attribute olarak eklemek yeterlidir.

![image](https://github.com/hasanbinboga/infera-test-case/assets/27738643/43018b66-1889-4a98-ba31-d818b94309ea)





[![Watch the video](https://img.youtube.com/vi/T-D1KVIuvjA/maxresdefault.jpg)](https://youtu.be/s-WHFHbX0Gk)



