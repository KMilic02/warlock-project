using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{    
    int ownership;
    ControlMode controlMode = ControlMode.movement;
    State state = State.normal;

    BaseCharacter character;

    Camera p_Camera;
    Plane playerPlane;
    [SerializeField] float cameraXOffset, cameraYOffset, cameraZOffset;
    [SerializeField] Vector3 cameraAngleEuler;
    Vector3 cameraPosition = new Vector2(0, 0);

    [SerializeField] float movementSpeed;
    float targetYRotation;
    Vector3 targetPosition;
    NavMeshPath pathing;
    int pathingNode = 1;

    void setCharacter(BaseCharacter character)
    {
        this.character = character;
    }

    void Start()
    {
        p_Camera = transform.GetComponentInChildren<Camera>();
        pathing = new NavMeshPath();
        NavMesh.CalculatePath(transform.position, transform.position, 0, pathing);
        setCameraPosition();
    }

    void Update()
    {
        playerPlane = new Plane(Vector3.up, transform.position);
        if (controlMode == ControlMode.movement)
        {
            handleMovement();
            handleCasting();
        }
        fixCameraPosition();
    }

    void setCameraPosition()
    {
        cameraPosition.x = cameraXOffset;
        cameraPosition.y = cameraYOffset;
        cameraPosition.z = cameraZOffset;
        p_Camera.transform.localPosition = cameraPosition;
        p_Camera.transform.rotation = Quaternion.Euler(cameraAngleEuler);
    }

    void fixCameraPosition()
    {
        var rotation = p_Camera.transform.rotation;
        rotation.eulerAngles = cameraAngleEuler;
        p_Camera.transform.rotation = rotation;
        p_Camera.transform.position = transform.position - p_Camera.transform.forward * 4.0f;
    }

    void handleMovement()
    {
        if (Input.GetMouseButtonDown(1) && state == State.normal)
        {
            targetPosition = Utilities.getClickEventPositionNavigation(p_Camera, playerPlane);
            NavMesh.CalculatePath(transform.position, targetPosition, NavMesh.AllAreas, pathing);
            pathingNode = 0;
        }

        if (pathingNode < pathing.corners.Length)
        {
            var horizontalPlaneTarget = new Vector3(pathing.corners[pathingNode].x, 0, pathing.corners[pathingNode].z);
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
            var tempTransformPosition = transform.position;
            var tempTargetPositionNode = pathing.corners[pathingNode];
            tempTransformPosition.y = 0;
            tempTargetPositionNode.y = 0;
            if (Vector3.Distance(tempTransformPosition, tempTargetPositionNode) <= 0.001f)
            {
                pathingNode++;
            }

            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(direction);
                var euler = transform.rotation.eulerAngles;
                transform.rotation= Quaternion.Euler(euler.x, euler.y + 90, euler.z);
            }
        }
    }

    void handleCasting()
    {

    }

    public void setTargetPosition(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }
}
