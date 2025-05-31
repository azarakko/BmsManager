using Microsoft.VisualStudio.TestTools.UnitTesting;
using BmsManager.ViewModel;
using BmsManager.Entity;
using System.IO.Abstractions.TestingHelpers;
using System.Collections.Generic;
using System.Linq;

namespace BmsManager.Tests.ViewModel
{
    [TestClass]
    public class RootDirectoryViewModelTests
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
        public void RootDirectoryViewModel_Properties_ReflectEntity()
        {
            var entity = new RootDirectory
            {
                ID = 1,
                Path = @"c:\test",
                ParentRootID = null
            };

            var tree = new RootTreeViewModel();
            var viewModel = new RootDirectoryViewModel(tree, entity);

            Assert.AreEqual(@"c:\test", viewModel.Text);
            Assert.AreEqual(@"c:\test", viewModel.FullPath);
            Assert.AreEqual(1, viewModel.ID);
            Assert.IsNotNull(viewModel.Folders);
            Assert.IsNotNull(viewModel.Children);
            Assert.IsNotNull(viewModel.LoadFromFileSystem);
            Assert.IsNotNull(viewModel.LoadFromDB);
            Assert.IsNotNull(viewModel.Remove);
        }

        [TestMethod]
        public void RootDirectoryViewModel_Text_ShowsFileNameForChild()
        {
            var parent = new RootDirectory { Path = @"c:\parent" };
            var entity = new RootDirectory
            {
                Path = @"c:\parent\child",
                ParentRootID = 1,
                Parent = parent
            };

            var tree = new RootTreeViewModel();
            var viewModel = new RootDirectoryViewModel(tree, entity);

            Assert.AreEqual("child", viewModel.Text);
        }

        [TestMethod]
        public void RootDirectoryViewModel_Text_ShowsFullPathForRoot()
        {
            var entity = new RootDirectory
            {
                Path = @"c:\test",
                ParentRootID = null
            };

            var tree = new RootTreeViewModel();
            var viewModel = new RootDirectoryViewModel(tree, entity);

            Assert.AreEqual(@"c:\test", viewModel.Text);
        }

        [TestMethod]
        public void RootDirectoryViewModel_Descendants_ReturnsAllDescendants()
        {
            var root = new RootDirectory { Path = @"c:\root" };
            var child1 = new RootDirectory { Path = @"c:\root\child1", Parent = root };
            var child2 = new RootDirectory { Path = @"c:\root\child2", Parent = root };
            var grandchild = new RootDirectory { Path = @"c:\root\child1\grandchild", Parent = child1 };

            root.Children.Add(child1);
            root.Children.Add(child2);
            child1.Children.Add(grandchild);

            var tree = new RootTreeViewModel();
            var rootViewModel = new RootDirectoryViewModel(tree, root);
            var child1ViewModel = new RootDirectoryViewModel(tree, child1);
            var child2ViewModel = new RootDirectoryViewModel(tree, child2);
            var grandchildViewModel = new RootDirectoryViewModel(tree, grandchild);

            rootViewModel.Children.Add(child1ViewModel);
            rootViewModel.Children.Add(child2ViewModel);
            child1ViewModel.Children.Add(grandchildViewModel);

            var descendants = rootViewModel.Descendants().ToArray();

            Assert.AreEqual(4, descendants.Length);
            Assert.IsTrue(descendants.Contains(rootViewModel));
            Assert.IsTrue(descendants.Any(d => d.FullPath == @"c:\root\child1"));
            Assert.IsTrue(descendants.Any(d => d.FullPath == @"c:\root\child2"));
            Assert.IsTrue(descendants.Any(d => d.FullPath == @"c:\root\child1\grandchild"));
        }

        [TestMethod]
        public void RootDirectoryViewModel_DescendantFolders_ReturnsAllFolders()
        {
            var root = new RootDirectory { Path = @"c:\root" };
            var folder1 = new BmsFolder { Path = @"c:\root\folder1", Root = root };
            var folder2 = new BmsFolder { Path = @"c:\root\folder2", Root = root };
            
            root.Folders.Add(folder1);
            root.Folders.Add(folder2);

            var tree = new RootTreeViewModel();
            var rootViewModel = new RootDirectoryViewModel(tree, root);
            rootViewModel.Folders.Add(folder1);
            rootViewModel.Folders.Add(folder2);

            var descendantFolders = rootViewModel.DescendantFolders;

            Assert.AreEqual(2, descendantFolders.Length);
            Assert.IsTrue(descendantFolders.Contains(folder1));
            Assert.IsTrue(descendantFolders.Contains(folder2));
        }

        [TestMethod]
        public void RootDirectoryViewModel_IsLoading_DefaultsFalse()
        {
            var entity = new RootDirectory { Path = @"c:\test" };
            var tree = new RootTreeViewModel();
            var viewModel = new RootDirectoryViewModel(tree, entity);

            Assert.IsFalse(viewModel.IsLoading);
        }

        [TestMethod]
        public void RootDirectoryViewModel_IsError_DefaultsFalse()
        {
            var entity = new RootDirectory { Path = @"c:\test" };
            var tree = new RootTreeViewModel();
            var viewModel = new RootDirectoryViewModel(tree, entity);

            Assert.IsFalse(viewModel.IsError);
        }
    }
}
