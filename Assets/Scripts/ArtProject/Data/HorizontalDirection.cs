using System;
using UnityEngine;

namespace ArtProject.Data
{
    public class HorizontalDirection
    {
        private readonly float _direction;

        public HorizontalDirection(float direction)
        {
            _direction = direction;
        }

        public float FlipX(float x)
        {
            return _direction switch
            {
                1 => Mathf.Abs(x),
                -1 => -Mathf.Abs(x),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}