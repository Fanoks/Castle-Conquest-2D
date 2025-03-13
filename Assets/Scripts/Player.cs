using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 6f;
    [SerializeField] float jump = 7f;
    private Rigidbody2D rg;
    private Collider2D col;
    private PolygonCollider2D feets;
    private Animator anim;

    void Start()
    {
        rg = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        feets = GetComponent<PolygonCollider2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Jump();
        Run();
    }

    private void Run()
    {
        float controlInput = Input.GetAxis("Horizontal");


        Vector2 playerVelocity = new Vector2(controlInput * speed, rg.linearVelocity.y);
        rg.linearVelocity = playerVelocity;

        bool runningHorizontaly = Mathf.Abs(rg.linearVelocity.x) > Mathf.Epsilon;
        anim.SetBool("Running", runningHorizontaly);

        FlipSprite(runningHorizontaly);
    }

    private void Jump()
    {
        LayerMask mask = LayerMask.GetMask("Ground");
        if (Input.GetKeyUp(KeyCode.Space) && feets.IsTouchingLayers(mask))
        {
            rg.linearVelocityY = jump;
        }
    }

    private void FlipSprite(bool runningHorizontaly)
    {
        if (runningHorizontaly)
        {
            transform.localScale = new Vector3(Mathf.Sign(rg.linearVelocity.x), 1f, 1f);
        }
    }
}
