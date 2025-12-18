using UnityEngine;
using FMODUnity;

public class AudioManager : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputReader inputReader;

    [Header("Sonidos FMOD")]
    [SerializeField] private EventReference sfxMove;



    private void OnEnable()
    {
        inputReader.MovementEvent += PlayMoveSound;
    }

    private void OnDisable()
    {
        inputReader.MovementEvent -= PlayMoveSound;
    }

    private void PlayMoveSound( Vector2 value)
    {
        if(value.magnitude > 0)
        RuntimeManager.PlayOneShot(sfxMove);
    }
}
