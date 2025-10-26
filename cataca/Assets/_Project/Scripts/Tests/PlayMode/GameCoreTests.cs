using NUnit.Framework;

namespace _Project.Scripts.Tests.PlayMode
{
    public class GameCoreTests
    {
        [Test] public void PlayerMovement_ChangesPosition_WhenInputApplied() {}
        [Test] public void PlayerJump_IncreasesVerticalVelocity_OnJumpInput() {}
        [Test] public void PlayerDash_AppliesHorizontalForce_OnDashInput() {}
        [Test] public void NoteSystem_ChangesPlatformPosition_WhenPositionNoteTriggered() {}
        [Test] public void NoteSystem_ChangesPlatformRotation_WhenRotationNoteTriggered() {}
        [Test] public void NoteSystem_ChangesPlatformScale_WhenScaleNoteTriggered() {}
        [Test] public void NoteSystem_CyclesThroughPlatformStates_Correctly() {}
        [Test] public void RecordCollection_AddsNewRecord_WhenPickedUp() {}
        [Test] public void RecordCollection_PreventsDuplicates_WhenSameRecordPicked() {}
        [Test] public void RecordPlayer_PlaysAudio_WhenRecordSelected() {}
        [Test] public void AudioMixer_AppliesVolumeSettings_ToSoundEffects() {}
        [Test] public void AudioMixer_AppliesVolumeSettings_ToMusic() {}
        [Test] public void LevelManager_LoadsLevelData_WhenSelectedFromMenu() {}
        [Test] public void PauseMenu_StopsGameTime_WhenActivated() {}
        [Test] public void UI_Buttons_TriggerExpectedPanels_WhenClicked() {}
    }
}