using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessController : MonoBehaviour
{
    [SerializeField]
    private Volume volume;
    [SerializeField]
    private ColorCurves colorCurves;
    [SerializeField]
    //private Keyframe value;
    float value;

    public float initValue = 0.5f;
    public float speed = 1f;
    //private VolumeParameter value;
    // Start is called before the first frame update
    void Start()
    {
        
        volume = GetComponent<Volume>();
        volume.profile.TryGet<ColorCurves>(out colorCurves);
        value = initValue;
        //value = colorCurves.hueVsHue.value[0];
        //colorCurves = volume.GetComponent<ColorCurves>();
    }

    // Update is called once per frame
    void Update()
    {
        colorCurves.SetDirty();
        value += 0.001f * speed;
        if (value >= 1.0f)
        {
            value = initValue;
        }
        colorCurves.hueVsHue.value.MoveKey(0, new Keyframe(0f, value));
    }
}
