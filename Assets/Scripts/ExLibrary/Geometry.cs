using UnityEngine;

public class Geometry
{
    /*
    두 3차원 점을 이은 간선을 나타내는 구조체
    각 점은 u, v로 표기
    */
    public struct Edge3
    {
        public Vector3 pointU, pointV;

        public Edge3(Vector3 _pointU, Vector3 _pointV)
        {
            pointU = _pointU;
            pointV = _pointV;
        }
    }

    /*
    두 2차원 점을 이은 간선을 나타내는 구조체
    각 점은 u, v로 표기
    */
    public struct Edge2
    {
        public Vector2 pointU, pointV;

        public Edge2(Vector2 _pointU, Vector2 _pointV)
        {
            pointU = _pointU;
            pointV = _pointV;
        }
    }
}
