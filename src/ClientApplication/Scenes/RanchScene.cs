using Amolenk.ServerlessPonies.Messages;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApplication.Scenes
{
    public class RanchScene : Scene,
        IEventHandler<AnimalPlacedEvent>,
        IEventHandler<AnimalMoodChangedEvent>
    {
        public const string Name = "Ranch";

        public override string GetName() => Name;

        [JSInvokable("create")]
        public override void Create()
        {
            Phaser(interop =>
            {
                interop
                    .AddSprite("world", "world", 400, 300, 0.2)
                    .AddSprite("button", "brush", 100, 125, 0.2)
                    .OnPointerUp("button", nameof(Button_PointerUp));

                foreach (var enclosure in State.Enclosures.Where(enclosure => enclosure.AnimalId != null))
                {
                    var animalSpriteName = SpriteName.Create("animal", enclosure.AnimalId);
                    
                    interop
                        .AddSprite(animalSpriteName, "logo", 320, 80, 0.2)
                        .OnPointerUp(animalSpriteName, nameof(Animal_PointerUp));
                }
            });
        }

        [JSInvokable]
        public Task Button_PointerUp(string spriteName)
        {
            State.SelectedEnclosureId = "1";

            Phaser(interop => interop.SwitchToScene(AnimalManagementScene.Name));

            return Task.CompletedTask;
        }

        [JSInvokable]
        public Task Animal_PointerUp(string spriteName)
        {
//            State.SelectedEnclosureId = "1";

            Phaser(interop => interop.SwitchToScene(AnimalCareScene.Name));

            return Task.CompletedTask;
        }

        public void Handle(AnimalPlacedEvent @event)
        {
            var enclosure = State.Enclosures.Find(enclosure => enclosure.Id == @event.EnclosureId);
            enclosure.AnimalId = @event.AnimalId;
        }

        public void Handle(AnimalMoodChangedEvent @event)
        {
            // TODO Update in state.

            // TODO Update on screen.

            Console.WriteLine($"Animal mood changed: [happiness: {@event.HappinessLevel}] [hungriness: {@event.HungrinessLevel}] [thirstiness: {@event.ThirstinessLevel}]");
        }
    }
}