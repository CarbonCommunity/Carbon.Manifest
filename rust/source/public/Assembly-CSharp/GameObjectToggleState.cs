using UnityEngine;

public class GameObjectToggleState : StateMachineBehaviour
{
	[MinMax (0f, 1f)]
	[Tooltip ("THe normalised range int he animation in which to apply the TargetState")]
	public Vector2 ValidNormalisedRange;

	[Tooltip ("What state to set the target object to, true = enabled, false = disabled")]
	public bool TargetState;

	[Tooltip ("What gameObject to toggle (ensure it's a unique name in the hierarchy)")]
	public string GameObjectName;

	public override void OnStateUpdate (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		base.OnStateUpdate (animator, stateInfo, layerIndex);
		bool flag = stateInfo.normalizedTime > ValidNormalisedRange.x && stateInfo.normalizedTime < ValidNormalisedRange.y;
		Model model = animator.gameObject.GetComponent<Model> ();
		if (model == null) {
			model = animator.gameObject.GetComponentInParent<Model> ();
		}
		if (model != null) {
			Transform transform = model.FindBone (GameObjectName);
			if (transform != null) {
				transform.gameObject.SetActive (flag ? TargetState : (!TargetState));
			}
		}
	}
}
