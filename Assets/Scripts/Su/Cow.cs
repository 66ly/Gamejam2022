using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow : MonoBehaviour
{
    // [SerializeField] public GameObject toolTipText = null;
    public GameObject squarePrefab;
    public float shootingForce = 500f;

    public Rigidbody2D rigidbody;
    [SerializeField] Transform groundCheckCollider;
    [SerializeField] LayerMask groundLayer;

    public Vector2 shootingDirection = Vector2.right;
    public float squareSpawnDistance = 1;


    float horizontalValue;
    bool isGrounded = true;
    const float groundCheckRadius = 0.2f;
    bool facingRight = true;
    float moveSpeed = 1;
    float runningSpeed = 3;
    bool isRunning;
    float jumpPower = 5;
    int jumpCount = 0;



    // Item loaded
    Item? loadedItem = null;

    // Start is called before the first frame update
    void Start()
    {
    }

    void FixedUpdate()
    {
        Move(horizontalValue);
    }

    // Update is called once per frame
    void Update()
    {
        horizontalValue = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.LeftShift))
            isRunning = true;
        //If LShift is released disable isRunning
        if (Input.GetKeyUp(KeyCode.LeftShift))
            isRunning = false;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        else if (Input.GetKey(KeyCode.Mouse0))
        {
            ShootItem();
        }
    }

    void Move(float dir)
    {
        // flip character sprite/animation if changing direction
        if ((dir > 0 && !facingRight) || (dir < 0 && facingRight))
        {
            Flip();
        }

        // set x axis displacement
        float xVal = dir * moveSpeed * 100 * Time.fixedDeltaTime;
        if (isRunning)
        {
            xVal *= runningSpeed;
        }

        Vector2 targetVelocity = new Vector2(xVal, rigidbody.velocity.y);
        rigidbody.velocity = targetVelocity;
    }

    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }

    void Jump()
    {
        if (isGrounded)
        {
            jumpCount = 0;
        }
        if (jumpCount < 2)
        {
            rigidbody.velocity = Vector2.up * jumpPower;
            jumpCount += 1;
        }
    }


    void GroundCheck()
    {
        // dbg!
        bool wasGrounded = isGrounded;
        isGrounded = false;
        //Check if the GroundCheckObject is colliding with other 2D Colliders that are in the "Ground" Layer
        //If yes (isGrounded true) else (isGrounded false)
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, groundCheckRadius, groundLayer);
        if (colliders.Length > 0)
        {
            isGrounded = true;
            if (!wasGrounded)
            {
                jumpCount += 1;
            }
        }
        else
        {
            // dbg! ???
            transform.parent = null;
        }

    }

    void ShootItem()
    {
        Item? loadedItem = gameObject.GetComponent<Inventory>().DropLoadedItem();
        if (!loadedItem == null)
        {
            CreateItem(loadedItem);
        }
        else
        {
            // toolTipText.gameObject.text = "Need to load an item first.";
        }
    }

    void CreateItem(Item item)
    {
        if (item.name == "Square")
        {
            CreateSquare();
        }
    }
    void CreateSquare()
    {
        // Calculate normalized direction from player to cursor
        Vector3 direction = GetCursorDirection();
        Vector3 playerPosition = gameObject.transform.position;
        Vector3 squarePosition = playerPosition + direction;
        Instantiate(squarePrefab, squarePosition, Quaternion.identity);
    }

    Vector3 GetCursorDirection()
    {
        // Convert cursor position to world position
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursorPosition.z = 0;

        // Get player position
        Vector3 playerPosition = gameObject.transform.position;

        // Calculate direction from player to cursor
        Vector3 direction = (cursorPosition - playerPosition).normalized * squareSpawnDistance;

        return direction;
    }
}