using UnityEngine;
using UnityEngine.Rendering;

public class DisableLeak : MonoBehaviour
{
    public Animator PipeLeakAnim;
    public ParticleSystem PipeLeak, Ambiance;
    public GameObject EffectHolder;
    public GameObject Tie;
    public GameObject Player;
    public LightController controller;
    public bool IsPumpOff { private set; get; }
    public bool IsFixed { private set; get; }

    // effects vars
    Volume effects;
    AudioLowPassFilter lowPass;
    AudioReverbFilter reverb;
    AudioEchoFilter echo;

    void Start()
    {
        effects = EffectHolder.GetComponent<Volume>();
        lowPass = EffectHolder.GetComponent<AudioLowPassFilter>();
        reverb = EffectHolder.GetComponent<AudioReverbFilter>();
        echo = EffectHolder.GetComponent<AudioEchoFilter>();
    }

    public void Disable(bool full)
    {
        if (!full) {
            PipeLeakAnim.SetTrigger("reduce");
            Ambiance.Stop();
            Ambiance.Clear();
            effects.enabled = false;
            Player.transform.SetParent(Tie.transform); // make tie player parent so no clipping out during sudden rotation zero
            Tie.GetComponent<Animator>().SetTrigger("Stop");
            Quaternion q = Tie.transform.rotation;
            q.eulerAngles = Vector3.zero;
            Tie.transform.rotation = q;
            IsPumpOff = true;
            controller.BulkState(false);
            RenderSettings.ambientLight = Color.black;
        }
        else
        {
            PipeLeak.Stop();
            PipeLeak.Clear();
            lowPass.enabled = false;
            reverb.enabled = false;
            echo.enabled = false;
            IsFixed = true;
        }
    }
}
