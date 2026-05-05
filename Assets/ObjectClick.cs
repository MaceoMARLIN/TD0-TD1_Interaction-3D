using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectClick : MonoBehaviour
{
   public float force = 5;
   public Animator anim;
   private bool etatF = true;
   private bool etatO = false;
   private bool etat1 = false;
   private bool etat2 = false;
   private bool cube2Counted = false;
   private int count_cube = 0;
   public Renderer door;
   public Material Vert_open;
   private bool materialChanged = false;

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
                    if (etat1 == false)
                    {
                        etat1 = true;
                        count_cube++;
                    }
                    rig.AddForce(Camera.main.transform.forward * 10);
                }
            }
            if (rig != null)
            {
                if (hit.collider.gameObject.name == "Cube")
                {
                    if (Input.GetMouseButton(0))
                    {
                        if (etat2 == false)
                        {
                            etat2 = true;
                            count_cube++;
                        }
                        rig.AddForce(rig.transform.up * force, ForceMode.Impulse);
                    }
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

        if (ObjectGrab.isGrabbed == true && cube2Counted == false)
        {
            count_cube ++;
            cube2Counted = true;
        }

        if (count_cube == 3 && materialChanged == false)
        {
            door.material = Vert_open;
            materialChanged = true;
        }
    }

    public void Manage()
    {   if (count_cube == 3)
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
}
        