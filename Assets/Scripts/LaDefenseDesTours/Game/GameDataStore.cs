﻿using System.Collections.Generic;
using Assets.Scripts.Core.Data;
using UnityEngine;

namespace Assets.Scripts.LaDefenseDesTours.Game
{
	/// <summary>
	/// The data store for TD
	/// </summary>
	public sealed class GameDataStore : GameDataStoreBase
	{
		/// <summary>
		/// A list of level IDs for completed levels
		/// </summary>
		//public List<LevelSaveData> completedLevels = new List<LevelSaveData>();

		/// <summary>
		/// Outputs to debug
		/// </summary>
		public override void PreSave()
		{
			Debug.Log("[GAME] Saving Game");
		}

		/// <summary>
		/// Outputs to debug
		/// </summary>
		public override void PostLoad()
		{
			Debug.Log("[GAME] Loaded Game");
		}

		/// <summary>
		/// Marks a level complete
		/// </summary>
		/// <param name="levelId">The levelId to mark as complete</param>
		/// <param name="starsEarned">Stars earned</param>
		public void CompleteLevel(string levelId, int starsEarned)
		{

		}

		/// <summary>
		/// Determines if a specific level is completed
		/// </summary>
		/// <param name="levelId">The level ID to check</param>
		/// <returns>true if the level is completed</returns>
		public bool IsLevelCompleted(string levelId)
		{

			return false;
		}

		/// <summary>
		/// Retrieves the star count for a given level
		/// </summary>
		public int GetNumberOfStarForLevel(string levelId)
		{
			return 0;
		}
	}
}