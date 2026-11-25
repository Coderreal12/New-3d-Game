using UnityEngine;
using UnityEngine.InputSystem;

public class WallRun : MonoBehaviour
{
    
    [Header("Spherecasts: (First One is Right, Second One Is Left")]
    [SerializeField] private Transform[] Raycasts;
    [SerializeField] private float Radius;
    [SerializeField] private string[] Walls;
    private bool[] Wall = new bool[2];

    [Header("Unfixed Variabes")]
    [SerializeField] private float JumpHeightLength;
    [SerializeField] private float WallJumpForce;
    [SerializeField] private float Duration;
    [Header("Scripts")]
    [SerializeField] Jumping PlayerScript;

    [Header("Player Transforms")]
    [SerializeField] private Transform Player;
    [SerializeField] private Rigidbody PlayerRB;
    [SerializeField] private Transform MoveTo;
    bool Left;
    bool Right;
    float temp;
    //Raycasts
 

    
    void Start()
    {
        
    }

   
    void Update()
    {
        
        CheckWall();
        SelectWall();
        SetWallJumpForce();
        
       if (Left && !PlayerScript.Grounded && Keyboard.current.aKey.isPressed && PlayerRB.linearVelocity.y >= JumpHeightLength || Right && !PlayerScript.Grounded && Keyboard.current.dKey.isPressed && PlayerRB.linearVelocity.y >= JumpHeightLength) 
        {
            WallRunLeft();
        }

    }

    private void FixedUpdate()
    {
         
    }

    private void CheckWall()
    {
        //Creats a loop for The raycasts Length Minus 1 (It starts at 0)
        for (int i = 0; i <= Raycasts.Length - 1; i++)
        {
            //Wall 1 is Right
            Wall[i] = Physics.CheckSphere(Raycasts[i].position, Radius, LayerMask.GetMask(Walls));
            
        }

    }


    private void SelectWall()
    {

        Right = Wall[0];
        Left = Wall[1];

    }
    private void SetWallJumpForce()
    {
if (PlayerScript.Grounded )
        {
            temp = WallJumpForce;
        }

    }
    private void WallRunLeft()
    {
        Vector3 TempT = MoveTo.transform.position - Player.transform.position;
        PlayerRB.AddForce(TempT * WallJumpForce - Vector3.down * - 1 * Duration);
      
        
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

            for (int i = 0; i <= 1; i++)
        {
            Gizmos.DrawSphere(Raycasts[i].position, Radius);
        }

        }
}
