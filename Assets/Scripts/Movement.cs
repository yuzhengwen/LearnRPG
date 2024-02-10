using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

namespace YuzuValentine
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(NavMeshAgent))]
    public class Movement : MonoBehaviour
    {
        private NavMeshAgent navMeshAgent;
        private float rotateSpeedMovement = 0.05f;
        private float rotateVelocity;

        private Animator anim;
        float motionSmoothTime = 0.1f;
        private Camera mainCamera;

        private GameObject target;
        private float stoppingDist = 1.5f;

        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            anim = GetComponent<Animator>();
            mainCamera = Camera.main;
        }
        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            Animation();
            Move();
        }

        private void Move()
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity))
                {
                    if (hit.collider.CompareTag("Terrain"))
                    {
                        MoveToPosition(hit.point);
                        target = null;
                    }
                    else if (hit.collider.CompareTag("Enemy"))
                    {
                        target = hit.collider.gameObject;
                        MoveToObject(target);
                    }
                }
            }
            // keep following as long as there is a target
            if (target != null)
                if (Vector3.Distance(target.transform.position, transform.position) > stoppingDist)
                    MoveToObject(target);
        }
        public void MoveToObject(GameObject go)
        {
            MoveToPosition(go.transform.position, stoppingDist);
        }
        public void MoveToPosition(Vector3 position, float stoppingDist = 0)
        {
            navMeshAgent.SetDestination(position);
            navMeshAgent.stoppingDistance = stoppingDist;
            MoveRotation(position);
        }
        public void MoveRotation(Vector3 position)
        {
            Quaternion lookAt = Quaternion.LookRotation(position - transform.position);
            float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y, lookAt.eulerAngles.y, ref rotateVelocity, rotateSpeedMovement * (Time.deltaTime * 5));
            transform.eulerAngles = new Vector3(0, rotationY, 0);
        }

        private void Animation()
        {
            float speed = navMeshAgent.velocity.magnitude / navMeshAgent.speed;
            anim.SetFloat("Speed", speed, motionSmoothTime, Time.deltaTime);
        }
    }
}
