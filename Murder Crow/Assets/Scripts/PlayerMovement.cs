using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Player player;
    
    private bool invertedWSControls;
    private bool invertedADControls;
    private float speed, tiltZ, tiltX, lowestHeight, maxVelocity, maxHeight, maxTilt, tiltSpeed, descendSpeed, ascendSpeed, sprintSpeed, turnSpeed, windFactor;
    public Vector3 windVelocity, angles; 
    [Range(-10.0f, 0.0f)]
    public float maxFallSpeed;
    [Range(0.0f, 10.0f)]
    public float maxAscendSpeed;

    void Start()
    {
        invertedWSControls = player.settings.WSBoxChecked;
        invertedADControls = player.settings.ADBoxChecked;
        speed = 3f;
        tiltZ = 0;
        tiltX = 0;
        lowestHeight = 9f;
        maxVelocity = 2f;
        maxHeight = 18f;
        maxTilt = 30;
        tiltSpeed = 30;
        descendSpeed = -2f;
        ascendSpeed = 0.8f;
        sprintSpeed = 6f;
        turnSpeed = 2.3f;
        windFactor = 0.5f;
        windVelocity = Vector3.zero;
    }

    void Update()
    {
        CheckIfReachedMaxHeight();
        CheckIfReachedLowestHeight();
        CheckIfSpeedingUp();
        UpdateWind();
    }

    private void FixedUpdate()
    {
        if (!player.targetIsSet)
        {
            if (!player.tutorialMode)
            {
                Vector3 localVel = transform.InverseTransformDirection(player.RB.velocity);
                localVel.z = speed;
                localVel.x = 0;
                localVel.y = Mathf.Clamp(localVel.y, maxFallSpeed, maxAscendSpeed);
                player.RB.velocity = transform.TransformDirection(localVel) + windVelocity;

                HandleMovementInput();
                UpdatePlayerRotation();
            }
            CheckIfInWindzone();
        }
        else
        {
            player.RB.constraints = RigidbodyConstraints.FreezePosition;
        }
    }

    private void CheckIfReachedMaxHeight()
    {
        maxAscendSpeed = (transform.position.y >= maxHeight && Input.GetKey(KeyCode.W)) ? 0 : 3.45f;
    }

    private void CheckIfReachedLowestHeight()
    {
        if (!player.targetIsSet && !player.reachedSkull && transform.position.y <= lowestHeight)
        {
            player.RB.constraints = !Input.GetKey(KeyCode.W) ? player.RB.constraints = RigidbodyConstraints.FreezePositionY : player.RB.constraints = RigidbodyConstraints.None;
        }
    }

    private void CheckIfSpeedingUp()
    {
        if (Input.GetKey(KeyCode.LeftShift) && speed != sprintSpeed)
        {
            StartCoroutine(SpeedBoost());
            speed = sprintSpeed;
            player.anim.SetFloat("flapSpeed", 1.15f);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            StopCoroutine(SpeedBoost());
            speed = 3f;
            player.anim.SetFloat("flapSpeed", 1f);
            player.anim.SetBool("isFlyingUp", false);
        }
    }
    private void UpdateWind()
    {
        if (player.windDirection != null)
        {
            windVelocity += (Vector3)player.windDirection * windFactor * Time.deltaTime;
        }
        else if (windVelocity != Vector3.zero)
        {
            windVelocity -= windVelocity.normalized;
            if (windVelocity.magnitude <= 1) windVelocity = Vector3.zero;
        }
    }

    private void HandleMovementInput()
    {
        // up/down movement
        if ((Input.GetKey(KeyCode.W) && !invertedWSControls) || (Input.GetKey(KeyCode.S) && invertedWSControls))
        {
            player.RB.AddForce(new Vector3(0, ascendSpeed, 0), ForceMode.Impulse);
            tiltX = transform.position.y < maxHeight - 2 
                ? Mathf.Max(tiltX - 20 * Time.fixedDeltaTime, -maxTilt) 
                : tiltX = Mathf.Min(tiltX + tiltSpeed * 2 * Time.fixedDeltaTime, 0);

            if (player.reachedTarget || player.reachedHunter)
            {
                player.RB.constraints = RigidbodyConstraints.None;
                player.RB.AddForce(new Vector3(0, ascendSpeed * 2f, 0), ForceMode.Impulse);
            }
        }
        else if (((Input.GetKey(KeyCode.S) && !invertedWSControls) || (Input.GetKey(KeyCode.W) && invertedWSControls)) &&
            !player.reachedTarget && !player.reachedHunter)
        {
            if (transform.position.y > lowestHeight)
            {
                tiltX = Mathf.Min(tiltX + 20 * Time.fixedDeltaTime, maxTilt);
                player.RB.AddForce(new Vector3(0, descendSpeed, 0), ForceMode.Impulse);
            }
            else
            {
                tiltX = Mathf.Max(tiltX - tiltSpeed * 2 * Time.fixedDeltaTime, 0);
            }
        }
        else if (tiltX != 0)
        {
            tiltX = tiltX < 0 ? Mathf.Min(tiltX + tiltSpeed * 2 * Time.fixedDeltaTime, 0) : Mathf.Max(tiltX - tiltSpeed * 2 * Time.fixedDeltaTime, 0);
        }

        // left/right movement
        if (!player.reachedTarget && !player.reachedHunter)
        {
            if ((Input.GetKey(KeyCode.A) && !invertedADControls) || (Input.GetKey(KeyCode.D) && invertedADControls))
            {
                float turn = Input.GetAxis("Horizontal") * turnSpeed * Time.fixedDeltaTime;
                tiltZ = Mathf.Min(tiltZ + tiltSpeed * Time.fixedDeltaTime, maxTilt);
                player.RB.AddTorque(transform.up * turn, ForceMode.VelocityChange);
                if (player.RB.angularVelocity.y <= -maxVelocity)
                {
                    player.RB.angularVelocity = new Vector3(player.RB.angularVelocity.x, -maxVelocity, player.RB.angularVelocity.z);
                }
            }
            else if ((Input.GetKey(KeyCode.D) && !invertedADControls) || (Input.GetKey(KeyCode.A) && invertedADControls))
            {
                float turn = Input.GetAxis("Horizontal") * turnSpeed * Time.fixedDeltaTime;
                tiltZ = Mathf.Max(tiltZ - tiltSpeed * Time.fixedDeltaTime, -maxTilt);
                player.RB.AddTorque(transform.up * turn, ForceMode.VelocityChange);
                if (player.RB.angularVelocity.y >= maxVelocity)
                {
                    player.RB.angularVelocity = new Vector3(player.RB.angularVelocity.x, maxVelocity, player.RB.angularVelocity.z);
                }
            }
            else if (tiltZ != 0)
            {
                player.RB.angularVelocity = new Vector3(0, 0, 0);
                tiltZ = tiltZ < 0 ? Mathf.Min(tiltZ + tiltSpeed * 2 * Time.fixedDeltaTime, 0) : Mathf.Max(tiltZ - tiltSpeed * 2 * Time.fixedDeltaTime, 0);
            }
        }
    }

    private void UpdatePlayerRotation()
    {
        angles = new Vector3(tiltX, transform.eulerAngles.y, tiltZ);
        transform.rotation = Quaternion.Euler(angles);
    }

    private void CheckIfInWindzone()
    {
        if (player.inWindZone)
        {
            player.RB.AddForce(player.windZone.GetComponent<WindArea>().direction * player.windZone.GetComponent<WindArea>().strength);
        }
    }

    private IEnumerator SpeedBoost()
    {
        player.anim.SetBool("isFlyingUp", true);
        yield return new WaitForSeconds(2f);
        player.anim.SetBool("isFlyingUp", false);
    }
}
