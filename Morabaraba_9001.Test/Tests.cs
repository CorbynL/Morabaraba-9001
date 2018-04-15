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
        public void Only12CowsPlacedForEachPlayer ()
        {
            //TODO: Each Player must only be able to place (at least) 12 cows
            Assert.That(false);
        }

        [Test]
        public void CowsCannotMoveDuringPlacement ()
        {
            //TODO: Check to see that cows cannot move during the placement phase
            Assert.That(false);
        }

        #endregion

        #region TESTS: Moving Phase
        
        [Test]
        public void CowCanOnlyMoveToConnectedSpace ()
        {
            //TODO: Check that a cow can't move to any other place other than the spaces connected to its position
            Assert.That(false);
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
