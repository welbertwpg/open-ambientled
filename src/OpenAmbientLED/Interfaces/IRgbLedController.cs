namespace OpenAmbientLED.Interfaces
{
    public interface IRgbLedController : ILedController
    {
        void SetColor(uint color);
    }
}
