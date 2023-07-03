using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorLocker : MonoBehaviour
{
    static public void SetCursorLockState(CursorLockMode lockState)
    {
        Cursor.lockState = lockState;
    }
}
