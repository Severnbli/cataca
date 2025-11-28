using _Project.Scripts.Core.Systems.Interfaces;
using _Project.Scripts.Features.Mechanics.Input.Configs;
using _Project.Scripts.Features.Mechanics.Input.Services;
using Leopotam.EcsLite;
using UnityEngine;

namespace _Project.Scripts.Features.Mechanics.Input.Systems
{
    public class InputServiceUpdateSystem : IEcsRunSystem, IEcsGameSystem
    {
        public InputServiceUpdateSystem(InputService inputService, InputControlsConfig controlsConfig)
        {
            _inputService = inputService;
            _controlsConfig = controlsConfig;
        }
        
        private InputService _inputService;
        private InputControlsConfig _controlsConfig;
            
        public void Run(IEcsSystems systems)
        {
            _inputService.Walk = _controlsConfig.Walk.action.ReadValue<Vector2>();
            _inputService.Jump = _controlsConfig.Jump.action.triggered;
            _inputService.Dash = _controlsConfig.Dash.action.triggered;
            _inputService.Position = _controlsConfig.Position.action.triggered;
            _inputService.Rotation = _controlsConfig.Rotation.action.triggered;
            _inputService.Scale = _controlsConfig.Scale.action.triggered;
        }
    }
}