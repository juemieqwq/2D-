using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;



public enum DirectionShape
{
    Box,
    Circle,
    Ray,
    Point
}

public enum DirectionType
{
    Cast,
    Overlap
}




[System.Serializable]
public struct DetectionInfo
{
    public DirectionShape directionShape;
    public DirectionType directionType;
    public Transform detectionPosition;
    public Vector2 detectionDirection;
    public float detectionDistance;
    public LayerMask targetLayerMask;
    //public float detectionAngle;
    public Vector2 detectionSize;
    public Color color;
}


public class PhysicalDetection : MonoBehaviour
{
    [SerializeField]
    private List<DetectionInfo> detectionInfos = new List<DetectionInfo>();
    [SerializeField]
    private bool isShow = false;
    public Dictionary<Transform, RaycastHit2D[]> DicRaycastHits = new Dictionary<Transform, RaycastHit2D[]>();
    public Dictionary<Transform, Collider2D[]> DicColliders = new Dictionary<Transform, Collider2D[]>();

    private void Update()
    {
        foreach (var detectionInfo in detectionInfos)
        {
            switch (detectionInfo.directionType)
            {
                case DirectionType.Cast:
                    var RaycastHits = CastDetection(detectionInfo);
                    DicRaycastHits[detectionInfo.detectionPosition] = RaycastHits;
                    break;
                case DirectionType.Overlap:
                    var Collders = OverlapDetection(detectionInfo);
                    DicColliders[detectionInfo.detectionPosition] = Collders;
                    break;
            }

        }
    }

    private RaycastHit2D[] CastDetection(DetectionInfo detectionInfo)
    {
        var position = detectionInfo.detectionPosition.position;
        var distance = detectionInfo.detectionDistance;
        var direction = detectionInfo.detectionDirection;
        var layerMark = detectionInfo.targetLayerMask;
        switch (detectionInfo.directionShape)
        {
            case DirectionShape.Box:
                return Physics2D.BoxCastAll(position, detectionInfo.detectionSize, 0, direction, distance, layerMark);

            case DirectionShape.Circle:
                return Physics2D.CircleCastAll(position, distance, direction, layerMark);

            case DirectionShape.Ray:
                return Physics2D.RaycastAll(position, direction, distance, layerMark);

            default:
                return null;
        }
    }

    private Collider2D[] OverlapDetection(DetectionInfo detectionInfo)
    {
        var position = detectionInfo.detectionPosition.position;
        var distance = detectionInfo.detectionDistance;
        var direction = detectionInfo.detectionDirection;
        var layerMark = detectionInfo.targetLayerMask;
        switch (detectionInfo.directionShape)
        {
            case DirectionShape.Box:
                return Physics2D.OverlapBoxAll(position, detectionInfo.detectionSize, 0, layerMark);

            case DirectionShape.Circle:
                return Physics2D.OverlapCircleAll(position, distance, layerMark);

            case DirectionShape.Point:
                return Physics2D.OverlapPointAll(position, layerMark);
            default:
                return null;
        }
    }


    private void OnDrawGizmos()
    {
        if (!isShow)
            return;
        foreach (var detectionInfo in detectionInfos)
        {
            var position = detectionInfo.detectionPosition.position;
            var targetPosition = (Vector2)detectionInfo.detectionPosition.position + (detectionInfo.detectionDirection * detectionInfo.detectionDistance);
            var size = detectionInfo.detectionSize;
            Gizmos.color = detectionInfo.color;
            switch (detectionInfo.directionType)
            {
                case DirectionType.Overlap:
                    switch (detectionInfo.directionShape)
                    {
                        case DirectionShape.Box:
                            Gizmos.DrawCube(position, size);
                            break;
                        case DirectionShape.Circle:
                            Gizmos.DrawSphere(position, detectionInfo.detectionDistance);
                            break;
                        case DirectionShape.Point:
                            Gizmos.DrawSphere(position, .1f);
                            break;
                    }

                    break;
                case DirectionType.Cast:
                    Gizmos.DrawLine(position, targetPosition);
                    switch (detectionInfo.directionShape)
                    {
                        case DirectionShape.Ray:
                            break;
                        case DirectionShape.Box:
                            Gizmos.DrawCube(position, size);
                            break;
                        case DirectionShape.Circle:
                            Gizmos.DrawSphere(position, detectionInfo.detectionDistance);
                            break;

                    }

                    break;
            }

        }
    }




}




