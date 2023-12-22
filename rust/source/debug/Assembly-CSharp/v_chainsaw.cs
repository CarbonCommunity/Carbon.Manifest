using UnityEngine;

public class v_chainsaw : MonoBehaviour
{
	public bool bAttacking;

	public bool bHitMetal;

	public bool bHitWood;

	public bool bHitFlesh;

	public bool bEngineOn;

	public ParticleSystem[] hitMetalFX;

	public ParticleSystem[] hitWoodFX;

	public ParticleSystem[] hitFleshFX;

	public SoundDefinition hitMetalSoundDef;

	public SoundDefinition hitWoodSoundDef;

	public SoundDefinition hitFleshSoundDef;

	public Sound hitSound;

	public GameObject hitSoundTarget;

	public float hitSoundFadeTime = 0.1f;

	public ParticleSystem smokeEffect;

	public Animator chainsawAnimator;

	public Renderer chainRenderer;

	public Material chainlink;

	private MaterialPropertyBlock block;

	private Vector2 saveST;

	private float chainSpeed = 0f;

	private float chainAmount = 0f;

	public float temp1;

	public float temp2;

	public void OnEnable ()
	{
		if (block == null) {
			block = new MaterialPropertyBlock ();
		}
		saveST = chainRenderer.sharedMaterial.GetVector ("_MainTex_ST");
	}

	private void Awake ()
	{
		chainlink = chainRenderer.sharedMaterial;
	}

	private void Start ()
	{
	}

	private void ScrollChainTexture ()
	{
		float z = (chainAmount = (chainAmount + Time.deltaTime * chainSpeed) % 1f);
		block.Clear ();
		block.SetVector ("_MainTex_ST", new Vector4 (saveST.x, saveST.y, z, 0f));
		chainRenderer.SetPropertyBlock (block);
	}

	private void Update ()
	{
		chainsawAnimator.SetBool ("attacking", bAttacking);
		smokeEffect.enableEmission = bEngineOn;
		if (bHitMetal) {
			chainsawAnimator.SetBool ("attackHit", value: true);
			ParticleSystem[] array = hitMetalFX;
			foreach (ParticleSystem particleSystem in array) {
				particleSystem.enableEmission = true;
			}
			ParticleSystem[] array2 = hitWoodFX;
			foreach (ParticleSystem particleSystem2 in array2) {
				particleSystem2.enableEmission = false;
			}
			ParticleSystem[] array3 = hitFleshFX;
			foreach (ParticleSystem particleSystem3 in array3) {
				particleSystem3.enableEmission = false;
			}
			DoHitSound (hitMetalSoundDef);
		} else if (bHitWood) {
			chainsawAnimator.SetBool ("attackHit", value: true);
			ParticleSystem[] array4 = hitMetalFX;
			foreach (ParticleSystem particleSystem4 in array4) {
				particleSystem4.enableEmission = false;
			}
			ParticleSystem[] array5 = hitWoodFX;
			foreach (ParticleSystem particleSystem5 in array5) {
				particleSystem5.enableEmission = true;
			}
			ParticleSystem[] array6 = hitFleshFX;
			foreach (ParticleSystem particleSystem6 in array6) {
				particleSystem6.enableEmission = false;
			}
			DoHitSound (hitWoodSoundDef);
		} else if (bHitFlesh) {
			chainsawAnimator.SetBool ("attackHit", value: true);
			ParticleSystem[] array7 = hitMetalFX;
			foreach (ParticleSystem particleSystem7 in array7) {
				particleSystem7.enableEmission = false;
			}
			ParticleSystem[] array8 = hitWoodFX;
			foreach (ParticleSystem particleSystem8 in array8) {
				particleSystem8.enableEmission = false;
			}
			ParticleSystem[] array9 = hitFleshFX;
			foreach (ParticleSystem particleSystem9 in array9) {
				particleSystem9.enableEmission = true;
			}
			DoHitSound (hitFleshSoundDef);
		} else {
			chainsawAnimator.SetBool ("attackHit", value: false);
			ParticleSystem[] array10 = hitMetalFX;
			foreach (ParticleSystem particleSystem10 in array10) {
				particleSystem10.enableEmission = false;
			}
			ParticleSystem[] array11 = hitWoodFX;
			foreach (ParticleSystem particleSystem11 in array11) {
				particleSystem11.enableEmission = false;
			}
			ParticleSystem[] array12 = hitFleshFX;
			foreach (ParticleSystem particleSystem12 in array12) {
				particleSystem12.enableEmission = false;
			}
		}
	}

	private void DoHitSound (SoundDefinition soundDef)
	{
	}
}
