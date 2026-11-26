
using UnityEngine;
using UnityEngine.InputSystem;

public class jumpFromTo : MonoBehaviour
{
    [Header("Player Variables")]
    [SerializeField] private Transform Player;
    [Header("Points From Armiture")]
    [SerializeField] private Transform StartPoint;
    [SerializeField] private Transform EndPoint;


    [SerializeField] private float Force;

    [SerializeField] private Vector3 offset;
    [SerializeField] private Transform[] ColTran;

    [SerializeField] private Rigidbody[] allRigid;
    private bool add;
    private void Update()
    {

        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            /*foreach (Rigidbody Rig in allRigid)
            {
                Rig.linearVelocity = Player.forward * Force;
            }*/
            allRigid[13].linearVelocity = Player.forward * Force;


            foreach (Transform tra in ColTran)
                {
                tra.eulerAngles = new Vector3(0, 0, 0);
            }

           
          
           
            //add = true;
        }
        
        if (add)
        {
 StartPoint.position = Player.position + offset;
        }
      

    }
}
