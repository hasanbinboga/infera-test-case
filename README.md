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





