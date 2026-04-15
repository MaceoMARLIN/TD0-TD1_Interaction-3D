using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectClick : MonoBehaviour
{
   public float force = 5;
   public Animator anim;
   private bool etatF = true;
   private bool etatO = false;

   void Update()
    {
       var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
       RaycastHit hit;
       if (Physics.Raycast(ray, out hit))
        {
            var selection = hit.transform;
            var rig = selection.GetComponent<Rigidbody>();

            if (hit.collider.gameObject.name == "Cube1")
            {
                if (Input.GetMouseButton(0))
                {
                    rig.AddForce(Camera.main.transform.forward * 10);
                }
            }
            if (hit.collider.gameObject.name == "Cube")
            {
                if (Input.GetMouseButton(0))
                {
                    rig.AddForce(rig.transform.up * force, ForceMode.Impulse);
                }
            }
            if (hit.collider.gameObject.name == "Porte")
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Manage();
                }
            }
        }
    }

    public void Manage()
    {
        if (etatF)
        {
            anim.Play("DoorOpen");
            etatF = false;
            etatO = true;
        }
        else if (etatO)
        {
            anim.Play("DoorClose");
            etatF = true;
            etatO = false;
        }
    }
}
        