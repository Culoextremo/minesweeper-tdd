using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: AssemblyCompany("Kalendra")]
[assembly: AssemblyProduct("BoardSystem")]
[assembly: AssemblyTitle("Domain")]

[assembly: AssemblyVersion("0.0.1")]
[assembly: AssemblyCopyright("(C) 2021 RGV")]

[assembly: InternalsVisibleTo("Kalendra.BoardSystem.Tests")]
[assembly: InternalsVisibleTo("Kalendra.BoardSystem.Editor.Tests")]
[assembly: InternalsVisibleTo("Kalendra.BoardSystem.Tests.Builders")]

[assembly: InternalsVisibleTo("Kalendra.Chess.Domain")]
[assembly: InternalsVisibleTo("Kalendra.Chess.Editor.Tests")]

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")] //Where the mocks are. 