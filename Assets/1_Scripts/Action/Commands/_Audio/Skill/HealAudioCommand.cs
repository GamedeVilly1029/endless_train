using UnityEngine;

public class HealAudioCommand : AudioCommand
{
    public HealAudioCommand(AudioMaster audioMaster) : base(audioMaster)
    {
    }
    public override void Execute()
    {
        _audioMaster.PlaySound("heal");
    }
}
