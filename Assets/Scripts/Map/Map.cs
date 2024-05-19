using UnityEngine;

public class Map : MonoBehaviour
{
    MapController mapController;

    private void Awake()
    {
        mapController = GetComponentInParent<MapController>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerArea"))
        {
            mapController.MapSwap();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            mapController.MainMap = transform;
        }
    }
}
