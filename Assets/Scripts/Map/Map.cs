using UnityEngine;

public class Map : MonoBehaviour
{
    SubItem_Map subItem_Map;

    private void Awake()
    {
        subItem_Map = GetComponentInParent<SubItem_Map>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerArea"))
        {
            subItem_Map.MapSwap();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            subItem_Map.MainMap = transform;
        }
    }
}
