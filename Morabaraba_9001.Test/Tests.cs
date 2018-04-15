using System;
using NUnit.Framework;
using NSubstitute;
using System.Linq;
using Morabaraba_9001.Classes;

namespace Morabaraba_9001.Test
{ 
    [TestFixture]
    public class Tests
    {
        #region TESTS: Start and Placement Phases

        [Test]
        public void BoardIsEmptyWhenGameStarts ()
        {            
            IBoard b = new Board();
            ICow[] c = b.Cows.Where(x => x.PlayerID == -1).ToArray();
            Assert.That(c.Length == 24);
        }

        [Test]        
        public void PlayerWithRedCowsGoesFirst ()
        {
            //TODO: Make sure that the the player who goes first has the dark cows            
            GameSession g = Substitute.For<GameSession>();            
            Assert.AreEqual(g.CurrentPlayer, GameSession.Player.Red);
        }

        [Test]
        public void CowsPlacedOnEmptySpacesOnly ()
        {
            Board b = new Board();
            // Place a cow at possition 0
            b.Cows[0] = new Cow(0,1);
            // Check that you can not place a cow at possition 0
            Assert.That(!b.CanPlaceAt(0));
            // Check that you can place a cow at a blank possition
            Assert.That(b.CanPlaceAt(1));
            //TODO: Check to see that when we place a cow, that it can only be placed on an empty space
        }

        [Test]
        public void Only12CowsPlacedForEachPlayer()
        {
            GameSession g = new GameSession();
            for (int i = 0; i < 24; i++)
            {
                g.Play(i);
            }

            //Placing phase is over
            Assert.True(g.CurrentPhase == GameSession.Phase.Moving);

            Cow[] player1Cows = g.board.getCowsByPlayer(0).ToArray();
            Cow[] player2Cows = g.board.getCowsByPlayer(1).ToArray();

            Assert.True(player1Cows.Length == 12 && player2Cows.Length == 12);
        }

        [Test]
        public void CowsCannotMoveDuringPlacement ()
        {
            int counter = 0;

            GameSession g = new GameSession();
            for (int i = 0; i < 24; i++)
            {
                if (g.CurrentPhase == GameSession.Phase.Placing) { counter++; }
                g.Play(i);
                
            }            
            Assert.True(counter == 24);
            Assert.True(g.CurrentPhase == GameSession.Phase.Moving);
        }

        #endregion

        #region TESTS: Moving Phase
        
        [Test]
        public void CowCanOnlyMoveToConnectedSpace ()
        {
            int[] allMoves = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23 };
            GameSession g = new GameSession();
            int[][] ExpectedMoves = new int[][]
        {
         new int[] {1,3,9},
         new int[] {0,2,4},
         new int[] {1,5,14},
         new int[] {0,4,6},
         new int[] {1,3,5,7},
         new int[] {2,4,8,13},
         new int[] {4,7,11},
         new int[] {4,6,8},
         new int[] {5,7,12},
         new int[] {0,10,21},
         new int[] {3,9,11,18},
         new int[] {6,10,15},
         new int[] {8,13,17},
         new int[] {5,12,14,20},
         new int[] {2,13,23},
         new int[] {11,16,18},
         new int[] {15,17,19},
         new int[] {12,16,20},
         new int[] {10,15,19,21},
         new int[] {16,18,20,22},
         new int[] {13,17,19,23},
         new int[] {9,18,22},
         new int[] {19,21,23},
         new int[] {14,20,22},
        };
            
            for(int i = 0; i < ExpectedMoves.Length; i++)
            {
                g.board.initialiseCows();
                g.board.Cows[i] = new Cow(i, -1);
                foreach (int move in ExpectedMoves[i])
                {
                    Assert.That(g.board.IsValidMove(i, move));
                }
                int[] invalidMoves = allMoves.Where(x => !ExpectedMoves[i].Contains(x)).ToArray();

                foreach(int move in invalidMoves)
                {
                    Assert.That(!g.board.IsValidMove(i, move));
                }
            }
        

    }

        [Test]
        public void OnlyMoveToEmptySpace ()
        {
            //TODO: Check that the move a cow makes is to an empty space only
            Assert.That(false);
        }

        [Test]
        public void MoveCreatesNoDuplicates ()
        {
            //TODO: The number of cows must on increase or decrease after a move
            Assert.That(false);
        }

        [Test]
        public void CowSelectedHasEmptySpacesToMoveTo ()
        {
            //TODO: Check that if we select a cow to move, that it can actually move i.e. there is an empty connected space next to it
            Assert.That(false);
        }

        #endregion

        #region TESTS: Flying Phase

        [Test]
        public void CanCowsFlyAtFlyPhase ()
        {
            //TODO: Cows can move to any empty space when the player only has 3 cows left
            Assert.That(false);
        }

        #endregion

        #region TESTS: General        

        [Test]
        public void ThreePlayerCowsInAlineFormsAMill ()
        {
            //TODO: Check that a mill is formed when 3 cows that have the same playerID are in a line
            Assert.That(false);
        }

        [Test]
        public void MillIsInvalidIfCowsAreDifferentIDs ()
        {
            //TODO: Check that mills can't be formed if any of the 3 cows is different in playerID
            Assert.That(false);
        }

        [Test]
        public void MillIsInvalidIfCowsNotInConnectedLine ()
        {
            //TODO: Check that the cows that will form a mill are in a line and connected in spaces
            Assert.That(false);
        }

        [Test]
        public void KillingFromAMillHappensOnce ()
        {
            //TODO: When a mill is formed, it can only kill a cow once in the same turn it is formed and not again after if it continues to exist
            Assert.That(false);
        }

        [Test]
        public void MillCowIsSafeIfNonMillCowsExist ()
        {
            //TODO: You cannot kill a cow in a mill if there exists cows that are not in a mill
            Assert.That(false);
        }

        [Test]
        public void CanKillMillCowIfAllCowsInMills ()
        {
            //TODO: You can kill a cow in a mill if all cows are in mills
            Assert.That(false);
        }

        [Test]
        public void PlayerCannotShootOwnCows ()
        {
            //TODO: You cannot kill your own cows when you are about to kill
            Assert.That(false);
        }

        [Test]
        public void PlayerCannotShootEmptySpaces ()
        {
            //TODO: You cannot shoot an empty space
            Assert.That(false);
        }

        [Test]
        public void CowsShotAreRemoved ()
        {
            //TODO: A cow that has been killed must be removed from the board and replaced by an empty space
            Assert.That(false);
        }

        [Test]
        public void WinIfPlayerCannotMove ()
        {
            //TODO: Check win condition that if the player cannot move any of their cows, then the other player wins
            Assert.That(false);
        }

        [Test]
        public void WinIfPlayerHasTwoCowsAndNotPlacing ()
        {
            //TODO: Check win condition that if the player has 2 or fewer cows and is not placing, then the other player wins
            Assert.That(false);
        }

        #endregion

    }
}
