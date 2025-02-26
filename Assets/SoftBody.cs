using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class SoftBody : MonoBehaviour
{
    [SerializeField] private float outwardOffset = 0.05f; // par exemple

    [SerializeField]
    public SpriteShapeController spriteShape;
    [SerializeField]
    public Transform[] points;
    void Awake()
    {
        UpdateVerticies();
    }

    void Update()
    {
        UpdateVerticies();

    }

    private void UpdateVerticies()
    {
        for (int i = 0; i < points.Length; i++)
        {

            Vector2 _vertex = points[i].localPosition;

            // Direction depuis le point vers le centre
            Vector2 _towardsCenter = (Vector2.zero - _vertex).normalized;

            // Rayon (en tenant compte du scale global si besoin)
            CircleCollider2D col = points[i].GetComponent<CircleCollider2D>();
            float _colliderRadius = col.radius * points[i].lossyScale.x;
            // ou .y si c'est un scale uniforme, à ajuster selon vos besoins

            // On ramène le point vers le centre d'une distance égale au rayon
            // (au lieu de l'éloigner)
            Vector2 splinePos = _vertex - _towardsCenter * (_colliderRadius + outwardOffset);
            try
            {
                spriteShape.spline.SetPosition(i, splinePos);

            }
            catch
            {

                spriteShape.spline.SetPosition(i, splinePos);
            }

            Vector2 _lt = spriteShape.spline.GetLeftTangent(i);
            Vector2 _newRt = Vector2.Perpendicular(_towardsCenter) * _lt.magnitude;
            Vector2 _newLt = -_newRt;

            spriteShape.spline.SetRightTangent(i, _newRt);
            spriteShape.spline.SetLeftTangent(i, _newLt);

        }
    }
}
