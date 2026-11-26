
using UnityEngine;
using UnityEngine.InputSystem;

public class Slide : MonoBehaviour
{

    [Header("Animation/Physics")]
    [SerializeField] private Animator Animation;

    [SerializeField] private Rigidbody PlayerRB;

    [SerializeField] private CapsuleCollider WalkingCol;
    [SerializeField] private CapsuleCollider SlidingCol;
    [Header("Sliding Floats")]
[SerializeField] private float duration;
    [SerializeField] private float MinSlideSpeed;

    [Header("Sliding Animations")]
    [SerializeField] private Jumping PlayerJMP;
    [SerializeField] private Player_Movent PlayerMVT;
    [SerializeField] private Transform GroundedLoc;


     [SerializeField]  private float Toloc;
    float RegHeight = 22;
    public bool done = false;
    private Vector3 FirstLoc;
     [SerializeField] private float ToUp;
    private void Start()
    {
        FirstLoc = GroundedLoc.localPosition;
    }
    void Update()
    {
        SetColliders();
        //If LeftCTRL is pressed and Its grounded and The speed of the player is above the minum speed.
        if (Keyboard.current.leftCtrlKey.wasPressedThisFrame && PlayerJMP.Grounded && Vector3.Magnitude(PlayerRB.linearVelocity) >= MinSlideSpeed)
        {
            //Set The Animation To sliding & Initiate the Sliding Protocall
            Animation.SetBool("Sliding", true);
            done = true;
            
        }    
    }
    private void FixedUpdate()
    {
        if(done)
        {
 PlayerMVT.SlideSpeed(ref done, duration, PlayerMVT.tempo);
          
           Toloc-= .5f;
        }
        else
        {
           
            Toloc += .5f;
            Animation.SetBool("Sliding", false);
        }
        
        Toloc = Mathf.Clamp(Toloc, -5, 0);
 
    }

    private void SetColliders()
    {

        WalkingCol.height = RegHeight + Toloc;
        GroundedLoc.localPosition=  FirstLoc + Vector3.down *  ToUp;


        
        
       SlidingCol.isTrigger = !done;
    }


}
