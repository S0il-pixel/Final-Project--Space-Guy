namespace Tests
{
    public class Tests //My unit testing. I dunno if it works, but hey, it's here.
    {

        public class WantedBoardTests
        {
            [Fact]
            public void ChoosingValidCriminal_ShouldReturnCorrectCriminal()
            {
                // Arrange
                List<Criminal> availableCriminals = new List<Criminal>
                    {
                    new Criminal("Jimmy the Throat Slitter!", 500, 1),
                    new Criminal("Vael the Shadow Walker", 800, 2),
                    new Criminal("Zorak Bloodfang", 1200, 3),
                    new Criminal("Mafie guy: Al Capone!", 2000, 3)      //Testing if the selection works
                    };

                int userChoice = 2; // Simulate choosing "Vael the Shadow Walker"

                // Act
                Criminal chosenCriminal = availableCriminals[userChoice - 1];

                // Assert
                Assert.Equal("Vael the Shadow Walker", chosenCriminal.Name);
                Assert.Equal(800, chosenCriminal.Bounty);
                Assert.Equal(2, chosenCriminal.Difficulty);
            }

            [Fact]
            public void CapturingCriminal_ShouldUpdatePlayerStats()
            {
                // Arrange
                PlayerCharacter player = new PlayerCharacter();
                Criminal capturedCriminal = new Criminal("Zorak Bloodfang", 1200, 3);

                // Act
                player.CapturedCriminals.Add(capturedCriminal);
                player.Credits += capturedCriminal.Bounty;              //Testing to see if the criminal list has the criminal, and if the player has the credits

                // Assert
                Assert.Contains(capturedCriminal, player.CapturedCriminals);
                Assert.Equal(1200, player.Credits);
            }

            [Fact]
            public void ValidateMiniGameDifficulty_ShouldTriggerCorrectMethod()
            {
                // Arrange
                int difficulty = 2;
                string miniGameMessage = "";

                // Act
                switch (difficulty)
                {
                    case 1:
                        miniGameMessage = "Easy mini-game triggered!";
                        break;
                    case 2:
                        miniGameMessage = "Normal mini-game triggered!";            //Testing if the difficulty is the one the player chose
                        break;
                    case 3:
                        miniGameMessage = "Difficult mini-game triggered!";
                        break;
                    default:
                        miniGameMessage = "Error!";
                        break;
                }

                // Assert
                Assert.Equal("Normal mini-game triggered!", miniGameMessage);
            }

        }
    }
}