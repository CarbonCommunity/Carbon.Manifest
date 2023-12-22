using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu (menuName = "Rust/Tutorials/Full Screen Help Info")]
public class TutorialFullScreenHelpInfo : ScriptableObject
{
	public enum MenuCategory
	{
		Movement,
		Crafting,
		Combat,
		Building
	}

	[Serializable]
	public struct Info
	{
		public TokenisedPhrase TextToDisplay;

		public Sprite StaticImage;

		public VideoClip VideoClip;
	}

	public static Phrase MovementPhrase = new Phrase ("help_cat_movement", "MOVEMENT");

	public static Phrase CraftingPhrase = new Phrase ("help_cat_crafting", "CRAFTING");

	public static Phrase CombatPhrase = new Phrase ("help_cat_combat", "COMBAT");

	public static Phrase BuildingPhrase = new Phrase ("help_cat_building", "BUILDING");

	public static Dictionary<MenuCategory, Phrase> CategoryPhraseLookup = new Dictionary<MenuCategory, Phrase> {
		{
			MenuCategory.Movement,
			MovementPhrase
		},
		{
			MenuCategory.Crafting,
			CraftingPhrase
		},
		{
			MenuCategory.Combat,
			CombatPhrase
		},
		{
			MenuCategory.Building,
			BuildingPhrase
		}
	};

	public Info ToDisplay;

	public MenuCategory Category;
}
