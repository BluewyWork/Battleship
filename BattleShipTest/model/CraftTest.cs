using BattleShip.model;
using BattleShip.model.aircraft;
using BattleShip.model.ship;
using BattleShip.utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipTest.model
{
    [TestClass]
    public class CraftTest
    {
        readonly string rn = "\r\n";
        Craft carrier, bomber;
        readonly int HIT_VALUE = -1;
        readonly int CRAFT_VALUE = 1;
        string scarrier, sbomber;

        [TestInitialize]
        public void SetUp()
        {
            scarrier = $"Carrier (EAST){rn}" +
                       $" -----{rn}" +
                       $"|     |{rn}" +
                       $"|     |{rn}" +
                       $"|•••••|{rn}" +
                       $"|     |{rn}" +
                       $"|     |{rn}" +
                       $" -----";

            sbomber = $"Bomber (SOUTH){rn}" +
                      $" -----{rn}" +
                      $"|  •  |{rn}" +
                      $"|⇶ • ⇶|{rn}" +
                      $"|•••••|{rn}" +
                      $"|  •  |{rn}" +
                      $"|     |{rn}" +
                      $" -----";

            carrier = new Carrier(Orientation.EAST);
            bomber = new Bomber(Orientation.SOUTH);
        }

        /* checking for GetShapeIndex and GetAbsolutePositions with parameter null */
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException),
            "Parameter null was inappropriately allowed.")]
        public void Craft_TestGetShapeIndexArgumentNullException()
        {
            carrier.GetShapeIndex(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException),
            "Parameter null was inappropriately allowed.")]
        public void Craft_TestGetAbsolutePositionsCoordinate()
        {
            carrier.GetAbsolutePositions(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException),
            "Parameter null was inappropriately allowed.")]
        public void Craft_TestGetAbsolutePositions()
        {
            carrier.GetAbsolutePositions();
        }

        /* positioning a Carrier in a hypothetic board. It shoots completely over it.
         * It is verified that in its Shape now where there was CRAFT_VALUE, now there is HIT_VALUE.
         */
        [TestMethod]
        public void Craft_TestHitCarrier()
        {
            carrier.SetPosition(new Coordinate2D(7, 8));
            for (int i = 7; i < 12; i++)
                carrier.Hit(new Coordinate2D(i, 10));

            int[] aux = carrier.GetShape()[(int)carrier.GetOrientation()];
            for (int i = 10; i < 15; i++)
                Assert.AreEqual(HIT_VALUE, aux[i]);
        }

        /* positioning a Carrier in a hypothetic board. It shoots completely over it.
         * It is checked that in its Shape, where it has been fired, there is now HIT_VALUE
         * and where it has not been fired and there was a CRAFT_VALUE, there is still a CRAFT_VALUE.
         */
        [TestMethod]
        public void Craft_TestHitBomber()
        {
            bomber.SetPosition(new Coordinate3D(1, 3, 10000));
            for (int i = 3; i < 7; i++)
                bomber.Hit(new Coordinate3D(3, i, 10000));
            for (int i = 1; i < 6; i++)
                if (i != 3)
                    bomber.Hit(new Coordinate3D(i, 5, 10000));

            int[] aux = bomber.GetShape()[(int)bomber.GetOrientation()];
            for (int i = 0; i < 4; i++)
            {
                Assert.AreEqual(HIT_VALUE, aux[i*5 + 2], $"aux[{i+5 + 2}]");
                if (((i * 5 + 2) > 9) && ((i * 5 + 2) < 15))
                    Assert.AreEqual(HIT_VALUE, aux[i * 5 + 2], $"aux[{i + 5 + 2}]");
            }

            Assert.AreEqual(CRAFT_VALUE, aux[5]);
            Assert.AreEqual(CRAFT_VALUE, aux[9]);
        }

        /* positioning a Carrier in a hypothetic board. It shoots completely over it.
         * It is checked that ToString() returns a string equals scarrier created in SetUp()
         */
        [TestMethod]
        public void Craft_TestToStringCarrierHitted()
        {
            carrier.SetPosition(new Coordinate2D(7, 8));
            for (int i = 7; i < 12; i++)
                carrier.Hit(new Coordinate2D(i, 10));

            Assert.AreEqual(scarrier, carrier.ToString());
        }

        /* positioning a Bomber in a hypothetic board. It shoots completely over it.
         * It is checked that ToString() returns a string equals sbomber created in SetUp()
         */
        [TestMethod]
        public void Craft_TestToStringBomberHitted()
        {
            bomber.SetPosition(new Coordinate3D(1, 3, 10000));
            for (int i = 3; i < 7; i++)
                bomber.Hit(new Coordinate3D(3, i, 10000));
            for (int i = 1; i < 6; i++)
                if (i != 3)
                    bomber.Hit(new Coordinate3D(i, 5, 10000));

            Assert.AreEqual(sbomber, bomber.ToString());
        }
    }
}
