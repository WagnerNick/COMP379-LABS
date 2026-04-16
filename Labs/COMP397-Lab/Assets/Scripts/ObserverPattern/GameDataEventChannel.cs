using UnityEngine;

[CreateAssetMenu(menuName = "Events/GameData Event")]
public class GameDataEventChannel : ScriptableObject
{
    public UnityEngine.Events.UnityAction<GameData> OnEventRaised;

    public void RaiseEvent(GameData value)
    {
        if (OnEventRaised == null) return;
        OnEventRaised.Invoke(value);
    }
}