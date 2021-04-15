using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public Rigidbody RB;
    public bool isGrounded, isAscending, targetIsSet, reachedTarget;
    public LayerMask clickLayer;
    public Vector3 target;
    public Transform targ, human1, human2, human3;
    public void Start()
    {
        attackSpeed = 0.5f;
        waitUntilAttack = 2f;
        lookAtTargetSpeed = 2f;
        TStimer = 3f;
    }

    void Update()
    {
        if (isGrounded)
        {
            RB.constraints = RigidbodyConstraints.FreezePosition;
            if (Input.GetKey(KeyCode.W))
            {
                RB.constraints = RigidbodyConstraints.None;
                isAscending = true;
                RB.AddForce(new Vector3(0, ascendSpeed * 2f, 0), ForceMode.Impulse);
                //targetIsSet = false;  // ändra denna senare
            }
        }
        else
        {
            #region set target
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePos = -Vector3.one;
                            
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100f, clickLayer))
                {
                    mousePos = hit.point;
                    if (hit.collider.gameObject.name == "Start")
                    {
                        targ = human1;
                    }
                    if (hit.collider.gameObject.name == "Options")
                    {
                        targ = human2;
                    }
                    if (hit.collider.gameObject.name == "Quit")
                    {
                        targ = human3;
                    }
                    targetIsSet = true;
                }

            }

            #endregion
                 
        }
    }
    public void StartTutorial()
    {
       // SceneManager.LoadScene("Tutorial");
    }

    public void LoadGame()

    {
        //SceneManager.LoadScene("MainScene");
        //Set the time back to 1:
        //Time.timeScale = 1;

    }

    public void QuitGame()
    {
        //Application.Quit();
    }
    public void LoadMenu()
    {

        //SceneManager.LoadScene("Credit");
    }
}
