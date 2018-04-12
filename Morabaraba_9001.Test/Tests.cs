using System;
using NUnit.Framework;
using System.Linq;
using Morabaraba_9001;

namespace Morabaraba_9001.Test
{
    [TestFixture]
    public class Tests
    {
        #region TESTS: Start and Placement Phases

        [Test]
        public void BoardIsEmptyWhenGameStarts ()
        {
            //TODO: Check that the board is empty when the game starts
        }

        [Test]        
        public void PlayerWithRedCowsGoesFirst ()
        {
            //TODO: Make sure that the the player who goes first has the dark cows
        }

        [Test]
        public void CowsPlacedOnEmptySpacesOnly ()
        {
            //TODO: Check to see that when we place a cow, that it can only be placed on an empty space
        }

        [Test]
        public void Only12CowsPlacedForEachPlayer ()
        {
            //TODO: Each Player must only be able to place (at least) 12 cows
        }

        [Test]
        public void CowsCannotMoveDuringPlacement ()
        {
            //TODO: Check to see that cows cannot move during the placement phase
        }

        #endregion

        #region TESTS: Moving Phase
        
        [Test]
        public void CowCanOnlyMoveToConnectedSpace ()
        {
            //TODO: Check that a cow can't move to any other place other than the spaces connected to its position
        }

        [Test]
        public void OnlyMoveToEmptySpace ()
        {
            //TODO: Check that the move a cow makes is to an empty space only
        }

        [Test]
        public void MoveCreatesNoDuplicates ()
        {
            //TODO: The number of cows must on increase or decrease after a move
        }

        [Test]
        public void CowSelectedHasEmptySpacesToMoveTo ()
        {
            //TODO: Check that if we select a cow to move, that it can actually move i.e. there is an empty connected space next to it
        }

        #endregion

        #region TESTS: Flying Phase

        [Test]
        public void CanCowsFlyAtFlyPhase ()
        {
            //TODO: Cows can move to any empty space when the player only has 3 cows left
        }

        #endregion

        #region TESTS: General

        [Test]
        public void ThreePlayerCowsInAlineFormsAMill ()
        {
            //TODO: Check that a mill is formed when 3 cows that have the same playerID are in a line
        }

        [Test]
        public void MillIsInvalidIfCowsAreDifferentIDs ()
        {
            //TODO: Check that mills can't be formed if any of the 3 cows is different in playerID
        }

        [Test]
        public void MillIsInvalidIfCowsNotInConnectedLine ()
        {
            //TODO: Check that the cows that will form a mill are in a line and connected in spaces
        }

        [Test]
        public void KillingFromAMillHappensOnce ()
        {
            //TODO: When a mill is formed, it can only kill a cow once in the same turn it is formed and not again after if it continues to exist
        }

        [Test]
        public void MillCowIsSafeIfNonMillCowsExist ()
        {
            //TODO: You cannot kill a cow in a mill if there exists cows that are not in a mill
        }

        [Test]
        public void CanKillMillCowIfAllCowsInMills ()
        {
            //TODO: You can kill a cow in a mill if all cows are in mills
        }

        [Test]
        public void PlayerCannotShootOwnCows ()
        {
            //TODO: You cannot kill your own cows when you are about to kill
        }

        [Test]
        public void PlayerCannotShootEmptySpaces ()
        {
            //TODO: You cannot shoot an empty space
        }

        [Test]
        public void CowsShotAreRemoved ()
        {
            //TODO: A cow that has been killed must be removed from the board and replaced by an empty space
        }

        [Test]
        public void WinIfPlayerCannotMove ()
        {
            //TODO: Check win condition that if the player cannot move any of their cows, then the other player wins
        }

        [Test]
        public void WinIfPlayerHasTwoCowsAndNotPlacing ()
        {
            //TODO: Check win condition that if the player has 2 or fewer cows and is not placing, then the other player wins
        }

        #endregion

    }
}
