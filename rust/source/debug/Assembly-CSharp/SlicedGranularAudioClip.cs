using System.Collections.Generic;
using UnityEngine;

public class SlicedGranularAudioClip : MonoBehaviour, IClientComponent
{
	public class Grain
	{
		private float[] sourceData;

		private int startSample = 0;

		private int currentSample = 0;

		private int attackTimeSamples = 0;

		private int sustainTimeSamples = 0;

		private int releaseTimeSamples = 0;

		private float gain = 0f;

		private float gainPerSampleAttack = 0f;

		private float gainPerSampleRelease = 0f;

		private int attackEndSample = 0;

		private int releaseStartSample = 0;

		private int endSample = 0;

		public bool finished => currentSample >= endSample;

		public void Init (float[] source, int start, int attack, int sustain, int release)
		{
			sourceData = source;
			startSample = start;
			currentSample = start;
			attackTimeSamples = attack;
			sustainTimeSamples = sustain;
			releaseTimeSamples = release;
			gainPerSampleAttack = 0.5f / (float)attackTimeSamples;
			gainPerSampleRelease = -0.5f / (float)releaseTimeSamples;
			attackEndSample = startSample + attackTimeSamples;
			releaseStartSample = attackEndSample + sustainTimeSamples;
			endSample = releaseStartSample + releaseTimeSamples;
			gain = 0f;
		}

		public float GetSample ()
		{
			if (currentSample >= sourceData.Length) {
				return 0f;
			}
			float num = sourceData [currentSample];
			if (currentSample <= attackEndSample) {
				gain += gainPerSampleAttack;
				if (gain > 0.5f) {
					gain = 0.5f;
				}
			} else if (currentSample >= releaseStartSample) {
				gain += gainPerSampleRelease;
				if (gain < 0f) {
					gain = 0f;
				}
			}
			currentSample++;
			return num * gain;
		}

		public void FadeOut ()
		{
			releaseStartSample = currentSample;
			endSample = releaseStartSample + releaseTimeSamples;
		}
	}

	public AudioClip sourceClip;

	public AudioClip granularClip;

	public int sampleRate = 44100;

	public float grainAttack = 0.1f;

	public float grainSustain = 0.1f;

	public float grainRelease = 0.1f;

	public float grainFrequency = 0.1f;

	public int grainAttackSamples = 0;

	public int grainSustainSamples = 0;

	public int grainReleaseSamples = 0;

	public int grainFrequencySamples = 0;

	public int samplesUntilNextGrain = 0;

	public List<Grain> grains = new List<Grain> ();

	public List<int> startPositions = new List<int> ();

	public int lastStartPositionIdx = int.MaxValue;
}
