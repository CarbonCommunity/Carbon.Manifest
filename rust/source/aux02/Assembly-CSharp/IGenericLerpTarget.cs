using System.Collections.Generic;
using Rust.Interpolation;

public interface IGenericLerpTarget<T> where T : ISnapshot<T>, new()
{
	float GetInterpolationDelay ();

	float GetInterpolationSmoothing ();

	void SetFrom (T snapshot);

	T GetCurrentState ();

	void DebugInterpolationState (Interpolator<T>.Segment segment, List<T> entries);
}
