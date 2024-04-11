using DG.Tweening;
using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipController : MonoBehaviour
{
    [SerializeField]
    Transform steeringPivot;
    [SerializeField]
    Transform barrelRollPivot;
    [SerializeField]
    Transform jumpPivot;
    [SerializeField]
    float increase;
    [SerializeField]
    GameObject particlesPowerUp;
    [SerializeField]
    GameObject particlesSalto;

    // RAYCAST
    RaycastHit downHit;
    float rayCastDistance = 10f;
    [SerializeField]
    LayerMask trackLayer;
    [SerializeField]
    LayerMask invisibleTrackLayer_RIGHT;
    [SerializeField]
    LayerMask invisibleTrackLayer_LEFT;
    bool lastFrameOnTrack = true;
    bool thisFrameOnTrack = true;


    // HOVER
    float hover_height = 2f; // 1.8f - .5f;
    float height_above_cast = 5;

    // LOGIC VARIABLES
    Vector3 prev_up;
    float current_speed;
    float max_speed = 230; // 170;
    float min_speed = 230;
    float accel = 90f;
    float deccel = 200f;

    float smooth_y;
    float height_smooth = 12f;

    // Rotation related
    private Quaternion tilt;
    private float pitch_smooth = 5f;
    Quaternion global_orientation;
    private Vector3 previousGravity;
    private float turn_angle;
    private float turn_speed = 60;
    SplineProjector splineProjector;

    // Jump
    public bool jumping = false;
    bool death;
    float jumpDuration = .5f;

    float rememberMaxTime = .1f;
    float rememberTimer = 0;

    void Awake()
    {
        splineProjector = GetComponent<SplineProjector>();

        Debug.DrawLine(transform.position, transform.position - transform.up * rayCastDistance, Color.green, 50);
        if (Physics.Raycast(transform.position, -transform.up, out downHit, rayCastDistance, trackLayer))
        {
            transform.position = downHit.point + hover_height * transform.up;
            transform.rotation = Quaternion.FromToRotation(transform.up, downHit.normal) * transform.rotation;
            previousGravity = -downHit.normal;
        }
    }
    private void Start()
    {
        GameManager.GetInstance().SetPlayer(this.gameObject);
    }
    // INPUT
    bool accelerate_pressed;
    public void Acelerate(InputAction.CallbackContext context)
    {
        if (context.started)
            accelerate_pressed = true;
        else if (context.canceled)
            accelerate_pressed = false;
    }

    public void setDeath(bool death) { this.death = death; }

    public void Jump(InputAction.CallbackContext context)
    {
        if (death) { 
            GameManager.GetInstance().restart();
            death = false; 
        }
        else if (context.started)
        {
            rememberTimer = rememberMaxTime;
        }

        //else if (context.canceled)
    }
   
    bool jumpRight;
    void Jump()
    {
       // AudioManager_PK.GetInstance().Play("Woosh", 2);//UnityEngine.Random.Range(.9f, 1.1f));

        jumpRight = !jumpRight;

        int i;
        if (jumpRight) i = 1;
        else i = -1;

        barrelRollPivot.DOKill();
        jumpPivot.DOLocalJump(Vector3.zero, 5, 1, jumpDuration);
        BarrelRoll(i, jumpDuration - .1f, 1);
        Invoke("StopJumping", jumpDuration - .1f);
    }

    void StopJumping()
    { jumping = false; }

    private void Update()
    {
        if (Time.timeScale == 0)
            return;

        prev_up = transform.up;

        // Calcular velocidad
        if (accelerate_pressed)
        {
            float realAccel = accel * Time.deltaTime;

            current_speed += (current_speed + realAccel > max_speed) ? 0 : realAccel;
        }
        else if (current_speed > 0)
        {
            current_speed -= deccel * Time.deltaTime;
            current_speed = Mathf.Max(current_speed, 0f);
        }
        //else
        //    current_speed = 0f;

        current_speed = Mathf.Max(current_speed, min_speed);

        int barrelRollOrientation = 1;
        // Comprobar si esta encima de la carretera
        if (Physics.Raycast(transform.position + height_above_cast * prev_up, -prev_up, out downHit, rayCastDistance, trackLayer))
        {
            Debug.DrawLine(transform.position + height_above_cast * prev_up, downHit.point, Color.green );

            GetTurnInput();
            thisFrameOnTrack = true;
        }
        else if (Physics.Raycast(transform.position + height_above_cast * prev_up, -prev_up, out downHit, rayCastDistance, invisibleTrackLayer_RIGHT))
        {
            Debug.DrawLine(transform.position + height_above_cast * prev_up, downHit.point, Color.red);

            thisFrameOnTrack = false;

            barrelRollOrientation = -1;

            horizontal_input = -1;
            orientationForward = 1;
        }
        else if (Physics.Raycast(transform.position + height_above_cast * prev_up, -prev_up, out downHit, rayCastDistance, invisibleTrackLayer_LEFT))
        {
            Debug.DrawLine(transform.position + height_above_cast * prev_up, downHit.point, Color.red);


            thisFrameOnTrack = false;
            barrelRollOrientation = 1;


            float dist = Vector3.Distance(splineProjector.result.position, transform.position);
            // Debug.Log("Dist = " + dist);

            horizontal_input = 1;
            orientationForward = -1;
        }
        else return;

        TurnShip();

        AdjustOrientation();
        //checkGroundMovement();
        MoveShip();


        // Barrel Roll
        // En caso de que se haya salido de la carretera
        if (lastFrameOnTrack && !thisFrameOnTrack)
        {
            barrelRollPivot.DOKill();
            BarrelRoll(barrelRollOrientation, 1, 2);
            Instantiate(particlesSalto, transform);
            //AudioManager_PK.GetInstance().Play("Woosh", 2);//UnityEngine.Random.Range(.9f, 1.1f));
        }
        // Cuando aterriza
        else if (!lastFrameOnTrack && thisFrameOnTrack)
        {
            lookForward = true;
            Invoke("StopLookingForward", .3f);
        }

        // Actualizar valores
        lastFrameOnTrack = thisFrameOnTrack;




        // Jump Remember

        // Se quiere saltar
        if (rememberTimer > 0)
        {
            // Comprobar si se puede saltar
            if (!jumping && thisFrameOnTrack)
            {
                rememberTimer = 0;

                jumping = true;
                Jump();
            }

            rememberTimer -= Time.deltaTime;
        }
    }
    int orientationForward = 1;
    bool lookForward = false;

    private void StopLookingForward()
    { lookForward = false; }

    float horizontal_input;
    float vertical_input;
    void GetTurnInput()
    {
        horizontal_input = Input.GetAxis("Horizontal");
        vertical_input = Input.GetAxis("Vertical");
    }

    void TurnShip()
    {
        //// INPUT
        //horizontal_input = Input.GetAxis("Horizontal");
        //vertical_input = Input.GetAxis("Vertical");

        // Calcular angulo de rotacion
        turn_angle = turn_speed * Time.deltaTime * horizontal_input;

        if (lookForward)
        { turn_angle = (GetForwardAngle() / 30) * orientationForward; }

        global_orientation = Quaternion.Euler(0, turn_angle, 0);

        // Inclinacion lateral
        float lateralRotation = Mathf.LerpAngle(steeringPivot.localRotation.eulerAngles.z, -turn_angle * 50, Time.deltaTime * 5);
        steeringPivot.localRotation = Quaternion.Euler(0, 0, lateralRotation);
    }

    void MoveShip()
    {
        float distance = downHit.distance - height_above_cast;
        smooth_y = Mathf.Lerp(smooth_y, hover_height - distance, Time.deltaTime * height_smooth);
        smooth_y = Mathf.Max(distance / -3, smooth_y); //sanity check on smooth_y

        transform.localPosition += prev_up * smooth_y;
        transform.position += transform.forward * (current_speed * Time.deltaTime);
    }

    void AdjustOrientation()
    {
        //transform.rotation = Quaternion.FromToRotation(transform.up, downHit.normal) * global_orientation;
        Vector3 desired_up = Vector3.Lerp(prev_up, downHit.normal, Time.deltaTime * pitch_smooth);
        tilt.SetLookRotation(transform.forward - Vector3.Project(transform.forward, desired_up), desired_up);
        transform.rotation = tilt * global_orientation;
        previousGravity = -downHit.normal;
    }

    private void BarrelRoll(int orientation = 1, float time = 1, float numLoops = 1)
    {
        barrelRollPivot.DOLocalRotate(new Vector3(0, 0, -360 * orientation * numLoops), time, RotateMode.FastBeyond360);
    }

    public void IncreaseVel()
    {
        min_speed += increase;
        max_speed += increase;
        //Debug.Log("Min:" + min_speed);
        //Debug.Log("Max:" + max_speed);
    }
    float GetForwardAngle()
    { return Vector3.Angle(transform.forward, splineProjector.result.forward); }

    public bool IsOnWall()
    {
        // Debug.Log(thisFrameOnTrack);
        return !thisFrameOnTrack;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PowerUp>() != null)
        {
            Instantiate(particlesPowerUp, transform);

            //float duration = par_powerup.main.duration + par_powerup.main.startLifetimeMultiplier;
            //Destroy(particlesPowerUp, duration);
        }
    }
}
