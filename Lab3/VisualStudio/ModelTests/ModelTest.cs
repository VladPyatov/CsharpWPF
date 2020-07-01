using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;

namespace ModelTests
{
    [TestClass]
    public class ModelTest
    {
        [TestMethod]
        public void ModelData_Test()
        {
            ModelData M = new ModelData(5, 6);

            Assert.AreEqual(5, M.N);
            Assert.AreEqual(6, M.P);
        }

        [TestMethod]
        public void F_Test()
        {
            ModelData M = new ModelData(5, 6);

            Assert.AreEqual(0, M.F(0));
        }

        [TestMethod]
        public void ModelViewX_Test()
        {
            ModelDataView M_view = new ModelDataView();
            M_view.X = -1;
            Assert.AreEqual("x must be less or equal " + 1 + " and greater or equal " + 0, M_view["X"]);

        }

        [TestMethod]
        public void ModelDataN_Test()
        {
            ModelData M = new ModelData(5, 6);
            M.N = -1;
            Assert.AreEqual("n must be less " + M.NMax + " and greater " + M.NMin, M["N"]);
        }

        [TestMethod]
        public void ModelDataN1_Test()
        {
            ModelData M = new ModelData(5, 6);
            M.N = 1;
            Assert.AreEqual("Grid can not consist of a single point", M["N"]);
        }

        [TestMethod]
        public void ModelDataP_Test()
        {
            ModelData M = new ModelData(5, 6);
            M.P = -1;
            Assert.IsFalse("p must be less " + M.PMax + " and greater " + M.PMin == M["P"]);
        }
    }
}
