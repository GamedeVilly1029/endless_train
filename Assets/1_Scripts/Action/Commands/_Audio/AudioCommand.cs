using UnityEngine;

public class AudioCommand : BaseActionCommand
{
    protected AudioMaster _audioMaster;

    public AudioCommand
    (
        AudioMaster audioMaster
    )
    {
        _audioMaster = audioMaster;
    }

    public override void Execute()
    {
        base.Execute();
    }
}