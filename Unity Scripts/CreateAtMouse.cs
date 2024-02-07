using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateAtMouse: MonoBehaviour
{
    public KeyCode Keycode;
        [SerializeField] private Transform Prefab;

    // Update is called once per frame
    void Update()
    {
     if (Input.GetKeyDown(Keycode))
    {
        Instantiate(Prefab, MousePosInWorldSpace(), Quaternion.identity); 
    }
Vector3 MousePosInWorldSpace()
{

   Vector3 mousePos = Input.mousePosition;
   return Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane));

}
}
}
