using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransform : MonoBehaviour
{

    [SerializeField] GameObject ghost_form;
    [SerializeField] GameObject physical_form;
    [SerializeField] float ghostFormDuration;

    Rigidbody2D rgbd2d;

    public bool _isPhysical = true;

    private void Awake()
    {
        rgbd2d = this.GetComponent<Rigidbody2D>();
        _fixRigidBody();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_isPhysical)
            {
                _transform();
            }

            _fixRigidBody();
        }
        
    }

    void _transform()
    {
        if (_isPhysical)
        {
            physical_form.SetActive(false);
            ghost_form.SetActive(true);
            _isPhysical = false;

            StartCoroutine(waitForSecondsTransformBack());
        }
        else
        {
            ghost_form.SetActive(false);
            physical_form.SetActive(true);
            _isPhysical = true;
        }
    }

    void _fixRigidBody()
    {
        if (_isPhysical)
        {
            rgbd2d.mass = 1f;
            rgbd2d.drag = 0f;
            rgbd2d.angularDrag = 0.05f;
        }
        else
        {
            rgbd2d.mass = 1000f;
            rgbd2d.drag = 0.1f;
            rgbd2d.angularDrag = 0.1f;
        }
    }

    IEnumerator waitForSecondsTransformBack()
    {
        yield return new WaitForSeconds(ghostFormDuration);

        _transform();
    }
}
