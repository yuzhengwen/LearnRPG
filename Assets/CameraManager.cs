using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace YuzuValentine
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private CinemachineVirtualCamera virtualCamera;
        [SerializeField] private bool usingVirtual = true;

        [SerializeField] private float cameraPanSpeed = 10f;
        [SerializeField] private float boundary = 10f;

        // Update is called once per frame
        void Update()
        {
            // when virtual camera enabled, camera follows player
            // when only using main camera, camera is fixed and only moves with mouse panning
            if (Input.GetKeyDown(KeyCode.Space))
            {
                usingVirtual = !usingVirtual;
                if (usingVirtual)
                    virtualCamera.gameObject.SetActive(true);
                else
                    virtualCamera.gameObject.SetActive(false);
            }
            // allow mouse to pan main camera
            if (!usingVirtual)
            {
                float x = Input.mousePosition.x;
                float y = Input.mousePosition.y;
                if (x < boundary)
                    mainCamera.transform.position += Vector3.left * Time.deltaTime * cameraPanSpeed;
                else if (x > Screen.width - boundary) mainCamera.transform.position += Vector3.right * Time.deltaTime * cameraPanSpeed;
                if (y < boundary)
                    mainCamera.transform.position += Vector3.back * Time.deltaTime * cameraPanSpeed;
                else if (y > Screen.height - boundary) mainCamera.transform.position += Vector3.forward * Time.deltaTime * cameraPanSpeed;
            }
        }
    }
}
