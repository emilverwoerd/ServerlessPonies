using Amolenk.ServerlessPonies.ClientApplication.Scenes;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amolenk.ServerlessPonies.ClientApplication.Phaser
{
    public interface IPhaserTextInterop
    {
        IPhaserTextInterop Value(string text);

        IPhaserTextInterop WithOrigin(double x, double y);
    }
}