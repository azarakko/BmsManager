using Microsoft.VisualStudio.TestTools.UnitTesting;
using BmsManager;
using BmsManager.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BmsManager.Tests
{
    [TestClass]
    public class UtilityExtendedTests
    {
        [TestMethod]
        public void GetMetaFromFiles_SingleFile_ReturnsCorrectMeta()
        {
            var files = new List<BmsFile>
            {
                new BmsFile
                {
                    Path = "test.bms",
                    Title = "Test Song",
                    Artist = "Test Artist",
                    Subtitle = "",
                    Genre = "",
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
                }
            };

            var (title, artist) = files.GetMetaFromFiles();

            Assert.AreEqual("Test Song", title);
            Assert.AreEqual("Test Artist", artist);
        }

        [TestMethod]
        public void GetMetaFromFiles_MultipleFilesWithCommonPrefix_ReturnsCommonPart()
        {
            var files = new List<BmsFile>
            {
                new BmsFile
                {
                    Path = "test1.bms",
                    Title = "Common Title [NORMAL]",
                    Artist = "Common Artist",
                    Subtitle = "",
                    Genre = "",
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
                },
                new BmsFile
                {
                    Path = "test2.bms",
                    Title = "Common Title [HYPER]",
                    Artist = "Common Artist",
                    Subtitle = "",
                    Genre = "",
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
                }
            };

            var (title, artist) = files.GetMetaFromFiles();

            Assert.AreEqual("Common Title ", title);
            Assert.AreEqual("Common Artist", artist);
        }

        [TestMethod]
        public void ToUnixMilliseconds_ConvertsCorrectly()
        {
            var dateTime = new DateTime(1970, 1, 1, 0, 0, 1, DateTimeKind.Utc);
            var result = dateTime.ToUnixMilliseconds();
            
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void ToUnixMilliseconds_EpochTime_ReturnsZero()
        {
            var epochTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var result = epochTime.ToUnixMilliseconds();
            
            Assert.AreEqual(0, result);
        }

        [DataTestMethod]
        [DataRow(null, "")]
        [DataRow("", "")]
        [DataRow("Simple Title", "Simple Title")]
        [DataRow("Title with (bracket)", "Title with ")]
        [DataRow("Title with [square]", "Title with ")]
        [DataRow("Title with <angle>", "Title with ")]
        [DataRow("Title with  double space", "Title with")]
        [DataRow("Title ending-", "Title ending")]
        [DataRow("Title ending～", "Title ending")]
        [DataRow("Title ending\"", "Title ending")]
        public void GetTitle_VariousInputs_ReturnsExpectedResults(string input, string expected)
        {
            var result = Utility.GetTitle(input);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetArtist_EmptyCollection_ReturnsEmpty()
        {
            var artists = new List<string>();
            var result = Utility.GetArtist(artists);
            
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void GetArtist_NullInput_ReturnsEmpty()
        {
            var result = Utility.GetArtist(null);
            
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void GetArtist_WithWhitespace_IgnoresWhitespace()
        {
            var artists = new List<string> { "  ", "Artist Name", "" };
            var result = Utility.GetArtist(artists);
            
            Assert.AreEqual("Artist Name", result);
        }

        [DataTestMethod]
        [DataRow(null, "")]
        [DataRow("", "")]
        [DataRow("Artist Name", "Artist Name")]
        [DataRow("Artist obj:something", "Artist")]
        [DataRow("Artist note:something", "Artist")]
        [DataRow("Artist 差分", "Artist")]
        [DataRow("Artist / obj:something", "Artist")]
        [DataRow("Artist (obj:something", "Artist")]
        public void RemoveObjer_VariousInputs_ReturnsExpectedResults(string input, string expected)
        {
            var result = Utility.RemoveObjer(input);
            Assert.AreEqual(expected, result);
        }

        [DataTestMethod]
        [DataRow("Normal Name", "Normal Name")]
        [DataRow("Name\\with\\backslash", "Name￥with￥backslash")]
        [DataRow("Name<with>brackets", "Name＜with＞brackets")]
        [DataRow("Name/with/slash", "Name／with／slash")]
        [DataRow("Name*with*asterisk", "Name＊with＊asterisk")]
        [DataRow("Name:with:colon", "Name：with：colon")]
        [DataRow("Name\"with\"quote", "Name"with"quote")]
        [DataRow("Name?with?question", "Name？with？question")]
        [DataRow("Name|with|pipe", "Name｜with｜pipe")]
        public void ToFileNameString_ReplacesInvalidCharacters(string input, string expected)
        {
            var result = Utility.ToFileNameString(input);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ToFileNameString_LongName_TruncatesTo50Characters()
        {
            var longName = new string('a', 100);
            var result = Utility.ToFileNameString(longName);
            
            Assert.AreEqual(50, result.Length);
            Assert.AreEqual(new string('a', 50), result);
        }
    }
}
