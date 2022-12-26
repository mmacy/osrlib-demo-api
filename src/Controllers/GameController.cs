using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using osrlib.Dice;
using osrlib.Core;

namespace OSRlibDemoAPI.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class GameController : ControllerBase
{
    private readonly ILogger<GameController> _logger;

    public GameController(ILogger<GameController> logger)
    {
        _logger = logger;
    }

    [HttpGet()]
    public int RollDice(int numDice, DieType numSides)
    {
        DiceHand dice = new DiceHand(numDice, numSides);

        return DiceRoll.RollDice(dice);
    }

    [HttpGet()]
    public Being RollCharacter(string name)
    {
        DiceHand d20 = new DiceHand(1, DieType.d20);
        DiceRoll roll = new DiceRoll(d20);

        Being character = new Being
        {
            Name = name,
            Defense = roll.RollDice(),
            MaxHitPoints = roll.RollDice() + 10
        };
        character.HitPoints = character.MaxHitPoints;
        character.RollAbilities();

        _logger.LogInformation("Rolled new character: {character}", character);

        return character;
    }
}
