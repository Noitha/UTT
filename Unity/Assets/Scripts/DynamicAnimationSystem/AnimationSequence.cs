using System;
using UnityEngine;

namespace DynamicAnimationSystem
{
    [Serializable]
    public class AnimationSequence
    {
        public string mechanism;
        public string animationName;
        public AnimationParameter[] parameters;
        
        public Vector3 GetVector3(int parameterIndex)
        {
            var vectorAsString = parameters[parameterIndex].value.Split(';');

            return new Vector3
            {
                x = float.Parse(vectorAsString[0]),
                y = float.Parse(vectorAsString[1]),
                z = float.Parse(vectorAsString[2])
            };
        }
        public float GetFloat(int parameterIndex)
        {
            return float.Parse(parameters[parameterIndex].value);
        }
        public AnimationMechanic GetAnimationMechanic()
        {
            return (AnimationMechanic) Enum.Parse(typeof(AnimationMechanic), mechanism);
        }
    }
    
    public enum AnimationMechanic
    {
        Spawn,
        Rotate,
        Walk,
        Talk,
        Sit,
        Lay
    }
}