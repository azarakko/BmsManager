using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BmsManager.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BmsManager.Tests.Data
{
    [TestClass]
    public class RootDirectoryTests
    {
        MockFileSystem mock;
        [TestInitialize]
        public void Initialize()
        {
            mock = new MockFileSystem();
            SystemProvider.Instance = new SystemProvider(mock);
        }

        [TestMethod]
        public void LoadFromFileSystem()
        {
            mock.AddDirectory(@"D:\parent");
            var root1Path = @"D:\parent\root1";
            mock.AddDirectory(root1Path);
            var folder1Path = @"D:\parent\root1\folder1";
            mock.AddDirectory(folder1Path);
            var folder2Path = @"D:\parent\root1\folder2";
            mock.AddDirectory(folder2Path);

            var root = new RootDirectory { Path = @"D:\parent" };

            Assert.AreEqual(@"D:\parent", root.Path);
            Assert.IsNotNull(root.Folders);
            Assert.IsNotNull(root.Children);
            Assert.IsTrue(mock.Directory.Exists(@"D:\parent"));
            Assert.IsTrue(mock.Directory.Exists(root1Path));
            Assert.IsTrue(mock.Directory.Exists(folder1Path));
            Assert.IsTrue(mock.Directory.Exists(folder2Path));
        }

        [TestMethod]
        public void Descendants()
        {
            var root = new RootDirectory { Path = @"root" };
            var child1 = new RootDirectory { Path = @"child1", Parent = root };
            var child2 = new RootDirectory { Path = @"child2", Parent = root };
            var son1 = new RootDirectory { Path = @"son1", Parent = child1 };
            var son2 = new RootDirectory { Path = @"son2", Parent = child1 };
            var son3 = new RootDirectory { Path = @"son3", Parent = child2 };
            
            root.Children.Add(child1);
            root.Children.Add(child2);
            child1.Children.Add(son1);
            child1.Children.Add(son2);
            child2.Children.Add(son3);

            Assert.AreEqual(@"root", root.Path);
            Assert.AreEqual(2, root.Children.Count);
            Assert.AreEqual(2, child1.Children.Count);
            Assert.AreEqual(1, child2.Children.Count);
            Assert.AreEqual(0, son1.Children.Count);
            Assert.AreEqual(0, son2.Children.Count);
            Assert.AreEqual(0, son3.Children.Count);
            Assert.AreEqual(root, child1.Parent);
            Assert.AreEqual(root, child2.Parent);
            Assert.AreEqual(child1, son1.Parent);
            Assert.AreEqual(child1, son2.Parent);
            Assert.AreEqual(child2, son3.Parent);
        }
    }
}
