using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Sources.Scripts.Dungeon
{
    public class DungeonEnemy : MonoBehaviour
    {
        [SerializeField] private Tag playerTag;
        [SerializeField] private LayerMask whatIsWall;
        [SerializeField] private LayerMask whatIsFloor;
        [SerializeField] private float movementSpeed = 5f;
        [SerializeField] private float chaseSpeed = 8f;
        [SerializeField] private float patrolMinTime;
        [SerializeField] private float patrolMaxTime;
        [SerializeField] private float agroRange;
        
        
        private GameObject _playerGo;
        private bool _isMoving;
        private Vector2 _targetPos;
        private List<Vector2> _availableTilesToMoveList = new List<Vector2>();
        private List<Node> _nodesList = new List<Node>();
        
        private Vector2 _hitSize;
        private void Start()
        {
            _hitSize = Vector2.one * 0.8f;
            _targetPos = transform.position;
            _playerGo = gameObject.FindWithTag(playerTag);
            StartCoroutine(Movement());

        }



        private void PatrolDungeon()
        {
            _availableTilesToMoveList.Clear();

            Collider2D hitUp = Physics2D.OverlapBox(_targetPos + Vector2.up, _hitSize, 0, whatIsWall);
            if (!hitUp)
            {
                _availableTilesToMoveList.Add(Vector2.up);
            }

            Collider2D hitRight = Physics2D.OverlapBox(_targetPos + Vector2.right, _hitSize, 0, whatIsWall);
            if (!hitRight)
            {
                _availableTilesToMoveList.Add(Vector2.right);
            }

            Collider2D hitDown = Physics2D.OverlapBox(_targetPos + Vector2.down, _hitSize, 0, whatIsWall);
            if (!hitDown)
            {
                _availableTilesToMoveList.Add(Vector2.down);
            }

            Collider2D hitLeft = Physics2D.OverlapBox(_targetPos + Vector2.left, _hitSize, 0, whatIsWall);
            if (!hitLeft)
            {
                _availableTilesToMoveList.Add(Vector2.left);
            }

            if (_availableTilesToMoveList.Count > 0)
            {
                int randomDirectionIndex = Random.Range(0, _availableTilesToMoveList.Count);
                _targetPos += _availableTilesToMoveList[randomDirectionIndex];
            }

            StartCoroutine(SmoothMove(Random.Range(patrolMinTime, patrolMaxTime)));
        }

        private IEnumerator SmoothMove(float speed)
        {
            _isMoving = true;
            while (Vector2.Distance(transform.position, _targetPos) > 0.01f)
            {
               //moving 1 tile at a time
               transform.position = Vector2.MoveTowards(transform.position, _targetPos, movementSpeed * Time.deltaTime);
               yield return null; //  waits 1 frame
            }
            transform.position = _targetPos;
            yield return new WaitForSeconds(speed);
            _isMoving = false;
        }

        private IEnumerator Movement()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.1f);
                if (!_isMoving)
                {
                    float distanceToPlayer = Vector2.Distance(transform.position, _playerGo.transform.position);
                    if (distanceToPlayer <= agroRange)
                    {
                        if (distanceToPlayer <= 1.1f) // save distance (less than sqrt(2))
                        {
                            Debug.Log(name+" attacks Player");
                            yield return new WaitForSeconds(Random.Range(0.5f, 1.15f));
                        }
                        else
                        {
                            Vector2 nextPos = FindNextStep(transform.position, _playerGo.transform.position);
                            if (nextPos != _targetPos)
                            {
                                //chase
                                _targetPos = nextPos;
                                StartCoroutine(SmoothMove(chaseSpeed));
                            }
                            else
                            {
                                PatrolDungeon();
                            }
                        }
                    }
                    else
                    {
                        PatrolDungeon();
                    }
                }
            }
        }

        private Vector2 FindNextStep(Vector2 startPos, Vector2 targetPos)
        {
            int listIndex = 0;
            Vector2 currentPos = startPos;
            _nodesList.Clear();
            _nodesList.Add(new Node(startPos, startPos));
            
            while (currentPos != targetPos && listIndex < 1000 && _nodesList.Count > 0)
            {
                //check walkable tiles in 4 directions
                CheckNode(currentPos + Vector2.up, currentPos);
                CheckNode(currentPos + Vector2.right, currentPos);
                CheckNode(currentPos + Vector2.down, currentPos);
                CheckNode(currentPos + Vector2.left, currentPos);
                listIndex++;
                if (listIndex < _nodesList.Count)
                {
                    currentPos = _nodesList[listIndex].Position;
                }
            }

            if (currentPos == targetPos)
            {
                _nodesList.Reverse(); // crawls backwards through the list
                for (int i = 0; i < _nodesList.Count; i++)
                {
                    if (currentPos == _nodesList[i].Position)
                    {
                        if (_nodesList[i].Parent == startPos)
                        {
                            return currentPos;
                        }
                        currentPos = _nodesList[i].Parent;
                        
                    }
                }
            }
            
            return startPos;
        }

        private void CheckNode(Vector2 pointPosition, Vector2 pointParent)
        {
            
            Collider2D hit = Physics2D.OverlapBox(pointPosition, _hitSize, 0, whatIsWall);
            if (!hit) // walkable area
            {
                _nodesList.Add(new Node(pointPosition, pointParent));
            }
        }
    }
}