namespace App.Scripts.Features.FeatureBase.Engine
{
    public interface IFeaturesEngine
    {
        public void Launch();
        public void Update();
        public void FixedUpdate();
        public void Dispose();
    }
}