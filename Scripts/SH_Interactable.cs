using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SH_Interactable : MonoBehaviour
{

    [SerializeField]
    public Camera Cam;
    [SerializeField]
    public LayerMask layerMask;

    [SerializeField]
    public Transform Face;

    [SerializeField]
    public float InteractionRange = 10f;

    [SerializeField]
    public TextMeshProUGUI openDoor;


    public MH_DoorController door;





    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        ItemLookedAt();

        if (door != null)
        {

            openDoor.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {

                OpenTheDoor();

            }


        }
        else
        {

            openDoor.gameObject.SetActive(false);
        }

    }


    private void ItemLookedAt()
    {
        Debug.Log("ItemLookedAt wird ausgeführt!");

        RaycastHit hitInfo;
        if (Physics.Raycast(Face.transform.position, Cam.transform.forward, out hitInfo, InteractionRange, layerMask))
        {

            var hit = hitInfo.collider.GetComponent<MH_DoorController>();
            Debug.Log(hit);

            if (hit == null)
            {

                door = null;
            }
            else
            {
                door = hit;
                openDoor.text = " E zum öffnen der Tür drücken";
            }



        }
        else
        {

            door = null;

        }



    }

    private void OpenTheDoor()
    {

        door.doorIsOpening = true;
        Debug.Log("Tür auf!");
    }
}