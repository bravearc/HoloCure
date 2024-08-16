using UnityEngine;

public class Hamburger : MonoBehaviour
{
    public void Init(Transform newPos)
    {
        transform.position = newPos.position + Vector3.down;
    }
}
