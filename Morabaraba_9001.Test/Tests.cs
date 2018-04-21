using System;
using NUnit.Framework;
using NSubstitute;
using System.Linq;
using Morabaraba_9001.Classes;
using System.Collections.Generic;
using System.Diagnostics;
using Morabaraba_9001.Interfaces;
using Morabaraba_9001.Factories;

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
            ICow[] c = b.Cows.Where(x => x.Color == Color.Black).ToArray();
            Assert.That(c.Length == 24);
        }

        [Test]
        public void PlayerWithRedCowsGoesFirst()
        {
            //TODO: Make sure that the the player who goes first has the dark cows   
            IGameSession gameSession  = GameSessionFactory.CreateGameSession();
            Assert.AreEqual(gameSession.Current_Player.Color, Color.Red);
        }

        static object[] TryToPlaceCowsAt = new object[]
        {
            new object[] {0,11,Color.Black},
            new object[] {1,11,Color.Black},
            new object[] {2,11,Color.Black},
            new object[] {3,11,Color.Black},
            new object[] {4,11,Color.Black},
            new object[] {5,11,Color.Black},
            new object[] {6,11,Color.Black},
            new object[] {7,11,Color.Black},
            new object[] {8,11,Color.Black},
            new object[] {9,11,Color.Black},
            new object[] {10,12,Color.Red},
            new object[] {11,12,Color.Red},
            new object[] {12,12,Color.Red},
            new object[] {13,12,Color.Red},
            new object[] {14,12,Color.Red},
            new object[] {15,12,Color.Red},
            new object[] {16,12,Color.Red},
            new object[] {17,12,Color.Red},
            new object[] {18,12,Color.Red},
            new object[] {19,12,Color.Red},
            new object[] {20,12,Color.Red},
            new object[] {21,12,Color.Red},
            new object[] {22,12,Color.Red},
            new object[] {23,12,Color.Red},
        };

        [Test]
        [TestCaseSource(nameof(TryToPlaceCowsAt))]
        public void CowsPlacedOnEmptySpacesOnly(int pos, int numLeft, Color c)
        {
            IBoard b = Substitute.For<IBoard>();
            b.Occupant(Arg.Any<int>()).Returns(new Cow(-1,c));
            ICowBox box = new CowBox();
            IReferee r = new Referee(b,box);
            IPlayer p1 = new Player(Color.Red,box);
            
            p1.Place(pos, b, r, Phase.Placing);

            //Cow was placed at empty position
            if(c == Color.Black)
            {
                //p1 has ll cows left
                Assert.True(box.RemainingCows(p1.Color) == 11);
            }

            //Position was occupied => no cow was placed
            //p1 still has all his cows
            else { Assert.True(box.RemainingCows(p1.Color) == 12); }
        }

        [Test]
        public void Only12CowsPlacedForEachPlayer()
        {
            IBoard b = new Board();
            ICowBox box = new CowBox();
            IReferee r = new Referee(b, box);
            IPlayer p = new Player(Color.Red, box);

            //Place all of p's cows
            for(int i = 0; i < 12; i++)
            {
                p.Place(i, b, r, Phase.Placing);
            }
            //Board should have all 12 of p's cows
            Assert.True(b.numCowsOnBoard() == 12);

            //try to place another cow for p
            p.Place(13, b, r, Phase.Placing);

            //Verify that no new cow has been placed
            Assert.True(b.numCowsOnBoard() == 12);
            
        }


        [Test]
        public void CowsCannotMoveDuringPlacement()
        {
            IBoard b = new Board();

            ICowBox box = Substitute.For<ICowBox>();
            box.TakeCow(Arg.Any<Color>()).Returns(new Cow(-1, Color.Red));

            IPlayer p1 = new Player(Color.Red,box);

            IReferee r = new Referee(b, box);

            for(int i = 0; i < 23; i++)
            {
                p1.Place(i, b, r,Phase.Placing);
                int[] moves = b.ConnectedSpaces(i);                

                foreach(int m in moves)
                {
                    Assert.False(p1.Move(i, m, b, r,Phase.Placing));
                }
            }
        }

        #endregion

        #region TESTS: Moving Phase

        // [Test]
        // public void CowCanOnlyMoveToConnectedSpace ()
        // {            
        //     GameSession g = new GameSession();
        //     int[] possibleMoves = { 1, 3, 9 }, invalidMoves = { 0, 2, 4, 23 };

        //     g.Play(0);

        //     foreach (int move in possibleMoves)
        //     {
        //         Assert.That(g.board.canMoveFrom(0, 0));
        //         Assert.That(g.board.canMoveTo(0, 0, move));
        //     }            
        //     foreach (int move in invalidMoves)
        //     {
        //         Assert.That(g.board.canMoveFrom(0, 0));
        //         Assert.That(!g.board.canMoveTo(0, 0, move));
        //     }
        // }       


        // static bool isCowtoPlace(int idx, int[] places)
        // {
        //     if (places.Contains(idx))
        //     {
        //         return true;
        //     }
        //     else return false;
        // }

        // static Cow placeCow(Cow cow)
        // {
        //     return new Cow(cow.Position, 0);
        // }

        // [Test]
        // public static void OnlyMoveToEmptySpace ()
        // {
        //     int[][] ExpectedMoves = new int[][]
        //{
        //  new int[] {1,3,9},
        //  new int[] {0,2,4},
        //  new int[] {1,5,14},
        //  new int[] {0,4,6},
        //  new int[] {1,3,5,7},
        //  new int[] {2,4,8,13},
        //  new int[] {4,7,11},
        //  new int[] {4,6,8},
        //  new int[] {5,7,12},
        //  new int[] {0,10,21},
        //  new int[] {3,9,11,18},
        //  new int[] {6,10,15},
        //  new int[] {8,13,17},
        //  new int[] {5,12,14,20},
        //  new int[] {2,13,23},
        //  new int[] {11,16,18},
        //  new int[] {15,17,19},
        //  new int[] {12,16,20},
        //  new int[] {10,15,19,21},
        //  new int[] {16,18,20,22},
        //  new int[] {13,17,19,23},
        //  new int[] {9,18,22},
        //  new int[] {19,21,23},
        //  new int[] {14,20,22},
        //};

        //     GameSession g = new GameSession();
        //     g.board.initialiseCows();
        //     Cow[][] a =
        //         {
        //         g.board.Cows.Select((x, idx) => isCowtoPlace(idx, ExpectedMoves[0]) ? placeCow(new Cow(idx, 0)) : x).ToArray(),
        //         g.board.Cows.Select((x, idx) => isCowtoPlace(idx, ExpectedMoves[1]) ? placeCow(new Cow(idx, 0)) : x).ToArray(),
        //         g.board.Cows.Select((x, idx) => isCowtoPlace(idx, ExpectedMoves[2]) ? placeCow(new Cow(idx, 0)) : x).ToArray(),
        //         g.board.Cows.Select((x, idx) => isCowtoPlace(idx, ExpectedMoves[3]) ? placeCow(new Cow(idx, 0)) : x).ToArray(),
        //         g.board.Cows.Select((x, idx) => isCowtoPlace(idx, ExpectedMoves[4]) ? placeCow(new Cow(idx, 0)) : x).ToArray(),
        //         g.board.Cows.Select((x, idx) => isCowtoPlace(idx, ExpectedMoves[5]) ? placeCow(new Cow(idx, 0)) : x).ToArray(),
        //         g.board.Cows.Select((x, idx) => isCowtoPlace(idx, ExpectedMoves[6]) ? placeCow(new Cow(idx, 0)) : x).ToArray(),
        //         g.board.Cows.Select((x, idx) => isCowtoPlace(idx, ExpectedMoves[7]) ? placeCow(new Cow(idx, 0)) : x).ToArray(),
        //         g.board.Cows.Select((x, idx) => isCowtoPlace(idx, ExpectedMoves[8]) ? placeCow(new Cow(idx, 0)) : x).ToArray(),
        //         g.board.Cows.Select((x, idx) => isCowtoPlace(idx, ExpectedMoves[9]) ? placeCow(new Cow(idx, 0)) : x).ToArray(),
        //         g.board.Cows.Select((x, idx) => isCowtoPlace(idx, ExpectedMoves[10]) ? placeCow(new Cow(idx, 0)) : x).ToArray(),
        //         g.board.Cows.Select((x, idx) => isCowtoPlace(idx, ExpectedMoves[11]) ? placeCow(new Cow(idx, 0)) : x).ToArray(),
        //         g.board.Cows.Select((x, idx) => isCowtoPlace(idx, ExpectedMoves[12]) ? placeCow(new Cow(idx, 0)) : x).ToArray(),
        //         g.board.Cows.Select((x, idx) => isCowtoPlace(idx, ExpectedMoves[13]) ? placeCow(new Cow(idx, 0)) : x).ToArray(),
        //         g.board.Cows.Select((x, idx) => isCowtoPlace(idx, ExpectedMoves[14]) ? placeCow(new Cow(idx, 0)) : x).ToArray(),
        //         g.board.Cows.Select((x, idx) => isCowtoPlace(idx, ExpectedMoves[15]) ? placeCow(new Cow(idx, 0)) : x).ToArray(),
        //         g.board.Cows.Select((x, idx) => isCowtoPlace(idx, ExpectedMoves[16]) ? placeCow(new Cow(idx, 0)) : x).ToArray(),
        //         g.board.Cows.Select((x, idx) => isCowtoPlace(idx, ExpectedMoves[17]) ? placeCow(new Cow(idx, 0)) : x).ToArray(),
        //         g.board.Cows.Select((x, idx) => isCowtoPlace(idx, ExpectedMoves[18]) ? placeCow(new Cow(idx, 0)) : x).ToArray(),
        //         g.board.Cows.Select((x, idx) => isCowtoPlace(idx, ExpectedMoves[19]) ? placeCow(new Cow(idx, 0)) : x).ToArray(),
        //         g.board.Cows.Select((x, idx) => isCowtoPlace(idx, ExpectedMoves[20]) ? placeCow(new Cow(idx, 0)) : x).ToArray(),
        //         g.board.Cows.Select((x, idx) => isCowtoPlace(idx, ExpectedMoves[21]) ? placeCow(new Cow(idx, 0)) : x).ToArray(),
        //         g.board.Cows.Select((x, idx) => isCowtoPlace(idx, ExpectedMoves[22]) ? placeCow(new Cow(idx, 0)) : x).ToArray(),
        //         g.board.Cows.Select((x, idx) => isCowtoPlace(idx, ExpectedMoves[23]) ? placeCow(new Cow(idx, 0)) : x).ToArray()
        //         };                

        //     //Cant move to occupied spaces
        //     for(int i = 0; i < 1; i++)
        //     {
        //         g.board.Cows = a[i];
        //         foreach(int move in ExpectedMoves[i])
        //         {
        //             Assert.That(!g.board.canMoveFrom(0, i));
        //             Assert.That(!g.board.canMoveTo(0, i, move));
        //         }
        //     }

        //     //Reset board
        //     g.board.initialiseCows();
        //     g.Play(0);

        //     //Can move to unoccupied spaces
        //     foreach (int move in ExpectedMoves[0])
        //     {               
        //         Assert.That(g.board.canMoveFrom(0, 0));
        //         Assert.That(g.board.canMoveTo(0, 0, move));                
        //     }
        // }

        // [Test]
        // public void MoveCreatesNoDuplicates()
        // {
        //     int[][] ExpectedMoves = new int[][]
        // {
        //  new int[] {1,3,9},
        //  new int[] {0,2,4},
        //  new int[] {1,5,14},
        //  new int[] {0,4,6,10},
        //  new int[] {1,3,5,7},
        //  new int[] {2,4,8,13},
        //  new int[] {3,7,11},
        //  new int[] {4,6,8},
        //  new int[] {5,7,12},
        //  new int[] {0,10,21},
        //  new int[] {3,9,11,18},
        //  new int[] {6,10,15},
        //  new int[] {8,13,17},
        //  new int[] {5,12,14,20},
        //  new int[] {2,13,23},
        //  new int[] {11,16,18},
        //  new int[] {15,17,19},
        //  new int[] {12,16,20},
        //  new int[] {10,15,19,21},
        //  new int[] {16,18,20,22},
        //  new int[] {13,17,19,23},
        //  new int[] {9,18,22},
        //  new int[] {19,21,23},
        //  new int[] {14,20,22}
        // };
        //     int[] allMoves = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23 };
        //     Board b = new Board();

        //     for (int i = 0; i < 23; i++)
        //     {
        //         for (int z = 0; z < ExpectedMoves[i].Length; z++)
        //         {
        //             //Reset board with one cow at specified position
        //             b.initialiseCows();
        //             b.Place(0, allMoves[i]);

        //             //Move cow to all connected spaces from it's current poisition
        //             b.Move(i, ExpectedMoves[i][z]);

        //             //Get number of cows on board
        //             int[] numCows = b.Cows.Select(x => (int)x.PlayerID).ToArray();
        //             int num = 0;    
        //             //.Aggregate was too hard apparently
        //             for (int j = 0; j < numCows.Length; j++)
        //             {
        //                 if (numCows[j] == 0) { num++; }
        //             }

        //             //After move, number of cows on board should still be one.
        //             Assert.That(num == 1);
        //         }
        //     }
        // }

        // [Test]
        // public void CowSelectedHasEmptySpacesToMoveTo ()
        // {
        //     GameSession g = new GameSession();

        //     //Place initial piece
        //     g.board.Place(0, 0);

        //     // Initial pieces' connected spaces
        //     int[] validMoves = new int[] { 1, 3, 9 };

        //     //Surround initial piece
        //     foreach(int i in validMoves)
        //     {
        //         g.board.Place(0, i);
        //     }

        //     //Assert that it cannot move to any connected spaces
        //     Assert.False(g.board.canMoveFrom(0, 0));
        // }

        #endregion

        #region TESTS: Flying Phase

        // [Test]
        // public void CanCowsFlyAtFlyPhase()
        // {
        //     int[] positions = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23 };
        //     int[][] ExpectedMoves = new int[][]
        // {
        //  new int[] {1,3,9},
        //  new int[] {0,2,4},
        //  new int[] {1,5,14},
        //  new int[] {0,4,6,10},
        //  new int[] {1,3,5,7},
        //  new int[] {2,4,8,13},
        //  new int[] {3,7,11},
        //  new int[] {4,6,8},
        //  new int[] {5,7,12},
        //  new int[] {0,10,21},
        //  new int[] {3,9,11,18},
        //  new int[] {6,10,15},
        //  new int[] {8,13,17},
        //  new int[] {5,12,14,20},
        //  new int[] {2,13,23},
        //  new int[] {11,16,18},
        //  new int[] {15,17,19},
        //  new int[] {12,16,20},
        //  new int[] {10,15,19,21},
        //  new int[] {16,18,20,22},
        //  new int[] {13,17,19,23},
        //  new int[] {9,18,22},
        //  new int[] {19,21,23},
        //  new int[] {14,20,22},
        // };

        //     GameSession g = new GameSession();

        //     foreach (int place in positions)
        //     {
        //         int[] unconnectSpaces = positions.Where(x => !ExpectedMoves[place].Contains(x)).ToArray();
        //     }
        // }
        #endregion

        #region TESTS: General        

        [Test]
        public void ThreePlayerCowsInAlineFormsAMill()
        {
            IBoard board = new Board();
            ICowBox box = new CowBox();
            IPlayer p = new Player(Color.Red, box);
            IReferee referee = new Referee(board, box);

            //Place 2 cows in a row, no mills form
            Assert.False(board.areNewMills(Color.Red));
            p.Place(0, board, referee,Phase.Placing);
            Assert.False(board.areNewMills(Color.Red));
            p.Place(1, board, referee, Phase.Placing);
            Assert.False(board.areNewMills(Color.Red));

            //Place 3rd cow in a row, mill forms
            p.Place(2, board, referee, Phase.Placing);
            Assert.True(board.areNewMills(Color.Red));

        }

        [Test]
        public void MillIsInvalidIfCowsAreDifferentIDs()
        {
            IBoard board = new Board();
            ICowBox box = new CowBox();
            IPlayer p1 = new Player(Color.Red, box);
            IPlayer p2 = new Player(Color.Blue, box);
            IReferee referee = new Referee(board, box);

            //Player 1 and player 2 place cows next to each other
            Assert.False(board.areNewMills(Color.Red));
            p1.Place(0, board, referee, Phase.Placing);
            Assert.False(board.areNewMills(Color.Red));
            p2.Place(1, board, referee, Phase.Placing);
            Assert.False(board.areNewMills(Color.Red));

            //3rd cow is placed next to the above cows, no mill forms for either place
            p1.Place(2, board, referee, Phase.Placing);
            Assert.False(board.areNewMills(Color.Red));
            Assert.False(board.areNewMills(Color.Blue));
        }

        // // THIS IS BRIAN's PROPERTY SO GITTTT OFF MY PROPERTY
        // [Test]
        // public void MillIsInvalidIfCowsNotInConnectedLine ()
        // {
        //     //TODO: Check that the cows that will form a mill are in a line and connected in spaces

        //     Board
        //         b = new Board(), //Check mill forms if not in straight line
        //         c = new Board(), //Check if mill forms if in straight line but one or two cows has different ID's
        //         d = new Board(); //Check if 3 cows with the same ID's in a line form a mill

        //     //Oblique line
        //     b.Place(0, 0);
        //     b.Place(0, 1);
        //     b.Place(0, 3);

        //     //In a line, but different cow
        //     c.Place(0, 0);
        //     c.Place(0, 1);
        //     c.Place(1, 2);

        //     //In a line
        //     d.Place(0, 0);
        //     d.Place(0, 1);
        //     d.Place(0, 2);

        //     //Check for any new mills
        //     b.UpdateMills();
        //     c.UpdateMills();
        //     d.UpdateMills();

        //     Mill[] b_mills = b.Mills.Where(x => x.Id == 0).ToArray();
        //     Mill[] c_mills = c.Mills.Where(x => x.Id == 0).ToArray();
        //     Mill[] d_mills = d.Mills.Where(x => x.Id == 0).ToArray();

        //     Assert.That(b_mills.Length == 0);
        //     Assert.That(c_mills.Length == 0);
        //     Assert.That(d_mills.Length == 1);
        // }

        // [Test]
        // public void KillingFromAMillHappensOnce ()
        // {
        //     //TODO: When a mill is formed, it can only kill a cow once in the same turn it is formed and not again after if it continues to exist

        //     GameSession g = new GameSession();

        //     g.Play(0); // Player 1 to A1
        //     g.Play(21); // Player 2 to G1
        //     g.Play(1); // Player 1 to A4
        //     g.Play(22); // Player 2 to G4
        //     g.Play(2); // Player 1 to A7 - mill is formed

        //     g.Play(22); // Kill cow at G4
        //     g.Play(21); // Try kill cow at G1 (Second kill)

        //     Assert.That(g.board.Cows[22].PlayerID == -1); //Assert that the first cow is indeed dead
        //     Assert.That(g.board.Cows[21].PlayerID == 1); //Assert that the second cow is still there and belongs to Player 2
        // }

        // [Test]
        // public void MillCowIsSafeIfNonMillCowsExist ()
        // {
        //     //TODO: You cannot kill a cow in a mill if there exists cows that are not in a mill
        //     GameSession g = new GameSession();

        //     g.Play(0); // Player 1 to A1
        //     g.Play(21); // Player 2 to G1
        //     g.Play(1); // Player 1 to A4
        //     g.Play(22); // Player 2 to G4
        //     g.Play(2); // Player 1 to A7 - mill is formed (A1,A4,A7)
        //     g.Play(22); // Kill cow at G4
        //     g.Play(22); // Player 2 to G4
        //     g.Play(3); // Player 1 to B2

        //     //We now have player one with 4 cows: 3 in a mill and one loose, so we can try see which cows we can kill

        //     // Try kill Player 1's cows A1 and B2 as Player 2 
        //     Assert.False(g.board.CanKillAt(1, 0)); //Assert that you can't kill cow at A1 since its in a mill and safe since B2 is not in a mill            
        //     Assert.True(g.board.CanKillAt(1, 3)); //Assert that you can kill cow at B2 since it is not in a mill            
        // }

        // [Test]
        // public void CanKillMillCowIfAllCowsInMills ()
        // {
        //     //TODO: You can kill a cow in a mill if all cows are in mills

        //     GameSession g = new GameSession();

        //     g.Play(0); // Player 1 to A1
        //     g.Play(21); // Player 2 to G1
        //     g.Play(1); // Player 1 to A4
        //     g.Play(22); // Player 2 to G4
        //     g.Play(2); // Player 1 to A7 - first mill is formed (A1,A4,A7)
        //     g.Play(22); // Kill cow at G4
        //     g.Play(22); // Player 2 to G4
        //     g.Play(3); // Player 1 to B2
        //     g.Play(18); // Player 2 to F2
        //     g.Play(4); // Player 1 to B4
        //     g.Play(19); // Player 2 to F4
        //     g.Play(5); // Player 1 to B6 - second mill is formed (B2,B4,B6)

        //     //We now have player one with 6 cows with 3 in each mill

        //     //Try kill Player 1's cows A1 and B2 as Player 2
        //     Assert.True(g.board.CanKillAt(1, 0)); //Assert that you can kill cow at A1 since all player 1's cows in a mill            
        //     Assert.True(g.board.CanKillAt(1, 3)); //Assert that you can kill cow at B2 since all player 1's cows in a mill
        // }

        // [Test]
        // public void PlayerCannotShootOwnCows ()
        // {
        //     //TODO: You cannot kill your own cows when you are about to kill
        //     GameSession g = new GameSession();

        //     g.Play(0); // Player 1 to A1
        //     g.Play(21); // Player 2 to G1

        //     //Try kill Player 1's cows A1 and B2 as Player 1
        //     Assert.False(g.board.CanKillAt(0, 0)); //Assert that you can't kill cow at A1 since it belongs to player 1        
        // }

        // [Test]
        // public void PlayerCannotShootEmptySpaces ()
        // {
        //     //TODO: You cannot shoot an empty space
        //     GameSession g = new GameSession();
        //     //Just try see if player 1 can kill any empty space (empty A1 in this case)
        //     Assert.False(g.board.CanKillAt(0, 0)); //Assert that you can't kill cow at A1 since it is empty
        // }

        // [Test]
        // public void CowsShotAreRemoved ()
        // {
        //     //TODO: A cow that has been killed must be removed from the board and replaced by an empty space
        //     GameSession g = new GameSession();

        //     g.Play(0); // Player 1 to A1
        //     g.Play(21); // Player 2 to G1
        //     g.Play(1); // Player 1 to A4
        //     g.Play(22); // Player 2 to G4
        //     g.Play(2); // Player 1 to A7 - first mill is formed (A1,A4,A7)
        //     g.Play(22); // Kill cow at G4

        //     //Player 2's cow at G4 is now dead and should now be an empty cow
        //     Assert.That(g.board.Cows[22].PlayerID == -1); //Assert that G4 is now an empty space
        // }

        // [Test]
        // public void WinIfPlayerCannotMove ()
        // {
        //     //TODO: Check win condition that if the player cannot move any of their cows, then the other player wins
        //     Assert.That(false);
        // }

        // [Test]
        // public void WinIfPlayerHasTwoCowsAndNotPlacing ()
        // {
        //     //TODO: Check win condition that if the player has 2 or fewer cows and is not placing, then the other player wins
        //     Assert.That(false);
        // }

        #endregion

    }
}
