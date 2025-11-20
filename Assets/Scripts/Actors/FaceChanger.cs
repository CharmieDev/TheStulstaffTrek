using System;
using NaughtyAttributes;
using UnityEngine;


public enum Faces
{
    None,
    Idle,
    Suspicious,
    Pressured
}
public class FaceChanger : MonoBehaviour
{
    [SerializeField] Material faceMaterial;

    public void OnEnable()
    {
        DialogueManager.Instance.OnFaceChanged.AddListener(OnFaceChanged);
    }

    public void OnDisable()
    {
        DialogueManager.Instance.OnFaceChanged.RemoveListener(OnFaceChanged);
    }

    private void OnFaceChanged()
    {
        ChangeFace(DialogueManager.Instance.CurrentFace);
    }

    public void ChangeFace(Faces face)
    {
        if (face == Faces.None) return;
        
        faceMaterial.SetFloat("_Tile", (int)face - 1);
    }

    [Button]
    public void TestChangeFace()
    {
        ChangeFace(Faces.Suspicious);
    }
}
