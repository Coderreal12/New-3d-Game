
using UnityEngine;
using UnityEngine.InputSystem;

public class Slide : MonoBehaviour
{

    [Header("Animation/Physics")]
    [SerializeField] private Animator Animation;

    [SerializeField] private Rigidbody PlayerRB;

    [SerializeField] private Collider WalkingCol;
    [SerializeField] private Collider SlidingCol;
    [Header("Sliding Floats")]
[SerializeField] private float duration;
    [SerializeField] private float MinSlideSpeed;

    [Header("Sliding Animations")]
    [SerializeField] private Jumping PlayerJMP;
    [SerializeField] private Player_Movent PlayerMVT;

    
    
    private bool done = false;

    private void Start()
    {
        
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
        }
        else
        {
            Animation.SetBool("Sliding", false);
        }
     
    }

    private void SetColliders()
    {
        WalkingCol.isTrigger = done;
        SlidingCol.isTrigger = !done;
    }


}
