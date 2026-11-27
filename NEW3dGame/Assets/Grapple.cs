

using UnityEngine;
using UnityEngine.InputSystem;

public class Grapple : MonoBehaviour
{
    public bool Holding;
    RaycastHit Hit;

    [Header("UnFixed Variables")]
[SerializeField] private float Force;



    [Header("On Spawn")]
    [SerializeField] private GameObject Grabable;
    private SpringJoint Joint;
    private Transform SpawnLoc;
    private GameObject TempGame;

    [Header("Fixed Variables")]
    [SerializeField] private Rigidbody rig;
    
    [SerializeField] float MaxLinear;
   


    void Update()
    {
        SendRaycast();
        OnContact();
        OnRelease();

     


    }



    private void SendRaycast()
    {

 if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            
            Vector3 pos = Mouse.current.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(pos);
           if ( Physics.Raycast(ray, out Hit, 100f, LayerMask.GetMask("Ground")))
            {
SpawnCube();
                Holding = true;
            }


        }
    }


    private void OnContact()
    {
 //If It is a grappable surface 
      if (Holding && Hit.collider != null)
        {
            



            Joint = TempGame.GetComponent<SpringJoint>();
            Joint.connectedBody = rig;

            rig.maxLinearVelocity = MaxLinear;

        }
    }

    private void OnRelease()
    {
 if (Holding && !Mouse.current.leftButton.isPressed)
        {
            Joint.connectedBody = null;
            Joint = null;
            Holding = false;
            Destroy(TempGame.gameObject);
            TempGame = null;
        }
    }

    private void SpawnCube()
    {
        TempGame = Instantiate(Grabable);
        TempGame.transform.position = Hit.point;

    }

}
