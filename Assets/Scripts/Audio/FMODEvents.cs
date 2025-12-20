using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{

    [field: Header("Walk Player SFX")]
    [field: SerializeField] public EventReference walkPlayer {  get; private set; }

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
