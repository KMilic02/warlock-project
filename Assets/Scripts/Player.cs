using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    int ownership;
    ControlMode controlMode = ControlMode.movement;
    State state = State.normal;

    BaseCharacter character;

    Camera p_Camera;
    [SerializeField] float cameraXOffset, cameraYOffset, cameraZOffset;
    [SerializeField] Vector3 cameraAngleEuler;
    Vector3 cameraPosition = new Vector2(0, 0);

    [SerializeField] float movementSpeed;
    float targetYRotation;
    Vector3 targetPosition;

    void setCharacter(BaseCharacter character)
    {
        this.character = character;
    }

    void Start()
    {
        p_Camera = transform.GetComponentInChildren<Camera>();
        setCameraPosition();
    }

    void Update()
    {
        if (controlMode == ControlMode.movement)
        {
            handleMovement();
        }
    }

    void setCameraPosition()
    {
        cameraPosition.x = cameraXOffset;
        cameraPosition.y = cameraYOffset;
        cameraPosition.z = cameraZOffset;
        p_Camera.transform.localPosition = cameraPosition;
        p_Camera.transform.rotation = Quaternion.Euler(cameraAngleEuler);
    }

    void handleMovement()
    {
        if (Input.GetMouseButtonDown(1) && state == State.normal)
        {
            targetPosition = Utilities.getClickEventPosition(p_Camera);
        }

        if (transform.position != targetPosition)
        {
            var horizontalPlaneTarget = new Vector3(targetPosition.x, 0, targetPosition.z);
            var horizontalPlaneCurrent = new Vector3(transform.position.x, 0, transform.position.z);
            var direction = (horizontalPlaneTarget - horizontalPlaneCurrent).normalized;
            Vector3 nextPosition = Vector3.zero;
            nextPosition.x = horizontalPlaneCurrent.x + direction.x * movementSpeed * Time.deltaTime;
            nextPosition.z = horizontalPlaneCurrent.z + direction.z * movementSpeed * Time.deltaTime;

            var nextPositionY = 0.0f;
            RaycastHit yRayHit;
            if (Physics.Raycast(nextPosition + Vector3.Scale(transform.position, Vector3.up), Vector3.down, out yRayHit, Mathf.Infinity, ~LayerMask.NameToLayer("Terrain")))
            {
                nextPositionY = yRayHit.point.y + transform.localScale.y * 0.5f;
            }
            nextPosition.y = nextPositionY;

            transform.position = nextPosition;
        }
    }

    public void setTargetPosition(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }
}
