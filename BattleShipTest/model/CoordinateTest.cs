using BattleShip.model;
using BattleShip.model.aircraft;
using BattleShip.model.ship;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipTest.model
{
    [TestClass]
    public class CoordinateTest
    {
        Coordinate cd2, cd3;

        [TestInitialize]
        public void SetUp()
        {
            cd2 = new Coordinate2D(-10, 7);
            cd3 = new Coordinate3D(15, 8, -2);
        }

        /* checking Set and Get with coordinates 2D and 3D from SetUp() */
        [TestMethod]
        public void Coordinate_TestSetGet()
        {
            Assert.AreEqual(-10, cd2.Get(0));
            Assert.AreEqual(7, cd2.Get(1));
            cd2.Set(0, -15);
            cd2.Set(1, 19);
            Assert.AreEqual(-15, cd2.Get(0));
            Assert.AreEqual(19, cd2.Get(1));

            Assert.AreEqual(15, cd3.Get(0));
            Assert.AreEqual(8, cd3.Get(1));
            Assert.AreEqual(-2, cd3.Get(2));
            cd3.Set(0, -15);
            cd3.Set(1, 19);
            cd3.Set(2, 18);
            Assert.AreEqual(-15, cd3.Get(0));
            Assert.AreEqual(19, cd3.Get(1));
            Assert.AreEqual(18, cd3.Get(2));
        }

        /* checking Set with a component out of bounds in a Coordinate 2D
         * in this case a ArgumentExcepction must be thrown
         */
        [TestMethod]
        public void Coordinate_TestSetArgumentException2D()
        {
            try
            {
                cd2.Set(-1, 12);
                Assert.Fail("Error: ArgumentException has not been thrown");
            }
            catch (ArgumentException)
            {
                Assert.AreEqual(cd2, cd2.Copy());
                cd2.Set(1, 5);
            }
        }

        /* checking Set with a component out of bounds in a Coordinate 3D
         * in this case a ArgumentExcepction must be thrown
         */
        [TestMethod]
        public void Coordinate_TestSetArgumentException3D()
        {
            try
            {
                cd3.Set(-1, 12);
                Assert.Fail("Error: ArgumentException has not been thrown");
            }
            catch (ArgumentException)
            {
                Assert.AreEqual(cd3, cd3.Copy());
                cd3.Set(2, 5);
            }
        }

        /* checking Get with a component out of bounds in a Coordinate 2D
         * in this case a ArgumentExcepction must be thrown
         */
        [TestMethod]
        public void Coordinate_TestGetArgumentException2D()
        {
            try
            {
                cd2.Get(-1);
                Assert.Fail("Error: ArgumentException has not been thrown");
            }
            catch (ArgumentException)
            {
                try
                {
                    cd2.Get(2);
                    Assert.Fail("Error: ArgumentException has not been thrown");
                }
                catch (ArgumentException)
                {
                    try
                    {
                        cd3.Get(3);
                        Assert.Fail("Error: ArgumentException has not been thrown");
                    }
                    catch (ArgumentException)
                    {
                        cd3.Get(1);
                    }
                }
            }
        }

        /* adding a Coordinate2D and a Coordinate3D.
         * the resulting coordinate dimension depends who invoke Add()
         */
        [TestMethod]
        public void Coordinate_TestAdd2Dand3D()
        {
            Coordinate aux2d = new Coordinate2D(5, 15);
            Coordinate aux3d = new Coordinate3D(5, 15, -2);

            Assert.AreEqual(aux2d, cd2.Add(cd3), "c2+c3");
            Assert.AreEqual(aux3d, cd3.Add(cd2), "c3+c2");
            Assert.AreNotEqual(aux2d, cd2, "aux2d!=cd2");
            Assert.AreNotEqual(aux3d, cd3, "aux3d!=cd3");
        }

        /* passing null as a parameter to Add(-), it s checking that
         * ArgumentNullException is thrown in Coordinates 2D and 3D
         */
        [TestMethod]
        public void Coordinate_TestAddNullParameterException()
        {
            try
            {
                cd2.Add(null);
                Assert.Fail("Error: ArgumentException has not been thrown");
            }
            catch (ArgumentNullException)
            {
                try
                {
                    cd3.Add(null);
                }
                catch (ArgumentNullException)
                {
                    cd3.Add(cd2);
                }
            }
        }

        /* passing null as a parameter to Substract(-), it s checking that
         * ArgumentNullException is thrown in Coordinates 2D and 3D
         */
        [TestMethod]
        public void Coordinate_TestSubstractNullParameterException()
        {
            try
            {
                cd2.Substract(null);
                Assert.Fail("Error: ArgumentException has not been thrown");
            }
            catch (ArgumentNullException)
            {
                try
                {
                    cd3.Substract(null);
                }
                catch (ArgumentNullException)
                {
                    cd3.Substract(cd2);
                }
            }
        }

        /* substracting a Coordinate2D and a Coordinate3D.
         * the resulting coordinate dimension depends who invoke Substract()
         */
        [TestMethod]
        public void Coordinate_TestSubstract2Dand3D()
        {
            Coordinate aux2d = new Coordinate2D(-25, -1);
            Coordinate aux3d = new Coordinate3D(25, 1, -2);

            Assert.AreEqual(aux2d, cd2.Substract(cd3), "c2-c3");
            Assert.AreEqual(aux3d, cd3.Substract(cd2), "c3-c2");
            Assert.AreNotEqual(aux2d, cd2, "aux2d!=cd2");
            Assert.AreNotEqual(aux3d, cd3, "aux3d!=cd3");
        }
    }
}
