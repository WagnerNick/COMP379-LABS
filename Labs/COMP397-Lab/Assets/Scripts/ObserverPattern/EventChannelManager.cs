using UnityEngine;

public class EventChannelManager : PersistentSingleton<EventChannelManager>
{
    public VoidEventChannel voidEvent;
    public FloatEventChannel floatEvent;
    public GameDataEventChannel gameDataEvent;
}