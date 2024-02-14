# infera-test-case
 
Uygulamayı geliştirmek için Volosoft tarafından geliştirilen kütüphaneleri kullanmaya karar verdim. 

Volosoft Abp Framework Açık kaynaklı(open source) bir web uygulama framework'üdür. Amacı sürdürülebilir kod yazılmasını sağlamaktır. Aynı zamanda developerların kod tekrarını engeller ve high level kod yazılmasını sağlar.

abp cli kurmak istediğimde aşağıdaki hatayı aldım.

`
C:\Users\PC>dotnet tool install -g Volo.Abp.Cli
Unhandled exception: Microsoft.DotNet.Cli.NuGetPackageDownloader.NuGetPackageNotFoundException: volo.abp.cli::[*, ), C:\Program Files (x86)\Microsoft SDKs\NuGetPackages\ NuGet akışlarında bulunamadı.
   at Microsoft.DotNet.Cli.NuGetPackageDownloader.NuGetPackageDownloader.GetMatchingVersionInternalAsync(String packageIdentifier, IEnumerable`1 packageSources, VersionRange versionRange, CancellationToken cancellationToken)
   at Microsoft.DotNet.Cli.NuGetPackageDownloader.NuGetPackageDownloader.GetBestPackageVersionAsync(PackageId packageId, VersionRange versionRange, PackageSourceLocation packageSourceLocation)
   at Microsoft.DotNet.Cli.ToolPackage.ToolPackageDownloader.<>c__DisplayClass8_0.<InstallPackage>b__0()
   at Microsoft.DotNet.Cli.TransactionalAction.Run[T](Func`1 action, Action commit, Action rollback)
   at Microsoft.DotNet.Tools.Tool.Install.ToolInstallGlobalOrToolPathCommand.Execute()
   at System.CommandLine.Invocation.InvocationPipeline.Invoke(ParseResult parseResult)
   at Microsoft.DotNet.Cli.Program.ProcessArgs(String[] args, TimeSpan startupTime, ITelemetry telemetryClient)
`

Sorunun .net 8 versiyonunun eski olmasından kaynaklandığını konu ile ilgili olarak issue oluşturulduğunu gördüm. 
https://github.com/dotnet/sdk/issues/35566

İlk olarak Windows update ve VS updatelerini tamamladım. Projeyi .Net 8 ile geliştirmeyi planlıyorum.
Sorunun Nuget package soruce'unun offline olarak ayarlanmasından kaynaklandığını belirledim.
![image](https://github.com/hasanbinboga/infera-test-case/assets/27738643/c99780df-1e4f-4695-afb8-826e180f0f33)

nuget source url: https://api.nuget.org/v3/index.json olarak güncellendi. 

`
C:\Repos\infera-test-case\Infera>dotnet tool install --global Volo.Abp.Cli --version 8.1.0-rc.1
NuGet paket imzası doğrulaması atlanıyor.
Şu komutu kullanarak aracı çağırabilirsiniz: abp
'volo.abp.cli' aracı (sürüm '8.1.0-rc.1') başarıyla yüklendi.
`
