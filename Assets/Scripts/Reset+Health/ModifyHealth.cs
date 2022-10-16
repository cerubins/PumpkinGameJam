using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyHealth : MonoBehaviour
{
    [SerializeField] int health;

    public void changeHealth(int change)
    {
        health += change;
    }
}
