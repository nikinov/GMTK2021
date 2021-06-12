using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    public float speed = 10f;

    [SerializeField] private Rigidbody2D rb;

    private Vector2 _movement;

    public Transform cam;

    public GameObject replayMenu;

    private void Update()
    {
        // Getting H & V input
        _movement.x = 1f;

        _movement.y = Input.GetAxisRaw("Vertical");


        // Camera movement
        cam.position = new Vector2(transform.position.x, 0f);
    }

    private void FixedUpdate()
    {
        // Movement
        rb.MovePosition(rb.position + _movement * (speed * Time.fixedDeltaTime));
    }

    // Collision and checking
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("obsticle")) Destroy();
    }

    // Death and replaying menu view
    private void Destroy()
    {
        Destroy(gameObject);

        replayMenu.SetActive(true);
    }
}