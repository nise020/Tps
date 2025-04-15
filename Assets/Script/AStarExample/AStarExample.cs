using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarExample : MonoBehaviour
{
    class Node
    {
        public int x, y;         // ����� ��ǥ
        public int gCost;        // ���������� ��������� �Ÿ�
        public int hCost;        // ��ǥ������ ���� �Ÿ�
        public Node parent;      // ��� �Դ��� ���� (��� ������)

        public int fCost => gCost + hCost;  // �� ���

        public bool walkable = true;        // ������ �� �ִ���

        public Node(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    int gridSize = 5; // 5x5 �׸���
    Node[,] grid;
}
