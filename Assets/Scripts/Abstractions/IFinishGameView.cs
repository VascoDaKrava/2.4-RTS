using Abstractions.Enums;

namespace Abstractions
{
    public interface IFinishGameView
    {
        void SetWin(FactionID factionID);
        bool ShowMessage { set; }
    }
}