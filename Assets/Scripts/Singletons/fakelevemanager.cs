using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fakelevemanager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject ghost_form;
    [SerializeField] GameObject physical_form;

    public bool isOverWorld;

    PhysicalMovementScript overworld_controller;
    GhostMovementController spirit_controller;

    void Start()
    {
         overworld_controller = physical_form.GetComponent<PhysicalMovementScript>();
         spirit_controller = ghost_form.GetComponent<GhostMovementController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            isOverWorld = !isOverWorld;
            if (isOverWorld)
            {
                overworld_controller.enabled = true;
                spirit_controller.enabled = false;
            }
            else
            {
                overworld_controller.enabled = false;
                spirit_controller.enabled = true;
            }
        }
    }
}
