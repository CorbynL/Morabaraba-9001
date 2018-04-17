using System;
using NUnit.Framework;
using NSubstitute;
using System.Linq;
using Morabaraba_9001.Classes;
using System.Collections.Generic;
using System.Diagnostics;

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
            //Simulate entire placing phase
            GameSession g = new GameSession();
            for (int i = 0; i < 24; i++)
            {
                g.Play(i);
            }

            //Placing phase is over
            Assert.True(g.CurrentPhase == GameSession.Phase.Moving);

            // List of cows each player owns
            Cow[] player1Cows = g.board.getCowsByPlayer(0).ToArray();
            Cow[] player2Cows = g.board.getCowsByPlayer(1).ToArray();

            // Each player must have 12 cows
            Assert.True(player1Cows.Length == 12 && player2Cows.Length == 12);
        }

        [Test]
        public void CowsCannotMoveDuringPlacement ()
        {
            int counter = 0;
            GameSession g = new GameSession();

            //Simulate placing phase (24 turns)
            for (int i = 0; i < 24; i++)
            {
                //increment if Phase = Placing
                if (g.CurrentPhase == GameSession.Phase.Placing) { counter++; }
                g.Play(i);
                
            }    
            //Each turn should have be in placing phase (24 turns)
            Assert.True(counter == 24);

            //Placing phase is over, Phase sould be Moving
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
         new int[] {0,4,6,10},
         new int[] {1,3,5,7},
         new int[] {2,4,8,13},
         new int[] {3,7,11},
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
            g.board.initialiseCows();

            for (int i = 0; i < ExpectedMoves.Length; i++)
            {       
                //Move cow to each possible poition from it's placement
                foreach (int move in ExpectedMoves[i])
                {
                    Assert.That(g.board.IsValidMove(i, move));
                }
                int[] invalidMoves = allMoves.Where(x => !ExpectedMoves[i].Contains(x)).ToArray();

                // Try to move cow to all other unconnected spaces
                foreach (int move in invalidMoves)
                {
                    Assert.That(!g.board.IsValidMove(i, move));
                }
            }
        
    }
        
        static bool isCowtoPlace(int idx, int[] places)
        {
            if (places.Contains(idx))
            {
                return true;
            }
            else return false;
        }

        static Cow placeCow(Cow cow)
        {
            return new Cow(cow.Position, 0);
        }

        [Test]
        public static void OnlyMoveToEmptySpace ()
        {
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

            GameSession g = new GameSession();
            g.board.initialiseCows();
            Cow[][] a =
                {
                g.board.Cows.Select((x, idx) => isCowtoPlace(idx, ExpectedMoves[0]) ? placeCow(new Cow(idx, 0)) : x).ToArray(),
                g.board.Cows.Select((x, idx) => isCowtoPlace(idx, ExpectedMoves[1]) ? placeCow(new Cow(idx, 0)) : x).ToArray(),
                g.board.Cows.Select((x, idx) => isCowtoPlace(idx, ExpectedMoves[2]) ? placeCow(new Cow(idx, 0)) : x).ToArray(),
                g.board.Cows.Select((x, idx) => isCowtoPlace(idx, ExpectedMoves[3]) ? placeCow(new Cow(idx, 0)) : x).ToArray(),
                g.board.Cows.Select((x, idx) => isCowtoPlace(idx, ExpectedMoves[4]) ? placeCow(new Cow(idx, 0)) : x).ToArray(),
                g.board.Cows.Select((x, idx) => isCowtoPlace(idx, ExpectedMoves[5]) ? placeCow(new Cow(idx, 0)) : x).ToArray(),
                g.board.Cows.Select((x, idx) => isCowtoPlace(idx, ExpectedMoves[6]) ? placeCow(new Cow(idx, 0)) : x).ToArray(),
                g.board.Cows.Select((x, idx) => isCowtoPlace(idx, ExpectedMoves[7]) ? placeCow(new Cow(idx, 0)) : x).ToArray(),
                g.board.Cows.Select((x, idx) => isCowtoPlace(idx, ExpectedMoves[8]) ? placeCow(new Cow(idx, 0)) : x).ToArray(),
                g.board.Cows.Select((x, idx) => isCowtoPlace(idx, ExpectedMoves[9]) ? placeCow(new Cow(idx, 0)) : x).ToArray(),
                g.board.Cows.Select((x, idx) => isCowtoPlace(idx, ExpectedMoves[10]) ? placeCow(new Cow(idx, 0)) : x).ToArray(),
                g.board.Cows.Select((x, idx) => isCowtoPlace(idx, ExpectedMoves[11]) ? placeCow(new Cow(idx, 0)) : x).ToArray(),
                g.board.Cows.Select((x, idx) => isCowtoPlace(idx, ExpectedMoves[12]) ? placeCow(new Cow(idx, 0)) : x).ToArray(),
                g.board.Cows.Select((x, idx) => isCowtoPlace(idx, ExpectedMoves[13]) ? placeCow(new Cow(idx, 0)) : x).ToArray(),
                g.board.Cows.Select((x, idx) => isCowtoPlace(idx, ExpectedMoves[14]) ? placeCow(new Cow(idx, 0)) : x).ToArray(),
                g.board.Cows.Select((x, idx) => isCowtoPlace(idx, ExpectedMoves[15]) ? placeCow(new Cow(idx, 0)) : x).ToArray(),
                g.board.Cows.Select((x, idx) => isCowtoPlace(idx, ExpectedMoves[16]) ? placeCow(new Cow(idx, 0)) : x).ToArray(),
                g.board.Cows.Select((x, idx) => isCowtoPlace(idx, ExpectedMoves[17]) ? placeCow(new Cow(idx, 0)) : x).ToArray(),
                g.board.Cows.Select((x, idx) => isCowtoPlace(idx, ExpectedMoves[18]) ? placeCow(new Cow(idx, 0)) : x).ToArray(),
                g.board.Cows.Select((x, idx) => isCowtoPlace(idx, ExpectedMoves[19]) ? placeCow(new Cow(idx, 0)) : x).ToArray(),
                g.board.Cows.Select((x, idx) => isCowtoPlace(idx, ExpectedMoves[20]) ? placeCow(new Cow(idx, 0)) : x).ToArray(),
                g.board.Cows.Select((x, idx) => isCowtoPlace(idx, ExpectedMoves[21]) ? placeCow(new Cow(idx, 0)) : x).ToArray(),
                g.board.Cows.Select((x, idx) => isCowtoPlace(idx, ExpectedMoves[22]) ? placeCow(new Cow(idx, 0)) : x).ToArray(),
                g.board.Cows.Select((x, idx) => isCowtoPlace(idx, ExpectedMoves[23]) ? placeCow(new Cow(idx, 0)) : x).ToArray()
                };                

            //Cant move to occupied spaces
            for(int i = 0; i < 1; i++)
            {
                g.board.Cows = a[i];
                foreach(int move in ExpectedMoves[i])
                {
                    Assert.That(!g.board.IsValidMove(i,move));
                }
            }

            //Reset board
            g.board.initialiseCows();

            //Can move to unoccupied spaces
            for (int i = 0; i < 1; i++)
            {
                foreach (int move in ExpectedMoves[i])
                {
                    Assert.That(g.board.IsValidMove(i, move));
                }
            }
        }

        [Test]
        public void MoveCreatesNoDuplicates()
        {
            int[][] ExpectedMoves = new int[][]
        {
         new int[] {1,3,9},
         new int[] {0,2,4},
         new int[] {1,5,14},
         new int[] {0,4,6,10},
         new int[] {1,3,5,7},
         new int[] {2,4,8,13},
         new int[] {3,7,11},
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
         new int[] {14,20,22}
        };
            int[] allMoves = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23 };
            Board b = new Board();

            for (int i = 0; i < 23; i++)
            {
                for (int z = 0; z < ExpectedMoves[i].Length; z++)
                {
                    //Reset board with one cow at specified position
                    b.initialiseCows();
                    b.Place(0, allMoves[i]);

                    //Move cow to all connected spaces from it's current poisition
                    b.Move(0, i, ExpectedMoves[i][z]);

                    //Get number of cows on board
                    int[] numCows = b.Cows.Select(x => (int)x.PlayerID).ToArray();
                    int num = 0;    
                    //.Aggregate was too hard apparently
                    for (int j = 0; j < numCows.Length; j++)
                    {
                        if (numCows[j] == 0) { num++; }
                    }

                    //After move, number of cows on board should still be one.
                    Assert.That(num == 1);
                }
            }
        }

        [Test]
        public void CowSelectedHasEmptySpacesToMoveTo ()
        {
            GameSession g = new GameSession();

            //Place initial piece
            g.board.Place(0, 0);

            // Initial pieces' connected spaces
            int[] validMoves = new int[] { 1, 3, 9 };

            //Surround initial piece
            foreach(int i in validMoves)
            {
                g.board.Place(0, i);
            }
            
            //Assert that it cannot move to any connected spaces
            foreach (int i in validMoves)
            {
                Assert.False(g.board.canMoveCow(0));
            }
        }

        #endregion

        #region TESTS: Flying Phase

        [Test]
        public void CanCowsFlyAtFlyPhase()
        {
            int[] positions = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23 };
            int[][] ExpectedMoves = new int[][]
        {
         new int[] {1,3,9},
         new int[] {0,2,4},
         new int[] {1,5,14},
         new int[] {0,4,6,10},
         new int[] {1,3,5,7},
         new int[] {2,4,8,13},
         new int[] {3,7,11},
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

            GameSession g = new GameSession();

            foreach (int place in positions)
            {
                int[] unconnectSpaces = positions.Where(x => !ExpectedMoves[place].Contains(x)).ToArray();
            }
        }
        #endregion

        #region TESTS: General        

        [Test]
        public void ThreePlayerCowsInAlineFormsAMill ()
        {
            GameSession g = new GameSession();

            //Place two cows in a row
            g.board.Place(0, 0);
            g.board.Place(0, 1);

            //Assert that no new mills have been formed
            g.board.UpdateMills();
            Assert.False(g.board.areNewMills(0));

            //place third cow in a row and assert that a mill has been formed
            g.board.Place(0, 2);
            Assert.True(g.board.areNewMills(0));
        }

        [Test]
        public void MillIsInvalidIfCowsAreDifferentIDs ()
        {
            GameSession g = new GameSession();

            //Place two cows 3 in a row, with differnt player Id's
            g.board.Place(0, 0);
            g.board.Place(1, 1);
            g.board.Place(0, 2);

            //Assert that no player has a new mill
            Assert.False(g.board.areNewMills(0));
            Assert.False(g.board.areNewMills(1));
        }

        // THIS IS BRIAN's PROPERTY SO GITTTT OFF MY PROPERTY
        [Test]
        public void MillIsInvalidIfCowsNotInConnectedLine ()
        {
            //TODO: Check that the cows that will form a mill are in a line and connected in spaces

            Board
                b = new Board(), //Check mill forms if not in straight line
                c = new Board(), //Check if mill forms if in straight line but one or two cows has different ID's
                d = new Board(); //Check if 3 cows with the same ID's in a line form a mill

            //Oblique line
            b.Place(0, 0);
            b.Place(0, 1);
            b.Place(0, 3);

            //In a line, but different cow
            c.Place(0, 0);
            c.Place(0, 1);
            c.Place(1, 2);

            //In a line
            d.Place(0, 0);
            d.Place(0, 1);
            d.Place(0, 2);

            //Check for any new mills
            b.UpdateMills();
            c.UpdateMills();
            d.UpdateMills();

            Mill[] b_mills = b.Mills.Where(x => x.Id == 0).ToArray();
            Mill[] c_mills = c.Mills.Where(x => x.Id == 0).ToArray();
            Mill[] d_mills = d.Mills.Where(x => x.Id == 0).ToArray();

            Assert.That(b_mills.Length == 0);
            Assert.That(c_mills.Length == 0);
            Assert.That(d_mills.Length == 1);
        }

        [Test]
        public void KillingFromAMillHappensOnce ()
        {
            //TODO: When a mill is formed, it can only kill a cow once in the same turn it is formed and not again after if it continues to exist

            GameSession g = new GameSession();

            g.Play(0); // Player 1 to A1
            g.Play(21); // Player 2 to G1
            g.Play(1); // Player 1 to A4
            g.Play(22); // Player 2 to G4
            g.Play(2); // Player 1 to A7 - mill is formed

            g.Play(22); // Kill cow at G4
            g.Play(21); // Try kill cow at G1 (Second kill)

            Assert.That(g.board.Cows[22].PlayerID == -1); //Assert that the first cow is indeed dead
            Assert.That(g.board.Cows[21].PlayerID == 1); //Assert that the second cow is still there and belongs to Player 2
        }

        [Test]
        public void MillCowIsSafeIfNonMillCowsExist ()
        {
            //TODO: You cannot kill a cow in a mill if there exists cows that are not in a mill
            GameSession g = new GameSession();

            g.Play(0); // Player 1 to A1
            g.Play(21); // Player 2 to G1
            g.Play(1); // Player 1 to A4
            g.Play(22); // Player 2 to G4
            g.Play(2); // Player 1 to A7 - mill is formed (A1,A4,A7)
            g.Play(22); // Kill cow at G4
            g.Play(22); // Player 2 to G4
            g.Play(3); // Player 1 to B2
            
            //We now have player one with 4 cows: 3 in a mill and one loose, so we can try see which cows we can kill
            
            // Try kill Player 1's cows A1 and B2 as Player 2 
            Assert.False(g.board.CanKillAt(1, 0)); //Assert that you can't kill cow at A1 since its in a mill and safe since B2 is not in a mill            
            Assert.True(g.board.CanKillAt(1, 3)); //Assert that you can kill cow at B2 since it is not in a mill            
        }

        [Test]
        public void CanKillMillCowIfAllCowsInMills ()
        {
            //TODO: You can kill a cow in a mill if all cows are in mills

            GameSession g = new GameSession();

            g.Play(0); // Player 1 to A1
            g.Play(21); // Player 2 to G1
            g.Play(1); // Player 1 to A4
            g.Play(22); // Player 2 to G4
            g.Play(2); // Player 1 to A7 - first mill is formed (A1,A4,A7)
            g.Play(22); // Kill cow at G4
            g.Play(22); // Player 2 to G4
            g.Play(3); // Player 1 to B2
            g.Play(18); // Player 2 to F2
            g.Play(4); // Player 1 to B4
            g.Play(19); // Player 2 to F4
            g.Play(5); // Player 1 to B6 - second mill is formed (B2,B4,B6)

            Cow[] x = g.board.Cows;
            //We now have player one with 6 cows with 3 in each mill

            //Try kill Player 1's cows A1 and B2 as Player 2
            Assert.True(g.board.CanKillAt(1, 0)); //Assert that you can kill cow at A1 since all player 1's cows in a mill            
            Assert.True(g.board.CanKillAt(1, 3)); //Assert that you can kill cow at B2 since all player 1's cows in a mill
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
