using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewModel;

namespace ViewModelTests
{
    [TestClass]
    public class ViewModelTest
    {
        [TestMethod]
        public void NewTest()
        {
            ViewM viewM = new ViewM(new MyTestAppUIServises());
            viewM.NewCommand.Execute(null);
            Assert.IsTrue(viewM.MDO.Count == 0);
        }

        [TestMethod]
        public void SaveTest()
        {
            MyTestAppUIServises MyTestUI = new MyTestAppUIServises();
            ViewM viewM = new ViewM(MyTestUI);
            viewM.SaveCommand.Execute(null);
            Assert.IsTrue(File.Exists(MyTestUI.SavePath()));
            File.Delete(MyTestUI.SavePath());
        }

        [TestMethod]
        public void OpenTest()
        {
            MyTestAppUIServises MyTestUI = new MyTestAppUIServises();
            ViewM viewM = new ViewM(MyTestUI);
            viewM.SaveCommand.Execute(null);

            viewM.OpenCommand.Execute(null);

            File.Delete(MyTestUI.OpenPath());
        }
        [TestMethod]
        public void AddTest()
        {
            MyTestAppUIServises MyTestUI = new MyTestAppUIServises();
            ViewM viewM = new ViewM(MyTestUI);
            int count = ViewM.ModelDataOb.Count;
            viewM.AddCommand.Execute(null);
            Assert.IsTrue(ViewM.ModelDataOb.Count==count+1);
        }

        [TestMethod]
        public void CanDeleteTest()
        {
            MyTestAppUIServises MyTestUI = new MyTestAppUIServises(new List<object> { ViewM.ModelDataOb[0] });
            ViewM viewM = new ViewM(MyTestUI);
            //int count = ViewM.ModelDataOb.Count;
            Assert.IsTrue(viewM.DeleteCommand.CanExecute(null));
        }

        [TestMethod]
        public void DeleteTest()
        {
            List<object> list = new List<object> { ViewM.ModelDataOb[0]};
            MyTestAppUIServises MyTestUI = new MyTestAppUIServises(list);
            ViewM viewM = new ViewM(MyTestUI);
            viewM.DeleteCommand.Execute(null);

            bool flag = false;
            foreach (var item in ViewM.ModelDataOb)
                flag = item == list[0];

            Assert.IsFalse(flag);
        }
    }

    class MyTestAppUIServises : IUIServises
    {
        private List<object> items;

        public MyTestAppUIServises() { }

        public MyTestAppUIServises(List<object> list)
        {
            items = list;
        }

        public bool CanAdd()
        {
            return true;
        }

        public bool CanDraw()
        {
            return false;
        }

        public string OpenPath()
        {
            return "C:\\Users\\Владислав\\Documents\\save_test";
        }

        public string SavePath()
        {
            return "C:\\Users\\Владислав\\Documents\\save_test";
        }

        public List<object> SelectedItems()
        {
            return items;
        }

        public bool WantToSave()
        {
            return false;
        }
    }
}
