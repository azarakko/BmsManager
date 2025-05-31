using Microsoft.VisualStudio.TestTools.UnitTesting;
using BmsManager.Entity;
using System;

namespace BmsManager.Tests.Entity
{
    [TestClass]
    public class BmsFileEntityTests
    {
        [TestMethod]
        public void BmsFile_Properties_SetAndGet()
        {
            var root = new RootDirectory { Path = @"c:\test" };
            var folder = new BmsFolder { Path = @"c:\test\folder", Root = root };
            var file = new BmsFile
            {
                Path = @"c:\test\folder\test.bms",
                Title = "Test Song",
                Subtitle = "Test Subtitle",
                Genre = "Test Genre",
                Artist = "Test Artist",
                SubArtist = "Test SubArtist",
                MD5 = "abc123",
                Sha256 = "def456",
                Banner = "banner.jpg",
                StageFile = "stage.jpg",
                BackBmp = "back.bmp",
                Preview = "preview.wav",
                PlayLevel = "12",
                Mode = 7,
                Difficulty = 3,
                Judge = 2,
                MinBpm = 120.0,
                MaxBpm = 180.0,
                Length = 120000,
                Notes = 1500,
                Feature = 0,
                HasBga = true,
                IsNoKeySound = false,
                ChartHash = "chart123",
                N = 1000,
                LN = 500,
                S = 100,
                LS = 50,
                Total = 300.0,
                Density = 12.5,
                PeakDensity = 25.0,
                EndDensity = 8.0,
                Distribution = "#distribution",
                MainBpm = 150.0,
                SpeedChange = "150,0,180,60000",
                LaneNotes = "100,50,10",
                Folder = folder
            };

            Assert.AreEqual(@"c:\test\folder\test.bms", file.Path);
            Assert.AreEqual("Test Song", file.Title);
            Assert.AreEqual("Test Subtitle", file.Subtitle);
            Assert.AreEqual("Test Genre", file.Genre);
            Assert.AreEqual("Test Artist", file.Artist);
            Assert.AreEqual("Test SubArtist", file.SubArtist);
            Assert.AreEqual("abc123", file.MD5);
            Assert.AreEqual("def456", file.Sha256);
            Assert.AreEqual("banner.jpg", file.Banner);
            Assert.AreEqual("stage.jpg", file.StageFile);
            Assert.AreEqual("back.bmp", file.BackBmp);
            Assert.AreEqual("preview.wav", file.Preview);
            Assert.AreEqual("12", file.PlayLevel);
            Assert.AreEqual(7, file.Mode);
            Assert.AreEqual(3, file.Difficulty);
            Assert.AreEqual(2, file.Judge);
            Assert.AreEqual(120.0, file.MinBpm);
            Assert.AreEqual(180.0, file.MaxBpm);
            Assert.AreEqual(120000, file.Length);
            Assert.AreEqual(1500, file.Notes);
            Assert.AreEqual(0, file.Feature);
            Assert.IsTrue(file.HasBga);
            Assert.IsFalse(file.IsNoKeySound);
            Assert.AreEqual("chart123", file.ChartHash);
            Assert.AreEqual(1000, file.N);
            Assert.AreEqual(500, file.LN);
            Assert.AreEqual(100, file.S);
            Assert.AreEqual(50, file.LS);
            Assert.AreEqual(300.0, file.Total);
            Assert.AreEqual(12.5, file.Density);
            Assert.AreEqual(25.0, file.PeakDensity);
            Assert.AreEqual(8.0, file.EndDensity);
            Assert.AreEqual("#distribution", file.Distribution);
            Assert.AreEqual(150.0, file.MainBpm);
            Assert.AreEqual("150,0,180,60000", file.SpeedChange);
            Assert.AreEqual("100,50,10", file.LaneNotes);
            Assert.AreEqual(folder, file.Folder);
        }

        [TestMethod]
        public void BmsFile_Folder_ThrowsWhenNull()
        {
            var file = new BmsFile
            {
                Path = @"c:\test.bms",
                Title = "Test",
                Subtitle = "",
                Genre = "",
                Artist = "",
                SubArtist = "",
                MD5 = "",
                Sha256 = "",
                Banner = "",
                StageFile = "",
                BackBmp = "",
                Preview = "",
                PlayLevel = "",
                ChartHash = "",
                Distribution = "",
                SpeedChange = "",
                LaneNotes = ""
            };
            
            Assert.ThrowsException<InvalidOperationException>(() => file.Folder);
        }

        [TestMethod]
        public void BmsFile_RequiredProperties_NotNull()
        {
            var file = new BmsFile
            {
                Path = @"c:\test.bms",
                Title = "Test Title",
                Subtitle = "Test Subtitle",
                Genre = "Test Genre",
                Artist = "Test Artist",
                SubArtist = "Test SubArtist",
                MD5 = "abc123",
                Sha256 = "def456",
                Banner = "banner.jpg",
                StageFile = "stage.jpg",
                BackBmp = "back.bmp",
                Preview = "preview.wav",
                PlayLevel = "12",
                ChartHash = "chart123",
                Distribution = "#distribution",
                SpeedChange = "150,0",
                LaneNotes = "100,50,10"
            };

            Assert.IsNotNull(file.Path);
            Assert.IsNotNull(file.Title);
            Assert.IsNotNull(file.Subtitle);
            Assert.IsNotNull(file.Genre);
            Assert.IsNotNull(file.Artist);
            Assert.IsNotNull(file.SubArtist);
            Assert.IsNotNull(file.MD5);
            Assert.IsNotNull(file.Sha256);
            Assert.IsNotNull(file.Banner);
            Assert.IsNotNull(file.StageFile);
            Assert.IsNotNull(file.BackBmp);
            Assert.IsNotNull(file.Preview);
            Assert.IsNotNull(file.PlayLevel);
            Assert.IsNotNull(file.ChartHash);
            Assert.IsNotNull(file.Distribution);
            Assert.IsNotNull(file.SpeedChange);
            Assert.IsNotNull(file.LaneNotes);
        }
    }
}
