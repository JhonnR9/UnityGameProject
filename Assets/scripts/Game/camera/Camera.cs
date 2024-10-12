using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;
    public Tilemap tilemap; 
    Camera cam; 

    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

    Vector3 cameraPosition;

    private void Awake()
    {
        cam = this.GetComponent<Camera>();
    }
    void Start()
    {
        // Calcular os limites automaticamente com base no Tilemap
        Bounds tilemapBounds = tilemap.localBounds;

        // Tamanho da c�mera em unidades do mundo (considerando o tamanho ortogr�fico)
        float cameraHeight = cam.orthographicSize * 2f;
        float cameraWidth = cameraHeight * cam.aspect;

        // Definir os valores m�nimo e m�ximo de X e Y com base no tamanho do Tilemap
        minX = tilemapBounds.min.x + cameraWidth / 2;
        maxX = tilemapBounds.max.x - cameraWidth / 2;
        minY = tilemapBounds.min.y + cameraHeight / 2;
        maxY = tilemapBounds.max.y - cameraHeight / 2;
    }

    void LateUpdate()
    {
        cameraPosition = playerTransform.position;

        // Limitar a posi��o da c�mera dentro dos limites calculados
        float clampedX = Mathf.Clamp(cameraPosition.x, minX, maxX);
        float clampedY = Mathf.Clamp(cameraPosition.y, minY, maxY);

        // Ajustar a posi��o da c�mera mantendo Z em -10 (posi��o padr�o para c�mera 2D)
        transform.position = new Vector3(clampedX, clampedY, -10f);
    }
}
