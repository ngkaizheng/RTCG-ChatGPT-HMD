using Oculus.Interaction.HandGrab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Oculus.Interaction.Demo
{
    public class torchlight : MonoBehaviour, IHandGrabUseDelegate
    {
        [Header("Input")]
        [SerializeField]
        private Transform _trigger;
        [SerializeField]
        private AnimationCurve _triggerRotationCurve;
        [SerializeField]
        private SnapAxis _axis = SnapAxis.X;
        [SerializeField]
        [Range(0f, 1f)]
        private float _releaseThresold = 0.3f;
        [SerializeField]
        [Range(0f, 1f)]
        private float _fireThresold = 0.9f;
        [SerializeField]
        private float _triggerSpeed = 3f;
        [SerializeField]
        private AnimationCurve _strengthCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
        [SerializeField]
        public AudioSource audioSource;

        [Header("Torch Light")]
        [SerializeField]
        private Light _torchLight;

        private bool _wasFired = false;
        private float _dampedUseStrength = 0;
        private float _lastUseTime;
        public bool isOn = false;

        #region input

        private void ToggleTorchLight()
        {
            isOn = !isOn;
            _torchLight.enabled = isOn;
            audioSource.Play();
        }

        private void UpdateTriggerRotation(float progress)
        {
            float value = _triggerRotationCurve.Evaluate(progress);
            Vector3 angles = _trigger.localEulerAngles;
            if ((_axis & SnapAxis.X) != 0)
            {
                angles.x = value;
            }
            if ((_axis & SnapAxis.Y) != 0)
            {
                angles.y = value;
            }
            if ((_axis & SnapAxis.Z) != 0)
            {
                angles.z = value;
            }
            _trigger.localEulerAngles = angles;
        }

        #endregion

        #region output

        public void BeginUse()
        {
            _dampedUseStrength = 0f;
            _lastUseTime = Time.realtimeSinceStartup;
        }

        public void EndUse()
        {
            // Do nothing for now
        }

        public float ComputeUseStrength(float strength)
        {
            float delta = Time.realtimeSinceStartup - _lastUseTime;
            _lastUseTime = Time.realtimeSinceStartup;
            if (strength > _dampedUseStrength)
            {
                _dampedUseStrength = Mathf.Lerp(_dampedUseStrength, strength, _triggerSpeed * delta);
            }
            else
            {
                _dampedUseStrength = strength;
            }
            float progress = _strengthCurve.Evaluate(_dampedUseStrength);
            UpdateTriggerProgress(progress);
            return progress;
        }

        private void UpdateTriggerProgress(float progress)
        {
            UpdateTriggerRotation(progress);

            if (progress >= _fireThresold && !_wasFired)
            {
                _wasFired = true;
                ToggleTorchLight();
            }
            else if (progress <= _releaseThresold)
            {
                _wasFired = false;
            }
        }

        #endregion
    }
}
