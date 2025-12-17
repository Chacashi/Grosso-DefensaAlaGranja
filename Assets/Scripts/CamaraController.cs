using UnityEngine;

public class CamaraController : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private InputReader inputReader;

    [Header("Ajustes de Movimiento")]
    [SerializeField] private float moveSpeed = 10f; // En 2D suele ser menor que en 3D
    [SerializeField] private float edgeSize = 50f; // Píxeles del borde
    [SerializeField] private float smoothTime = 0.2f; // Inercia

    [Header("Límites del Mapa (2D)")]
    [SerializeField] private Vector2 limitX = new Vector2(-20, 20); // Izquierda / Derecha
    [SerializeField] private Vector2 limitY = new Vector2(-15, 15); // Abajo / Arriba

    // Variables internas
    private Vector3 currentVelocity;
    private Vector3 targetPosition;

    private void Awake()
    {
        targetPosition = transform.position;
    }

    private void LateUpdate()
    {
        HandleCameraMovement();
    }

    private void HandleCameraMovement()
    {
        // 1. INPUT
        Vector2 mousePos = inputReader.MousePosition;
        Vector3 inputDir = Vector3.zero;

        // 2. LÓGICA DE BORDES (Aquí está el cambio clave)
        // Eje X (Igual que antes)
        if (mousePos.x > Screen.width - edgeSize) inputDir.x = 1f;
        else if (mousePos.x < edgeSize) inputDir.x = -1f;

        // Eje Y (Ahora mapeamos Y de pantalla a Y del mundo)
        if (mousePos.y > Screen.height - edgeSize) inputDir.y = 1f;
        else if (mousePos.y < edgeSize) inputDir.y = -1f;

        // 3. CALCULAR DESTINO
        Vector3 moveVector = inputDir * moveSpeed * Time.deltaTime;
        targetPosition += moveVector;

        // 4. LIMITAR (CLAMP)
        targetPosition.x = Mathf.Clamp(targetPosition.x, limitX.x, limitX.y);
        targetPosition.y = Mathf.Clamp(targetPosition.y, limitY.x, limitY.y);

        // ¡IMPORTANTE EN 2D!
        // Mantener la Z original (normalmente -10). Si la pones en 0, la cámara 
        // se mete dentro de los sprites y se deja de ver todo.
        targetPosition.z = transform.position.z;

        // 5. MOVIMIENTO SUAVE
        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref currentVelocity,
            smoothTime
        );
    }

    // --- DEBUG VISUAL 2D ---
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        // Dibujamos un rectángulo vertical (X, Y) en lugar de uno plano
        float centerX = (limitX.x + limitX.y) / 2;
        float centerY = (limitY.x + limitY.y) / 2;
        Vector3 center = new Vector3(centerX, centerY, 0); // Z en 0 para verlo en el editor

        float sizeX = limitX.y - limitX.x;
        float sizeY = limitY.y - limitY.x;
        Vector3 size = new Vector3(sizeX, sizeY, 1);

        Gizmos.DrawWireCube(center, size);
    }
}
