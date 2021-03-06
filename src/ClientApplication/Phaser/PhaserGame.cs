using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Amolenk.ServerlessPonies.ClientApplication.Scenes;

namespace Amolenk.ServerlessPonies.ClientApplication.Phaser
{
    public class PhaserGame
    {
        private readonly IPhaserInterop _phaser;
        private readonly IStateManager _stateManager;
        private readonly Dictionary<string, Scene> _scenes;
        private readonly IDictionary<Type, Action<object, IStateManager>> _handlers;

        public PhaserGame(IPhaserInterop phaser, IStateManager stateManager, IEnumerable<Scene> scenes,
            IDictionary<Type, Action<object, IStateManager>> handlers)
        {
            _phaser = phaser;
            _stateManager = stateManager;
            _scenes = scenes.ToDictionary(scene => scene.GetName());
            _handlers = handlers;
        }

        public void Start(string containerElement, string title)
        {
            _phaser.Start(containerElement, title);
        }

        public void HandleEvent(object @event)
        {
            if (_handlers.TryGetValue(@event.GetType(), out Action<object, IStateManager> handler))
            {
                handler(@event, _stateManager);
            }
        }
    }
}