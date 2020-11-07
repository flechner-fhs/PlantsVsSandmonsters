using System.Collections;
using System.Collections.Generic;
using System.Net.Cache;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathFinder : MonoBehaviour
{
    private static PathFinder instance;

    public static readonly float PATH_AGE = 1;

    public static PathFinder Instance { get => instance; }

    public Tilemap Obstacles;

    public struct Path
    {
        public GameObject Target;
        public List<Vector3> Directions;
        public Vector3Int TargetPos;
        public float age;

    }

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        instance = this;
    }

    public Path GetPathTo(GameObject me, GameObject target)
    {
        Vector3Int myPos = Obstacles.WorldToCell(me.transform.position);
        Vector3Int targetPos = Obstacles.WorldToCell(target.transform.position);

        List<Node> newNodes = new List<Node>();
        List<Node> checkedNodes = new List<Node>();

        //Set Start Position
        if(Obstacles.GetTile(myPos) != null)
        {
            Vector3 myPosActual = me.transform.position;
            Vector3 tilePosActual = Obstacles.CellToWorld(myPos) + new Vector3(.5f, .5f, 0);
            Vector3 dir = (myPosActual - tilePosActual).normalized;

            if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
            {
                newNodes.Add(new Node(myPos + Vector3Int.right * (int)Mathf.Sign(dir.x), 0, null));
                if (Obstacles.GetTile(newNodes[0].Position) != null)
                    newNodes[0] = new Node(myPos + Vector3Int.up * (int)Mathf.Sign(dir.y), 0, null);
            }
            else
            {
                newNodes.Add(new Node(myPos + Vector3Int.up * (int)Mathf.Sign(dir.y), 0, null));
                if (Obstacles.GetTile(newNodes[0].Position) != null)
                    newNodes[0] = new Node(myPos + Vector3Int.right * (int)Mathf.Sign(dir.x), 0, null);
            }
        }
        else
        {
            newNodes.Add(new Node(myPos, 0, null));
        }

        int i = 0;
        int max = 1000;
        while(newNodes.Count > 0)
        {
            if (i++ > max) return new Path();

            Node node = newNodes[0];

            if (Obstacles.GetTile(node.Position) == null)
            {
                if (node.Position.x != targetPos.x || node.Position.y != targetPos.y)
                {
                    AddIfAbsent(ref newNodes, checkedNodes, new Node(node.Position + Vector3Int.left, node.Distance + 1, node));
                    AddIfAbsent(ref newNodes, checkedNodes, new Node(node.Position + Vector3Int.right, node.Distance + 1, node));
                    AddIfAbsent(ref newNodes, checkedNodes, new Node(node.Position + Vector3Int.up, node.Distance + 1, node));
                    AddIfAbsent(ref newNodes, checkedNodes, new Node(node.Position + Vector3Int.down, node.Distance + 1, node));
                }
                else
                {
                    Path returnValue = new Path();
                    returnValue.Target = target;
                    returnValue.TargetPos = targetPos;
                    returnValue.Directions = new List<Vector3>();
                    returnValue.age = PATH_AGE;
                    while (true)
                    {
                        returnValue.Directions.Insert(0, Obstacles.CellToWorld(node.Position) + new Vector3(.5f, .5f, 0));
                        node = node.Parent;
                        if(node == null || node.Parent == null)
                        {
                            returnValue.Directions = SimplifyPath(returnValue.Directions);
                            return returnValue;
                        }
                    }
                }
            }

            newNodes.Remove(node);
            checkedNodes.Add(node);
            newNodes.Sort((a, b) => {
                float distanceA = a.Distance + (a.Position - targetPos).magnitude;
                float distanceB = b.Distance + (b.Position - targetPos).magnitude;
                return distanceA > distanceB ? 1 : distanceA == distanceB ? 0 : - 1;
                });
        }

        return new Path();
    }

    private List<Vector3> SimplifyPath(List<Vector3> list)
    {
        List<Vector3> newList = new List<Vector3>();

        bool isX = false;
        bool checking = false;
        Vector3 startNode = list[0];
        Vector3 lastNode = list[0];
        list.RemoveAt(0);

        while (list.Count > 0)
        {
            //New Direction
            if(!checking)
            {
                //Where is the new Direction
                isX = startNode.x == list[0].x;
                checking = true;

                //If there is at least one more node that is in the same direction follow
                if(list.Count >= 2 && ((startNode.x == list[1].x && isX) || (startNode.y == list[1].y && !isX)))
                {
                    lastNode = list[0];
                    list.RemoveAt(0);
                }
                //Otherwise, just add it and start a new direction
                else
                {
                    newList.Add(startNode);
                    startNode = list[0];
                    list.RemoveAt(0);
                    checking = false;
                }
            }
            else
            {
                //If the node doesn't break the direction delete it
                if ((startNode.x == list[0].x && isX) || (startNode.y == list[0].y && !isX))
                {
                    lastNode = list[0];
                    list.RemoveAt(0);
                }
                //If it does, add the start node to path and start counting from new node
                else
                {
                    newList.Add(startNode);
                    startNode = lastNode;
                    lastNode = list[0];
                    list.RemoveAt(0);
                    checking = false;
                }
            }
        }
        newList.Add(startNode);
        //newList.RemoveAt(0);
        return newList;
    }

    private void AddIfAbsent(ref List<Node> newList, List<Node> checkedList, Node node)
    {
        if (checkedList.Find(x => x.Position.x == node.Position.x && x.Position.y == node.Position.y) == null)
            newList.Add(node);
    }

    private class Node
    {
        public Vector3Int Position;
        public float Distance;
        public Node Parent;

        public Node(Vector3Int Position, float Distance, Node Parent)
        {
            this.Position = Position;
            this.Distance = Distance;
            this.Parent = Parent;
        }
    }
}
