using Microsoft.VisualStudio.TestTools.UnitTesting;
using BmsManager.ViewModel;
using BmsManager.Entity;
using System.IO.Abstractions.TestingHelpers;
using System.Collections.Generic;
using System;

namespace BmsManager.Tests.ViewModel
{
    [TestClass]
    public class BmsFolderViewModelTests
    {
        [TestInitialize]
        public void Setup()
        {
            var mockFileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { @"c:\test\folder\test.bms", new MockFileData("test content") }
            });
            SystemProvider.Instance = new SystemProvider(mockFileSystem);
        }

        [TestMethod]
        public void BmsFolderViewModel_Properties_ReflectEntity()
        {
            var root = new RootDirectory { Path = @"c:\test" };
            var entity = new BmsFolder
            {
                ID = 1,
                Path = @"c:\test\folder",
                Title = "Test Title",
                Artist = "Test Artist",
                Root = root
            };

            var viewModel = new BmsFolderViewModel(entity);

            Assert.AreEqual("Test Title", viewModel.Title);
            Assert.AreEqual("Test Artist", viewModel.Artist);
            Assert.AreEqual(@"c:\test\folder", viewModel.FullPath);
            Assert.AreEqual(1, viewModel.ID);
            Assert.IsNotNull(viewModel.Files);
            Assert.IsNotNull(viewModel.OpenFolder);
        }

        [TestMethod]
        public void BmsFolderViewModel_Files_InitializedFromEntity()
        {
            var root = new RootDirectory { Path = @"c:\test" };
            var folder = new BmsFolder
            {
                Path = @"c:\test\folder",
                Root = root
            };
            
            var file = new BmsFile
            {
                Path = @"c:\test\folder\test.bms",
                Title = "Test Song",
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
                LaneNotes = "",
                Folder = folder
            };
            
            folder.Files.Add(file);

            var viewModel = new BmsFolderViewModel(folder);

            Assert.AreEqual(1, viewModel.Files.Count);
            Assert.AreEqual("Test Song", viewModel.Files[0].Title);
        }

        [TestMethod]
        public void BmsFolderViewModel_SetMetaFromName_ParsesCorrectly()
        {
            var root = new RootDirectory { Path = @"c:\test" };
            var entity = new BmsFolder
            {
                Path = @"c:\test\[Artist Name]Song Title",
                Root = root
            };

            var viewModel = new BmsFolderViewModel(entity);
            viewModel.SetMetaFromName();

            Assert.AreEqual("Artist Name", viewModel.Artist);
            Assert.AreEqual("Song Title", viewModel.Title);
        }

        [TestMethod]
        public void BmsFolderViewModel_SetMetaFromName_NoDelimiter_NoChange()
        {
            var root = new RootDirectory { Path = @"c:\test" };
            var entity = new BmsFolder
            {
                Path = @"c:\test\FolderWithoutDelimiter",
                Artist = "Original Artist",
                Title = "Original Title",
                Root = root
            };

            var viewModel = new BmsFolderViewModel(entity);
            viewModel.SetMetaFromName();

            Assert.AreEqual("Original Artist", viewModel.Artist);
            Assert.AreEqual("Original Title", viewModel.Title);
        }
    }
}
