namespace SoccerInfo.Domain.Models.PlayerCharacteristicsAggregate;

public static class PlayerExtensions
{
	public static void UpdateGeneralCharacteristics(this Player player, float? height, string? leadingFoot, DateTime? clubJoinDate, DateTime? contractExpirationDate)
	{
		if (player.Characteristics == null)
			player.Characteristics = new PlayerCharacteristic();

		player.Characteristics.Height = height;
		player.Characteristics.LeadingFoot = leadingFoot;
		player.Characteristics.ClubJoinDate = clubJoinDate;
		player.Characteristics.ContractExpirationDate = contractExpirationDate;
	}

	public static void UpdateBirthPlace(this Player player, string city, string country)
	{
		if (player.Characteristics == null)
			player.Characteristics = new PlayerCharacteristic();

		if (player.Characteristics.BrithPlace == null)
			player.Characteristics.BrithPlace = new BrithPlace();

		player.Characteristics.BrithPlace.City = city;
		player.Characteristics.BrithPlace.Country = country;
	}


	public static void UpdateNationalTeam(this Player player, string name, string country, int caps, int goals)
	{
		if (player.Characteristics == null)
			player.Characteristics = new PlayerCharacteristic();

		if (player.Characteristics.NationalTeam == null)
			player.Characteristics.NationalTeam = new NationalTeam();

		player.Characteristics.NationalTeam.Name = name;
		player.Characteristics.NationalTeam.Caps = caps;
		player.Characteristics.NationalTeam.Goals = goals;
		player.Characteristics.NationalTeam.Country = country;
	}

	public static void UpdateSocials(this Player player, IEnumerable<SocialMedia> socials)
	{
		if (player.Characteristics == null)
			player.Characteristics = new PlayerCharacteristic();

		if (player.Characteristics.Socials == null)
			player.Characteristics.Socials = new List<SocialMedia>();

        foreach (var socialMedia in socials)
		{
            var existingSocialMedia = player.Characteristics.Socials.SingleOrDefault(x => x.Equals(socialMedia));	
			
			if (existingSocialMedia != null)
                existingSocialMedia.Link = socialMedia.Link;
			else
                player.Characteristics.Socials.Add(socialMedia);
        }
	}

    public static bool ContainsOutfieldPlayerStats(this Player player)
    {
        if (player.Characteristics?.OutfieldPlayerStats != null && player.Characteristics.OutfieldPlayerStats.Any())
            return true;
        else
            return false;
    }

    public static bool ContainsGoalKeeperStats(this Player player)
	{
        if (player.Characteristics?.GoalKeeperStats != null && player.Characteristics.GoalKeeperStats.Any())
			return true;
		else 
			return false;
    }


    public static void UpdateStats<T>(this Player player, T stats) where T : StatsBase, new()
	{
		if (stats == null)
			throw new Exception("stats are null");

		if (player.Position == "Goalkeeper")
			player.UpdateGoalKeeperStats((stats as GoalKeeperStats)!);
		else
            player.UpdateOutfieldPlayerStats((stats as OutfieldPlayerStats)!);

    }

    private static void UpdateOutfieldPlayerStats(this Player player, OutfieldPlayerStats stats)
    {
        if (player.Characteristics == null)
            player.Characteristics = new PlayerCharacteristic();

        if (player.Characteristics.OutfieldPlayerStats == null)
            player.Characteristics.OutfieldPlayerStats = new HashSet<OutfieldPlayerStats>();

		var existingStats = player.Characteristics.OutfieldPlayerStats.SingleOrDefault(x => x.Equals(stats));

		if (existingStats == null)
            player.Characteristics.OutfieldPlayerStats.Add(stats);
		else
			existingStats.Update(stats);
    }

    private static void UpdateGoalKeeperStats(this Player player, GoalKeeperStats stats)
    {
        if (player.Characteristics == null)
            player.Characteristics = new PlayerCharacteristic();

        if (player.Characteristics.GoalKeeperStats == null)
            player.Characteristics.GoalKeeperStats = new HashSet<GoalKeeperStats>();

        var existingStats = player.Characteristics.GoalKeeperStats.SingleOrDefault(x => x.Equals(stats));

        if (existingStats == null)
            player.Characteristics.GoalKeeperStats.Add(stats);
        else
            existingStats.Update(stats);
    }
}
