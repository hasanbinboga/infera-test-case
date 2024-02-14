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