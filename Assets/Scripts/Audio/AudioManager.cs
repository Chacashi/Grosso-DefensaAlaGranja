using FMODUnity;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    
    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance != null)
        {

            Debug.Log("Error ps manin");
        }
        Instance = this;

       
    }

    public void PlayOneShoot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }
    public EventInstance CreateEventInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        return eventInstance;
    }
}
