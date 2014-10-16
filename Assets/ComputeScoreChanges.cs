using UnityEngine;
using System.Collections;

namespace BESTScoring
{

public static class ComputeScoreChanges
{
	public static int RedTeamScore(int redTeamBlades, int blueTeamBlades, bool useRedTower)
	{
		int RedScore = 0;
		if (useRedTower)
		{
			//score if using red tower goes here.
			if (redTeamBlades == 3)
				RedScore = 160 + 30;
			else if (redTeamBlades == 2)
				RedScore = 200 + 20;
			else if (redTeamBlades == 1)
				RedScore = 180 + 10;
			else if (redTeamBlades == 0) 
				RedScore = 80 + 0;
		} 
		else
		{
			//score if using blue tower goes here.
			if (redTeamBlades == 3)
				RedScore = 160 + 30;
			else if (redTeamBlades == 2)
				RedScore = 100 + 20;
			else if (redTeamBlades == 1)
				RedScore = 40 + 10;
			else if (redTeamBlades == 0) 
				RedScore = 0;
		 }

		return RedScore;
	}

	public static int BlueTeamScore(int redTeamBlades, int blueTeamBlades, bool useRedTower)
	{
		int BlueScore = 5;
		if (useRedTower)
		{
			//score if using red tower goes here.
			if (redTeamBlades == 3)
				BlueScore = 0;
			else if (redTeamBlades == 2)
				BlueScore = 120;
			else if (redTeamBlades == 1)
				BlueScore = 140;
			else if (redTeamBlades == 0) 
				BlueScore = 80;
		} 
		else
		{ 
			//score if using blue tower goes here.
			BlueScore = 0;

		}

		return BlueScore;
	}
}

}
