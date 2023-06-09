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
    public class CoordinateFactoryTest
    {
        /* Correct creation of Coordinate */
        [TestMethod]
        public void CoordinateFactory_TestCreateCoordinateOk()
        {
            Coordinate aux = new Coordinate2D(-1, 23);
            Coordinate c = CoordinateFactory.CreateCoordinate(-1, 23);
            Assert.AreEqual(aux, c);

            aux = new Coordinate3D(15, -7, 18);
            c = CoordinateFactory.CreateCoordinate(15, -7, 18);
            Assert.AreEqual(aux, c);
        }

        /* an incorrect number of integers are passed to CreateCoordinate()
         * ArgumentException is thrown
         */
        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Inappropriate number of arguments.")]
        public void CoordinateFactory_TestCreateCoordinateException()
        {
            try
            {
                CoordinateFactory.CreateCoordinate(-1);
                Assert.Fail("Error: ArgumentException has not been thrown");
            }
            catch (ArgumentException)
            {
                try
                {
                    CoordinateFactory.CreateCoordinate(-1, 3, 4, 0);
                    Assert.Fail("Error: ArgumentException has not been thrown");
                }
                catch (ArgumentException)
                {
                    CoordinateFactory.CreateCoordinate();
                }
            }
        }
    }
}
