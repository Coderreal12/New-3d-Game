using UnityEngine;
using UnityEngine.InputSystem;
public class Look_At_player : MonoBehaviour
{


    [SerializeField] Player_Movent mov;

    [Header("For Collsiion Detection")]
    [SerializeField] private float radius;



    [Header("Unfixed Variables")]
    [SerializeField] float dis;
    [SerializeField] private Vector3 OldPos;
    [SerializeField] private Vector3 NewPos;
    [SerializeField] private Rigidbody cam;

    [Header("Scrolling")]
    [SerializeField] int ScrollInto;
    [SerializeField] int scrollOut;

    [SerializeField] int ScrollIntoShift;
    [SerializeField] int ScrollOutShift;

    [Header("inputs")]
    [SerializeField] private InputActionReference scroll;

    //bools
    bool Check;

    //Floats
    float y;

    //Vector 3
    [Header("Camera Clipping Fix")]
    private Vector3 offset;

    [SerializeField] private float Scrollofst;
    [SerializeField] private float Speed;
    [SerializeField] private float Radi;
    [SerializeField] private float MaxFowClip;
    [SerializeField] private float MinFowClip;

    [SerializeField] private float MinTraClip;
    [SerializeField] private float MaxTraClip;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

     
        SetDistance();
        
        Colliderdetection();
    }

    private void FixedUpdate()
    {
        
    }
    private void SetDistance()
    {
if (mov.flip)
        {
            transform.localPosition = OldPos;
Scroll(ScrollInto, scrollOut);

        }
        else
        {
            transform.localPosition = NewPos + offset;
            Scroll(ScrollIntoShift, ScrollOutShift);
        }
    }

    private void Scroll(int scrollOut, int ScrollInto)
    {
        
        y += scroll.action.ReadValue<float>();
        if (y >= ScrollInto)
        {
            y--;
        }
        if(y <= scrollOut)
        {
            y++;
        }
      
        transform.Translate(0, 0, y + Scrollofst);
        
    }

    private void Colliderdetection()
    {
        Check = Physics.CheckSphere(transform.position, radius);
        if (Check)
        {
            Scrollofst += Speed;


        }
        else if (!Check && !Physics.CheckSphere(transform.position, Radi))
        {


            Scrollofst -= Speed;
        }
        Scrollofst = Mathf.Clamp(Scrollofst, MinFowClip, MaxFowClip);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, radius);
    }

    private void OnEnable()
    {
        scroll.action.Enable();
    }

    private void OnDisable()
    {
        scroll.action.Disable();
    }

}
