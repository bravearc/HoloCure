using System.Collections;
using UnityEngine;
// ĳ���� �̵��� �ǰ� �� ���� �ð� ���� ����� ������ ��ũ��Ʈ
public class Player_Move : MonoBehaviour
{
    private Rigidbody2D playerRbody;
    public GameObject playerHp;
    public GameObject[] m_Hp;
    public Transform m_HpController;

    float _axisHor;
    float _axisVer;
    public static string gameState;
    public static bool isDamage = false;

    // ����
    public static float moveSpeed { get; set; }
    private void Awake()
    {

    }


    void Start()
    {
        playerRbody = GetComponent<Rigidbody2D>();

        gameState = "playing";
    }

    void FixedUpdate()
    {
        _axisHor = Input.GetAxisRaw("Horizontal");
        _axisVer = Input.GetAxisRaw("Vertical");

        Vector2 move = new Vector2(_axisHor, _axisVer) * 20;

        if (move.sqrMagnitude > 1)
        {
            move.Normalize();
        }

        playerRbody.MovePosition(playerRbody.position + move * Time.fixedDeltaTime);

    }
    private void OnCollisionEnter2D(Collision2D other)
    {

    }

}