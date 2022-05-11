using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [Range(0, 360)]
    public float viewAngle = 360;
    public float viewRadius = 40;
    public LayerMask allyMask, opposingMask, obstacleMask;

    [Range(0, 2)]
    public float samplingRate = 0.08f;
    public float edgeDstThreshold = 1f;

    Mesh viewMesh;
    public MeshFilter viewMeshFilter;

    List<Transform> visibleEnemies = new List<Transform>();
    List<Vector3> capturedVertices = new List<Vector3>();

    /*
    fov 메쉬를 초기화하고 시야 내의 적을 찾는 코루틴 호출
    */
    private void Start()
    {
        viewMesh = new Mesh();
        viewMesh.name = "view";
        viewMeshFilter.mesh = viewMesh;

        // StartCoroutine("ScanEnemiesWithDelay", 0.1f);
        StartCoroutine(ScanEnemiesWithDelay(0.05f));
    }

    private void LateUpdate()
    {
        DrawFieldOfView();
    }

    /*
    viewRadius를 원지름으로 한 원 반경 내에서 시야에 닿는 적 오브젝트를 visibleEnemies에 저장
    */
    private void ScanVisibleEnemies()
    {
        foreach (Transform e in visibleEnemies)
        {
            Managers.Visible.SubtractVisible(e.gameObject);
        }
        visibleEnemies.Clear();

        Collider[] candidates = Physics.OverlapSphere(transform.position, viewRadius, opposingMask);

        foreach (Collider e in candidates)
        {
            Transform target = e.transform;
            Vector3 dirToEnemy = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.position, target.position) < viewAngle / 2)
            {
                float distToEnemy = (target.position - transform.position).magnitude;
                if (Physics.Raycast(transform.position, dirToEnemy, distToEnemy, obstacleMask))
                {
                    Managers.Visible.AddVisible(target.gameObject);
                    visibleEnemies.Add(target);
                }
            }
        }
    }

    /*
    프레임마다 적을 찾지 않고 delay 기반의 코루틴을 호출하여 최적화
    */
    IEnumerator ScanEnemiesWithDelay(float delay)
    {
        while (true)
        {
            ScanVisibleEnemies();
            yield return new WaitForSecondsRealtime(delay);
        }
    }

    /*
    설정한 영역에 대해 raycast를 쏴 fov mesh를 계산함
    */
    private void DrawFieldOfView()
    {
        capturedVertices.Clear();
        int steps = Mathf.RoundToInt(viewAngle * samplingRate);
        float anglePiece = viewAngle / steps;
        
        CastFootprint prevVertex = CaptureRaycast(transform.eulerAngles.y - viewAngle / 2);

        capturedVertices.Add(prevVertex.pos);

        for (int i = 1; i <= steps; i++)
        {
            float currAngle = transform.eulerAngles.y - viewAngle / 2 + i * anglePiece;
            CastFootprint nextVertex = CaptureRaycast(currAngle);

            bool exceeded = Mathf.Abs(nextVertex.dist - prevVertex.dist) > edgeDstThreshold;
            if ((prevVertex.isHit != nextVertex.isHit) || (prevVertex.isHit && nextVertex.isHit && exceeded))
            {
                Geometry.Edge3 e = EdgeInterpolation(prevVertex, nextVertex);
                float eps = 0.0001f;

                if (e.pointU != Vector3.zero)
                {
                    capturedVertices.Add(e.pointU);
                }
                if (e.pointV != Vector3.zero && (e.pointV - e.pointU).sqrMagnitude > eps)
                {
                    capturedVertices.Add(e.pointV);
                }
            }

            capturedVertices.Add(nextVertex.pos);
            prevVertex = nextVertex;
        }

        // mesh 구축 코드
        int totalVertices = capturedVertices.Count + 1;
        Vector3[] vertices = new Vector3[totalVertices];
        vertices[0] = Vector3.zero;
        int[] triangles = new int[(totalVertices - 2) * 3];

        for (int i = 0; i < totalVertices - 2; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(capturedVertices[i]);
            triangles[i * 3] = 0;
            triangles[i * 3 + 1] = i + 1;
            triangles[i * 3 + 2] = i + 2;
        }
        vertices[totalVertices - 1] = transform.InverseTransformPoint(capturedVertices[totalVertices - 2]);

        viewMesh.Clear();
        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
    }

    /*
    조밀한 fov mesh 구현을 위해 두 점 사이를 보간하는 점(최대 2개)를 찾음
    */
    private Geometry.Edge3 EdgeInterpolation(CastFootprint minCast, CastFootprint maxCast)
    {
        float minAngle = minCast.angle, maxAngle = maxCast.angle;
        Vector3 u = Vector3.zero, v = Vector3.zero;
        
        // 이진 탐색에 필요한 iteration 횟수를 100으로 정함
        // 일반적으로 실수 값에 대해 100회 하는 것이 좋다고 알려짐
        for (int i = 0; i < 100; i++)
        {
            float midAngle = minAngle + (maxAngle - minAngle) / 2;
            CastFootprint castShot = CaptureRaycast(midAngle);

            float distDiff = Mathf.Abs(maxCast.dist - minCast.dist);
            bool exceeded = edgeDstThreshold < distDiff;
            if (!exceeded && minCast.isHit == castShot.isHit)
            {
                minAngle = midAngle;
                u = castShot.pos;
            }
            else
            {
                maxAngle = midAngle;
                v = castShot.pos;
            }
        }

        return new Geometry.Edge3(u, v);
    }

    /*
    globalAngle 방향으로 쏜 raycast의 결과를 CastFoorprint struct로 반환
    */
    private CastFootprint CaptureRaycast(float globalAngle)
    {
        Vector3 direction = DirFromAngle(globalAngle, true);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, direction, out hit, viewRadius, obstacleMask))
        {
            return new CastFootprint(true, hit.point, hit.distance, globalAngle);
        }
        else
        {
            Vector3 destination = transform.position + viewRadius * direction;
            return new CastFootprint(false, destination, viewRadius, globalAngle);
        }
    }

    /*
    주어진 극좌표 각에 대해(y축 오일러 각) (x, 0, z) 3차원 방향 벡터를 구함
    */
    public Vector3 DirFromAngle(float angleDegrees, bool isWorld)
    {
        if (!isWorld)
        {
            angleDegrees += transform.eulerAngles.y;
        }

        float rad = (-angleDegrees + 90) * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(rad), 0, Mathf.Sin(rad)).normalized;
    }

    /*
    raycast 결과를 담은 struct
    */
    public struct CastFootprint
    {
        public bool isHit;
        public Vector3 pos;
        public float dist;
        public float angle;

        public CastFootprint(bool _hit, Vector3 _pos, float _dist, float _angle)
        {
            isHit = _hit;
            pos = _pos;
            dist = _dist;
            angle = _angle;
        }
    }
}
