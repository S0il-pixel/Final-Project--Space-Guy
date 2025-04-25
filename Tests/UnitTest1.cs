namespace Tests
{
    public class Tests
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
                    new Criminal("Mafie guy: Al Capone!", 2000, 3)
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
                player.Credits += capturedCriminal.Bounty;

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
                        miniGameMessage = "Normal mini-game triggered!";
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

            [Fact]
            public void ValidChoice_ShouldInvokeCorrectAction()
            {
                // Arrange
                var player = new PlayerCharacter();
                var system = new MainMenuSystem();
                string simulatedInput = "shop";
                string capturedOutput = string.Empty;

                Func<string> inputProvider = () => simulatedInput;
                Action<string> outputProvider = output => capturedOutput = output;

                // Act
                system.MainMenu(player, inputProvider, outputProvider);

                // Assert
                Assert.Contains("Hunger:", capturedOutput);
                Assert.Contains("Fuel:", capturedOutput);
            }

            [Fact]
            public void InvalidChoice_ShouldProvideErrorMessage()
            {
                // Arrange
                var player = new PlayerCharacter();
                var system = new MainMenuSystem();
                string simulatedInput = "invalid";
                string capturedOutput = string.Empty;

                Func<string> inputProvider = () => simulatedInput;
                Action<string> outputProvider = output => capturedOutput = output;

                // Act
                system.MainMenu(player, inputProvider, outputProvider);

                // Assert
                Assert.Contains("Invalid choice.", capturedOutput);
            }
        }

        public class PlanetExplorerTests
        {
                [Fact]
                public void PlanetAttributes_ShouldBeRetrievedCorrectly()
                {
                    // Arrange
                    var planetTypes = Assembly.GetExecutingAssembly().GetTypes()
                        .Where(t => t.GetCustomAttributes<PlanetAttribute>().Any());

                    // Act & Assert
                    foreach (var type in planetTypes)
                    {
                        // Get the attribute
                        var attribute = type.GetCustomAttribute<PlanetAttribute>();
                        Assert.NotNull(attribute);

                        // Validate attribute properties
                        Assert.NotNull(attribute.TerrainType);
                        Assert.True(attribute.DangerLevel > 0);
                        Assert.NotNull(attribute.UniqueResource);
                        Assert.NotEmpty(attribute.Criminals);
                        Assert.NotEmpty(attribute.ShopItems);

                        var nameProperty = type.GetProperty("Name");
                        var descriptionProperty = type.GetProperty("Description");

                        Assert.NotNull(nameProperty);
                        Assert.NotNull(descriptionProperty);
                    }
                }

                [Fact]
                public void SpecificPlanetData_ShouldMatchExpectedValues()
                {
                    // Arrange
                    var planetType = typeof(TerraPrime);
                    var attribute = planetType.GetCustomAttribute<PlanetAttribute>();
                    var instance = Activator.CreateInstance(planetType);

                    // Act
                    var name = planetType.GetProperty("Name")?.GetValue(instance)?.ToString();
                    var description = planetType.GetProperty("Description")?.GetValue(instance)?.ToString();

                    // Assert
                    Assert.Equal("Terra Prime", name);
                    Assert.Equal("A rocky planet rich in rare minerals and suitable for exploration.", description);

                    Assert.Equal("Rocky", attribute.TerrainType);
                    Assert.Equal(1, attribute.DangerLevel);
                    Assert.Equal("Rare Minerals", attribute.UniqueResource);
                    Assert.Contains("Jimmy the Throat Slitter", attribute.Criminals);
                    Assert.Contains("Basic Mining Laser", attribute.ShopItems);
                }
            
        }
    }
}