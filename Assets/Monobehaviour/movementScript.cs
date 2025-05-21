using UnityEngine;

public class movementtest : MonoBehaviour
{
    private Vector2 moveAmount;
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D m_RB;
    
    [SerializeField] private Transform lightmove;
    public Animator anim;
    private const string horizontal = "Horizontal";
    private const string vertical = "Vertical";
    private const string endHorizontal = "endHorizontal";
    private const string endVertical = "endVertical";
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        moveAmount = moveInput.normalized * speed;
        anim.SetFloat(horizontal, moveAmount.x);
        anim.SetFloat(vertical, moveAmount.y);

        if(moveAmount != Vector2.zero)
        {
            anim.SetFloat(endHorizontal, moveAmount.x);
            anim.SetFloat(endVertical, moveAmount.y);

        }

        switch (moveInput)
        {
            case Vector2 vector when vector.Equals(Vector2.left):
                
                lightmove.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
                break;

            case Vector2 vector when vector.Equals(Vector2.right):
                
                lightmove.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
                break;

            case Vector2 vector when vector.Equals(Vector2.down):
                
                lightmove.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                break;

            case Vector2 vector when vector.Equals(Vector2.up):
                
                lightmove.rotation = Quaternion.Euler(new Vector3(0, 0, -180));
                break;
            default:

                break;


        }


    }

    private void FixedUpdate()
    {
        m_RB.MovePosition(m_RB.position + moveAmount * Time.fixedDeltaTime);
    }
}