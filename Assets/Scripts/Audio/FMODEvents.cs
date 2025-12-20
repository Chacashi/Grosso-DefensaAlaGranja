using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Ambience")]
    [field: SerializeField] public EventReference fightTime { get; private set; }

    [field: Header("Walk Player SFX")]
    [field: SerializeField] public EventReference walkPlayer {  get; private set; }
    [field: SerializeField] public EventReference cowSound { get; private set; }

    public static FMODEvents Instance {  get; private set; }

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.Log("Mal ps pappai");
        }

        Instance = this;

    }
}
