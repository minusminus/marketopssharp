﻿using MarketOps;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// Ogólne informacje o zestawie są kontrolowane poprzez następujący 
// zestaw atrybutów. Zmień wartości tych atrybutów, aby zmodyfikować informacje
// powiązane z zestawem.
[assembly: AssemblyTitle("MarketOps.SystemDefs")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct(VersionInfo.AssemblyProduct)]
[assembly: AssemblyCopyright(VersionInfo.AssemblyCopyright)]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Ustawienie elementu ComVisible na wartość false sprawia, że typy w tym zestawie są niewidoczne
// dla składników COM. Jeśli potrzebny jest dostęp do typu w tym zestawie z
// COM, ustaw wartość true dla atrybutu ComVisible tego typu.
[assembly: ComVisible(false)]

// Następujący identyfikator GUID jest identyfikatorem biblioteki typów w przypadku udostępnienia tego projektu w modelu COM
[assembly: Guid("e9909ebc-1d6d-47cf-9258-e3fc3d7484f3")]

// Informacje o wersji zestawu zawierają następujące cztery wartości:
//
//      Wersja główna
//      Wersja pomocnicza
//      Numer kompilacji
//      Rewizja
//
// Możesz określić wszystkie wartości lub użyć domyślnych numerów kompilacji i poprawki
// przy użyciu symbolu „*”, tak jak pokazano poniżej:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion(VersionInfo.AssemblyVersion)]
[assembly: AssemblyFileVersion(VersionInfo.AssemblyFileVersion)]
[assembly: AssemblyInformationalVersion(VersionInfo.AssemblyFileVersion)]

[assembly: InternalsVisibleTo("MarketOps.SystemDefs.Tests")]
