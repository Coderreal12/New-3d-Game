using UnityEngine;
using UnityEngine.InputSystem;

public class Jumping : MonoBehaviour
{
    [Header("Jumping Variables")]
    [SerializeField] float JumpForce;
    [SerializeField] private float Radius;
    [SerializeField] Rigidbody Player;
    [SerializeField] Transform SphereLoc;
    [SerializeField] string[] Ground;

    [Header("Animaiton Controller")]
    [SerializeField] private Animator Animation;

    //bool
    public bool Grounded;
    public bool jump;
    // Update is called once per frame
    void Update()
    {
        

        //Grounded is equal to the value of the sphere casts
        Grounded = Physics.CheckSphere(SphereLoc.position, Radius, LayerMask.GetMask(Ground));

        //If Grounded and Space pressed.
        if (Physics.CheckSphere(SphereLoc.position, Radius, LayerMask.GetMask(Ground)) && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            
            Jump();
            
        }
        //For animation
        jump = Grounded;
       SetGrounded();
    }

    private void FixedUpdate()
    {
     

    }

    private void SetGrounded()
    {
Player.linearDamping = 10f;

        //If Inair then set the lineardamping to 0
        if (!Physics.CheckSphere(SphereLoc.position, Radius, LayerMask.GetMask(Ground)))
        {
            Player.linearDamping = 0f;
        }
    }
    private void Jump()
    {
        //Add a force up Duhh.
        Player.AddForce(0, JumpForce, 0);
     
        
    

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawSphere(SphereLoc.position, Radius);

    }

   
}
