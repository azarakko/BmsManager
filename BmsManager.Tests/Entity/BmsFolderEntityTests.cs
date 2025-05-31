using Microsoft.VisualStudio.TestTools.UnitTesting;
using BmsManager.Entity;
using System;

namespace BmsManager.Tests.Entity
{
    [TestClass]
    public class BmsFolderEntityTests
    {
        [TestMethod]
        public void BmsFolder_Properties_SetAndGet()
        {
            var root = new RootDirectory { Path = @"c:\test" };
            var folder = new BmsFolder 
            { 
                Path = @"c:\test\folder",
                Artist = "Test Artist",
                Title = "Test Title",
                HasText = true,
                Preview = "preview.wav",
                FolderUpdateDate = DateTime.Now,
                Root = root
            };

            Assert.AreEqual(@"c:\test\folder", folder.Path);
            Assert.AreEqual("Test Artist", folder.Artist);
            Assert.AreEqual("Test Title", folder.Title);
            Assert.IsTrue(folder.HasText);
            Assert.AreEqual("preview.wav", folder.Preview);
            Assert.AreEqual(root, folder.Root);
            Assert.IsNotNull(folder.Files);
        }

        [TestMethod]
        public void BmsFolder_Root_ThrowsWhenNull()
        {
            var folder = new BmsFolder { Path = @"c:\test" };
            
            Assert.ThrowsException<InvalidOperationException>(() => folder.Root);
        }

        [TestMethod]
        public void BmsFolder_RequiredPath_NotNull()
        {
            var folder = new BmsFolder { Path = @"c:\test" };
            
            Assert.IsNotNull(folder.Path);
            Assert.AreEqual(@"c:\test", folder.Path);
        }
    }
}
