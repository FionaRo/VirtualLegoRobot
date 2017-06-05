using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.UnityScripts
{
    class Moving : MonoBehaviour
    {
        public WheelCollider Right, Left;
        private UnitTypes unitDistance;
        private float Distance;
        private float time;
        public Rigidbody car;
        private float rpm;
        private float speed1, speed2;
        public Transform COM;
        private bool _work = false;

        void Start()
        {
            Transform centerOfMass = transform.Find("CenterOfMass");
            car.centerOfMass = COM.localPosition;
            Debug.Log(COM.localPosition);

        }

        void Update()
        {
            if (GlobalVariables.ImportData != null)
            {
                Debug.Log("Данные получены");
                Right.motorTorque = (float)GlobalVariables.ImportData.Motor1.Power;
                Left.motorTorque = (float)GlobalVariables.ImportData.Motor2.Power;
                unitDistance = GlobalVariables.ImportData.Distance.Unit;
                rpm = (float)GlobalVariables.ImportData.Motor1.Power / 100 * 100;
                time = 0;
                speed1 = (float)GlobalVariables.ImportData.Motor1.Power;
                speed2 = (float)GlobalVariables.ImportData.Motor2.Power;
                GlobalVariables.ImportData = null;
                car.velocity = 40 * (car.velocity.normalized);
                _work = true;
                return;
            }
            if (!_work) return;
            Debug.Log(Right.rpm + " - " + rpm);
            time += Time.deltaTime;
           if (Right.rpm - rpm > 5)
            {
                Right.brakeTorque = 50;
                Left.brakeTorque = 50;
                //Right.motorTorque = Right.rpm - rpm;
                //Left.motorTorque = Right.rpm - rpm;
            }
            else if (rpm - Right.rpm > 5)
            {
                //Right.motorTorque = rpm - Right.rpm;
                //Left.motorTorque = rpm - Left.rpm;
                Right.brakeTorque = 0;
                Left.brakeTorque = 0;
            }
            else
            {
                Right.brakeTorque = 0;
                Left.brakeTorque = 0;

            }

            if (GlobalVariables.ExportData.Count > 0)
            {
                //Debug.Log(Right.rpm);
                switch (unitDistance)
                {
                    case UnitTypes.Degrees:
                        if (rpm / 60 * time * 360 >= GlobalVariables.ExportData.Peek().ValueCompare)
                        {
                            Right.brakeTorque = 100;
                            Left.brakeTorque = 100;
                            GlobalVariables.ExportData.Peek().CanContinue = true;
                            _work = false;
                        }
                        break;
                    case UnitTypes.Rotations:
                        if (rpm / 60 * time >= GlobalVariables.ExportData.Peek().ValueCompare)
                        {
                            Right.brakeTorque = 100;
                            Left.brakeTorque = 100;
                            GlobalVariables.ExportData.Peek().CanContinue = true;
                            _work = false;
                        }
                        break;
                    case UnitTypes.Seconds:
                        if (time >= GlobalVariables.ExportData.Peek().ValueCompare)
                        {
                            Right.brakeTorque = 100;
                            Left.brakeTorque = 100;
                            GlobalVariables.ExportData.Peek().CanContinue = true;
                            _work = false;
                        }
                        break;
                }
            }

        }
    }
}
