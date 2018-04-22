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
        public void BoardIsEmptyWhenGameStarts()
        {
            IBoard b = new Board();
            ICow[] c = b.Cows.Where(x => x.Color == Color.Black).ToArray();
            Assert.That(c.Length == 24);
        }

        [Test]
        public void PlayerWithRedCowsGoesFirst()
        {
            //TODO: Make sure that the the player who goes first has the dark cows   
            IGameSession gameSession = GameSessionFactory.CreateGameSession();
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
            b.Occupant(Arg.Any<int>()).Returns(new Cow(-1, c));
            ICowBox box = new CowBox();
            IReferee r = new Referee(b, box);
            IPlayer p1 = new Player(Color.Red, box);

            p1.Place(pos, b, r, Phase.Placing);

            //Cow was placed at empty position
            if (c == Color.Black)
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
            for (int i = 0; i < 12; i++)
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

            IPlayer p1 = new Player(Color.Red, box);

            IReferee r = new Referee(b, box);

            for (int i = 0; i < 23; i++)
            {
                p1.Place(i, b, r, Phase.Placing);
                int[] moves = b.ConnectedSpaces(i);

                foreach (int m in moves)
                {
                    Assert.False(p1.Move(i, m, b, r, Phase.Placing));
                }
            }
        }

        #endregion

        #region TESTS: Moving Phase


        [Test]
        public void CowCanOnlyMoveToConnectedSpace()
        {
            int[] allMoves = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23 };

            for (int i = 0; i < 24; i++)
            {
                IBoard b = new Board();
                ICowBox box = Substitute.For<ICowBox>();
                box.TakeCow(Arg.Any<Color>()).Returns(new Cow(-1, Color.Red));

                IReferee r = new Referee(b, box);

                b.Place(new Cow(-1, Color.Red), i);

                int[] ValidMoves = b.ConnectedSpaces(i);
                int[] inValidMoves = allMoves.Where(x => !ValidMoves.Contains(x)).ToArray();

                foreach (int a in ValidMoves)
                {
                    Assert.True(r.CanMove(Color.Red, i, a, Phase.Moving));
                }
                foreach (int z in inValidMoves)
                {
                    Assert.False(r.CanMove(Color.Red, i, z, Phase.Moving));
                }

            }
        }

        static object[] MoveToPositionsOfOccupiedandNotOccupiuedSpaces = new object[]
        {
                new object[] {0 ,new int[] { 1, 3, 9 },       Color.Black},
                new object[] {1 ,new int[] { 0, 2, 4 },       Color.Black},
                new object[] {2 ,new int[] { 1, 5, 14 },      Color.Black},
                new object[] {3 ,new int[] { 0, 4, 6, 10 },   Color.Black},
                new object[] {4 ,new int[] { 1, 3, 5, 7 },    Color.Black},
                new object[] {5 ,new int[] { 2, 4, 8, 13 },   Color.Black},
                new object[] {6 ,new int[] { 3, 7, 11 },      Color.Black},
                new object[] {7 ,new int[] { 4, 6, 8 },       Color.Black},
                new object[] {8 ,new int[] { 5, 7, 12 },      Color.Black},
                new object[] {9 ,new int[] { 0, 10, 21 },     Color.Black},
                new object[] {10 ,new int[] { 3, 9, 11, 18 },  Color.Black},
                new object[] {11 ,new int[] { 6, 10, 15 },     Color.Red},
                new object[] {12 ,new int[] { 8, 13, 17 },     Color.Red},
                new object[] {13 ,new int[] { 5, 12, 14, 20 }, Color.Red},
                new object[] {14 ,new int[] { 2, 13, 23 },     Color.Red},
                new object[] {15 ,new int[] { 11, 16, 18 },    Color.Red},
                new object[] {16 ,new int[] { 15, 17, 19 },    Color.Red},
                new object[] {17 ,new int[] { 12, 16, 20 },    Color.Red},
                new object[] {18 ,new int[] { 10, 15, 19, 21 },Color.Red},
                new object[] {19 ,new int[] { 16, 18, 20, 22 },Color.Red},
                new object[] {20 ,new int[] { 13, 17, 19, 23 },Color.Red},
                new object[] {21 ,new int[] { 9, 18, 22 },     Color.Red},
                new object[] {22 ,new int[] { 19, 21, 23 },    Color.Red},
                new object[] {23 ,new int[] { 14, 20, 22 },    Color.Red}
        };

        [Test]
        [TestCaseSource(nameof(MoveToPositionsOfOccupiedandNotOccupiuedSpaces))]
        public static void OnlyMoveToEmptySpace(int pos, int[] moves, Color c)
        {
            IBoard board = Substitute.For<IBoard>();
            board.Occupant(Arg.Any<int>()).Returns(new Cow(-1, c));
            IPlayer P = Substitute.For<IPlayer>();
            ICowBox box = Substitute.For<ICowBox>();
            box.RemainingCows(Arg.Any<Color>()).Returns(12);
            IReferee r = new Referee(board, box);

            if (c == Color.Red)
            {
                foreach (int m1 in moves)
                {
                    Assert.False(r.CanMove(Color.Red, pos, m1, Phase.Moving));
                }

            }
            if (c == Color.Black)
            {
                foreach (int m2 in moves)
                {
                    Assert.True(r.CanMove(Color.Red, pos, m2, Phase.Moving));
                }

            }
        }

        static object[][] CowPositionsAndConnectedSpaces = new object[][]
        {
            new object[] { 0, new int[] {1,3,9} },
            new object[] { 1, new int[] {0,2,4} },
            new object[] { 2, new int[] {1,5,14} },
            new object[] { 3, new int[] {0,4,6,10} },
            new object[] { 4, new int[] {1,3,5,7} },
            new object[] { 5, new int[] {2,4,8,13} },
            new object[] { 6, new int[] {3,7,11} },
            new object[] { 7, new int[] {4,6,8} },
            new object[] { 8, new int[] {5,7,12} },
            new object[] { 9, new int[] {0,10,21} },
            new object[] { 10, new int[] {3,9,11,18} },
            new object[] { 11, new int[] {6,10,15} },
            new object[] { 12, new int[] {8,13,17} },
            new object[] { 13, new int[] {5,12,14,20} },
            new object[] { 14, new int[] {2,13,23} },
            new object[] { 15, new int[] {11,16,18} },
            new object[] { 16, new int[] {15,17,19} },
            new object[] { 17, new int[] {12,16,20} },
            new object[] { 18, new int[] {10,15,19,21} },
            new object[] { 19, new int[] {16,18,20,22} },
            new object[] { 20, new int[] {13,17,19,23} },
            new object[] { 21, new int[] {9,18,22} },
            new object[] { 22, new int[] {19,21,23} },
            new object[] { 23, new int[] {14,20,22} }
        };


        [Test]
        [TestCaseSource(nameof(CowPositionsAndConnectedSpaces))]
        public static void MoveCreatesNoDuplicates(int pos, int[] moves)
        {
            foreach (int move in moves)
            {
                IBoard b = new Board();
                ICowBox box = new CowBox();
                IReferee r = new Referee(b, box);

                b.Place(new Cow(-1, Color.Red), pos);
                b.Move(pos, move);

                ICow oldPosition = b.Occupant(pos);
                Assert.That(oldPosition.Color == Color.Black);
                Assert.That(b.numCowsOnBoard() == 1);
            }       
        }


        #endregion

        #region TESTS: Flying Phase

        static int[] Positions = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23 };

        [Test]
        
        public static void CanCowsFlyAtFlyPhase()
        {            for (int i = 0; i < 22; i++)
                for (int j = i + 1; j < 23; j++)
                    for (int k = j + 1; k < 24; k++)                    
                    {
                        IBoard b = new Board();
                        ICowBox box = new CowBox();
                        IReferee r = new Referee(b, box);
                        
                        b.Place(new Cow(-1, Color.Red), i);
                        b.Place(new Cow(-1, Color.Red), j);
                        b.Place(new Cow(-1, Color.Red), k);
                        
                        int[] EmptyPositions = Positions.Where(x => x != i && x != j && x != k).ToArray();

                        for (int l = 0; l < EmptyPositions.Length; l++)
                        {
                            Assert.True(r.CanFlyTo(Color.Red, EmptyPositions[l], Phase.Moving));
                            Assert.True(r.CanFlyTo(Color.Red, EmptyPositions[l], Phase.Moving));
                            Assert.True(r.CanFlyTo(Color.Red, EmptyPositions[l], Phase.Moving));
                        }
                    }              
        }
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

        // THIS IS BRIAN's PROPERTY SO GITTTT OFF MY PROPERTY

        IMill[] Mills = new Mill[] 
        {
            new Mill(new int[] { 0, 1, 2 }),        // A1, A4, A7
            new Mill(new int[] { 3, 4, 5 } ),       // B2, B4, B6
            new Mill(new int[] { 6, 7, 8 }),        // C3, C4, C5
            new Mill(new int[] { 9, 10, 11 }),      // D1, D2, D3
            new Mill(new int[] { 12, 13, 14 }),     // D5, D6, D7
            new Mill(new int[] { 15, 16, 17 }),     // E3, E4, E5
            new Mill(new int[] { 18, 19, 20 }),     // F2, F4, F6
            new Mill(new int[] { 21, 22, 23 }),     // G1, G4, G7
            new Mill(new int[] { 0, 9, 21 }),       // A1, D1, G1
            new Mill(new int[] { 3, 10, 18 }),      // B2, D2, F2
            new Mill(new int[] { 6, 11, 15 }),      // C3, D3, E3
            new Mill(new int[] { 1, 4, 7 }),        // A4, B4, C4
            new Mill(new int[] { 16, 19, 22 }),     // E4, F4, G4
            new Mill(new int[] { 8, 12, 17 }),      // C5, D5, E5
            new Mill(new int[] { 5, 13, 20 }),      // B6, D6, F6
            new Mill(new int[] { 2, 14, 23 }),      // A7, D7, G7
            new Mill(new int[] { 0, 3, 6 }),        // A1, B2, C3
            new Mill(new int[] { 15, 18, 21 }),     // E3, F2, G1
            new Mill(new int[] { 2, 5, 8 }),        // C5, B6, A7
            new Mill(new int[] { 17, 20, 23 })      // E5, F6, G7
        };

        [Test]        
        public void MillIsInvalidIfCowsNotInConnectedLine()
        {
            //TODO: Check that the cows that will form a mill are in a line and connected in spaces           

            for (int i = 0; i < 22; i++)
                for (int j = i + 1; j < 23; j++)
                    for (int k = j + 1; k < 24; k++)
                    {
                        IBoard b = new Board();
                        ICowBox box = new CowBox();
                        IReferee r = new Referee(b, box);  
                        
                        b.Place(new Cow(-1, Color.Red), i);
                        b.Place(new Cow(-1, Color.Red), j);
                        b.Place(new Cow(-1, Color.Red), k);

                        IMill[] AnyMills = Mills.Where(x => x.Positions[0] == i && x.Positions[1] == j && x.Positions[2] == k).ToArray();

                        if (AnyMills.Length == 0)
                            Assert.False(b.areNewMills(Color.Red));
                        else
                            Assert.True(b.areNewMills(Color.Red));
                        
                    }

        }

        [Test]
        public void KillingFromAMillHappensOnce()
        {
            //TODO: When a mill is formed, it can only kill a cow once in the same turn it is formed and not again after if it continues to exist

            
        }

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
