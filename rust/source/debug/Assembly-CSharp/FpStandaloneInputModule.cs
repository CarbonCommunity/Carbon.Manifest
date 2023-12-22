using UnityEngine.EventSystems;

public class FpStandaloneInputModule : StandaloneInputModule
{
	public PointerEventData CurrentData => m_PointerData.ContainsKey (-1) ? m_PointerData [-1] : new PointerEventData (EventSystem.current);
}
