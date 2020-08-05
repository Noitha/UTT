using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UrbanTimeTravel.MobileUtils
{
    public class RotatorObject : MonoBehaviour
    {

        #region Variables
        RectTransform imageTransform;
        RectTransform basePosition;
        float turnDuration = .1f;
        bool rotating = false;
        #endregion


        #region Main Methods
        private void Start()
        {
            imageTransform = gameObject.GetComponent<RectTransform>();
            basePosition = imageTransform;
        }
        #endregion

        #region Helper Methods
        public void FlipScreen(DeviceOrientation previousOrientation, DeviceOrientation currentOrientation)
        {
            switch (previousOrientation)
            {
                case DeviceOrientation.FaceUp:
                case DeviceOrientation.FaceDown:
                case DeviceOrientation.Portrait:
                    switch (currentOrientation)
                    {
                        case DeviceOrientation.PortraitUpsideDown:
                            TurnImage(180);
                            break;
                        case DeviceOrientation.LandscapeLeft:
                            TurnImage(90);
                            break;
                        case DeviceOrientation.LandscapeRight:
                            TurnImage(-90);
                            break;
                        default:
                            break;
                    }
                    break;
                case DeviceOrientation.PortraitUpsideDown:
                    switch (currentOrientation)
                    {
                        case DeviceOrientation.FaceUp:
                        case DeviceOrientation.FaceDown:
                        case DeviceOrientation.Portrait:
                            TurnImage(0);
                            break;
                        case DeviceOrientation.LandscapeLeft:
                            TurnImage(90);
                            break;
                        case DeviceOrientation.LandscapeRight:
                            TurnImage(-90);
                            break;
                        default:
                            break;
                    }
                    break;
                case DeviceOrientation.LandscapeLeft:
                    switch (currentOrientation)
                    {
                        case DeviceOrientation.FaceUp:
                        case DeviceOrientation.FaceDown:
                        case DeviceOrientation.Portrait:
                            TurnImage(0);
                            break;
                        case DeviceOrientation.PortraitUpsideDown:
                            TurnImage(180);
                            break;
                        case DeviceOrientation.LandscapeRight:
                            TurnImage(-90);
                            break;
                        default:
                            break;
                    }
                    break;
                case DeviceOrientation.LandscapeRight:
                    switch (currentOrientation)
                    {
                        case DeviceOrientation.FaceUp:
                        case DeviceOrientation.FaceDown:
                        case DeviceOrientation.Portrait:
                            TurnImage(0);
                            break;
                        case DeviceOrientation.PortraitUpsideDown:
                            TurnImage(180);
                            break;
                        case DeviceOrientation.LandscapeLeft:
                            TurnImage(90);
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
        }


        private void TurnImage(float turnAngle)
        {
            if (!rotating)
            {
                StartCoroutine(RotateAround(turnAngle));
            }
            else
            {
                StartCoroutine(WaitAndRotate(90));
            }
        }

        private IEnumerator WaitAndRotate(float turnAngle)
        {
            yield return new WaitUntil(() => rotating = false);
            yield return StartCoroutine(RotateAround(turnAngle));
        }

        private IEnumerator RotateAround(float turnAngle)
        {
            yield return StartCoroutine(Rotate(turnAngle));
        }

        private IEnumerator Rotate(float turnAngle)
        {
            rotating = true;
            Quaternion startRotation = basePosition.rotation;
            Quaternion endRotation = Quaternion.AngleAxis(turnAngle, Vector3.back);

            for (float t = 0; t < turnDuration; t += Time.deltaTime)
            {
                imageTransform.transform.rotation = Quaternion.Lerp(startRotation, endRotation, t / turnDuration);
                yield return null;
            }

            imageTransform.transform.rotation = endRotation;
            rotating = false;
            basePosition = imageTransform;
        }

        #endregion;
    }
}

