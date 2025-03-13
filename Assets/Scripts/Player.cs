using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 6f;
    [SerializeField] float jump = 7f;
    [SerializeField] float climbspeed = 3f;

    private Rigidbody2D rg;
    private BoxCollider2D col;
    private PolygonCollider2D feets;
    private Animator anim;

    void Start()
    {
        rg = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        feets = GetComponent<PolygonCollider2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Jump();
        Run();
        Climb();
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
        if (Input.GetKeyUp(KeyCode.Space) && feets.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            rg.linearVelocityY = jump;
        }
    }

    private void Climb()
    {
        if (col.IsTouchingLayers(LayerMask.GetMask("Lader")))
        {
            float controlInput = Input.GetAxis("Vertical");
            Vector2 playerVelocity = new Vector2(rg.linearVelocityX, controlInput * climbspeed);

            rg.linearVelocity = playerVelocity;
            rg.gravityScale = 0f;

            anim.SetBool("Climbing", true);
        }
        else
        {
            anim.SetBool("Climbing", false);
            rg.gravityScale = 1f;
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
