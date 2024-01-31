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
    private Keyframe value;
    //private VolumeParameter value;
    // Start is called before the first frame update
    void Start()
    {
        
        volume = GetComponent<Volume>();
        volume.profile.TryGet<ColorCurves>(out colorCurves);
        value = colorCurves.hueVsHue.value[0];
        //colorCurves = volume.GetComponent<ColorCurves>();
    }

    // Update is called once per frame
    void Update()
    {
        value.inWeight += 0.01f;
        //colorCurves.hueVsHue.value.MoveKey(0, value);
    }
}
