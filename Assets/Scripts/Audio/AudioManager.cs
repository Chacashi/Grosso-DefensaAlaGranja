using FMODUnity;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using FMOD.Studio;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{

    private List<EventInstance> eventInstances;
    private List<StudioEventEmitter> eventEmitters;

    private EventInstance ambienceEventInstance;

    [SerializeField] private SettingSO audioSettings;

    [Header("Slider Music")]
    [SerializeField] private Slider sliderMaster;
    [SerializeField] private Slider sliderMusic;
    [SerializeField] private Slider sliderSFX;

    [Header("Text Music")]
    [SerializeField] private TMP_Text textMaster;
    [SerializeField] private TMP_Text textMusic;
    [SerializeField] private TMP_Text textSFX;

    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance != null)
        {

            Debug.Log("Error ps manin");
        }
        Instance = this;

        eventInstances = new List<EventInstance>();
        eventEmitters = new List<StudioEventEmitter>(); 
        //audioSettings.LoadVolumes();
    }

    private void OnDestroy()
    {
        audioSettings.SaveVolumes();

        CleanUp();
    }
    private void Start()
    {
        InitializeAmbience(FMODEvents.Instance.fightTime);

        sliderMaster.value = audioSettings.GetMasterVolume();
        sliderMaster.onValueChanged.AddListener(UpdateMasterVolume);

         sliderMusic.value = audioSettings.GetMusicVolume();
        sliderMusic.onValueChanged.AddListener(UpdateMusicVolume);

        sliderSFX.value = audioSettings.GetSFXVolume();
        sliderSFX.onValueChanged.AddListener(UpdateSFXVolume);

        UpdateMasterVolume(sliderMaster.value);
        UpdateMusicVolume(sliderMusic.value);
        UpdateSFXVolume(sliderSFX.value);


        
    }

    private void UpdateMasterVolume(float value)
    {
        textMaster.text = Mathf.RoundToInt(value * 100).ToString();
        audioSettings.SetMasterVolume(value);
    }

    private void UpdateMusicVolume(float value)
    {
        textMusic.text = Mathf.RoundToInt(value * 100).ToString();
        audioSettings.SetMusicVolume(value);
    }

    private void UpdateSFXVolume(float value)
    {
        textSFX.text = Mathf.RoundToInt(value * 100).ToString();
        audioSettings.SetSFXVolume(value);
    }

    private void InitializeAmbience(EventReference ambienceEventReference)
    {
        ambienceEventInstance = CreateEventInstance(ambienceEventReference);
        ambienceEventInstance.start();
    }

    private void SetAmbienceParameter(string parameterName, float parameterValue)
    {
        ambienceEventInstance.setParameterByName(parameterName, parameterValue);
    }
    public void PlayOneShoot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }
    public EventInstance CreateEventInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        eventInstances.Add(eventInstance);
        return eventInstance;
    }

    public StudioEventEmitter InitializeEventEmitter(EventReference eventReference, GameObject emitterGameObject)
    {
        StudioEventEmitter emitter = emitterGameObject.GetComponent<StudioEventEmitter>();
        emitter.EventReference = eventReference;
        eventEmitters.Add(emitter);
        return emitter;

    }


    private void CleanUp()
    {
        foreach (EventInstance eventInstance in eventInstances)
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            eventInstance.release();
        }


        foreach (StudioEventEmitter emitter in eventEmitters)
        {
            emitter.Stop();
        }
    }
}
