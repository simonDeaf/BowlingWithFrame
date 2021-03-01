using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingWithFrame
{
    [TestFixture]
    public class BowlingTest
    {
        BowlingGame game;

        public BowlingTest()
        {
        }

        [SetUp]
        public void SetUp()
        {
            game = new BowlingGame();
        }

        [Test]
        public void Hookup()
        {
            Assert.True(true);
        }

        [Test]
        // -- -- -- -- -- -- -- -- -- -- (20 rolls: 10 pairs of miss) = 10 frames * 0 points = 0
        public void GutterBalls()
        {
            ManyOpenFrames(10, 0, 0);
            Assert.AreEqual(0, game.Score());
        }

        [Test]
        // 5/ 5/ 5/ 5/ 5/ 5/ 5/ 5/ 5/ 5/5 (21 rolls: 10 pairs of 5 and spare, with a final 5) = 10 frames * 15 points = 150
        public void SpareFive()
        {
            for (int i = 0; i < 10; i++)
                game.Spare(5, 5);
            game.BonusRoll(5);
            game.BonusRoll(5);
            game.BonusRoll(5);
            Assert.AreEqual(150, game.Score());
        }

        [Test]
        // X X X X X X X X X X X X (12 rolls: 12 strikes) = 10 frames * 30 points = 300
        public void Perfect()
        {
            for (int i = 0; i < 10; i++)
                game.Strike();
            game.BonusRoll(10);
            game.BonusRoll(10);
            Assert.AreEqual(300, game.Score());
        }

        [Test]
        // 9- 9- 9- 9- 9- 9- 9- 9- 9- 9- (20 rolls: 10 pairs of 9 and miss) = 10 frames * 9 points = 90
        public void Nines()
        {
            ManyOpenFrames(10, 9, 0);
            Assert.AreEqual(90, game.Score());
        }

        [Test]
        // 33 33 33 33 33 33 33 33 33 33 (20 rolls: 10 pairs of 3) = 10 frames * (3+3) points = 60
        public void Threes()
        {
            ManyOpenFrames(10, 3, 3);
            Assert.AreEqual(60, game.Score());
        }

        [Test]
        // 4/ 35 -- -- -- -- -- -- -- -- (20 rolls: one spare, (3+5)=8  and 8 pairs of miss) = 10 + 3 + (3+5) points = 21
        public void Spare()
        {
            game.Spare(4, 6);
            game.OpenFrame(3, 5);
            ManyOpenFrames(8, 0, 0);
            Assert.AreEqual(21, game.Score());
        }

        [Test]
        // 4/ 53 -- -- -- -- -- -- -- -- (20 rolls: one spare, (5+3)=8  and 8 pairs of miss) = 10 + 5 + (3+5) points = 23
        public void Spare2()
        {
            game.Spare(4, 6);
            game.OpenFrame(5, 3);
            ManyOpenFrames(8, 0, 0);
            Assert.AreEqual(23, game.Score());
        }

        [Test]
        // X 53 -- -- -- -- -- -- -- -- (19 rolls: one strike, (5+3)=8  and 8 pairs of miss) = 10 + 8 + (3+5) points = 26
        public void Strike()
        {
            game.Strike();
            game.OpenFrame(5, 3);
            ManyOpenFrames(8, 0, 0);
            Assert.AreEqual(26, game.Score());
        }

        [Test]
        // -- -- -- -- -- -- -- -- -- X53 (21 rolls: 9 pairs of miss, one strike and (5+3)=8 ) = 10 + 8 points = 18
        public void StrikeFinalFrame()
        {
            ManyOpenFrames(9, 0, 0);
            game.Strike();
            game.BonusRoll(5);
            game.BonusRoll(3);
            Assert.AreEqual(18, game.Score());
        }

        [Test]
        // -- -- -- -- -- -- -- -- -- 4/5 (21 rolls: 9 pairs of miss, one spare with a final 5 ) = 10 + 5 points = 15
        public void SpareFinalFrame()
        {
            ManyOpenFrames(9, 0, 0);
            game.Spare(4, 6);
            game.BonusRoll(5);
            Assert.AreEqual(15, game.Score());
        }

        [Test]
        // X 4/ X 4/ X 4/ X 4/ X 4/X (16 rolls: 5 pairs of strike and spare with a final strike ) = 10 points = 200
        public void Alternating()
        {
            for (int i = 0; i < 5; i++)
            {
                game.Strike();
                game.Spare(4, 6);
            }
            game.BonusRoll(10);
            Assert.AreEqual(200, game.Score());
        }

        private void ManyOpenFrames(int count, int firstThrow, int secondThrow)
        {
            for (int frameNumber = 0; frameNumber < count; frameNumber++)
                game.OpenFrame(firstThrow, secondThrow);
        }
    }
}