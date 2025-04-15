using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarExample : MonoBehaviour
{
    class Node
    {
        public int x, y;         // 노드의 좌표
        public int gCost;        // 시작점에서 여기까지의 거리
        public int hCost;        // 목표까지의 추정 거리
        public Node parent;      // 어디서 왔는지 저장 (경로 추적용)

        public int fCost => gCost + hCost;  // 총 비용

        public bool walkable = true;        // 지나갈 수 있는지

        public Node(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    int gridSize = 5; // 5x5 그리드
    Node[,] grid;
}
