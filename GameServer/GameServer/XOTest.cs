using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace GameServer
{
    [TestFixture]
    public class XOTest
    {
        XO xo = new XO();

        [SetUp]
        public void NewXO()
        {
            xo = new XO();
        }

        [Test]
        public void VictoryTestMainDiagonal()
        {
            xo.Turn1("0");
            xo.Turn2("1");
            xo.Turn1("4");
            xo.Turn2("6");
            xo.Turn1("8");

            Assert.AreEqual(1, xo.Checking());
        }

        [Test]
        public void VictoryTestSecondaryDiagonal()
        {
            xo.Turn1("2");
            xo.Turn2("1");
            xo.Turn1("4");
            xo.Turn2("7");
            xo.Turn1("6");

            Assert.AreEqual(1, xo.Checking());
        }

        [Test]
        public void VictoryTestFirstColumn()
        {
            xo.Turn1("0");
            xo.Turn2("1");
            xo.Turn1("3");
            xo.Turn2("7");
            xo.Turn1("6");

            Assert.AreEqual(1, xo.Checking());
        }

        [Test]
        public void VictoryTestSecondColumn()
        {
            xo.Turn1("1");
            xo.Turn2("2");
            xo.Turn1("4");
            xo.Turn2("6");
            xo.Turn1("7");

            Assert.AreEqual(1, xo.Checking());
        }

        [Test]
        public void VictoryTestThirdColumn()
        {
            xo.Turn1("2");
            xo.Turn2("1");
            xo.Turn1("5");
            xo.Turn2("7");
            xo.Turn1("8");

            Assert.AreEqual(1, xo.Checking());
        }

        [Test]
        public void VictoryTestFirstRow()
        {
            xo.Turn1("0");
            xo.Turn2("5");
            xo.Turn1("1");
            xo.Turn2("7");
            xo.Turn1("2");

            Assert.AreEqual(1, xo.Checking());
        }

        [Test]
        public void VictoryTestSecondRow()
        {
            xo.Turn1("3");
            xo.Turn2("1");
            xo.Turn1("4");
            xo.Turn2("7");
            xo.Turn1("5");

            Assert.AreEqual(1, xo.Checking());
        }

        [Test]
        public void VictoryTestThirdRow()
        {
            xo.Turn1("6");
            xo.Turn2("1");
            xo.Turn1("7");
            xo.Turn2("0");
            xo.Turn1("8");

            Assert.AreEqual(1, xo.Checking());
        }

        [Test]
        public void StandoffTest1()
        {
            xo.Turn1("6");
            xo.Turn2("1");
            xo.Turn1("7");
            xo.Turn2("8");
            xo.Turn1("0");
            xo.Turn2("3");
            xo.Turn1("4");
            xo.Turn2("2");
            xo.Turn1("5");

            Assert.AreEqual(-1, xo.Checking());
        }

        [Test]
        public void StandoffTest2()
        {
            xo.Turn1("7");
            xo.Turn2("1");
            xo.Turn1("2");
            xo.Turn2("4");
            xo.Turn1("3");
            xo.Turn2("8");
            xo.Turn1("5");
            xo.Turn2("6");
            xo.Turn1("0");

            Assert.AreEqual(-1, xo.Checking());
        }

        [Test]
        public void StandoffTest3()
        {
            xo.Turn1("0");
            xo.Turn2("1");
            xo.Turn1("2");
            xo.Turn2("3");
            xo.Turn1("4");
            xo.Turn2("6");
            xo.Turn1("5");
            xo.Turn2("8");
            xo.Turn1("7");

            Assert.AreEqual(-1, xo.Checking());
        }

        [Test]
        public void StandoffTest4()
        {
            xo.Turn1("0");
            xo.Turn2("2");
            xo.Turn1("1");
            xo.Turn2("3");
            xo.Turn1("5");
            xo.Turn2("4");
            xo.Turn1("6");
            xo.Turn2("7");
            xo.Turn1("8");

            Assert.AreEqual(-1, xo.Checking());
        }

        [Test]
        public void StandoffTest5()
        {
            xo.Turn1("1");
            xo.Turn2("0");
            xo.Turn1("2");
            xo.Turn2("5");
            xo.Turn1("3");
            xo.Turn2("6");
            xo.Turn1("4");
            xo.Turn2("7");
            xo.Turn1("8");

            Assert.AreEqual(-1, xo.Checking());
        }
    }
}
