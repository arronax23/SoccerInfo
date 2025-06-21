using SoccerInfo.Domain.Repositories;

namespace SoccerInfo.Domain.Models.GeneralPosition;
public class GeneralPositionService(IGeneralPositionLookupRepository repository)
{
    public async Task AttachGeneralPosition(Player player)
    {
        if (player is null || player.Position is null) 
            throw new Exception("Not valid player");

        if (player.Position == "Goalkeeper")
            player.GeneralPosition = await repository.Get(GeneralPosition.Goalkeeper);
        else if (player.Position == "Centre-Back" || player.Position == "Right-Back" || player.Position == "Left-Back")
            player.GeneralPosition = await repository.Get(GeneralPosition.Defender);
        else if (player.Position == "Attacking Midfield" ||
                 player.Position == "Central Midfield" ||
                 player.Position == "Defensive Midfield" ||
                 player.Position == "Left Midfield" ||
                 player.Position == "Right Midfield" ||
                 player.Position == "Midfielder" ||
                 player.Position == "Right Winger" ||
                 player.Position == "Left Winger"
            )
            player.GeneralPosition = await repository.Get(GeneralPosition.Midfielder);
        else if (player.Position == "Centre-Forward" || player.Position == "Striker" || player.Position == "Second Striker")
            player.GeneralPosition = await repository.Get(GeneralPosition.Forward);
        else
            throw new Exception("Not valid position");
    }
}
