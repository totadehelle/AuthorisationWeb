using System;

namespace AuthorizationWeb.Controllers
{
    public interface IModule
    {
        void Input();
        event Action OnQuit;
    }
}