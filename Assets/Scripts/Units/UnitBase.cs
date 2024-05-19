using AStar;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBase : MonoBehaviour, IUnit
{
   

    #region Fields And Variables
    [SerializeField] private UnitStats unitStats=new UnitStats();
    [SerializeField] private Node currentNode;
    [SerializeField] private Collider[] coll=new Collider[1];
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] private Animator animator; // Animator reference
    [SerializeField] private string ParamterOfMove = "Speed"; 
    [SerializeField] private float ParamterValWhileMoving = 4f;
    private Stack<Node> path = new Stack<Node>();
    Coroutine moveRoutine;
    // Delegate for unit events
    public delegate void UnitEventHandler(UnitBase unit);

    // Event triggered when the unit finishes a task
    public event UnitEventHandler OnTaskCompleted;
    #endregion

    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion
    #region Custom Methods
      public UnitStats GetUnitStats()
        {
           return unitStats;
        }

        public void Move(Stack<Node> nodes)
        {
        path = nodes;
        StopMoving();
        moveRoutine = StartCoroutine(MoveAlong());
        }
        IEnumerator MoveAlong()
        {
        if (animator != null)
        {
            animator.SetFloat(ParamterOfMove, ParamterValWhileMoving); // Set isMoving to false
        }
        while (true)
            {
            if (currentNode == null)
            {
                if (path.Count == 0)
                {
                    OnTaskCompleted?.Invoke(this);
                    StopMoving();
                   yield break; // Path is completed
                }
                currentNode = path.Pop();
            }
        
                Vector3 targetPosition = currentNode.transform.position;
                transform.LookAt(targetPosition);
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

                if (transform.position == targetPosition)
                {
                    currentNode = null; // Reached the node, move to the next one
                }
                yield return new WaitForEndOfFrame();
                }
        }
        /// <summary>
        /// Sets Unit Stats
        /// </summary>
        /// <param name="Stats"> Stats to set</param>
        public void SetUnitStats(UnitStats Stats)
        {
        if (Stats != null) { unitStats = Stats; }
       
        }

        public void StopMoving()
        {
        if (animator != null)
        {
            animator.SetFloat(ParamterOfMove, 0); // Set isMoving to false
        }
        if (moveRoutine != null) StopCoroutine(moveRoutine);
        DetectCurrentNode();
        }
        /// <summary>
        /// Generates random Unit Stats in case of more complex game it would  be diffrent 
        /// </summary>
        public void UnitSetup()
        {
            unitStats.GenerateRandomStats();
        }
        public void DetectCurrentNode()
        {
            coll[0] = null; 
            Physics.OverlapSphereNonAlloc(transform.position, 0.3f, coll, GridManager.GetInstance().GetLayerMask(),QueryTriggerInteraction.Collide);
            if (coll[0] != null)
            {
               Node n= coll[0].GetComponentInParent<Node>();
            if(n != null) { currentNode = n; }
            }
        }
        public Node GetCurrentNode() { return currentNode; }
    /// <summary>
    /// After Leading Unit ends his task all Units stops
    /// </summary>
    public void ReceiveNotification()
    {
        StopMoving();
    }
    // implemented just for IUnit
    public void ReceiveNotification(IUnit unit)
    {
      //
    }
    #endregion
}
