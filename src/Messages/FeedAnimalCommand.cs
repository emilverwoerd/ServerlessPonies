﻿using System;

namespace Amolenk.ServerlessPonies.Messages
{
    public class FeedAnimalCommand
    {
        public string PlayerName { get; set; }

        public string AnimalName { get; set; }
    }
}
