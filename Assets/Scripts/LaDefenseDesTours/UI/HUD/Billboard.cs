using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.LaDefenseDesTours
{
    class Billboard : MonoBehaviour
    {
        private Camera mainCamera;

        void Start()
        {
            mainCamera = Camera.main;
        }

        void LateUpdate()
        {
            if (mainCamera != null)
            {
                transform.LookAt(transform.position + mainCamera.transform.forward);
            }
        }
    }
}
