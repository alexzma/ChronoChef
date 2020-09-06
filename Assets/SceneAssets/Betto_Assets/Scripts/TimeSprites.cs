using UnityEngine;

[CreateAssetMenu]
// Contains the sprites for each of the time versions of an object.
public class TimePrefabs : ScriptableObject
{
    // Prefabs in the order of past to future.
    // That is, prefabs[0] is the oldest sprite (in terms of timeline) while prefabs[prefabs.length-1] is the most future prefab
    public Transform[] prefabs;
}
