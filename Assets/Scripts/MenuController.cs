using System.Collections;
using System.Collections.Generic;
using UnityEngine;






public class MenuController : MonoBehaviour
{
    public Texture2D MouseClickTexture, MouseTexture;
    public float speed;

    private Vector2 CursorDir;
  
    

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("joystick 1 button 0"))
        {
            Cursor.SetCursor(MouseClickTexture, Vector2.zero, CursorMode.Auto);
            CursorControl.SimulateLeftClick();
        }
        else{
            Cursor.SetCursor(MouseTexture, Vector2.zero, CursorMode.Auto);
        }
    }

    void FixedUpdate()
    {
            if (Input.GetAxis("MenuH") != 0 || Input.GetAxis("MenuV") != 0)
        {
            CursorDir = new Vector2(Input.GetAxis("MenuH"), Input.GetAxis("MenuV"));
           // CursorDir.Normalize();
            CursorControl.SetLocalCursorPos((Vector2)Input.mousePosition + (CursorDir * speed));      
        }
    }


}
