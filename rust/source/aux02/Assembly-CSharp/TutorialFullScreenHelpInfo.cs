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

	public static Translate.Phrase MovementPhrase = new Translate.Phrase ("help_cat_movement", "MOVEMENT");

	public static Translate.Phrase CraftingPhrase = new Translate.Phrase ("help_cat_crafting", "CRAFTING");

	public static Translate.Phrase CombatPhrase = new Translate.Phrase ("help_cat_combat", "COMBAT");

	public static Translate.Phrase BuildingPhrase = new Translate.Phrase ("help_cat_building", "BUILDING");

	public static Dictionary<MenuCategory, Translate.Phrase> CategoryPhraseLookup = new Dictionary<MenuCategory, Translate.Phrase> {
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
