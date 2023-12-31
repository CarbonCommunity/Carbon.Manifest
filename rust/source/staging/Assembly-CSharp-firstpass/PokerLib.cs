using System;
using PokerEvaluator;

public static class PokerLib
{
	public enum PokerResult
	{
		RoyalFlush,
		StraightFlush,
		FourOfAKind,
		FullHouse,
		Flush,
		Straight,
		ThreeOfAKind,
		TwoPair,
		Pair,
		HighCard
	}

	public const int CLUB = 32768;

	public const int DIAMOND = 16384;

	public const int HEART = 8192;

	public const int SPADE = 4096;

	public static int FindIt (int key)
	{
		int num = 0;
		int num2 = 4887;
		while (num <= num2) {
			int num3 = num2 + num >> 1;
			if (key < Arrays.products [num3]) {
				num2 = num3 - 1;
				continue;
			}
			if (key > Arrays.products [num3]) {
				num = num3 + 1;
				continue;
			}
			return num3;
		}
		throw new ApplicationException ("ERROR:  no match found; key = " + key);
	}

	public static void InitDeck (int[] deck)
	{
		int num = 0;
		int num2 = 32768;
		int num3 = 0;
		while (num3 < 4) {
			int num4 = 0;
			while (num4 < 13) {
				deck [num] = Arrays.primes [num4] | (num4 << 8) | num2 | (1 << 16 + num4);
				num4++;
				num++;
			}
			num3++;
			num2 >>= 1;
		}
	}

	public static int FindCard (int rank, int suit, int[] deck)
	{
		for (int i = 0; i < 52; i++) {
			int num = deck [i];
			if ((num & suit) != 0 && Rank (num) == rank) {
				return i;
			}
		}
		return -1;
		static int Rank (int card)
		{
			return (card >> 8) & 0xF;
		}
	}

	public static ushort Eval5Cards (int c1, int c2, int c3, int c4, int c5)
	{
		int num = (c1 | c2 | c3 | c4 | c5) >> 16;
		if (((uint)(c1 & c2 & c3 & c4 & c5) & 0xF000u) != 0) {
			return Arrays.flushes [num];
		}
		ushort num2 = Arrays.unique5 [num];
		if (num2 != 0) {
			return num2;
		}
		num = (c1 & 0xFF) * (c2 & 0xFF) * (c3 & 0xFF) * (c4 & 0xFF) * (c5 & 0xFF);
		num = FindIt (num);
		return Arrays.values [num];
	}

	public static ushort Eval5Hand (int[] hand)
	{
		return Eval5Cards (hand [0], hand [1], hand [2], hand [3], hand [4]);
	}

	public static ushort Eval7Hand (int[] hand)
	{
		ushort num = ushort.MaxValue;
		int[] array = new int[5];
		for (int i = 0; i < 21; i++) {
			for (int j = 0; j < 5; j++) {
				array [j] = hand [Arrays.perm7 [i, j]];
			}
			ushort num2 = Eval5Hand (array);
			if (num2 < num) {
				num = num2;
			}
		}
		return num;
	}

	public static PokerResult EvalToResultName (ushort eval)
	{
		if (eval > 6185) {
			return PokerResult.HighCard;
		}
		if (eval > 3325) {
			return PokerResult.Pair;
		}
		if (eval > 2467) {
			return PokerResult.TwoPair;
		}
		if (eval > 1609) {
			return PokerResult.ThreeOfAKind;
		}
		if (eval > 1599) {
			return PokerResult.Straight;
		}
		if (eval > 322) {
			return PokerResult.Flush;
		}
		if (eval > 166) {
			return PokerResult.FullHouse;
		}
		if (eval > 10) {
			return PokerResult.FourOfAKind;
		}
		if (eval > 1) {
			return PokerResult.StraightFlush;
		}
		return PokerResult.RoyalFlush;
	}

	public static string HandToString (int[] hand)
	{
		string text = string.Empty;
		for (int i = 0; i < hand.Length; i++) {
			int index = (hand [i] >> 8) & 0xF;
			text = string.Concat (str2: ((hand [i] & 0x8000) != 32768) ? (((hand [i] & 0x4000) != 16384) ? (((hand [i] & 0x2000) != 8192) ? "♠" : "♥") : "♦") : "♣", str0: text, str1: "23456789TJQKA" [index].ToString (), str3: " ");
		}
		return text;
	}
}
