using UnityEngine;
using System.Collections;

namespace BESTScoring
{

public static class ComputeScoreChanges
{
	public static int RedTeamScore(int redTeamBlades, int blueTeamBlades, bool useRedTower)
	{
		return Random.Range(0,200);
	}

	public static int BlueTeamScore(int redTeamBlades, int blueTeamBlades, bool useRedTower)
	{
		return Random.Range(0,200);
	}
}

}
