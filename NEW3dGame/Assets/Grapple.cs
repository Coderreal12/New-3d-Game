

using UnityEngine;
using UnityEngine.InputSystem;

public class Grapple : MonoBehaviour
{
    private bool Holding;
    RaycastHit Hit;

    [Header("UnFixed Variables")]
[SerializeField] private float Force;

    [Header("Fixed Variables")]
    [SerializeField] private Rigidbody rig;
    private SpringJoint Joint;
    

    void Update()
    {
        SendRaycast();
     
     
      if (Holding && Hit.collider != null)
        {

            Joint = Hit.collider.GetComponent<SpringJoint>();
            Joint.connectedBody = rig;
        }
      if (Holding && !Mouse.current.leftButton.isPressed)
        {
            Joint.connectedBody = null;
            Joint = null;
            Holding = false;
        }


    }



    private void SendRaycast()
    {
 if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector3 pos = Mouse.current.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(pos);
           if ( Physics.Raycast(ray, out Hit, 100f, LayerMask.GetMask("Ground")))
            {

                Holding = true;
            }


        }
    }
}
