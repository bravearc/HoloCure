using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField]Map_Stage1 _stage1;

    private void Awake()
    {
        _stage1 = GetComponentInParent<Map_Stage1>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerArea"))
        {
            _stage1.MapSwap();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Character"))
        {
            _stage1.MainMap = transform;
        }
    }
}
