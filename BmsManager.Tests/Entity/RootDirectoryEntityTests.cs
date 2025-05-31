using Microsoft.VisualStudio.TestTools.UnitTesting;
using BmsManager.Entity;
using System;

namespace BmsManager.Tests.Entity
{
    [TestClass]
    public class RootDirectoryEntityTests
    {
        [TestMethod]
        public void RootDirectory_Properties_SetAndGet()
        {
            var parent = new RootDirectory { Path = @"c:\parent" };
            var root = new RootDirectory 
            { 
                Path = @"c:\test",
                ParentRootID = 1,
                FolderUpdateDate = DateTime.Now,
                Parent = parent
            };

            Assert.AreEqual(@"c:\test", root.Path);
            Assert.AreEqual(1, root.ParentRootID);
            Assert.AreEqual(parent, root.Parent);
            Assert.IsNotNull(root.Folders);
            Assert.IsNotNull(root.Children);
        }

        [TestMethod]
        public void RootDirectory_RequiredPath_NotNull()
        {
            var root = new RootDirectory { Path = @"c:\test" };
            
            Assert.IsNotNull(root.Path);
            Assert.AreEqual(@"c:\test", root.Path);
        }

        [TestMethod]
        public void RootDirectory_Collections_InitializedEmpty()
        {
            var root = new RootDirectory { Path = @"c:\test" };
            
            Assert.IsNotNull(root.Folders);
            Assert.IsNotNull(root.Children);
            Assert.AreEqual(0, root.Folders.Count);
            Assert.AreEqual(0, root.Children.Count);
        }

        [TestMethod]
        public void RootDirectory_ParentChild_Relationship()
        {
            var parent = new RootDirectory { Path = @"c:\parent" };
            var child = new RootDirectory { Path = @"c:\parent\child", Parent = parent };
            
            parent.Children.Add(child);
            
            Assert.AreEqual(parent, child.Parent);
            Assert.AreEqual(1, parent.Children.Count);
            Assert.IsTrue(parent.Children.Contains(child));
        }
    }
}
