
using UnityEngine;
using FMOD.Studio;

public class PlayerMovement : MonoBehaviour
{
    
    [Header ("Movimmiento")]
    [SerializeField] private float speed;

    [Header("Input")]
    [SerializeField] private InputReader inputReader;


    private Rigidbody2D rb;
    private Vector2 inputVector;

    //audio
    private EventInstance playerWalk;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();


    }

    private void OnEnable()
    {

        inputReader.MovementEvent += SetMovementValue;
        inputReader.InteractEvent += SetInteract;

    }

    private void OnDisable()
    {
        inputReader.MovementEvent -= SetMovementValue;
        inputReader.InteractEvent -= SetInteract;
    }

    private void Start()
    {

        playerWalk = AudioManager.Instance.CreateEventInstance(FMODEvents.Instance.walkPlayer);
    }

    private void SetMovementValue(Vector2 value)
    {
        inputVector = value;
    }


    private void SetInteract()
    {
        Debug.Log("Gaaaa");
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = inputVector * speed;    
        UpdateSound();
    }


    private void UpdateSound()
    {
        if (rb.linearVelocity != Vector2.zero)
        {

            PLAYBACK_STATE playbackState;
            playerWalk.getPlaybackState(out playbackState);
            if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
            {
                playerWalk.start();
            }

        }


        else
        {
            playerWalk.stop(STOP_MODE.ALLOWFADEOUT);
        }
    }







}
