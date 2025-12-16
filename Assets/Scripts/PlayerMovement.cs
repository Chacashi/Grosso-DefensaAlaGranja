
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    
    [Header ("Movimmiento")]
    [SerializeField] private float speed;

    [Header("Input")]
    [SerializeField] private InputReader inputReader;


    private Rigidbody2D rb;
    private Vector2 inputVector;

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

    }

    








}
