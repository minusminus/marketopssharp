using System;
using NUnit.Framework;
using Shouldly;
using System.IO;

namespace MarketOps.DataPump.Tests
{
    [TestFixture]
    public class SystemFileUnzipperTests
    {
        private readonly SystemFileUnzipper TestObj = new SystemFileUnzipper();

        private string _baseDir;
        private string _zipDir;
        private string _testZipPath;

        [SetUp]
        public void SetUp()
        {
            _baseDir = Path.GetDirectoryName(new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath);
            _zipDir = Path.Combine(_baseDir, "SystemFileUnzipperTests");
            _testZipPath = Path.Combine(_baseDir, "TestFiles", "testzip.zip");

            DirectoryUtils.ClearDir(_zipDir, true);
        }

        [TearDown]
        public void TearDown()
        {
            DirectoryUtils.ClearDir(_zipDir, false);
        }

        [Test]
        public void Unzip__FileUnzipped()
        {
            TestObj.Unzip(_testZipPath, _zipDir);
            File.Exists(Path.Combine(_zipDir, "USD.mst")).ShouldBeTrue();
            File.Exists(Path.Combine(_zipDir, "EUR.mst")).ShouldBeTrue();
        }

        [Test]
        public void Unzip_EmptyZipPath__Throws()
        {
            Should.Throw<Exception>(() => TestObj.Unzip("", _zipDir));
        }

        [Test]
        public void Unzip_EmptyDestPath__Throws()
        {
            Should.Throw<Exception>(() => TestObj.Unzip(_testZipPath, ""));
        }

        [Test]
        public void Unzip_NotExistingZipFile__Throws()
        {
            Should.Throw<Exception>(() => TestObj.Unzip(Path.Combine(_baseDir, "notexistingzipfile.zip"), _zipDir));
        }

        [Test]
        public void Unzip_NotExistingDestPath__Throws()
        {
            Should.Throw<Exception>(() => TestObj.Unzip(_testZipPath, Path.Combine(_baseDir, "notexistingpath")));
        }
    }
}
