using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 5.0f;
    private Rigidbody2D rg;
    private Animator anim;

    void Start()
    {
        rg = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
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

    private void FlipSprite(bool runningHorizontaly)
    {
        if (runningHorizontaly)
        {
            transform.localScale = new Vector3(Mathf.Sign(rg.linearVelocity.x), 1f, 1f);
        }
    }
}
